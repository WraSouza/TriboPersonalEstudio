using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public Command OpenLogin { get; set; }

        public MainViewModel()
        {
            OpenLogin = new Command( () => OpenLoginView());
        }

        private void OpenLoginView()
        {
            Application.Current.MainPage = new View.LoginView();
        }
    }
}
