using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    public class Story
    {
        public const string
            Deleted = "Silinmiş",
            NotStarted = "Başlamamış",
            Going = "Devam Ediyor",
            Done = "Tamamlandı";
        private string name { get; set; }
        private string description { get; set; }
        private DateTime startingDate { get; set; }
        private DateTime endingDate { get; set; }
        private string state { get; set; }
        private int deadline { get; set; }

        public Story(string Name, string Description, int Deadline)
        {
            this.startingDate = DateTime.Now.Date;
            this.state = StoryState.NotStarted;
            EditStory(Name, Description,Deadline);
        }
        public Story()
        { }

        public string GetName()
        {
            return name;
        }


        public string GetDescription()
        {
            return description;
        }


        public DateTime GetStartingDate()
        {
            return startingDate;
        }

        public void EditEndingDate(DateTime startingDate,int deadline)
        {
            this.endingDate = startingDate.AddDays(deadline);
        }

        public DateTime GetEndingDate()
        {
            return endingDate;
        }

        public string GetState()
        {
            return state;
        }

        public int GetDeadline()
        {
            return deadline;
        }

        public void EditStory(string Name, string Description, int Deadline)
        {
            this.name = Name;
            this.description = Description;
            this.deadline = Deadline;
        }

        public void EditState(string state)
        {
            this.state = state;
        }
       
    }
}
