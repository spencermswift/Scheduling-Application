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
    public partial class Delete_User : Form
    {
        public static List<KeyValuePair<string, object>> UserList;
        public void setUserList(List<KeyValuePair<string,object>> list)
        {
            UserList = list;
        }
        public static List<KeyValuePair<string,object>> getUserList()
        {
            return UserList;
        }
        public Delete_User()
        {
            InitializeComponent();
            populateUserList();
            comboBoxDefaultSettings();
        }

        public void populateUserList()
        {
            MySqlConnection connection = new MySqlConnection(Database.getConnectionString());
            try
            {
                string query = "SELECT userId, concat(userName, ' --ID: ', userId) as Display FROM user;";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, connection);
                connection.Open();
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet, "User");
                deleteComboBox.DisplayMember = "Display";
                deleteComboBox.ValueMember = "userId";
                deleteComboBox.DataSource = dataSet.Tables["User"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured! " + ex);
            }
        }

        public void comboBoxDefaultSettings()
        {
            deleteComboBox.SelectedItem = null;
            deleteComboBox.Text = "--Select--";
            
        }


        private void deleteComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (deleteComboBox.ValueMember == Database.getUserID().ToString())
            {
                MessageBox.Show("You cannot delete the active user. Please logout and switch user to try again.");
                return;
            }
            else
            {
                DataRowView dataRowView = deleteComboBox.SelectedItem as DataRowView;
                int id = Convert.ToInt32(deleteComboBox.SelectedValue);
                var userList = Database.getUserList(id);
                setUserList(userList);
                if (deleteComboBox.SelectedIndex != -1)
                {
                    deleteButton.Enabled = true;
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int userId;
            bool parseOK = Int32.TryParse(deleteComboBox.SelectedValue.ToString(), out userId);
            if (userId == Database.getUserID())
            {
                MessageBox.Show("You cannot delete the active user. Please logout and switch user to try again.");
                return;
            }
            else
            {
                DialogResult confirmation = MessageBox.Show("Would you like to delete this user? This cannot be undone.", "", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    try
                    {
                        //delete appointment
                        var list = getUserList();

                        //lambda expression to convert list to dictionary
                        IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                        Database.deleteUser(dictionary["userId"].ToString());
                        MessageBox.Show("User successfully deleted.");
                        //refresh appointment list
                        populateUserList();
                        this.Owner.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
