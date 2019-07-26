using System;
using System.Collections.Generic;
using System.Text;

namespace WMSImportData.Model
{
    public class w_temp_Project
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
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// PrjName
        /// </summary>		
        private string _prjname;
        public string PrjName
        {
            get { return _prjname; }
            set { _prjname = value; }
        }
        /// <summary>
        /// DeptCode
        /// </summary>		
        private string _deptCode;
        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value; }
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
