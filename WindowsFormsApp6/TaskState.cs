using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    public class TaskState
    {
        public const string
                Deleted = "Silinmiş",
                Backlog = "Backlog",
                Sprint = "Sprint",
                ToDo = "Yapılacak",
                Doing = "Yapılıyor",
                Testing = "Test Ediliyor",
                Done = "Tamamlandı";
    }
}
