using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TriboPersonalEstudio.Services
{
    public class Horario
    {
        public static bool VerificaHorario(TimeSpan horaInicial, TimeSpan horaFinal)
        {
            bool confirmaMaior = false;

            if(horaFinal > horaInicial)
            {
                confirmaMaior = true;
            }

            return confirmaMaior;
        }
    }
}
