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
    internal class UserServices
    {
        FirebaseClient firebase;

        public UserServices()
        {
            firebase = new FirebaseClient("https://tribopersonalacademia-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> LoginUser(string name, string passwd)
        {
            var user = (await firebase.Child("Usuario")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.NomeUsuario == name)
                .Where(u => u.Object.SenhaAluno == passwd)
                .FirstOrDefault();


            return (user != null);
        }

        public async Task<string> GetUserStatus(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.StatusAluno;
        }

        public async Task<bool> IsUSerExists(string nomeUsuario)
        {
            var user = (await firebase.Child("Usuario")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.NomeUsuario == nomeUsuario)
                .FirstOrDefault();

            return (user != null);
        }
        

        public async Task<bool> CadastraAluno(Usuario usuario)
        {
            if (await IsUSerExists(usuario.NomeUsuario) == false)
            {
                await firebase.Child("Usuario")
                    .PostAsync(new Usuario()
                    {
                        NomeUsuario = usuario.NomeUsuario,
                        NomeAluno = usuario.NomeAluno,
                        SenhaAluno = usuario.SenhaAluno,
                        StatusAluno = usuario.StatusAluno,
                        IsProfessor = usuario.IsProfessor,
                        QtdeVezesSemana = usuario.QtdeVezesSemana,
                        PeriodoContrato = usuario.PeriodoContrato,
                        TipoPlano = usuario.TipoPlano,
                        CreatedAt = usuario.CreatedAt,
                        VencimentoEm = usuario.VencimentoEm,
                        ValorMensalidade = usuario.ValorMensalidade,
                        CaminhoImagem = usuario.CaminhoImagem,
                        IsPaymentUpdated = usuario.IsPaymentUpdated,
                        CurrentMonth = usuario.CurrentMonth,
                        DataInicioPlano = usuario.DataInicioPlano
                    });

                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> GetUserProfile(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.IsProfessor;
        }

        public async Task<string> GetUserSenha(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.SenhaAluno;
        }

        public async Task<List<Usuario>> RetornaUsuarios()
        {
            return (await firebase.Child("Usuario")
                .OnceAsync<Usuario>()).Select(item => new Usuario
                {
                    NomeAluno = item.Object.NomeAluno,
                    IsProfessor = item.Object.IsProfessor,
                    TipoPlano = item.Object.TipoPlano,
                    VencimentoEm = item.Object.VencimentoEm,
                    ValorMensalidade = item.Object.ValorMensalidade,
                    PeriodoContrato = item.Object.PeriodoContrato,
                    CaminhoImagem = item.Object.CaminhoImagem,
                    CreatedAt = item.Object.CreatedAt,
                    StatusAluno = item.Object.StatusAluno,
                    IsPaymentUpdated = item.Object.IsPaymentUpdated,
                    CurrentMonth = item.Object.CurrentMonth

                }).ToList();
        }

        public async Task<bool> DesativarUsuario(string name)
        {
            var alunos = await RetornaUsuarios();
            var toUpdatePerson = (await firebase
              .Child("Usuario")
              .OnceAsync<Usuario>()).Where(a => a.Object.NomeAluno == name).FirstOrDefault();

            toUpdatePerson.Object.StatusAluno = "Bloqueado";

            await firebase
           .Child("Usuario")
           .Child(toUpdatePerson.Key)
           .PutAsync(toUpdatePerson.Object);

            return true;
        }

        public async Task<bool> DesativarUsuarioPlanoVencido()
        {
            var alunos = await RetornaAlunosAtivos();
            bool existeAluno = true;

            if (alunos != null)
            {
                foreach (var dados in alunos)
                {
                    if (DateTime.Parse(dados.VencimentoEm) < DateTime.Today)
                    {
                        var toUpdatePerson = (await firebase
                                            .Child("Usuario")
                                            .OnceAsync<Usuario>()).Where(a => DateTime.Parse(a.Object.VencimentoEm) < DateTime.Today).FirstOrDefault();


                        toUpdatePerson.Object.StatusAluno = "Inativo";

                        await firebase
                       .Child("Usuario")
                       .Child(toUpdatePerson.Key)
                       .PutAsync(toUpdatePerson.Object);

                    }
                    else
                    {
                        existeAluno = false;
                    }
                }
            }
            return existeAluno;

        }

        public async Task<List<Usuario>> RetornaAlunoEspecifico(string name)
        {
            var alunos = await RetornaUsuarios();
            await firebase
                .Child("Usuario")
                .OnceAsync<Usuario>();

            return alunos.Where(a => a.NomeAluno == name).ToList();
        }

        public async Task<List<Usuario>> RetornaAlunos()
        {
            var alunos = await RetornaUsuarios();
            await firebase
                .Child("Usuario")
                .OnceAsync<Usuario>();

            return alunos.Where(a => a.IsProfessor == false).ToList();
        }

        public async Task<List<Usuario>> RetornaAlunosInativos()
        {
            var alunos = await RetornaUsuarios();
            const string status = "Inativo";
            await firebase
                .Child("Usuario")
                .OnceAsync<Usuario>();

            return alunos.Where(a => a.StatusAluno == status).ToList();
        }

        public async Task<List<Usuario>> RetornaAlunosAtivos()
        {
            var alunos = await RetornaUsuarios();
            const string status = "Ativo";
            await firebase
                .Child("Usuario")
                .OnceAsync<Usuario>();

            return alunos.Where(a => a.StatusAluno == status && a.IsProfessor == false).ToList();
        }

        public async Task<bool> AtualizaStatusAlunoMensalidade(string nomeUsuario)
        {
            var alunos = await RetornaAlunosAtivos();
            var toUpdateUser = (await firebase
              .Child("Usuario")
              .OnceAsync<Usuario>()).Where(a => a.Object.NomeAluno == nomeUsuario).FirstOrDefault();

            toUpdateUser.Object.IsPaymentUpdated = true;
            toUpdateUser.Object.CurrentMonth = DateTime.Today.Month.ToString();

            await firebase
           .Child("Usuario")
           .Child(toUpdateUser.Key)
           .PutAsync(toUpdateUser.Object);

            return true;
        }

        public async Task<bool> RenovarPlano(Usuario usuario)
        {
            var alunos = await RetornaAlunosAtivos();
            var toUpdateUser = (await firebase
              .Child("Usuario")
              .OnceAsync<Usuario>()).Where(a => a.Object.NomeAluno == usuario.NomeAluno).FirstOrDefault();

            toUpdateUser.Object.CreatedAt = usuario.CreatedAt;
            toUpdateUser.Object.VencimentoEm = usuario.VencimentoEm;
            toUpdateUser.Object.StatusAluno = "Ativo";
            toUpdateUser.Object.IsPaymentUpdated = usuario.IsPaymentUpdated;
            toUpdateUser.Object.CurrentMonth = usuario.CurrentMonth;

            await firebase
           .Child("Usuario")
           .Child(toUpdateUser.Key)
           .PutAsync(toUpdateUser.Object);

            return true;
        }

        public async Task<List<Usuario>> RetornaAlunosMensalidadeNaoPaga()
        {
            string ultimoMesPago = ((DateTime.Today.Month) - 1).ToString();

            var alunos = await RetornaAlunosAtivos();
            await firebase
                .Child("Usuario")
                .OnceAsync<Usuario>();

            return alunos.Where(a => a.IsPaymentUpdated == false
            && a.CurrentMonth == DateTime.Today.Month.ToString() && a.IsProfessor == false && a.StatusAluno == "Ativo").ToList();
        }

        public async Task AlterarStatusAlunoMensalidadeNaoPaga()
        {
            var alunos = await RetornaAlunosAtivos();
            if(alunos.Count > 0)
            {
                string ultimoMesPago = ((DateTime.Today.Month) - 1).ToString();
                var toUpdateUser = (await firebase
                  .Child("Usuario")
                  .OnceAsync<Usuario>()).Where(a => a.Object.CurrentMonth == ultimoMesPago && a.Object.IsPaymentUpdated == true).FirstOrDefault();

                if(toUpdateUser != null)
                {
                    toUpdateUser.Object.IsPaymentUpdated = false;
                    toUpdateUser.Object.CurrentMonth = DateTime.Today.Month.ToString();


                    await firebase
                   .Child("Usuario")
                   .Child(toUpdateUser.Key)
                   .PutAsync(toUpdateUser.Object);
                }              

                
            }

        }
    }
}
