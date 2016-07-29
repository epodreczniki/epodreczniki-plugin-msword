namespace EP_WordPlugin
{
    partial class EPWomiCatalogControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
      this.btSearchWomi = new System.Windows.Forms.Button();
      this.tabControl_EP = new System.Windows.Forms.TabControl();
      this.tabPage_Tools = new System.Windows.Forms.TabPage();
      this.groupBoxDownloading = new System.Windows.Forms.GroupBox();
      this.progressBarDownloading = new System.Windows.Forms.ProgressBar();
      this.dataGridView_Tools = new System.Windows.Forms.DataGridView();
      this.Tools_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Tools_Button = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
      this.Tools_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Tools_Template = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bindingSource_Tools = new System.Windows.Forms.BindingSource(this.components);
      this.label2 = new System.Windows.Forms.Label();
      this.cbProfile = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPage_WOMI = new System.Windows.Forms.TabPage();
      this.radioButtonName = new System.Windows.Forms.RadioButton();
      this.radioButtonTitle = new System.Windows.Forms.RadioButton();
      this.labelWomiGoToPage = new System.Windows.Forms.Label();
      this.btWomiGoToLastPage = new System.Windows.Forms.Button();
      this.numericWomiPageUpDown = new System.Windows.Forms.NumericUpDown();
      this.progressBar_WomiSearch = new System.Windows.Forms.ProgressBar();
      this.btWomiPageNext = new System.Windows.Forms.Button();
      this.btWomiPagePrevious = new System.Windows.Forms.Button();
      this.dataGridView_WOMI = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.bindingSource_WOMI = new System.Windows.Forms.BindingSource(this.components);
      this.labelFoundWomi = new System.Windows.Forms.Label();
      this.groupBox_WomiSearch = new System.Windows.Forms.GroupBox();
      this.progressBar_WomiFolders = new System.Windows.Forms.ProgressBar();
      this.btRefresh = new System.Windows.Forms.Button();
      this.textTitle = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cbSubFolder = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbMainFolder = new System.Windows.Forms.ComboBox();
      this.labelMainFolder = new System.Windows.Forms.Label();
      this.tabPage_History = new System.Windows.Forms.TabPage();
      this.bnDelHistory = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.lv_History = new System.Windows.Forms.ListView();
      this.columnLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnDir = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.toolTipWOMI = new System.Windows.Forms.ToolTip(this.components);
      this.timerWOMI = new System.Windows.Forms.Timer(this.components);
      this.tabControl_EP.SuspendLayout();
      this.tabPage_Tools.SuspendLayout();
      this.groupBoxDownloading.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Tools)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Tools)).BeginInit();
      this.tabPage_WOMI.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericWomiPageUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WOMI)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource_WOMI)).BeginInit();
      this.groupBox_WomiSearch.SuspendLayout();
      this.tabPage_History.SuspendLayout();
      this.SuspendLayout();
      // 
      // btSearchWomi
      // 
      this.btSearchWomi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btSearchWomi.Location = new System.Drawing.Point(289, 153);
      this.btSearchWomi.Name = "btSearchWomi";
      this.btSearchWomi.Size = new System.Drawing.Size(100, 23);
      this.btSearchWomi.TabIndex = 1;
      this.btSearchWomi.Text = "Szukaj";
      this.btSearchWomi.UseVisualStyleBackColor = true;
      this.btSearchWomi.Click += new System.EventHandler(this.btSearchWomi_Click);
      // 
      // tabControl_EP
      // 
      this.tabControl_EP.Controls.Add(this.tabPage_Tools);
      this.tabControl_EP.Controls.Add(this.tabPage_WOMI);
      this.tabControl_EP.Controls.Add(this.tabPage_History);
      this.tabControl_EP.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl_EP.Location = new System.Drawing.Point(0, 0);
      this.tabControl_EP.Margin = new System.Windows.Forms.Padding(0);
      this.tabControl_EP.Name = "tabControl_EP";
      this.tabControl_EP.Padding = new System.Drawing.Point(25, 3);
      this.tabControl_EP.SelectedIndex = 0;
      this.tabControl_EP.Size = new System.Drawing.Size(416, 720);
      this.tabControl_EP.TabIndex = 0;
      this.tabControl_EP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tabControl_EP_KeyPress);
      // 
      // tabPage_Tools
      // 
      this.tabPage_Tools.Controls.Add(this.groupBoxDownloading);
      this.tabPage_Tools.Controls.Add(this.dataGridView_Tools);
      this.tabPage_Tools.Controls.Add(this.label2);
      this.tabPage_Tools.Controls.Add(this.cbProfile);
      this.tabPage_Tools.Controls.Add(this.label1);
      this.tabPage_Tools.Location = new System.Drawing.Point(4, 22);
      this.tabPage_Tools.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
      this.tabPage_Tools.Name = "tabPage_Tools";
      this.tabPage_Tools.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
      this.tabPage_Tools.Size = new System.Drawing.Size(408, 694);
      this.tabPage_Tools.TabIndex = 0;
      this.tabPage_Tools.Text = "Narzędzia";
      this.tabPage_Tools.UseVisualStyleBackColor = true;
      // 
      // groupBoxDownloading
      // 
      this.groupBoxDownloading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxDownloading.Controls.Add(this.progressBarDownloading);
      this.groupBoxDownloading.Location = new System.Drawing.Point(4, 652);
      this.groupBoxDownloading.Name = "groupBoxDownloading";
      this.groupBoxDownloading.Size = new System.Drawing.Size(398, 39);
      this.groupBoxDownloading.TabIndex = 5;
      this.groupBoxDownloading.TabStop = false;
      this.groupBoxDownloading.Text = "Pobieranie nowej wersji narzędzi";
      // 
      // progressBarDownloading
      // 
      this.progressBarDownloading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBarDownloading.Location = new System.Drawing.Point(7, 18);
      this.progressBarDownloading.Name = "progressBarDownloading";
      this.progressBarDownloading.Size = new System.Drawing.Size(385, 14);
      this.progressBarDownloading.TabIndex = 0;
      // 
      // dataGridView_Tools
      // 
      this.dataGridView_Tools.AllowUserToAddRows = false;
      this.dataGridView_Tools.AllowUserToDeleteRows = false;
      this.dataGridView_Tools.AllowUserToResizeColumns = false;
      this.dataGridView_Tools.AllowUserToResizeRows = false;
      this.dataGridView_Tools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView_Tools.AutoGenerateColumns = false;
      this.dataGridView_Tools.BackgroundColor = System.Drawing.SystemColors.ControlLight;
      this.dataGridView_Tools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView_Tools.ColumnHeadersVisible = false;
      this.dataGridView_Tools.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tools_Id,
            this.Tools_Button,
            this.Column1,
            this.Tools_Name,
            this.Tools_Template});
      this.dataGridView_Tools.DataSource = this.bindingSource_Tools;
      dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView_Tools.DefaultCellStyle = dataGridViewCellStyle14;
      this.dataGridView_Tools.GridColor = System.Drawing.SystemColors.ControlLight;
      this.dataGridView_Tools.Location = new System.Drawing.Point(4, 69);
      this.dataGridView_Tools.MultiSelect = false;
      this.dataGridView_Tools.Name = "dataGridView_Tools";
      this.dataGridView_Tools.ReadOnly = true;
      this.dataGridView_Tools.RowHeadersVisible = false;
      this.dataGridView_Tools.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataGridView_Tools.RowTemplate.Height = 40;
      this.dataGridView_Tools.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dataGridView_Tools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView_Tools.ShowCellErrors = false;
      this.dataGridView_Tools.ShowEditingIcon = false;
      this.dataGridView_Tools.ShowRowErrors = false;
      this.dataGridView_Tools.Size = new System.Drawing.Size(398, 577);
      this.dataGridView_Tools.TabIndex = 3;
      this.dataGridView_Tools.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Tools_CellContentClick);
      // 
      // Tools_Id
      // 
      this.Tools_Id.DataPropertyName = "Id";
      this.Tools_Id.HeaderText = "Id";
      this.Tools_Id.Name = "Tools_Id";
      this.Tools_Id.ReadOnly = true;
      this.Tools_Id.Visible = false;
      // 
      // Tools_Button
      // 
      this.Tools_Button.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.Tools_Button.DataPropertyName = "CommandText";
      dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(4);
      this.Tools_Button.DefaultCellStyle = dataGridViewCellStyle12;
      this.Tools_Button.HeaderText = "Komenda";
      this.Tools_Button.MinimumWidth = 80;
      this.Tools_Button.Name = "Tools_Button";
      this.Tools_Button.ReadOnly = true;
      this.Tools_Button.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Tools_Button.Text = "";
      this.Tools_Button.ToolTipText = "Nacisnij przycisk aby wstawić element do dokumentu";
      this.Tools_Button.Width = 80;
      // 
      // Column1
      // 
      this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.Column1.DataPropertyName = "Icon";
      this.Column1.HeaderText = "Icon";
      this.Column1.MinimumWidth = 40;
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Width = 40;
      // 
      // Tools_Name
      // 
      this.Tools_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Tools_Name.DataPropertyName = "LabelLong";
      dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      this.Tools_Name.DefaultCellStyle = dataGridViewCellStyle13;
      this.Tools_Name.HeaderText = "Name";
      this.Tools_Name.Name = "Tools_Name";
      this.Tools_Name.ReadOnly = true;
      this.Tools_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // Tools_Template
      // 
      this.Tools_Template.DataPropertyName = "Template";
      this.Tools_Template.HeaderText = "";
      this.Tools_Template.Name = "Tools_Template";
      this.Tools_Template.ReadOnly = true;
      this.Tools_Template.Visible = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(213, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Narzędzia dostępne dla wybranego profilu   ";
      // 
      // cbProfile
      // 
      this.cbProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbProfile.FormattingEnabled = true;
      this.cbProfile.Location = new System.Drawing.Point(4, 24);
      this.cbProfile.Name = "cbProfile";
      this.cbProfile.Size = new System.Drawing.Size(398, 21);
      this.cbProfile.TabIndex = 1;
      this.cbProfile.SelectedIndexChanged += new System.EventHandler(this.cbProfile_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 7);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(30, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Profil";
      // 
      // tabPage_WOMI
      // 
      this.tabPage_WOMI.Controls.Add(this.radioButtonName);
      this.tabPage_WOMI.Controls.Add(this.radioButtonTitle);
      this.tabPage_WOMI.Controls.Add(this.labelWomiGoToPage);
      this.tabPage_WOMI.Controls.Add(this.btWomiGoToLastPage);
      this.tabPage_WOMI.Controls.Add(this.numericWomiPageUpDown);
      this.tabPage_WOMI.Controls.Add(this.progressBar_WomiSearch);
      this.tabPage_WOMI.Controls.Add(this.btWomiPageNext);
      this.tabPage_WOMI.Controls.Add(this.btWomiPagePrevious);
      this.tabPage_WOMI.Controls.Add(this.dataGridView_WOMI);
      this.tabPage_WOMI.Controls.Add(this.labelFoundWomi);
      this.tabPage_WOMI.Controls.Add(this.groupBox_WomiSearch);
      this.tabPage_WOMI.Location = new System.Drawing.Point(4, 22);
      this.tabPage_WOMI.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
      this.tabPage_WOMI.Name = "tabPage_WOMI";
      this.tabPage_WOMI.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage_WOMI.Size = new System.Drawing.Size(408, 694);
      this.tabPage_WOMI.TabIndex = 1;
      this.tabPage_WOMI.Text = "WOMI";
      this.tabPage_WOMI.UseVisualStyleBackColor = true;
      // 
      // radioButtonName
      // 
      this.radioButtonName.AutoSize = true;
      this.radioButtonName.Location = new System.Drawing.Point(118, 210);
      this.radioButtonName.Name = "radioButtonName";
      this.radioButtonName.Size = new System.Drawing.Size(58, 17);
      this.radioButtonName.TabIndex = 15;
      this.radioButtonName.Text = "Nazwa";
      this.radioButtonName.UseVisualStyleBackColor = true;
      this.radioButtonName.CheckedChanged += new System.EventHandler(this.radioButtonName_CheckedChanged);
      // 
      // radioButtonTitle
      // 
      this.radioButtonTitle.AutoSize = true;
      this.radioButtonTitle.Checked = true;
      this.radioButtonTitle.Location = new System.Drawing.Point(10, 210);
      this.radioButtonTitle.Name = "radioButtonTitle";
      this.radioButtonTitle.Size = new System.Drawing.Size(50, 17);
      this.radioButtonTitle.TabIndex = 14;
      this.radioButtonTitle.TabStop = true;
      this.radioButtonTitle.Text = "Tytuł";
      this.radioButtonTitle.UseVisualStyleBackColor = true;
      this.radioButtonTitle.CheckedChanged += new System.EventHandler(this.radioButtonTitle_CheckedChanged);
      // 
      // labelWomiGoToPage
      // 
      this.labelWomiGoToPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.labelWomiGoToPage.AutoSize = true;
      this.labelWomiGoToPage.Location = new System.Drawing.Point(191, 673);
      this.labelWomiGoToPage.Name = "labelWomiGoToPage";
      this.labelWomiGoToPage.Size = new System.Drawing.Size(12, 13);
      this.labelWomiGoToPage.TabIndex = 13;
      this.labelWomiGoToPage.Text = "z";
      // 
      // btWomiGoToLastPage
      // 
      this.btWomiGoToLastPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btWomiGoToLastPage.Location = new System.Drawing.Point(208, 668);
      this.btWomiGoToLastPage.Name = "btWomiGoToLastPage";
      this.btWomiGoToLastPage.Size = new System.Drawing.Size(48, 23);
      this.btWomiGoToLastPage.TabIndex = 12;
      this.btWomiGoToLastPage.Text = "1000";
      this.btWomiGoToLastPage.UseVisualStyleBackColor = true;
      this.btWomiGoToLastPage.Click += new System.EventHandler(this.btWomiGoToLastPage_Click);
      // 
      // numericWomiPageUpDown
      // 
      this.numericWomiPageUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.numericWomiPageUpDown.Location = new System.Drawing.Point(137, 670);
      this.numericWomiPageUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericWomiPageUpDown.Name = "numericWomiPageUpDown";
      this.numericWomiPageUpDown.Size = new System.Drawing.Size(48, 20);
      this.numericWomiPageUpDown.TabIndex = 11;
      this.numericWomiPageUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericWomiPageUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // progressBar_WomiSearch
      // 
      this.progressBar_WomiSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar_WomiSearch.Location = new System.Drawing.Point(296, 192);
      this.progressBar_WomiSearch.Name = "progressBar_WomiSearch";
      this.progressBar_WomiSearch.Size = new System.Drawing.Size(100, 13);
      this.progressBar_WomiSearch.TabIndex = 10;
      this.progressBar_WomiSearch.Visible = false;
      // 
      // btWomiPageNext
      // 
      this.btWomiPageNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btWomiPageNext.Location = new System.Drawing.Point(262, 668);
      this.btWomiPageNext.Name = "btWomiPageNext";
      this.btWomiPageNext.Size = new System.Drawing.Size(48, 23);
      this.btWomiPageNext.TabIndex = 6;
      this.btWomiPageNext.Text = ">";
      this.btWomiPageNext.UseVisualStyleBackColor = true;
      this.btWomiPageNext.Click += new System.EventHandler(this.btWomiPageNext_Click);
      // 
      // btWomiPagePrevious
      // 
      this.btWomiPagePrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btWomiPagePrevious.Location = new System.Drawing.Point(83, 668);
      this.btWomiPagePrevious.Name = "btWomiPagePrevious";
      this.btWomiPagePrevious.Size = new System.Drawing.Size(48, 23);
      this.btWomiPagePrevious.TabIndex = 5;
      this.btWomiPagePrevious.Text = "<";
      this.btWomiPagePrevious.UseVisualStyleBackColor = true;
      this.btWomiPagePrevious.Click += new System.EventHandler(this.btWomiPagePrevious_Click);
      // 
      // dataGridView_WOMI
      // 
      this.dataGridView_WOMI.AllowUserToAddRows = false;
      this.dataGridView_WOMI.AllowUserToDeleteRows = false;
      this.dataGridView_WOMI.AllowUserToResizeColumns = false;
      this.dataGridView_WOMI.AllowUserToResizeRows = false;
      this.dataGridView_WOMI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView_WOMI.AutoGenerateColumns = false;
      this.dataGridView_WOMI.BackgroundColor = System.Drawing.SystemColors.ControlLight;
      this.dataGridView_WOMI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView_WOMI.ColumnHeadersVisible = false;
      this.dataGridView_WOMI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewButtonColumn1,
            this.Column4,
            this.dataGridViewTextBoxColumn2,
            this.Column2,
            this.Column3});
      this.dataGridView_WOMI.DataSource = this.bindingSource_WOMI;
      dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView_WOMI.DefaultCellStyle = dataGridViewCellStyle11;
      this.dataGridView_WOMI.GridColor = System.Drawing.SystemColors.ControlLight;
      this.dataGridView_WOMI.Location = new System.Drawing.Point(5, 238);
      this.dataGridView_WOMI.MultiSelect = false;
      this.dataGridView_WOMI.Name = "dataGridView_WOMI";
      this.dataGridView_WOMI.ReadOnly = true;
      this.dataGridView_WOMI.RowHeadersVisible = false;
      this.dataGridView_WOMI.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataGridView_WOMI.RowTemplate.Height = 40;
      this.dataGridView_WOMI.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dataGridView_WOMI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView_WOMI.ShowCellErrors = false;
      this.dataGridView_WOMI.ShowCellToolTips = false;
      this.dataGridView_WOMI.ShowEditingIcon = false;
      this.dataGridView_WOMI.ShowRowErrors = false;
      this.dataGridView_WOMI.Size = new System.Drawing.Size(397, 424);
      this.dataGridView_WOMI.TabIndex = 4;
      this.dataGridView_WOMI.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_WOMI_CellContentClick);
      this.dataGridView_WOMI.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_WOMI_CellMouseMove);
      this.dataGridView_WOMI.MouseEnter += new System.EventHandler(this.dataGridView_MouseEnter);
      this.dataGridView_WOMI.MouseLeave += new System.EventHandler(this.dataGridView_WOMI_MouseLeave);
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
      this.dataGridViewTextBoxColumn1.HeaderText = "Id";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Visible = false;
      // 
      // dataGridViewButtonColumn1
      // 
      this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dataGridViewButtonColumn1.DataPropertyName = "CommandText";
      dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(4);
      this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle8;
      this.dataGridViewButtonColumn1.HeaderText = "Komenda";
      this.dataGridViewButtonColumn1.MinimumWidth = 80;
      this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
      this.dataGridViewButtonColumn1.ReadOnly = true;
      this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridViewButtonColumn1.Text = "";
      this.dataGridViewButtonColumn1.ToolTipText = "Naciśnij przycisk aby wstawić element do dokumentu";
      this.dataGridViewButtonColumn1.Width = 80;
      // 
      // Column4
      // 
      this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.Column4.DataPropertyName = "Icon";
      this.Column4.HeaderText = "Ikonka";
      this.Column4.MinimumWidth = 40;
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Column4.Width = 40;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";
      dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle9;
      this.dataGridViewTextBoxColumn2.HeaderText = "Title";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "MediaType";
      this.Column2.HeaderText = "MediaType";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Visible = false;
      // 
      // Column3
      // 
      this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.Column3.DataPropertyName = "CommandBrowserText";
      dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(4);
      this.Column3.DefaultCellStyle = dataGridViewCellStyle10;
      this.Column3.HeaderText = "Browser";
      this.Column3.MinimumWidth = 60;
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Column3.ToolTipText = "Naciśnij przycisk aby zobaczyć informacje";
      this.Column3.Width = 60;
      // 
      // labelFoundWomi
      // 
      this.labelFoundWomi.AutoSize = true;
      this.labelFoundWomi.Location = new System.Drawing.Point(7, 193);
      this.labelFoundWomi.Name = "labelFoundWomi";
      this.labelFoundWomi.Size = new System.Drawing.Size(93, 13);
      this.labelFoundWomi.TabIndex = 3;
      this.labelFoundWomi.Text = "Znalezione WOMI";
      // 
      // groupBox_WomiSearch
      // 
      this.groupBox_WomiSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_WomiSearch.Controls.Add(this.progressBar_WomiFolders);
      this.groupBox_WomiSearch.Controls.Add(this.btRefresh);
      this.groupBox_WomiSearch.Controls.Add(this.textTitle);
      this.groupBox_WomiSearch.Controls.Add(this.btSearchWomi);
      this.groupBox_WomiSearch.Controls.Add(this.label5);
      this.groupBox_WomiSearch.Controls.Add(this.cbSubFolder);
      this.groupBox_WomiSearch.Controls.Add(this.label4);
      this.groupBox_WomiSearch.Controls.Add(this.cbMainFolder);
      this.groupBox_WomiSearch.Controls.Add(this.labelMainFolder);
      this.groupBox_WomiSearch.Location = new System.Drawing.Point(7, 4);
      this.groupBox_WomiSearch.Name = "groupBox_WomiSearch";
      this.groupBox_WomiSearch.Size = new System.Drawing.Size(395, 182);
      this.groupBox_WomiSearch.TabIndex = 2;
      this.groupBox_WomiSearch.TabStop = false;
      this.groupBox_WomiSearch.Text = "Wyszukiwanie";
      // 
      // progressBar_WomiFolders
      // 
      this.progressBar_WomiFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar_WomiFolders.Location = new System.Drawing.Point(289, 19);
      this.progressBar_WomiFolders.Name = "progressBar_WomiFolders";
      this.progressBar_WomiFolders.Size = new System.Drawing.Size(100, 13);
      this.progressBar_WomiFolders.TabIndex = 9;
      this.progressBar_WomiFolders.Visible = false;
      // 
      // btRefresh
      // 
      this.btRefresh.Location = new System.Drawing.Point(6, 153);
      this.btRefresh.Name = "btRefresh";
      this.btRefresh.Size = new System.Drawing.Size(132, 23);
      this.btRefresh.TabIndex = 8;
      this.btRefresh.Text = "Odśwież listę katalogów";
      this.btRefresh.UseVisualStyleBackColor = true;
      this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
      // 
      // textTitle
      // 
      this.textTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textTitle.Location = new System.Drawing.Point(6, 121);
      this.textTitle.MaxLength = 50;
      this.textTitle.Name = "textTitle";
      this.textTitle.Size = new System.Drawing.Size(383, 20);
      this.textTitle.TabIndex = 6;
      this.textTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textTitle_KeyDown);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(7, 105);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(105, 13);
      this.label5.TabIndex = 5;
      this.label5.Text = "Wszystkie metadane";
      // 
      // cbSubFolder
      // 
      this.cbSubFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbSubFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbSubFolder.FormattingEnabled = true;
      this.cbSubFolder.Location = new System.Drawing.Point(6, 77);
      this.cbSubFolder.Name = "cbSubFolder";
      this.cbSubFolder.Size = new System.Drawing.Size(383, 21);
      this.cbSubFolder.TabIndex = 4;
      this.cbSubFolder.SelectedIndexChanged += new System.EventHandler(this.cbSubFolder_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(4, 61);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(61, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Podkatalog";
      // 
      // cbMainFolder
      // 
      this.cbMainFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMainFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbMainFolder.FormattingEnabled = true;
      this.cbMainFolder.Location = new System.Drawing.Point(6, 35);
      this.cbMainFolder.Name = "cbMainFolder";
      this.cbMainFolder.Size = new System.Drawing.Size(383, 21);
      this.cbMainFolder.TabIndex = 2;
      this.cbMainFolder.SelectedIndexChanged += new System.EventHandler(this.cbMainFolder_SelectedIndexChanged);
      // 
      // labelMainFolder
      // 
      this.labelMainFolder.AutoSize = true;
      this.labelMainFolder.Location = new System.Drawing.Point(4, 19);
      this.labelMainFolder.Name = "labelMainFolder";
      this.labelMainFolder.Size = new System.Drawing.Size(82, 13);
      this.labelMainFolder.TabIndex = 0;
      this.labelMainFolder.Text = "Główny katalog";
      // 
      // tabPage_History
      // 
      this.tabPage_History.Controls.Add(this.bnDelHistory);
      this.tabPage_History.Controls.Add(this.label6);
      this.tabPage_History.Controls.Add(this.lv_History);
      this.tabPage_History.Location = new System.Drawing.Point(4, 22);
      this.tabPage_History.Name = "tabPage_History";
      this.tabPage_History.Size = new System.Drawing.Size(408, 694);
      this.tabPage_History.TabIndex = 2;
      this.tabPage_History.Text = "Historia";
      this.tabPage_History.UseVisualStyleBackColor = true;
      // 
      // bnDelHistory
      // 
      this.bnDelHistory.Location = new System.Drawing.Point(248, 6);
      this.bnDelHistory.Name = "bnDelHistory";
      this.bnDelHistory.Size = new System.Drawing.Size(89, 23);
      this.bnDelHistory.TabIndex = 2;
      this.bnDelHistory.Text = "Usuń historię";
      this.bnDelHistory.UseVisualStyleBackColor = true;
      this.bnDelHistory.Click += new System.EventHandler(this.bnDelHistory_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(3, 11);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(132, 13);
      this.label6.TabIndex = 1;
      this.label6.Text = "Historia importów...            ";
      // 
      // lv_History
      // 
      this.lv_History.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.lv_History.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
      this.lv_History.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lv_History.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnLink,
            this.columnDir,
            this.columnDate});
      this.lv_History.FullRowSelect = true;
      this.lv_History.Location = new System.Drawing.Point(0, 40);
      this.lv_History.Name = "lv_History";
      this.lv_History.ShowItemToolTips = true;
      this.lv_History.Size = new System.Drawing.Size(405, 658);
      this.lv_History.TabIndex = 0;
      this.lv_History.UseCompatibleStateImageBehavior = false;
      this.lv_History.View = System.Windows.Forms.View.Details;
      this.lv_History.Click += new System.EventHandler(this.lv_History_Click);
      // 
      // columnLink
      // 
      this.columnLink.Text = "Link";
      this.columnLink.Width = 125;
      // 
      // columnDir
      // 
      this.columnDir.Text = "Katalog";
      this.columnDir.Width = 109;
      // 
      // columnDate
      // 
      this.columnDate.Text = "Data";
      this.columnDate.Width = 82;
      // 
      // toolTipWOMI
      // 
      this.toolTipWOMI.AutomaticDelay = 100;
      this.toolTipWOMI.AutoPopDelay = 8000;
      this.toolTipWOMI.InitialDelay = 100;
      this.toolTipWOMI.OwnerDraw = true;
      this.toolTipWOMI.ReshowDelay = 20;
      this.toolTipWOMI.ShowAlways = true;
      this.toolTipWOMI.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.toolTipWOMI.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipWOMI_Draw);
      this.toolTipWOMI.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipWOMI_Popup);
      // 
      // timerWOMI
      // 
      this.timerWOMI.Interval = 1000;
      this.timerWOMI.Tick += new System.EventHandler(this.timerWOMI_Tick);
      // 
      // EPWomiCatalogControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabControl_EP);
      this.MinimumSize = new System.Drawing.Size(300, 300);
      this.Name = "EPWomiCatalogControl";
      this.Size = new System.Drawing.Size(416, 720);
      this.Load += new System.EventHandler(this.EPWomiCatalogControl_Load);
      this.tabControl_EP.ResumeLayout(false);
      this.tabPage_Tools.ResumeLayout(false);
      this.tabPage_Tools.PerformLayout();
      this.groupBoxDownloading.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Tools)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource_Tools)).EndInit();
      this.tabPage_WOMI.ResumeLayout(false);
      this.tabPage_WOMI.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericWomiPageUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WOMI)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource_WOMI)).EndInit();
      this.groupBox_WomiSearch.ResumeLayout(false);
      this.groupBox_WomiSearch.PerformLayout();
      this.tabPage_History.ResumeLayout(false);
      this.tabPage_History.PerformLayout();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSearchWomi;
        private System.Windows.Forms.TabControl tabControl_EP;
        private System.Windows.Forms.TabPage tabPage_Tools;
        private System.Windows.Forms.TabPage tabPage_WOMI;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_Tools;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bindingSource_Tools;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tools_Id;
        private System.Windows.Forms.DataGridViewButtonColumn Tools_Button;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tools_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tools_Template;
        private System.Windows.Forms.GroupBox groupBox_WomiSearch;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSubFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMainFolder;
        private System.Windows.Forms.Label labelMainFolder;
        private System.Windows.Forms.DataGridView dataGridView_WOMI;
        private System.Windows.Forms.Label labelFoundWomi;
        private System.Windows.Forms.BindingSource bindingSource_WOMI;
        private System.Windows.Forms.Button btWomiPageNext;
        private System.Windows.Forms.Button btWomiPagePrevious;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.GroupBox groupBoxDownloading;
        private System.Windows.Forms.ProgressBar progressBarDownloading;
        private System.Windows.Forms.ProgressBar progressBar_WomiSearch;
        private System.Windows.Forms.ProgressBar progressBar_WomiFolders;
        private System.Windows.Forms.ToolTip toolTipWOMI;
        private System.Windows.Forms.TabPage tabPage_History;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lv_History;
        private System.Windows.Forms.ColumnHeader columnLink;
        private System.Windows.Forms.ColumnHeader columnDir;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.Button bnDelHistory;
        private System.Windows.Forms.Timer timerWOMI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.Button btWomiGoToLastPage;
        private System.Windows.Forms.NumericUpDown numericWomiPageUpDown;
        private System.Windows.Forms.Label labelWomiGoToPage;
        private System.Windows.Forms.RadioButton radioButtonName;
        private System.Windows.Forms.RadioButton radioButtonTitle;
    }
}
