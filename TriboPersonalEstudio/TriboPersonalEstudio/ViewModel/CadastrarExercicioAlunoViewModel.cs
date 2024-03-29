﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.FirebaseServices;
using TriboPersonalEstudio.Model;
using TriboPersonalEstudio.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.ViewModel
{
    internal class CadastrarExercicioAlunoViewModel : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; private set; } = new ObservableCollection<Usuario>();

        readonly UserServices usuarios = new UserServices();

        readonly ExercicioServices exercicioServices = new ExercicioServices();

        private object _rdButtonDiaSemana;

        private string _grupoExercicios;
        private TimeSpan horaInicial;
        private TimeSpan horaFinal;

        public Command CadastrarExercicioAlunoCommand { get; set; }

        public CadastrarExercicioAlunoViewModel()
        {
            BuscaAluno();
            CadastrarExercicioAlunoCommand = new Command<Usuario>(async (model) => await CadastrarAluno(model));
        }

        public string GrupoExercicios
        {
            get => _grupoExercicios;
            set
            {
                _grupoExercicios = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraInicial
        {
            get => horaInicial;
            set
            {
                horaInicial = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraFinal
        {
            get => horaFinal;
            set
            {
                horaFinal = value;
                OnPropertyChanged();
            }
        }

        public object DiaSemanaButton
        {
            get => _rdButtonDiaSemana;
            set
            {
                _rdButtonDiaSemana = value;
                OnPropertyChanged();
            }
        }
        
        async Task CadastrarAluno(Usuario model)
        {
            object diaSemana = DiaSemanaButton;
            string nomeAluno = model.NomeAluno;
            string hora = HoraInicial.ToString();
            var novoExercicio = new Exercicios()
            {
                NomeAluno = nomeAluno,
                DiaSemana = diaSemana,
                GrupoExercicio = GrupoExercicios,
                CaminhoImagem = Preferences.Get("ImagemAluno", "default_value"),
                HoraInicial = String.Format("{0:00}:{1:00}",HoraInicial.Hours, HoraInicial.Minutes),
                HoraFinal = String.Format("{0:00}:{1:00}", HoraFinal.Hours, HoraFinal.Minutes)
            };

            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                if (DiaSemanaButton != null)
                {
                    if (String.IsNullOrEmpty(GrupoExercicios))
                    {
                        Mensagem.MensagemCamposObrigatorios();
                    }
                    else
                    {
                        bool verificaHorarios = Horario.VerificaHorario(HoraInicial, HoraFinal);
                        if (verificaHorarios)
                        {

                            bool confirmaCadastro = await exercicioServices.CadastraExercicio(novoExercicio);

                            if (confirmaCadastro)
                            {
                                Mensagem.MensagemCadastroComSucesso();
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Ops..", "Horário Final Deve Ser Maior que Hora de Início", "OK");
                        }
                    }
                    

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ops..", "Favor Selecionar Um Dia da Semana", "OK");
                }


            }
            else
            {
                Mensagem.MensagemErroConexao();
            }
        }

        async void BuscaAluno()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                string nomeAluno = Preferences.Get("NomeAluno", "default_value");

                var usuariosCadastrados = await usuarios.RetornaAlunoEspecifico(nomeAluno);

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
    }
}
