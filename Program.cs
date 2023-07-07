﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EldenRingCodeHotloader {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new MainForm() );
		}
		
		public static string GetExecutingDirectory() {
			return new FileInfo( typeof( Program ).Assembly.Location ).DirectoryName;
		}

	}
}