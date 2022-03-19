using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.Services;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class CadastroAlunoViewModel : BaseViewModel
    {
        private string _nomeAluno;
        private string _nomeUsuario;
        const string senhaPadrao = "1234";
        private object _rdButtonVezesPorSemana;
        const bool isProfessor = false;
        const bool isAtivo = true;
        private object _rdButtonPlano;
        private object _rdButtonPrazo;

        public Command CadastraAluno { get; set; }

        public object PlanoButton
        {
            get => _rdButtonPlano;
            set
            {
                _rdButtonPlano = value;
                OnPropertyChanged();
            }
        }

        public object PrazoButton
        {
            get => _rdButtonPrazo;
            set
            {
                _rdButtonPrazo = value;
                OnPropertyChanged();
            }
        }

        public object QtdeVezesPorSemana
        {
            get => _rdButtonVezesPorSemana;
            set
            {
                _rdButtonVezesPorSemana = value;
                OnPropertyChanged();
            }
        }

        public string NomeAluno
        {
            get => _nomeAluno;
            set
            {
                _nomeAluno = value;
                OnPropertyChanged();
            }
        }

        public string NomeUsuario
        {
            get => _nomeUsuario;
            set
            {
                _nomeUsuario = value.ToLower();
                OnPropertyChanged();
            }
        }

        public CadastroAlunoViewModel()
        {
            CadastraAluno = new Command(async () => await CadastrarAluno());
        }

        private async Task CadastrarAluno()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();
            try
            {
                if (verificaConexao)
                {
                    if (string.IsNullOrEmpty(NomeAluno) || string.IsNullOrEmpty(NomeUsuario))
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Os Campos São Obrigatórios", "OK");
                    }
                    else
                    {
                        var tipoPlano = PlanoButton;
                        var userService = new UserServices();
                        var novoUsuario = new Usuario() {
                            NomeAluno = NomeAluno,
                            IsAtivo = isAtivo,
                            CreatedAt = DateTime.Now,
                            IsProfessor = isProfessor,
                            QtdeVezesSemana = QtdeVezesPorSemana,
                            PeriodoContrato = PrazoButton,
                            SenhaAluno = senhaPadrao,
                            NomeUsuario = NomeUsuario,
                            TipoPlano = PlanoButton                            
                        };


                        bool confirmaCadastro = await userService.CadastraAluno(novoUsuario);

                        if (confirmaCadastro)
                        {
                            await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário Cadastrado Com Sucesso", "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Info", "Usuário Já Cadastrado No Sistema", "OK");
                        }
                        
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não Foi Possível Verificar Credenciais. Verifique Sua Conexão de Internet.", "OK");
                }

            }catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }

        }

    }
}
