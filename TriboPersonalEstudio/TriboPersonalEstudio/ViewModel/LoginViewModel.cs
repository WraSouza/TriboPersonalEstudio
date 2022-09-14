using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class LoginViewModel : BaseViewModel
    {
        private string _nomeUsuario;
        private string _senhaUsuario;
        private bool _Result;
        private bool _IsBusy;
        private const string _statusUsuario = "Ativo";
        UserServices userServices = new UserServices();
        public Command LoginCommand { get; set; }
        public Command AbrirSobreView { get; set; }

        public string Nome
        {
            get => _nomeUsuario;
            set
            {
                _nomeUsuario = value.ToLower();
                OnPropertyChanged();
            }
        }

        public string Senha
        {
            get => _senhaUsuario;
            set
            {
                _senhaUsuario = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login foi realizado com sucesso
        public bool Result
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login está sendo realizado para evitar concorrência
        public bool IsBusy
        {
            get => _Result;
            set
            {
                _Result = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            VerificaAlunosVencidos();
            LoginCommand = new Command(async () => await LoginCommandAsync());
            AbrirSobreView = new Command(() => AbrirPaginaSobreCommandAsync());
        }

        private void AbrirPaginaSobreCommandAsync()
        {
            Application.Current.MainPage = new View.SobreAcademiaView();
        }

        private async Task VerificaAlunosVencidos()
        {
            await userServices.DesativarUsuarioPlanoVencido();
        }

        //Login
        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {
                bool verificaConexao = Conectividade.VerificaConectividade();

                if (verificaConexao)
                {
                    IsBusy = true;
                    var userService = new UserServices();
                    Result = await userService.LoginUser(Nome, Senha);

                    await userService.AlterarStatusAlunoMensalidadeNaoPaga();

                    if (Result)
                    {
                        Preferences.Set("Nome", Nome.ToUpper());

                        string status = await userService.GetUserStatus(Nome);

                        if (status != _statusUsuario)
                        {
                            await Application.Current.MainPage.DisplayAlert("Info", "Usuário Sem Autorização de Acesso", "OK");
                        }
                        else
                        {

                            string senhaNoBanco = await userService.GetUserSenha(Nome);

                            if (senhaNoBanco == "1234")
                            {
                                Application.Current.MainPage = new View.AlunoAppShell();
                                //Application.Current.MainPage = new View.TrocarSenhaView();
                            }
                            else
                            {
                                bool verificaSeProfessor = await userService.GetUserProfile(Nome);
                                if (verificaSeProfessor)
                                {
                                    Application.Current.MainPage = new View.ProfessorAppShell();
                                }
                                else
                                {
                                    Application.Current.MainPage = new View.AlunoAppShell();
                                }

                            }

                        }

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Usuário/Senha Inválidos", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não Foi Possível Verificar Credenciais. Verifique Sua Conexão de Internet.", "OK");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
