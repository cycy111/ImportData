using System;

namespace ZH.DIContract.Security
{
	// Token: 0x02000017 RID: 23
	public class SecurityUserKey
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00007FF8 File Offset: 0x000061F8
		public static string EncryptionPwdKey(string userCode, string pwd)
		{
			string result;
			try
			{
				bool flag = string.IsNullOrEmpty(userCode) || string.IsNullOrEmpty(pwd);
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					string data = MD5Encryptor.Md5Encryption(pwd);
					AESEncryptor aesencryptor = new AESEncryptor(userCode + "adlfu;om;=.?,rdt,./@3$^%*()", AESBits.BITS256);
					result = aesencryptor.Encrypt(data);
				}
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0400002B RID: 43
		public const string PwdKey = "adlfu;om;=.?,rdt,./@3$^%*()";
	}
}
