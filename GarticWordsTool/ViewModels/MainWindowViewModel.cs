using Prism.Mvvm;

namespace GarticWordsTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _titie;

        public string Titie
        {
            get
            {
                return _titie;
            }
            set
            {
                _titie = value;
                SetProperty(ref _titie, value);
            }
        }

        public MainWindowViewModel()
        {
            Titie = "Gartic词库编辑工具";
        }
    }
}
