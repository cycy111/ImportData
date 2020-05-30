using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCenter.Api.Infrastructure.WmsEncryption
{
    public class WmsEncryptionHelper
    {
        public static string Encrypt(string UserCode, string Password)
        {
            string enctyPwd = Password.Trim();
            ZH.DIContract.Security.AESEncryptor aes = new ZH.DIContract.Security.AESEncryptor(UserCode.ToUpper() + ZH.DIContract.Security.SecurityUserKey.PwdKey, ZH.DIContract.Security.AESBits.BITS256);

            string inputPwd = string.Empty;
            if (!string.IsNullOrEmpty(enctyPwd))
            {
                inputPwd = aes.Encrypt(enctyPwd);
            }
            return inputPwd;

        }
    }
}
