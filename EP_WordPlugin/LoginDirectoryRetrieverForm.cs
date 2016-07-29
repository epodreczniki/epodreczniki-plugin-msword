using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EP_WordPlugin
{
  public partial class LoginDirectoryRetrieverForm : Form
  {
    public string UserName;
    public string UserPass;
    public bool SaveUserAndPassword;

    public LoginDirectoryRetrieverForm(string UserName, string UserPass, bool SaveUserAndPass)
    {
      InitializeComponent();
      tbUserName.Text = UserName;
      tbUserPassword.Text = UserPass;
      chSaveUserAndPass.Checked = SaveUserAndPass;
    }

    private void bnOK_Click(object sender, EventArgs e)
    {
      UserName = tbUserName.Text;
      UserPass = tbUserPassword.Text;
      SaveUserAndPassword = chSaveUserAndPass.Checked;
    }
  }
}
