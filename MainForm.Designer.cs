namespace EldenRingCodeHotloader {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}

			base.Dispose( disposing );
		}

	#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.FilePathTextBox = new System.Windows.Forms.TextBox();
			this.FileWatcher = new System.IO.FileSystemWatcher();
			this.FilePathLabel = new System.Windows.Forms.Label();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.HotloadButton = new System.Windows.Forms.Button();
			this.HotloadAutomaticallyCheckbox = new System.Windows.Forms.CheckBox();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.CreditLink = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SplitContainer = new System.Windows.Forms.SplitContainer();
			this.HotloadLog = new System.Windows.Forms.ListBox();
			this.UpdatedFilesList = new System.Windows.Forms.ListBox();
			( (System.ComponentModel.ISupportInitialize) ( this.FileWatcher ) ).BeginInit();
			this.panel1.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.SplitContainer ) ).BeginInit();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// FilePathTextBox
			// 
			this.FilePathTextBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.FilePathTextBox.Enabled = false;
			this.FilePathTextBox.Location = new System.Drawing.Point( 12, 26 );
			this.FilePathTextBox.Name = "FilePathTextBox";
			this.FilePathTextBox.Size = new System.Drawing.Size( 590, 26 );
			this.FilePathTextBox.TabIndex = 4;
			this.FilePathTextBox.TextChanged += new System.EventHandler( this.OnMonitoredPathChanged );
			// 
			// FileWatcher
			// 
			this.FileWatcher.EnableRaisingEvents = true;
			this.FileWatcher.Filter = "*.hks";
			this.FileWatcher.SynchronizingObject = this;
			this.FileWatcher.Changed += new System.IO.FileSystemEventHandler( this.OnMonitoredFileChanged );
			// 
			// FilePathLabel
			// 
			this.FilePathLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.FilePathLabel.Location = new System.Drawing.Point( 12, 3 );
			this.FilePathLabel.Name = "FilePathLabel";
			this.FilePathLabel.Size = new System.Drawing.Size( 680, 23 );
			this.FilePathLabel.TabIndex = 5;
			this.FilePathLabel.Text = "Monitored Path";
			// 
			// BrowseButton
			// 
			this.BrowseButton.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.BrowseButton.Enabled = false;
			this.BrowseButton.Location = new System.Drawing.Point( 608, 22 );
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size( 84, 35 );
			this.BrowseButton.TabIndex = 6;
			this.BrowseButton.Text = "Browse";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler( this.OnBrowseClicked );
			// 
			// HotloadButton
			// 
			this.HotloadButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.HotloadButton.Enabled = false;
			this.HotloadButton.Location = new System.Drawing.Point( 0, 367 );
			this.HotloadButton.Name = "HotloadButton";
			this.HotloadButton.Size = new System.Drawing.Size( 704, 74 );
			this.HotloadButton.TabIndex = 3;
			this.HotloadButton.Text = "Hotload Now";
			this.HotloadButton.UseVisualStyleBackColor = true;
			this.HotloadButton.Click += new System.EventHandler( this.OnHotloadClicked );
			// 
			// HotloadAutomaticallyCheckbox
			// 
			this.HotloadAutomaticallyCheckbox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.HotloadAutomaticallyCheckbox.Enabled = false;
			this.HotloadAutomaticallyCheckbox.Location = new System.Drawing.Point( 12, 58 );
			this.HotloadAutomaticallyCheckbox.Name = "HotloadAutomaticallyCheckbox";
			this.HotloadAutomaticallyCheckbox.Size = new System.Drawing.Size( 387, 24 );
			this.HotloadAutomaticallyCheckbox.TabIndex = 7;
			this.HotloadAutomaticallyCheckbox.Text = "Hotload automatically on script save";
			this.HotloadAutomaticallyCheckbox.UseVisualStyleBackColor = true;
			// 
			// StatusLabel
			// 
			this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.StatusLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
			this.StatusLabel.Location = new System.Drawing.Point( 0, 0 );
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size( 704, 51 );
			this.StatusLabel.TabIndex = 8;
			this.StatusLabel.Text = "Select .hks Script Directory";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.Controls.Add( this.CreditLink );
			this.panel1.Controls.Add( this.BrowseButton );
			this.panel1.Controls.Add( this.FilePathTextBox );
			this.panel1.Controls.Add( this.HotloadAutomaticallyCheckbox );
			this.panel1.Controls.Add( this.FilePathLabel );
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point( 0, 278 );
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size( 704, 89 );
			this.panel1.TabIndex = 10;
			// 
			// CreditLink
			// 
			this.CreditLink.ActiveLinkColor = System.Drawing.Color.Black;
			this.CreditLink.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
			this.CreditLink.LinkArea = new System.Windows.Forms.LinkArea( 0, 100 );
			this.CreditLink.LinkColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 100 ) ) ) ), ( (int) ( ( (byte) ( 100 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
			this.CreditLink.Location = new System.Drawing.Point( 449, 59 );
			this.CreditLink.Name = "CreditLink";
			this.CreditLink.Size = new System.Drawing.Size( 243, 23 );
			this.CreditLink.TabIndex = 8;
			this.CreditLink.TabStop = true;
			this.CreditLink.Text = "Hotload credit to Meowmaritus";
			this.CreditLink.UseCompatibleTextRendering = true;
			this.CreditLink.VisitedLinkColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 100 ) ) ) ), ( (int) ( ( (byte) ( 100 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
			this.CreditLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.OnCreditLinkClicked );
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
			this.label1.Location = new System.Drawing.Point( 0, 0 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 336, 35 );
			this.label1.TabIndex = 11;
			this.label1.Text = "Hotload Log";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font( "Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
			this.label2.Location = new System.Drawing.Point( 0, 0 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 364, 35 );
			this.label2.TabIndex = 12;
			this.label2.Text = "Files Ready for Hotload";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SplitContainer
			// 
			this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer.Location = new System.Drawing.Point( 0, 51 );
			this.SplitContainer.Name = "SplitContainer";
			// 
			// SplitContainer.Panel1
			// 
			this.SplitContainer.Panel1.Controls.Add( this.HotloadLog );
			this.SplitContainer.Panel1.Controls.Add( this.label1 );
			// 
			// SplitContainer.Panel2
			// 
			this.SplitContainer.Panel2.Controls.Add( this.UpdatedFilesList );
			this.SplitContainer.Panel2.Controls.Add( this.label2 );
			this.SplitContainer.Size = new System.Drawing.Size( 704, 227 );
			this.SplitContainer.SplitterDistance = 336;
			this.SplitContainer.TabIndex = 13;
			// 
			// HotloadLog
			// 
			this.HotloadLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HotloadLog.Enabled = false;
			this.HotloadLog.FormattingEnabled = true;
			this.HotloadLog.ItemHeight = 20;
			this.HotloadLog.Location = new System.Drawing.Point( 0, 35 );
			this.HotloadLog.Name = "HotloadLog";
			this.HotloadLog.Size = new System.Drawing.Size( 336, 192 );
			this.HotloadLog.TabIndex = 12;
			// 
			// UpdatedFilesList
			// 
			this.UpdatedFilesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UpdatedFilesList.Enabled = false;
			this.UpdatedFilesList.FormattingEnabled = true;
			this.UpdatedFilesList.ItemHeight = 20;
			this.UpdatedFilesList.Location = new System.Drawing.Point( 0, 35 );
			this.UpdatedFilesList.Name = "UpdatedFilesList";
			this.UpdatedFilesList.Size = new System.Drawing.Size( 364, 192 );
			this.UpdatedFilesList.TabIndex = 13;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 144F, 144F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size( 704, 441 );
			this.Controls.Add( this.SplitContainer );
			this.Controls.Add( this.panel1 );
			this.Controls.Add( this.StatusLabel );
			this.Controls.Add( this.HotloadButton );
			this.Location = new System.Drawing.Point( 15, 15 );
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Elden Ring HKS Script Hotloader";
			this.Load += new System.EventHandler( this.OnFormLoad );
			( (System.ComponentModel.ISupportInitialize) ( this.FileWatcher ) ).EndInit();
			this.panel1.ResumeLayout( false );
			this.panel1.PerformLayout();
			this.SplitContainer.Panel1.ResumeLayout( false );
			this.SplitContainer.Panel2.ResumeLayout( false );
			( (System.ComponentModel.ISupportInitialize) ( this.SplitContainer ) ).EndInit();
			this.SplitContainer.ResumeLayout( false );
			this.ResumeLayout( false );
		}

		private System.Windows.Forms.LinkLabel CreditLink;

		private System.Windows.Forms.ListBox UpdatedFilesList;

		private System.Windows.Forms.ListBox HotloadLog;

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer SplitContainer;

		private System.Windows.Forms.Label label1;

		private System.Windows.Forms.Panel panel1;

		private System.Windows.Forms.Label StatusLabel;

		private System.Windows.Forms.CheckBox HotloadAutomaticallyCheckbox;

		private System.Windows.Forms.Button BrowseButton;

		private System.Windows.Forms.Label FilePathLabel;

		private System.IO.FileSystemWatcher FileWatcher;

		private System.Windows.Forms.TextBox FilePathTextBox;

		private System.Windows.Forms.Button HotloadButton;

	#endregion
	}
}