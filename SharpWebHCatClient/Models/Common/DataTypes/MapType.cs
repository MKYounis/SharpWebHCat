using System;
namespace SharpHive.Models.Common.DataTypes
{
    public class MapType
    {
        public static string MapDataType(string primitiveType, string dataType)
        {
            return string.Format("MAP <{0}, {1}>", primitiveType, dataType);
        }
    }
}
