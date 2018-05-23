using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    public class Task
    {
        // belki task story ve workerlara ID eklenebilir 
        private int story_id { get; set; }
        private int worker_id { get; set; }
        private string taskName { get; set; }
        private string details { get; set; }
        private DateTime commitmentDate { get; set; }
        private DateTime endingDate { get; set; }
        private string state { get; set; }
        private int deadline { get; set; }

        public Task(int story_id,string gorevTanimi,string gorevDetayi, DateTime commDate, int deadline,int worker_id)
        {
            this.state = TaskState.Backlog;
            EditTask(story_id, gorevTanimi, gorevDetayi, commDate ,deadline,worker_id);
        }
        public Task()
        {
        }
        

        public string GetTaskName()// task adını geri döndür
        {
            return taskName;
        }


        public string GetDetails()// task'ın detaylarını döndür
        {
            return details;
        }

        public int GetDeadline()// task'ın detaylarını döndür
        {
            return deadline;
        }

        public DateTime GetEndingDate()// task'ın detaylarını döndür
        {
            return endingDate;
        }

        public DateTime GetCommitmentDate()// task başlama tarihini döndür
        {
            return commitmentDate;
        }


       public int GetWorker()// task görevlisini döndür
        {
            return worker_id;
        }

        public string GetTaskState()
        {
            return state;
        }

        public int GetStory()
        {
            return story_id;
        }

        public void EditTask(int story_id, string gorevTanimi, string gorevDetayi, DateTime commDate, int deadline, int worker_id)
        {
            this.story_id = story_id;
            this.taskName = gorevTanimi;
            this.details = gorevDetayi;
            this.worker_id = worker_id;
            this.commitmentDate = commDate;
            this.deadline = deadline;
            this.endingDate = commDate.AddDays(deadline);
        }

        public void EditState(string state)
        {
            this.state = state;
        }


    }
}
