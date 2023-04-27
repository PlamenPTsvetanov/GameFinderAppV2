using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinderAppV2.ViewModels
{
    public class PublisherViewModel
    {
        private PublisherModel _publisher;

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
    }
}
