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

        private ObservableCollection<DisclosureMaterial> Materials
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

        public IEnumerable<DisclosureMaterial> GetDisclosureMaterials() => Materials.ToList();

        public DisclosureMaterial GetDisclosureMaterial(int id) => Materials.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Loads dummy materials data
        /// </summary>
        private void LoadMaterials()
        {
            materials = new ObservableCollection<DisclosureMaterial>();

            for (int i = 1; i <= 200; i++)
            {
                var disclosureMaterial = new DisclosureMaterial
                {
                    Id = i,
                    Name = $"{{ARGC-{i}}}",
                    HistoryStatus = i % 5 == 0 ? HistoryStatusEnum.Current : HistoryStatusEnum.Archived,
                    SupplierShortName = i % 3 == 0 ? "Fuji" : "N/A",
                    ReceivedOn = DateTime.Today.AddDays(i * -1),
                    ValidFrom = DateTime.Today.AddDays(i * -1),
                    ValidTo = null,
                    Incomplete = i % 7 == 0,
                    Tpd2Relevant = i % 3 == 0,
                    Evaluated = i % 10 == 0,
                    Country = i % 3 == 0 ? "Spain" : null
                };

                for (int j = 1; j <= 30; j++)
                {
                    disclosureMaterial.FlatViewItems.Add(new DisclosureFlatViewItem
                    {
                        InternalId = j + 1000,
                        Name = j % 3 == 0 ? $"Material name {j}" : $"Short {j}",
                        Percentage = j % 4 == 0 ? j * 1.7 : j * 1.3,
                        Functions = "Flavor",
                        Cas = $"{i}-{j}-{i % 5}",
                        Fema = j % 8 == 0 ? $"({2980 + j})" : $"{3047 + j}",
                        Coe = j % 4 == 0 ? null : (j % 7 == 0 ? $"({400 + j}n)" : $"{120 + j}"),
                        Enbr = j % 3 == 0 ? null : (j % 5 == 0 ? $"(E{1500 + j})" : $"{1500 + j}"),
                    });
                }

                materials.Add(disclosureMaterial);
            }
        }
    }
}
