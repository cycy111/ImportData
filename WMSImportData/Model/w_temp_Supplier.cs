using System;
using System.Collections.Generic;
using System.Text;

namespace WMSImportData.Model
{
    public class w_temp_Supplier
    {

        /// <summary>
        /// DocEntry
        /// </summary>		
        private string _docentry;
        public string DocEntry
        {
            get { return _docentry; }
            set { _docentry = value; }
        }
        /// <summary>
        /// DeptCode
        /// </summary>		
        private string _deptcode;
        public string DeptCode
        {
            get { return _deptcode; }
            set { _deptcode = value; }
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
        /// SupName
        /// </summary>		
        private string _supname;
        public string SupName
        {
            get { return _supname; }
            set { _supname = value; }
        }
        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
