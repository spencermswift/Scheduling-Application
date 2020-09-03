using System;
using System.Windows.Forms;

namespace Customer_Scheduling_Application
{
    public partial class Add_Customer : Form
    {

        public Add_Customer()
        {
            InitializeComponent();
        }

        private void NameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
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
        private void PhoneText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            bool pass = emptyCheck();
            if (pass == true)
            {
                int country = Database.createCountry(countryText.Text);
                int city = Database.createCity(country, cityText.Text);
                int address = Database.createAddress(city, addressText.Text, zipText.Text, phoneText.Text);
                Database.createCustomer(Database.getID("customer", "customerId") + 1, nameText.Text, address, yesRadio.Checked ? 1 : 0, Database.getTime(), Database.getUserName());
                MessageBox.Show("Customer successfully created!");
                this.Owner.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter information for all fields.");
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
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
