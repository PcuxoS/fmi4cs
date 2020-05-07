using System;
using System.Collections.Generic;
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

    /*Function types*/
    /* Type definitions */
    public enum fmi2Status
    {
        fmi2OK,
        fmi2Warning,
        fmi2Discard,
        fmi2Error,
        fmi2Fatal,
        fmi2Pending
    };

    public enum fmi2Type
    {
        fmi2ModelExchange,
        fmi2CoSimulation
    };

    public enum fmi2StatusKind
    {
        fmi2DoStepStatus,
        fmi2PendingStatus,
        fmi2LastSuccessfulTime,
        fmi2Terminated
    };

    
    public delegate void fmi2CallbackLogger(fmi2ComponentEnvironment componentEnvironment,
                                     fmi2String instanceName,
                                     fmi2Status status,
                                     fmi2String category,
                                     fmi2String message,
                                     IntPtr args0 , IntPtr args1, IntPtr args2, IntPtr args3,
                                     IntPtr args4, IntPtr args5, IntPtr args6, IntPtr args7);

    public delegate IntPtr fmi2CallbackAllocateMemory(size_t nobj, size_t size);
    public delegate void fmi2CallbackFreeMemory(IntPtr obj);
    public delegate void fmi2StepFinished(fmi2ComponentEnvironment componentEnvironment,
                                   fmi2Status status);

    [StructLayout(LayoutKind.Sequential)]
    public struct fmi2CallbackFunctions
    {
        public fmi2CallbackLogger logger;
        public fmi2CallbackAllocateMemory allocateMemory;
        public fmi2CallbackFreeMemory freeMemory;
        public fmi2StepFinished stepFinished;
        public fmi2ComponentEnvironment componentEnvironment;
    };

    [StructLayout(LayoutKind.Sequential)]
    struct fmi2EventInfo
    {
        fmi2Boolean newDiscreteStatesNeeded;
        fmi2Boolean terminateSimulation;
        fmi2Boolean nominalsOfContinuousStatesChanged;
        fmi2Boolean valuesOfContinuousStatesChanged;
        fmi2Boolean nextEventTimeDefined;
        fmi2Real nextEventTime;
    };
}
