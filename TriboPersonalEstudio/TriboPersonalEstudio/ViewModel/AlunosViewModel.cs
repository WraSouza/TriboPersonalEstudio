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
        public Command RenovarPlanoAlunoView { get; set; }
        private readonly string Mensal = "Mensal";
        private readonly string Trimestral = "Trimestral";
        private DateTime VencimentoEm;

        readonly UserServices usuarios = new UserServices();
        readonly MensalidadeServices mensalidade = new MensalidadeServices();

        public AlunosViewModel()
        {
            BuscaAlunos();
            BuscaAlunosInativos();
            AtualizaTela = new Command(AtualizarTela);
            AbrirCadastroAlunoView = new Command(async () => await AbrirCadastroAluno());
            IrParaAlunoDetailView = new Command<Usuario>((model) => AbrirAlunoDetailView(model));
            IrParaCadastroExerciciosAlunoView = new Command<Usuario>((model) => AbrirCadastroExercicioAlunoView(model));

            RenovarPlanoAlunoView = new Command<Usuario>((model) => RenovarPlano(model));
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

        private async void RenovarPlano(Usuario model)
        {
            if (model is null)
            {
                return;
            }

            DateTime vencimento = Convert.ToDateTime(model.VencimentoEm);

            if (model.PeriodoContrato.ToString() == Mensal)
            {
                VencimentoEm = vencimento.AddDays(30);

            }
            else if (model.PeriodoContrato.ToString() == Trimestral)
            {
                VencimentoEm = vencimento.AddDays(90);
            }
            else
            {
                VencimentoEm = vencimento.AddDays(180);

            }

            var renovaPlano = await Application.Current.MainPage.DisplayAlert("", "Renovar Plano?", "Sim", "Não");

            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {                

                if (renovaPlano)
                {
                    bool ispaid = await Mensagem.MensagemMesAtualPago();

                    if (ispaid)
                    { 
                        
                        if((vencimento.Month < DateTime.Today.Month) && (vencimento.Year == DateTime.Today.Year))
                        {                            
                            var novoUsuario = new Usuario()
                            {
                                NomeAluno = model.NomeAluno,
                                DataInicioPlano = DateTime.Today.ToShortDateString(),
                                StatusAluno = "Ativo",
                                CurrentMonth = DateTime.Today.Month.ToString(),
                                IsPaymentUpdated = true,
                                VencimentoEm = VencimentoEm.ToShortDateString()
                            };

                            bool confirmaRenovacaoPlano = await usuarios.RenovarPlano(novoUsuario);

                            if (confirmaRenovacaoPlano)
                            {
                                Mensagem.MensagemRenovacaoPlano();
                            }

                            var novoCadastroMensalidade = new Mensalidade()
                            {
                                NomeAluno = model.NomeAluno,
                                CaminhoImagem = model.CaminhoImagem,
                                Mes = DateTime.Today.Month.ToString(),
                                IsPaid = true,
                                ValorMensalidade = model.ValorMensalidade.ToString()

                            };

                            bool confirmaMensalidade = await mensalidade.CadastraAlunoMensalidade(novoCadastroMensalidade);
                        }
                        else
                        {
                            var usuario = new Usuario()
                            {
                                NomeAluno = model.NomeAluno,
                                DataInicioPlano = model.VencimentoEm,
                                StatusAluno = "Ativo",
                                CurrentMonth = vencimento.Month.ToString(),
                                IsPaymentUpdated = true,
                                VencimentoEm = VencimentoEm.ToShortDateString()
                            };

                            bool confirmaRenovacao = await usuarios.RenovarPlano(usuario);

                            if (confirmaRenovacao)
                            {
                                Mensagem.MensagemRenovacaoPlano();
                            }
                        }                        

                        
                    }
                    else
                    {
                        int currentMonth = (DateTime.Today.Month);

                        var usuario = new Usuario()
                        {
                            NomeAluno = model.NomeAluno,                           
                            DataInicioPlano = model.VencimentoEm,
                            StatusAluno = "Ativo",
                            CurrentMonth = currentMonth.ToString(),
                            IsPaymentUpdated = false,
                            VencimentoEm = VencimentoEm.ToShortDateString()
                        };

                        bool confirmaRenovacao = await usuarios.RenovarPlano(usuario);

                        if (confirmaRenovacao)
                        {
                            Mensagem.MensagemRenovacaoPlano();
                        }

                    }

                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
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
                Mensagem.MensagemErroConexao();                
            }
        }


        async Task AbrirCadastroAluno()
        {
            var route = $"{nameof(View.CadastrarAlunoView)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
