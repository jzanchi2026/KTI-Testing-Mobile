using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTI_Testing__Mobile_.Models
{
    public  class Material
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int currentAmount { get; set; }
        public Material(int i, string n, int q, int c)
        {
            this.id = i;
            this.name = n;
            this.quantity = q;
            this.currentAmount = c;

            
        }

    }
}
