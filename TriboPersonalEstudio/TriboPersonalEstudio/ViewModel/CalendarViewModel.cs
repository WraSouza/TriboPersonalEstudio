using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.Services;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class CalendarViewModel : BaseViewModel
    {
        public ObservableCollection<ExerciciosGroup> Exercicios { get; private set; } = new ObservableCollection<ExerciciosGroup>();

        public Command AtualizaTela { get; }

        public CalendarViewModel()
        {
            BuscaCalendario();
            AtualizaTela = new Command(AtualizarTela);            
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

        async void AtualizarTela()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                Exercicios.Clear();
                BuscaCalendario();


                IsRefreshing = false;
            }
            else
            {
                Mensagem.MensagemErroConexao();

                IsRefreshing = false;
            }


        }

        async void BuscaCalendario()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                ExercicioServices dados = new ExercicioServices();
                var dadosCalendario = await dados.RetornaExercicios();
                ObservableCollection<Exercicios> novoCalendarioSegunda = new ObservableCollection<Exercicios>();
                ObservableCollection<Exercicios> novoCalendarioTerca = new ObservableCollection<Exercicios>();
                ObservableCollection<Exercicios> novoCalendarioQuarta = new ObservableCollection<Exercicios>();
                ObservableCollection<Exercicios> novoCalendarioQuinta = new ObservableCollection<Exercicios>();
                ObservableCollection<Exercicios> novoCalendarioSexta = new ObservableCollection<Exercicios>();
                ObservableCollection<Exercicios> novoCalendarioSabado = new ObservableCollection<Exercicios>();
                

                foreach (var item in dadosCalendario.OrderByDescending(x => x.DiaSemana).Reverse())
                {
                    switch (item.DiaSemana)
                    {
                        case "1":
                            novoCalendarioSegunda.Add(item);
                            break;

                        case "2":
                            novoCalendarioTerca.Add(item);
                            break;

                        case "3":
                            novoCalendarioQuarta.Add(item);
                            break;

                        case "4":
                            novoCalendarioQuinta.Add(item);
                            break;

                        case "5":
                            novoCalendarioSexta.Add(item);
                            break;

                        case "6":
                            novoCalendarioSabado.Add(item);
                            break;                        

                    }

                }

                if (novoCalendarioSegunda.Count > 0)
                {
                    Exercicios.Add(new ExerciciosGroup("Segunda-Feira", novoCalendarioSegunda));
                }

                if (novoCalendarioTerca.Count > 0)
                {
                    Exercicios.Add(new ExerciciosGroup("Terça-Feira", novoCalendarioTerca));
                }

                if (novoCalendarioQuarta.Count > 0)
                {
                    Exercicios.Add(new ExerciciosGroup("Quarta-Feira", novoCalendarioQuarta));
                }

                if (novoCalendarioQuinta.Count > 0)
                {
                    Exercicios.Add(new ExerciciosGroup("Quinta-Feira", novoCalendarioQuinta));
                }

                if (novoCalendarioSexta.Count > 0)
                {
                    Exercicios.Add(new ExerciciosGroup("Sexta-Feira", novoCalendarioSexta));
                }

                if (novoCalendarioSabado.Count > 0)
                {
                    Exercicios.Add(new ExerciciosGroup("Sábado", novoCalendarioSabado));
                }
                
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }



        }
    }
}
