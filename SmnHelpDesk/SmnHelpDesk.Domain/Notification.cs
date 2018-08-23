using System.Collections.Generic;
using System.Linq;

namespace SmnHelpDesk.Domain
{
    public class Notification
    {
        public Notification()
        {
            Notifications = new List<string>();
        }

        private List<string> Notifications { get; set; }

        public void Add(string mensagem)
        {
            Notifications.Add(mensagem);
        }

        public bool Any => Notifications.Any();

        public List<string> Get => Notifications;
    }
}
