using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZH.DIContract.Security
{
	// Token: 0x02000015 RID: 21
	public class AESEncryptor
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00007A6E File Offset: 0x00005C6E
		public AESEncryptor(string password, AESBits encryptionBits)
		{
			this.fPassword = password;
			this.fEncryptionBits = encryptionBits;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007A9E File Offset: 0x00005C9E
		public AESEncryptor(string password, AESBits encryptionBits, byte[] salt)
		{
			this.fPassword = password;
			this.fEncryptionBits = encryptionBits;
			this.fSalt = salt;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007AD8 File Offset: 0x00005CD8
		private byte[] iEncrypt(byte[] data, byte[] key, byte[] iV)
		{
			MemoryStream memoryStream = new MemoryStream();
			Rijndael rijndael = Rijndael.Create();
			rijndael.Key = key;
			rijndael.IV = iV;
			CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
			rijndael.Dispose();
			cryptoStream.Write(data, 0, data.Length);
			cryptoStream.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007B38 File Offset: 0x00005D38
		public string Encrypt(string data)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(data);
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(this.fPassword, this.fSalt);
			string result;
			switch (this.fEncryptionBits)
			{
			case AESBits.BITS128:
				result = Convert.ToBase64String(this.iEncrypt(bytes, passwordDeriveBytes.GetBytes(16), passwordDeriveBytes.GetBytes(16)));
				break;
			case AESBits.BITS192:
				result = Convert.ToBase64String(this.iEncrypt(bytes, passwordDeriveBytes.GetBytes(24), passwordDeriveBytes.GetBytes(16)));
				break;
			case AESBits.BITS256:
				result = Convert.ToBase64String(this.iEncrypt(bytes, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)));
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public byte[] Encrypt(byte[] data)
		{
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(this.fPassword, this.fSalt);
			byte[] result;
			switch (this.fEncryptionBits)
			{
			case AESBits.BITS128:
				result = this.iEncrypt(data, passwordDeriveBytes.GetBytes(16), passwordDeriveBytes.GetBytes(16));
				break;
			case AESBits.BITS192:
				result = this.iEncrypt(data, passwordDeriveBytes.GetBytes(24), passwordDeriveBytes.GetBytes(16));
				break;
			case AESBits.BITS256:
				result = this.iEncrypt(data, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007C74 File Offset: 0x00005E74
		private byte[] iDecrypt(byte[] data, byte[] key, byte[] iv)
		{
			MemoryStream memoryStream = new MemoryStream();
			Rijndael rijndael = Rijndael.Create();
			rijndael.Key = key;
			rijndael.IV = iv;
			CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(data, 0, data.Length);
			cryptoStream.Close();
			rijndael.Dispose();
			return memoryStream.ToArray();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public string Decrypt(string data)
		{
			byte[] data2 = Convert.FromBase64String(data);
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(this.fPassword, this.fSalt);
			string result;
			switch (this.fEncryptionBits)
			{
			case AESBits.BITS128:
				result = Encoding.Unicode.GetString(this.iDecrypt(data2, passwordDeriveBytes.GetBytes(16), passwordDeriveBytes.GetBytes(16)));
				break;
			case AESBits.BITS192:
				result = Encoding.Unicode.GetString(this.iDecrypt(data2, passwordDeriveBytes.GetBytes(24), passwordDeriveBytes.GetBytes(16)));
				break;
			case AESBits.BITS256:
				result = Encoding.Unicode.GetString(this.iDecrypt(data2, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)));
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007D88 File Offset: 0x00005F88
		public byte[] Decrypt(byte[] data)
		{
			byte[] result;
			using (PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(this.fPassword, this.fSalt))
			{
				switch (this.fEncryptionBits)
				{
				case AESBits.BITS128:
					result = this.iDecrypt(data, passwordDeriveBytes.GetBytes(16), passwordDeriveBytes.GetBytes(16));
					break;
				case AESBits.BITS192:
					result = this.iDecrypt(data, passwordDeriveBytes.GetBytes(24), passwordDeriveBytes.GetBytes(16));
					break;
				case AESBits.BITS256:
					result = this.iDecrypt(data, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
					break;
				default:
					result = null;
					break;
				}
			}
			return result;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00007E34 File Offset: 0x00006034
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00007E4C File Offset: 0x0000604C
		public string Password
		{
			get
			{
				return this.fPassword;
			}
			set
			{
				this.fPassword = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00007E58 File Offset: 0x00006058
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00007E70 File Offset: 0x00006070
		public AESBits EncryptionBits
		{
			get
			{
				return this.fEncryptionBits;
			}
			set
			{
				this.fEncryptionBits = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00007E7C File Offset: 0x0000607C
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00007E94 File Offset: 0x00006094
		public byte[] Salt
		{
			get
			{
				return this.fSalt;
			}
			set
			{
				this.fSalt = value;
			}
		}

		// Token: 0x04000028 RID: 40
		private string fPassword;

		// Token: 0x04000029 RID: 41
		private AESBits fEncryptionBits;

		// Token: 0x0400002A RID: 42
		private byte[] fSalt = new byte[]
		{
			0,
			1,
			2,
			28,
			29,
			30,
			3,
			4,
			5,
			15,
			32,
			33,
			173,
			175,
			164
		};
	}
}
