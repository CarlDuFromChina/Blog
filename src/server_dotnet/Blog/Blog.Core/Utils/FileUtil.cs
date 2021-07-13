using Blog.Core.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Core.Utils
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileUtil
    {
        #region CRUD
        /// <summary>
        /// 获取文件列表路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<string> GetFileList(string name, FolderType type = FolderType.Bin, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var path = type.GetPath();
            if (!Directory.Exists(path))
            {
                return new List<string>();
            }
            return Directory.GetFiles(path, name, searchOption);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="image"></param>
        /// <param name="filePath"></param>
        public static void SaveFile(Stream stream, string filePath)
        {
            // 文件已存在
            if (File.Exists(filePath))
            {
                return;
            }

            var fs = new FileStream(filePath, FileMode.Create);
            try
            {
                var bytes = stream.ToByteArray();
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
            finally
            {
                fs.Close();
            }

        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 删除文件夹下所有文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFolder(string filePath, List<string> ignoreList)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(filePath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        if (!ignoreList.Contains(subdir.Name))
                        {
                            subdir.Delete(true);
                        }
                    }
                    else
                    {
                        if (!ignoreList.Contains(i.Name))
                        {
                            DeleteFile(i.FullName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteFolder(string filePath)
        {
            DeleteFolder(filePath, new List<string>());
        }
        #endregion

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Stream GetFileStream(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                var stream = fileInfo.OpenRead();
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            return null;
        }

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<MemoryStream> GetFileStreamAsync(string filePath)
        {
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="files">文件列表</param>
        /// <param name="targetFolder">目标文件夹</param>
        public static void MoveFiles(List<string> files, string targetFolder)
        {
            files.ForEach(item =>
            {
                FileInfo fileInfo = new FileInfo(item);
                fileInfo.MoveTo(Path.Combine(targetFolder, fileInfo.Name));
            });
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        public static void CopyFile(string sourcePath, string destPath)
        {
            var fileInfo = new FileInfo(sourcePath);
            AssertUtil.CheckBoolean<SpException>(fileInfo.Exists == false, $"未找到{fileInfo.Name}配置文件，请检查", "BAE7CF07-8F31-4122-9A94-3E5E247191A0");
            if (File.Exists(destPath))
            {
                File.Delete(destPath);
            }
            fileInfo.CopyTo(destPath);
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="folderName">The folder to add</param>
        /// <param name="compressedFileName">The package to create</param>
        /// <param name="overrideExisting">Override exsisitng files</param>
        /// <returns></returns>
        public static bool PackageFolder(string folderName, string compressedFileName, bool overrideExisting)
        {
            if (folderName.EndsWith(@"\"))
                folderName = folderName.Remove(folderName.Length - 1);
            bool result = false;
            if (!Directory.Exists(folderName))
            {
                return result;
            }

            if (!overrideExisting && File.Exists(compressedFileName))
            {
                return result;
            }
            try
            {
                using (Package package = Package.Open(compressedFileName, FileMode.Create))
                {
                    var fileList = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
                    foreach (string fileName in fileList)
                    {

                        //The path in the package is all of the subfolders after folderName
                        string pathInPackage;
                        pathInPackage = Path.GetDirectoryName(fileName).Replace(folderName, string.Empty) + "/" + Path.GetFileName(fileName);

                        Uri partUriDocument = PackUriHelper.CreatePartUri(new Uri(pathInPackage, UriKind.Relative));
                        PackagePart packagePartDocument = package.CreatePart(partUriDocument, "", CompressionOption.Maximum);
                        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            fileStream.CopyTo(packagePartDocument.GetStream());
                        }
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error zipping folder " + folderName, e);
            }

            return result;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileName">The file to compress</param>
        /// <param name="compressedFileName">The archive file</param>
        /// <param name="overrideExisting">override existing file</param>
        /// <returns></returns>
        public static bool PackageFile(string fileName, string compressedFileName, bool overrideExisting)
        {
            bool result = false;

            if (!File.Exists(fileName))
            {
                return result;
            }

            if (!overrideExisting && File.Exists(compressedFileName))
            {
                return result;
            }

            try
            {
                Uri partUriDocument = PackUriHelper.CreatePartUri(new Uri(Path.GetFileName(fileName), UriKind.Relative));

                using (Package package = Package.Open(compressedFileName, FileMode.OpenOrCreate))
                {
                    if (package.PartExists(partUriDocument))
                    {
                        package.DeletePart(partUriDocument);
                    }

                    PackagePart packagePartDocument = package.CreatePart(partUriDocument, "", CompressionOption.Maximum);
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.CopyTo(packagePartDocument.GetStream());
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error zipping file " + fileName, e);
            }

            return result;
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="folderName">The folder to extract the package to</param>
        /// <param name="compressedFileName">The package file</param>
        /// <param name="overrideExisting">override existing files</param>
        /// <returns></returns>
        public static bool UncompressFile(string folderName, string compressedFileName, bool overrideExisting)
        {
            bool result = false;
            try
            {
                if (!File.Exists(compressedFileName))
                {
                    return result;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                using (Package package = Package.Open(compressedFileName, FileMode.Open, FileAccess.Read))
                {
                    foreach (PackagePart packagePart in package.GetParts())
                    {
                        ExtractPart(packagePart, folderName, overrideExisting);
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error unzipping file " + compressedFileName, e);
            }

            return result;
        }

        static void ExtractPart(PackagePart packagePart, string targetDirectory, bool overrideExisting)
        {
            string stringPart = targetDirectory + HttpUtility.UrlDecode(packagePart.Uri.ToString()).Replace('\\', '/');

            if (!Directory.Exists(Path.GetDirectoryName(stringPart)))
                Directory.CreateDirectory(Path.GetDirectoryName(stringPart));

            if (!overrideExisting && File.Exists(stringPart))
                return;
            using (FileStream fileStream = new FileStream(stringPart, FileMode.Create))
            {
                packagePart.GetStream().CopyTo(fileStream);
            }
        }
    }
}
