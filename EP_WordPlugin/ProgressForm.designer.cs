namespace EP_WordPlugin
{
  partial class ProgressForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
      this.progressDownloadBar = new System.Windows.Forms.ProgressBar();
      this.bnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // progressDownloadBar
      // 
      this.progressDownloadBar.Location = new System.Drawing.Point(12, 25);
      this.progressDownloadBar.Name = "progressDownloadBar";
      this.progressDownloadBar.Size = new System.Drawing.Size(480, 22);
      this.progressDownloadBar.TabIndex = 0;
      // 
      // bnCancel
      // 
      this.bnCancel.Location = new System.Drawing.Point(223, 60);
      this.bnCancel.Name = "bnCancel";
      this.bnCancel.Size = new System.Drawing.Size(75, 23);
      this.bnCancel.TabIndex = 1;
      this.bnCancel.Text = "Anuluj";
      this.bnCancel.UseVisualStyleBackColor = true;
      this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(166, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Ściąganie konwertera z Internetu ";
      // 
      // ProgressForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(519, 95);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.bnCancel);
      this.Controls.Add(this.progressDownloadBar);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ProgressForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Postęp ściągania pliku";
      this.Load += new System.EventHandler(this.ProgressForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.ProgressBar progressDownloadBar;
    private System.Windows.Forms.Button bnCancel;
    private System.Windows.Forms.Label label1;

  }
}