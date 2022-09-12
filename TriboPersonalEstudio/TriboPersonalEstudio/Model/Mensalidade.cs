using System;
using System.Collections.Generic;
using System.Text;

namespace TriboPersonalEstudio.Model
{
    internal class Mensalidade
    {
        public string NomeAluno { get; set; }
        public string CaminhoImagem { get; set; }
        public string Mes { get; set; }
        public bool IsPaid { get; set; }
        public string ValorMensalidade { get; set; }
        public string AnoAtual { get; set; }
    }
}
