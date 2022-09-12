using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class AlunoDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; private set; } = new ObservableCollection<Usuario>();
        UserServices userServices = new UserServices();
        public Command AbrirCadastroExercicio { get; }

        public AlunoDetailViewModel()
        {
            BuscaAluno();
            AbrirCadastroExercicio = new Command<Usuario>(async (model) => await CadastroExercicioView(model));
        }

        private async Task CadastroExercicioView(Usuario model)
        {
            Preferences.Set("NomeAluno", model.NomeAluno);
            Preferences.Set("ImagemAluno", model.CaminhoImagem);
            var route = $"{nameof(View.CadastroExercicioAlunoView)}";
            await Shell.Current.GoToAsync(route);
        }

        async void BuscaAluno()
        {
            string nomeAluno = Preferences.Get("NomeAluno", "default_value");

            var dadosAluno = await userServices.RetornaAlunoEspecifico(nomeAluno);

            foreach(var informacoes in dadosAluno)
            {
                Usuarios.Add(informacoes);
            }
        }
    }
}
