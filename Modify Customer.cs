using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Customer_Scheduling_Application
{
    public partial class Modify_Customer : Form
    {
        public static List<KeyValuePair<string, object>> CustList;
        public Modify_Customer()
        {
            InitializeComponent();
            populateCustomerList();
            comboBoxDefaultSettings();
        }

        public void setCustList(List<KeyValuePair<string, object>> list)
        {
            CustList = list;
        }
        public static List<KeyValuePair<string, object>> getCustList()
        {
            return CustList;
        }

        //sets form to be locked and provides a default value for the comboBox
        public void comboBoxDefaultSettings()
        {
            modifyComboBox.SelectedItem = null;
            modifyComboBox.Text = "--Select--";
            nameText.Text = null;
            phoneText.Text = null;
            addressText.Text = null;
            cityText.Text = null;
            zipText.Text = null;
            countryText.Text = null;
            yesRadio.Checked = false;
            noRadio.Checked = false;
            nameText.Enabled = false;
            phoneText.Enabled = false;
            addressText.Enabled = false;
            cityText.Enabled = false;
            zipText.Enabled = false;
            countryText.Enabled = false;
            yesRadio.Enabled = false;
            noRadio.Enabled = false;
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
                modifyComboBox.DisplayMember = "Display";
                modifyComboBox.ValueMember = "customerId";
                modifyComboBox.DataSource = dataSet.Tables["Cust"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured! " + ex);
            }
        }
        public void populateFields(List<KeyValuePair<string, object>> custList)
        {
            //lambda expressions to retrieve values from custList
            nameText.Text = custList.First(kvp => kvp.Key == "customerName").Value.ToString();
            phoneText.Text = custList.First(kvp => kvp.Key == "phone").Value.ToString();
            addressText.Text = custList.First(kvp => kvp.Key == "address").Value.ToString();
            cityText.Text = custList.First(kvp => kvp.Key == "city").Value.ToString();
            zipText.Text = custList.First(kvp => kvp.Key == "postalCode").Value.ToString();
            countryText.Text = custList.First(kvp => kvp.Key == "country").Value.ToString();
            if (Convert.ToInt32(custList.First(kvp => kvp.Key == "active").Value) == 1)
            {
                yesRadio.Checked = true;
            }
            else
            {
                noRadio.Checked = true;
            }
        }
        //populates currently selected customer information into text boxes
        private void ModifyComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRowView dataRowView = modifyComboBox.SelectedItem as DataRowView;
            int id = Convert.ToInt32(modifyComboBox.SelectedValue);
            var customerList = Database.findCustomer(id);
            setCustList(customerList);
            if (customerList != null)
            {
                populateFields(customerList);
                nameText.Enabled = true;
                phoneText.Enabled = true;
                addressText.Enabled = true;
                cityText.Enabled = true;
                zipText.Enabled = true;
                countryText.Enabled = true;
                yesRadio.Enabled = true;
                noRadio.Enabled = true;
                updateButton.Enabled = true;
            }
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
                DialogResult confirmation = MessageBox.Show("Are you sure you want to update this customer?", "", MessageBoxButtons.YesNo);
                if (confirmation == DialogResult.Yes)
                {
                    try
                    {
                        var list = getCustList();
                        //lambda expression to convert list to dictionary
                        IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                        dictionary["customerName"] = nameText.Text;
                        dictionary["phone"] = phoneText.Text;
                        dictionary["address"] = addressText.Text;
                        dictionary["city"] = cityText.Text;
                        dictionary["postalCode"] = zipText.Text;
                        dictionary["country"] = countryText.Text;
                        dictionary["active"] = yesRadio.Checked ? 1 : 0;
                        Database.updateCustomer(dictionary);
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
            }
            else
            {
                MessageBox.Show("Please enter information for each field.");
            }
        }

        //prevents user from entering invalid characters into text boxes.
        private void NameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PhoneText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddressText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CityText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ZipText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CountryText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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
    }
}
