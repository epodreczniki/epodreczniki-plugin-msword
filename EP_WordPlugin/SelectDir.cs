using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace EP_WordPlugin
{
  public partial class SelectDirForm : Form
  {
    public string szSelectedID;
    TreeNode tnSelected = null;
    public SelectDirForm(string szBuffer, string szStoredID)
    {
      InitializeComponent();

      try
      {
        using (XmlReader reader = XmlReader.Create(new StringReader(szBuffer) /*StreamReader(szFile)*/))
        {
          reader.Read();
          reader.ReadStartElement("directory");
          TreeNode tn = new TreeNode("/");
          tvDirs.Nodes.Add(tn);
          ReadDirectoryXML(reader, tn, szStoredID);
          reader.ReadEndElement();
        }

        tvDirs.Sort();

        if(tnSelected != null)
        {
          tnSelected.EnsureVisible();
          tvDirs.SelectedNode = tnSelected; 
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void ReadDirectoryXML(XmlReader reader, TreeNode tn, string szSelID)
    {
      reader.ReadStartElement("id");
      string szID = reader.ReadString();
      tn.Tag = szID;
      reader.ReadEndElement();

      if (szID == szSelID)
        tnSelected = tn;

      reader.ReadStartElement("name");
      string szName = reader.ReadString();
      tn.Text = szName;
      reader.ReadEndElement();

      if (reader.IsEmptyElement)
      {
        reader.ReadStartElement("directories");
        return;
      }
      reader.ReadStartElement("directories");

      bool fSubFolder;
      do
      {
        fSubFolder = reader.Name == "directory";
        if (fSubFolder)
        {
          reader.ReadStartElement("directory");
          TreeNode tnChild = new TreeNode("tmp");
          tn.Nodes.Add(tnChild);
          ReadDirectoryXML(reader, tnChild, szSelID);
          reader.ReadEndElement();
        }

      } while (fSubFolder);

      reader.ReadEndElement();
    }

    private void bnOK_Click(object sender, EventArgs e)
    {
      //MessageBox.Show("Ustawiam Tag");
      szSelectedID = tvDirs.SelectedNode.Tag.ToString();
    }
  }
}
