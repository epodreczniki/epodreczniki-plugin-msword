using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using COM = System.Runtime.InteropServices.ComTypes;
using System.Reflection;
using System.Security.Cryptography;

namespace EP_WordPlugin
{
  public partial class ConverterSingleFileForm : Form
  {
    bool m_fImport = false;
    string InstallKonwerterDir;
    string ErrorBufferConverProccess;
    string ImportDirStructureXML;
//    string LastStatusLine;
    string ErrorBuffer;
    string UserName;
    string UserPass;
    bool m_fErrorDuringConversion = false;

    public bool fClosingForm = false;

    string m_szFilePathToConvert;
    string LastStatusLine;

    WebClient m_webClient;
    ProgressForm formProgress;

    delegate void SetTextCallback(string text);
    delegate void SetContinueImporting(string text);
    delegate void HideFormSelectDirWaitCallback();
    delegate void ShowFormSelectDirWaitCallback();

    public Process convertProcess = null;
    public Process upgradeProcess = null;
    public Process selectDirProcess = null;
    public Process publicationUploaderProcess = null;

    SelectDirWaitForm formSelectDirWait2;// = new SelectDirWaitForm();

    List<status_line> arStatusLines;

    public ConverterSingleFileForm(bool fImport = false)
    {
      try
      {
        m_fImport = fImport;

        InitializeComponent();
        
        formProgress = new ProgressForm();
        arStatusLines = new List<status_line>();

        if (CheckAndIsntallKonwenter())
        {
          if (m_fImport)
            Import();
          else
            RunConversion("3");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private bool CheckAndIsntallKonwenter()
    {
      // 0. stworzyć katalog >>Konwerter_OOXML<<
      InstallKonwerterDir = Environment.GetEnvironmentVariable("HOMEDRIVE") + "\\Konwerter_OOXML";
      System.IO.Directory.CreateDirectory(InstallKonwerterDir);

      if (File.Exists(InstallKonwerterDir + @"\\konwerter_ooxml.exe") && File.Exists(InstallKonwerterDir + @"\\konwerter_ooxml.cfg"))
        return true;  // wszystko powinnno już być skonfigurowane 

      //1. sprawdzenie czy jest java
      string javaPath = GetJavaInstallationPath();
      if (javaPath == "")
        MessageBox.Show("Proszę zainstalować javę ze strony  http://www.java.com/pl/download/");

      //2. pobranie pliku >>konwerter_ooxml.exe<<
      try
      {
        m_webClient = new WebClient();
        m_webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompletedFromDropbox);
        m_webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChangedFromDropbox);
//CONFIG: adres URL do pobrania pliku konwerter_ooxml.exe
        m_webClient.DownloadFileAsync(new Uri(Properties.Settings.Default.URL_KONWERTER), InstallKonwerterDir + "\\konwerter_ooxml.exe");

        formProgress.SetWebClient(m_webClient);
        formProgress.ShowDialog();

      }
      catch (System.Net.WebException exWeb)
      {
        MessageBox.Show(exWeb.Message);
      }
      catch (System.NotSupportedException exNot)
      {
        MessageBox.Show(exNot.Message);
      }

      return false;
    }

    private void DownloadProgressChangedFromDropbox(object sender, DownloadProgressChangedEventArgs e)
    {
      formProgress.progressDownloadBar.Value = e.ProgressPercentage;
    }

    private void DownloadFileCompletedFromDropbox(object sender, AsyncCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        return;
      }
      else if (e.Error != null)
      {   // trzeba sprawdzić czy da się ściągnąć inny plik
        m_webClient = new WebClient();
        m_webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompletedFromGoogle);
        m_webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChangedFromGoogle);
//CONFIG: zapasowy adres URL do pobrania pliku konwerter_ooxml.exe
        m_webClient.DownloadFileAsync(new Uri(Properties.Settings.Default.URL_KONWERTER_BACKUP), InstallKonwerterDir + "\\konwerter_ooxml.exe");

        formProgress.SetWebClient(m_webClient);
        formProgress.ShowDialog();
      }
      else
      {
        formProgress.Hide();

        SetText("Zakończono ściąganie konwertera!");

        ContinuIntallingProcess();
      }
    }

    private void DownloadProgressChangedFromGoogle(object sender, DownloadProgressChangedEventArgs e)
    {
      formProgress.progressDownloadBar.Value = e.ProgressPercentage;
    }

    private void DownloadFileCompletedFromGoogle(object sender, AsyncCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        return;
      }
      else if (e.Error != null)
      {   // trzeba sprawdzić czy da się ściągnąć inny plik
      }
      else
      {
        formProgress.Hide();

        SetText("Zakończono ściąganie konwertera!");

        ContinuIntallingProcess();
      }
    }

    private void ContinuIntallingProcess()
    {
      //3. Pobranie ścieżki do Office
      Microsoft.Office.Interop.Word.Application appOffice = new Microsoft.Office.Interop.Word.Application();
      string OfficePath = appOffice.Path;

      //4. Pobranie ścieżki do katalogu fontów
      string fontDir = Environment.GetEnvironmentVariable("WINDIR") + "\\fonts";

      //5. stworzenie i zapis pliku konfiguracyjnego
      string configFile =
      "%CONFIG=(" + Environment.NewLine +
      "#Ścieżka do katalogu z Microsoft Word." + Environment.NewLine +
      "#Jest ona niezbędna do odnalezienia pliku konwertującego OMML to MathML (OMML2MML.XSL)." + Environment.NewLine +
      "PATH_MS_WORD => '" + OfficePath + "'," + Environment.NewLine +
      "#Ścieżka do katalogu Windows z fontami" + Environment.NewLine +
      "#W prototypie niezbędna do uzyskania polskich znaków w wynikowych plikach pdf." + Environment.NewLine +
      "PATH_WINDOWS_FONTS => '" + fontDir + "'," + Environment.NewLine +
      "#Ścieżka do katalogu roboczego konwertera." + Environment.NewLine +
      "#UWAGA: Proszę używać w ścieżce jedynie niediakrytyzowanych liter, cyfr oraz znaków :\\/_-" + Environment.NewLine +
      "#UWAGA: Ścieżka nie powinna wskazywać na istniejący katalog, katalog roboczy zostanie stworzony przez konwerter" + Environment.NewLine +
      "PATH_INSTALACJA => '" + InstallKonwerterDir + "\\dane'," + Environment.NewLine +
      "#Ścieżka do Javy (należy pobrać i zainstalować wcześniej: http://www.java.com/pl/download/)." + Environment.NewLine +
      "#Java jest niezbędna do przeprowadzenia transformacji XSLT oraz wygenerowania dokumentów z podglądem." + Environment.NewLine +
      "#Jeżeli zmienna nie zostanie zdefiniowana, zostanie pobrana wartość ze zmiennych środowiskowych." + Environment.NewLine +
      "PATH_JAVA_HOME => ''," + Environment.NewLine +
      ") ";

      using (StreamWriter outfile = new StreamWriter(InstallKonwerterDir + @"\\konwerter_ooxml.cfg", false, Encoding.GetEncoding(1250)))
      {
        outfile.Write(configFile);
      }

      //6. upgrade konwerter
      UpgradeKonwenter();
    }


    private void UpgradeKonwenter()
    {
      /*lvStatus.Items.Clear();
      lvEPK.Items.Clear();
      arStatusLines.Clear();
      arStatusItems.Clear();

      bnRunConversion.Enabled = false;
      bnStopConversion.Enabled = false;
      bnImporting.Enabled = false;
      lvEPK.Enabled = false;
      bnSelectFolder.Enabled = false;
      tbDocDir.Enabled = false;*/

      // Use ProcessStartInfo class
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.CreateNoWindow = true;
      startInfo.UseShellExecute = false;
      startInfo.FileName = InstallKonwerterDir + "\\konwerter_ooxml.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = "-a";
      startInfo.WorkingDirectory = InstallKonwerterDir;
      startInfo.RedirectStandardOutput = true;
      startInfo.StandardOutputEncoding = Encoding.GetEncoding("ibm852");

      try
      {
        // Start the process with the info we specified.
        // Call WaitForExit and then the using statement will close.
        upgradeProcess = Process.Start(startInfo);
        upgradeProcess.EnableRaisingEvents = true;
        upgradeProcess.Exited += new EventHandler(upgradeProcess_Exited);
        upgradeProcess.OutputDataReceived += new DataReceivedEventHandler(OutputUpgradeProcessHandler);
        upgradeProcess.BeginOutputReadLine();

      }
      catch (Exception e1)
      {
        MessageBox.Show(e1.Message);
      }

    }

    private void RunConversion(string szPublicationID)
    {
      /*fStopClicked = false;
      bnRunConversion.Enabled = false;
      bnImporting.Enabled = false;
      bnSelectFolder.Enabled = false;
      tbDocDir.Enabled = false;

      lvEPK.Items.Clear();
      arStatusLines.Clear();
      arStatusItems.Clear();
      llOutput.Text = "";*/

      lvStatus.Items.Clear();
      ErrorBufferConverProccess = "";

      // 0. stworzyć katalog dla tymczasowych dokumentow
      string strPath4Tmp = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "Tmp");
      System.IO.Directory.CreateDirectory(strPath4Tmp);

      String docxDirName = DateTime.Now.ToString();
      docxDirName = docxDirName.Replace(":", "_");

      String strDocxFullName = strPath4Tmp + "\\" + docxDirName;

      System.IO.Directory.CreateDirectory(strDocxFullName);


      m_szFilePathToConvert = strDocxFullName + "\\" + Globals.ThisAddIn.Application.ActiveDocument.Name; 
      COM.IPersistFile pp = (COM.IPersistFile)Globals.ThisAddIn.Application.ActiveDocument;
      pp.Save( m_szFilePathToConvert, false);

      if (m_szFilePathToConvert.EndsWith(".docx") == false && m_szFilePathToConvert.EndsWith(".docm") == false)
        m_szFilePathToConvert += ".docx"; 

      // Use ProcessStartInfo class
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.CreateNoWindow = true;
      startInfo.UseShellExecute = false;
      startInfo.FileName = InstallKonwerterDir + "\\konwerter_ooxml.exe";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      if(m_fImport)
        startInfo.Arguments = "-k \"" + strDocxFullName + "\" -publikacja nopdf=1 -publikacja publication.destination.directoryId=" + szPublicationID;
      else
        startInfo.Arguments = "-k \"" + strDocxFullName + "\" -publikacja publication.destination.directoryId=" + szPublicationID;

      startInfo.WorkingDirectory = InstallKonwerterDir;
      startInfo.RedirectStandardOutput = true;
      startInfo.RedirectStandardError = true;
      startInfo.StandardOutputEncoding = Encoding.GetEncoding("ibm852");

      try
      {
        // Start the process with the info we specified.
        // Call WaitForExit and then the using statement will close.
        convertProcess = Process.Start(startInfo);
        convertProcess.EnableRaisingEvents = true;
        convertProcess.Exited += new EventHandler(convertProcess_Exited);

        convertProcess.OutputDataReceived += new DataReceivedEventHandler(OutputConverProcessHandler);
        convertProcess.ErrorDataReceived += new DataReceivedEventHandler(ErrorConverProcessHandler);

        convertProcess.BeginOutputReadLine();
        convertProcess.BeginErrorReadLine();
      }
      catch (Exception e1)
      {
        MessageBox.Show(e1.Message);
      }

    }

    private void convertProcess_Exited(object sender, System.EventArgs e)
    {
      convertProcess.WaitForExit();

      if (fClosingForm)
        return;

      if (ErrorBufferConverProccess != "")
      {
        MessageBox.Show(ErrorBufferConverProccess);

        return;
      }

      if (m_fImport == false || m_fErrorDuringConversion == true)
      {
        string szFile = m_szFilePathToConvert;

        if (szFile.EndsWith("docm"))
          szFile = szFile.Substring(0, szFile.LastIndexOf('.')) + "_m.pdf";
        else if (szFile.EndsWith("docx"))
          szFile = szFile.Substring(0, szFile.LastIndexOf('.')) + "_x.pdf";

        if (File.Exists(szFile))
          Process.Start(szFile);
      }
      else
        bnImporting_ClickContinueRest();

    }

    private void OutputConverProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null)
        SetText(outLine.Data);
    }

    private void ErrorConverProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null)
        ErrorBufferConverProccess += outLine.Data + Environment.NewLine;
    }

    private void SetText(string text)
    {
     // if (fClosingForm)
       // return;

      // InvokeRequired required compares the thread ID of the 
      // calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (this.laStatus.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(SetText);
        this.Invoke(d, new object[] { text });
      }
      else
      {
        ParseOutputLine(text);
      }
    }

    private void upgradeProcess_Exited(object sender, System.EventArgs e)
    {
      upgradeProcess.WaitForExit();

      RunConversion("3");
      //EnableButton();
    }

    private void OutputUpgradeProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null)
        SetText(outLine.Data);
    }

    private void Import()
    {
      ImportDirStructureXML = "";
      ErrorBuffer = "";

      //Get the assembly informationSystem.Reflection.Assembly
      Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();

      //Location is where the assembly is run from 
      string assemblyLocation = assemblyInfo.Location;

      //CodeBase is the location of the ClickOnce deployment files
      Uri uriCodeBase = new Uri(assemblyInfo.CodeBase);
      string ClickOnceLocation = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString());

      Properties.Settings.Default.Reload();

      UserName = Properties.Settings.Default.UserName;
      if (Properties.Settings.Default.UserPass != "")
      {
        string EntropyTmp = Properties.Settings.Default.Entropy;
        byte[] entropy = new byte[20];
        for (int i = 0; i < 20; i++)
          entropy[i] = (byte)EntropyTmp[i];

        string UserPassTmp = Properties.Settings.Default.UserPass;
        byte[] ciphertext = new byte[UserPassTmp.Length];
        for (int i = 0; i < UserPassTmp.Length; i++)
          ciphertext[i] = (byte)UserPassTmp[i];

        //entropy = Encoding.UTF8.GetBytes(EntropyTmp, 0, 20);
        //byte[] ciphertext = Encoding.UTF8.GetBytes(UserPassTmp);

        byte[] plaintext = ProtectedData.Unprotect(ciphertext, entropy, DataProtectionScope.CurrentUser);
        UserPass = Encoding.UTF8.GetString(plaintext);
      }
      else
        UserPass = "";

      LoginDirectoryRetrieverForm dlgLoginForm = new LoginDirectoryRetrieverForm(UserName, UserPass, Properties.Settings.Default.SaveUserAndPass);
      if (dlgLoginForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        UserName = dlgLoginForm.UserName;
        UserPass = dlgLoginForm.UserPass;

        if (dlgLoginForm.SaveUserAndPassword)
        {
          // Data to protect. Convert a string to a byte[] using Encoding.UTF8.GetBytes().
          byte[] plaintext;
          plaintext = Encoding.UTF8.GetBytes(UserPass);

          // Generate additional entropy (will be used as the Initialization vector)
          byte[] entropy = new byte[20];
          using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
          {
            rng.GetBytes(entropy);
          }

          byte[] ciphertext = ProtectedData.Protect(plaintext, entropy, DataProtectionScope.CurrentUser);

          //string szEntropy = System.Text.Encoding.UTF8.GetString(entropy);
          string szEntropy = "";
          for (int i = 0; i < 20; i++)
            szEntropy += (char)entropy[i];
          Properties.Settings.Default.Entropy = szEntropy;

          string szCipher = "";
          for (int i = 0; i < ciphertext.Length; i++)
            szCipher += (char)ciphertext[i];
          Properties.Settings.Default.UserPass = szCipher;

          Properties.Settings.Default.UserName = UserName;
        }
        else
        {
          Properties.Settings.Default.Entropy = "";
          Properties.Settings.Default.UserName = "";
          Properties.Settings.Default.UserPass = "";
        }

        Properties.Settings.Default.SaveUserAndPass = dlgLoginForm.SaveUserAndPassword;
        Properties.Settings.Default.Save();

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        startInfo.FileName = GetJavaInstallationPath() + "\\bin\\java"; //ClickOnceLocation + "\\Apps\\DirectoryRetriever\\retriever.bat";//InstallKonwerterDir + "\\directory_retriever\\retriever.bat";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //startInfo.Arguments = "-cp lib\\directory-retriever.jar;lib\\dcore-common-base.jar;lib\\dcore-server-ms.jar;lib\\dlteam-fwork-services-common.jar;lib\\dlteam-fwork-services-ss.jar;lib\\log4j-1.2.17.jar;lib\\commons-lang-2.1.jar pl.psnc.dlibra.DirectoryRetriever " + UserName + " " + UserPass;
        startInfo.Arguments = "-cp lib/ep-rt-tools.jar;lib/dlibra-common-base.jar;lib/dlibra-app-base.jar;lib/dlibra-app-util.jar;lib/dlibra-app-extension-api.jar;lib/dlibra-common-utils.jar;lib/dlibra-common-services.jar;lib/ep-rt-app-extension-mf-xml.jar;lib/ep-rt-common.jar;lib/log4j.jar;lib/jpf.jar;lib/jsap.jar;lib/velocity.jar;lib/commons-lang.jar;lib/commons-logging.jar;lib/commons-collections.jar;lib/httpclient.jar;lib/httpcore.jar pl.psnc.ep.rt.tools.DirectoryRetriever " + UserName + " " + UserPass;
        startInfo.WorkingDirectory = ClickOnceLocation + "\\Apps\\DirRetrieverAndPubUploader";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.StandardOutputEncoding = Encoding.GetEncoding("utf-8");
        //startInfo.StandardOutputEncoding = Encoding.GetEncoding(1250);

        try
        {
          // Start the process with the info we specified.
          // Call WaitForExit and then the using statement will close.
          selectDirProcess = Process.Start(startInfo);
          selectDirProcess.EnableRaisingEvents = true;
          selectDirProcess.Exited += new EventHandler(selectDirProcess_Exited);
          selectDirProcess.OutputDataReceived += new DataReceivedEventHandler(OutputSelectDirProcessHandler);
          selectDirProcess.ErrorDataReceived += new DataReceivedEventHandler(ErrorSelectDirProcessHandler);
          selectDirProcess.BeginOutputReadLine();
          selectDirProcess.BeginErrorReadLine();

          formSelectDirWait2 = new SelectDirWaitForm();
          formSelectDirWait2.SetProcess(selectDirProcess);
          if (formSelectDirWait2.ShowDialog() == DialogResult.OK)
          {
            if (ImportDirStructureXML != null && ImportDirStructureXML != "")
              bnImporting_ClickContinue(ImportDirStructureXML);
          }
          else
            Close();
        }
        catch (Exception e1)
        {
          formSelectDirWait2.Hide();
          MessageBox.Show(e1.Message);
        }
      }
      else
        Close();
    }

    private void selectDirProcess_Exited(object sender, System.EventArgs e)
    {
      try
      {
        selectDirProcess.WaitForExit();

        if ((ImportDirStructureXML != null && ImportDirStructureXML != ""))
          formSelectDirWait2.DialogResult = DialogResult.OK;
        else
          formSelectDirWait2.DialogResult = DialogResult.Cancel;

        if (ErrorBuffer != "")
          MessageBox.Show(ErrorBuffer);
      }
      catch (Exception e1)
      {
        MessageBox.Show(e1.Message);
      }

    }

    private void bnImporting_ClickContinue(string text)
    {
      if (fClosingForm)
      {
        Close();
        return;
      }

      // InvokeRequired required compares the thread ID of the 
      // calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      /*if (this.formSelectDirWait2.InvokeRequired)
      {
        SetContinueImporting d = new SetContinueImporting(bnImporting_ClickContinue);
        this.Invoke(d, new object[] { text });
      }
      else
      {*/
        //formSelectDirWait2.Hide();
        //HideFormSelectDirWait();

        if (text.StartsWith("Usage:"))
        {
          Close();
          return;
        }

        string szSelDir = Properties.Settings.Default.SelectedDirectory;
        SelectDirForm dlgSelectDir = new SelectDirForm(text, szSelDir);
        if (dlgSelectDir.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
        {
          Properties.Settings.Default.SelectedDirectory = dlgSelectDir.szSelectedID;
          Properties.Settings.Default.Save();

          RunConversion(dlgSelectDir.szSelectedID);
        }
        else
          Close();
      //}
    }

/*
    private void HideFormSelectDirWait()
    {
      if (fClosingForm)
        return;

      // InvokeRequired required compares the thread ID of the 
      // calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (formSelectDirWait2.InvokeRequired)
      {
        HideFormSelectDirWaitCallback d = new HideFormSelectDirWaitCallback(HideFormSelectDirWait);
        this.Invoke(d, new object[] { });
      }
      else
      {
        formSelectDirWait2.Hide();
      }
    }

    private void ShowFormSelectDirWait()
    {
      if (fClosingForm)
        return;

      // InvokeRequired required compares the thread ID of the 
      // calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (this.laStatus.InvokeRequired)
      {
        ShowFormSelectDirWaitCallback d = new ShowFormSelectDirWaitCallback(ShowFormSelectDirWait);
        this.Invoke(d, new object[] { });
      }
      else
      {
        ;// formSelectDirWait2.ShowDialog();
      }
    }
*/
    private void OutputSelectDirProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null)
      {
        ImportDirStructureXML += outLine.Data;
      }
    }

    private void ErrorSelectDirProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null && outLine.Data != "")
        ErrorBuffer += outLine.Data + Environment.NewLine;
    }

    private void bnImporting_ClickContinueRest()
    {
      ErrorBuffer = "";
      string szPublicationPropertiesPath = GetPublicationPropertiesPath();

      //Get the assembly informationSystem.Reflection.Assembly
      Assembly assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();

      //Location is where the assembly is run from 
      string assemblyLocation = assemblyInfo.Location;


      //CodeBase is the location of the ClickOnce deployment files
      Uri uriCodeBase = new Uri(assemblyInfo.CodeBase);
      string ClickOnceLocation = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString());


      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.CreateNoWindow = true;
      startInfo.UseShellExecute = false;
      startInfo.FileName = GetJavaInstallationPath() + "\\bin\\java"; //ClickOnceLocation + "\\Apps\\DirectoryRetriever\\retriever.bat";//InstallKonwerterDir + "\\directory_retriever\\retriever.bat";
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      //"-cp lib/cmdln-publication-uploader-1.0.0.jar;lib/dcore-common-base-1.2.1-SNAPSHOT.jar;lib/dcore-app-base-1.2.1-SNAPSHOT.jar;lib/dcore-app-util-1.2.1-SNAPSHOT.jar;lib/dcore-app-extension-api-1.2.1-SNAPSHOT.jar;lib/ep-rt-app-extension-mf-xml-1.3.0.jar;lib/dcore-server-ms-1.2.1-SNAPSHOT.jar;lib/ep-rt-common-0.1.jar;lib/dlteam-fwork-services-common-1.0.6.jar;lib/dlteam-fwork-services-ss-1.0.6.jar;lib/dlteam-tools-util-1.0.4.jar;lib/log4j-1.2.17.jar;lib/commons-lang-2.1.jar;lib/commons-logging-1.1.jar;lib/commons-collections-3.2.1.jar;lib/velocity-dep-1.4.jar;lib/jpf-0.12.jar pl.psnc.dlibra.PublicationUploader "
      //startInfo.Arguments = "-cp lib/cmdln-publication-uploader.jar;lib/dcore-common-base.jar;lib/dcore-app-base.jar;lib/dcore-app-util.jar;lib/dcore-app-extension-api.jar;lib/ep-rt-app-extension-mf-xml-1.3.0.jar;lib/dcore-server-ms.jar;lib/ep-rt-common-0.1.jar;lib/dlteam-fwork-services-common.jar;lib/dlteam-fwork-services-ss.jar;lib/dlteam-tools-util-1.0.4.jar;lib/log4j-1.2.17.jar;lib/commons-lang-2.1.jar;lib/commons-logging-1.1.jar;lib/commons-collections-3.2.1.jar;lib/velocity-dep-1.4.jar;lib/jpf-0.12.jar pl.psnc.dlibra.PublicationUploader " + UserName + " " + UserPass + " \"" + szPublicationPropertiesPath + "\"";

      startInfo.Arguments = "-cp lib/ep-rt-tools.jar;lib/dlibra-common-base.jar;lib/dlibra-app-base.jar;lib/dlibra-app-util.jar;lib/dlibra-app-extension-api.jar;lib/dlibra-common-utils.jar;lib/dlibra-common-services.jar;lib/ep-rt-app-extension-mf-xml.jar;lib/ep-rt-common.jar;lib/log4j.jar;lib/jpf.jar;lib/jsap.jar;lib/velocity.jar;lib/commons-lang.jar;lib/commons-logging.jar;lib/commons-collections.jar;lib/httpclient.jar;lib/httpcore.jar pl.psnc.ep.rt.tools.PublicationUploader " + UserName + " " + UserPass + " \"" + szPublicationPropertiesPath + "\" temp=true";
      startInfo.WorkingDirectory = ClickOnceLocation + "\\Apps\\DirRetrieverAndPubUploader";
      startInfo.RedirectStandardOutput = true;
      startInfo.RedirectStandardError = true;
      startInfo.StandardOutputEncoding = Encoding.GetEncoding("utf-8");

      try
      {
        // Start the process with the info we specified.
        // Call WaitForExit and then the using statement will close.
        publicationUploaderProcess = Process.Start(startInfo);
        publicationUploaderProcess.EnableRaisingEvents = true;
        publicationUploaderProcess.Exited += new EventHandler(PublicationUploaderProcess_Exited);
        publicationUploaderProcess.OutputDataReceived += new DataReceivedEventHandler(OutputPublicationUploaderProcessHandler);
        publicationUploaderProcess.ErrorDataReceived += new DataReceivedEventHandler(ErrorPublicationUploaderProcessHandler);
        publicationUploaderProcess.BeginOutputReadLine();
        publicationUploaderProcess.BeginErrorReadLine();


      }
      catch (Exception e1)
      {

        MessageBox.Show(e1.Message);
      }
    }

    private void OutputPublicationUploaderProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null)
      {
        LastStatusLine = outLine.Data;
        SetText(outLine.Data);
      }
    }

    private void ErrorPublicationUploaderProcessHandler(object sendingProcess,
                DataReceivedEventArgs outLine)
    {
      if (outLine.Data != null && outLine.Data != "")
      {
        ErrorBuffer += outLine.Data + Environment.NewLine;
      }
    }


    private void PublicationUploaderProcess_Exited(object sender, System.EventArgs e)
    {
      try
      {
        publicationUploaderProcess.WaitForExit();

        //EnableButton();

        int iIndex = -1;
        if (LastStatusLine != "")
          iIndex = Convert.ToInt32(LastStatusLine);

        if (iIndex != -1 && iIndex != 0)
        {
//CONFIG: adres Otwartego Repozytorium Treści
          string szLink = Properties.Settings.Default.URL_OTWARTE_REPOZYTORIUM_TRESCI + "/repo/rt/publication/" + iIndex.ToString() + "/content";
          DateTime time = DateTime.Now;

          //SetText(szLink);
          Process.Start(szLink);

          string strPath4HistoryLog = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "history.log");
          TextWriter w = File.AppendText(strPath4HistoryLog);
          w.WriteLine("{0}||{1}||{2}", szLink, "Katalog tymczasowy", time.ToString("s"));
          w.Close();

          foreach (Microsoft.Office.Tools.CustomTaskPane objCustomTaskPane in Globals.ThisAddIn.CustomTaskPanes)
          {
            ((EPWomiCatalogControl)objCustomTaskPane.Control).lv_History_AddItem(szLink, "Katalog tymczasowy", time);
          }
        }
        if (ErrorBuffer != "")
          MessageBox.Show(ErrorBuffer);
        
        //else
          //MessageBox.Show("Importowanie zakończone!");
      }
      catch (FormatException /*e1*/)
      {
        MessageBox.Show("Proces importowania nie zakończył się pomyślnie!");
      }

    }



    void ParseOutputLine(string text)
    {
      bool fStatus = text.StartsWith("EPK_STATUS_");
      bool fLog = false;
      if (fStatus == false)
      {
        fLog = text.StartsWith("EPK_LOG");

        if (fLog == false)
        { // wynik nie z konwertera, a z innego programu
          status_line stLine = new status_line(text, Status.INFO);
          arStatusLines.Add(stLine);
          ListViewItem lviItem = lvStatus.Items.Add(stLine.message);
          lviItem.BackColor = conver_status_to_color(stLine.status);
          lvStatus.EnsureVisible(lviItem.Index);
        }
      }
      else // add status line
      {
        status_line stLine = new status_line(text.Substring(text.IndexOf(";;;") + ";;;".Length), conver_string_to_status(text.Substring("EPK_STATUS_".Length, text.IndexOf(";;;") - "EPK_STATUS_".Length)));
        arStatusLines.Add(stLine);
        ListViewItem lviItem = lvStatus.Items.Add(stLine.message);
        lviItem.BackColor = conver_status_to_color(stLine.status);
        lvStatus.EnsureVisible(lviItem.Index);

        if (stLine.status == Status.ERROR || stLine.status == Status.FATAL)
          m_fErrorDuringConversion = true;
      }

      if (fLog)
      {
        string[] words = { ";;;" };
        string[] entries = text.Split(words, StringSplitOptions.RemoveEmptyEntries);
        string szFileName = entries[1];
        string szDirPath = entries[2];
        Status eStatus = conver_string_to_status(entries[3].Substring("EPK_STATUS_".Length));
        string szMessage = entries[4];

        /*int i;
        for (i = 0; i < lvEPK.Items.Count; i++)
        {
          if (szFileName == lvEPK.Items[i].Text && szDirPath == lvEPK.Items[i].SubItems[1].Text)
          {
            lvEPK.Items[i].SubItems[2].Text = entries[3].Substring("EPK_STATUS_".Length);
            lvEPK.Items[i].SubItems[3].Text = szMessage;
            lvEPK.Items[i].BackColor = conver_status_to_color(eStatus);
            status_line stLine = new status_line(entries[3].Substring("EPK_STATUS_".Length), eStatus);

            arStatusLines.Add(stLine);
            ListViewItem lviItem = lvStatus.Items.Add(stLine.message);
            lviItem.BackColor = conver_status_to_color(stLine.status);
            lvStatus.EnsureVisible(lviItem.Index);
            
            break;
          }
        }
        if (i >= lvEPK.Items.Count)
        {*/
          /*ListViewItem lviItem = lvEPK.Items.Add(szFileName);
          lviItem.SubItems.Add(szDirPath);
          lviItem.SubItems.Add(entries[3].Substring("EPK_STATUS_".Length));
          lviItem.BackColor = conver_status_to_color(eStatus);
        */
        status_line stLine = new status_line(szMessage /*entries[3].Substring("EPK_STATUS_".Length)*/, eStatus);

          arStatusLines.Add(stLine);
          ListViewItem lviItem = lvStatus.Items.Add(stLine.message);
          lviItem.BackColor = conver_status_to_color(stLine.status);
          lvStatus.EnsureVisible(lviItem.Index);

          if (stLine.status == Status.ERROR || stLine.status == Status.FATAL)
            m_fErrorDuringConversion = true;
          
          
          //arStatusItems.Add(stLine);

          /*lviItem.SubItems.Add(szMessage);
          lviItem.Tag = stLine;

          if (stLine.status == Status.DONE)
            lviItem.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline);*/
        //}

      }

      /*if (convertProcess != null)
      {
        bnRunConversion.Enabled = convertProcess.HasExited;
      }*/
    }


    private string GetJavaInstallationPath()
    {
      try
      {
        string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
        if (!string.IsNullOrEmpty(environmentPath))
        {
          return environmentPath;
        }

        string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
        using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
        {
          string currentVersion = rk.GetValue("CurrentVersion").ToString();
          using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
          {
            return key.GetValue("JavaHome").ToString();
          }
        }
      }
      catch (Exception /*ex*/)
      {
        MessageBox.Show("Proszę zainstalować Javę");
      }

      return "";
    }

    private string GetPublicationPropertiesPath()
    {
      string szPublicationPath = m_szFilePathToConvert;
      //string szBookTitle = szPublicationPath.Substring(szPublicationPath.LastIndexOf("\\") + 1);
      szPublicationPath = szPublicationPath.Substring(0, szPublicationPath.LastIndexOf("\\") + 1);
      szPublicationPath += "_ep_import";
      return szPublicationPath;
    }

    private Status conver_string_to_status(string szMessage)
    {
      if (szMessage == "ERROR")
        return Status.ERROR;
      else if (szMessage == "INFO")
        return Status.INFO;
      else if (szMessage == "WARNING")
        return Status.WARN;
      else if (szMessage == "FATAL")
        return Status.FATAL;
      else if (szMessage == "DONE")
        return Status.DONE;

      return Status.UNKNOWN;
    }

    private Color conver_status_to_color(Status status)
    {
      switch (status)
      {
        case Status.ERROR:
          return Color.Red;
        case Status.FATAL:
          return Color.Purple;
        case Status.INFO:
          return Color.White;
        case Status.UNKNOWN:
          return Color.Orange;
        case Status.WARN:
          return Color.Yellow;
        case Status.DONE:
          return Color.Green;
      }

      return Color.Green;
    }

    private void bnClose_Click(object sender, EventArgs e)
    {
      if (convertProcess != null && convertProcess.HasExited == false)
      {
        convertProcess.Kill();
      }

      if (upgradeProcess != null && upgradeProcess.HasExited == false)
      {
        upgradeProcess.Kill();
      }

      if (publicationUploaderProcess != null && publicationUploaderProcess.HasExited == false)
      {
        publicationUploaderProcess.Kill();
      }

      Close();
    }

    private void OnClosingForm(object sender, FormClosingEventArgs e)
    {
      fClosingForm = true;
    }

  }
}
