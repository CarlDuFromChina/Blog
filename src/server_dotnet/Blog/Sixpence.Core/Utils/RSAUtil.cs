using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common.Utils
{
    /// <summary>
    /// RSA帮助类（非对称加密）
    /// </summary>
    public static class RSAUtil
    {
        private static Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        /// <summary>
        /// 获取加密所使用的key，RSA算法是一种非对称密码算法，所谓非对称，就是指该算法需要一对密钥，使用其中一个加密，则需要用另一个才能解密。
        /// </summary>
        public static string GetKey()
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            var PrivateKey = rSACryptoServiceProvider.ToXmlString(true); // 获取公匙和私匙，用于解密
            var PublicKey = ExportPublicKeyToPEMFormat(rSACryptoServiceProvider);
            if (!keyValuePairs.ContainsKey(PublicKey))
            {
                keyValuePairs.Add(PublicKey, PrivateKey);
            }
            return PublicKey;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static byte[] Encryption(string str, string publicKey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(publicKey);
            byte[] buffer = Encoding.UTF8.GetBytes(str); // 将明文转换为byte[]

            // 加密后的数据就是一个byte[] 数组,可以以 文件的形式保存 或 别的形式(网上很多教程,使用Base64进行编码化保存)
            byte[] EncryptBuffer = rSACryptoServiceProvider.Encrypt(buffer, false); // 进行加密
            return EncryptBuffer;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string Encryption2(string str, string publickKey)
        {
            var EncryptBase64 = Convert.ToBase64String(Encryption(str, publickKey));
            return EncryptBase64;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">加密后内容</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string Decrypt(string content, string publicKey)
        {
            try
            {
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                rSACryptoServiceProvider.FromXmlString(keyValuePairs[publicKey]);

                var inputBytes = Convert.FromBase64String(content);
                int bufferSize = rSACryptoServiceProvider.KeySize / 8;
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes), outputStream = new MemoryStream())
                {
                    while (true)
                    {
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var rawBytes = rSACryptoServiceProvider.Decrypt(temp, false);
                        outputStream.Write(rawBytes, 0, rawBytes.Length);
                    }
                    return Encoding.UTF8.GetString(outputStream.ToArray());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // 移除失效RSA密钥对
                keyValuePairs.Remove(publicKey);
            }
        }

        /// <summary>
        /// 导出公钥PEM格式
        /// </summary>
        /// <param name="csp"></param>
        /// <returns></returns>
        private static string ExportPublicKeyToPEMFormat(RSACryptoServiceProvider csp)
        {
            TextWriter outputStream = new StringWriter();

            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);



                    //All Parameter Must Have Value so Set Other Parameter Value Whit Invalid Data  (for keeping Key Structure  use "parameters.Exponent" value for invalid data)

                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.D
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.P
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.Q
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DP
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DQ
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.InverseQ

                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();

                // outputStream.WriteLine("-----BEGIN PUBLIC KEY-----");

                // Output as Base64 with lines chopped at 64 characters

                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                }

                // outputStream.WriteLine("-----END PUBLIC KEY-----");
                return outputStream.ToString();
            }

        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;

            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }

            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }

                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }

            }

        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }

                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }

            }

        }

    }
}
