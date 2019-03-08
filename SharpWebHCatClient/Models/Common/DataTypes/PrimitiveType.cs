using System;
namespace SharpHive.Models.Common
{
    public static class PrimitiveType
    {
        public static string TinyIntDataType()
        {
            return "TINYINT";
        }

        public static string SmallIntDataType()
        {
                return "SMALLINT";
        }

        public static string IntDataType()
        {
                return "INT";
        }

        public static string BigIntDataType()
        {
                return "BIGINT";
        }

        public static string BooleanDataType()
        {
                return "BOOLEAN";
        }

        public static string FloatDataType()
        {
            return "FLOAT";
        }

        public static string DoubleDataType()
        {
                return "DOUBLE";
        }

        /// <summary>
        /// (Note: Available in Hive 2.2.0 and later)
        /// </summary>
        /// <returns>The precision data type.</returns>
        public static string DoublePrecisionDataType()
        {
                return "DOUBLE PRECISION";
        }

        public static string StringDataType()
        {
                return "STRING";
        }

        /// <summary>
        /// (Note: Available in Hive 0.8.0 and later)
        /// </summary>
        /// <returns>The data type.</returns>
        public static string BinaryDataType()
        {
                return "BINARY";
        }

        /// <summary>
        /// (Note: Available in Hive 0.8.0 and later)
        /// </summary>
        /// <returns>The stamp data type.</returns>
        public static string TimeStampDataType()
        {
                return "TIMESTAMP";
        }

        /// <summary>
        /// (Note: Available in Hive 0.11.0 and later)
        /// </summary>
        /// <returns>The data type.</returns>
        public static string DecimalDataType()
        {
                return "DECIMAL";
        }
        /// <summary>
        /// (Note: Available in Hive 0.13.0 and later).
        /// </summary>
        /// <returns>The decimal.</returns>
        /// <param name="precision">Precision.</param>
        /// <param name="scale">Scale.</param>
        public static string GetDecimalDataType(int precision, int scale)
        {
            return string.Format("DECIMAL({0}, {1})", precision, scale);
        }

        /// <summary>
        /// (Note: Available in Hive 0.12.0 and later)
        /// </summary>
        /// <returns>The date data type.</returns>
        public static string DateDataType()
        {
                return "DATE";
        }

        /// <summary>
        /// (Note: Available in Hive 0.12.0 and later)
        /// </summary>
        /// <returns>The data type.</returns>
        public static string VarcharDataType()
        {
                return "VARCHAR";
        }

        public static string CharDataType()
        {
            return "CHAR";
        }
    }
}
