using EVEMarket.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEMarket.WPF.ViewModel
{
    public class RegionViewModel : ViewModelBase
    {
        private readonly Region _model;
        private string name;

        public RegionViewModel(Region model)
        {
            _model = model;
        }

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
    }
}
