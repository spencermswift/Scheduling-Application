using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Customer_Scheduling_Application
{
    public partial class Portal : Form
    {
        public Portal()
        {
            InitializeComponent();
            appointmentCalendar.DataSource = displayCalendar(weekRadio.Checked);
           // displayReminder(appointmentCalendar);
        }
        public void updateCalendar()
        {
            appointmentCalendar.DataSource = displayCalendar(weekRadio.Checked);
        }
        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            Form addCustomer = new Add_Customer();
            addCustomer.Owner = this;
            addCustomer.Show();
            this.Hide();

        }

        private void ModifyCustomerButton_Click(object sender, EventArgs e)
        {
            Form modifyCustomer = new Modify_Customer();
            modifyCustomer.Owner = this;
            modifyCustomer.Show();
            this.Hide();
        }

        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            Form deleteCustomer = new Delete_Customer();
            deleteCustomer.Owner = this;
            deleteCustomer.Show();
            this.Hide();
        }

        private void AddAppointmentButton_Click(object sender, EventArgs e)
        {
            Form addAppointment = new Add_Appointment();
            addAppointment.Owner = this;
            addAppointment.Show();
            this.Hide();
        }

        private void ModifyAppointmentButton_Click(object sender, EventArgs e)
        {
            Form modifyAppointment = new Modify_Appointment();
            modifyAppointment.Owner = this;
            modifyAppointment.Show();
            this.Hide();
        }

        private void DeleteAppointmentButton_Click(object sender, EventArgs e)
        {
            Form deleteAppointment = new Delete_Appointment();
            deleteAppointment.Owner = this;
            deleteAppointment.Show();
            this.Hide();
        }
        public static Array displayCalendar(bool week)
        {
            MySqlConnection connection = new MySqlConnection(Database.connectionString);
            connection.Open();
            var query = $"SELECT customerId, type, start, end, appointmentId, userId FROM appointment WHERE userId = '{Database.getUserID()}'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            Dictionary<int, Hashtable> appointments = new Dictionary<int, Hashtable>();

            //create dictionary of appointments
            while (reader.Read())
            {
                Hashtable appointment = new Hashtable();
                appointment.Add("customerId", reader[0]);
                appointment.Add("type", reader[1]);
                appointment.Add("start", reader[2]);
                appointment.Add("end", reader[3]);
                appointment.Add("userId", reader[5]);
                appointments.Add(Convert.ToInt32(reader[4]), appointment);
            }
            reader.Close();

            //assigns username to appointment dictionary
            foreach (var appointment in appointments.Values)
            {
                query = $"SELECT userName FROM user WHERE userId = '{appointment["userId"]}'";
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                reader.Read();
                appointment.Add("userName", reader[0]);
                reader.Close();
            }

            //assigns customerName to appointment dictionary
            foreach (var appointment in appointments.Values)
            {
                query = $"SELECT customerName FROM customer WHERE customerId = '{appointment["customerId"]}'";
                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                reader.Read();
                appointment.Add("customerName", reader[0]);
                reader.Close();
            }
            Dictionary<int, Hashtable> parsedAppointments = new Dictionary<int, Hashtable>();
            foreach (var appointment in appointments)
            {
                DateTime start = DateTime.Parse(appointment.Value["start"].ToString());
                DateTime end = DateTime.Parse(appointment.Value["end"].ToString());
                DateTime today = DateTime.UtcNow;
                //week is checked this is our appointment list
                if (week)
                {
                    DateTime sunday = today.AddDays(-(int)today.DayOfWeek);
                    DateTime saturday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Saturday);
                    if (start >= sunday && end < saturday)
                    {
                        parsedAppointments.Add(appointment.Key, appointment.Value);
                    }
                }
                //if not we get month appointments
                else
                {
                    DateTime firstOfMonth = new DateTime(today.Year, today.Month, 1);
                    DateTime lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);
                    if (start >= firstOfMonth && end < lastOfMonth)
                    {
                        parsedAppointments.Add(appointment.Key, appointment.Value);
                    }
                }
            }

            //what we display to the user in the calendar view
            Database.setAppointments(appointments);
            var apptArray = from row in parsedAppointments select new
                            {
                                id = row.Key,
                                type = row.Value["type"],
                                start = Convert.ToDateTime(row.Value["start"].ToString()).ToLocalTime(),
                                end = Convert.ToDateTime(row.Value["end"].ToString()).ToLocalTime(),
                                customer = row.Value["customerName"]
                            };
            connection.Close();
            return apptArray.ToArray();
            
        }
        

        private void WeekRadio_CheckedChanged(object sender, EventArgs e)
        {
            updateCalendar();
        }

        

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            updateCalendar();
        }

        private void ApptByMonthButton_Click(object sender, EventArgs e)
        {
            Form appointmentReport = new Appointment_Report_by_Month();
            appointmentReport.Owner = this;
            appointmentReport.Show();
            this.Hide();
        }

        private void AppointmentsByConsultant_Click(object sender, EventArgs e)
        {
            Form appointConsultant = new Appointment_Report_by_Consultant();
            appointConsultant.Owner = this;
            appointConsultant.Show();
            this.Hide();
        }

        private void AppointmentsByCustomer_Click(object sender, EventArgs e)
        {
            Form appointCustomer = new Appointment_Report_by_Customer();
            appointCustomer.Owner = this;
            appointCustomer.Show();
            this.Hide();
        }

        private void Portal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Recording.logOut(Database.getUserName());
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            Form addUser = new Add_User();
            addUser.Owner = this;
            addUser.Show();
            this.Hide();
        }

        private void modifyUser_Click(object sender, EventArgs e)
        {
            Form modifyUser = new Modify_User();
            modifyUser.Owner = this;
            modifyUser.Show();
            this.Hide();
        }

        private void deleteUser_Click(object sender, EventArgs e)
        {
            Form deleteUser = new Delete_User();
            deleteUser.Owner = this;
            deleteUser.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form countAppointmentForm = new Count_Report_by_Type();
            countAppointmentForm.Owner = this;
            countAppointmentForm.Show();
            this.Hide();
        }
    }
}
