using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameFinderAppV2.ViewModels
{
    public class PublisherViewModel
    {
        private PublisherModel _publisher;
        private static DatabaseModel _db = new DatabaseModel();

        public PublisherViewModel()
        {
            _publisher = new PublisherModel();
        }

        public PublisherViewModel(PublisherModel p)
        {
            _publisher = p;
        }

        public string Name { get{ return _publisher.Name; } }
        public double Rating { get{ return _publisher.Rating; } }

        public static List<PublisherViewModel> filter(ref List<TextBox> generatedTextBoxes)
        {
            List<PublisherModel> pubModels = _db.Publishers.ToList();
            List<PublisherModel> filtered = FilterViewModel.filter(ref generatedTextBoxes, ref pubModels);

            List<PublisherViewModel> ret = new List<PublisherViewModel>();
            foreach (PublisherModel pub in filtered)
            {
                ret.Add(new PublisherViewModel(pub));
            }

            return ret;
        }
    }
}
