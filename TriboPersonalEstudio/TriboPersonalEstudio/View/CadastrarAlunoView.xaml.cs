using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriboPersonalEstudio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarAlunoView : ContentPage
    {
        public CadastrarAlunoView()
        {
            InitializeComponent();

            datePicker.Date = DateTime.Now;
        }
    }
}