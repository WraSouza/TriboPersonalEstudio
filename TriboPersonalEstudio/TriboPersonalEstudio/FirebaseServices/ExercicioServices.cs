using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.Model;

namespace TriboPersonalEstudio.FirebaseServices
{
    internal class ExercicioServices
    {
        FirebaseClient firebase;

        public ExercicioServices()
        {
            firebase = new FirebaseClient("https://tribopersonalacademia-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> CadastraExercicio(Exercicios exercicios)
        {
            
                await firebase.Child("Exercicio")
                    .PostAsync(new Exercicios()
                    {
                        NomeAluno = exercicios.NomeAluno,
                        DiaSemana = exercicios.DiaSemana,
                        GrupoExercicio = exercicios.GrupoExercicio,
                        CaminhoImagem = exercicios.CaminhoImagem,
                        HoraFinal = exercicios.HoraFinal,
                        HoraInicial = exercicios.HoraInicial
                    });

                return true;            

        }

        public async Task<List<Exercicios>> RetornaExercicios()
        {
            return (await firebase.Child("Exercicio")
                .OnceAsync<Exercicios>()).Select(item => new Exercicios
                {
                    NomeAluno = item.Object.NomeAluno,
                    DiaSemana = item.Object.DiaSemana,
                    GrupoExercicio = item.Object.GrupoExercicio,
                    CaminhoImagem = item.Object.CaminhoImagem,
                    HoraInicial = item.Object.HoraInicial,
                    HoraFinal = item.Object.HoraFinal

                }).ToList();
        }

        public async Task<List<Exercicios>> RetornaExercicioDia()
        {
            var alunos = await RetornaExercicios();            
           int diaAtual = (int)DateTime.Today.DayOfWeek;
            await firebase
                .Child("Usuario")
                .OnceAsync<Usuario>();

            return alunos.Where(d => d.DiaSemana.ToString() == diaAtual.ToString()).ToList();
        }
    }
}
