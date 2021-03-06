using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriboPersonalEstudio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlunosView : ContentPage
    {
        public AlunosView()
        {
            InitializeComponent();
            BindingContext = new AlunosViewModel();
        }

        protected async override void OnAppearing()
        {
            UserServices usuarios = new UserServices();

            collectionView.ItemsSource = await usuarios.RetornaAlunos();
        }

    }
}