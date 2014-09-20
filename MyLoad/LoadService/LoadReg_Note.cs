using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;

namespace MyLoad.LoadService
{
    public class LoadReg_Note : MyLoadBase
    {
        public string Message = string.Empty;
        public LoadReg_Note(string Message)
        {
            this.Message = Message;
            mTemplatePath = "~/Templates/Service/Reg_Note.htm";
            Init();
        }

        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
                string[] Arr = new string[] { Message };
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, Arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
