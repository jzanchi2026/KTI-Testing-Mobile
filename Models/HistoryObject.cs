using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KTI_Testing__Mobile_.Models
{
    public class HistoryObject
    {
        private int recordId;
        private string userId;
        private int id;
        private float takenQ;
        private float retQ;
        private DateTime checkoutTime;
        // If returntime has a year of 0001, it means it hasn't been returned yet
        private DateTime returnTime;
        public HistoryObject() { }
        //For tools
        public HistoryObject(int r, int t, string u, DateTime c, DateTime re)
        {
            this.recordId = r;
            this.userId = u;
            this.id = t;
            this.checkoutTime = c;
            this.returnTime = re;
        }
        //For materials
        public HistoryObject(int r, int t, string u, DateTime c, DateTime re, float tq, float rq)
        {
            this.recordId = r;
            this.userId = u;
            this.id = t;
            this.checkoutTime = c;
            this.returnTime = re;
            this.takenQ = tq;
            this.retQ = rq;
        }
        public int RecordId
        {
            get { return recordId; }
            set { recordId = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime CheckoutTime
        {
            get { return checkoutTime; }
            set { checkoutTime = value; }
        }
        public DateTime ReturnTime
        {
            get { return returnTime; }
            set { returnTime = value; }
        }
        public float TakenQ
        {
            get { return takenQ; }
            set { takenQ = value; }
        }
    }
}
