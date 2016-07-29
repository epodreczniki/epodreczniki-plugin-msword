using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;


namespace EP_WordPlugin
{
  public partial class ProgressForm : Form
  {
    private WebClient m_webClient;

    public ProgressForm()
    {
      m_webClient = null;
      InitializeComponent();
    }

    private void bnCancel_Click(object sender, EventArgs e)
    {
      if (m_webClient != null)
        m_webClient.CancelAsync();
    }

    public void SetWebClient(WebClient webClient)
    {
      m_webClient = webClient;
    }

    private void ProgressForm_Load(object sender, EventArgs e)
    {

    }
  }
}
