using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KTI_Testing__Mobile_.Models
{
    public class Tool
    {
        private int id;

        private string name = "";

        private string icon;

        private bool status;


        public Tool(int i, string n, string s)
        {
            this.id = i;
            this.name = n;
            this.status = (s == "") ? true : false;

            
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
