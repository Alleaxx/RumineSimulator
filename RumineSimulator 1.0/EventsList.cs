using System.Collections.Generic;
using System.Linq;

namespace RumineSimulator
{
    internal class EventsList
    {
        public List<Events> EventsListArr = new List<Events>();

        public int ev_amount { get; private set; }

        public Events EventGenerate(
          string passed,
          UsersStorage Users,
          DateProgress Dates,
          double pages)
        {
            ++this.ev_amount;
            this.EventsListArr.Add(new Events(passed, Users, Dates, pages, this.EventsListArr.Count));
            Events events = this.EventsListArr[this.EventsListArr.Count - 1];
            for (int index = 0; index < Users.users.Count<User>(); ++index)
            {
                if (Users.users[index] == events.Creator)
                    events.Creator.UsersEvents.Add(events);
            }
            return events;
        }
    }
}
