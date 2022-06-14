using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class AlunosViewModel : BaseViewModel
    {
        UserServices userServices = new UserServices();

        private string _textoBotao;       

        public Command IrParaAlunoDetailView { get; set; }
       

        public string TextoBotao
        {
            get => _textoBotao;
            set
            {
                if(_textoBotao==null)
                {
                    _textoBotao = "Desativar";
                }
                _textoBotao = value;
                OnPropertyChanged();
            }
        }       

        public AlunosViewModel()
        {            
            IrParaAlunoDetailView = new Command<Usuario>((model) => AbrirAlunoDetailView(model)); 

        }    
        
        private async void AbrirAlunoDetailView()
        {
           
            var route = $"{nameof(View.AlunoDetailView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void AbrirAlunoDetailView(Usuario model)
        {

            if (model is null)
            {
                return;
            }

            Preferences.Set("NomeAluno", model.NomeAluno);
            var route = $"{nameof(View.AlunoDetailView)}";
            await Shell.Current.GoToAsync(route);
            
        }
    }
}
