using System;

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

            switch (type.Name.ToLower())
            {
                case "int":
                case "int32":
                case "int64":
                    long.TryParse(s.IfStringVoidForNumeric(), out long numValue);
                    return numValue.ToString();

                case "bool":
                case "boolean":
                    bool.TryParse(s.IfStringVoidForNumeric(), out bool boolValue);
                    return boolValue.ToString().ToLower();

                case "decimal":
                    decimal.TryParse(s.IfStringVoidForNumeric(), out decimal decValue);
                    return decValue.ToString().Replace(",", ".");

                case "string":
                    return $"\"{s.Replace("\"", "\"\"")}\"";

                case "datetime":
                    DateTime.TryParse(s, out DateTime dateTimeStr);
                    return $"new DateTime({dateTimeStr.Year}, {dateTimeStr.Month}, {dateTimeStr.Day}, {dateTimeStr.Hour}, {dateTimeStr.Minute}, {dateTimeStr.Second})";

                // TO DO: manage other types if needed

                default:
                    return s;
            }
        }
    }
}
