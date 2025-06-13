using System;

namespace SAE2._01_Loxam.Utils
{
    public static class SafeConvert
    {
        public static DateTime? SafeParseDateTime(object value)
        {
            if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return DateTime.Parse(value.ToString());
        }
    }
}
