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
    internal class MensalidadeServices
    {
        FirebaseClient firebase;

        public MensalidadeServices()
        {
            firebase = new FirebaseClient("https://tribopersonalacademia-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> CadastraAlunoMensalidade(Mensalidade dadosMensalidade)
        {
            await firebase.Child("Mensalidade")
                .PostAsync(new Mensalidade()
                {
                    CaminhoImagem = dadosMensalidade.CaminhoImagem,
                    NomeAluno = dadosMensalidade.NomeAluno,
                    IsPaid = dadosMensalidade.IsPaid,
                    Mes = dadosMensalidade.Mes,
                    ValorMensalidade = dadosMensalidade.ValorMensalidade
                });

            return true;

        }

        public async Task<List<Mensalidade>> RetornaTodasMensalidades()
        {
            return (await firebase.Child("Mensalidade")
                .OnceAsync<Mensalidade>()).Select(item => new Mensalidade
                {
                    IsPaid = item.Object.IsPaid,
                    Mes = item.Object.Mes,
                    ValorMensalidade = item.Object.ValorMensalidade

                }).ToList();
        }

        public async Task<List<Mensalidade>> RetornaMensalidade()
        {
            var todasMensalidade = await RetornaTodasMensalidades();

            await firebase
                .Child("Mensalidade")
                .OnceAsync<Mensalidade>();

            return todasMensalidade.Where(a => a.IsPaid == true).ToList();            

        }

        public async Task<bool> CadastraMensalidadePaga(Mensalidade dadosMensalidade)
        {
            await firebase.Child("Mensalidade")
                .PostAsync(new Mensalidade()
                {
                    NomeAluno = dadosMensalidade.NomeAluno,
                    CaminhoImagem = dadosMensalidade.CaminhoImagem,
                    Mes = dadosMensalidade.Mes,
                    IsPaid = dadosMensalidade.IsPaid,
                    ValorMensalidade = dadosMensalidade.ValorMensalidade,

                });

            return true;
        }
    }
}
