using System;
using System.Runtime.InteropServices;

namespace WinApi
{
    public class SimpleWinApi
    {
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long x);

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long x);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        public static extern IntPtr FreeLibrary(IntPtr module);


        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr module, string proc);

        public static Delegate LoadFunction<T>(IntPtr hModule, string functionName)
        {
            var functionAddress = GetProcAddress(hModule, functionName);
            return Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(T));
        }
    }
}
