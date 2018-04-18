using EVEMarket.Model;

namespace EVEMarket.WPF.ViewModel
{
    public class ConstellationViewModel
    {
        private Constellation _model;

        public string Name => _model.Name;

        public ConstellationViewModel(Constellation model)
        {
            this._model = model;
        }
    }
}