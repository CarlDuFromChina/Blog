using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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
        /// 读取csv数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static DataTable Read(string fileName, DataColumnCollection columns, CsvConfiguration configuration = null)
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
                    var manager = EntityManagerFactory.GetManager();
                    var list = csvReader.GetRecords<dynamic>().Select(item => new RouteValueDictionary(item)).ToList();
                    var dt = new DataTable();

                    foreach (DataColumn column in columns)
                    {
                        dt.Columns.Add(new DataColumn(column.ColumnName, column.DataType));
                    }

                    list.ForEach(item =>
                    {
                        DataRow row = dt.NewRow();
                        foreach (DataColumn column in dt.Columns)
                        {
                            var value = item[column.ColumnName];
                            if (!string.IsNullOrEmpty(value?.ToString()))
                            {
                                row[column.ColumnName] = item[column.ColumnName];
                            }
                        }
                        dt.Rows.Add(row);
                    });

                    return dt;
                }
            }
        }

        /// <summary>
        /// 数据写入CSV文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="fileName"></param>
        public static void Write<T>(IEnumerable<T> dataList, string fileName, CsvConfiguration configuration = null) where T : BaseEntity
        {
            if (configuration == null)
                configuration = GetCsvDefaultConfiguration();

            if (string.IsNullOrEmpty(fileName))
                return;

            using (var fs = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                using (var csvWriter = new CsvWriter(fs, configuration))
                {
                    csvWriter.Context.RegisterClassMap(new EntityMap<T>());
                    csvWriter.WriteHeader<T>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(dataList);
                }
            }
        }
    }

    public class EntityMap<T> : ClassMap<T> where T : BaseEntity
    {
        public EntityMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.GetEntityName()).Ignore();
        }
    }
}
