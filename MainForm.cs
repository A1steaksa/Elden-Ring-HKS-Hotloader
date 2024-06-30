using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EldenRingCodeHotloader.LiveRefresh;
using WK.Libraries.BetterFolderBrowserNS;
using Timer = System.Threading.Timer;

namespace EldenRingCodeHotloader {
	public partial class MainForm : Form {
		
		public static MainForm Instance { get; private set; }

		Dictionary<HotloadStatus, Color> _hotloadStatusColors = new() {
			{ HotloadStatus.Disabled, Color.DarkSlateGray },
			{ HotloadStatus.NeverHotloaded, Color.Orange },
			{ HotloadStatus.HasHotloaded, Color.Green },
			{ HotloadStatus.Failed, Color.Red }
		};

		public enum HotloadStatus {
			Disabled,
			NeverHotloaded,
			HasHotloaded,
			Failed
		}
		
		private HotloadStatus _hotloadStatus = HotloadStatus.Disabled;
		private string _lastHotloadedFileName = "";
		private DateTime _lastHotloadTime = DateTime.MinValue;

		private Dictionary<string, int> _updatedFiles = new();

		private Timer _lastHotloadUpdateTimer;
		
		private static string lastDirectoryFileName = "lastDirectory.txt";
		
		public MainForm() {
			Instance = this;
			
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}
		
		private void OnFormLoad( object sender, EventArgs e ) {
			BrowseButton.Enabled = true;
			FilePathTextBox.Enabled = true;
			
			FileWatcher.Path = Program.GetExecutingDirectory();
			
			_lastHotloadUpdateTimer = new Timer( OnHotloadTimerTick, null, 0, 1000 );

			SetHotloadStatus( HotloadStatus.Disabled );
			
			LoadLastDirectory();
		}
		
		// Saves a given path in a text file in the same directory as the executable
		public void SaveLastDirectory( string path ) {
			string lastDirectoryPath = Path.Combine( Program.GetExecutingDirectory(), lastDirectoryFileName );
			File.WriteAllText( lastDirectoryPath, path );
		}

		// Loads a saved path, if it exists
		public void LoadLastDirectory() {
			string lastDirectoryPath = Path.Combine( Program.GetExecutingDirectory(), lastDirectoryFileName );
			if( File.Exists( lastDirectoryPath ) ) {
				string lastDirectory = File.ReadAllText( lastDirectoryPath );
				FilePathTextBox.Text = lastDirectory;
				FileWatcher.Path = lastDirectory;
				SetHotloadStatus( HotloadStatus.NeverHotloaded );
			}
		}

		public void Log( string text ) {
			HotloadLog.Invoke( new Action( () => {
				HotloadLog.Items.Add( DateTime.Now.ToShortTimeString() + " - " + text );
			} ) );	
		}

		public void SetHotloadStatus( HotloadStatus status, string logMessage = "" ) {
			_hotloadStatus = status;

			bool hotloadButtonEnabled = false;
			bool hotloadAutomaticallyCheckboxEnabled = false;
			string statusLabelText = "";

			switch( status ) {
				case HotloadStatus.Disabled:
					hotloadButtonEnabled = false;
					hotloadAutomaticallyCheckboxEnabled = false;

					statusLabelText = "Select a script folder to enable hotloading";
					
					break;
				case HotloadStatus.NeverHotloaded:
					hotloadButtonEnabled = false;
					hotloadAutomaticallyCheckboxEnabled = true;

					statusLabelText = "Waiting for script changes";
					
					break;
				case HotloadStatus.HasHotloaded:
					hotloadButtonEnabled = false;
					hotloadAutomaticallyCheckboxEnabled = true;

					break;
				case HotloadStatus.Failed:
					hotloadButtonEnabled = false;
					hotloadAutomaticallyCheckboxEnabled = true;

					statusLabelText = "Hotload failed";
					
					break;
			}

			// Update the UI
			HotloadButton.Invoke( new Action( () => HotloadButton.Enabled = hotloadButtonEnabled ) );
			HotloadAutomaticallyCheckbox.Invoke( new Action( () => HotloadAutomaticallyCheckbox.Enabled = hotloadAutomaticallyCheckboxEnabled ) );
			if( statusLabelText.Length > 0 ) {
				StatusLabel.Invoke( new Action( () => StatusLabel.Text = statusLabelText ) );
			}

			if( logMessage.Length > 0 ) {
				Log( logMessage );
			}
			
			StatusLabel.ForeColor = _hotloadStatusColors[status];
		}

		private void HotloadMultiple( List<string> filePaths ) {
			foreach( string path in filePaths ) {
				Hotload( path );
			}
		}
		
		private void Hotload( string path ) {
			
			FileInfo file = new FileInfo( path );

			if( !File.Exists( file.FullName ) ) {
				Log( $"File {file.Name} does not exist" );
				SetHotloadStatus( HotloadStatus.Failed );
				return;
			}

			string chrName = file.Name.Replace( ".hks", "" );
			
			_lastHotloadedFileName = file.Name;
			_lastHotloadTime = DateTime.Now;
			SetHotloadStatus( HotloadStatus.HasHotloaded );
			
			if( RequestFileReload.RequestReloadChr( chrName ) ) {
				Log( $"Hotloaded {file.Name}" );	
			} else {
				Log( $"Failed to hotload {file.Name}" );
			}
		}
		
		private void OnMonitoredFileChanged( object sender, FileSystemEventArgs e ) {
			if( e.ChangeType != WatcherChangeTypes.Changed ) {
				return;
			}
			
			FileInfo changedFile = new FileInfo( e.FullPath );
			
			if( !_updatedFiles.ContainsKey( changedFile.FullName ) ) {
				_updatedFiles.Add( changedFile.FullName, 0 );
			}

			_updatedFiles[changedFile.FullName]++;

			UpdatedFilesList.Items.Clear();

			foreach( KeyValuePair<string,int> pair in _updatedFiles ) {
				UpdatedFilesList.Items.Add( new FileInfo( pair.Key ).Name + " (" + (pair.Value/2) + ")" );
			}
				
			HotloadButton.Enabled = true;
		}

		private void OnBrowseClicked( object sender, EventArgs e ) {
			BetterFolderBrowser folderBrowser = new BetterFolderBrowser();
			folderBrowser.Title = "Select Elden Ring mod folder containing .hks files";

			if( FilePathTextBox.Text.Trim().Length != 0 && Directory.Exists( FilePathTextBox.Text ) ) {
				folderBrowser.RootFolder = FilePathTextBox.Text;
			} else {
				folderBrowser.RootFolder = Program.GetExecutingDirectory();
			}
			
			folderBrowser.Multiselect = false;
			DialogResult result = folderBrowser.ShowDialog();
			
			if( result == DialogResult.OK ) {
				FilePathTextBox.Text = folderBrowser.SelectedFolder;
				FileWatcher.Path = folderBrowser.SelectedFolder;
				SaveLastDirectory( folderBrowser.SelectedFolder );
				SetHotloadStatus( HotloadStatus.NeverHotloaded );
			}
		}

		private void OnMonitoredPathChanged( object sender, EventArgs e ) {
			SetHotloadStatus( HotloadStatus.Disabled );

			if( FilePathTextBox.Text.Length == 0 ) {
				HotloadAutomaticallyCheckbox.Checked = false;
				return;
			}
			
			if( !Directory.Exists( FilePathTextBox.Text ) ) {
				HotloadAutomaticallyCheckbox.Checked = false;
				return;
			}
			
			FileWatcher.Path = FilePathTextBox.Text;
			SetHotloadStatus( HotloadStatus.NeverHotloaded );
		}

		private void OnHotloadClicked( object sender, EventArgs e ) {
			HotloadButton.Enabled = false;
			if( _updatedFiles.Count == 0 ) {
				return;
			}
			HotloadMultiple( _updatedFiles.Keys.ToList() );
			_updatedFiles.Clear();
			UpdatedFilesList.Items.Clear();
		}
		
		private void OnHotloadTimerTick( object state ) {
			if( _hotloadStatus == HotloadStatus.HasHotloaded ) {
				TimeSpan timeSinceLastHotload = DateTime.Now - _lastHotloadTime;
				StatusLabel.Invoke( new Action( () => StatusLabel.Text = $"Hotloaded {_lastHotloadedFileName} {timeSinceLastHotload.TotalSeconds:0}s ago" ) );
			}
			
			bool hasFilesToHotload = _updatedFiles.Count > 0;
			if( hasFilesToHotload && HotloadAutomaticallyCheckbox.Checked ) {
				HotloadMultiple( _updatedFiles.Keys.ToList() );
				_updatedFiles.Clear();

				UpdatedFilesList.Invoke( new Action( () => UpdatedFilesList.Items.Clear() ) );
				HotloadButton.Invoke( new Action( () => HotloadButton.Enabled = false ) );
			}
			
		}

		private void OnCreditLinkClicked( object sender, EventArgs e ) {
			Process.Start( "https://github.com/Meowmaritus/" );
		}

	}
}