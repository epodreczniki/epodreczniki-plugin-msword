namespace EP_WordPlugin
{
    partial class RibbonEP : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonEP()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.tabEBooks = this.Factory.CreateRibbonTab();
      this.groupView = this.Factory.CreateRibbonGroup();
      this.btWomiPanel = this.Factory.CreateRibbonToggleButton();
      this.btConverterForm = this.Factory.CreateRibbonButton();
      this.btConvertSingleFile = this.Factory.CreateRibbonButton();
      this.btImportSingleFile = this.Factory.CreateRibbonButton();
      this.bnAbout = this.Factory.CreateRibbonButton();
      this.chChnageStyle = this.Factory.CreateRibbonCheckBox();
      this.tabEBooks.SuspendLayout();
      this.groupView.SuspendLayout();
      // 
      // tabEBooks
      // 
      this.tabEBooks.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
      this.tabEBooks.Groups.Add(this.groupView);
      this.tabEBooks.Label = "e-Podręczniki";
      this.tabEBooks.Name = "tabEBooks";
      // 
      // groupView
      // 
      this.groupView.Items.Add(this.btWomiPanel);
      this.groupView.Items.Add(this.btConverterForm);
      this.groupView.Items.Add(this.btConvertSingleFile);
      this.groupView.Items.Add(this.btImportSingleFile);
      this.groupView.Items.Add(this.bnAbout);
      this.groupView.Items.Add(this.chChnageStyle);
      this.groupView.Label = "Widok";
      this.groupView.Name = "groupView";
      // 
      // btWomiPanel
      // 
      this.btWomiPanel.Checked = true;
      this.btWomiPanel.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
      this.btWomiPanel.Image = global::EP_WordPlugin.Properties.Resources.narzedzia;
      this.btWomiPanel.Label = "Panel Narzędzia/WOMI";
      this.btWomiPanel.Name = "btWomiPanel";
      this.btWomiPanel.ShowImage = true;
      this.btWomiPanel.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btWomiPanel_Click);
      // 
      // btConverterForm
      // 
      this.btConverterForm.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
      this.btConverterForm.Image = global::EP_WordPlugin.Properties.Resources.konwersja_i_import_jeden_plik;
      this.btConverterForm.Label = "Konwersja podręcznika";
      this.btConverterForm.Name = "btConverterForm";
      this.btConverterForm.ShowImage = true;
      this.btConverterForm.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btConverterForm_Click);
      // 
      // btConvertSingleFile
      // 
      this.btConvertSingleFile.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
      this.btConvertSingleFile.Image = global::EP_WordPlugin.Properties.Resources.konwersja_i_import;
      this.btConvertSingleFile.Label = "Konwersja pojedynczego pliku";
      this.btConvertSingleFile.Name = "btConvertSingleFile";
      this.btConvertSingleFile.ShowImage = true;
      this.btConvertSingleFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btConverSingleFile);
      // 
      // btImportSingleFile
      // 
      this.btImportSingleFile.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
      this.btImportSingleFile.Image = global::EP_WordPlugin.Properties.Resources.konwersja_i_import;
      this.btImportSingleFile.Label = "Import pojedynczego pliku";
      this.btImportSingleFile.Name = "btImportSingleFile";
      this.btImportSingleFile.ShowImage = true;
      this.btImportSingleFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btImportSingleFile_Click);
      // 
      // bnAbout
      // 
      this.bnAbout.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
      this.bnAbout.Image = global::EP_WordPlugin.Properties.Resources.menu4b;
      this.bnAbout.Label = "O dodatku...";
      this.bnAbout.Name = "bnAbout";
      this.bnAbout.ShowImage = true;
      this.bnAbout.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.bnAbout_Click);
      // 
      // chChnageStyle
      // 
      this.chChnageStyle.Checked = true;
      this.chChnageStyle.Label = "Automatycznie zmień styl na e-Podręcznikowy";
      this.chChnageStyle.Name = "chChnageStyle";
      this.chChnageStyle.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.chChnageStyle_Click);
      // 
      // RibbonEP
      // 
      this.Name = "RibbonEP";
      this.RibbonType = "Microsoft.Word.Document";
      this.Tabs.Add(this.tabEBooks);
      this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
      this.tabEBooks.ResumeLayout(false);
      this.tabEBooks.PerformLayout();
      this.groupView.ResumeLayout(false);
      this.groupView.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabEBooks;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupView;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btWomiPanel;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btConverterForm;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btConvertSingleFile;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton bnAbout;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btImportSingleFile;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox chChnageStyle;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonEP Ribbon1
        {
            get { return this.GetRibbon<RibbonEP>(); }
        }
    }
}
