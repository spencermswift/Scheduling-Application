using Google.Protobuf.WellKnownTypes;
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
    public partial class Add_User : Form
    {
        public Add_User()
        {
            InitializeComponent();
        }

        

        private void addUser_Click(object sender, EventArgs e)
        {

            bool pass = emptyCheck();
            if (pass == true)
            {
                if (password.Text == password2.Text)
                {
                    Database.createUser(Database.getID("user", "userId") + 1, userNameText.Text, password.Text, yesRadio.Checked ? 1 : 0, Database.getTime(), Database.getUserName());
                    MessageBox.Show("Customer successfully created!");
                    this.Owner.Show();
                    this.Close();
                }
                else MessageBox.Show("Please ensure passwords match.");
            }
            else MessageBox.Show("Please enter information for all fields");
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void userNameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
