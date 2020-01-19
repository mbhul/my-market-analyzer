using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Runtime.Serialization;

namespace MyMarketAnalyzer
{
    public class Profile : IXmlSerializable
    {
        public List<Equity> WatchlistItems;
        public String CurrentLocation { get; private set; }
        public Boolean IsInitializing { get; private set; }

        private static String sDefaultLocation = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + "Profiles\\";
        private String Name;

        private static Profile _instance;
        public static Profile Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Profile();
                }
                return _instance;
            }
        }

        public Profile()
        {
            Name = "Default";
            WatchlistItems = new List<Equity>();
            CurrentLocation = sDefaultLocation;
            IsInitializing = false;
        }

        #region XML Serialization Functions
        private XmlElement ToXmlElement()
        {
            XmlDocument doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new System.Xml.Serialization.XmlSerializer(typeof(Profile)).Serialize(writer, this);
            }

            return doc.DocumentElement;
        }

        // Xml Serialization Infrastructure
        public void WriteXml(XmlWriter writer)
        {
            XmlDocument doc = new XmlDocument();

            writer.WriteStartElement("Watchlist");

            foreach(Equity eq in this.WatchlistItems)
            {
                writer.WriteStartElement("WatchItem");
                writer.WriteElementString("Name", eq.Name);
                writer.WriteElementString("Last", eq.DailyLast[eq.DailyLast.Count - 1].ToString());
                writer.WriteElementString("Change", eq.DailyChg.ToString() + "(" + eq.DailyChgPct.ToString() + "%)");
                writer.WriteElementString("Date", eq.DailyTime[eq.DailyTime.Count - 1].ToString());
                writer.WriteElementString("Source", eq.LiveDataAddress);
                writer.WriteElementString("Hist", eq.DataFileName);
                writer.WriteElementString("Listed", eq.ListedMarket);
                writer.WriteEndElement();
            }
            
            writer.WriteEndElement();
        }

        public void ReadXml(XmlReader reader)
        {
            XmlReader subReader;
            String txt, chgStr;
            Double lastPrice;
            DateTime lastDate;

            reader.MoveToContent();
            reader.ReadStartElement();
            IsInitializing = true;

            //subReader = reader.ReadSubtree();

            while (reader.ReadToFollowing("WatchItem"))
            {
                subReader = reader.ReadSubtree();

                subReader.ReadToFollowing("Name");
                if (subReader.IsEmptyElement)
                {
                    txt = "";
                }
                else
                {
                    txt = subReader.ReadElementContentAsString();
                }
                Equity eq = new Equity(txt);

                /*** Get Last Data Values ***/
                if (subReader.Name != "Last")
                {
                    subReader.ReadToFollowing("Last");
                }
                if (subReader.IsEmptyElement)
                {
                    lastPrice = 0;
                }
                else
                {
                    txt = subReader.ReadElementContentAsString();
                    lastPrice = Double.Parse(txt);
                }

                if (subReader.Name != "Change")
                {
                    subReader.ReadToFollowing("Change");
                }
                if (subReader.IsEmptyElement)
                {
                    chgStr = "";
                }
                else
                {
                    chgStr = subReader.ReadElementContentAsString();
                }

                if (subReader.Name != "Date")
                {
                    subReader.ReadToFollowing("Date");
                }
                if (subReader.IsEmptyElement)
                {
                    lastDate = DateTime.Today;
                }
                else
                {
                    txt = subReader.ReadElementContentAsString();
                    lastDate = DateTime.Parse(txt);
                }

                eq.LoadDataFromProfile(this, lastPrice, chgStr, lastDate);
                /****************************/

                //Live data url
                if(subReader.Name != "Source")
                {
                    subReader.ReadToFollowing("Source");
                }
                if (subReader.IsEmptyElement)
                {
                    txt = "";
                }
                else
                {
                    txt = subReader.ReadElementContentAsString();
                }
                eq.LiveDataAddress = txt;

                //Historical data file
                if (subReader.Name != "Hist")
                {
                    subReader.ReadToFollowing("Hist");
                }
                if (subReader.IsEmptyElement)
                {
                    txt = "";
                }
                else
                {
                    txt = subReader.ReadElementContentAsString();
                }
                eq.DataFileName = txt;

                //Historical data file
                if (subReader.Name != "Listed")
                {
                    subReader.ReadToFollowing("Listed");
                }
                if (subReader.IsEmptyElement)
                {
                    txt = "";
                }
                else
                {
                    txt = subReader.ReadElementContentAsString();
                }
                eq.ListedMarket = txt;

                this.WatchlistItems.Add(eq);
            }

            IsInitializing = false;
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }
        #endregion

        /*****************************************************************************
         *  FUNCTION:  Load
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public Profile Load(String pFPath)
        {
            XmlDocument doc = new XmlDocument();

            if(ValidatePath(pFPath))
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "MyMarketAnalyzer";
                xRoot.IsNullable = true;

                XmlSerializer serializer = new XmlSerializer(typeof(Profile), xRoot);
                Stream stream = File.Open(pFPath, FileMode.Open);
                _instance = (Profile)serializer.Deserialize(stream);
                stream.Close();
            }

            return _instance;
        }

        /*****************************************************************************
         *  FUNCTION:  Save
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public bool Save(String pFPath)
        {
            bool success = false;
            XmlDocument doc = new XmlDocument();
            XmlNode nodeIn;

            if (!ValidatePath(pFPath))
            {
                pFPath = sDefaultLocation + Name + ".mma";
            }
            CurrentLocation = pFPath;

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement elementMain = doc.CreateElement(string.Empty, "MyMarketAnalyzer", string.Empty);
            doc.AppendChild(elementMain);

            nodeIn = doc.ImportNode(this.ToXmlElement(), true);
            elementMain.AppendChild(nodeIn);

            doc.Save(pFPath);
            success = true;

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  ValidatePath
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private bool ValidatePath(String pFPath)
        {
            bool success = true;

            try
            {
                Path.GetFullPath(pFPath);
                if(pFPath.Substring(pFPath.Length - 4) != ".mma")
                {
                    success = false;
                }
            }
            catch(Exception)
            {
                success = false;
            }

            return success;
        }
    }



}
