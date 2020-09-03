using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Customer_Scheduling_Application
{
    public partial class Count_Report_by_Type : Form
    {
        public Count_Report_by_Type()
        {
            InitializeComponent();
            IDictionary<string, object> dictionary = Database.appointmentByTypeCount();
            cleanings.Text = dictionary["cleaning"].ToString();
            cavities.Text = dictionary["cavities"].ToString();
            wisdomTeeth.Text = dictionary["wisdomTeeth"].ToString();
            consultations.Text = dictionary["consultation"].ToString();
            rootCanals.Text = dictionary["rootCanals"].ToString();
            other.Text = dictionary["other"].ToString();
            label1.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        
    }
}
