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
    public partial class Modify_User : Form
    {
        public static List<KeyValuePair<string, object>> userList;
        public Modify_User()
        {
            InitializeComponent();
            populateUserList();
            comboBoxDefaultSettings();
            

        }

        public void setUserList(List<KeyValuePair<string, object>> list)
        {
            userList = list;
        }
        public static List<KeyValuePair<string,object>> getUserList()
        {
            return userList;
        }

        public void comboBoxDefaultSettings()
        {
            modifyComboBox.SelectedItem = null;
            modifyComboBox.Text = "--Select--";
            userName.Text = null;
            password.Text = null;
            password2.Text = null;
            yesRadio.Checked = false;
            noRadio.Checked = false;
            userName.Enabled = false;
            password.Enabled = false;
            password2.Enabled = false;
            yesRadio.Enabled = false;
            noRadio.Enabled = false;
            updateButton.Enabled = false;
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
                modifyComboBox.DisplayMember = "Display";
                modifyComboBox.ValueMember = "userId";
                modifyComboBox.DataSource = dataSet.Tables["User"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured! " + ex);
            }
        }
        
        

        private void ModifyComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRowView dataRowView = modifyComboBox.SelectedItem as DataRowView;
            int id = Convert.ToInt32(modifyComboBox.SelectedValue);
            var userList = Database.findUser(id);
            setUserList(userList);
            if (userList != null)
            {
                
                userName.Enabled = true;
                password.Enabled = true;
                password2.Enabled = true;
                yesRadio.Enabled = true;
                noRadio.Enabled = true;
                updateButton.Enabled = true;
                
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            bool pass = emptyCheck();

            if (pass == true)
            {
                DialogResult confirmation = MessageBox.Show("Are you sure you want to update this user?", "", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    if (password.Text == password2.Text)
                    {
                        try
                        {
                            var list = getUserList();
                            //lambda expression to convert list to dictionary
                            IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                            dictionary["userName"] = userName.Text;
                            dictionary["password"] = password.Text;
                            dictionary["active"] = yesRadio.Checked ? 1 : 0;
                            Database.updateUser(dictionary);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        finally
                        {
                            MessageBox.Show("Customer information updated");
                        }
                    }
                    else MessageBox.Show("Please ensure passwords match.");
                }
            }
            else
            {
                MessageBox.Show("Please enter information for each field.");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
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
 
    }
}
