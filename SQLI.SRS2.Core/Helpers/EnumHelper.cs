using System;
using System.Reflection;

namespace SQLI.SRS2.Core.Helpers
{
    public static class EnumHelper
    {
        public static T GetAttribute<T>(Enum value)
            where T : Attribute
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            return (T)fieldInfo.GetCustomAttribute(typeof(T), false) ?? throw new Exception($"EnumHelper/GetAttribute - Can't find attribute of type '{typeof(T).Name}' in value '{value}' from Enum '{value.GetType().Name}'");
        }
    }
}
