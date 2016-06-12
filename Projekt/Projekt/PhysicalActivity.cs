using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    //PhysicalActivity
    class PhysicalActivity
    {
        public string Date { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public double Calories { get; set; }
        public string Info { get; set; }
        public ActivityType ActivityType { get; set; }

        public virtual string GetInfo()
        {
            return this.ActivityType + "_Acivity";
        }
    }
    
}
