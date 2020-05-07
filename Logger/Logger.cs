using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logger
{
    class SimpleLogger
    {
        String logPath = @"Temp\log.txt";
        StreamWriter logFile = new StreamWriter(logPath, true);

        public void WriteLog(IntPtr componentEnvironment,
                                     String instanceName,
                                     fmi2Status status,
                                     fmi2String category,
                                     fmi2String message,
                                     params String[] values)

    }
}
