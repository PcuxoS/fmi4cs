using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Wrapper
{

    using DLLAPI = WinApi.SimpleWinApi;
    /* Type definitions of variables passed as arguments*/
    //Original typedef:
    //typedef void* fmi2Component;               /* Pointer to FMU instance       */
    //typedef void* fmi2ComponentEnvironment;    /* Pointer to FMU environment    */
    //typedef void* fmi2FMUstate;                /* Pointer to internal FMU state */
    //typedef unsigned int fmi2ValueReference;
    //typedef double fmi2Real;
    //typedef int fmi2Integer;
    //typedef int fmi2Boolean;
    //typedef char fmi2Char;
    //typedef const fmi2Char* fmi2String;
    //typedef char fmi2Byte;

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

    namespace Delegates
    {
        /***************************************************
        Types for Common Functions
        ****************************************************/

        /* Inquire version numbers of header files and setting logging status */
        delegate IntPtr _fmi2GetTypesPlatformTYPE(); /*Original: typedef const char* fmi2GetTypesPlatformTYPE(void)*/
        delegate IntPtr _fmi2GetVersionTYPE(); /*Original: typedef const char* fmi2GetVersionTYPE(void)*/

        delegate fmi2Status fmi2SetDebugLoggingTYPE(fmi2Component c,
                                                    fmi2Boolean loggingOn,
                                                    size_t nCategories,
                                                    fmi2String[] categories);

        /* Creation and destruction of FMU instances and setting debug status */
        delegate fmi2Component fmi2InstantiateTYPE(fmi2String instanceName,
                                                  fmi2Type fmuType,
                                                  fmi2String fmuGUID,
                                                  fmi2String fmuResourceLocation,
                                                  ref fmi2CallbackFunctions functions,
                                                  fmi2Boolean visible,
                                                  fmi2Boolean loggingOn);

        delegate void fmi2FreeInstanceTYPE(fmi2Component c);
    }

    /***************************************************
    Realizations
    ****************************************************/

    class SimpleWrapper : IWrapper
    {
        private string dllPath;
        private IntPtr dllHandle;

        private Delegates._fmi2GetTypesPlatformTYPE _fmi2GetTypesPlatform;
        private Delegates._fmi2GetVersionTYPE _fmi2GetVersion;

        public SimpleWrapper(String path)
        {
            Console.WriteLine("File exist = " + System.IO.File.Exists(path));
            dllPath = path;
            dllHandle = WinApi.SimpleWinApi.LoadLibrary(path);
            Console.WriteLine("Lib loaded = {0}", dllHandle != IntPtr.Zero);

            _fmi2GetTypesPlatform = (Delegates._fmi2GetTypesPlatformTYPE)DLLAPI
                .LoadFunction<Delegates._fmi2GetTypesPlatformTYPE>(dllHandle, "fmi2GetTypesPlatform");
            _fmi2GetVersion = (Delegates._fmi2GetVersionTYPE)DLLAPI
                .LoadFunction<Delegates._fmi2GetVersionTYPE>(dllHandle, "fmi2GetVersion");
        }




        /*********************************************
         * Interface
         ********************************************/

        public void fmi2FreeInstance(fmi2ComponentEnvironment c)
        {
            throw new NotImplementedException();
        }

        public string fmi2GetTypesPlatform()
        {
            return Marshal.PtrToStringAnsi(_fmi2GetTypesPlatform());
        }

        public string fmi2GetVersion()
        {
            return Marshal.PtrToStringAnsi(_fmi2GetVersion());
        }

        public fmi2ComponentEnvironment fmi2Instantiate(string instanceName, fmi2Type fmuType, string fmuGUID, string fmuResourceLocation, ref fmi2CallbackFunctions functions, int visible, int loggingOn)
        {
            throw new NotImplementedException();
        }

        public fmi2Status fmi2SetDebugLogging(fmi2ComponentEnvironment c, int loggingOn, size_t nCategories, string[] categories)
        {
            throw new NotImplementedException();
        }
    }
}
