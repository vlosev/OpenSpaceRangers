using System;

namespace Common.Serialization
{
    public static class SerializationUtils
    {
        public static string[] ConvertGuidToStringArray(Guid[] guids)
        {
            var ret = new string[guids.Length];
            for (var i = 0; i < guids.Length; ++i)
            {
                ret[i] = guids[i].ToString();
            }
            
            return ret;
        }
        
        public static Guid[] ConvertStringToGuidArray(string[] strings)
        {
            var ret = new Guid[strings.Length];
            for (var i = 0; i < strings.Length; ++i)
            {
                if (Guid.TryParse(strings[i], out var guid))
                    ret[i] = guid;
            }
            
            return ret;
        }
    }
}