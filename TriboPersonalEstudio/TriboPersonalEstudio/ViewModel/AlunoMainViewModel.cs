using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriboPersonalEstudio.ViewModel
{
    internal class AlunoMainViewModel : BaseViewModel
    {
        public AlunoMainViewModel()
        {
            MostraMensagem();
        }

        async void MostraMensagem()
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Description = "Academia Hoje",
                Title = "Mensagem da Tribo Personal Estudio",
                ReturningData = "Aula Hoje",                
                Schedule =  {
                               NotifyTime = DateTime.Now.AddSeconds(5) // Used for Scheduling local notification, if not specified notification will show immediately.
                          }

            };

            await LocalNotificationCenter.Current.Show(notification);
        }
    }
}
