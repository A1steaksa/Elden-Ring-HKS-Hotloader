using System;
using System.Text;

namespace EldenRingCodeHotloader.LiveRefresh {
	public static class RequestFileReload {

		public static bool RequestReloadChr( string chrName ) {
			byte[] chrNameBytes = Encoding.Unicode.GetBytes( chrName );
			
			try {
				Memory.AttachProc( "eldenring" );
				if( Memory.ProcessHandle == IntPtr.Zero )
					Memory.AttachProc( "start_protected_game" );
				
				if( Memory.ProcessHandle != IntPtr.Zero ) {
					var fileInfo = Memory.AttachedProcess.MainModule.FileVersionInfo;
					int gameVersion = fileInfo.FileMajorPart * 1_00_00_00
					                  + fileInfo.FileMinorPart * 1_00_00
					                  + fileInfo.FileBuildPart * 1_00
					                  + fileInfo.FilePrivatePart;

					var chrReload = Kernel32.VirtualAllocEx( Memory.ProcessHandle, IntPtr.Zero, 256, 0x1000 | 0x2000, 0x40 );
					var chrReload_DataSetup = Kernel32.VirtualAllocEx( Memory.ProcessHandle, IntPtr.Zero, 256, 0x1000 | 0x2000, 0x40 );

					if( chrReload != IntPtr.Zero && chrReload_DataSetup != IntPtr.Zero ) {
						try {
							Memory.UpdateEldenRingAobs();

							var dataPointer = Memory.ReadInt64( (IntPtr) Memory.ReadInt64( Memory.EldenRing_WorldChrManPtr + ( Memory.GetIngameReloadIniOptionIntHex( "EldenRing_WorldChrManStructOffset" ) ?? 0x1E668 ) ) + 0x0 );

							Memory.WriteInt64( chrReload_DataSetup + 0x8, dataPointer ); // Pointer to data
							Memory.WriteInt64( chrReload_DataSetup + 0x58, ( chrReload_DataSetup + 0x100 ).ToInt64() ); // Pointer to string
							Memory.WriteInt8( chrReload_DataSetup + 0x70, 0x1F ); // String length
							Memory.WriteBytes( chrReload_DataSetup + 0x100, chrNameBytes );

							// Crash fix offset, last updated for 1.05
							var writeBytes = Memory.GetIngameReloadIniOptionByteArrayHex( "EldenRing_CrashPatchOffset_WriteBytes" ) ?? new byte[] { 0x48, 0x31, 0xD2 };
							Memory.WriteBytes( Memory.EldenRing_CrashFixPtr, writeBytes );

							byte[] buffer = null;

							if( gameVersion >= 01_07_00_00 ) {
								buffer = new byte[] {
									0x48, 0xBB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // mov rbx,0000000000000000 (ChrReload_DataSetup)
									0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // mov rcx,0000000000000000 (WorldChrMan)
									0x48, 0x8B, 0x91, 0x68, 0xE6, 0x01, 0x00, // mov rdx,[rcx+0001E668]
									0x48, 0x89, 0x1A, // mov [rdx],rbx
									0x48, 0x89, 0x13, // mov [rbx],rdx
									0x48, 0x8B, 0x91, 0x68, 0xE6, 0x01, 0x00, // mov rdx,[rcx+0001E668]
									0x48, 0x89, 0x5A, 0x08, // mov [rdx+08],rbx
									0x48, 0x89, 0x53, 0x08, // mov [rbx+08],rdx
									0xC7, 0x81, 0x70, 0xE6, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, // mov [rcx+0001E670],00000001 { 1 }
									0xC7, 0x81, 0x78, 0xE6, 0x01, 0x00, 0x00, 0x00, 0x20, 0x41, // mov [rcx+0001E678],41200000 { 10.00 }
									0xC3, // ret 
								};
							} else {
								buffer = new byte[] {
									0x48, 0xBB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // mov rbx,0000000000000000 (ChrReload_DataSetup)
									0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // mov rcx,0000000000000000 (WorldChrMan)
									0x48, 0x8B, 0x91, 0xC0, 0x85, 0x01, 0x00, // mov rdx,[rcx+000185C0]
									0x48, 0x89, 0x1A, // mov [rdx],rbx
									0x48, 0x89, 0x13, // mov [rbx],rdx
									0x48, 0x8B, 0x91, 0xC0, 0x85, 0x01, 0x00, // mov rdx,[rcx+000185C0]
									0x48, 0x89, 0x5A, 0x08, // mov [rdx+08],rbx
									0x48, 0x89, 0x53, 0x08, // mov [rbx+08],rdx
									0xC7, 0x81, 0xC8, 0x85, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, // mov [rcx+000185C8],00000001 { 1 }
									0xC7, 0x81, 0xD0, 0x85, 0x01, 0x00, 0x00, 0x00, 0x20, 0x41, // mov [rcx+000185D0],41200000 { 10.00 }
									0xC3, // ret 
								};
							}

							Array.Copy( BitConverter.GetBytes( chrReload_DataSetup.ToInt64() ), 0, buffer, 0x2, 0x8 );
							Array.Copy( BitConverter.GetBytes( Memory.EldenRing_WorldChrManPtr.ToInt64() ), 0, buffer, 0xC, 0x8 );


							Memory.WriteBytes( chrReload, buffer );

							var threadHandle = Kernel32.CreateRemoteThread( Memory.ProcessHandle, IntPtr.Zero, 0, chrReload, IntPtr.Zero, 0, out var threadId );
							if( threadHandle != IntPtr.Zero ) {
								Kernel32.WaitForSingleObject( threadHandle, 30000 );
							}

							return true;
						} finally {
							Kernel32.VirtualFreeEx( Memory.ProcessHandle, chrReload, 256, 2 );
							Kernel32.VirtualFreeEx( Memory.ProcessHandle, chrReload_DataSetup, 256, 2 );
						}
					}
				} else {
					MainForm.Instance.SetHotloadStatus( MainForm.HotloadStatus.Failed, "Unable to find process" );
				}
			} catch( Exception e ) {
				MainForm.Instance.SetHotloadStatus( MainForm.HotloadStatus.Failed, e.Message );
			}

			return false;
		}
	}
}