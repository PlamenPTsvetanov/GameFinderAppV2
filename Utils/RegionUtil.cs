using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameFinderAppV2.Utils
{
    public class RegionUtil
    {
        private static DatabaseModel _db = new DatabaseModel();
        private RegionModel region;
        public string name { get { return region.name; } }
        public long population { get { return region.population; } }
        public string timezone { get { return region.timezone; } }

        public RegionUtil(RegionModel r) { region = r; }

        public static List<RegionUtil> filter(ref List<TextBox> generatedTextBoxes)
        {
            List<RegionModel> regionModel = _db.Regions.ToList();
            List<RegionModel> filtered = FilterUtil.filter(ref generatedTextBoxes, ref regionModel);

            List<RegionUtil> ret = new List<RegionUtil>();
            foreach (RegionModel region in filtered)
            {
                ret.Add(new RegionUtil(region));
            }

            return ret;
        }
    }
}
