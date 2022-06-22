using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriboPersonalEstudio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilAlunoView : ContentPage
    {
        public PerfilAlunoView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var nomeAluno = Preferences.Get("NomeAluno", "default_value");
            UserServices usuarios = new UserServices();
            collectionView.ItemsSource = await usuarios.RetornaAlunoEspecifico(nomeAluno);
        }
    }
}