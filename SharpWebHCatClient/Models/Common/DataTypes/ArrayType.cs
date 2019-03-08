using System;
namespace SharpHive.Models.Common.DataTypes
{
    public static class ArrayType
    {
        public static string ArrayDataType(string itemsDataType)
        {
            return string.Format("ARRAY <{0}>", itemsDataType);
        }
    }
}
