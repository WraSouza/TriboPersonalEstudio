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
    internal class MensalidadeMesViewModel : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; private set; } = new ObservableCollection<Usuario>();

        public Command AtualizaTela { get; }

        public Command AlteraStatusMensalidadePago { get; set; }

        readonly UserServices usuarios = new UserServices();

        MensalidadeServices mensalidadeServices = new MensalidadeServices();

        public MensalidadeMesViewModel()
        {
            BuscaAlunos();

            AtualizaTela = new Command(AtualizarTela);

            AlteraStatusMensalidadePago = new Command<Usuario>(async (model) => await AtualizaStatusPagoView(model));

        }

        private async Task AtualizaStatusPagoView(Usuario model)
        {
            var novaMensalidadePaga = new Mensalidade()
            {
                NomeAluno = model.NomeAluno,
                CaminhoImagem = model.CaminhoImagem,
                IsPaid = true,
                Mes = DateTime.Today.Month.ToString(),
                ValorMensalidade = model.ValorMensalidade
            };


            bool confirmaConectividade = Conectividade.VerificaConectividade();

            if (confirmaConectividade)
            {
                bool confirmaAtualizacaoMensalidade = await mensalidadeServices.CadastraMensalidadePaga(novaMensalidadePaga);

                bool confirmaAtualizacaoMensalidadeUsuario = await usuarios.AtualizaStatusAlunoMensalidade(model.NomeAluno);
                if (confirmaAtualizacaoMensalidade && confirmaAtualizacaoMensalidadeUsuario)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Status Atualizado Com Sucesso", "OK");
                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }

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

        private async void AbrirAlunoDetailView(Usuario model)
        {
            if (model is null)
            {
                return;
            }

            Preferences.Set("NomeUsuario", model.NomeAluno);
            var route = $"{nameof(View.AlunoDetailView)}";
            await Shell.Current.GoToAsync(route);
        }

        async void AtualizarTela()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                Usuarios.Clear();

                BuscaAlunos();

                IsRefreshing = false;
            }
            else
            {
                Mensagem.MensagemErroConexao();

                IsRefreshing = false;
            }

        }

        async void BuscaAlunos()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {

                var usuariosCadastrados = await usuarios.RetornaAlunosMensalidadeNaoPaga();

                foreach (var alunos in usuariosCadastrados)
                {
                    Usuarios.Add(alunos);
                }

            }
            else
            {
                Mensagem.MensagemErroConexao();
            }
        }
    }
}
