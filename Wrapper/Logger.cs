using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Wrapper
{
    /* Type definitions of variables passed as arguments*/
    using fmi2Component = IntPtr;               /* Pointer to FMU instance       */
    using fmi2ComponentEnvironment = IntPtr;    /* Pointer to FMU environment    */
    using fmi2FMUstate = IntPtr;                /* Pointer to internal FMU state */
    using fmi2ValueReference = UInt32;
    using fmi2Real = Double;
    using fmi2Integer = Int32;
    using fmi2Boolean = Int32;
    using fmi2Char = Char;
    using fmi2String = String;
    using fmi2Byte = Char;
    using size_t = UIntPtr;

    namespace Logger
    {
        partial class SimpleLogger
        {
            [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public unsafe static extern int vsprintf(
                IntPtr buffer,
                String format,
                IntPtr* args);

            [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
            public unsafe  static extern int _vscprintf(
                String format,
                IntPtr* args);

            String logPath;
            StreamWriter logFile;

            public SimpleLogger(String logPath = @"Temp\log.txt")
            {
                this.logPath = logPath;
                logFile = new StreamWriter(this.logPath, true);
                logFile.AutoFlush = true;
            }


            public unsafe void WriteLog(IntPtr componentEnvironment,
                                 String instanceName,
                                 fmi2Status status,
                                 fmi2String category,
                                 fmi2String message,
                                 IntPtr args0 = default(IntPtr), IntPtr args1 = default(IntPtr), IntPtr args2 = default(IntPtr), IntPtr args3 = default(IntPtr),
                                 IntPtr args4 = default(IntPtr), IntPtr args5 = default(IntPtr), IntPtr args6 = default(IntPtr), IntPtr args7 = default(IntPtr))
            {
                IntPtr* ptr = &args0;
                var byteLength = _vscprintf(message, ptr) +1;
                var buffer = Marshal.AllocHGlobal(byteLength);
                vsprintf(buffer, message, ptr);

                String finalMessage = "";
                finalMessage = Marshal.PtrToStringAnsi(buffer);

                String logMessage = System.DateTime.Now.ToString()
                    + "::"
                    +instanceName
                    + "::"
                    + status.ToString()
                    + "::"
                    + category
                    + "::"
                    + finalMessage;
                Console.WriteLine(logMessage);
                logFile.WriteLine(logMessage);
            }
            public void WriteLogTest(IntPtr componentEnvironment,
                                 IntPtr instanceName,
                                 fmi2Status status,
                                 fmi2String category,
                                 fmi2String message,
                                 params String[] values)
            {
                var instName = Marshal.PtrToStringBSTR(instanceName);
                String logMessage = System.DateTime.Now.ToString()
                    + "::"
                    + instName
                    + "::"
                    + status.ToString()
                    + "::"
                    + category
                    + "::"
                    + message;
                Console.WriteLine(logMessage);
                logFile.WriteLine(logMessage);
            }

        }
                
    }
}
