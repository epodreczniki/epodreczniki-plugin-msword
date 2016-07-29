using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Threading;
using Newtonsoft.Json;
using Word = Microsoft.Office.Interop.Word;
//using Range = Microsoft.Office.Interop.Word.StoryRanges;

namespace EP_WordPlugin
{
    public partial class EPWomiCatalogControl : UserControl
    {
//CONFIG: adres Otwartego Repozytorium Treści
      string apiUrlGetFolders = Properties.Settings.Default.URL_OTWARTE_REPOZYTORIUM_TRESCI + "/repo/folders?womi-only";
//CONFIG: adres Otwartego Repozytorium Treści
      string apiUrlSearchWomi = Properties.Settings.Default.URL_OTWARTE_REPOZYTORIUM_TRESCI + "/repo/searchwomi";
//CONFIG: adres Otwartego Repozytorium Treści
      string apiUrlPicture4WomiTemplate = Properties.Settings.Default.URL_OTWARTE_REPOZYTORIUM_TRESCI + "/repo/womi/{0}/image/classic?miniature";
//CONFIG: adres Otwartego Repozytorium Treści
      string apiUrlPicture4WomiSecondTemplate = Properties.Settings.Default.URL_OTWARTE_REPOZYTORIUM_TRESCI + "/repo/womi/{0}/image/ebook";
//CONFIG: adres Otwartego Repozytorium Treści
      string apiUrlInfo4WomiTemplate = Properties.Settings.Default.URL_OTWARTE_REPOZYTORIUM_TRESCI + "/repo/rt/docmetadata?id={0}";

        const int m_iWomiPageSize = 20;

        int m_WomiCurrentToolTipRow = -1;
        Image m_imageToolTip;
        int m_ToolTipTextHeight = 14;
        bool m_fPopupToolTip = false;
        private ToolsManager m_objToolsManager = null;
        private WomiManager m_objWomiManager = null;

        private int m_iWomiCurrentPageIndex = -1;
        private int m_iWomiFoundCount = 0;

		    delegate void lv_History_AddItemCallback(string Link,string Dir,DateTime time);

        private bool m_fProfilesLoaded = false;
        private TemporaryZipFileInfo m_objTempZipFileInfo = null;


        private Mutex m_MutexUnpackZipArchive = new Mutex(true, "UnzipMutex");

        public EPWomiCatalogControl()
        {
            InitializeComponent();
        }

        private int ReadValueFromVersionFile(string strVersionFilePath)
        {
            int iVersion = -1;
            if (File.Exists(strVersionFilePath))
            {
                using (XmlTextReader xmlReader = new XmlTextReader(strVersionFilePath))
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.Load(xmlReader);

                    XmlNode nodeVersion = objXmlDoc.SelectSingleNode("/plugin-descriptor-info/version");
                    if (nodeVersion != null)
                    {
                        if (Int32.TryParse(nodeVersion.InnerText, out iVersion))
                        {
                            return iVersion;
                        }
                    }

                    xmlReader.Close();
                }
            }

            return iVersion;
        }
        private void LoadToolsConfiguration()
        {
            string strPath4Tools = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "Tools");

            string strPath4CurrentVersionXmlFile = String.Join("\\", strPath4Tools, "current-plugin-descriptor-info.xml");
            string strPath4DownloadVersionXmlFile = String.Join("\\", strPath4Tools, "plugin-descriptor-info.xml");

            int iOldVersion = -1;
            //int iNewVersion = -1;

            WebClient webClient = new WebClient();

            try
            {
                iOldVersion = ReadValueFromVersionFile(strPath4CurrentVersionXmlFile);
                if (iOldVersion != -1)
                {
                    Globals.ThisAddIn.Path4CurrentTools = String.Join("\\", strPath4Tools, iOldVersion.ToString());
                }
                else
                {
                    if (!Directory.Exists(strPath4Tools))
                    {
                        Directory.CreateDirectory(strPath4Tools);
                    }

                    Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
                    Uri uriClickOnceLocation = new Uri(assemblyInfo.CodeBase);
                    string strClickOnceLocation = Path.GetDirectoryName(uriClickOnceLocation.LocalPath.ToString());

                    string strPath2InstalledZip = String.Join("\\", strClickOnceLocation, "Apps", "Tools.zip");
                    if (File.Exists(strPath2InstalledZip))
                    {
                        ZipFile.ExtractToDirectory(strPath2InstalledZip, strPath4Tools);
                        iOldVersion = ReadValueFromVersionFile(strPath4CurrentVersionXmlFile);
                        if (iOldVersion != -1)
                        {
                            Globals.ThisAddIn.Path4CurrentTools = String.Join("\\", strPath4Tools, iOldVersion.ToString());
                        }
                    }
                    else
                    {
                        if (!File.Exists(strPath2InstalledZip))
                        {
                            Globals.ThisAddIn.ShowErrorMessage(String.Format("Brak pliku : {0}", strPath2InstalledZip), String.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Globals.ThisAddIn.ShowErrorMessage("Wystąpił błąd w trakcie odtwarzania konfiguracji narzędzi", ex.ToString());
            }


            if (iOldVersion > 0)
            {
                this.tabPage_Tools.Text = String.Format("Narzędzia ( wersja {0} )", iOldVersion);
            }
        }
        private void HideDownloadZipGroupBox()
        {
            this.progressBarDownloading.Visible = false;
            this.groupBoxDownloading.Visible = false;
            this.dataGridView_Tools.Height =
                (this.groupBoxDownloading.Location.Y - this.dataGridView_Tools.Location.Y) + this.groupBoxDownloading.Height;
        }
        private void WebClientOnDownloadToolsZipProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.progressBarDownloading.Value = e.ProgressPercentage;
                this.progressBarDownloading.Invalidate();
            });
        }
        private void WebClientOnDownloadToolsZipCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (m_objTempZipFileInfo != null && e.Error == null)
            {
                int iNewVersion = m_objTempZipFileInfo.NewVersion;
                string strZipFilePath = m_objTempZipFileInfo.ZipFilePath;
                m_objTempZipFileInfo = null;

                if (File.Exists(strZipFilePath))
                {
                    try
                    {
                        string strDirectory4NewVersion = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath,
                            "EP_WORD_Plugin", "Tools", iNewVersion.ToString());
                        string strPath4CurrentVersionXmlFile = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath,
                            "EP_WORD_Plugin", "Tools", "current-plugin-descriptor-info.xml");
                        string strPath4DownloadVersionXmlFile = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath,
                            "EP_WORD_Plugin", "Tools", "plugin-descriptor-info.xml");

                        m_MutexUnpackZipArchive.WaitOne();
                        // BEGIN sekcja krytyczna
                        ZipFile.ExtractToDirectory(strZipFilePath, strDirectory4NewVersion);

                        string strDirectory4OldVersion = Globals.ThisAddIn.Path4CurrentTools;
                        Globals.ThisAddIn.Path4CurrentTools = strDirectory4NewVersion;

                        if (File.Exists(strPath4CurrentVersionXmlFile))
                        {
                            File.Delete(strPath4CurrentVersionXmlFile);
                        }
                        if (File.Exists(strPath4DownloadVersionXmlFile))
                        {
                            File.Move(strPath4DownloadVersionXmlFile, strPath4CurrentVersionXmlFile);
                        }

                        this.Invoke((MethodInvoker)delegate
                        {
                            this.tabPage_Tools.Text = String.Format("Narzędzia ( wersja {0} )", iNewVersion);

                            bool fIsError = false;
                            ToolsManager objTM = CreateToolsManager(out fIsError);
                            if (!fIsError)
                            {
                                m_objToolsManager = objTM;
                            }

                            LoadProfiles();
                            LoadStyleSet4Profile();
                            LoadTools4Profile();

                            // usunięcie starej wersji
                            if (!String.IsNullOrEmpty(strDirectory4OldVersion) && Directory.Exists(Globals.ThisAddIn.Path4CurrentTools))
                            {
                                Directory.Delete(strDirectory4OldVersion, true);
                            }

                            HideDownloadZipGroupBox();
                        });

                        // END sekcja krytyczna
                        m_MutexUnpackZipArchive.ReleaseMutex();

                    }
                    catch (Exception ex)
                    {
                        Globals.ThisAddIn.ShowErrorMessage("Wystąpił błąd w trakcie konfigurowania nowej wersji narzędzi.", ex.ToString());
                    }

                    // usunięcie ZIP-a
                    File.Delete(strZipFilePath);
                }
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    HideDownloadZipGroupBox(); // TODO - informacja o błędzie pobrania nowej wersji
                });
            }
        }

        public ToolsManager CreateToolsManager(out bool fIsError)
        {
            fIsError = false;
            
            if (!String.IsNullOrEmpty(Globals.ThisAddIn.Path4CurrentTools))
            {
                try
                {
                    string strPath4XML = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "plugin-descriptor.xml");

                    ToolsManager objTM = null;

                    using (XmlTextReader xmlReader = new XmlTextReader(strPath4XML))
                    {
                        XmlDocument objXmlDoc = new XmlDocument();
                        objXmlDoc.Load(xmlReader);

                        objTM = new ToolsManager();

                        // Narzędzia
                        XmlNodeList nodeXmlList = objXmlDoc.SelectNodes("/plugin-descriptor/tools/tool");
                        if (nodeXmlList != null && nodeXmlList.Count > 0)
                        {
                            XmlNode nodeParam;
                            foreach (XmlNode node in nodeXmlList)
                            {
                                string strId = node.Attributes["id"].Value;
                                string strLabel = String.Empty;
                                string strToolTip = String.Empty;
                                string strIcon = String.Empty;
                                string strTemplate = String.Empty;

                                nodeParam = node.SelectSingleNode("label");
                                if (nodeParam != null)
                                {
                                    strLabel = nodeParam.InnerText;
                                }
                                nodeParam = node.SelectSingleNode("alt");
                                if (nodeParam != null)
                                {
                                    strToolTip = nodeParam.InnerText;
                                }
                                nodeParam = node.SelectSingleNode("icon");
                                if (nodeParam != null)
                                {
                                    strIcon = nodeParam.InnerText;
                                }
                                nodeParam = node.SelectSingleNode("template");
                                if (nodeParam != null)
                                {
                                    strTemplate = nodeParam.InnerText;
                                }

                                bool fTemplateFileExists = false;
                                if(!String.IsNullOrEmpty(strTemplate))
                                {
                                    string strPath4TemplateXML = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "templates", strTemplate);
                                    fTemplateFileExists = File.Exists(strPath4TemplateXML);
                                }

                                objTM.AddTool(strId, strLabel, strToolTip, strIcon, strTemplate, fTemplateFileExists);
                            }
                        }
                        // Zespoły
                        nodeXmlList = objXmlDoc.SelectNodes("/plugin-descriptor/teams/team");
                        if (nodeXmlList != null && nodeXmlList.Count > 0)
                        {
                            XmlNodeList nodeXmlListTool2Teams;
                            foreach (XmlNode node in nodeXmlList)
                            {
                                XmlNode nodeTemp = node.SelectSingleNode("name");
                                if (nodeTemp != null && !String.IsNullOrEmpty(nodeTemp.InnerText))
                                {
                                    string strName = nodeTemp.InnerText;
                                    string strStyleSetFile = String.Empty;
                                    string strWOMITemplate = String.Empty;
                                    string strWOMITemplateA = String.Empty;
                                    string strWOMITemplateB = String.Empty;

                                    nodeTemp = node.SelectSingleNode("styleset");
                                    if (nodeTemp != null && !String.IsNullOrEmpty(nodeTemp.InnerText))
                                    {
                                        strStyleSetFile = nodeTemp.InnerText;
                                    }

                                    nodeTemp = node.SelectSingleNode("womi-template");
                                    if (nodeTemp != null && !String.IsNullOrEmpty(nodeTemp.InnerText))
                                    {
                                        strWOMITemplate = nodeTemp.InnerText;
                                    }

                                    nodeTemp = node.SelectSingleNode("womi-template-A");
                                    if (nodeTemp != null && !String.IsNullOrEmpty(nodeTemp.InnerText))
                                    {
                                        strWOMITemplateA = nodeTemp.InnerText;
                                    }

                                    nodeTemp = node.SelectSingleNode("womi-template-B");
                                    if (nodeTemp != null && !String.IsNullOrEmpty(nodeTemp.InnerText))
                                    {
                                        strWOMITemplateB = nodeTemp.InnerText;
                                    }

                                    objTM.AddTeam(strName, strStyleSetFile, strWOMITemplate, strWOMITemplateA, strWOMITemplateB);

                                    nodeXmlListTool2Teams = node.SelectNodes("tools/tool");
                                    if (nodeXmlListTool2Teams != null && nodeXmlListTool2Teams.Count > 0)
                                    {
                                        foreach (XmlNode nodeTool in nodeXmlListTool2Teams)
                                        {
                                            objTM.AddTool2Team(nodeTool.Attributes["id"].Value, strName);
                                        }
                                    }
                                }
                            }
                        }

                        xmlReader.Close();
                    }

                    return objTM;
                }
                catch(Exception ex)
                {
                    Globals.ThisAddIn.ShowErrorMessage("Wystąpił błąd w trakcie przetwarzania pliku konfiguracyjnego narzędzi.", ex.ToString());
                    fIsError = true;
                }
            }

            return null;
        }
        private void LoadProfiles()
        {
            if (m_objToolsManager != null)
            {
                PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();
                string strCurrentProfile = objPUS.TeamName;
                
                List<EP_Team> arobjTeam = m_objToolsManager.GetTeamList();
                if (arobjTeam != null && arobjTeam.Count > 0)
                {
                    this.cbProfile.DataSource = arobjTeam;
                    this.cbProfile.DisplayMember = "Name";
                    this.cbProfile.ValueMember = "Name";
                }

                if (String.IsNullOrEmpty(strCurrentProfile))
                {
                    if (this.cbProfile.SelectedItem != null)
                    {
                        objPUS.TeamName = ((EP_Team)this.cbProfile.SelectedItem).Name;
                    }
                }
                else
                {
                    int iIndex = this.cbProfile.FindStringExact(strCurrentProfile);
                    if (iIndex > 0)
                    {
                        this.cbProfile.SelectedIndex = iIndex;
                    }
                    else
                    {
                        if (this.cbProfile.SelectedItem != null)
                        {
                            objPUS.TeamName = ((EP_Team)this.cbProfile.SelectedItem).Name;
                        }
                    }
                }

                m_fProfilesLoaded = true;
            }
        }
        private void LoadStyleSet4Profile()
        {
            if (this.cbProfile.SelectedItem != null)
            {
                string strStyleSet = ((EP_Team)this.cbProfile.SelectedItem).StyleSet;
                string strStyleSetFile = String.Format("{0}.dotm", strStyleSet);

                string strAppDataFolder = Environment.ExpandEnvironmentVariables("%APPDATA%");
                string strPath2TargetStyleSetDir = String.Join("\\", strAppDataFolder, "Microsoft", "QuickStyles");
                string strPath2TargetStyleSetFile = String.Join("\\", strPath2TargetStyleSetDir, strStyleSetFile);

                string strPath2SourceStyleSetFile = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "stylesets", strStyleSetFile);

                try
                {
                    if (!File.Exists(strPath2TargetStyleSetFile) && File.Exists(strPath2SourceStyleSetFile))
                    {
                        if (!Directory.Exists(strPath2TargetStyleSetDir))
                        {
                            Directory.CreateDirectory(strPath2TargetStyleSetDir);
                        }

                        File.Copy(strPath2SourceStyleSetFile, strPath2TargetStyleSetFile);
                    }

                    if (Properties.Settings.Default.ChangeStyle == true)
                    {
                      bool fSavedBefore = Globals.ThisAddIn.Application.ActiveDocument.Saved;

                      Globals.ThisAddIn.Application.ActiveDocument.ApplyQuickStyleSet2(strStyleSet);
                      if (fSavedBefore)
                      {
                        Globals.ThisAddIn.Application.ActiveDocument.Saved = true;
                      }
                    }
                }
                catch (Exception)
                {
                    // brak aktywnego dokumentu w oknie aplikacji
                }
            }
        }
        private void LoadTools4Profile()
        {
            if (m_objToolsManager != null && this.cbProfile.SelectedItem != null)
            {
                EP_Team objSelectedTeam = (EP_Team)this.cbProfile.SelectedItem;
                List<EP_Tool> arobjTool = m_objToolsManager.GetToolList4Team(objSelectedTeam.Name);
                if (arobjTool != null && arobjTool.Count > 0)
                {
                    this.bindingSource_Tools.DataSource = arobjTool;
                }
                else
                {
                    this.bindingSource_Tools.DataSource = null;
                }
            }
        }
        private void UseTool(string strXmlFileName)
        {
            string strPath4Template = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "templates", strXmlFileName);
            if (File.Exists(strPath4Template))
            {
                Microsoft.Office.Interop.Word.Application app = Globals.ThisAddIn.Application;
                if (app.Selection != null)
                {
                    try
                    {
                      object missing = Type.Missing;
                        app.Selection.InsertFile(strPath4Template, ref missing, false, ref missing, ref missing);
                        app.ActiveWindow.SetFocus();
                    }
                    catch(Exception ex)
                    {
                        Globals.ThisAddIn.ShowErrorMessage(String.Format("Wystąpił błąd w trakcie użycia narzędzia z pliku {0}.", strXmlFileName), ex.ToString());
                    }
                }
            }
        }

        private void LoadWomiFolders(bool fForceRefreshing)
        {
            bool fWomiFoldersLoaded = false;
            string strPath4Cache = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "WomiFolders.xml");
            if (!fForceRefreshing && File.Exists(strPath4Cache))
            {
                try
                {
                    using (XmlTextReader xmlReader = new XmlTextReader(strPath4Cache))
                    {
                        XmlDocument objXmlDoc = new XmlDocument();
                        objXmlDoc.Load(xmlReader);

                        LoadWomiFolders2GUI(objXmlDoc, false);
                    }

                    fWomiFoldersLoaded = true;
                }
                catch(Exception /*ex*/)
                {

                }
            }

            if (!fWomiFoldersLoaded)
            {
                this.progressBar_WomiFolders.Value = 0;
                this.progressBar_WomiFolders.Visible = true;

                this.btRefresh.Enabled = false;

                // Pobranie danych
                WebClient webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.DownloadStringCompleted += WebClientOnDownloadFoldersCompleted;
                webClient.DownloadProgressChanged += WebClientOnDownloadFoldersProgressChanged;
                webClient.DownloadStringAsync(new Uri(apiUrlGetFolders));
            }
        }
        private void LoadWomiFolders2GUI(XmlDocument objXmlDoc, bool fWebClientOnDownloadFoldersCompleted)
        {
            if (objXmlDoc != null)
            {
                XmlNodeList nodeXmlList = objXmlDoc.SelectNodes("/Root/subfolders");

                List<WomiFolderItem> arobjWFI = new List<WomiFolderItem>();
                arobjWFI.Add(new WomiFolderItem(-1, "-- bez wyboru --"));

                if (nodeXmlList != null && nodeXmlList.Count > 0)
                {
                    m_objWomiManager = new WomiManager();
                    foreach (XmlNode nodeFolder in nodeXmlList)
                    {
                        m_objWomiManager.AddWomiFolder(nodeFolder);
                    }

                    // załadowanie listy
                    arobjWFI.AddRange(m_objWomiManager.GetWomiFolderList(false));
                }

                if (fWebClientOnDownloadFoldersCompleted)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.cbMainFolder.DataSource = arobjWFI;
                        this.cbMainFolder.DisplayMember = "FolderName";
                        this.cbMainFolder.ValueMember = "FolderId";

                        PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();
                        if (objPUS.MainFolderId != -1)
                        {
                            for (int iInd = 0; iInd < this.cbMainFolder.Items.Count; iInd++)
                            {
                                WomiFolderItem objWFI = (WomiFolderItem)this.cbMainFolder.Items[iInd];
                                if (objWFI.FolderId == objPUS.MainFolderId)
                                {
                                    this.cbMainFolder.SelectedIndex = iInd;
                                    break;
                                }
                            }
                        }

                        LoadWomiSubFolders();
                    });
                }
                else
                {
                    this.cbMainFolder.DataSource = arobjWFI;
                    this.cbMainFolder.DisplayMember = "FolderName";
                    this.cbMainFolder.ValueMember = "FolderId";

                    PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();
                    if (objPUS.MainFolderId != -1)
                    {
                        for (int iInd = 0; iInd < this.cbMainFolder.Items.Count; iInd++)
                        {
                            WomiFolderItem objWFI = (WomiFolderItem)this.cbMainFolder.Items[iInd];
                            if (objWFI.FolderId == objPUS.MainFolderId)
                            {
                                this.cbMainFolder.SelectedIndex = iInd;
                                break;
                            }
                        }
                    }

                    LoadWomiSubFolders();
                }

            }
        }
        private void LoadWomiSubFolders()
        {
            List<WomiFolderItem> arobjWFI = new List<WomiFolderItem>();
            arobjWFI.Add(new WomiFolderItem(-1, "-- bez wyboru --"));

            if (m_objWomiManager != null && this.cbMainFolder.SelectedItem != null)
            {
                WomiFolderItem objWFI = (WomiFolderItem)this.cbMainFolder.SelectedItem;

                if(objWFI.FolderId != -1)
                {
                    WomiFolder objWF = m_objWomiManager.GetMainWomiFolder(objWFI.FolderId);
                    if(objWF != null)
                    {
                        arobjWFI.AddRange(objWF.GetSubfolderItems(""));
                    }
                }
            }

            this.cbSubFolder.DataSource = arobjWFI;
            this.cbSubFolder.DisplayMember = "FolderName";
            this.cbSubFolder.ValueMember = "FolderId";

            PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();
            if (objPUS.SubFolderId != -1)
            {
                for (int iInd = 0; iInd < this.cbSubFolder.Items.Count; iInd++)
                {
                    WomiFolderItem objWFI = (WomiFolderItem)this.cbSubFolder.Items[iInd];
                    if (objWFI.FolderId == objPUS.SubFolderId)
                    {
                        this.cbSubFolder.SelectedIndex = iInd;
                        break;
                    }
                }
            }
        }
        private void SearchWomi(int iPageIndex)
        {
            this.progressBar_WomiSearch.Visible = true;

            this.btSearchWomi.Enabled = false;

            PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();

            int iFilter_FolderId = 1;
            int iMainFolderId = -1;
            if (this.cbMainFolder.SelectedItem != null)
            {
                WomiFolderItem objMain = (WomiFolderItem)this.cbMainFolder.SelectedItem;
                iMainFolderId = objMain.FolderId;
            }

            if (iMainFolderId == -1)
            {
                objPUS.MainFolderId = -1;
                objPUS.SubFolderId = -1;
            }
            else
            {
                int iSubFolderId = -1;
                if (this.cbSubFolder.SelectedItem != null)
                {
                    WomiFolderItem objSub = (WomiFolderItem)this.cbSubFolder.SelectedItem;
                    iSubFolderId = objSub.FolderId;
                }

                if (iSubFolderId != -1)
                {
                    iFilter_FolderId = iSubFolderId;
                }
                else
                {
                    iFilter_FolderId = iMainFolderId;
                }

                objPUS.MainFolderId = iMainFolderId;
                objPUS.SubFolderId = iSubFolderId;
            }

            string strURL = String.Format("{0}?folder={1}", apiUrlSearchWomi, iFilter_FolderId);
            if (!String.IsNullOrEmpty(this.textTitle.Text))
            {
              string szText = this.textTitle.Text;
              szText.Trim();
              /*if (szText.LastIndexOf('*') == szText.Length - 1 && szText.Length > 0)
              {
                szText = szText.Remove(szText.Length - 1, 1);
              }*/

              //strURL = String.Format("{0}&text={1}*&keyword={2}*", strURL, szText, szText);
              if(szText.LastIndexOf('_') == -1)
                strURL = String.Format("{0}&all={1}*", strURL, szText);
              else
                strURL = String.Format("{0}&all={1}", strURL, szText);
            }

            objPUS.Text2Search = this.textTitle.Text;

            if(iPageIndex >= 0 && m_iWomiPageSize > 0)
            {
                strURL = String.Format("{0}&pageIndex={1}&pageSize={2}", strURL, iPageIndex, m_iWomiPageSize);
            }

            m_iWomiCurrentPageIndex = iPageIndex;

            // Pobranie danych
            WebClient webClient = new WebClient();
            webClient.Headers["Accept"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.DownloadStringCompleted += WebClientOnDownloadWOMIsCompleted;
            webClient.DownloadProgressChanged += WebClientOnDownloadWOMIsProgressChanged;
            webClient.DownloadStringAsync(new Uri(strURL));

            dataGridView_WOMI.Focus();
        }
        private void AddWomi2Document(WomiItem objWI)
        {
            if (objWI != null)
            {
                Microsoft.Office.Interop.Word.Application app = Globals.ThisAddIn.Application;
                if (app.Selection != null && app.Selection.Tables.Count <= 1) // nie można zaznaczyć więcej niż jednej tabeli
                {
                  string szWOMITemplate = "WOMI.xml";
                  string szWOMITemplateA = "WOMI_kontekstA.xml";
                  string szWOMITemplateB = "WOMI_kontekstB.xml";

                  EP_Team objTeam = (EP_Team)this.cbProfile.SelectedItem;
                  if (objTeam != null)
                  {
                    if (objTeam.WOMITemplate != "")
                      szWOMITemplate = objTeam.WOMITemplate;

                    if (objTeam.WOMITemplateA != "")
                      szWOMITemplateA = objTeam.WOMITemplateA;

                    if (objTeam.WOMITemplateB != "")
                      szWOMITemplateB = objTeam.WOMITemplateB;
                  }

                    string strPath2WomiTemplate = String.Empty;
                    if (app.Selection.Tables.Count == 1 && app.Selection.Tables[1] != null)
                    {
                        Microsoft.Office.Interop.Word.Table objTable = app.Selection.Tables[1];
                        if(objTable.Descr.StartsWith("EP_METADATA_GLOBAL") || 
                           objTable.Descr.StartsWith("EP_METADATA_CWICZENIE") ||
                           objTable.Descr.StartsWith("EP_METADATA_ZADANIE"))
                        {
                          strPath2WomiTemplate = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "womi", szWOMITemplateA);
                        }
                        else if(objTable.Descr.CompareTo("EP_METADATA_BIOGRAM") == 0 ||
                                objTable.Descr.CompareTo("EP_METADATA_WYDARZENIE") == 0 ||
                                objTable.Descr.CompareTo("EP_WOMI_GALLERY") == 0)
                        {
                          strPath2WomiTemplate = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "womi", szWOMITemplateB);
                        }
                        else
                        {
                          strPath2WomiTemplate = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "womi", szWOMITemplate);
                        }
                    }
                    else
                    {
                      strPath2WomiTemplate = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "womi", szWOMITemplate);
                    }





                    if (File.Exists(strPath2WomiTemplate))
                    {
                        string strWOMI = String.Empty;

                        using (XmlTextReader xmlReader = new XmlTextReader(strPath2WomiTemplate))
                        {
                            XmlDocument objXmlDoc = new XmlDocument();
                            objXmlDoc.Load(xmlReader);

                            strWOMI = objXmlDoc.OuterXml;

                            xmlReader.Close();
                        }

                        strWOMI = strWOMI.Replace("[WOMI_ID]", String.Format("{0:D5}", objWI.Id));
                        strWOMI = strWOMI.Replace("[WOMI_TYPE]", objWI.MediaType);

                        string strObjWITitle = SecurityElement.Escape(objWI.Title);
                        strWOMI = strWOMI.Replace("[WOMI_TITLE]", strObjWITitle);

                        string strObjWName = SecurityElement.Escape(objWI.Name);
                        strWOMI = strWOMI.Replace("[WOMI_NAME]", strObjWName);

                        try
                        {
                            app.Selection.InsertXML(strWOMI);

                            int wordWomiSelectionBegin = app.Selection.End;

                            Microsoft.Office.Interop.Word.Range rng = app.ActiveDocument.Range();
                            rng.Find.ClearFormatting();
                            rng.Find.Replacement.ClearFormatting();
                            rng.Find.Text = String.Format("WOMI_IMG_URL_{0:D5}", objWI.Id);
                            object missing = Type.Missing;
                            if (rng.Find.Execute(ref missing,
                                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                                ref missing, ref missing))
                            {
                                rng.Select();

                                string strTempPictureFileName = Guid.NewGuid().ToString().Replace("-", "");
                                string strPath4TEMP_PICTURE = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", strTempPictureFileName);

                                try
                                {
                                  // Test
                                  //Microsoft.Office.Interop.Word.InlineShape objIS = app.Selection.InlineShapes.AddPicture(String.Format(apiUrlPicture4WomiTemplate, objWI.Id), true, true);
                                  //objIS.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                                  //objIS.Width = 150;

                                  WebClient webClient = new WebClient();
                                  webClient.DownloadFile(String.Format(apiUrlPicture4WomiTemplate, objWI.Id), strPath4TEMP_PICTURE);

                                  if (File.Exists(strPath4TEMP_PICTURE))
                                  {
                                    Microsoft.Office.Interop.Word.InlineShape picture = app.Selection.InlineShapes.AddPicture(strPath4TEMP_PICTURE, false, true);
                                    picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    picture.Width = 200;

                                    if (picture.Height > picture.Width * 5)
                                      picture.Height = 800;
                                    File.Delete(strPath4TEMP_PICTURE);
                                  }
                                }
                                catch (WebException /*ex*/)
                                {
                                  //Get the assembly informationSystem.Reflection.Assembly
                                  Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();

                                  //Location is where the assembly is run from 
                                  string assemblyLocation = assemblyInfo.Location;

                                  Uri uriCodeBase = new Uri(assemblyInfo.CodeBase);
                                  string ClickOnceLocation = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString());

                                  // może być tak, że nie będzie miniatury
                                  if (objWI.MediaType == "AUDIO")
                                  {
                                    Microsoft.Office.Interop.Word.InlineShape picture = app.Selection.InlineShapes.AddPicture(ClickOnceLocation + "\\Images\\ikonka-nutka(200px).gif", false, true);
                                    picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    picture.Width = 200;

                                  }
                                  else
                                  {
                                    Microsoft.Office.Interop.Word.InlineShape picture = app.Selection.InlineShapes.AddPicture(ClickOnceLocation + "\\Images\\ikonka-puzzle(200px).gif", false, true);
                                    picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                                    picture.Width = 200;

                                  }
                                }
                                catch (Exception ex)
                                {
                                  Globals.ThisAddIn.ShowErrorMessage("Wystąpił błąd w trakcie pobierania miniaturki elementu WOMI", ex.ToString());
                                }

                                if (app.Selection.Tables.Count == 1)
                                {
                                    Microsoft.Office.Interop.Word.Table objWomiTable = app.Selection.Tables[1];
                                    if (objWomiTable != null)
                                    {
                                        app.Selection.Start = objWomiTable.Range.End;
                                    }
                                }
                            }

                            rng.Find.ClearFormatting();
                            rng.Find.Replacement.ClearFormatting();

                            app.ActiveWindow.SetFocus();
                        }
                        catch (Exception ex)
                        {
                            Globals.ThisAddIn.ShowErrorMessage("Wystąpił błąd w trakcie wklejania elementu WOMI do dokumentu", ex.ToString());
                        }
                    }
                }
            }
        }
        private void ShowWomiInBrowser(WomiItem objWI)
        {
            if (objWI != null)
            {
                string strUrl = String.Format(apiUrlInfo4WomiTemplate, objWI.Id);
                ProcessStartInfo objPSI = new ProcessStartInfo(strUrl);

                try
                {
                    try
                    {
                        Process.Start(objPSI);
                    }
                    catch (Exception)
                    {
                        // Jeśli nie jest ustawiona domyślna przeglądarka w Windows 8
                        Process.Start("iexplore.exe", objPSI.FileName);
                    }
                }
                catch(Exception ex)
                {
                    StringBuilder objSB = new StringBuilder();
                    objSB.Append("Wystąpił problem z wyświetleniem informacji o elemencie WOMI.");
                    objSB.Append("\n");
                    objSB.Append("Rozwiązaniem jest ustawienie w systemie domyślnej przeglądarki lub");
                    objSB.Append("\n");
                    objSB.Append("instalacja programu 'Internet Explorer'");
                    Globals.ThisAddIn.ShowErrorMessage(objSB.ToString(), ex.ToString());
                }
            }
        }

        private void EPWomiCatalogControl_Load(object sender, EventArgs e)
        {
            LoadToolsConfiguration();

            bool fIsError = false;
            m_MutexUnpackZipArchive.WaitOne();
            ToolsManager objTM = CreateToolsManager(out fIsError);
            m_MutexUnpackZipArchive.ReleaseMutex();
            if (!fIsError)
            {
                m_objToolsManager = objTM;
            }
            
            LoadProfiles();
            LoadStyleSet4Profile();
            LoadTools4Profile();

            LoadWomiFolders(false);

            PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();
            this.textTitle.Text = objPUS.Text2Search;

            this.btWomiPageNext.Enabled = false;
            this.btWomiPagePrevious.Enabled = false;
            this.numericWomiPageUpDown.Enabled = false;
            this.btWomiGoToLastPage.Text = "1";
            this.btWomiGoToLastPage.Enabled = false;

            LoadHistoryFile();
        }

        private void WebClientOnDownloadFoldersCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            List<WomiFolderItem> arobjWFI = new List<WomiFolderItem>();
            arobjWFI.Add(new WomiFolderItem(-1, "-- bez wyboru --"));

            if (e.Error == null && e.Result != null)
            {
                // Analiza rezultatu
                XmlDocument objXmlDoc = JsonConvert.DeserializeXmlNode(e.Result, "Root");
                LoadWomiFolders2GUI(objXmlDoc, true);

                this.Invoke((MethodInvoker)delegate
                {
                    this.labelMainFolder.Text = "Główny katalog";
                    this.progressBar_WomiFolders.Visible = false;

                    this.btRefresh.Enabled = true;
                });

                try
                {
                    string strPath4Cache = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "WomiFolders.xml");
                    objXmlDoc.Save(strPath4Cache);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.labelMainFolder.Text = "Główny katalog (BŁĄD POBIERANIA DANYCH)";
                    this.progressBar_WomiFolders.Visible = false;

                    this.btRefresh.Enabled = true;
                });
            }
        }
        private void WebClientOnDownloadFoldersProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.progressBar_WomiFolders.Value = e.ProgressPercentage;
                this.progressBar_WomiFolders.Invalidate();
            });
        }

        private void WebClientOnDownloadWOMIsCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                // Analiza rezultatu
                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(e.Result, "Root");
                XmlNode nodeCount = xmlDoc.SelectSingleNode("/Root/count");
                if(nodeCount != null)
                {
                    Int32.TryParse(nodeCount.InnerText, out m_iWomiFoundCount);
                }

                List<WomiItem> arobjWomiItem = null;

                XmlNodeList nodeXmlList = xmlDoc.SelectNodes("/Root/items");
                if (nodeXmlList != null && nodeXmlList.Count > 0)
                {
                    arobjWomiItem = new List<WomiItem>();
                    XmlNode nodeParam = null;
                    foreach(XmlNode nodeWI in nodeXmlList)
                    {
                        int iId = -1;
                        nodeParam = nodeWI.SelectSingleNode("id");
                        if(nodeParam != null && Int32.TryParse(nodeParam.InnerText, out iId))
                        {
                            nodeParam = nodeWI.SelectSingleNode("media-type");
                            if (nodeParam != null)
                            {
                                string strMediaType = nodeParam.InnerText;
                                nodeParam = nodeWI.SelectSingleNode("title");
                                if (nodeParam != null)
                                {
                                    string strTitle = nodeParam.InnerText;

                                    string strAuthor = String.Empty;
                                    nodeParam = nodeWI.SelectSingleNode("author");
                                    if (nodeParam != null)
                                    {
                                        strAuthor = nodeParam.InnerText;
                                    }
                                    string strKeywords = String.Empty;
                                    nodeParam = nodeWI.SelectSingleNode("keywords");
                                    if (nodeParam != null)
                                    {
                                        strKeywords = nodeParam.InnerText;
                                    }

                                    string strName = String.Empty;
                                    nodeParam = nodeWI.SelectSingleNode("name");
                                    if (nodeParam != null)
                                    {
                                      strName = nodeParam.InnerText;
                                    }

                                    arobjWomiItem.Add(new WomiItem(iId, strMediaType, strTitle, strAuthor, strKeywords, strName));
                                }
                            }
                        }
                    }

                }

                this.Invoke((MethodInvoker)delegate
                {
                    if(this.radioButtonName.Checked == true)
                      this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
                    else
                      this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";

                    this.bindingSource_WOMI.DataSource = arobjWomiItem;

                    if (m_iWomiFoundCount > 0)
                    {
                        int iPageCount = (m_iWomiFoundCount / m_iWomiPageSize);
                        if ((m_iWomiFoundCount % m_iWomiPageSize) > 0)
                        {
                            iPageCount++;
                        }

                        this.labelFoundWomi.Text = String.Format("Znalezione WOMI ({0}, strona {1} z {2})",
                            m_iWomiFoundCount, (m_iWomiCurrentPageIndex + 1), iPageCount);

                        this.btWomiPagePrevious.Enabled = (m_iWomiCurrentPageIndex > 0);
                        this.btWomiPageNext.Enabled = ((m_iWomiCurrentPageIndex + 1) < iPageCount);

                        this.numericWomiPageUpDown.Enabled = (iPageCount > 1);
                        this.numericWomiPageUpDown.Minimum = 1;
                        this.numericWomiPageUpDown.Value = m_iWomiCurrentPageIndex + 1;
                        this.numericWomiPageUpDown.Maximum = iPageCount;
                        this.btWomiGoToLastPage.Text = iPageCount.ToString();
                        this.btWomiGoToLastPage.Enabled = (iPageCount > 1);
                    }
                    else
                    {
                        this.labelFoundWomi.Text = "Znalezione WOMI (brak)";
                        this.btWomiPagePrevious.Enabled = false;
                        this.btWomiPageNext.Enabled = false;
                        this.numericWomiPageUpDown.Enabled = false;
                        this.numericWomiPageUpDown.Minimum = 1;
                        this.numericWomiPageUpDown.Value = 1;
                        this.numericWomiPageUpDown.Maximum = 1;
                        this.btWomiGoToLastPage.Text = "1";
                        this.btWomiGoToLastPage.Enabled = false;
                    }

                    this.progressBar_WomiSearch.Visible = false;

                    this.btSearchWomi.Enabled = true;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.labelFoundWomi.Text = "Znalezione WOMI (BŁĄD POBIERANIA DANYCH)";
                    this.progressBar_WomiSearch.Visible = false; ;

                    this.btWomiPagePrevious.Enabled = false;
                    this.btWomiPageNext.Enabled = false;
                    this.numericWomiPageUpDown.Enabled = false;
                    this.numericWomiPageUpDown.Minimum = 1;
                    this.numericWomiPageUpDown.Value = 1;
                    this.numericWomiPageUpDown.Maximum = 1;
                    this.btWomiGoToLastPage.Text = "1";
                    this.btWomiGoToLastPage.Enabled = false;

                    this.btSearchWomi.Enabled = true;
                });
            }
        }
        private void WebClientOnDownloadWOMIsProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.progressBar_WomiSearch.Value = e.ProgressPercentage;
                this.progressBar_WomiSearch.Invalidate();
            });
        }

        private void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_fProfilesLoaded)
            {
                PluginUserSettings objPUS = Globals.ThisAddIn.GetPluginUserSettings();
                if (this.cbProfile.SelectedItem != null)
                {
                    objPUS.TeamName = ((EP_Team)this.cbProfile.SelectedItem).Name;
                }

                LoadStyleSet4Profile();
                LoadTools4Profile();

                try
                {
                    Globals.ThisAddIn.Application.ActiveWindow.SetFocus();
                }
                catch
                {
                    // brak aktywnego dokumentu
                }
            }
        }
        private void dataGridView_Tools_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                string strTemplate = (string)this.dataGridView_Tools.SelectedRows[0].Cells[4].Value;
                UseTool(strTemplate);
            }
        }

        private void tabControl_EP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13 && this.tabControl_EP.SelectedIndex == 1 && this.btSearchWomi.Enabled)
            {
                SearchWomi(0);
            }
        }
        private void cbMainFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWomiSubFolders();
            this.btSearchWomi.Focus();
        }
        private void cbSubFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btSearchWomi.Focus();
        }
        private void textTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                SearchWomi(0);
            }
        }
        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadWomiFolders(true);
        }
        private void btSearchWomi_Click(object sender, EventArgs e)
        {
            SearchWomi(0);
        }
        private void dataGridView_WOMI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                //this.dataGridView_WOMI.DataSource
                WomiItem objWI = this.dataGridView_WOMI.SelectedRows[0].DataBoundItem as WomiItem;
                //WomiItem selected = (WomiItem)row;

                /*int iId = (int)this.dataGridView_WOMI.Rows[e.RowIndex].Cells[0].Value;
              
                string strTitle = (string)this.dataGridView_WOMI.Rows[e.RowIndex].Cells[3].Value;
                string strName = (string)this.dataGridView_WOMI.Rows[e.RowIndex].Cells[3].Value;


                string strMediaType = (string)this.dataGridView_WOMI.Rows[e.RowIndex].Cells[4].Value;

                WomiItem objWI = new WomiItem(iId, strMediaType, strTitle, String.Empty, String.Empty, strName);*/
                AddWomi2Document(objWI);
            }
            else if(e.ColumnIndex == 5)
            {
                int iId = (int)this.dataGridView_WOMI.Rows[e.RowIndex].Cells[0].Value;
                WomiItem objWI = new WomiItem(iId, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                ShowWomiInBrowser(objWI);
            }
        }
        private void btWomiPagePrevious_Click(object sender, EventArgs e)
        {
            int iChosenPageIndex = ((int)this.numericWomiPageUpDown.Value) - 1;
            if (m_iWomiCurrentPageIndex != iChosenPageIndex)
            {
                SearchWomi(iChosenPageIndex);
            }            
            else if(m_iWomiCurrentPageIndex > 0)
            {
                SearchWomi(m_iWomiCurrentPageIndex - 1);
            }
        }
        private void btWomiGoToLastPage_Click(object sender, EventArgs e)
        {
            int iPageCount = (m_iWomiFoundCount / m_iWomiPageSize);
            if ((m_iWomiFoundCount % m_iWomiPageSize) > 0)
            {
                iPageCount++;
            }

            SearchWomi(iPageCount - 1);
        }
        private void btWomiPageNext_Click(object sender, EventArgs e)
        {
            int iChosenPageIndex = ((int)this.numericWomiPageUpDown.Value) - 1;
            //if (iChosenPageIndex == 0)
            //  return;

            if (m_iWomiCurrentPageIndex != iChosenPageIndex)
            {
              if (iChosenPageIndex != 0)
                SearchWomi(iChosenPageIndex - 1);
            }
            else
            {
                int iPageCount = (m_iWomiFoundCount / m_iWomiPageSize);
                if ((m_iWomiFoundCount % m_iWomiPageSize) > 0)
                {
                    iPageCount++;
                }

                if ((m_iWomiCurrentPageIndex + 1) < iPageCount)
                {
                    SearchWomi(m_iWomiCurrentPageIndex + 1);
                }
            }
        }

        private void toolTipWOMI_Draw(object sender, DrawToolTipEventArgs e)
        {
          // Draw the custom background.
          Rectangle imageRect = e.Bounds;
          imageRect.Height -= m_ToolTipTextHeight;
          Rectangle textRect = new Rectangle(imageRect.X, imageRect.Height, imageRect.Width, m_ToolTipTextHeight);

          e.Graphics.FillRectangle(SystemBrushes.ActiveCaption, imageRect);

          e.Graphics.FillRectangle(SystemBrushes.Window, textRect);


          // Draw the standard border.
          e.DrawBorder();

          if (m_imageToolTip != null)
          {
            imageRect.X = (imageRect.Width - m_imageToolTip.Size.Width) / 2;
            imageRect.Width = m_imageToolTip.Size.Width;

            e.Graphics.DrawImage(m_imageToolTip, imageRect);
          }

          using (StringFormat sf = new StringFormat())
          {
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Far;
            sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            using (Font f = new Font("Tahoma", 9))
            {
              e.Graphics.DrawString(e.ToolTipText, f,
                  SystemBrushes.ActiveCaptionText, e.Bounds, sf);
            }
          }
        }

        private void toolTipWOMI_Popup(object sender, PopupEventArgs e)
        {
          if (m_fPopupToolTip == false)
          {
            e.Cancel = true;
            //m_fPopupToolTip = true;
            return;
          }
          

          bool fTryAgain = false;
          int iId = (int)this.dataGridView_WOMI.Rows[m_WomiCurrentToolTipRow].Cells[0].Value;

          try
          {

            WebClient webClient = new WebClient();
            byte[] myDataBuffer = webClient.DownloadData(String.Format(apiUrlPicture4WomiTemplate, iId));
            Stream s = new MemoryStream();
            s.Write(myDataBuffer, 0, myDataBuffer.Length);
            m_imageToolTip = Bitmap.FromStream(s);
          }
          catch (ArgumentException /*ex1*/)
          {
            fTryAgain = true; // może się tak zdarzyć bo nieodpowiedni format obrazka
          }
          catch (WebException)
          {
            fTryAgain = true; // może się tak zdarzyć bo nieodpowiedni format obrazka
          }
          catch (Exception ex)
          {
            System.Windows.Forms.MessageBox.Show(ex.ToString(), "EP_WordPlugin : Wystąpił nieoczekiwany błąd");
          }

          if (fTryAgain)
          {
            try
            {
              WebClient webClient = new WebClient();
              byte[] myDataBuffer = webClient.DownloadData(String.Format(apiUrlPicture4WomiSecondTemplate, iId));
              Stream s = new MemoryStream();
              s.Write(myDataBuffer, 0, myDataBuffer.Length);
              m_imageToolTip = Bitmap.FromStream(s);
            }
            catch (WebException)
            {
              // dzwięk nie ma obrazka
              m_imageToolTip.Dispose();
              m_imageToolTip = null;
            }
            catch (Exception ex)
            {
              System.Windows.Forms.MessageBox.Show(ex.ToString(), "EP_WordPlugin : Wystąpił nieoczekiwany błąd");
            }
          }

          Size sizeToolTip = new Size(0,0);
          if (m_imageToolTip != null)
            sizeToolTip = m_imageToolTip.Size;

          using (Font f = new Font("Tahoma", 9))
          {
            Size sizeText = TextRenderer.MeasureText( toolTipWOMI.GetToolTip(e.AssociatedControl), f);
            m_ToolTipTextHeight = sizeText.Height;

            if (sizeText.Width > sizeToolTip.Width)
              sizeToolTip.Width = sizeText.Width;

            sizeToolTip.Height += sizeText.Height;

            if (m_imageToolTip != null)
              e.ToolTipSize = sizeToolTip;
            else
              e.ToolTipSize = sizeText;
          }
        }

        private void dataGridView_WOMI_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
          if (e.RowIndex != m_WomiCurrentToolTipRow)
          {
            m_fPopupToolTip = false;
            this.toolTipWOMI.Hide(this.dataGridView_WOMI);
            timerWOMI.Enabled = false;
            m_WomiCurrentToolTipRow = e.RowIndex;
            timerWOMI.Enabled = true;
          }
        }

        private void dataGridView_WOMI_MouseLeave(object sender, EventArgs e)
        {
          timerWOMI.Enabled = false;
        }

        private void dataGridView_MouseEnter(object sender, EventArgs e)
        {
          this.toolTipWOMI.SetToolTip(this.dataGridView_WOMI, " ");
          //this.dataGridView_WOMI.ShowCellToolTips = true;
          timerWOMI.Enabled = false;
          timerWOMI.Enabled = true;
        }

        private void timerWOMI_Tick(object sender, EventArgs e)
        {
          timerWOMI.Enabled = false;
          if (m_WomiCurrentToolTipRow != -1 && dataGridView_WOMI.Rows.Count > 0 && m_WomiCurrentToolTipRow < dataGridView_WOMI.Rows.Count)
          {
            //m_fPopupToolTip = true;
            //this.toolTipWOMI.Active = true;
            m_fPopupToolTip = true;
            //this.toolTipWOMI.Show(/*dataGridView_WOMI.Rows[m_WomiCurrentToolTipRow].Cells[2].Value.ToString()*/"ble ble", this.dataGridView_WOMI);
            this.toolTipWOMI.SetToolTip(this.dataGridView_WOMI, dataGridView_WOMI.Rows[m_WomiCurrentToolTipRow].Cells[3].Value.ToString());
            //m_fPopupToolTip = false;
          }
        }

        private void lv_History_Click(object sender, EventArgs e)
        {
          Process.Start(lv_History.SelectedItems[0].Text);
        }

        public void lv_History_AddItem(string Link, string Dir, DateTime time)
        {
          if (this.lv_History.InvokeRequired)
          {
            lv_History_AddItemCallback d = new lv_History_AddItemCallback(lv_History_AddItem);
            this.Invoke(d, new object[] { Link, Dir, time });
          }
          else
          {

            if (lv_History != null)
            {
              ListViewItem lvItem = this.lv_History.Items.Insert(0, Link);
              lvItem.SubItems.Add(Dir);
              lvItem.SubItems.Add(time.ToString("s"));
            }
          }
        }

        private void LoadHistoryFile()
        {
          Cursor.Current = Cursors.WaitCursor;

          this.lv_History.Items.Clear();

          string strPath4HistoryLog = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "history.log");
          if (File.Exists(strPath4HistoryLog))
          {
            StreamReader r = File.OpenText(strPath4HistoryLog);
            string line;
            while ((line = r.ReadLine()) != null)
            {
              string[] words = { "||" };
              string[] entries = line.Split(words, StringSplitOptions.RemoveEmptyEntries);

              ListViewItem lvItem = this.lv_History.Items.Insert(0, entries[0]);
              lvItem.SubItems.Add(entries[1]);
              lvItem.SubItems.Add(entries[2]);
            }

            r.Close();
          }

          Cursor.Current = Cursors.Default;
        }

        private void SaveHistoryFile()
        {
          Cursor.Current = Cursors.WaitCursor;

          string strPath4HistoryLog = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "history.log");
          StreamWriter w = File.CreateText(strPath4HistoryLog);
          for (int i = 0; i < lv_History.Items.Count; i++)
          {
            w.WriteLine("{0}||{1}||{2}", lv_History.Items[i].Text, lv_History.Items[i].SubItems[1].Text, lv_History.Items[i].SubItems[2].Text);
          }
          w.Close();

          Cursor.Current = Cursors.Default;
        }

        private void bnDelHistory_Click(object sender, EventArgs e)
        {
          this.lv_History.Items.Clear();
          string strPath4HistoryLog = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "history.log");
          File.Delete(strPath4HistoryLog);
        }

        private void radioButtonTitle_CheckedChanged(object sender, EventArgs e)
        {
          this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";
        }

        private void radioButtonName_CheckedChanged(object sender, EventArgs e)
        {
          this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
        }
    }

    public class TemporaryZipFileInfo
    {
        private int m_iNewVersion = -1;
        private string m_strZipFilePath = String.Empty;

        public int NewVersion
        {
            get { return m_iNewVersion; }
        }
        public string ZipFilePath
        {
            get { return m_strZipFilePath; }
        }

        public TemporaryZipFileInfo(int iNewVersion, string strZipFilePath)
        {
            m_iNewVersion = iNewVersion;
            m_strZipFilePath = strZipFilePath;
        }
    }

    [Serializable]
    public class PluginUserSettings
    {
        // Tools
        private string m_strTeamName = String.Empty;
        // WOMI
        private int m_iMainFolderId = -1;
        private int m_iSubFolderId = -1;
        private string m_strText2Search = String.Empty;

        public string TeamName
        {
            get { return m_strTeamName; }
            set { m_strTeamName = value; }
        }
        public int MainFolderId
        {
            get { return m_iMainFolderId; }
            set { m_iMainFolderId = value; }
        }
        public int SubFolderId
        {
            get { return m_iSubFolderId; }
            set { m_iSubFolderId = value; }
        }
        public string Text2Search
        {
            get { return m_strText2Search; }
            set { m_strText2Search = value; }
        }

        public PluginUserSettings()
        {
        }
    }
}
