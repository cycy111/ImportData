using System;
using System.Collections.Generic;
using System.Text;

namespace WMSImportData.Model
{
    public class w_temp_ProductType
    {

        /// <summary>
        /// TypeID
        /// </summary>		
        private string _typeid;
        public string TypeID
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        /// <summary>
        /// AccountID
        /// </summary>		
        private string _accountid;
        public string AccountID
        {
            get { return _accountid; }
            set { _accountid = value; }
        }
        /// <summary>
        /// Name
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// SortFlag
        /// </summary>		
        private int _sortflag;
        public int SortFlag
        {
            get { return _sortflag; }
            set { _sortflag = value; }
        }
        /// <summary>
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// IsEnable
        /// </summary>		
        private string _isenable;
        public string IsEnable
        {
            get { return _isenable; }
            set { _isenable = value; }
        }

    }
}
