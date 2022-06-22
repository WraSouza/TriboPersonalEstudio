using Plugin.Media;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.Services;
using Xamarin.Forms;
using System.Diagnostics;
using Plugin.Media.Abstractions;
using Firebase.Storage;

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
        private DateTime VencimentoEm;
        private DateTime _createdAt;
        private string _valorMensalidade;
        private readonly string Mensal = "Mensal";
        private readonly string Trimestral = "Trimestral";
        private ImageSource _caminhoImagem;
        private Image image;
        MediaFile file;

        public Command CadastraAluno { get; set; }
        public Command SelecionarImagem { get; set; }

        public ImageSource CaminhoImagem
        {
            get { return _caminhoImagem;
                    }
            set { _caminhoImagem = value; OnPropertyChanged(); }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        public string ValorMensalidade
        {
            get => _valorMensalidade;
            set
            {
                _valorMensalidade = value;
                OnPropertyChanged();
            }
        }

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
            SelecionarImagem = new Command(async () => await SelecionarImagemGaleria());
        }

        private async Task SelecionarImagemGaleria()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                CaminhoImagem = ImageSource.FromStream(() => file.GetStream());
                //await StoreImages(file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<string> StoreImages(Stream imageStream)
        {
            string nomeAluno = "imagemPerfil";
            string imgurl = null;
            string storageImage = null;
            if (nomeAluno != null)
            {
                storageImage = await new FirebaseStorage("tribopersonalacademia.appspot.com")
              .Child("Fotos")
              .Child(nomeAluno + ".jpg")
              .PutAsync(imageStream);
                imgurl = storageImage;

            }
            return imgurl;
        }

        private async Task CadastrarAluno()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            try
            {
                if (verificaConexao)
                {
                    if (string.IsNullOrEmpty(NomeAluno))
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Os Campos São Obrigatórios", "OK");
                    }
                    else
                    {
                        var tipoPlano = PlanoButton;
                        var userService = new UserServices();

                        if (PrazoButton.ToString() == Mensal)
                        {
                            VencimentoEm = CreatedAt.AddDays(30);

                        }
                        else if (PrazoButton.ToString() == Trimestral)
                        {
                            VencimentoEm = CreatedAt.AddDays(90);
                        }
                        else
                        {
                            VencimentoEm = CreatedAt.AddDays(180);

                        }

                        var imagemURL = await StoreImages(file.GetStream());

                        string referenciaImagem = imagemURL.ToString();

                        var qtdeAlunos = userService.RetornaAlunos();

                        var novoUsuario = new Usuario()
                        {
                            NomeAluno = NomeAluno,
                            NomeUsuario = NomeUsuario,
                            IsAtivo = isAtivo,
                            CreatedAt = CreatedAt.Date.ToShortDateString(),
                            IsProfessor = isProfessor,
                            QtdeVezesSemana = QtdeVezesPorSemana,
                            PeriodoContrato = PrazoButton,
                            SenhaAluno = senhaPadrao,
                            TipoPlano = PlanoButton,
                            VencimentoEm = VencimentoEm.Date.ToShortDateString(),
                            ValorMensalidade = ValorMensalidade,
                            CaminhoImagem = referenciaImagem

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

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }

        }

    }
}
