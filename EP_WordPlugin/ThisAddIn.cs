using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Microsoft;
using System.Deployment;


namespace EP_WordPlugin
{
    public partial class ThisAddIn
    {
        private string m_strPath4CurrentTools = String.Empty;
        private PluginUserSettings m_objPluginUserSettings = null;
        List<Word.Window> m_arWindows = new List<Word.Window>();


        public string Path4CurrentTools
        {
            get { return m_strPath4CurrentTools; }
            set { m_strPath4CurrentTools = value; }
        }
        public PluginUserSettings GetPluginUserSettings()
        {
            if(m_objPluginUserSettings == null)
            {
                m_objPluginUserSettings = new PluginUserSettings(); 
            }

            return m_objPluginUserSettings;
        }

        public void CreateRightPanel(Word.Window wn)
        {
          

            if(wn == null)
            {
                return;
            }

          
            if (m_arWindows.IndexOf(wn) == -1)
              m_arWindows.Add(wn);
            else
              return;

            List<int> arobj2RemoveIndex = null;

            Microsoft.Office.Tools.CustomTaskPane objFoundPane = null;
            for (int iInd = 0; (objFoundPane == null && iInd < Globals.ThisAddIn.CustomTaskPanes.Count); iInd++)
            {
                Microsoft.Office.Tools.CustomTaskPane objCustomTaskPane = Globals.ThisAddIn.CustomTaskPanes[iInd];

                try
                {
                    if (((Word.Window)objCustomTaskPane.Window) == wn)
                    {
                        objFoundPane = objCustomTaskPane;
                    }
                }
                catch(Exception )
                {
                    if (arobj2RemoveIndex == null)
                        arobj2RemoveIndex = new List<int>();

                    arobj2RemoveIndex.Add(iInd);
                }
            }

            if (arobj2RemoveIndex != null && arobj2RemoveIndex.Count > 0)
            {
                for (int iInd2Remove = arobj2RemoveIndex.Count - 1; iInd2Remove >= 0; iInd2Remove-- )
                //foreach (int iInd2Remove in arobj2RemoveIndex)
                {
                  Globals.ThisAddIn.CustomTaskPanes.RemoveAt(arobj2RemoveIndex[iInd2Remove]);
                }
            }

            if (objFoundPane == null)
            {
                EPWomiCatalogControl panelWomiCatalogControl = new EPWomiCatalogControl();
                objFoundPane = Globals.ThisAddIn.CustomTaskPanes.Add(panelWomiCatalogControl, "E-Podręczniki", wn);
                objFoundPane.Visible = true;
                objFoundPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                objFoundPane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                objFoundPane.Width = 400;
                objFoundPane.VisibleChanged += new EventHandler(panelWomiTask_VisibleChanged);
            }

            Globals.Ribbons.Ribbon1.btWomiPanel.Checked = objFoundPane.Visible;
        }
        public void ShowErrorMessage(string strMessage, string strExceptionDetails)
        {
            ErrorDlg objDlg = new ErrorDlg(strMessage, strExceptionDetails);
            objDlg.ShowDialog();
        }

        private void ReadUserConfiguration()
        {
            try
            {
                string strPath4Settings = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "UserSettings.xml");
                if (File.Exists(strPath4Settings))
                {
                    XmlSerializer objSerializer = new XmlSerializer(typeof(PluginUserSettings));
                    object objResult = null;
                    using (XmlReader objReader = new XmlTextReader(strPath4Settings))
                    {
                        objResult = objSerializer.Deserialize(objReader);
                    }
                    m_objPluginUserSettings = (PluginUserSettings)objResult;
                }
                //ShowErrorMessage("Wystąpił problem z odczytem konfiguracji użytkownika.", "test ble ble ble");
            }
            catch(Exception ex)
            {
                ShowErrorMessage("Wystąpił problem z odczytem konfiguracji użytkownika.", ex.ToString());
            }
        }
        private void SaveUserConfiguration()
        {
            try
            {
                string strPath4Settings = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "UserSettings.xml");
                XmlSerializer objSerializer = new XmlSerializer(typeof(PluginUserSettings));
                using (XmlWriter objWriter = new XmlTextWriter(strPath4Settings, Encoding.UTF8))
                {
                    objSerializer.Serialize(objWriter, m_objPluginUserSettings);
                }
            }
            catch(IOException ioex)
            {
                int ERROR_SHARING_VIOLATION = 32;
                int ERROR_LOCK_VIOLATION = 33;

                int errorCode = Marshal.GetHRForException(ioex) & ((1 << 16) - 1);
                if(errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION)
                {
                    // plik konfiguracyjny jest właśnie zapisywany przez inną instancję Word 2013
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage("Wystąpił problem z zapisem konfiguracji użytkownika.", ex.ToString());
            }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ReadUserConfiguration();

            ((Microsoft.Office.Interop.Word.ApplicationEvents4_Event)this.Application).NewDocument += WordApplicationDocumentNew;
            this.Application.DocumentOpen += WordApplicationDocumentOpen;
            this.Application.DocumentChange += WordApplicationDocumentChange;
            this.Application.DocumentBeforeClose += WordApplicationDocumentBeforeClose;

            //this.Application.
            
            //Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            //Application.Application.

            try
            {
                // Działa dla wersji Word 2010
                CreateRightPanel(this.Application.ActiveWindow);
            }
            catch
            {
                // Nie działa dla wersji Word 2013
            }
        }
        private void WordApplicationDocumentNew(Word.Document doc)
        {
            // Działa tylko dla wersji Word 2013
            try
            {
              CreateRightPanel(doc.ActiveWindow);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Wystąpił problem w trakcie otwierania panelu E-Podręczniki.", ex.ToString());
            }
        }
        private void WordApplicationDocumentOpen(Word.Document doc)
        {
            // Działa dla wersji Word 2010 i Word 2013
            try
            {
              CreateRightPanel(doc.ActiveWindow);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Wystąpił problem w trakcie otwierania panelu E-Podręczniki.", ex.ToString());
            }
        }

        private void WordApplicationDocumentChange()
        {
            // Działa dla wersji Word 2010 i Word 2013
            try
            {
              CreateRightPanel(this.Application.ActiveWindow);

              int iInd;
              for (iInd = 0; iInd < Globals.ThisAddIn.CustomTaskPanes.Count; iInd++)
              {
                Microsoft.Office.Tools.CustomTaskPane objCustomTaskPane = Globals.ThisAddIn.CustomTaskPanes[iInd];
                if (((Word.Window)objCustomTaskPane.Window) == this.Application.ActiveWindow)
                {
                  Globals.Ribbons.Ribbon1.btWomiPanel.Checked = objCustomTaskPane.Visible;
                  break;
                }
              }

              if(iInd >= Globals.ThisAddIn.CustomTaskPanes.Count)
                Globals.Ribbons.Ribbon1.btWomiPanel.Checked = false;

            }
            catch (Exception /*ex*/)
            {
             //   ShowErrorMessage("Wystąpił problem w trakcie otwierania panelu E-Podręczniki.", ex.ToString());
            }
        }

        private void WordApplicationDocumentBeforeClose(Word.Document doc, ref bool fCancel)
        {
          try
          {
            m_arWindows.Remove((Word.Window)doc.ActiveWindow);
          }
          catch (Exception)
          {
          }
        }


        private void panelWomiTask_VisibleChanged(object sender, System.EventArgs e)
        {
            Microsoft.Office.Tools.CustomTaskPane paneWomiTask = (Microsoft.Office.Tools.CustomTaskPane)sender;
            Globals.Ribbons.Ribbon1.btWomiPanel.Checked = paneWomiTask.Visible;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            SaveUserConfiguration();

            // clean file cache
            bool fRet = true;

            while (fRet)
            {
              try
              {
                string strPath4Tmp = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "Tmp");
                if (System.IO.Directory.Exists(strPath4Tmp))
                  System.IO.Directory.Delete(strPath4Tmp, true);
                fRet = false;
              }
              catch (Exception ex)
              {
                if (MessageBox.Show(ex.Message, "Błąd czyszczenia katalogu z plikami tymczasowymi", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                  fRet = false;
              }
            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
