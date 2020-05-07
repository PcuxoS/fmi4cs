using System;
using System.Collections.Generic;
using System.Text;

namespace Wrapper
{
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

    public interface IWrapper
    {
        /***************************************************
        Types for Common Functions
        ****************************************************/

        /* Inquire version numbers of header files and setting logging status */
        String fmi2GetTypesPlatform(); 
        String fmi2GetVersion(); 

        fmi2Status fmi2SetDebugLogging(fmi2Component c,
                                           fmi2Boolean loggingOn,
                                           size_t nCategories,
                                           fmi2String[] categories);

        /* Creation and destruction of FMU instances and setting debug status */
        fmi2Component fmi2Instantiate(fmi2String instanceName,
                                          fmi2Type fmuType,
                                          fmi2String fmuGUID,
                                          fmi2String fmuResourceLocation,
                                          ref fmi2CallbackFunctions functions,
                                          fmi2Boolean visible,
                                          fmi2Boolean loggingOn);

        void fmi2FreeInstance(fmi2Component c);
    }
}
