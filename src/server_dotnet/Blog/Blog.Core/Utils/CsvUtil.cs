using CsvHelper;
using CsvHelper.Configuration;
using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Blog.Core.Utils
{
    public static class CsvUtil
    {
        static CsvUtil()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static CsvConfiguration GetCsvDefaultConfiguration()
        {
            return new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Encoding = Encoding.UTF8,
            };
        }

        /// <summary>
        /// 读取CSV文件数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IEnumerable<T> Read<T>(string fileName, CsvConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = GetCsvDefaultConfiguration();

            if (!File.Exists(fileName))
            {
                return null;
            }

            using (var streamReader = new StreamReader(fileName))
            {
                using (var csvReader = new CsvReader(streamReader, configuration))
                {
                    var list = csvReader.GetRecords<T>();
                    return list;
                }
            }
        }

        /// <summary>
        /// 数据写入CSV文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="fileName"></param>
        public static void Write<T>(IEnumerable<T> dataList, string fileName, CsvConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = GetCsvDefaultConfiguration();

            if (string.IsNullOrEmpty(fileName))
                return;

            using (var fs = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                using (var csvWriter = new CsvWriter(fs, configuration))
                {
                    csvWriter.WriteHeader<T>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(dataList);
                }
            }
        }
    }
}
