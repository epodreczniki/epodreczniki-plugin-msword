namespace EP_WordPlugin
{
  partial class ConverterSingleFileForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverterSingleFileForm));
      this.lvStatus = new System.Windows.Forms.ListView();
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.laStatus = new System.Windows.Forms.Label();
      this.bnClose = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lvStatus
      // 
      this.lvStatus.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
      this.lvStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
      this.lvStatus.FullRowSelect = true;
      this.lvStatus.Location = new System.Drawing.Point(11, 34);
      this.lvStatus.Name = "lvStatus";
      this.lvStatus.Size = new System.Drawing.Size(1128, 121);
      this.lvStatus.TabIndex = 13;
      this.lvStatus.UseCompatibleStateImageBehavior = false;
      this.lvStatus.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Komunikat";
      this.columnHeader5.Width = 1090;
      // 
      // laStatus
      // 
      this.laStatus.AutoSize = true;
      this.laStatus.Location = new System.Drawing.Point(11, 12);
      this.laStatus.Name = "laStatus";
      this.laStatus.Size = new System.Drawing.Size(105, 13);
      this.laStatus.TabIndex = 12;
      this.laStatus.Text = "Status przetwarzania";
      // 
      // bnClose
      // 
      this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.bnClose.Location = new System.Drawing.Point(1057, 166);
      this.bnClose.Name = "bnClose";
      this.bnClose.Size = new System.Drawing.Size(83, 23);
      this.bnClose.TabIndex = 11;
      this.bnClose.Text = "Zamknij";
      this.bnClose.UseVisualStyleBackColor = true;
      this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
      // 
      // ConverterSingleFileForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1153, 199);
      this.Controls.Add(this.lvStatus);
      this.Controls.Add(this.laStatus);
      this.Controls.Add(this.bnClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConverterSingleFileForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Konwersja pojedynczego pliku...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosingForm);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvStatus;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.Label laStatus;
    private System.Windows.Forms.Button bnClose;
  }
}