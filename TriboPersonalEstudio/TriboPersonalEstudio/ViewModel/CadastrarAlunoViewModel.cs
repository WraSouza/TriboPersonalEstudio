using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class CadastrarAlunoViewModel : BaseViewModel
    {
        private string _nomeAluno;
        private string _nomeUsuario;
        const string senhaPadrao = "1234";
        private object _rdButtonVezesPorSemana;
        const bool isProfessor = false;
        const string isAtivo = "Ativo";
        private object _rdButtonPlano;
        private object _rdButtonPrazo;
        private DateTime VencimentoEm;
        private DateTime _createdAt;
        private string _valorMensalidade;
        private readonly string Mensal = "Mensal";
        private readonly string Trimestral = "Trimestral";
        private ImageSource _caminhoImagem;
        Stream caminhoPhoto;
        private bool _Result;
        private bool _IsBusy;
        private bool confirmaCadastro;
        private string imagemURL;
        private string referenciaImagem;
        MensalidadeServices mensalidade = new MensalidadeServices();

        public Command CadastrarAlunoCommand { get; set; }

        public CadastrarAlunoViewModel()
        {
            CadastrarAlunoCommand = new Command(async () => await CadastrarAluno());
        }


        public bool Result
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _Result;
            set
            {
                _Result = value;
                OnPropertyChanged();
            }
        }

        public ImageSource CaminhoImagem
        {
            get
            {
                return _caminhoImagem;
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

        public Command BaterFotoAsync
        {
            get
            {
                return new Command(async (e) =>
                {
                    var photo = await MediaPicker.CapturePhotoAsync();
                    var stream = await LoadPhotoAsync(photo);
                });
            }
        }

        public Command SelecionarImagem
        {
            get
            {
                return new Command(async (e) =>
                {
                    var photo = await MediaPicker.PickPhotoAsync();                    
                    var stream = await LoadPhotoAsync(photo);
                });
            }
        }


        async Task<Stream> LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {               
                return null;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            var stream = await photo.OpenReadAsync();
            caminhoPhoto = stream;
            CaminhoImagem = photo.FullPath;

            return stream;
        }

        private async Task<string> StoreImages(Stream imageStream)
        {
            string nomeAluno = NomeAluno;
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

        async Task CadastrarAluno()
        {

            if (IsBusy)
                return;

            bool verificaConexao = Conectividade.VerificaConectividade();

            if (CreatedAt > DateTime.Today)
            {
                await Application.Current.MainPage.DisplayAlert("Ops..", "A Data Não Pode Ser Maior do que Hoje", "OK");
                return;
            }

            try
            {
                if (verificaConexao)
                {
                    IsBusy = true;
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
                            VencimentoEm = CreatedAt.AddDays(60);
                        }
                        else
                        {
                            VencimentoEm = CreatedAt.AddDays(150);

                        }                        
                        
                        imagemURL = await StoreImages(caminhoPhoto);                        

                        referenciaImagem = imagemURL.ToString();                        

                        var qtdeAlunos = userService.RetornaAlunos();

                        var ispaid = await Application.Current.MainPage.DisplayAlert("", "O Mês Atual Está Pago?", "Sim", "Não");

                        if (ispaid)
                        {
                            var novoUsuario = new Usuario()
                            {
                                NomeAluno = NomeAluno,
                                NomeUsuario = NomeUsuario,
                                StatusAluno = isAtivo,
                                CreatedAt = CreatedAt.Date.ToShortDateString(),
                                IsProfessor = isProfessor,
                                QtdeVezesSemana = QtdeVezesPorSemana,
                                PeriodoContrato = PrazoButton,
                                SenhaAluno = senhaPadrao,
                                TipoPlano = PlanoButton,
                                VencimentoEm = VencimentoEm.Date.ToShortDateString(),
                                ValorMensalidade = ValorMensalidade,
                                CaminhoImagem = referenciaImagem,
                                IsPaymentUpdated = true,
                                LastPaidMonth = DateTime.Today.Month.ToString()

                            };

                            confirmaCadastro = await userService.CadastraAluno(novoUsuario);
                        }
                        else
                        {
                            int lastMonth = (DateTime.Today.Month);

                            var novoUsuario = new Usuario()
                            {
                                NomeAluno = NomeAluno,
                                NomeUsuario = NomeUsuario,
                                StatusAluno = isAtivo,
                                CreatedAt = CreatedAt.Date.ToShortDateString(),
                                IsProfessor = isProfessor,
                                QtdeVezesSemana = QtdeVezesPorSemana,
                                PeriodoContrato = PrazoButton,
                                SenhaAluno = senhaPadrao,
                                TipoPlano = PlanoButton,
                                VencimentoEm = VencimentoEm.Date.ToShortDateString(),
                                ValorMensalidade = ValorMensalidade,
                                CaminhoImagem = referenciaImagem,
                                IsPaymentUpdated = false,
                                LastPaidMonth = lastMonth.ToString()

                            };

                            confirmaCadastro = await userService.CadastraAluno(novoUsuario);
                        }


                        int mesAtual = DateTime.Now.Month;

                        if (CreatedAt.Month == mesAtual)
                        {
                            if (ispaid)
                            {
                                var novoCadastroMensalidade = new Mensalidade()
                                {
                                    NomeAluno = NomeAluno,
                                    CaminhoImagem = referenciaImagem,
                                    Mes = mesAtual.ToString(),
                                    IsPaid = true,
                                    ValorMensalidade = ValorMensalidade.ToString()

                                };

                                bool confirmaMensalidade = await mensalidade.CadastraAlunoMensalidade(novoCadastroMensalidade);
                            }

                        }
                        else
                        {
                            if (ispaid)
                            {
                                //TO Do, colocar <= depois.
                                for (int i = CreatedAt.Month; i <= mesAtual; i++)
                                {
                                    //Já cadastro o aluno na classe Mensalidade
                                    var novoCadastroMensalidade = new Mensalidade()
                                    {
                                        NomeAluno = NomeAluno,
                                        CaminhoImagem = referenciaImagem,
                                        Mes = i.ToString(),
                                        IsPaid = true,
                                        ValorMensalidade = ValorMensalidade.ToString()
                                    };

                                    bool confirmaMensalidade = await mensalidade.CadastraAlunoMensalidade(novoCadastroMensalidade);
                                }
                            }
                            else
                            {
                                //TO Do, colocar <= depois.
                                for (int i = CreatedAt.Month; i < mesAtual; i++)
                                {
                                    //Já cadastro o aluno na classe Mensalidade
                                    var novoCadastroMensalidade = new Mensalidade()
                                    {
                                        NomeAluno = NomeAluno,
                                        CaminhoImagem = referenciaImagem,
                                        Mes = i.ToString(),
                                        IsPaid = true,
                                        ValorMensalidade = ValorMensalidade.ToString()
                                    };

                                    bool confirmaMensalidade = await mensalidade.CadastraAlunoMensalidade(novoCadastroMensalidade);
                                }
                            }


                        }


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
            finally
            {
                IsBusy = false;
            }
        }
    }
}
