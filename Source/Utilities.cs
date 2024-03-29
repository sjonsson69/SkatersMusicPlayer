﻿using MySqlConnector;
using NAudio.Wave;
using System;
using System.Configuration;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace SkatersMusicPlayer
{
    public partial class FormMusicPlayer : Form
    {
        public static string getDBString(SQLiteDataReader reader, int column, string defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetString(column);
            }
        }
        public static string getDBString(SQLiteDataReader reader, string columnname, string defaultValue)
        {
            int column = reader.GetOrdinal(columnname);
            return getDBString(reader, column, defaultValue);
        }

        public static string getDBString(MySqlDataReader reader, int column, string defaultValue)
        {
            if (reader.IsDBNull(column))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetString(column);
            }
        }
        public static string getDBString(MySqlDataReader reader, string columnname, string defaultValue)
        {
            int column = reader.GetOrdinal(columnname);
            return getDBString(reader, column, defaultValue);
        }

        private static void loadMusicFolder(string Folder, bool randomize, ListView LV)
        {
            try
            {
                // Clear current list
                LV.Items.Clear();

                // Check to see if there is a path, otherwise we use current folder
                string D = Path.GetDirectoryName(Folder + "\\");
                if (string.IsNullOrEmpty(D))
                {   // There was no path. Use current folder
                    D = ".\\";
                }

                // Get all files from the directory and subdirectories
                FileInfo[] fiArray = new DirectoryInfo(D).GetFiles(Path.GetFileName("*.*"), System.IO.SearchOption.AllDirectories);

                // Initialize audioreader so we can verify if the files found are playable
                AudioFileReader audioFileReaderTest = null;

                // Loop for each file to load the file into the list
                foreach (FileInfo fi in fiArray)
                {
                    try
                    {
                        //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                        audioFileReaderTest = new AudioFileReader(fi.FullName);
                        //LV.Items.Add(fi.Name).SubItems.Add(fi.FullName);
                        ListViewItem item = new ListViewItem(fi.Name);
                        item.SubItems.Add(fi.FullName);
                        item.ToolTipText = string.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds); ;
                        LV.Items.Add(item);
                    }
                    catch (Exception)
                    {
                        // Do nothing
                    }
                    finally
                    {
                        if (audioFileReaderTest != null)
                        {
                            // Dispose of the reader
                            audioFileReaderTest.Dispose();
                            audioFileReaderTest = null;
                        }
                    }

                }

                // Change column size so the music fits
                LV.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                // Randomize the music
                if (randomize)
                {
                    Random rng = new Random();
                    int i = LV.Items.Count;
                    while (i > 1)
                    {
                        i--;
                        int k = rng.Next(i + 1);
                        ListViewItem I1 = (ListViewItem)LV.Items[k];
                        ListViewItem I2 = (ListViewItem)LV.Items[i];
                        LV.Items[k] = new ListViewItem();
                        LV.Items[i] = new ListViewItem();
                        LV.Items[k] = I2;
                        LV.Items[i] = I1;
                    }
                }

                //Select first item to load music
                if (LV.Items.Count != 0)
                {
                    LV.Items[0].Selected = true;
                }

            }
            catch (DirectoryNotFoundException)
            {
                //No directory. Do nothing
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void loadXMLfile()
        {
            //Load XmlFile
            try
            {
                doc.Load(Properties.Resources.XML_FILENAME);
            }
            catch (FileNotFoundException)
            {
                doc.AppendChild(doc.CreateElement("Name"));
                doc.DocumentElement.SetAttribute("Name", "New competition");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Competition XML-file\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Set competitionname in form header
            this.Text = "Skaters MusicPlayer - " + doc.DocumentElement.GetAttribute("Name");

            // Empty Listview
            listViewParticipants.Items.Clear();
            listViewParticipants.Tag = null;

            // Load categories
            loadCategories(doc, comboBoxCategory);
        }

        public static void loadCategories(XmlDocument doc, ComboBox CB)
        {
            if (doc != null && CB != null)
            {
                try
                {
                    // Remove all old data
                    CB.Items.Clear();

                    if (doc.DocumentElement != null)
                    {
                        foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                        {
                            // Does the Category have Short?
                            if (tableNode.Attributes.GetNamedItem("HasShort") != null)
                            {
                                CB.Items.Add(tableNode.Attributes.GetNamedItem("Name").Value + " - Short");
                            }
                            CB.Items.Add(tableNode.Attributes.GetNamedItem("Name").Value + " - Free "); // Extra space required to be removed when we select a category
                        }
                    }
                }
                catch (Exception e)
                {
                    _ = MessageBox.Show("Error loading categories\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private static string getXMLElement(XmlElement element, string ChildElement, string defaultValue)
        {
            string value = defaultValue;
            if (element != null)
            {
                if (string.IsNullOrEmpty(ChildElement))
                {
                    value = element.InnerText;
                }
                else
                {
                    if (element[ChildElement] != null)
                    {
                        value = element[ChildElement].InnerText;
                    }
                }
            }
            return value;
        }

        private static string getXMLElementAttribute(XmlElement element, string ChildElement, string Attribute, string defaultValue)
        {
            string value = defaultValue;
            if (element != null)
            {
                if (string.IsNullOrEmpty(ChildElement))
                {
                    value = element.GetAttribute(Attribute);
                }
                else
                {
                    if (element[ChildElement] != null)
                    {
                        value = element[ChildElement].GetAttribute(Attribute);
                    }
                }
            }
            return value;
        }

        public static string getMD5HashFromFile(string fileName)
        {
            using (var md5 = SHA256.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private void loadParticipants(XmlDocument doc, string selected, ListView LV)
        {
            // Split selected itemtext into Category and Segment
            string Category = selected.Substring(0, selected.Length - 8);
            string Segment = selected.Substring(selected.Length - 5, 5).Trim();

            // Remove all participants, and also remove Tag so we dont colorcode from last category/segment.
            LV.Items.Clear();
            LV.Tag = null;

            // Initialize audioreader so we can verify if the files found are playable
            AudioFileReader audioFileReaderTest = null;

            if (doc.DocumentElement != null)
            {
                // Loop throu all Categories to find the selected one
                foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                {
                    // Is it the correct category?
                    if (tableNode.Attributes.GetNamedItem("Name").Value == Category)
                    {
                        foreach (XmlNode participant in tableNode.ChildNodes)
                        {
                            // Update infotext that we are working...
                            toolStripStatusLabel1.Text = "Loading participant:" + getXMLElement(participant["FirstName"], string.Empty, string.Empty) + " " + getXMLElement(participant["LastName"], string.Empty, string.Empty);
                            statusStrip1.Update();

                            ListViewItem I = new ListViewItem(getXMLElement(participant[Segment], "StartNo", string.Empty));
                            I.SubItems.Add(getXMLElement(participant["FirstName"], string.Empty, string.Empty) + " " + getXMLElement(participant["LastName"], string.Empty, string.Empty));
                            I.SubItems.Add(getXMLElement(participant["Club"], string.Empty, string.Empty));
                            string musicFile = getXMLElement(participant[Segment], "MusicFile", string.Empty);
                            string MD5Value = getXMLElementAttribute(participant[Segment], "MusicFile", "MD5", string.Empty);
                            try
                            {
                                //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                                audioFileReaderTest = new AudioFileReader(musicFile);
                                // Check MD5 for musicfile so there hasn't been a change of file since imported.
                                if (!string.IsNullOrEmpty(MD5Value) && MD5Value != getMD5HashFromFile(musicFile))
                                {
                                    I.SubItems.Add(Properties.Resources.CHECKSUM_ERROR);
                                    I.SubItems.Add(musicFile);
                                    I.ForeColor = Color.Orange;
                                }
                                else
                                {// MD5 is correct!
                                    _ = I.SubItems.Add($"{(int)audioFileReaderTest.TotalTime.TotalMinutes:00}:{audioFileReaderTest.TotalTime.Seconds:00}");
                                    _ = I.SubItems.Add(musicFile);
                                }
                            }
                            catch (Exception)
                            {
                                // Can't read file
                                if (string.IsNullOrEmpty(musicFile))
                                {
                                    I.SubItems.Add(Properties.Resources.NO_FILE);
                                }
                                else
                                {
                                    I.SubItems.Add(Properties.Resources.INVALID_FILE);
                                }
                                I.SubItems.Add(musicFile);
                                I.ForeColor = Color.Red;
                            }
                            finally
                            {
                                if (audioFileReaderTest != null)
                                {
                                    // Dispose of the reader
                                    audioFileReaderTest.Dispose();
                                    audioFileReaderTest = null;
                                }
                            }

                            I.SubItems.Add(getXMLElement(participant[Segment], "MusicName", string.Empty));

                            LV.Items.Add(I);
                        }

                        // Resize columns to show all content
                        LV.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); // StartNo
                        LV.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // Name
                        LV.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // Club
                        LV.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); // Length
                        LV.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // Music file
                        LV.Columns[5].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // MusicName

                    }
                }
            }
        }

        public static void loadParticipantsDV(XmlDocument doc, string selected, DataGridView DV)
        {
            if (doc != null && selected != null && DV != null)
            {
                // Split selected itemtext into Category and Segment
                string Category = selected.Substring(0, selected.Length - 8);
                string Segment = selected.Substring(selected.Length - 5, 5).Trim();
                string OtherSegment = "Short";
                if (Segment == "Short")
                {
                    OtherSegment = "Free";
                }

                DV.Rows.Clear();

                // Initialize audioreader so we can verify if the files found are playable
                AudioFileReader audioFileReaderTest = null;

                if (doc.DocumentElement != null)
                {
                    // Loop throu all Categorieses to find the selected one
                    foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                    {
                        // Is it the correct category?
                        if (tableNode.Attributes.GetNamedItem("Name").Value == Category)
                        {
                            foreach (XmlNode participant in tableNode.ChildNodes)
                            {

                                DV.Rows.Add();
                                int rownr = DV.Rows.Count - 2;
                                DV[0, rownr].Value = getXMLElement(participant[Segment], "StartNo", string.Empty);
                                DV[1, rownr].Value = getXMLElement(participant["FirstName"], string.Empty, string.Empty);
                                DV[2, rownr].Value = getXMLElement(participant["LastName"], string.Empty, string.Empty);
                                DV[3, rownr].Value = getXMLElement(participant["Club"], string.Empty, string.Empty);
                                if (participant.Attributes.GetNamedItem("ID") != null)
                                {
                                    DV[4, rownr].Value = participant.Attributes.GetNamedItem("ID").Value;
                                }
                                if (participant.Attributes.GetNamedItem("BirthDate") != null)
                                {
                                    DV[5, rownr].Value = participant.Attributes.GetNamedItem("BirthDate").Value;
                                }

                                DV[6, rownr].Value = getXMLElement(participant[Segment], "MusicName", string.Empty);

                                // Store information for the other segment, so we can recreate the element when we save
                                DV[11, rownr].Value = getXMLElement(participant[OtherSegment], "StartNo", string.Empty);
                                DV[12, rownr].Value = getXMLElement(participant[OtherSegment], "MusicFile", string.Empty);
                                DV[13, rownr].Value = getXMLElementAttribute(participant[OtherSegment], "MusicFile", "MD5", string.Empty);
                                DV[14, rownr].Value = getXMLElement(participant[OtherSegment], "MusicName", string.Empty);

                                // Load music file and information, and check the file information
                                string musicFile = getXMLElement(participant[Segment], "MusicFile", string.Empty);
                                DV[9, rownr].Value = musicFile;
                                string MD5Value = getXMLElementAttribute(participant[Segment], "MusicFile", "MD5", string.Empty);
                                DV[10, rownr].Value = MD5Value;
                                try
                                {
                                    //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                                    audioFileReaderTest = new AudioFileReader(musicFile);
                                    // Check MD5 for musicfile so there hasn't been a change of file since imported.
                                    if (string.IsNullOrEmpty(MD5Value) || MD5Value == getMD5HashFromFile(musicFile))
                                    {// MD5 is correct!
                                        DV[8, rownr].Value = string.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds);
                                    }
                                    else
                                    {// MD5 doesn't match!
                                        DV[8, rownr].Value = "Checksum error";
                                        DV.Rows[rownr].DefaultCellStyle.ForeColor = Color.Orange;
                                    }
                                }
                                catch (Exception)
                                {
                                    // Can't read file
                                    if (string.IsNullOrEmpty(musicFile))
                                    {
                                        DV[8, rownr].Value = "No file";
                                    }
                                    else
                                    {
                                        DV[8, rownr].Value = "Invalid file";
                                    }
                                    DV.Rows[rownr].DefaultCellStyle.ForeColor = Color.Red;
                                }
                                finally
                                {
                                    if (audioFileReaderTest != null)
                                    {
                                        // Dispose of the reader
                                        audioFileReaderTest.Dispose();
                                        audioFileReaderTest = null;
                                    }
                                }

                            }

                        }
                    }
                }
            }
        }

        private void loadIndTA2(XmlDocument doc, string indTAFilename)
        {
            XmlDocument indTA = new XmlDocument() { XmlResolver = null };


            //Load XmlFile
            try
            {

                indTA.Load(indTAFilename);

                if (indTA.DocumentElement != null)
                {
                    // Set competition name från EventHeader
                    foreach (XmlNode tableNode in indTA.DocumentElement.GetElementsByTagName("EventHeader"))
                    {// Set competition name
                        doc.DocumentElement.SetAttribute("Name", getXMLElement(tableNode["EventName"], string.Empty, string.Empty));
                    }

                    // Loop Person/SyncroTeam/Pair
                    var elements = indTA.DocumentElement.GetElementsByTagName("Team");
                    if (elements.Count == 0)
                    {// No Teamnames, try Person
                        elements = indTA.DocumentElement.GetElementsByTagName("Person");
                    }
                    foreach (XmlNode personNodeIndTA in elements)
                    {
                        // Categories for Persons
                        string categoryName = getXMLElement(personNodeIndTA.ParentNode.ParentNode["Class"], string.Empty, string.Empty);
                        string discipline = getXMLElement(personNodeIndTA.ParentNode.ParentNode["Discipline"], string.Empty, string.Empty);
                        if (discipline != "Singel")
                        {//Add discipline to category if not singel
                            categoryName = categoryName + " (" + discipline + ")";
                        }
                        if (!string.IsNullOrEmpty(categoryName) && categoryName != "Tränare" && categoryName != "Lagledare")
                        {//We got a categoryname, continue to import
                         //Find the category in Competition
                            XmlNode categoryNode = null;
                            foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                            {
                                if (node.Attributes.GetNamedItem("Name").Value == categoryName)
                                {
                                    categoryNode = node;
                                }
                            }

                            // If category not found, create a new category
                            if (categoryNode == null)
                            {//New category. Create structure
                                categoryNode = doc.CreateElement(Properties.Resources.XMLTAG_CATEGORY);
                                XmlAttribute attributeName = doc.CreateAttribute("Name");
                                attributeName.Value = categoryName;
                                categoryNode.Attributes.Append(attributeName);
                                //if (hasSHort??)
                                //{
                                //    XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                                //    attributeShort.Value = "true";
                                //    categoryNode.Attributes.Append(attributeShort);
                                //}
                                doc.DocumentElement.AppendChild(categoryNode);
                            }

                            // Locate participant for update or create a participant
                            string ID = getXMLElement(personNodeIndTA["IdrottOnlineID"], string.Empty, string.Empty);
                            string FirstName = getXMLElement(personNodeIndTA["FirstName"], string.Empty, string.Empty);
                            string LastName = getXMLElement(personNodeIndTA["SurName"], string.Empty, string.Empty);
                            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                            {// No name, Get Syncro/Pair name
                                ID = getXMLElementAttribute((XmlElement)personNodeIndTA, string.Empty, "Id", string.Empty);
                                FirstName = getXMLElement(personNodeIndTA["Name"], string.Empty, string.Empty);
                            }
                            string Club = getXMLElement(personNodeIndTA["Organization"], "OrganizationName", string.Empty);
                            string MusicSP = getXMLElement(personNodeIndTA["MusicSP"], string.Empty, string.Empty);
                            string MusicFS = getXMLElement(personNodeIndTA["MusicFS"], string.Empty, string.Empty);

                            // Try to find if participant already present using ID number from indTA and match with ID from Competition
                            XmlNode personNode = null;
                            if (categoryNode.HasChildNodes && !string.IsNullOrEmpty(ID))
                            {
                                foreach (XmlNode personNodeDoc in categoryNode)
                                {
                                    if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                    {
                                        personNode = personNodeDoc;
                                    }
                                }
                            }


                            // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                            if (personNode == null)
                            {
                                if (categoryNode.HasChildNodes)
                                {
                                    foreach (XmlNode personNodeDoc in categoryNode)
                                    {
                                        if (getXMLElement(personNodeDoc["FirstName"], string.Empty, string.Empty) == FirstName &&
                                            getXMLElement(personNodeDoc["LastName"], string.Empty, string.Empty) == LastName &&
                                            getXMLElement(personNodeDoc["Club"], string.Empty, string.Empty) == Club)
                                        {
                                            personNode = personNodeDoc;
                                        }
                                    }

                                }
                            }


                            // If person not found, create a new person
                            if (personNode == null)
                            {
                                personNode = doc.CreateElement(Properties.Resources.XMLTAG_PARTICIPANT);
                                XmlAttribute attributeID = doc.CreateAttribute("ID");
                                attributeID.Value = ID;
                                personNode.Attributes.Append(attributeID);
                                XmlAttribute attributeBD = doc.CreateAttribute("BirthDate");
                                attributeBD.Value = getXMLElement(personNodeIndTA["BirthDate"], string.Empty, string.Empty); ;
                                personNode.Attributes.Append(attributeBD);

                                categoryNode.AppendChild(personNode);
                            }

                            // Make sure we have elements
                            if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                            if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                            if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));
                            if (personNode["Short"] == null) personNode.AppendChild(doc.CreateElement("Short"));
                            if (personNode["Short"]["StartNo"] == null) personNode["Short"].AppendChild(doc.CreateElement("StartNo"));
                            if (personNode["Short"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicSP)) personNode["Short"].AppendChild(doc.CreateElement("MusicName"));
                            if (personNode["Free"] == null) personNode.AppendChild(doc.CreateElement("Free"));
                            if (personNode["Free"]["StartNo"] == null) personNode["Free"].AppendChild(doc.CreateElement("StartNo"));
                            if (personNode["Free"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicFS)) personNode["Free"].AppendChild(doc.CreateElement("MusicName"));

                            // Update 
                            personNode["FirstName"].InnerText = FirstName;
                            personNode["LastName"].InnerText = LastName;
                            personNode["Club"].InnerText = Club;
                            if (!string.IsNullOrWhiteSpace(MusicSP))
                            {
                                personNode["Short"]["MusicName"].InnerText = MusicSP;
                            }
                            if (!string.IsNullOrWhiteSpace(MusicFS))
                            {
                                personNode["Free"]["MusicName"].InnerText = MusicFS;
                            }

                        }

                    }

                    doc.Save(Properties.Resources.XML_FILENAME);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading IndTA XML-file\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void loadISUCalcXML(XmlDocument doc, string p)
        {
            XmlDocument ISUXML = new XmlDocument() { XmlResolver = null };

            //Load XmlFile
            try
            {

                ISUXML.Load(p);

                if (ISUXML.DocumentElement != null)
                {
                    // Loop for events
                    foreach (XmlNode eventNode in ISUXML.DocumentElement.GetElementsByTagName("Event"))
                    {// Set competition name
                        //string compName = getXMLElementAttribute(eventNode["Event"], string.Empty, "EVT_NAME", string.Empty);
                        string compName = eventNode.Attributes.GetNamedItem("EVT_NAME").Value;
                        doc.DocumentElement.SetAttribute("Name", compName);


                        foreach (XmlElement personNodeISU in ISUXML.DocumentElement.GetElementsByTagName("Person_Couple_Team"))
                        {
                            // Locate participant for update or create a participant
                            string ID = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_EXTDT", string.Empty);  //External ID - IdrottonlineID?
                            string Birthdate = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_BDAY", string.Empty);  //Birthdate - Used to find music
                            string FirstName = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_GNAME", string.Empty);
                            string LastName = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_FNAME", string.Empty);
                            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                            {//Inget personnamn. Hämta tävingsnamnet (antagligen Team eller Par)
                                FirstName = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_CNAME", string.Empty);
                            }
                            string Club = getXMLElementAttribute(personNodeISU["Club"], string.Empty, "PCT_CNAME", string.Empty);
                            string PARID = personNodeISU.ParentNode.Attributes.GetNamedItem("PAR_ID").Value;  //ParticipandID. Used to find startno
                            string StartNo1 = string.Empty;
                            string StartNo2 = string.Empty;
                            string MusicSP = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_SPMNAM", string.Empty);
                            string MusicFS = getXMLElementAttribute(personNodeISU.ParentNode["Person_Couple_Team"], string.Empty, "PCT_FSMNAM", string.Empty);
                            bool hasShort = false;

                            string categoryName = personNodeISU.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Attributes.GetNamedItem("CAT_NAME").Value;

                            // Loop throu segments to find segments types and startno
                            foreach (XmlElement segmentNode in personNodeISU.ParentNode.ParentNode.ParentNode.ParentNode)
                            {
                                string type = string.Empty;
                                if (segmentNode.Attributes.GetNamedItem("SCP_TYPE") != null)
                                {
                                    type = segmentNode.Attributes.GetNamedItem("SCP_TYPE").Value;
                                }
                                if (type == "S")
                                {
                                    hasShort = true;
                                }

                                // Loop throu performmace to find participant and startno
                                foreach (XmlElement perfNode in segmentNode.GetElementsByTagName("Performance"))
                                {
                                    //Do we have a PAR_ID and a Startnum?
                                    if (perfNode.Attributes.GetNamedItem("PAR_ID") != null && perfNode.Attributes.GetNamedItem("PRF_STNUM") != null)
                                    {
                                        //Is it our current participant?
                                        if (perfNode.Attributes.GetNamedItem("PAR_ID").Value == PARID)
                                        {
                                            //Check short or free
                                            if (type == "S")
                                            {
                                                StartNo1 = perfNode.Attributes.GetNamedItem("PRF_STNUM").Value;
                                            }
                                            if (type == "F")
                                            {
                                                StartNo2 = perfNode.Attributes.GetNamedItem("PRF_STNUM").Value;
                                            }

                                        }
                                        type = segmentNode.Attributes.GetNamedItem("SCP_TYPE").Value;
                                    }

                                }
                            }


                            //Find the category in Competition
                            XmlNode categoryNode = null;
                            foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                            {
                                if (node.Attributes.GetNamedItem("Name").Value == categoryName)
                                {
                                    categoryNode = node;
                                }
                            }

                            // If category not found, create a new category
                            if (categoryNode == null)
                            {//New category. Create structure
                                categoryNode = doc.CreateElement(Properties.Resources.XMLTAG_CATEGORY);
                                XmlAttribute attributeName = doc.CreateAttribute("Name");
                                attributeName.Value = categoryName;
                                categoryNode.Attributes.Append(attributeName);
                                if (hasShort)
                                {
                                    XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                                    attributeShort.Value = "true";
                                    categoryNode.Attributes.Append(attributeShort);
                                }
                                doc.DocumentElement.AppendChild(categoryNode);
                            }

                            // Update value for HasShort
                            if (hasShort)
                            {
                                categoryNode.Attributes.GetNamedItem("HasShort");
                                XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                                attributeShort.Value = "true";
                                categoryNode.Attributes.Append(attributeShort);
                            }

                            // Try to find if participant already present using ID number from indTA and match with ID from Competition
                            XmlNode personNode = null;
                            if (categoryNode.HasChildNodes && !string.IsNullOrEmpty(ID))
                            {
                                foreach (XmlNode personNodeDoc in categoryNode)
                                {
                                    if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                    {
                                        personNode = personNodeDoc;
                                    }
                                }
                            }


                            // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                            if (personNode == null)
                            {
                                if (categoryNode.HasChildNodes)
                                {
                                    foreach (XmlNode personNodeDoc in categoryNode)
                                    {
                                        if (getXMLElement(personNodeDoc["FirstName"], string.Empty, string.Empty) == FirstName &&
                                            getXMLElement(personNodeDoc["LastName"], string.Empty, string.Empty) == LastName &&
                                            getXMLElement(personNodeDoc["Club"], string.Empty, string.Empty) == Club)
                                        {
                                            personNode = personNodeDoc;
                                        }
                                    }

                                }
                            }


                            // If person not found, create a new person
                            if (personNode == null)
                            {
                                personNode = doc.CreateElement(Properties.Resources.XMLTAG_PARTICIPANT);
                                XmlAttribute attributeID = doc.CreateAttribute("ID");
                                attributeID.Value = ID;
                                personNode.Attributes.Append(attributeID);
                                XmlAttribute attributeBD = doc.CreateAttribute("BirthDate");
                                attributeBD.Value = Birthdate;
                                personNode.Attributes.Append(attributeBD);

                                categoryNode.AppendChild(personNode);
                            }

                            // Make sure we have elements
                            if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                            if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                            if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));
                            if (personNode["Short"] == null) personNode.AppendChild(doc.CreateElement("Short"));
                            if (personNode["Short"]["StartNo"] == null) personNode["Short"].AppendChild(doc.CreateElement("StartNo"));
                            if (personNode["Short"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicSP)) personNode["Short"].AppendChild(doc.CreateElement("MusicName"));
                            if (personNode["Free"] == null) personNode.AppendChild(doc.CreateElement("Free"));
                            if (personNode["Free"]["StartNo"] == null) personNode["Free"].AppendChild(doc.CreateElement("StartNo"));
                            if (personNode["Free"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicFS)) personNode["Free"].AppendChild(doc.CreateElement("MusicName"));

                            // Update 
                            personNode["FirstName"].InnerText = FirstName;
                            personNode["LastName"].InnerText = LastName;
                            if (!string.IsNullOrWhiteSpace(Club))
                            {
                                personNode["Club"].InnerText = Club;
                            }
                            personNode["Short"]["StartNo"].InnerText = StartNo1.PadLeft(3, ' '); ;
                            if (!string.IsNullOrWhiteSpace(MusicSP))
                            {
                                personNode["Short"]["MusicName"].InnerText = MusicSP;
                            }
                            personNode["Free"]["StartNo"].InnerText = StartNo2.PadLeft(3, ' '); ;
                            if (!string.IsNullOrWhiteSpace(MusicFS))
                            {
                                personNode["Free"]["MusicName"].InnerText = MusicFS;
                            }

                        }


                    }



                    doc.Save(Properties.Resources.XML_FILENAME);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading ISUCalc XML-file\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void loadStarFS(XmlDocument doc, string filename)
        {
            //Load StarFS database
            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=" + filename + "; Version=3; FailIfMissing=True;"))
                {
                    con.Open();

                    //Query database for Eventname
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT EVENT_NAME, DB_VERSION FROM EVENT", con))
                    {
                        string compName = string.Empty;
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                compName = getDBString(reader, "EVENT_NAME", string.Empty);
                            }
                            reader.Close();
                        }

                        //Not an event, use CompetitionName instead
                        if (String.IsNullOrEmpty(compName))
                        {
                            cmd.CommandText = "SELECT COMPETITION_NAME FROM COMPETITION";
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    compName = getDBString(reader, "COMPETITION_NAME", string.Empty);
                                }
                                reader.Close();
                            }
                        }
                        //Store Event/Competition name
                        doc.DocumentElement.SetAttribute("Name", compName);

                        //Get Participants
                        cmd.CommandText = "SELECT P.INDTA_ID, BIRTHDATE, FIRSTNAME, LASTNAME, CLUBNAME, CAST(START_NO AS TEXT) AS START_NO, MUSIC, DISCIPLINE, CATEGORY_NAME || CASE WHEN DISCIPLINE='Singel' THEN '' ELSE ' ('||DISCIPLINE||')' END AS CATEGORY_NAME\n"
                                          + "FROM PARTICIPANT P\n"
                                          + "INNER JOIN BASEDATA B ON B.CATEGORY_ID = P.CATEGORY_ID\n"
                                          + "INNER JOIN CATEGORY C ON C.CATEGORY_ID = P.CATEGORY_ID\n"
                                          + "ORDER BY IFNULL(START_TIME, 'xxxxxx'), B.CATEGORY_ID, P.START_NO";
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Locate participant for update or create a participant
                                string ID = getDBString(reader, "INDTA_ID", string.Empty);  //IdrottonlineID
                                string Birthdate = getDBString(reader, "BIRTHDATE", string.Empty);  //Birthdate - Used to find music
                                string FirstName = getDBString(reader, "FIRSTNAME", string.Empty);
                                string LastName = getDBString(reader, "LASTNAME", string.Empty);
                                string Club = getDBString(reader, "CLUBNAME", string.Empty);
                                string StartNo2 = getDBString(reader, "START_NO", string.Empty);
                                string MusicFS = getDBString(reader, "MUSIC", string.Empty);
                                string categoryName = getDBString(reader, "CATEGORY_NAME", string.Empty);

                                //Find the category in Competition
                                XmlNode categoryNode = null;
                                foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                                {
                                    if (node.Attributes.GetNamedItem("Name").Value == categoryName)
                                    {
                                        categoryNode = node;
                                    }
                                }

                                // If category not found, create a new category
                                if (categoryNode == null)
                                {//New category. Create structure
                                    categoryNode = doc.CreateElement(Properties.Resources.XMLTAG_CATEGORY);
                                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                                    attributeName.Value = categoryName;
                                    categoryNode.Attributes.Append(attributeName);
                                    doc.DocumentElement.AppendChild(categoryNode);
                                }

                                // Try to find if participant already present using ID number from indTA and match with ID from Competition
                                XmlNode personNode = null;
                                if (categoryNode.HasChildNodes && !string.IsNullOrEmpty(ID))
                                {
                                    foreach (XmlNode personNodeDoc in categoryNode)
                                    {
                                        if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                        {
                                            personNode = personNodeDoc;
                                        }
                                    }
                                }


                                // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                if (personNode == null)
                                {
                                    if (categoryNode.HasChildNodes)
                                    {
                                        foreach (XmlNode personNodeDoc in categoryNode)
                                        {
                                            if (getXMLElement(personNodeDoc["FirstName"], string.Empty, string.Empty) == FirstName &&
                                                getXMLElement(personNodeDoc["LastName"], string.Empty, string.Empty) == LastName &&
                                                getXMLElement(personNodeDoc["Club"], string.Empty, string.Empty) == Club)
                                            {
                                                personNode = personNodeDoc;
                                            }
                                        }

                                    }
                                }


                                // If person not found, create a new person
                                if (personNode == null)
                                {
                                    personNode = doc.CreateElement(Properties.Resources.XMLTAG_PARTICIPANT);
                                    XmlAttribute attributeID = doc.CreateAttribute("ID");
                                    attributeID.Value = ID;
                                    personNode.Attributes.Append(attributeID);
                                    XmlAttribute attributeBD = doc.CreateAttribute("BirthDate");
                                    attributeBD.Value = Birthdate;
                                    personNode.Attributes.Append(attributeBD);

                                    categoryNode.AppendChild(personNode);
                                }

                                // Make sure we have elements
                                if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                                if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                                if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));
                                if (personNode["Free"] == null) personNode.AppendChild(doc.CreateElement("Free"));
                                if (personNode["Free"]["StartNo"] == null) personNode["Free"].AppendChild(doc.CreateElement("StartNo"));
                                if (personNode["Free"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicFS)) personNode["Free"].AppendChild(doc.CreateElement("MusicName"));

                                // Update 
                                personNode["FirstName"].InnerText = FirstName;
                                personNode["LastName"].InnerText = LastName;
                                if (!string.IsNullOrWhiteSpace(Club))
                                {
                                    personNode["Club"].InnerText = Club;
                                }
                                personNode["Free"]["StartNo"].InnerText = StartNo2.PadLeft(3, ' '); ;
                                if (!string.IsNullOrWhiteSpace(MusicFS))
                                {
                                    personNode["Free"]["MusicName"].InnerText = MusicFS;
                                }

                            }  //while reader-Participants

                            //Save updates Musicplayer-XML
                            doc.Save(Properties.Resources.XML_FILENAME);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading StarFS database\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void loadFSM(XmlDocument doc, string database)
        {
            //Load StarFS database
            try
            {
                var conString = new MySqlConnectionStringBuilder
                {
                    Server = settings.FSMServer,
                    Port = settings.FSMPort,
                    UserID = settings.FSMUsername,
                    Password = settings.FSMPassword,
                    Database= database
                };
                using (MySqlConnection con = new MySqlConnection(conString.ToString()))
                {
                    con.Open();

                    //Query database for CompetitionName
                    using (MySqlCommand cmd = new MySqlCommand("SELECT Name FROM competition", con))
                    {
                        string compName = string.Empty;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                compName = getDBString(reader, "Name", string.Empty);
                            }
                            reader.Close();
                        }

                        //Store Event/Competition name
                        doc.DocumentElement.SetAttribute("Name", compName);

                        //Get Participants
                        cmd.CommandText = Properties.Resources.SQL_FSM_PARTICIPANTS;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Locate participant for update or create a participant
                                string ID = getDBString(reader, "FederationId", string.Empty);  //IdrottonlineID
                                string Birthdate = getDBString(reader, "BirthDate", string.Empty);  //Birthdate - Used to find music
                                string FirstName = getDBString(reader, "FirstName", string.Empty);
                                string LastName = getDBString(reader, "LastName", string.Empty);
                                string Club = getDBString(reader, "CLUBNAME", string.Empty);
                                string StartNo1 = getDBString(reader, "StartNoShort", string.Empty);
                                string MusicSP = getDBString(reader, "MusicShort", string.Empty);
                                string StartNo2 = getDBString(reader, "StartNoFree", string.Empty);
                                string MusicFS = getDBString(reader, "MusicFree", string.Empty);
                                string categoryName = getDBString(reader, "CategoryName", string.Empty);

                                //Find the category in Competition
                                XmlNode categoryNode = null;
                                foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_CATEGORY))
                                {
                                    if (node.Attributes.GetNamedItem("Name").Value == categoryName)
                                    {
                                        categoryNode = node;
                                    }
                                }

                                // If category not found, create a new category
                                if (categoryNode == null)
                                {//New category. Create structure
                                    categoryNode = doc.CreateElement(Properties.Resources.XMLTAG_CATEGORY);
                                    XmlAttribute attributeName = doc.CreateAttribute("Name");
                                    attributeName.Value = categoryName;
                                    categoryNode.Attributes.Append(attributeName);
                                    doc.DocumentElement.AppendChild(categoryNode);
                                }

                                // Try to find if participant already present using ID number from indTA and match with ID from Competition
                                XmlNode personNode = null;
                                if (categoryNode.HasChildNodes && !string.IsNullOrEmpty(ID))
                                {
                                    foreach (XmlNode personNodeDoc in categoryNode)
                                    {
                                        if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                        {
                                            personNode = personNodeDoc;
                                        }
                                    }
                                }


                                // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                if (personNode == null)
                                {
                                    if (categoryNode.HasChildNodes)
                                    {
                                        foreach (XmlNode personNodeDoc in categoryNode)
                                        {
                                            if (getXMLElement(personNodeDoc["FirstName"], string.Empty, string.Empty) == FirstName &&
                                                getXMLElement(personNodeDoc["LastName"], string.Empty, string.Empty) == LastName &&
                                                getXMLElement(personNodeDoc["Club"], string.Empty, string.Empty) == Club)
                                            {
                                                personNode = personNodeDoc;
                                            }
                                        }

                                    }
                                }


                                // If person not found, create a new person
                                if (personNode == null)
                                {
                                    personNode = doc.CreateElement(Properties.Resources.XMLTAG_PARTICIPANT);
                                    XmlAttribute attributeID = doc.CreateAttribute("ID");
                                    attributeID.Value = ID;
                                    personNode.Attributes.Append(attributeID);
                                    XmlAttribute attributeBD = doc.CreateAttribute("BirthDate");
                                    attributeBD.Value = Birthdate;
                                    personNode.Attributes.Append(attributeBD);

                                    categoryNode.AppendChild(personNode);
                                }

                                // Make sure we have elements
                                if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                                if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                                if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));
                                if (personNode["Short"] == null) personNode.AppendChild(doc.CreateElement("Short"));
                                if (personNode["Short"]["StartNo"] == null) personNode["Short"].AppendChild(doc.CreateElement("StartNo"));
                                if (personNode["Short"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicSP)) personNode["Short"].AppendChild(doc.CreateElement("MusicName"));
                                if (personNode["Free"] == null) personNode.AppendChild(doc.CreateElement("Free"));
                                if (personNode["Free"]["StartNo"] == null) personNode["Free"].AppendChild(doc.CreateElement("StartNo"));
                                if (personNode["Free"]["MusicName"] == null && !string.IsNullOrWhiteSpace(MusicFS)) personNode["Free"].AppendChild(doc.CreateElement("MusicName"));

                                // Update 
                                personNode["FirstName"].InnerText = FirstName;
                                personNode["LastName"].InnerText = LastName;
                                if (!string.IsNullOrWhiteSpace(Club))
                                {
                                    personNode["Club"].InnerText = Club;
                                }
                                personNode["Short"]["StartNo"].InnerText = StartNo1.PadLeft(3, ' ').TrimEnd();
                                if (!string.IsNullOrWhiteSpace(MusicSP))
                                {
                                    personNode["Short"]["MusicName"].InnerText = MusicSP;
                                }
                                personNode["Free"]["StartNo"].InnerText = StartNo2.PadLeft(3, ' ').TrimEnd();
                                if (!string.IsNullOrWhiteSpace(MusicFS))
                                {
                                    personNode["Free"]["MusicName"].InnerText = MusicFS;
                                }

                            }  //while reader-Participants

                            //Save updates Musicplayer-XML
                            doc.Save(Properties.Resources.XML_FILENAME);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading FS Manager database\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void autoconnectMusic()
        {
            string MissingFiles = string.Empty;

            foreach (XmlNode participantNode in doc.DocumentElement.GetElementsByTagName(Properties.Resources.XMLTAG_PARTICIPANT))
            {
                string BirthDate = string.Empty;
                if (participantNode.Attributes.GetNamedItem("BirthDate") != null)
                {
                    BirthDate = participantNode.Attributes.GetNamedItem("BirthDate").Value.Replace("-", string.Empty);
                    if (string.IsNullOrEmpty(BirthDate))
                    {
                        BirthDate = "*";
                    }
                    else
                    {
                        BirthDate += "xxxx";
                    }
                }
                string ID = string.Empty;
                if (participantNode.Attributes.GetNamedItem("ID") != null)
                {
                    ID = participantNode.Attributes.GetNamedItem("ID").Value;
                }

                // Update infotext that we are working...
                toolStripStatusLabel1.Text = "Connecting music for " + getXMLElement(participantNode["FirstName"], string.Empty, string.Empty) + " " + getXMLElement(participantNode["LastName"], string.Empty, string.Empty);
                statusStrip1.Update();

                //Syncro?
                if (string.IsNullOrEmpty(getXMLElement(participantNode["LastName"], string.Empty, string.Empty)))
                {//No last name, probably Syncro or Pair

                }

                // Free music
                // Search for music
                FileInfo[] fiArray = null;
                string FileToFind = (getXMLElement(participantNode["FirstName"], string.Empty, string.Empty) + "_" + getXMLElement(participantNode["LastName"], string.Empty, string.Empty) + "_" + BirthDate + "_" + (string.IsNullOrEmpty(ID) ? "*" : ID) + "_Friåkning.*").Replace(" ", "_").Replace("/", "_");
                //Syncro/Pair?
                if (string.IsNullOrEmpty(getXMLElement(participantNode["LastName"], string.Empty, string.Empty)))
                {//No last name, probably Syncro or Pair
                    FileToFind = (getXMLElement(participantNode["FirstName"], string.Empty, string.Empty) + "_Friåkning.*").Replace(" ", "_").Replace("/", "_");
                }

                try
                {
                    fiArray = new DirectoryInfo(Properties.Resources.PARTICIPANT_MUSIC_DIRECTORY).GetFiles(FileToFind);
                }
                catch (Exception)
                {
                }

                if (fiArray != null && fiArray.Length != 0)
                {// We have a match

                    string SearchFile = fiArray[0].FullName;
                    // Make the path relative if possible.
                    if (SearchFile.Substring(0, Application.StartupPath.Length) == Application.StartupPath)
                    {// Remove startpath
                        SearchFile = SearchFile.Substring(Application.StartupPath.Length + 1);  //Also remove the backslash from path
                    }

                    string MD5 = verifyMusicFile(SearchFile);
                    if (string.IsNullOrEmpty(MD5))
                    {
                        MissingFiles = MissingFiles + "File:" + SearchFile + " is not a valid music file\n";
                    }
                    else
                    {
                        if (MD5.Substring(0, 4) == "MD5:")
                        {
                            MissingFiles = MissingFiles + "File:" + SearchFile + " Identical file with " + MD5.Substring(4) + "\n";
                        }
                        else
                        {
                            // Make sure we have the nodes
                            if (participantNode["Free"] == null) participantNode.AppendChild(doc.CreateElement("Free"));
                            if (participantNode["Free"]["MusicFile"] == null) participantNode["Free"].AppendChild(doc.CreateElement("MusicFile"));

                            participantNode["Free"]["MusicFile"].InnerText = SearchFile;
                            participantNode["Free"]["MusicFile"].SetAttribute("MD5", MD5);
                        }
                    }
                }
                else
                {
                    MissingFiles = MissingFiles + Properties.Resources.PARTICIPANT_MUSIC_DIRECTORY + FileToFind + "\n";
                }



                // Does the Category have Short?
                if (participantNode.ParentNode.Attributes.GetNamedItem("HasShort") != null)
                {
                    // Short music
                    // Search for music
                    fiArray = null;
                    FileToFind = (getXMLElement(participantNode["FirstName"], string.Empty, string.Empty) + "_" + getXMLElement(participantNode["LastName"], string.Empty, string.Empty) + "_" + BirthDate + "_" + (string.IsNullOrEmpty(ID) ? "*" : ID) + "_Kortprogram.*").Replace(" ", "_").Replace("/", "_");
                    if (string.IsNullOrEmpty(getXMLElement(participantNode["LastName"], string.Empty, string.Empty)))
                    {//No last name, probably Syncro or Pair
                        FileToFind = (getXMLElement(participantNode["FirstName"], string.Empty, string.Empty) + "_Kortprogram.*").Replace(" ", "_").Replace("/", "_");
                    }
                    try
                    {
                        fiArray = new DirectoryInfo(Properties.Resources.PARTICIPANT_MUSIC_DIRECTORY).GetFiles(FileToFind);
                    }
                    catch (Exception)
                    {
                    }

                    if (fiArray != null && fiArray.Length != 0)
                    {// We have a match


                        string SearchFile = fiArray[0].FullName;
                        // Make the path relative if possible.
                        if (SearchFile.Substring(0, Application.StartupPath.Length) == Application.StartupPath)
                        {// Remove startpath
                            SearchFile = SearchFile.Substring(Application.StartupPath.Length + 1);  //Also remove the backslash from path
                        }

                        string MD5 = verifyMusicFile(SearchFile);
                        if (string.IsNullOrEmpty(MD5))
                        {
                            MissingFiles = MissingFiles + "File:" + SearchFile + " is not a valid music file\n";
                        }
                        else
                        {
                            if (MD5.Substring(0, 4) == "MD5:")
                            {
                                MissingFiles = MissingFiles + "File:" + SearchFile + " Identical file with " + MD5.Substring(4) + "\n";
                            }
                            else
                            {
                                // Make sure we have the nodes
                                if (participantNode["Short"] == null) participantNode.AppendChild(doc.CreateElement("Short"));
                                if (participantNode["Short"]["MusicFile"] == null) participantNode["Short"].AppendChild(doc.CreateElement("MusicFile"));

                                participantNode["Short"]["MusicFile"].InnerText = SearchFile;
                                participantNode["Short"]["MusicFile"].SetAttribute("MD5", MD5);
                            }
                        }
                    }
                    else
                    {
                        MissingFiles = MissingFiles + Properties.Resources.PARTICIPANT_MUSIC_DIRECTORY + FileToFind + "\n";
                    }
                }



            } // foreach participantNode

            // Inform user of missing files or if all participants are connected
            if (!string.IsNullOrEmpty(MissingFiles))
            {
                _ = MessageBox.Show(MissingFiles, Properties.Resources.CAPTION_MISSING_MUSIC_FILES, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _ = MessageBox.Show(Properties.Resources.ALL_PARTICIPANTS_AUTOCONNECTED, Properties.Resources.CAPTION_AUTOCONNECT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            doc.Save(Properties.Resources.XML_FILENAME);
        }

        private string verifyMusicFile(string fileName)
        {
            string MD5 = string.Empty;
            AudioFileReader audioFileReaderTest = null;
            try
            {
                //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                audioFileReaderTest = new AudioFileReader(fileName);
                MD5 = getMD5HashFromFile(fileName);
                //TODO:Check MD5 for alla participants to verify no dublicates
                foreach (XmlNode MusicFileNode in doc.DocumentElement.GetElementsByTagName("MusicFile"))
                {
                    if (fileName != MusicFileNode.InnerText)
                    {
                        if (MusicFileNode.Attributes.GetNamedItem("MD5") != null && MD5 == MusicFileNode.Attributes.GetNamedItem("MD5").Value.ToString())
                        {
                            MD5 = "MD5:" + MusicFileNode.InnerText;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Do nothing
            }
            finally
            {
                if (audioFileReaderTest != null)
                {
                    audioFileReaderTest.Dispose();
                }
            }
            return MD5;
        }

        private int unzipMusicFiles(int NumberOfFiles, String FileName)
        {
            try
            {
                string extractPath = Application.StartupPath + @"\" + Properties.Resources.PARTICIPANT_MUSIC_DIRECTORY;

                // Create directory if it doesn't exist
                new DirectoryInfo(extractPath).Create();

                // Unzip file...
                using (ZipArchive archive = ZipFile.OpenRead(FileName))
                {
                    int nrOfFiles = archive.Entries.Count;
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                        {
                            // Update infotext that we are working...
                            toolStripStatusLabel1.Text = "Unzip file (" + NumberOfFiles.ToString() + " of " + nrOfFiles.ToString() + ") " + entry.Name;
                            statusStrip1.Update();

                            // Gets the full path to ensure that relative segments are removed.
                            string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                            // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                            // are case-insensitive.
                            if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                            {
                                entry.ExtractToFile(destinationPath, true);
                                NumberOfFiles += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error extracting music files\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return NumberOfFiles;
        }

    }
}
