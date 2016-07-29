using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Drawing;
using Microsoft.Office.Tools.Ribbon;

namespace EP_WordPlugin
{
    public partial class RibbonEP
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
          Properties.Settings.Default.Reload();
          chChnageStyle.Checked = Properties.Settings.Default.ChangeStyle;
        }

        private void btWomiPanel_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Tools.CustomTaskPane objFoundPane = null;
            foreach(Microsoft.Office.Tools.CustomTaskPane objCustomTaskPane in Globals.ThisAddIn.CustomTaskPanes)
            {
                try
                {
                    if (objCustomTaskPane.Window == Globals.ThisAddIn.Application.ActiveWindow)
                    {
                        objFoundPane = objCustomTaskPane;
                        break;
                    }
                }
                catch
                {
                    // w oknie aplikacji nie ma otwartego dokumentu
                }
            }

            if (objFoundPane != null)
            {
                objFoundPane.Visible = !objFoundPane.Visible;
            }
        }

        private void btConverterForm_Click(object sender, RibbonControlEventArgs e)
        {
          ConventerForm formCon = new ConventerForm();
          formCon.ShowDialog();
        }

        private void btConverSingleFile(object sender, RibbonControlEventArgs e)
        {
          ConverterSingleFileForm formCon = new ConverterSingleFileForm();
          formCon.ShowDialog();
        }

        private void bnAbout_Click(object sender, RibbonControlEventArgs e)
        {
          AboutBox1 formAbout = new AboutBox1();
          formAbout.ShowDialog();
        }

        private void btImportSingleFile_Click(object sender, RibbonControlEventArgs e)
        {
          ConverterSingleFileForm formCon = new ConverterSingleFileForm(true);
          formCon.ShowDialog();
        }

        private void chChnageStyle_Click(object sender, RibbonControlEventArgs e)
        {
          Properties.Settings.Default.ChangeStyle = chChnageStyle.Checked;

          Properties.Settings.Default.Save();
        }

    }
}
