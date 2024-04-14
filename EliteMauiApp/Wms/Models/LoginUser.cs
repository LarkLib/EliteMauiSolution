using Elite.LMS.Maui.ViewModels;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite.LMS.Maui.Models
{
    public sealed class LoginUser : NotificationObject
    {
        private static volatile LoginUser _instance = null;
        private static object syncRoot = new Object();
        private LoginUser() { }
        public static LoginUser Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new LoginUser();
                    }
                }
                return _instance;
            }
        }
        public Int64 UserId
        {
            get;
            set;
        }
        private string userCode;
        public string UserCode
        {
            get { return userCode; }
            set
            {
                if (userCode != value)
                {
                    userCode = value;
                    OnPropertyChanged();
                }
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LoginPwd
        {
            get;
            set;
        }
        public string IsEffective
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public Int64 RoleId
        {
            get;
            set;
        }
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set
            {
                if (roleName != value)
                {
                    roleName = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
