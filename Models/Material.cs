using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTI_Testing__Mobile_.Models
{
    public  class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public float CurrentAmount { get; set; }
        public Material(int i, string n, float q, float c)
        {
            this.Id = i;
            this.Name = n;
            this.Quantity = q;
            this.CurrentAmount = c;
        }

    }
}
