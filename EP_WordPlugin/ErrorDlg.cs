using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EP_WordPlugin
{
    public partial class ErrorDlg : Form
    {
        private string m_strMessage = String.Empty;
        private string m_strDetails = String.Empty;

        public ErrorDlg(string strMessage, string strDetails)
        {
            m_strMessage = strMessage;
            m_strDetails = strDetails;
            
            InitializeComponent();
        }

        private void ErrorDlg_Load(object sender, EventArgs e)
        {
            this.textMessage.Text = m_strMessage;
            this.textDetails.Text = m_strDetails;
        }

        private void bnSendMail_Click(object sender, EventArgs e)
        {
          System.Diagnostics.Process proc = new System.Diagnostics.Process();
          proc.StartInfo.FileName = "mailto:plugin-msword@epodreczniki.pl?subject=" + this.textMessage.Text + "&body=" + this.textDetails.Text;
          proc.Start();
        }
    }
}
