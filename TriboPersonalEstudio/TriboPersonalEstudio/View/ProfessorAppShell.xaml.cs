using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriboPersonalEstudio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessorAppShell : Shell
    {
        public ProfessorAppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CadastrarAlunoView), typeof(CadastrarAlunoView));

            Routing.RegisterRoute(nameof(AlunosView), typeof(AlunosView));

            Routing.RegisterRoute(nameof(FinancasView), typeof(FinancasView));

            Routing.RegisterRoute(nameof(MensalidadeMesView), typeof(MensalidadeMesView));

            Routing.RegisterRoute(nameof(AlunoDetailView), typeof(AlunoDetailView));

            Routing.RegisterRoute(nameof(ProfessorMainView), typeof(ProfessorMainView));

            Routing.RegisterRoute(nameof(CadastroExercicioAlunoView), typeof(CadastroExercicioAlunoView));
        }
    }
}