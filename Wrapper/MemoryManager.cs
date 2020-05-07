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

    namespace MemoryManager
    {
        class SimpleManager
        {
            public static IntPtr AllocateMemory(size_t nobj, size_t size)
            {
                return Marshal.AllocHGlobal((int)nobj * (int)size);
            }
            
            public static void FreeMemory(IntPtr obj)
            {
                Marshal.FreeHGlobal(obj);
            }
        }
    }
}
