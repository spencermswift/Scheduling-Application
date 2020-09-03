using MySql.Data.MySqlClient;
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
    public partial class Appointment_Report_by_Customer : Form
    {
        public Appointment_Report_by_Customer()
        {
            InitializeComponent();
            populateCustomerList();
            custComboBox.SelectedItem = null;
            dataGridView1.Visible = false;
            custComboBox.Text = "--Select--";
            label2.Text = DateTime.Now.ToString();
        }
        public void populateCustomerList()
        {
            MySqlConnection connection = new MySqlConnection(Database.getConnectionString());

            try
            {
                string query = "SELECT customerId, concat(customerName, ' --ID: ', customerId) as Display FROM customer;";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, connection);
                connection.Open();
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "Cust");
                custComboBox.DisplayMember = "Display";
                custComboBox.ValueMember = "customerId";
                custComboBox.DataSource = dataSet.Tables["Cust"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured! " + ex);
            }
        }

        private void CustComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (custComboBox.SelectedIndex != -1)
            {
                int id = Convert.ToInt32(custComboBox.SelectedValue);
                DataTable dataTableRecord = Database.getAppointmentListByCustomer(id.ToString());
                dataGridView1.DataSource = dataTableRecord;
                dataGridView1.Visible = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
