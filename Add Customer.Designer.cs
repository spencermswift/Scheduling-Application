namespace Customer_Scheduling_Application
{
    partial class Add_Customer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.phoneText = new System.Windows.Forms.TextBox();
            this.addressText = new System.Windows.Forms.TextBox();
            this.cityText = new System.Windows.Forms.TextBox();
            this.zipText = new System.Windows.Forms.TextBox();
            this.countryText = new System.Windows.Forms.TextBox();
            this.yesRadio = new System.Windows.Forms.RadioButton();
            this.noRadio = new System.Windows.Forms.RadioButton();
            this.addCustomerButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Phone";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "City";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Zip Code";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Country";
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(84, 30);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(100, 20);
            this.nameText.TabIndex = 7;
            this.nameText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameText_KeyPress);
            // 
            // phoneText
            // 
            this.phoneText.Location = new System.Drawing.Point(84, 58);
            this.phoneText.Name = "phoneText";
            this.phoneText.Size = new System.Drawing.Size(100, 20);
            this.phoneText.TabIndex = 8;
            this.phoneText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PhoneText_KeyPress);
            // 
            // addressText
            // 
            this.addressText.Location = new System.Drawing.Point(84, 88);
            this.addressText.Name = "addressText";
            this.addressText.Size = new System.Drawing.Size(100, 20);
            this.addressText.TabIndex = 9;
            this.addressText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddressText_KeyPress);
            // 
            // cityText
            // 
            this.cityText.Location = new System.Drawing.Point(84, 114);
            this.cityText.Name = "cityText";
            this.cityText.Size = new System.Drawing.Size(100, 20);
            this.cityText.TabIndex = 10;
            this.cityText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CityText_KeyPress);
            // 
            // zipText
            // 
            this.zipText.Location = new System.Drawing.Point(84, 142);
            this.zipText.Name = "zipText";
            this.zipText.Size = new System.Drawing.Size(100, 20);
            this.zipText.TabIndex = 11;
            this.zipText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZipText_KeyPress);
            // 
            // countryText
            // 
            this.countryText.Location = new System.Drawing.Point(84, 172);
            this.countryText.Name = "countryText";
            this.countryText.Size = new System.Drawing.Size(100, 20);
            this.countryText.TabIndex = 12;
            this.countryText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CountryText_KeyPress);
            // 
            // yesRadio
            // 
            this.yesRadio.AutoSize = true;
            this.yesRadio.Location = new System.Drawing.Point(81, 202);
            this.yesRadio.Name = "yesRadio";
            this.yesRadio.Size = new System.Drawing.Size(45, 19);
            this.yesRadio.TabIndex = 13;
            this.yesRadio.TabStop = true;
            this.yesRadio.Text = "Yes";
            this.yesRadio.UseVisualStyleBackColor = true;
            // 
            // noRadio
            // 
            this.noRadio.AutoSize = true;
            this.noRadio.Location = new System.Drawing.Point(132, 202);
            this.noRadio.Name = "noRadio";
            this.noRadio.Size = new System.Drawing.Size(41, 19);
            this.noRadio.TabIndex = 14;
            this.noRadio.TabStop = true;
            this.noRadio.Text = "No";
            this.noRadio.UseVisualStyleBackColor = true;
            // 
            // addCustomerButton
            // 
            this.addCustomerButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.addCustomerButton.Location = new System.Drawing.Point(23, 225);
            this.addCustomerButton.Name = "addCustomerButton";
            this.addCustomerButton.Size = new System.Drawing.Size(75, 23);
            this.addCustomerButton.TabIndex = 15;
            this.addCustomerButton.Text = "Create";
            this.addCustomerButton.UseVisualStyleBackColor = false;
            this.addCustomerButton.Click += new System.EventHandler(this.AddCustomerButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cancelButton.Location = new System.Drawing.Point(109, 225);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(234, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Please complete all customer information";
            // 
            // Add_Customer
            // 
            this.AcceptButton = this.addCustomerButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 260);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addCustomerButton);
            this.Controls.Add(this.noRadio);
            this.Controls.Add(this.yesRadio);
            this.Controls.Add(this.countryText);
            this.Controls.Add(this.zipText);
            this.Controls.Add(this.cityText);
            this.Controls.Add(this.addressText);
            this.Controls.Add(this.phoneText);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Add_Customer";
            this.Text = "Add Customer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.TextBox phoneText;
        private System.Windows.Forms.TextBox addressText;
        private System.Windows.Forms.TextBox cityText;
        private System.Windows.Forms.TextBox zipText;
        private System.Windows.Forms.TextBox countryText;
        private System.Windows.Forms.RadioButton yesRadio;
        private System.Windows.Forms.RadioButton noRadio;
        private System.Windows.Forms.Button addCustomerButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label8;
    }
}