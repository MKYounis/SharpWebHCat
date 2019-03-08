using System.Text;
using System.Collections.Generic;

namespace SharpHive.Models.Common.DataTypes
{
    public class RowFormat
    {
        public static string GetDelimitedRowFormat(char? FieldsTerminatedBy, char? EscapedBy, char? CollectionItemsTerminatedBy, char? MapKeysTerminatedBy, char? LinesTerminatedBy, char? NullDefinedAs)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DELIMITED ");

            if (FieldsTerminatedBy.HasValue)
                stringBuilder.AppendFormat("FIELDS TERMINATED BY '{0}' ", FieldsTerminatedBy.Value);
            if (EscapedBy.HasValue)
                stringBuilder.AppendFormat("ESCAPED BY '{0}' ", EscapedBy.Value);
            if (CollectionItemsTerminatedBy.HasValue)
                stringBuilder.AppendFormat("COLLECTION ITEMS TERMINATED BY '{0}' ", CollectionItemsTerminatedBy.Value);
            if (MapKeysTerminatedBy.HasValue)
                stringBuilder.AppendFormat("MAP KEYS TERMINATED BY '{0}' ", MapKeysTerminatedBy.Value);
            if (LinesTerminatedBy.HasValue)
                stringBuilder.AppendFormat("LINES TERMINATED BY '{0}' ", LinesTerminatedBy.Value);
            if (NullDefinedAs.HasValue)
                stringBuilder.AppendFormat("NULL DEFINED AS '{0}'", NullDefinedAs.Value);
            
            return stringBuilder.ToString();
        }

        public static string GetSerdeRowFormat(string serdeName, List<KeyValuePair<string, string>> properties)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("SERDE {0} ", serdeName);
            if (properties.Count > 0)
            {
                stringBuilder.Append("WITH SERDEPROPERTIES (");
                for (int i = 0; i < properties.Count; i++)
                {
                    stringBuilder.AppendFormat("{0}={1}", properties[i].Key, properties[i].Value);
                    if (i < properties.Count - 1)
                        stringBuilder.Append(",");
                }
                stringBuilder.Append(")");
            }
            return stringBuilder.ToString();
        }


    }
}
