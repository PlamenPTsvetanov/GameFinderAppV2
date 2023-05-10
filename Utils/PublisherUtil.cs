using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameFinderAppV2.Utils
{
    public class PublisherUtil
    {
        private PublisherModel _publisher;
        private static DatabaseModel _db = new DatabaseModel();

        public PublisherUtil()
        {
            _publisher = new PublisherModel();
        }

        public PublisherUtil(PublisherModel p)
        {
            _publisher = p;
        }

        public string Name { get { return _publisher.Name; } }
        public double Rating { get { return _publisher.Rating; } }

        public static List<PublisherUtil> filter(ref List<TextBox> generatedTextBoxes)
        {
            List<PublisherModel> pubModels = _db.Publishers.ToList();
            List<PublisherModel> filtered = FilterUtil.filter(ref generatedTextBoxes, ref pubModels);

            List<PublisherUtil> ret = new List<PublisherUtil>();
            foreach (PublisherModel pub in filtered)
            {
                ret.Add(new PublisherUtil(pub));
            }

            return ret;
        }
    }
}
