using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Threading.Tasks;

namespace EP_WordPlugin
{
    public class WomiManager
    {
        private List<WomiFolder> m_arobjWomiFolders = null;

        public WomiManager()
        {            
        }

        public void AddWomiFolder(XmlNode nodeFolder)
        {
            if (nodeFolder != null)
            {
                if (m_arobjWomiFolders == null)
                    m_arobjWomiFolders = new List<WomiFolder>();

                XmlNode nodeParam = null;
                XmlNodeList nodelistSubfolders = null;
                int iId = -1;
                string strName = String.Empty;
                int iWomiCount = 0;

                nodeParam = nodeFolder.SelectSingleNode("id");
                if (nodeParam != null)
                {
                    if (Int32.TryParse(nodeParam.InnerText, out iId))
                    {
                        nodeParam = nodeFolder.SelectSingleNode("name");
                        if (nodeParam != null)
                        {
                            strName = nodeParam.InnerText;

                            nodeParam = nodeParam.SelectSingleNode("womi-count");
                            if (nodeParam != null)
                            {
                                Int32.TryParse(nodeParam.InnerText, out iWomiCount);
                            }

                            WomiFolder objWF = new WomiFolder(iId, strName, iWomiCount);
                            m_arobjWomiFolders.Add(objWF);

                            nodelistSubfolders = nodeFolder.SelectNodes("subfolders");
                            if (nodelistSubfolders != null && nodelistSubfolders.Count > 0)
                            {
                                objWF.AddSubfolders(nodelistSubfolders);
                            }
                        }
                    }
                }
            }
        }
        public List<WomiFolderItem> GetWomiFolderList(bool fIncludeSubfolders)
        {
            if (m_arobjWomiFolders != null && m_arobjWomiFolders.Count > 0)
            {
                m_arobjWomiFolders.Sort(new WomiFolderComparer());
                
                List<WomiFolderItem> arobjReturn = new List<WomiFolderItem>();

                foreach(WomiFolder objWF in m_arobjWomiFolders)
                {
                    string strFolderName = 
                        ((objWF.WomiCount > 0) ? String.Format("{0} ({1})", objWF.FolderName, objWF.WomiCount) : objWF.FolderName);
                    WomiFolderItem objWFI = new WomiFolderItem(objWF.FolderId, strFolderName);
                    arobjReturn.Add(objWFI);

                    if (fIncludeSubfolders)
                    {
                        List<WomiFolderItem> arobjWFI = objWF.GetSubfolderItems("    ");
                        if (arobjWFI != null && arobjWFI.Count > 0)
                        {
                            arobjReturn.AddRange(arobjWFI);
                        }
                    }
                }

                return arobjReturn;
            }

            return null;
        }
        public WomiFolder GetMainWomiFolder(int iFolderId)
        {
            foreach(WomiFolder objItem in m_arobjWomiFolders)
            {
                if(objItem.FolderId == iFolderId)
                {
                    return objItem;
                }
            }
            
            return null;
        }

        public string GetInfo()
        {
            return "Info";
        }
    }

    public class WomiFolder
    {
        private int m_iId = -1;
        private string m_strFolderName = String.Empty;
        private int m_iWomiCount = 0;
        private List<WomiFolder> m_arobjWF = null;

        public int FolderId
        {
            get { return m_iId; }
        }
        public string FolderName
        {
            get { return m_strFolderName; }
        }
        public int WomiCount
        {
            get { return m_iWomiCount; }
        }

        public WomiFolder(int iId, string strName, int iWomiCount)
        {
            m_iId = iId;
            m_strFolderName = strName;
            m_iWomiCount = iWomiCount;
        }

        public void AddSubfolders(XmlNodeList xmlSubfolders)
        {
            if (xmlSubfolders != null && xmlSubfolders.Count > 0)
            {
                m_arobjWF = new List<WomiFolder>();

                XmlNode nodeParam = null;
                XmlNodeList nodelistSubfolders = null;
                int iId = -1;
                string strName = String.Empty;
                int iWomiCount = 0;

                foreach (XmlNode xmlFolder in xmlSubfolders)
                {
                    nodeParam = xmlFolder.SelectSingleNode("id");
                    if (nodeParam != null && Int32.TryParse(nodeParam.InnerText, out iId))
                    {
                        nodeParam = xmlFolder.SelectSingleNode("name");
                        if (nodeParam != null)
                        {
                            strName = nodeParam.InnerText;

                            nodeParam = xmlFolder.SelectSingleNode("womi-count");
                            if (nodeParam != null)
                            {
                                Int32.TryParse(nodeParam.InnerText, out iWomiCount);
                            }

                            WomiFolder objWF = new WomiFolder(iId, strName, iWomiCount);
                            m_arobjWF.Add(objWF);

                            nodelistSubfolders = xmlFolder.SelectNodes("subfolders");
                            if (nodelistSubfolders != null && nodelistSubfolders.Count > 0)
                            {
                                objWF.AddSubfolders(nodelistSubfolders);
                            }
                        }
                    }
                }

                m_arobjWF.Sort(new WomiFolderComparer());
            }
        }
        public List<WomiFolderItem> GetSubfolderItems(string strPrefix)
        {
            if (m_arobjWF != null && m_arobjWF.Count > 0)
            {
                List<WomiFolderItem> arobjReturn = new List<WomiFolderItem>();

                foreach (WomiFolder objWF in m_arobjWF)
                {
                    string strFolderName = ((objWF.WomiCount > 0) ? 
                        String.Format("{0}{1} ({2})", strPrefix, objWF.FolderName, objWF.WomiCount) : 
                        String.Format("{0}{1}", strPrefix, objWF.FolderName));

                    WomiFolderItem objWFI = new WomiFolderItem(objWF.FolderId, strFolderName);
                    arobjReturn.Add(objWFI);

                    List<WomiFolderItem> arobjWFI = objWF.GetSubfolderItems(String.Format("    {0}", strPrefix));
                    if (arobjWFI != null && arobjWFI.Count > 0)
                    {
                        arobjReturn.AddRange(arobjWFI);
                    }
                }

                return arobjReturn;
            }

            return null;
        }
    }
    public class WomiFolderItem
    {
        private int m_iFolderId = -1;
        private string m_strFolderName = String.Empty;
 
        public int FolderId
        {
            get { return m_iFolderId; }
        }
        public string FolderName
        {
            get { return m_strFolderName; }
        }

        public WomiFolderItem(int iFolderId, string strFolderName)
        {
            m_iFolderId = iFolderId;
            m_strFolderName = strFolderName;
        }
    }

    public class WomiItem
    {
        private int m_iId = -1;
        private string m_strMediaType = String.Empty;
        private string m_strTitle = String.Empty;
        private string m_strAuthor = String.Empty;
        private string m_strKeywords = String.Empty;
        private string m_strName = String.Empty;
        private Image m_imageIcon = null;

        public int Id
        {
            get { return m_iId; }
        }
        public string MediaType
        {
            get { return m_strMediaType; }
        }
        public string Title
        {
            get { return m_strTitle; }
        }
        public string Name
        {
          get { return m_strName; }
        }
        public string TitleLong
        {
            get
            {
                return (String.IsNullOrEmpty(m_strAuthor) ? m_strTitle : String.Format("{0} ({1})", m_strTitle, m_strAuthor));
            }
        }
        public string Author
        {
            get { return m_strAuthor; }
        }
        public string Keywords
        {
            get { return m_strKeywords; }
        }
        public Image Icon
        {
            get { return m_imageIcon; }
        }

        public string CommandText
        {
            get { return "<- Wstaw"; }
        }

        public string CommandBrowserText
        {
            get { return "Info"; }
        }

        public WomiItem(int iId, string strMediaType, string strTitle, string strAuthor, string strKeywords, string strName)
        {
            m_iId = iId;
            m_strMediaType = strMediaType;
            m_strTitle = strTitle;
            m_strAuthor = strAuthor;
            m_strKeywords = strKeywords;
            m_strName = strName;

            if (strMediaType.CompareTo("AUDIO") == 0)
            {
                m_imageIcon = global::EP_WordPlugin.Properties.Resources.womi_audio;
            }
            else if (strMediaType.CompareTo("IMAGE") == 0)
            {
                m_imageIcon = global::EP_WordPlugin.Properties.Resources.womi_image;
            }
            else if (strMediaType.CompareTo("ICON") == 0)
            {
                m_imageIcon = global::EP_WordPlugin.Properties.Resources.womi_icon;
            }
            else if (strMediaType.CompareTo("VIDEO") == 0)
            {
                m_imageIcon = global::EP_WordPlugin.Properties.Resources.womi_video;
            }
            else if (strMediaType.CompareTo("OINT") == 0)
            {
                m_imageIcon = global::EP_WordPlugin.Properties.Resources.womi_oint;
            }
        }
    }


    public class WomiFolderComparer : IComparer<WomiFolder>
    {
        public int Compare(WomiFolder obj1, WomiFolder obj2)
        {
            return obj1.FolderName.CompareTo(obj2.FolderName);
        }
    }

}
