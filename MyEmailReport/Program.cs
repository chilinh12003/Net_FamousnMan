using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using MyEmailReport.Report;

namespace MyEmailReport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EmailReport mEmailReport = new EmailReport();
                mEmailReport.Run();

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }
    }
}
