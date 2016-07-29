using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace EP_WordPlugin
{
  public partial class SelectDirWaitForm : Form
  {
    public Process selectDirProcess = null;

    public SelectDirWaitForm()
    {
      InitializeComponent();
    }

    public void SetProcess(Process selectDirProcessTmp)
    {
      selectDirProcess = selectDirProcessTmp;
    }

    private void bnCancel_Click(object sender, EventArgs e)
    {
      try
      {
        if (selectDirProcess != null && selectDirProcess.HasExited == false)
        selectDirProcess.Kill();

        Hide();
      }
      catch (Exception e1)
      {
        MessageBox.Show(e1.Message);
      }
    }
  }
}
