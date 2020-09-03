using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Customer_Scheduling_Application
{
    public partial class Modify_Appointment : Form
    {
        public static List<KeyValuePair<string, object>> appointmentList;
        public void setAppointList(List<KeyValuePair<string,object>> list)
        {
            appointmentList = list;
        }
        public static List<KeyValuePair<string,object>> getAppointList()
        {
            return appointmentList;
        }
        public Modify_Appointment()
        {
            InitializeComponent();
            populateCustomerList();
            comboBoxDefaultSettings();
        }

        public void comboBoxDefaultSettings()
        {
            customerComboBox.SelectedItem = null;
            customerComboBox.Text = "--Select--";
            appointmentComboBox.Enabled = false;
            appointmentText.Enabled = false;
            descriptionText.Enabled = false;
            locationText.Enabled = false;
            contactText.Enabled = false;
            typeComboBox.Enabled = false;
            startDateValue.Enabled = false;
            endDateValue.Enabled = false;
            updateButton.Enabled = false;
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
                customerComboBox.DisplayMember = "Display";
                customerComboBox.ValueMember = "customerId";
                customerComboBox.DataSource = dataSet.Tables["Cust"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured! " + ex);
            }
        }


        public void populateAppointmentList()
        {
            MySqlConnection connection = new MySqlConnection(Database.getConnectionString());
            string utcOffset = (TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).ToString().Substring(0, 6));
            try
            {
                string query = $"SELECT appointmentId, concat(type, ' --Time: ', DATE_FORMAT(CONVERT_TZ(start, '+00:00', '{utcOffset}'), '%M %D %Y %r')) as Display FROM appointment WHERE customerId = '{customerComboBox.SelectedValue}';";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, connection);
                connection.Open();
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "Appoint");
                appointmentComboBox.DisplayMember = "Display";
                appointmentComboBox.ValueMember = "appointmentId";
                appointmentComboBox.DataSource = dataSet.Tables["Appoint"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured! " + ex);
            }
        }

        private void CustomerComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            populateAppointmentList();
            appointmentComboBox.Enabled = true;
            appointmentText.Enabled = false;  
            descriptionText.Enabled = false;
            locationText.Enabled = false;
            contactText.Enabled = false;
            typeComboBox.Enabled = false;
            startDateValue.Enabled = false;
            endDateValue.Enabled = false;
            updateButton.Enabled = false;
            appointmentText.Text = null;
            descriptionText.Text = null;
            locationText.Text = null;
            contactText.Text = null;
            typeComboBox.SelectedItem = null;
        }

        private void AppointmentComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRowView dataRowView = appointmentComboBox.SelectedItem as DataRowView;
            int id = Convert.ToInt32(appointmentComboBox.SelectedValue);
            var appointmentList = Database.getAppointmentList(id);
            setAppointList(appointmentList);

            if (appointmentComboBox.SelectedIndex != -1)
            {
                appointmentText.Enabled = true;
                descriptionText.Enabled = true;
                locationText.Enabled = true;
                contactText.Enabled = true;
                typeComboBox.Enabled = true;
                startDateValue.Enabled = true;
                endDateValue.Enabled = true;
                updateButton.Enabled = true;
                populateFields(appointmentList);
            }
        }
        public void populateFields(List<KeyValuePair<string, object>> appointmentList)
        {
            //lambda expressions used to retrieve values from appointmentList
            appointmentText.Text = appointmentList.First(kvp => kvp.Key == "title").Value.ToString();
            descriptionText.Text = appointmentList.First(kvp => kvp.Key == "description").Value.ToString();
            locationText.Text = appointmentList.First(kvp => kvp.Key == "location").Value.ToString();
            contactText.Text = appointmentList.First(kvp => kvp.Key == "contact").Value.ToString();
            typeComboBox.SelectedIndex = typeComboBox.FindStringExact(appointmentList.First(kvp => kvp.Key == "type").Value.ToString());
            string start = appointmentList.Find(kvp => kvp.Key == "start").Value.ToString();
            string end = appointmentList.Find(kvp => kvp.Key == "end").Value.ToString();
            startDateValue.Value = Convert.ToDateTime(start).ToLocalTime(); 
            endDateValue.Value = Convert.ToDateTime(end).ToLocalTime();
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            bool pass = emptyCheck();
            if (pass == true)
            {
                DialogResult confirmation = MessageBox.Show("Are you sure you want to update this appointment?", "", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    try
                    {
                        DateTime start = startDateValue.Value.ToUniversalTime();
                        DateTime end = endDateValue.Value.ToUniversalTime();
                        int available = appointmentAllowed(start, end);
                        switch (available)
                        {
                            case 0:
                                var list = getAppointList();
                                //lambda expression to convert the appointment list to a dictionary
                                IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                                dictionary["appointmentId"] = appointmentComboBox.SelectedValue;
                                dictionary["customerId"] = customerComboBox.SelectedValue;
                                dictionary["title"] = appointmentText.Text;
                                dictionary["description"] = descriptionText.Text;
                                dictionary["location"] = locationText.Text;
                                dictionary["contact"] = contactText.Text;
                                dictionary["type"] = typeComboBox.SelectedItem.ToString();
                                dictionary["start"] = startDateValue.Value;
                                dictionary["end"] = endDateValue.Value;
                                dictionary["url"] = customerComboBox.SelectedValue;
                                Database.updateAppointment(dictionary);
                                MessageBox.Show("Customer appointment successfully updated!");
                                this.Owner.Show();
                                this.Close();
                                break;
                            case 1:
                                MessageBox.Show("The appointment is outside of business hours. Please choose a different time."); ;
                                break;
                            case 2:
                                MessageBox.Show("The appointment conflicts with another appointment. Please choose another time.");
                                break;
                            case 3:
                                MessageBox.Show("The appointment start is after the end time.");
                                break;
                            case 4:
                                MessageBox.Show("The appointments start and end date are not on the same date.");
                                break;
                        }


                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            if (pass == false)
            {
                MessageBox.Show("Please enter a value for all fields.");
            }
        }

        private bool emptyCheck()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int appointmentAllowed(DateTime start, DateTime end)
        {

            DateTime systemStart = start.ToLocalTime();
            DateTime systemEnd = end.ToLocalTime();
            DateTime businessStart = DateTime.Today.AddHours(8);
            DateTime businessEnd = DateTime.Today.AddHours(17);

            if (systemStart.TimeOfDay < businessStart.TimeOfDay || systemEnd.TimeOfDay > businessEnd.TimeOfDay)
            {
                return 1;
            }
            if (Database.appointmentOverlaps(start, end) == true)
            {
                return 2;
            }
            if (systemStart.TimeOfDay > systemEnd.TimeOfDay)
            {
                return 3;
            }
            if (systemStart.Date != systemEnd.Date)
            {
                return 4;
            }

            return 0;
        }
    }
}
