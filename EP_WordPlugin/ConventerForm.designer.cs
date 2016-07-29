namespace EP_WordPlugin
{
  partial class ConventerForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConventerForm));
      this.label1 = new System.Windows.Forms.Label();
      this.tbDocDir = new System.Windows.Forms.TextBox();
      this.bnSelectFolder = new System.Windows.Forms.Button();
      this.bnRunConversion = new System.Windows.Forms.Button();
      this.bnStopConversion = new System.Windows.Forms.Button();
      this.bnImporting = new System.Windows.Forms.Button();
      this.bnClose = new System.Windows.Forms.Button();
      this.laStatus = new System.Windows.Forms.Label();
      this.lvEPK = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label2 = new System.Windows.Forms.Label();
      this.lvStatus = new System.Windows.Forms.ListView();
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.label3 = new System.Windows.Forms.Label();
      this.llOutput = new System.Windows.Forms.LinkLabel();
      this.linkDirLabel = new System.Windows.Forms.LinkLabel();
      this.label4 = new System.Windows.Forms.Label();
      this.chGeneratePDF = new System.Windows.Forms.CheckBox();
      this.contextMenuStripLink = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.kopiujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStripLink.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(110, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Folder z dokumentami";
      // 
      // tbDocDir
      // 
      this.tbDocDir.Location = new System.Drawing.Point(8, 31);
      this.tbDocDir.Name = "tbDocDir";
      this.tbDocDir.Size = new System.Drawing.Size(1039, 20);
      this.tbDocDir.TabIndex = 1;
      // 
      // bnSelectFolder
      // 
      this.bnSelectFolder.Location = new System.Drawing.Point(1053, 29);
      this.bnSelectFolder.Name = "bnSelectFolder";
      this.bnSelectFolder.Size = new System.Drawing.Size(83, 23);
      this.bnSelectFolder.TabIndex = 2;
      this.bnSelectFolder.Text = "Wskaż folder";
      this.bnSelectFolder.UseVisualStyleBackColor = true;
      this.bnSelectFolder.Click += new System.EventHandler(this.bnSelectFolder_Click);
      // 
      // bnRunConversion
      // 
      this.bnRunConversion.Location = new System.Drawing.Point(144, 65);
      this.bnRunConversion.Name = "bnRunConversion";
      this.bnRunConversion.Size = new System.Drawing.Size(127, 23);
      this.bnRunConversion.TabIndex = 3;
      this.bnRunConversion.Text = "Sama konwersja";
      this.bnRunConversion.UseVisualStyleBackColor = true;
      this.bnRunConversion.Click += new System.EventHandler(this.bnRunConversion_Click);
      // 
      // bnStopConversion
      // 
      this.bnStopConversion.Location = new System.Drawing.Point(1015, 65);
      this.bnStopConversion.Name = "bnStopConversion";
      this.bnStopConversion.Size = new System.Drawing.Size(121, 23);
      this.bnStopConversion.TabIndex = 4;
      this.bnStopConversion.Text = "Zatrzymaj";
      this.bnStopConversion.UseVisualStyleBackColor = true;
      this.bnStopConversion.Click += new System.EventHandler(this.bnStopConversion_Click);
      // 
      // bnImporting
      // 
      this.bnImporting.Location = new System.Drawing.Point(8, 65);
      this.bnImporting.Name = "bnImporting";
      this.bnImporting.Size = new System.Drawing.Size(121, 23);
      this.bnImporting.TabIndex = 5;
      this.bnImporting.Text = "Konwertuj && Importuj";
      this.bnImporting.UseVisualStyleBackColor = true;
      this.bnImporting.Click += new System.EventHandler(this.bnImporting_Click);
      // 
      // bnClose
      // 
      this.bnClose.Location = new System.Drawing.Point(1054, 484);
      this.bnClose.Name = "bnClose";
      this.bnClose.Size = new System.Drawing.Size(83, 23);
      this.bnClose.TabIndex = 6;
      this.bnClose.Text = "Zamknij";
      this.bnClose.UseVisualStyleBackColor = true;
      this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
      // 
      // laStatus
      // 
      this.laStatus.AutoSize = true;
      this.laStatus.Location = new System.Drawing.Point(8, 330);
      this.laStatus.Name = "laStatus";
      this.laStatus.Size = new System.Drawing.Size(105, 13);
      this.laStatus.TabIndex = 7;
      this.laStatus.Text = "Status przetwarzania";
      // 
      // lvEPK
      // 
      this.lvEPK.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.lvEPK.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.lvEPK.FullRowSelect = true;
      this.lvEPK.GridLines = true;
      this.lvEPK.Location = new System.Drawing.Point(8, 114);
      this.lvEPK.Name = "lvEPK";
      this.lvEPK.Size = new System.Drawing.Size(1128, 209);
      this.lvEPK.TabIndex = 8;
      this.lvEPK.UseCompatibleStateImageBehavior = false;
      this.lvEPK.View = System.Windows.Forms.View.Details;
      this.lvEPK.Click += new System.EventHandler(this.lv_Click);
      this.lvEPK.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Nazwa pliku";
      this.columnHeader1.Width = 192;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Katalog";
      this.columnHeader2.Width = 409;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Status";
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Wiadomość";
      this.columnHeader4.Width = 438;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 95);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(90, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Pliki dokumentów";
      // 
      // lvStatus
      // 
      this.lvStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
      this.lvStatus.FullRowSelect = true;
      this.lvStatus.Location = new System.Drawing.Point(8, 352);
      this.lvStatus.Name = "lvStatus";
      this.lvStatus.Size = new System.Drawing.Size(1128, 121);
      this.lvStatus.TabIndex = 10;
      this.lvStatus.UseCompatibleStateImageBehavior = false;
      this.lvStatus.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Komunikat";
      this.columnHeader5.Width = 1090;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(8, 481);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(80, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "Wersja bieżąca";
      // 
      // llOutput
      // 
      this.llOutput.AutoSize = true;
      this.llOutput.ContextMenuStrip = this.contextMenuStripLink;
      this.llOutput.Location = new System.Drawing.Point(189, 481);
      this.llOutput.Name = "llOutput";
      this.llOutput.Size = new System.Drawing.Size(0, 13);
      this.llOutput.TabIndex = 12;
      this.llOutput.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOutput_LinkClicked);
      // 
      // linkDirLabel
      // 
      this.linkDirLabel.AutoSize = true;
      this.linkDirLabel.ContextMenuStrip = this.contextMenuStripLink;
      this.linkDirLabel.Location = new System.Drawing.Point(190, 508);
      this.linkDirLabel.Name = "linkDirLabel";
      this.linkDirLabel.Size = new System.Drawing.Size(0, 13);
      this.linkDirLabel.TabIndex = 13;
      this.linkDirLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDirLabel_LinkClicked);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(9, 510);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(93, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "Wersja najnowsza";
      // 
      // chGeneratePDF
      // 
      this.chGeneratePDF.AutoSize = true;
      this.chGeneratePDF.Checked = true;
      this.chGeneratePDF.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chGeneratePDF.Location = new System.Drawing.Point(308, 69);
      this.chGeneratePDF.Name = "chGeneratePDF";
      this.chGeneratePDF.Size = new System.Drawing.Size(589, 17);
      this.chGeneratePDF.TabIndex = 15;
      this.chGeneratePDF.Text = "Pomiń generowanie pdf dla modułów, w których nie występują błędy (UWAGA: istnieją" +
    "ce pliki pdf zostaną skasowane).";
      this.chGeneratePDF.UseVisualStyleBackColor = true;
      // 
      // contextMenuStripLink
      // 
      this.contextMenuStripLink.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kopiujToolStripMenuItem});
      this.contextMenuStripLink.Name = "contextMenuStripLink";
      this.contextMenuStripLink.Size = new System.Drawing.Size(109, 26);
      // 
      // kopiujToolStripMenuItem
      // 
      this.kopiujToolStripMenuItem.Name = "kopiujToolStripMenuItem";
      this.kopiujToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
      this.kopiujToolStripMenuItem.Text = "Kopiuj";
      this.kopiujToolStripMenuItem.Click += new System.EventHandler(this.kopiujToolStripMenuItem_Click);
      // 
      // ConventerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1144, 529);
      this.Controls.Add(this.chGeneratePDF);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.linkDirLabel);
      this.Controls.Add(this.llOutput);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lvStatus);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lvEPK);
      this.Controls.Add(this.laStatus);
      this.Controls.Add(this.bnClose);
      this.Controls.Add(this.bnImporting);
      this.Controls.Add(this.bnStopConversion);
      this.Controls.Add(this.bnRunConversion);
      this.Controls.Add(this.bnSelectFolder);
      this.Controls.Add(this.tbDocDir);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConventerForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Konwenter DOCX";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
      this.contextMenuStripLink.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbDocDir;
    private System.Windows.Forms.Button bnSelectFolder;
    private System.Windows.Forms.Button bnRunConversion;
    private System.Windows.Forms.Button bnStopConversion;
    private System.Windows.Forms.Button bnImporting;
    private System.Windows.Forms.Button bnClose;
    private System.Windows.Forms.Label laStatus;
    private System.Windows.Forms.ListView lvEPK;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListView lvStatus;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.LinkLabel llOutput;
    private System.Windows.Forms.LinkLabel linkDirLabel;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox chGeneratePDF;
    private System.Windows.Forms.ContextMenuStrip contextMenuStripLink;
    private System.Windows.Forms.ToolStripMenuItem kopiujToolStripMenuItem;
  }
}