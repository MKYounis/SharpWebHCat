using System.Text;
using System.Collections.Generic;

namespace SharpHive.Models.Common.DataTypes
{
    public class UnionType
    {
        public static string StructDataType(List<string> dataType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UNIONTYPE < ");
            for (int i = 0; i < dataType.Count; i++)
            {
                stringBuilder.Append(dataType[i]);
                if (i < dataType.Count - 1)
                    stringBuilder.Append(", ");
            }
            stringBuilder.Append(">");
            return stringBuilder.ToString();
        }
    }
}
