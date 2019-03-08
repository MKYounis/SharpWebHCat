using System.Text;
using System.Collections.Generic;

namespace SharpHive.Models.Common.DataTypes
{
    public class StructType
    {
        public static string StructDataType(List<KeyValuePair<string, string>> columnName_dataType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("STRUCT < ");
            for (int i = 0; i < columnName_dataType.Count; i++)
            {
                stringBuilder.AppendFormat("{0} : {1}", columnName_dataType[i].Key, columnName_dataType[i].Value);
                if (i < columnName_dataType.Count - 1)
                    stringBuilder.Append(", ");
            }
            stringBuilder.Append(">");
            return stringBuilder.ToString();
        }
    }
}
