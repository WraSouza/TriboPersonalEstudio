using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace TriboPersonalEstudio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancasView : ContentPage
    {
        private float porcentagem;
        private bool verificaSeMaior;
        private bool verificaSeIgual;
        private int currentYear = DateTime.Today.Year;

        public FinancasView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            UserServices usuarios = new UserServices();
            MensalidadeServices mensalidades = new MensalidadeServices();

            UserServices userServices = new UserServices();
            float somaJaneiro = 0.00f;
            float somaFevereiro = 0.00f;
            float somaMarco = 0.00f;
            float somaAbril = 0.00f;
            float somaMaio = 0.00f;
            float somaJunho = 0.00f;
            float somaJulho = 0.00f;
            float somaAgosto = 0.00f;
            float somaSetembro = 0.00f;
            float somaOutubro = 0.00f;
            float somaNovembro = 0.00f;
            float somaDezembro = 0.00f;

            //Mensalidades
            float somaMensalidadeJaneiro = 0.00f;
            float somaMensalidadeFevereiro = 0.00f;
            float somaMensalidadeMarco = 0.00f;
            float somaMensalidadeAbril = 0.00f;
            float somaMensalidadeMaio = 0.00f;
            float somaMensalidadeJunho = 0.00f;
            float somaMensalidadeJulho = 0.00f;
            float somaMensalidadeAgosto = 0.00f;
            float somaMensalidadeSetembro = 0.00f;
            float somaMensalidadeOutubro = 0.00f;
            float somaMensalidadeNovembro = 0.00f;
            float somaMensalidadeDezembro = 0.00f;

            var lista = await userServices.RetornaAlunosAtivos();

            foreach (var valores in lista)
            {

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 1 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month == 1 ) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month == 1) && valores.StatusAluno == "Ativo")
                {
                    somaJaneiro += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 2 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 2) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 2) && valores.StatusAluno == "Ativo")
                {
                    somaFevereiro += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 3 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 3) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 3) && valores.StatusAluno == "Ativo")
                {
                    somaMarco += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 4 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 4) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 4) && valores.StatusAluno == "Ativo")
                {
                    somaAbril += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 5 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 5) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 5) && valores.StatusAluno == "Ativo")
                {
                    somaMaio += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 6 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 6) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 6) && valores.StatusAluno == "Ativo")
                {
                    somaJunho += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 7 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 7) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 7) && valores.StatusAluno == "Ativo")
                {
                    somaJulho += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 8 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 8) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 8) && valores.StatusAluno == "Ativo")
                {
                    somaAgosto += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 9 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 9) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear))  && Convert.ToDateTime(valores.CreatedAt).Month <= 9) && valores.StatusAluno == "Ativo")
                {
                    somaSetembro += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 10 && ( Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 10) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 10) && valores.StatusAluno == "Ativo")
                {
                    somaOutubro += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 11 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 11) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 11) && valores.StatusAluno == "Ativo")
                {
                    somaNovembro += float.Parse(valores.ValorMensalidade);
                }

                if ((((Convert.ToDateTime(valores.VencimentoEm).Month > 12 && (Convert.ToDateTime(valores.VencimentoEm).Year == currentYear)) || (Convert.ToDateTime(valores.VencimentoEm).Month < 12) && (Convert.ToDateTime(valores.VencimentoEm).Year > currentYear)) && Convert.ToDateTime(valores.CreatedAt).Month <= 12) && valores.StatusAluno == "Ativo")
                {
                    somaDezembro += float.Parse(valores.ValorMensalidade);
                }

            }

            List<Entry> entries = new List<Entry>
                {
                    new Entry(somaJaneiro)
                    {
                        Color = SKColor.Parse("#8B405F"),
                        Label = "Janeiro",
                        ValueLabel = somaJaneiro.ToString()
                    },
                    new Entry(somaFevereiro)
                    {
                        Color = SKColor.Parse("#7CCFED"),
                        Label = "Fevereiro",
                        ValueLabel = somaFevereiro.ToString()
                    },
                    new Entry(somaMarco)
                    {
                        Color = SKColor.Parse("#6A8A9D"),
                        Label = "Março",
                        ValueLabel = somaMarco.ToString()
                    },
                    new Entry(somaAbril)
                    {
                        Color = SKColor.Parse("#315E75"),
                        Label = "Abril",
                        ValueLabel = somaAbril.ToString()
                    },
                    new Entry(somaMaio)
                    {
                        Color = SKColor.Parse("#BD382D"),
                        Label = "Maio",
                        ValueLabel = somaMaio.ToString()
                    },
                    new Entry(somaJunho)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Junho",
                        ValueLabel = somaJunho.ToString()
                    },
                    new Entry(somaJulho)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Julho",
                        ValueLabel = somaJulho.ToString()
                    },
                    new Entry(somaAgosto)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Agosto",
                        ValueLabel = somaAgosto.ToString()
                    },
                    new Entry(somaSetembro)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Setembro",
                        ValueLabel = somaSetembro.ToString()
                    },
                    new Entry(somaOutubro)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Outubro",
                        ValueLabel = somaOutubro.ToString()
                    },
                    new Entry(somaNovembro)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Novembro",
                        ValueLabel = somaNovembro.ToString()
                    },
                    new Entry(somaDezembro)
                    {
                        Color = SKColor.Parse("#B40101"),
                        Label = "Dezembro",
                        ValueLabel = somaDezembro.ToString()
                    }
                };

            //Inserir valores somados da tabela Matrícula
            var mensalidade = await mensalidades.RetornaMensalidade();


            foreach (var itens in mensalidade)
            {
                if (itens.Mes == "1")
                {
                    somaMensalidadeJaneiro += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "2")
                {
                    somaMensalidadeFevereiro += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "3")
                {
                    somaMensalidadeMarco += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "4")
                {
                    somaMensalidadeAbril += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "5")
                {
                    somaMensalidadeMaio += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "6")
                {
                    somaMensalidadeJunho += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "7")
                {
                    somaMensalidadeJulho += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "8")
                {
                    somaMensalidadeAgosto += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "9")
                {
                    somaMensalidadeSetembro += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "10")
                {
                    somaMensalidadeOutubro += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "11")
                {
                    somaMensalidadeNovembro += float.Parse(itens.ValorMensalidade);
                }

                if (itens.Mes == "12")
                {
                    somaMensalidadeDezembro += float.Parse(itens.ValorMensalidade);
                }
            }

            lblJaneiro.Text = somaMensalidadeJaneiro.ToString();

            lblFevereiro.Text = somaMensalidadeFevereiro.ToString();

            lblMarco.Text = somaMensalidadeMarco.ToString();

            lblAbril.Text = somaMensalidadeAbril.ToString();

            lblMaio.Text = somaMensalidadeMaio.ToString();

            lblJunho.Text = somaMensalidadeJunho.ToString();

            lblJulho.Text = somaMensalidadeJulho.ToString();

            lblAgosto.Text = somaMensalidadeAgosto.ToString();

            lblSetembro.Text = somaMensalidadeSetembro.ToString();

            lblOutubro.Text = somaMensalidadeOutubro.ToString();

            lblNovembro.Text = somaMensalidadeNovembro.ToString();

            lblDezembro.Text = somaMensalidadeDezembro.ToString();

            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeJaneiro, somaMensalidadeFevereiro);

            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeJaneiro, somaMensalidadeFevereiro);

            lblPorcentagemFevereiro.Text = porcentagem.ToString("F2") + "%";


            if (verificaSeMaior)
            {
                lblPorcentagemFevereiro.TextColor = Color.Green;
                imageFevereiro.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeJaneiro, somaMensalidadeFevereiro);

                if (verificaSeIgual)
                {
                    lblPorcentagemFevereiro.TextColor = Color.Gray;
                    imageFevereiro.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemFevereiro.TextColor = Color.Red;
                    imageFevereiro.Source = "downarrow.png";
                }
            }

            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeFevereiro, somaMensalidadeMarco);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeFevereiro, somaMensalidadeMarco);

            lblPorcentagemMarco.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemMarco.TextColor = Color.Green;
                imageMarco.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeFevereiro, somaMensalidadeMarco);

                if (verificaSeIgual)
                {
                    lblPorcentagemMarco.TextColor = Color.Gray;
                    imageMarco.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemMarco.TextColor = Color.Red;
                    imageMarco.Source = "downarrow.png";
                }
            }

            //Marco-Abril
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeMarco, somaMensalidadeAbril);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeMarco, somaMensalidadeAbril);

            lblPorcentagemAbril.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemAbril.TextColor = Color.Green;
                imageAbril.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeMarco, somaMensalidadeAbril);

                if (verificaSeIgual)
                {
                    lblPorcentagemAbril.TextColor = Color.Gray;
                    imageAbril.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemAbril.TextColor = Color.Red;
                    imageAbril.Source = "downarrow.png";
                }
            }

            //Abril-Maio
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeAbril, somaMensalidadeMaio);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeAbril, somaMensalidadeMaio);

            lblPorcentagemMaio.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemMaio.TextColor = Color.Green;
                imageMaio.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeAbril, somaMensalidadeMaio);

                if (verificaSeIgual)
                {
                    lblPorcentagemMaio.TextColor = Color.Gray;
                    imageMaio.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemMaio.TextColor = Color.Red;
                    imageMaio.Source = "downarrow.png";
                }
            }

            //Maio-Junho
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeMaio, somaMensalidadeJunho);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeMaio, somaMensalidadeJunho);

            lblPorcentagemJunho.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemJunho.TextColor = Color.Green;
                imageJunho.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeMaio, somaMensalidadeJunho);

                if (verificaSeIgual)
                {
                    lblPorcentagemJunho.TextColor = Color.Gray;
                    imageJunho.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemJunho.TextColor = Color.Red;
                    imageJunho.Source = "downarrow.png";
                }
            }

            //Junho-Julho
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeJunho, somaMensalidadeJulho);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeJunho, somaMensalidadeJulho);

            lblPorcentagemJulho.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemJulho.TextColor = Color.Green;
                imageJulho.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeJunho, somaMensalidadeJulho);

                if (verificaSeIgual)
                {
                    lblPorcentagemJulho.TextColor = Color.Gray;
                    imageJulho.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemJulho.TextColor = Color.Red;
                    imageJulho.Source = "downarrow.png";
                }
            }

            //Julho-Agosto
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeJulho, somaMensalidadeAgosto);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeJulho, somaMensalidadeAgosto);

            lblPorcentagemAgosto.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemAgosto.TextColor = Color.Green;
                imageAgosto.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeJulho, somaMensalidadeAgosto);

                if (verificaSeIgual)
                {
                    lblPorcentagemAgosto.TextColor = Color.Gray;
                    imageAgosto.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemAgosto.TextColor = Color.Red;
                    imageAgosto.Source = "downarrow.png";
                }
            }

            //Agosto-Setembro
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeAgosto, somaMensalidadeSetembro);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeAgosto, somaMensalidadeSetembro);

            lblPorcentagemSetembro.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemSetembro.TextColor = Color.Green;
                imageSetembro.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeAgosto, somaMensalidadeSetembro);

                if (verificaSeIgual)
                {
                    lblPorcentagemSetembro.TextColor = Color.Gray;
                    imageSetembro.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemSetembro.TextColor = Color.Red;
                    imageSetembro.Source = "downarrow.png";
                }
            }

            //Setembro-Outubro
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeSetembro, somaMensalidadeOutubro);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeSetembro, somaMensalidadeOutubro);

            lblPorcentagemOutubro.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemOutubro.TextColor = Color.Green;
                imageOutubro.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeSetembro, somaMensalidadeOutubro);

                if (verificaSeIgual)
                {
                    lblPorcentagemOutubro.TextColor = Color.Gray;
                    imageOutubro.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemOutubro.TextColor = Color.Red;
                    imageOutubro.Source = "downarrow.png";
                }
            }

            //Outubro-Novembro
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeOutubro, somaMensalidadeNovembro);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeOutubro, somaMensalidadeNovembro);

            lblPorcentagemNovembro.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemNovembro.TextColor = Color.Green;
                imageNovembro.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeOutubro, somaMensalidadeNovembro);

                if (verificaSeIgual)
                {
                    lblPorcentagemNovembro.TextColor = Color.Gray;
                    imageNovembro.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemNovembro.TextColor = Color.Red;
                    imageNovembro.Source = "downarrow.png";
                }
            }

            //Novembro-Dezembro
            verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeNovembro, somaMensalidadeDezembro);
            porcentagem = Calculo.CalculaPorcentagem(somaMensalidadeNovembro, somaMensalidadeDezembro);

            lblPorcentagemDezembro.Text = porcentagem.ToString("F2") + "%";

            if (verificaSeMaior)
            {
                lblPorcentagemDezembro.TextColor = Color.Green;
                imageDezembro.Source = "uparrow.png";
            }
            else
            {
                verificaSeIgual = Calculo.VerificaSeIgual(somaMensalidadeNovembro, somaMensalidadeDezembro);

                if (verificaSeIgual)
                {
                    lblPorcentagemDezembro.TextColor = Color.Gray;
                    imageDezembro.Source = "rightarrow.png";
                }
                else
                {
                    lblPorcentagemDezembro.TextColor = Color.Red;
                    imageDezembro.Source = "downarrow.png";
                }
            }

            //verificaSeMaior = Calculo.VerificaSeMaior(somaMensalidadeFevereiro, somaMensalidadeMarco);

            if (DateTime.Today.Month == 1)
            {
                lblValor.Text = somaMensalidadeJaneiro.ToString();
            }

            if (DateTime.Today.Month == 2)
            {
                lblValor.Text = somaMensalidadeFevereiro.ToString();
            }

            if (DateTime.Today.Month == 3)
            {
                lblValor.Text = somaMensalidadeMarco.ToString();
            }

            if (DateTime.Today.Month == 4)
            {
                lblValor.Text = somaMensalidadeAbril.ToString();
            }

            if (DateTime.Today.Month == 5)
            {
                lblValor.Text = somaMensalidadeMaio.ToString();
            }

            if (DateTime.Today.Month == 6)
            {
                lblValor.Text = somaMensalidadeJunho.ToString();
            }

            if (DateTime.Today.Month == 7)
            {
                lblValor.Text = somaMensalidadeJulho.ToString();
            }

            if (DateTime.Today.Month == 8)
            {
                lblValor.Text = somaMensalidadeAgosto.ToString();
            }

            if (DateTime.Today.Month == 9)
            {
                lblValor.Text = somaMensalidadeSetembro.ToString();
            }

            if (DateTime.Today.Month == 10)
            {
                lblValor.Text = somaMensalidadeOutubro.ToString();
            }

            if (DateTime.Today.Month == 11)
            {
                lblValor.Text = somaMensalidadeNovembro.ToString();
            }

            if (DateTime.Today.Month == 12)
            {
                lblValor.Text = somaMensalidadeDezembro.ToString();
            }

            lblMesAtual.Text = DateTime.Today.ToString("MMMM").ToUpper();

            grafico.Chart = new LineChart() { Entries = entries };
        }
    }
}