using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriboPersonalEstudio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroAlunoView : ContentPage
    {
        public CadastroAlunoView()
        {
            InitializeComponent();

            datePicker.Date = DateTime.Today;

        }
        
    }
}