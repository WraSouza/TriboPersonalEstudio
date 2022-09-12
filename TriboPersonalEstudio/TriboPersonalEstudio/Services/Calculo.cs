using System;
using System.Collections.Generic;
using System.Text;

namespace TriboPersonalEstudio.Services
{
    internal class Calculo
    {
        public static bool VerificaSeMaior(float rendaMesAnterior, float rendaMesAtual)
        {
            bool confirmaVerificacao = false;

            if (rendaMesAtual > rendaMesAnterior)
            {
                confirmaVerificacao = true;
            }

            return confirmaVerificacao;
        }

        public static bool VerificaSeIgual(float rendaMesAnterior, float rendaMesAtual)
        {
            bool confirmaVerificacao = false;

            if (rendaMesAtual == rendaMesAnterior)
            {
                confirmaVerificacao = true;
            }

            return confirmaVerificacao;
        }

        public static float CalculaPorcentagem(float rendaMesAnterior, float rendaMesAtual)
        {
            float resultado = 0f;

            if ((rendaMesAtual == 0 && rendaMesAnterior == 0) || (rendaMesAnterior == rendaMesAtual))
            {
                return resultado;
            }
            else if (rendaMesAtual > rendaMesAnterior && rendaMesAnterior == 0)
            {
                resultado = ((rendaMesAtual - rendaMesAnterior) / rendaMesAtual) * 100;
            }
            else if (rendaMesAtual < rendaMesAnterior)
            {
                resultado = ((rendaMesAnterior - rendaMesAtual) / rendaMesAnterior) * 100;
            }
            else if (rendaMesAtual >= rendaMesAnterior)
            {
                resultado = ((rendaMesAtual - rendaMesAnterior) / rendaMesAnterior) * 100;
            }



            return resultado;
        }
    }
}
