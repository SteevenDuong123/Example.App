using Prism.Mvvm;

namespace Example.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Example Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
