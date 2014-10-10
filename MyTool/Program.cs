using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using MyTool.ReportSync;
namespace MyTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ChargeSync mChargeSync = new ChargeSync();
                //mChargeSync.Run();

                //EmailReport mEmailReport = new EmailReport();
                //mEmailReport.Run();

                //ReportSync.SubSync mSync_Sub = new ReportSync.SubSync();
                //mSync_Sub.Run();

                ReportSync.SyncSub_Partner mSyncSub_Partner = new ReportSync.SyncSub_Partner();
                mSyncSub_Partner.Run();

                //ReportSync.MOSync mSync_MO = new ReportSync.MOSync();
                //mSync_MO.Run();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }
    }
}
