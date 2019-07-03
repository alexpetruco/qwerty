using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_UI
{
    class CashBoxView
    {
        CashDesk cashDesk;//as cashdesk
        public Label CashDeskName { get; set; }
        public NumericUpDown Price { get; set; } 
        public ProgressBar QueueLenght {get;set; }
        public Label LeaveCustomersCount { get; set; }
        public CashBoxView(CashDesk _cashDesk,int number,int x,int y)
        {
            cashDesk = _cashDesk;
            CashDeskName = new Label();
            Price = new NumericUpDown();
            LeaveCustomersCount = new Label();
            QueueLenght = new ProgressBar();
            // 
            // label
            // 
            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y);
            CashDeskName.Name = "label" + number;
            CashDeskName.Size = new System.Drawing.Size(35, 13);
            CashDeskName.TabIndex = number;
            CashDeskName.Text = cashDesk.ToString();
            // 
            // numericUpDown
            // 
            Price.Location = new System.Drawing.Point(x+80, y);
            Price.Name = "numericUpDown"+number;
            Price.Size = new System.Drawing.Size(120, 20);
            Price.TabIndex = number;
            Price.Maximum = 100000000000;
            // 
            // progressBar1
            //

            QueueLenght.Location = new System.Drawing.Point(x+250, y);
            QueueLenght.Maximum = cashDesk.MaxQueueLength;
            QueueLenght.Name = "progressBar"+number;
            QueueLenght.Size = new System.Drawing.Size(100, 23);
            QueueLenght.TabIndex = number;
            QueueLenght.Value = 0;

            LeaveCustomersCount.AutoSize = true;
            LeaveCustomersCount.Location = new System.Drawing.Point(x+400, y);
            LeaveCustomersCount.Name = "label2" + number;
            LeaveCustomersCount.Size = new System.Drawing.Size(35, 13);
            LeaveCustomersCount.TabIndex = number;
            LeaveCustomersCount.Text = "";

            cashDesk.Checkclosed += CashDesk_CheckClosed; //подписка на событие
         }

        private void CashDesk_CheckClosed(object sender, Check e)
        { 
                Price.Invoke((Action)delegate
             {
                Price.Value += e.Price;
                QueueLenght.Value = cashDesk.Count;
                LeaveCustomersCount.Text = cashDesk.ExitCustomer.ToString();
             }); // вызов подписки события из асинхронного потока в основной
            
        }
    }
}
 