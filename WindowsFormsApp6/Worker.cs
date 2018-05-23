using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6
{
    public class Worker
    {
        private string name { get; set; }
        private System.Drawing.Color personalColor = System.Drawing.Color.Silver;
        private int experience { get; set; }


        public Worker(string Name,System.Drawing.Color Color)
        {
            this.experience = 0;
            EditWorker(Name, Color);
        }
        public Worker()
        {
        }


        public string GetName()
        {
            return name;
        }

        public int GetExp()
        {
            return experience;
        }

        public System.Drawing.Color GetColor()// kişisel çalışan rengi döndür 
        {
            return personalColor;
        }


        public void EditWorker(string Name,System.Drawing.Color Color)
        {
            this.name = Name;
            this.personalColor = Color;
        }

    }
}
