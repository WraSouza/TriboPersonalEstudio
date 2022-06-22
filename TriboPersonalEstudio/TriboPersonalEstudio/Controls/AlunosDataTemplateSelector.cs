using System;
using System.Collections.Generic;
using System.Text;
using TriboPersonalEstudio.Model;
using Xamarin.Forms;

namespace TriboPersonalEstudio.Controls
{
    public  class AlunosDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AlunosDesabilitados { get; set; }
        public DataTemplate AlunosHabilitados { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Usuario)item).IsAtivo == true ? AlunosDesabilitados : AlunosHabilitados;
        }
    }
}
