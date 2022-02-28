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
        private string _tipoPlano;
        const bool isProfessor = false;
        const bool isAtivo = true;

        public Command CadastraAluno { get; set; }

        public string TipoPlano
        {
            get => _tipoPlano;
            set
            {
                _tipoPlano = value.ToLower();
                OnPropertyChanged();
            }
        }

        public string NomeAluno
        {
            get => _nomeAluno;
            set
            {
                _nomeAluno = value.ToLower();
                OnPropertyChanged();
            }
        }

        public string NomeUsuario
        {
            get => _nomeUsuario;
            set
            {
                _nomeUsuario = value;
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
                        var userService = new UserServices();
                        var novoUsuario = new Usuario() {
                            NomeAluno = NomeAluno,
                            IsAtivo = isAtivo,
                            CreatedAt = DateTime.Now,
                            
                        };
                        

                        //bool verificaLogin = userService.LoginUser(N)
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
