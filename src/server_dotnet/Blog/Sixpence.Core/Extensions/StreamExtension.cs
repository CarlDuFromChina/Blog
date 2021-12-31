using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common
{
    /// <summary>
    /// 流扩展
    /// </summary>
    public static class StreamExtension
    {
        /// <summary>
        /// 加密流
        /// </summary>
        /// <typeparam name="Algorithm"></typeparam>
        /// <param name="stream"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Stream Encrypt<Algorithm>(Stream stream, byte[] key)
            where Algorithm : SymmetricAlgorithm
        {
            var alg = Activator.CreateInstance<Algorithm>();
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, null);
            alg.Key = pdb.GetBytes(alg.KeySize / 8);
            alg.GenerateIV();
            alg.IV = pdb.GetBytes(alg.IV.Length);

            ICryptoTransform encryptor = alg.CreateEncryptor();
            return new CryptoStream(stream, encryptor, CryptoStreamMode.Read);
        }


        /// <summary>
        /// 解密流
        /// </summary>
        /// <typeparam name="Algorithm"></typeparam>
        /// <param name="stream"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Stream Decrypt<Algorithm>(Stream stream, byte[] key)
            where Algorithm : SymmetricAlgorithm
        {
            var alg = Activator.CreateInstance<Algorithm>();
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, null);
            alg.Key = pdb.GetBytes(alg.KeySize / 8);
            alg.GenerateIV();
            alg.IV = pdb.GetBytes(alg.IV.Length);

            ICryptoTransform encryptor = alg.CreateDecryptor();
            return new CryptoStream(stream, encryptor, CryptoStreamMode.Read);
        }

        /// <summary>
        /// 文件流转Byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
