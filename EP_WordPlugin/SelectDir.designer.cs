namespace EP_WordPlugin
{
  partial class SelectDirForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDirForm));
      this.tvDirs = new System.Windows.Forms.TreeView();
      this.bnOK = new System.Windows.Forms.Button();
      this.bnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // tvDirs
      // 
      this.tvDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tvDirs.HideSelection = false;
      this.tvDirs.Location = new System.Drawing.Point(12, 12);
      this.tvDirs.Name = "tvDirs";
      this.tvDirs.Size = new System.Drawing.Size(558, 381);
      this.tvDirs.TabIndex = 0;
      // 
      // bnOK
      // 
      this.bnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.bnOK.Location = new System.Drawing.Point(404, 399);
      this.bnOK.Name = "bnOK";
      this.bnOK.Size = new System.Drawing.Size(75, 23);
      this.bnOK.TabIndex = 1;
      this.bnOK.Text = "OK";
      this.bnOK.UseVisualStyleBackColor = true;
      this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
      // 
      // bnCancel
      // 
      this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.bnCancel.Location = new System.Drawing.Point(495, 399);
      this.bnCancel.Name = "bnCancel";
      this.bnCancel.Size = new System.Drawing.Size(75, 23);
      this.bnCancel.TabIndex = 2;
      this.bnCancel.Text = "Anuluj";
      this.bnCancel.UseVisualStyleBackColor = true;
      // 
      // SelectDirForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(585, 430);
      this.Controls.Add(this.bnCancel);
      this.Controls.Add(this.bnOK);
      this.Controls.Add(this.tvDirs);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SelectDirForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Zaznacz Katalog";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView tvDirs;
    private System.Windows.Forms.Button bnOK;
    private System.Windows.Forms.Button bnCancel;
  }
}