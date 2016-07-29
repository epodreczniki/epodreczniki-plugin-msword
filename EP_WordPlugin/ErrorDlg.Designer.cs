namespace EP_WordPlugin
{
    partial class ErrorDlg
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
      this.textMessage = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.textDetails = new System.Windows.Forms.TextBox();
      this.btClose = new System.Windows.Forms.Button();
      this.bnSendMail = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Komunikat";
      // 
      // textMessage
      // 
      this.textMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textMessage.Location = new System.Drawing.Point(13, 30);
      this.textMessage.Multiline = true;
      this.textMessage.Name = "textMessage";
      this.textMessage.ReadOnly = true;
      this.textMessage.Size = new System.Drawing.Size(621, 70);
      this.textMessage.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(16, 107);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Szczegóły";
      // 
      // textDetails
      // 
      this.textDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textDetails.Location = new System.Drawing.Point(13, 124);
      this.textDetails.Multiline = true;
      this.textDetails.Name = "textDetails";
      this.textDetails.ReadOnly = true;
      this.textDetails.Size = new System.Drawing.Size(621, 400);
      this.textDetails.TabIndex = 3;
      // 
      // btClose
      // 
      this.btClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btClose.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btClose.Location = new System.Drawing.Point(378, 530);
      this.btClose.Name = "btClose";
      this.btClose.Size = new System.Drawing.Size(75, 23);
      this.btClose.TabIndex = 4;
      this.btClose.Text = "Zamknij";
      this.btClose.UseVisualStyleBackColor = true;
      // 
      // bnSendMail
      // 
      this.bnSendMail.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.bnSendMail.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.bnSendMail.Location = new System.Drawing.Point(173, 530);
      this.bnSendMail.Name = "bnSendMail";
      this.bnSendMail.Size = new System.Drawing.Size(75, 23);
      this.bnSendMail.TabIndex = 5;
      this.bnSendMail.Text = "Wyślij błąd";
      this.bnSendMail.UseVisualStyleBackColor = true;
      this.bnSendMail.Click += new System.EventHandler(this.bnSendMail_Click);
      // 
      // ErrorDlg
      // 
      this.AcceptButton = this.btClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(646, 559);
      this.ControlBox = false;
      this.Controls.Add(this.bnSendMail);
      this.Controls.Add(this.btClose);
      this.Controls.Add(this.textDetails);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textMessage);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ErrorDlg";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "EP_WordPlugin : Wystąpił nieoczekiwany błąd";
      this.Load += new System.EventHandler(this.ErrorDlg_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDetails;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button bnSendMail;
    }
}