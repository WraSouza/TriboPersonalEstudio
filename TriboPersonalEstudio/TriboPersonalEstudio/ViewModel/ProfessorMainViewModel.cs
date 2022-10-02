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
    internal class ProfessorMainViewModel : BaseViewModel
    {
        public ObservableCollection<Exercicios> Exercicios { get; private set; } = new ObservableCollection<Exercicios>();
        public Command AtualizaTela { get; }

        readonly ExercicioServices exercicioServices = new ExercicioServices();

        public ProfessorMainViewModel()
        {
            BuscaExercicios();
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
                
                BuscaExercicios();
                
                IsRefreshing = false;
            }
            else
            {
                Mensagem.MensagemErroConexao();

                IsRefreshing = false;
            }

        }

        async void BuscaExercicios()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                var exerciciosCadastrados = await exercicioServices.RetornaExercicioDia();

                foreach (var exercicios in exerciciosCadastrados.OrderByDescending(x => x.HoraInicial).Reverse())
                {
                    Exercicios.Add(exercicios);
                }

            }
            else
            {
                Mensagem.MensagemErroConexao();
            }
        }
    }
}
