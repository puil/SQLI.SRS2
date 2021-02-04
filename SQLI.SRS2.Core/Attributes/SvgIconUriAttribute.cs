using System;

namespace SQLI.SRS2.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class SvgIconUriAttribute : Attribute
    {
        public string SourceUri { get; set; }

        public SvgIconUriAttribute(string sourceUri)
        {
            this.SourceUri = sourceUri;
        }
    }
}
