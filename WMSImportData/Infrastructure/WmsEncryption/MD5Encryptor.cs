using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZH.DIContract.Security
{
	// Token: 0x02000016 RID: 22
	public class MD5Encryptor
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00007EA0 File Offset: 0x000060A0
		public static string Md5Encryption(string encryStr)
		{
			bool flag = string.IsNullOrEmpty(encryStr);
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				using (MD5 md = MD5.Create())
				{
					byte[] array = md.ComputeHash(Encoding.UTF8.GetBytes(encryStr));
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i].ToString("x");
						bool flag2 = text.Length == 1;
						if (flag2)
						{
							stringBuilder.Append("0");
						}
						stringBuilder.Append(text);
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007F60 File Offset: 0x00006160
		public static string GetMD5HashFromFile(string fileName)
		{
			string result;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open);
				MD5 md = new MD5CryptoServiceProvider();
				byte[] array = md.ComputeHash(fileStream);
				fileStream.Close();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
