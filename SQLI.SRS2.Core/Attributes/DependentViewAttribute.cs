using System;

namespace SQLI.SRS2.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DependentViewAttribute : Attribute
    {
        public string Region { get; internal set; }
        public Type Type { get; internal set; }

        public DependentViewAttribute(string region, Type type)
        {
            this.Region = region;
            this.Type = type;
        }
    }
}
