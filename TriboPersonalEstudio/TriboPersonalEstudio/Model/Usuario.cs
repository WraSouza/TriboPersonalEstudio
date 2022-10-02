using System;
using System.Collections.Generic;
using System.Text;

namespace TriboPersonalEstudio.Model
{
    internal class Usuario
    {
        public string NomeAluno { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaAluno { get; set; }
        public bool IsProfessor { get; set; }
        public string StatusAluno { get; set; }
        public string CreatedAt { get; set; }
        public string DataInicioPlano { get; set; }
        public string VencimentoEm { get; set; }
        public object TipoPlano { get; set; }
        public object QtdeVezesSemana { get; set; }
        public object PeriodoContrato { get; set; }
        public string ValorMensalidade { get; set; }
        public string CaminhoImagem { get; set; }
        public bool IsPaymentUpdated { get; set; }
        public string CurrentMonth { get; set; }
        public string AnoAtual { get; set; }
    }
}
