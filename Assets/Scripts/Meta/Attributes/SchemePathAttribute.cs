using System;

namespace Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SchemePathAttribute : Attribute
    {
        public readonly string Path;

        public SchemePathAttribute(string path)
        {
            Path = path;
        }
    }
}