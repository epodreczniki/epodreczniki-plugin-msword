namespace EP_WordPlugin
{
  partial class LoginDirectoryRetrieverForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDirectoryRetrieverForm));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tbUserName = new System.Windows.Forms.TextBox();
      this.tbUserPassword = new System.Windows.Forms.TextBox();
      this.bnOK = new System.Windows.Forms.Button();
      this.bnCancel = new System.Windows.Forms.Button();
      this.chSaveUserAndPass = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(62, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Użytkownik";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(36, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Hasło";
      // 
      // tbUserName
      // 
      this.tbUserName.Location = new System.Drawing.Point(107, 6);
      this.tbUserName.Name = "tbUserName";
      this.tbUserName.Size = new System.Drawing.Size(211, 20);
      this.tbUserName.TabIndex = 2;
      // 
      // tbUserPassword
      // 
      this.tbUserPassword.Location = new System.Drawing.Point(107, 34);
      this.tbUserPassword.Name = "tbUserPassword";
      this.tbUserPassword.PasswordChar = '*';
      this.tbUserPassword.Size = new System.Drawing.Size(211, 20);
      this.tbUserPassword.TabIndex = 3;
      // 
      // bnOK
      // 
      this.bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.bnOK.Location = new System.Drawing.Point(162, 92);
      this.bnOK.Name = "bnOK";
      this.bnOK.Size = new System.Drawing.Size(75, 23);
      this.bnOK.TabIndex = 4;
      this.bnOK.Text = "OK";
      this.bnOK.UseVisualStyleBackColor = true;
      this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
      // 
      // bnCancel
      // 
      this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.bnCancel.Location = new System.Drawing.Point(243, 92);
      this.bnCancel.Name = "bnCancel";
      this.bnCancel.Size = new System.Drawing.Size(75, 23);
      this.bnCancel.TabIndex = 5;
      this.bnCancel.Text = "Anuluj";
      this.bnCancel.UseVisualStyleBackColor = true;
      // 
      // chSaveUserAndPass
      // 
      this.chSaveUserAndPass.AutoSize = true;
      this.chSaveUserAndPass.Checked = true;
      this.chSaveUserAndPass.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chSaveUserAndPass.Location = new System.Drawing.Point(15, 69);
      this.chSaveUserAndPass.Name = "chSaveUserAndPass";
      this.chSaveUserAndPass.Size = new System.Drawing.Size(175, 17);
      this.chSaveUserAndPass.TabIndex = 6;
      this.chSaveUserAndPass.Text = "Zapamiętaj użytkownika i hasło";
      this.chSaveUserAndPass.UseVisualStyleBackColor = true;
      // 
      // LoginDirectoryRetrieverForm
      // 
      this.AcceptButton = this.bnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.bnCancel;
      this.ClientSize = new System.Drawing.Size(330, 127);
      this.Controls.Add(this.chSaveUserAndPass);
      this.Controls.Add(this.bnCancel);
      this.Controls.Add(this.bnOK);
      this.Controls.Add(this.tbUserPassword);
      this.Controls.Add(this.tbUserName);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "LoginDirectoryRetrieverForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Logowanie...";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbUserName;
    private System.Windows.Forms.TextBox tbUserPassword;
    private System.Windows.Forms.Button bnOK;
    private System.Windows.Forms.Button bnCancel;
    private System.Windows.Forms.CheckBox chSaveUserAndPass;
  }
}