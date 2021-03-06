﻿using System;
using System.Data;
using System.Text;

namespace db2ent.Misc
{
    /// <summary>
    /// Put extension methods here
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// For empty strings, associated to numerical values, returns zero. Otherwise, returns the given string
        /// </summary>
        /// <param name="s">The string to check for numeric compliance</param>
        /// <returns></returns>
        private static string IfStringVoidForNumeric(this string s)
        {
            return string.IsNullOrEmpty(s) ? "0" : s;
        }

        /// <summary>
        /// Given a DataRow ItemArray specific value and its column type, format the output string
        /// </summary>
        /// <param name="o">The object value read from DataRowItemArray</param>
        /// <param name="type">The type of the column the value was read from</param>
        /// <returns></returns>
        public static string ToTypedString(this object o, Type type)
        {
            var s = o.ToString();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int32:
                case TypeCode.Int64:
                    long.TryParse(s.IfStringVoidForNumeric(), out long numValue);
                    return numValue.ToString();

                case TypeCode.Boolean:
                    bool.TryParse(s.IfStringVoidForNumeric(), out bool boolValue);
                    return boolValue.ToString().ToLower();

                case TypeCode.Decimal:
                    decimal.TryParse(s.IfStringVoidForNumeric(), out decimal decValue);
                    return decValue.ToString().Replace(",", ".");

                case TypeCode.String:
                    return $"\"{s.Replace("\"", "\"\"")}\"";

                case TypeCode.DateTime:
                    DateTime.TryParse(s, out DateTime dateTimeStr);
                    return $"new DateTime({dateTimeStr.Year}, {dateTimeStr.Month}, {dateTimeStr.Day}, {dateTimeStr.Hour}, {dateTimeStr.Minute}, {dateTimeStr.Second})";

                // TO DO: manage other types if needed

                default:
                    return s;
            }
        }

        /// <summary>
        /// Executes POCO representation of given table as plain text
        /// </summary>
        /// <param name="dt">The DataTable to process</param>
        /// <returns></returns>
        public static string DataTableToString(this DataTable dt)
        {
            var sb = new StringBuilder();

            if (dt.Rows.Count == 0) sb.AppendLine("\t// No records where found");

            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                sb.AppendLine($"\tnew {dt.TableName}()");
                sb.AppendLine("\t{");

                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    sb.Append("\t\t")
                      .Append(dt.Columns[colIndex].ColumnName)
                      .Append(" = ")
                      .Append(dt.Rows[rowIndex].ItemArray[colIndex].ToTypedString(dt.Columns[colIndex].DataType))
                      .AppendLine(",");
                }

                sb.AppendLine("\t},");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates a class representation from the given DataTable schema
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SchemaToClass(this DataTable dt)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"public partial class {dt.TableName}")
              .AppendLine("{");

            foreach(DataColumn column in dt.Columns)
            {
                sb.AppendLine($"\tpublic {column.DataType.Name} {column.ColumnName} {{ get; set; }}");
            }

            sb.AppendLine("};");

            return sb.ToString();
        }
    }
}
