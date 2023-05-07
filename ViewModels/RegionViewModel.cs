using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameFinderAppV2.ViewModels
{
    public class RegionViewModel
    {
        private static DatabaseModel _db = new DatabaseModel();
        private RegionModel region;
        public string name { get { return region.name; } }
        public long population { get { return region.population; } }
        public string timezone { get { return region.timezone; } }

        public RegionViewModel(RegionModel r) { region = r; }

        public static List<RegionViewModel> filter(ref List<TextBox> generatedTextBoxes)
        {
            List<RegionModel> regionModel = _db.Regions.ToList();
            List<RegionModel> filtered = FilterViewModel.filter(ref generatedTextBoxes, ref regionModel);

            List<RegionViewModel> ret = new List<RegionViewModel>();
            foreach (RegionModel region in filtered)
            {
                ret.Add(new RegionViewModel(region));
            }

            return ret;
        }
    }
}
