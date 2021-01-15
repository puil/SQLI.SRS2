using SQLI.SRS2.Business.Core;
using SQLI.SRS2.Business.Resources;
using System.ComponentModel;

namespace SQLI.SRS2.Business.Disclosure
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum HistoryStatusEnum
    {
        [LocalizedDescription("enum_HistoryStatusEnum_Current", typeof(StringResources))]
        Current,
        [LocalizedDescription("enum_HistoryStatusEnum_Archived", typeof(StringResources))]
        Archived
    }
}
