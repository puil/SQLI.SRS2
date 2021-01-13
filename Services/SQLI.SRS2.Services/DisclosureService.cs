using SQLI.SRS2.Business.Disclosure;
using SQLI.SRS2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SQLI.SRS2.Services
{
    public class DisclosureService : IDisclosureService
    {
        private ObservableCollection<DisclosureMaterial> materials;

        public ObservableCollection<DisclosureMaterial> Materials
        {
            get
            {
                if (materials == null)
                    LoadMaterials();

                return materials;
            }
        }

        public IEnumerable<DisclosureMaterialHeader> GetDisclosureMaterialsHeaders()
        {
            return Materials.Select(x => new DisclosureMaterialHeader
            {
                Id = x.Id,
                Name = x.Name,
                HistoryStatus = x.HistoryStatus
            }).ToList();
        }

        public IEnumerable<DisclosureMaterial> GetDisclosureMaterials()
        {
            return Materials.ToList();
        }

        public DisclosureMaterial GetDisclosureMaterial(int id)
        {
            return Materials.FirstOrDefault(x => x.Id == id);
        }


        private void LoadMaterials()
        {
            materials = new ObservableCollection<DisclosureMaterial>();

            for (int i = 1; i <= 200; i++)
            {
                materials.Add(new DisclosureMaterial
                {
                    Id = i,
                    Name = $"{{ARGC-{i}}}",
                    HistoryStatus = i % 5 == 0 ? HistoryStatusEnum.Current : HistoryStatusEnum.Archived,
                    ReceivedOn = DateTime.Today.AddDays(i * -1),
                    ValidFrom = DateTime.Today.AddDays(i * -1),
                    ValidTo = null,
                    Incomplete = i % 7 == 0,
                    Tpd2Relevant = i % 3 == 0,
                    Evaluated = i % 10 == 0,
                    Country = i % 3 == 0 ? "Spain" : null
                });
            }

            //System.Threading.Thread.Sleep(2000);        
        }
    }
}
