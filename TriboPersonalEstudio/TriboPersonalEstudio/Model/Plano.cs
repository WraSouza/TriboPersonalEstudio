using System;
using System.Collections.Generic;
using System.Text;

namespace TriboPersonalEstudio.Model
{
    internal class Plano
    {
        public string Periodicidade { get; set; }
        public string TipoPlano { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadePorSemana { get; set; }
    }
}
