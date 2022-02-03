using System;
using System.Collections.Generic;
using System.Text;

namespace TriboPersonalEstudio.Model
{
    internal class Usuario
    {
        public string NomeAluno { get; set; }
        public string NomeAcesso { get; set; }
        public string NomeCurto { get; set; }
        public string SenhaAluno { get; set; }
        public bool IsProfessor { get; set; }
        public bool IsAtivo { get; set; }
    }
}
