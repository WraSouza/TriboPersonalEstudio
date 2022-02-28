using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriboPersonalEstudio.Model;

namespace TriboPersonalEstudio.FirebaseServices
{
    internal class PlanoServices
    {
        FirebaseClient firebase;

        public PlanoServices()
        {
            firebase = new FirebaseClient("https://tribopersonalacademia-default-rtdb.firebaseio.com/");
        }

        public async Task<List<Plano>> RetornaPlanosIndividuais()
        {
            var planosIndividuais = await RetornaPlanos();

            await firebase
                .Child("Plano")
                .OnceAsync<Plano>();

            return planosIndividuais.Where(a => a.TipoPlano == "Individual").ToList();
        }

        public async Task<List<Plano>> RetornaPlanos()
        {
            return (await firebase.Child("Plano")
                .OnceAsync<Plano>()).Select(item => new Plano
                {
                    Periodicidade = item.Object.Periodicidade,
                    QuantidadePorSemana = item.Object.QuantidadePorSemana,
                    TipoPlano = item.Object.TipoPlano,
                    Valor = item.Object.Valor
                }).ToList();

        }
    }
}
