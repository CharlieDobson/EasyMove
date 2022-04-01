namespace EasyMove
{
    partial class EasyMoveMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyMoveMainForm));
            this.ADGroupCombo1 = new System.Windows.Forms.ComboBox();
            this.ADGroupCombo2 = new System.Windows.Forms.ComboBox();
            this.ADGroupListbox1 = new System.Windows.Forms.ListBox();
            this.ADGroupListbox2 = new System.Windows.Forms.ListBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DomainListBox = new System.Windows.Forms.ComboBox();
            this.MoveToGroup2Button = new System.Windows.Forms.Button();
            this.MoveToGroup1Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ADGroupCombo1
            // 
            this.ADGroupCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ADGroupCombo1.FormattingEnabled = true;
            this.ADGroupCombo1.Location = new System.Drawing.Point(27, 71);
            this.ADGroupCombo1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ADGroupCombo1.Name = "ADGroupCombo1";
            this.ADGroupCombo1.Size = new System.Drawing.Size(394, 32);
            this.ADGroupCombo1.TabIndex = 1;
            this.ADGroupCombo1.SelectedIndexChanged += new System.EventHandler(this.ADGroupCombo1_SelectedIndexChanged);
            // 
            // ADGroupCombo2
            // 
            this.ADGroupCombo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ADGroupCombo2.FormattingEnabled = true;
            this.ADGroupCombo2.Location = new System.Drawing.Point(567, 71);
            this.ADGroupCombo2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ADGroupCombo2.Name = "ADGroupCombo2";
            this.ADGroupCombo2.Size = new System.Drawing.Size(394, 32);
            this.ADGroupCombo2.TabIndex = 2;
            this.ADGroupCombo2.SelectedIndexChanged += new System.EventHandler(this.ADGroupCombo2_SelectedIndexChanged);
            // 
            // ADGroupListbox1
            // 
            this.ADGroupListbox1.FormattingEnabled = true;
            this.ADGroupListbox1.ItemHeight = 24;
            this.ADGroupListbox1.Location = new System.Drawing.Point(27, 122);
            this.ADGroupListbox1.Margin = new System.Windows.Forms.Padding(2);
            this.ADGroupListbox1.Name = "ADGroupListbox1";
            this.ADGroupListbox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ADGroupListbox1.Size = new System.Drawing.Size(394, 364);
            this.ADGroupListbox1.TabIndex = 3;
            // 
            // ADGroupListbox2
            // 
            this.ADGroupListbox2.FormattingEnabled = true;
            this.ADGroupListbox2.ItemHeight = 24;
            this.ADGroupListbox2.Location = new System.Drawing.Point(567, 122);
            this.ADGroupListbox2.Margin = new System.Windows.Forms.Padding(2);
            this.ADGroupListbox2.Name = "ADGroupListbox2";
            this.ADGroupListbox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ADGroupListbox2.Size = new System.Drawing.Size(394, 364);
            this.ADGroupListbox2.TabIndex = 6;
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(620, 515);
            this.OKBtn.Margin = new System.Windows.Forms.Padding(2);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(97, 33);
            this.OKBtn.TabIndex = 7;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(744, 515);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(97, 33);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Enabled = false;
            this.ApplyBtn.Location = new System.Drawing.Point(864, 515);
            this.ApplyBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(97, 33);
            this.ApplyBtn.TabIndex = 9;
            this.ApplyBtn.Text = "Apply";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Domain:";
            // 
            // DomainListBox
            // 
            this.DomainListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DomainListBox.FormattingEnabled = true;
            this.DomainListBox.Location = new System.Drawing.Point(117, 19);
            this.DomainListBox.Name = "DomainListBox";
            this.DomainListBox.Size = new System.Drawing.Size(304, 32);
            this.DomainListBox.TabIndex = 0;
            this.DomainListBox.SelectedIndexChanged += new System.EventHandler(this.DomainListBox_SelectedIndexChanged);
            // 
            // MoveToGroup2Button
            // 
            this.MoveToGroup2Button.Image = global::EasyMove.Properties.Resources.right_arrow_35px_16px;
            this.MoveToGroup2Button.Location = new System.Drawing.Point(472, 221);
            this.MoveToGroup2Button.Margin = new System.Windows.Forms.Padding(0);
            this.MoveToGroup2Button.Name = "MoveToGroup2Button";
            this.MoveToGroup2Button.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.MoveToGroup2Button.Size = new System.Drawing.Size(46, 33);
            this.MoveToGroup2Button.TabIndex = 4;
            this.MoveToGroup2Button.UseVisualStyleBackColor = true;
            this.MoveToGroup2Button.Click += new System.EventHandler(this.MoveToGroup2Button_Click);
            // 
            // MoveToGroup1Button
            // 
            this.MoveToGroup1Button.Image = global::EasyMove.Properties.Resources.left_arrow_35px_16px;
            this.MoveToGroup1Button.Location = new System.Drawing.Point(472, 335);
            this.MoveToGroup1Button.Margin = new System.Windows.Forms.Padding(0);
            this.MoveToGroup1Button.Name = "MoveToGroup1Button";
            this.MoveToGroup1Button.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.MoveToGroup1Button.Size = new System.Drawing.Size(46, 33);
            this.MoveToGroup1Button.TabIndex = 5;
            this.MoveToGroup1Button.UseVisualStyleBackColor = true;
            this.MoveToGroup1Button.Click += new System.EventHandler(this.MoveToGroup1Button_Click);
            // 
            // EasyMoveMainForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 564);
            this.Controls.Add(this.DomainListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.MoveToGroup2Button);
            this.Controls.Add(this.MoveToGroup1Button);
            this.Controls.Add(this.ADGroupListbox2);
            this.Controls.Add(this.ADGroupListbox1);
            this.Controls.Add(this.ADGroupCombo2);
            this.Controls.Add(this.ADGroupCombo1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "EasyMoveMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LDAP Easy Move";
            this.Load += new System.EventHandler(this.EasyMoveMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ADGroupCombo1;
        private System.Windows.Forms.ComboBox ADGroupCombo2;
        private System.Windows.Forms.ListBox ADGroupListbox1;
        private System.Windows.Forms.ListBox ADGroupListbox2;
        private System.Windows.Forms.Button MoveToGroup1Button;
        private System.Windows.Forms.Button MoveToGroup2Button;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DomainListBox;
    }
}

