using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TriboPersonalEstudio.Services
{
    public class Mensagem
    {
        public static void MensagemErroConexao()
        {
            Application.Current.MainPage.DisplayAlert("Ops!!", "Algo Deu Errado. Verifique Sua Conexão de Internet.", "OK");
        }

        public static void MensagemCamposObrigatorios()
        {
            Application.Current.MainPage.DisplayAlert("Erro", "Os Campos São Obrigatórios", "OK");
        }

        public static void MensagemCadastroComSucesso()
        {
            Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastrado Realizado Com Sucesso", "OK");
        }

        public static Task<bool> MensagemMesAtualPago()
        {
            var resposta = Application.Current.MainPage.DisplayAlert("", "O Mês Atual Está Pago?", "Sim", "Não");

            return resposta;
        }

        public static void MensagemRenovacaoPlano()
        {
             Application.Current.MainPage.DisplayAlert("Parabéns", "Renovação do Plano Realizada Com Sucesso","OK");            
        }





    }
}
