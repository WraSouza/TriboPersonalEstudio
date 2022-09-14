using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TriboPersonalEstudio.Model
{
    internal class ExerciciosGroup : ObservableCollection<Exercicios>
    {
        public string DiaSemana { get; private set; }

        public ExerciciosGroup(string diaSemana, ObservableCollection<Exercicios> calendario) : base(calendario)
        {
            DiaSemana = diaSemana;
        }

        public override string ToString()
        {
            return DiaSemana;
        }
    }
}
