using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment;
using System.Net;
using System.IO;

namespace EP_WordPlugin
{
  partial class AboutBox1 : Form
  {
    WebClient m_webClient;
    string m_strPathFileVersion;

    delegate void SetTextCallback(string text);

    public AboutBox1()
    {
      Cursor test = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;

      InitializeComponent();
      this.Text = String.Format("O... {0}", AssemblyTitle);
      this.labelProductName.Text = AssemblyProduct;
      this.labelVersion.Text = String.Format("Wersja kompilacji: {0}", AssemblyVersion);
      this.labelCopyright.Text = String.Format("Wersja pobrana z sieci: {0}", AssemblyFileVersion);/*AssemblyCopyright*/;
      this.labelCompanyName.Text = AssemblyCompany;
      this.textBoxDescription.Text = AssemblyDescription;

      try
      {
        m_strPathFileVersion = String.Join("\\", System.Windows.Forms.Application.LocalUserAppDataPath, "EP_WORD_Plugin", "EP_WordPlugin.vsto");

        m_webClient = new WebClient();
        m_webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompletedFromDropbox);
        m_webClient.DownloadFileAsync(new Uri("http://av.epodreczniki.pl/VSTO/EP_WordPlugin.vsto"), m_strPathFileVersion);
      }
      catch (System.Net.WebException exWeb)
      {
        MessageBox.Show(exWeb.Message);
      }
      catch (System.NotSupportedException exNot)
      {
        MessageBox.Show(exNot.Message);
      }


      Cursor.Current = test;
    }

    private void DownloadFileCompletedFromDropbox(object sender, AsyncCompletedEventArgs e)
    {
      string szVersion = "brak danych";

      if (e.Error == null)
      {

        if (File.Exists(m_strPathFileVersion))
        {

          StreamReader r = File.OpenText(m_strPathFileVersion);
          string line;
          while ((line = r.ReadLine()) != null)
          {
            if(line.IndexOf("assemblyIdentity name=\"EP_WordPlugin.vsto\"") != -1)
            {
              int iIndex = line.IndexOf("version=");
              if (iIndex != -1)
              {
                int iIndex2 = line.IndexOf(" ", iIndex);
                if (iIndex2 != -1 && iIndex2 > iIndex)
                {
                  szVersion = line.Substring(iIndex + 8, iIndex2 - (iIndex + 8));
                  szVersion= szVersion.Replace("\"", "");
                  break;
                }
              }
            }
          }

          r.Close();

          File.Delete(m_strPathFileVersion);
        }

      }

      SetText(szVersion);    
    }

    private void SetText(string text)
    {
      // if (fClosingForm)
      // return;

      // InvokeRequired required compares the thread ID of the 
      // calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (this.laNetVersion.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(SetText);
        this.Invoke(d, new object[] { text });
      }
      else
      {
        ;//ParseOutputLine(text);
        this.laNetVersion.Text += " " + text;
      }
    }


    #region Assembly Attribute Accessors

    public string AssemblyTitle
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length > 0)
        {
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          if (titleAttribute.Title != "")
          {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public string AssemblyFileVersion
    {
      get
      {
        if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
          return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
        else
          return "Aplikacja zainstalowana lokalnie!";// (" + Assembly.GetExecutingAssembly().GetName()..ToString(); +")";
      }
    }

    public string AssemblyDescription
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public string AssemblyProduct
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public string AssemblyCompany
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }
    #endregion

  }
}
