﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HTN.BITS.MCS.SCN.LOCKDOWN
{
    static class CreateLaunchApp
    {
        public class ProcessInfo
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public IntPtr ProcessID;
            public IntPtr ThreadID;
        }

        [DllImport("CoreDll.DLL", SetLastError = true)]
        private static extern int CreateProcess(String imageName, String cmdLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, Int32 boolInheritHandles, Int32 dwCreationFlags, IntPtr lpEnvironment, IntPtr lpszCurrentDir, byte[] si, ProcessInfo pi);

        [DllImport("coredll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("coredll")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int GetExitCodeProcess(IntPtr hProcess, ref int lpExitCode);

        public static void LaunchApp(string strPath, string strParms)
        {
            ProcessInfo pi = new ProcessInfo();
            byte[] si = new byte[128];
            CreateProcess(strPath, strParms, IntPtr.Zero, IntPtr.Zero, 0, 0, IntPtr.Zero, IntPtr.Zero, si, pi);
            // This line can be commented out if you do not want to wait for the process to exit
            WaitForSingleObject(pi.hProcess, 0xFFFFFFFF);
            int exitCode = 0;
            GetExitCodeProcess(pi.hProcess, ref exitCode);
            CloseHandle(pi.hProcess);
            CloseHandle(pi.hThread);
            return;
        }
    }
}
