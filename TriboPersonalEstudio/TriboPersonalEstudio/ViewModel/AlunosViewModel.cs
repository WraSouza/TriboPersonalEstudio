using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class AlunosViewModel : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; private set; } = new ObservableCollection<Usuario>();
        public ObservableCollection<Usuario> UsuariosInativos { get; private set; } = new ObservableCollection<Usuario>();
        public Command AtualizaTela { get; }
        public Command IrParaAlunoDetailView { get; set; }
        public Command IrParaCadastroExerciciosAlunoView { get; set; }
        public Command AbrirCadastroAlunoView { get; set; }

        readonly UserServices usuarios = new UserServices();

        public AlunosViewModel()
        {
            BuscaAlunos();
            BuscaAlunosInativos();
            AtualizaTela = new Command(AtualizarTela);
            AbrirCadastroAlunoView = new Command(async () => await AbrirCadastroAluno());
            IrParaAlunoDetailView = new Command<Usuario>((model) => AbrirAlunoDetailView(model));
            IrParaCadastroExerciciosAlunoView = new Command<Usuario>((model) => AbrirCadastroExercicioAlunoView(model));
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private async void AbrirCadastroExercicioAlunoView(Usuario model)
        {
            if (model is null)
            {
                return;
            }

            Preferences.Set("NomeUsuario", model.NomeAluno);
            var route = $"{nameof(View.CadastroExercicioAlunoView)}";
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

        async void AtualizarTela()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                Usuarios.Clear();
                UsuariosInativos.Clear();
                BuscaAlunos();
                BuscaAlunosInativos();


                IsRefreshing = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");

                IsRefreshing = false;
            }

        }

        async void BuscaAlunos()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {

                var usuariosCadastrados = await usuarios.RetornaAlunosAtivos();

                foreach (var alunos in usuariosCadastrados)
                {
                    Usuarios.Add(alunos);
                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");
            }
        }

        async void BuscaAlunosInativos()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {

                var usuariosCadastrados = await usuarios.RetornaAlunosInativos();

                foreach (var alunos in usuariosCadastrados)
                {
                    UsuariosInativos.Add(alunos);
                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");
            }
        }


        async Task AbrirCadastroAluno()
        {
            var route = $"{nameof(View.CadastrarAlunoView)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
