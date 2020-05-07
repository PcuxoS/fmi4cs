using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Wrapper;
using Wrapper.Logger;
using Wrapper.Delegates;

namespace fmi4cs
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

    class Program
    {
        
        private delegate IntPtr TestDeleg();

        public static void TestScript1()
        {

            var unzipper = new Unzipper.SimpleUnzipper();
            unzipper.Delete("Temp");
            unzipper.Unzip("FmuExportCrossCompile.fmu");
            Console.WriteLine("FMU unziped");
            Console.WriteLine("Error code = {0}", Marshal.GetLastWin32Error());
            var path = @"Temp\FmuExportCrossCompile.fmu\binaries\win64\FmuExportCrossCompile.dll";
            Console.WriteLine("File exist = " + System.IO.File.Exists(path));
            var dll = WinApi.SimpleWinApi.LoadLibrary(path);
            Console.WriteLine("Lib loaded = {0}", dll != IntPtr.Zero);

            TestDeleg deleg = (TestDeleg)WinApi.SimpleWinApi.LoadFunction<TestDeleg>(dll, "fmi2GetVersion");
            Console.WriteLine("Function loaded = {0}", deleg != null);

            var charArr = deleg();
            string str = Marshal.PtrToStringAnsi(charArr);
            Console.WriteLine("String = " + str);
            Console.WriteLine("Error code = {0}", Marshal.GetLastWin32Error());

            fmi2InstantiateTYPE fmi2Instantiate = (fmi2InstantiateTYPE)WinApi
                .SimpleWinApi
                .LoadFunction<fmi2InstantiateTYPE>(dll, "fmi2Instantiate");
            Console.WriteLine("fmiInstatiate loaded = {0}", fmi2Instantiate != null);
            Console.WriteLine("Error code = {0}", Marshal.GetLastWin32Error());
            var instanceName = "TestInstance";
            var fmuType = fmi2Type.fmi2CoSimulation;
            var fmuGuid = @"{5a4fa8dc-cd97-434c-ab30-e352e643f853}";
            var fmuResourceLocation = @"file" + Path.GetFullPath(@"Temp\FmuExportCrossCompile.fmu") + @"\resources";
            Wrapper.Logger.SimpleLogger loggerTest = new Wrapper.Logger.SimpleLogger();
            Wrapper.MemoryManager.SimpleManager managerTest = new Wrapper.MemoryManager.SimpleManager();

            fmi2CallbackFunctions function = new fmi2CallbackFunctions
            {
                logger = loggerTest.WriteLog,
                allocateMemory = Wrapper.MemoryManager.SimpleManager.AllocateMemory,
                freeMemory = Wrapper.MemoryManager.SimpleManager.FreeMemory,
                componentEnvironment = IntPtr.Zero
            };
            Int32 visible = 0;
            Int32 loggingOn = 1;
            var fmuInstance = fmi2Instantiate(instanceName,
                fmuType,
                fmuGuid,
                fmuResourceLocation,
                ref function,
                visible,
                loggingOn);
            Console.WriteLine("Instance created = {0}", fmuInstance != IntPtr.Zero);
            Console.WriteLine("Error code = {0}", Marshal.GetLastWin32Error());
            Console.ReadLine();
            WinApi.SimpleWinApi.FreeLibrary(dll);
            unzipper.Delete(@"Temp\FmuExportCrossCompile.fmu");
        }


        [DllImport(@"C:\Users\Me\Documents\Project\FMUs\Modelica.StateGraph.Examples.FirstExample\binaries\win64\Modelica_StateGraph_Examples_FirstExample.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr fmi2Instantiate(fmi2String instanceName,
                                              fmi2Type fmuType,
                                              fmi2String fmuGUID,
                                              fmi2String fmuResourceLocation,
                                              ref fmi2CallbackFunctions functions,
                                              fmi2Boolean visible,
                                              fmi2Boolean loggingOn);
        
        public static void TestScript2()
        {
            var instanceName = "TestInstance";
            var fmuType = fmi2Type.fmi2CoSimulation;
            var _fmuGuid = "{2a13a763-32b1-4de6-b9e2-fb1e3d21c786}";
            var fmuResourceLocation = @"file: " + Path.GetFullPath(@"Temp") + @"\resources";
            Wrapper.Logger.SimpleLogger loggerTest = new Wrapper.Logger.SimpleLogger();

            fmi2CallbackFunctions function = new fmi2CallbackFunctions
            {
                logger = loggerTest.WriteLog,
                allocateMemory = Wrapper.MemoryManager.SimpleManager.AllocateMemory,
                freeMemory = Wrapper.MemoryManager.SimpleManager.FreeMemory,
                componentEnvironment = IntPtr.Zero
            };
            Int32 visible = 1;
            Int32 loggingOn = 1;
            var fmuInstance = fmi2Instantiate(instanceName,
                fmuType,
                _fmuGuid,
                fmuResourceLocation,
                ref function,
                visible,
                loggingOn);
        }

        [DllImport("TestLib.dll")]
        public static extern void ReadCStr(String value);

        [DllImport("TestLib.dll")]
        public static extern void Compare(String value);

        [DllImport("TestLib.dll")]
        public static extern void FromFMI(String value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = true)]
        public delegate void TestDeleg2(String str, IntPtr args0, IntPtr args1, IntPtr args2, IntPtr args3);

        public unsafe static void TestFunc(String str, IntPtr args0, IntPtr args1, IntPtr args2, IntPtr args3)
        {
            Console.WriteLine("Start TestFunc");
            Console.WriteLine(str);
            IntPtr* test = &args0;
            
            var byteLength = SimpleLogger._vscprintf(str, test) + 1;
            var buffer = Marshal.AllocHGlobal(byteLength);
            var totatlLength = SimpleLogger.vsprintf(buffer, str, test);

            var finalMessage = Marshal.PtrToStringAnsi(buffer);
            Console.WriteLine(finalMessage);
            Marshal.FreeHGlobal(buffer);
            
        }

        [DllImport("TestLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void TestLogger(String value, TestDeleg2 logger);

        

        
        public static void TestScript3()
        {
            String str = "{2a13a763-32b1-4de6-b9e2-fb1e3d21c786}";
            ReadCStr(str);
            Compare(str);
            FromFMI(str);
            var logger = new Wrapper.Logger.SimpleLogger();
            fmi2CallbackLogger writeLog = new fmi2CallbackLogger(logger.WriteLog);
            TestDeleg2 outFunc = new TestDeleg2(TestFunc);
            TestLogger(str, TestFunc);

        }
        public static void TestScript4()
        {
            Wrapper.SimpleWrapper wrapper = new Wrapper.SimpleWrapper(@"C:\Users\Me\Documents\Project\FMUs\Modelica.StateGraph.Examples.FirstExample\binaries\win64\Modelica_StateGraph_Examples_FirstExample.dll");
            var test = wrapper.fmi2GetTypesPlatform();
            var test2 = wrapper.fmi2GetVersion();
            Console.WriteLine(test + " " + test2);
        }

        static void Main(string[] args)
        {
            try
            {
                TestScript4();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Generic Exception Handler: {e}");
            }
            Console.WriteLine("End");
            Console.ReadLine();
        }

        
    }
}
