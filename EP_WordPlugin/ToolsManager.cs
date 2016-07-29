using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace EP_WordPlugin
{
    public class ToolsManager
    {
        List<EP_Team> m_arobjTeam = null;
        Dictionary<string, EP_Team> m_arstrobjName2Team = null; // name -> object
        Dictionary<string, EP_Tool> m_arstrobjId2Tool = null; // id -> object

        public ToolsManager()
        {
        }

        public void AddTeam(string strName, string strStyleSetFile, string strWOMITemplate, string strWOMITemplateA, string strWOMITemplateB)
        {
            if (!String.IsNullOrEmpty(strName))
            {
                EP_Team objTeam = new EP_Team(strName, strStyleSetFile, strWOMITemplate, strWOMITemplateA, strWOMITemplateB);
                if (m_arobjTeam == null)
                {
                    m_arobjTeam = new List<EP_Team>();
                }

                m_arobjTeam.Add(objTeam);

                if(m_arstrobjName2Team == null)
                {
                    m_arstrobjName2Team = new Dictionary<string, EP_Team>();
                }

                m_arstrobjName2Team.Add(strName, objTeam);
            }
        }
        public void AddTool(string strId, string strLabel, string strToolTip, string strIcon, string strTemplate, bool fTemplateFileExists)
        {
            if(!String.IsNullOrEmpty(strLabel) && !String.IsNullOrEmpty(strIcon) && !String.IsNullOrEmpty(strTemplate))
            {
                string strPath4Icon = String.Join("\\", Globals.ThisAddIn.Path4CurrentTools, "icons", strIcon);
                Image imageIcon = null;
                using(FileStream fileIcon = new FileStream(strPath4Icon, FileMode.Open))
                {
                    imageIcon = Image.FromStream(fileIcon);
                    fileIcon.Close();
                }

                EP_Tool objTool = new EP_Tool(strId, strLabel, strToolTip, imageIcon, strTemplate, fTemplateFileExists);

                if (m_arstrobjId2Tool == null)
                {
                    m_arstrobjId2Tool = new Dictionary<string, EP_Tool>();
                }

                if (!m_arstrobjId2Tool.ContainsKey(strId))
                    m_arstrobjId2Tool.Add(strId, objTool);
            }
        }
        public void AddTool2Team(string strToolId, string strTeamName)
        {
            if (!String.IsNullOrEmpty(strToolId) && !String.IsNullOrEmpty(strTeamName) &&
                m_arstrobjName2Team != null && m_arstrobjName2Team[strTeamName] != null &&
                m_arstrobjId2Tool != null && m_arstrobjId2Tool[strToolId] != null)
            {
                EP_Team objTeam = m_arstrobjName2Team[strTeamName];
                objTeam.AddTool(strToolId);
            }
        }

        public List<EP_Team> GetTeamList()
        {
            return m_arobjTeam;
        }
        public List<EP_Tool> GetToolList4Team(string strTeamName)
        {
            if (!String.IsNullOrEmpty(strTeamName) && m_arstrobjName2Team != null && m_arstrobjName2Team[strTeamName] != null)
            {
                EP_Team objTeam = m_arstrobjName2Team[strTeamName];
                if (objTeam.ToolList != null && objTeam.ToolList.Count > 0)
                {
                    List<EP_Tool> arobjTool = new List<EP_Tool>();
                    foreach (string strToolId in objTeam.ToolList)
                    {
                        if (m_arstrobjId2Tool != null && m_arstrobjId2Tool[strToolId] != null)
                        {
                            arobjTool.Add(m_arstrobjId2Tool[strToolId]);
                        }
                    }

                    return arobjTool;
                }
            }

            return null;
        }

        public EP_Tool GetTool(string strId)
        {
            if (m_arstrobjId2Tool.ContainsKey(strId))
            {
                return m_arstrobjId2Tool[strId];
            }

            return null;
        }
    }

    public class EP_Team
    {
        string m_strName = String.Empty;
        string m_strStyleSetName = String.Empty;
        List<string> m_arstrTool = null;

        string m_strWOMITemplate = String.Empty;
        string m_strWOMITemplateA = String.Empty;
        string m_strWOMITemplateB = String.Empty;

        public string Name
        {
            get { return m_strName; }
        }
        public string StyleSet
        {
            get { return m_strStyleSetName; }
        }
        public List<string> ToolList
        {
            get { return m_arstrTool; }
        }
        public string WOMITemplate
        {
          get { return m_strWOMITemplate; }
        }
        public string WOMITemplateA
        {
          get { return m_strWOMITemplateA; }
        }
        public string WOMITemplateB
        {
          get { return m_strWOMITemplateB; }
        }

        public EP_Team(string strName, string strStyleSetName, string strWOMITemplate, string strWOMITemplateA, string strWOMITemplateB)
        {
            m_strName = strName;
            m_strStyleSetName = strStyleSetName;
            m_strWOMITemplate = strWOMITemplate;
            m_strWOMITemplateA = strWOMITemplateA;
            m_strWOMITemplateB = strWOMITemplateB;
        }

        public void AddTool(string strToolLabel)
        {
            if (String.IsNullOrEmpty(strToolLabel))
                return;

            if(m_arstrTool == null)
            {
                m_arstrTool = new List<string>();
            }

            m_arstrTool.Add(strToolLabel);
        }
    }

    public class EP_Tool
    {
        private string m_strId = String.Empty;
        private string m_strLabel = String.Empty;
        private string m_strToolTip = String.Empty;
        private Image m_imageIcon = null;
        private string m_strTemplate = String.Empty;
        private bool m_fTemplateFileExists = true;

        public string Id
        {
            get { return m_strId; }
        }
        public string Label
        {
            get { return m_strLabel; }
        }
        public string LabelLong
        {
            get
            {
                return (String.IsNullOrEmpty(m_strToolTip) ? m_strLabel : String.Format("{0} ({1})", m_strLabel, m_strToolTip));
            }
        }
        public string ToolTip
        {
            get { return m_strToolTip; }
        }
        public Image Icon
        {
            get { return m_imageIcon; }
        }
        public string Template
        {
            get { return m_strTemplate; }
        }
        public bool TemplateFileExists
        {
            get { return m_fTemplateFileExists; }
        }
        public string CommandText
        {
            get { return (m_fTemplateFileExists ? "<- Wstaw" : "Brak pliku !"); }
        }

        public EP_Tool(string strId, string strLabel, string strToolTip, Image imageIcon, string strTemplate, bool fTemplateFileExists)
        {
            m_strId = strId;
            m_strLabel = strLabel;
            m_strToolTip = strToolTip;
            m_imageIcon = imageIcon;
            m_strTemplate = strTemplate;
            m_fTemplateFileExists = fTemplateFileExists;
        }
    }

}
