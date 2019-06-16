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
        CashDesk cashDesk;
        public Label Label { get; set; }
        public NumericUpDown NumericUpDown { get; set; }


        public CashBoxView(CashDesk _cashDesk,int number,int x,int y)
        {
            cashDesk = _cashDesk;
            Label = new Label();
            NumericUpDown = new NumericUpDown();

            Label.AutoSize = true;
            Label.Location = new System.Drawing.Point(x, y);
            Label.Name = "label1" + number;
            Label.Size = new System.Drawing.Size(35, 13);
            Label.TabIndex = number;
            Label.Text = cashDesk.ToString();
            // 
            // numericUpDown
            // 
            NumericUpDown.Location = new System.Drawing.Point(x+80, y);
            NumericUpDown.Name = "numericUpDown1"+number;
            NumericUpDown.Size = new System.Drawing.Size(120, 20);
            NumericUpDown.TabIndex = number;
        
         }
    }
}
 