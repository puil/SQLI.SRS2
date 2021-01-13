using SQLI.SRS2.Business.Disclosure;
using System.Collections.Generic;

namespace SQLI.SRS2.Services.Interfaces
{
    public interface IDisclosureService
    {
        IEnumerable<DisclosureMaterialHeader> GetDisclosureMaterialsHeaders();
        IEnumerable<DisclosureMaterial> GetDisclosureMaterials();
        DisclosureMaterial GetDisclosureMaterial(int id);
    }
}
