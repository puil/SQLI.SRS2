using System;
using System.ComponentModel;
using System.Resources;

namespace SQLI.SRS2.Business.Core
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly ResourceManager resourceManager;
        private readonly string resourceKey;

        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            this.resourceManager = new ResourceManager(resourceType);
            this.resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                var description = resourceManager.GetString(resourceKey);
                return string.IsNullOrWhiteSpace(description) ? $"[[{resourceKey}]]" : description;
            }
        }
    }
}
