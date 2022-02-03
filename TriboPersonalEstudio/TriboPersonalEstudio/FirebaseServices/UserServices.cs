using Firebase.Database;
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
                .Where(u => u.Object.NomeAcesso == name)
                .Where(u => u.Object.SenhaAluno == passwd)
                .FirstOrDefault();


            return (user != null);
        }            

        public async Task<bool> GetUserStatus(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeAcesso == name)
               .FirstOrDefault();

            return user.Object.IsAtivo;
        }

        public async Task<bool> GetUserProfile(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeAcesso == name)
               .FirstOrDefault();

            return user.Object.IsProfessor;
        }

        public async Task<string> GetUserSenha(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeAcesso == name)
               .FirstOrDefault();

            return user.Object.SenhaAluno;
        }
    }
}
