using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTI_Testing__Mobile_.Models
{
    public class HistoryObject
    {
        private int recordId;
        private string userId;
        private int toolId;
        private DateTime checkoutTime;
        // If returntime has a year of 0001, it means it hasn't been returned yet
        private DateTime returnTime;
        public HistoryObject() { }
        public HistoryObject(int r, int t, string u, DateTime c, DateTime re)
        {
            this.recordId = r;
            this.userId = u;
            this.toolId = t;
            this.checkoutTime = c;
            this.returnTime = re;
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

        public int ToolId
        {
            get { return toolId; }
            set { toolId = value; }
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
    }
}
