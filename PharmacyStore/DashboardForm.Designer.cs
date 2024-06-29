namespace PharmacyStore
{
    partial class DashboardForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logOut_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sales_button = new System.Windows.Forms.Button();
            this.order_button = new System.Windows.Forms.Button();
            this.report_button = new System.Windows.Forms.Button();
            this.addItem_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.employe_button = new System.Windows.Forms.Button();
            this.stock_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.logOut_Button);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(833, 83);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(131, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Pharmacitical Limited";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(386, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "--/--/--  --:--:--";
            // 
            // logOut_Button
            // 
            this.logOut_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logOut_Button.FlatAppearance.BorderSize = 0;
            this.logOut_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MidnightBlue;
            this.logOut_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.logOut_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logOut_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOut_Button.ForeColor = System.Drawing.SystemColors.Control;
            this.logOut_Button.Location = new System.Drawing.Point(667, 21);
            this.logOut_Button.Name = "logOut_Button";
            this.logOut_Button.Size = new System.Drawing.Size(137, 47);
            this.logOut_Button.TabIndex = 4;
            this.logOut_Button.Text = "LOG OUT";
            this.logOut_Button.UseVisualStyleBackColor = true;
            this.logOut_Button.Click += new System.EventHandler(this.onLog_out);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::PharmacyStore.Properties.Resources.images_4;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 77);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Elephant", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(127, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "InDrugs";
            // 
            // sales_button
            // 
            this.sales_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sales_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sales_button.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.sales_button.FlatAppearance.BorderSize = 0;
            this.sales_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sales_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sales_button.Location = new System.Drawing.Point(88, 46);
            this.sales_button.MaximumSize = new System.Drawing.Size(90, 90);
            this.sales_button.Name = "sales_button";
            this.sales_button.Size = new System.Drawing.Size(90, 90);
            this.sales_button.TabIndex = 1;
            this.sales_button.Text = "TODAY\'s SALES";
            this.sales_button.UseVisualStyleBackColor = false;
            this.sales_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // order_button
            // 
            this.order_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.order_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.order_button.FlatAppearance.BorderSize = 0;
            this.order_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order_button.Location = new System.Drawing.Point(202, 46);
            this.order_button.MaximumSize = new System.Drawing.Size(90, 90);
            this.order_button.Name = "order_button";
            this.order_button.Size = new System.Drawing.Size(90, 90);
            this.order_button.TabIndex = 2;
            this.order_button.Text = "ORDER";
            this.order_button.UseCompatibleTextRendering = true;
            this.order_button.UseVisualStyleBackColor = false;
            this.order_button.Click += new System.EventHandler(this.order_button_Click);
            // 
            // report_button
            // 
            this.report_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.report_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.report_button.FlatAppearance.BorderSize = 0;
            this.report_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.report_button.Location = new System.Drawing.Point(88, 160);
            this.report_button.MaximumSize = new System.Drawing.Size(90, 90);
            this.report_button.Name = "report_button";
            this.report_button.Size = new System.Drawing.Size(90, 90);
            this.report_button.TabIndex = 3;
            this.report_button.Text = "REPORT";
            this.report_button.UseVisualStyleBackColor = false;
            // 
            // addItem_button
            // 
            this.addItem_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addItem_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.addItem_button.FlatAppearance.BorderSize = 0;
            this.addItem_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItem_button.Location = new System.Drawing.Point(202, 160);
            this.addItem_button.MaximumSize = new System.Drawing.Size(90, 90);
            this.addItem_button.Name = "addItem_button";
            this.addItem_button.Size = new System.Drawing.Size(90, 90);
            this.addItem_button.TabIndex = 7;
            this.addItem_button.Text = "ANALYTICS";
            this.addItem_button.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.employe_button);
            this.panel2.Controls.Add(this.stock_button);
            this.panel2.Controls.Add(this.sales_button);
            this.panel2.Controls.Add(this.addItem_button);
            this.panel2.Controls.Add(this.order_button);
            this.panel2.Controls.Add(this.report_button);
            this.panel2.Location = new System.Drawing.Point(186, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(488, 363);
            this.panel2.TabIndex = 5;
            // 
            // employe_button
            // 
            this.employe_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.employe_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.employe_button.FlatAppearance.BorderSize = 0;
            this.employe_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employe_button.Location = new System.Drawing.Point(315, 160);
            this.employe_button.MaximumSize = new System.Drawing.Size(90, 90);
            this.employe_button.Name = "employe_button";
            this.employe_button.Size = new System.Drawing.Size(90, 90);
            this.employe_button.TabIndex = 6;
            this.employe_button.Text = "STAFF";
            this.employe_button.UseVisualStyleBackColor = false;
            // 
            // stock_button
            // 
            this.stock_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stock_button.BackColor = System.Drawing.Color.CornflowerBlue;
            this.stock_button.FlatAppearance.BorderSize = 0;
            this.stock_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock_button.Location = new System.Drawing.Point(315, 46);
            this.stock_button.MaximumSize = new System.Drawing.Size(90, 90);
            this.stock_button.Name = "stock_button";
            this.stock_button.Size = new System.Drawing.Size(90, 90);
            this.stock_button.TabIndex = 5;
            this.stock_button.Text = "STOCK";
            this.stock_button.UseVisualStyleBackColor = false;
            this.stock_button.Click += new System.EventHandler(this.stock_button_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimer_Tick);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::PharmacyStore.Properties.Resources.pexels_pixelcop_3970396;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(833, 455);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Name] Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sales_button;
        private System.Windows.Forms.Button order_button;
        private System.Windows.Forms.Button report_button;
        private System.Windows.Forms.Button addItem_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button stock_button;
        private System.Windows.Forms.Button employe_button;
        private System.Windows.Forms.Button logOut_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
    }
}