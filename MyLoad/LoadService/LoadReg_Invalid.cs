using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;

namespace MyLoad.LoadService
{
    public class LoadReg_Invalid : MyLoadBase
    {
        public LoadReg_Invalid()
        {
            mTemplatePath = "~/Templates/Service/Reg_Invalid.htm";
            Init();
        }

        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
                string[] Arr = new string[] { };
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, Arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
