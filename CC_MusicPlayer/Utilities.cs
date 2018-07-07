using NAudio.Wave;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;

namespace SkatersMusicPlayer
{
    public partial class FormMusicPlayer : Form
    {
        public string GetConfigurationValue(string Key, string Default)
        {
            string Value = Default;
            try
            {
                Value = ConfigurationManager.AppSettings[Key].ToString();
            }
            catch (Exception)
            {
                //Do nothing
            }
            return Value;
        }

        public void LoadMusicFolder(string Folder, bool randomize, ListView LV)
        {
            try
            {
                // Clear current list
                LV.Items.Clear();

                // Check to see if there is a path, otherwise we use current folder
                string D = Path.GetDirectoryName(Folder + "\\");
                if (D == String.Empty)
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
                        item.ToolTipText = String.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds); ;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadXMLfile()
        {
            //Load XmlFile
            try
            {
                doc.Load("competition.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Competition XML-file\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Set competitionname in form header
            this.Text = "Skaters MusicPlayer - " + doc.DocumentElement.GetAttribute("Name");

            // Empty Listview
            listViewSkaters.Items.Clear();
            listViewSkaters.Tag = null;

            // Load classes
            LoadClasses(doc, comboBoxClass);
        }

        public void LoadClasses(XmlDocument doc, ComboBox CB)
        {
            try
            {
                // Remove all old data
                CB.Items.Clear();

                if (doc.DocumentElement != null)
                {
                    foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName("Class"))
                    {
                        // Does the Class contain have Short?
                        if (tableNode.Attributes.GetNamedItem("HasShort") != null)
                        {
                            CB.Items.Add(tableNode.Attributes.GetNamedItem("Name").Value + " - Short");
                        }
                        CB.Items.Add(tableNode.Attributes.GetNamedItem("Name").Value + " - Free "); // Extra space required to be removed when we select a class
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Classes\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string GetXMLElement(XmlElement element, string ChildElement, string defaultValue)
        {
            string value = defaultValue;
            if (element != null)
            {
                if (ChildElement == string.Empty)
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

        private string GetXMLElementAttribute(XmlElement element, string ChildElement, string Attribute, string defaultValue)
        {
            string value = defaultValue;
            if (element != null)
            {
                if (ChildElement == string.Empty)
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

        public string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private void LoadSkaters(XmlDocument doc, string selected, ListView LV)
        {
            // Split selected itemtext into Main and Sub classes.
            string MainClass = selected.Substring(0, selected.Length - 8);
            string SubClass = selected.Substring(selected.Length - 5, 5).Trim();

            // Remove all skaters, and also remove Tag so we dont colorcode from last class.
            LV.Items.Clear();
            LV.Tag = null;

            // Initialize audioreader so we can verify if the files found are playable
            AudioFileReader audioFileReaderTest = null;

            if (doc.DocumentElement != null)
            {
                // Loop throu all Classes to find the selected one
                foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName("Class"))
                {
                    // Is it the correct class?
                    if (tableNode.Attributes.GetNamedItem("Name").Value == MainClass)
                    {
                        foreach (XmlNode skater in tableNode.ChildNodes)
                        {
                            ListViewItem I = new ListViewItem(GetXMLElement(skater[SubClass], "StartNo", ""));
                            I.SubItems.Add(GetXMLElement(skater["FirstName"], string.Empty, string.Empty) + " " + GetXMLElement(skater["LastName"], string.Empty, string.Empty));
                            I.SubItems.Add(GetXMLElement(skater["Club"], string.Empty, string.Empty));
                            string musicFile = GetXMLElement(skater[SubClass], "MusicFile", string.Empty);
                            string MD5Value = GetXMLElementAttribute(skater[SubClass], "MusicFile", "MD5", string.Empty);
                            try
                            {
                                //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                                audioFileReaderTest = new AudioFileReader(musicFile);
                                // Check MD5 for musicfile so there hasn't been a change of file since imported.
                                if (MD5Value != string.Empty && MD5Value != GetMD5HashFromFile(musicFile))
                                {
                                    I.SubItems.Add("Checksum error");
                                    I.SubItems.Add(musicFile);
                                    I.ForeColor = Color.Orange;
                                }
                                else
                                {// MD5 is correct!
                                    I.SubItems.Add(String.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds));
                                    I.SubItems.Add(musicFile);
                                }
                            }
                            catch (Exception)
                            {
                                // Can't read file
                                if (musicFile == string.Empty)
                                {
                                    I.SubItems.Add("No file");
                                }
                                else
                                {
                                    I.SubItems.Add("Invalid file");
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

                            LV.Items.Add(I);
                        }

                        // Resize columns to show all content
                        LV.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); // StartNo
                        LV.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // Name
                        LV.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // Club
                        LV.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize); // Length
                        LV.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); // Music file

                    }
                }
            }
        }

        public void LoadSkatersDV(XmlDocument doc, string selected, DataGridView DV)
        {
            // Split selected itemtext into Main and Sub classes.
            string MainClass = selected.Substring(0, selected.Length - 8);
            string SubClass = selected.Substring(selected.Length - 5, 5).Trim();
            string SecondSubClass = "Short";
            if (SubClass == "Short")
            {
                SecondSubClass = "Free";
            }

            DV.Rows.Clear();

            // Initialize audioreader so we can verify if the files found are playable
            AudioFileReader audioFileReaderTest = null;

            if (doc.DocumentElement != null)
            {
                // Loop throu all Classes to find the selected one
                foreach (XmlNode tableNode in doc.DocumentElement.GetElementsByTagName("Class"))
                {
                    // Is it the correct class?
                    if (tableNode.Attributes.GetNamedItem("Name").Value == MainClass)
                    {
                        foreach (XmlNode skater in tableNode.ChildNodes)
                        {

                            DV.Rows.Add();
                            int rownr = DV.Rows.Count - 2;
                            DV[0, rownr].Value = GetXMLElement(skater[SubClass], "StartNo", "");
                            DV[1, rownr].Value = GetXMLElement(skater["FirstName"], string.Empty, string.Empty);
                            DV[2, rownr].Value = GetXMLElement(skater["LastName"], string.Empty, string.Empty);
                            DV[3, rownr].Value = GetXMLElement(skater["Club"], string.Empty, string.Empty);
                            if (skater.Attributes.GetNamedItem("ID") != null)
                            {
                                DV[4, rownr].Value = skater.Attributes.GetNamedItem("ID").Value;
                            }
                            if (skater.Attributes.GetNamedItem("BirthDate") != null)
                            {
                                DV[5, rownr].Value = skater.Attributes.GetNamedItem("BirthDate").Value;
                            }

                            // Store information for the other class, so we can recreate the element when we save
                            DV[10, rownr].Value = GetXMLElement(skater[SecondSubClass], "StartNo", "");
                            DV[11, rownr].Value = GetXMLElement(skater[SecondSubClass], "MusicFile", string.Empty);
                            DV[12, rownr].Value = GetXMLElementAttribute(skater[SecondSubClass], "MusicFile", "MD5", string.Empty);

                            // Load music file and information, and check the file information
                            string musicFile = GetXMLElement(skater[SubClass], "MusicFile", string.Empty);
                            DV[8, rownr].Value = musicFile;
                            string MD5Value = GetXMLElementAttribute(skater[SubClass], "MusicFile", "MD5", string.Empty);
                            DV[9, rownr].Value = MD5Value;
                            try
                            {
                                //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                                audioFileReaderTest = new AudioFileReader(musicFile);
                                // Check MD5 for musicfile so there hasn't been a change of file since imported.
                                if (MD5Value == string.Empty || MD5Value == GetMD5HashFromFile(musicFile))
                                {// MD5 is correct!
                                    DV[7, rownr].Value = String.Format("{0:00}:{1:00}", (int)audioFileReaderTest.TotalTime.TotalMinutes, audioFileReaderTest.TotalTime.Seconds);
                                }
                                else
                                {// MD5 doesn't match!
                                    DV[7, rownr].Value = "Checksum error";
                                    DV.Rows[rownr].DefaultCellStyle.ForeColor = Color.Orange;
                                }
                            }
                            catch (Exception)
                            {
                                // Can't read file
                                if (musicFile == string.Empty)
                                {
                                    DV[7, rownr].Value = "No file";
                                }
                                else
                                {
                                    DV[7, rownr].Value = "Invalid file";
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

        public void LoadIndTA1(XmlDocument doc, string indTAFilename)
        {
            XmlDocument indTA = new XmlDocument();


            //Load XmlFile
            try
            {

                indTA.Load(indTAFilename);

                if (indTA.DocumentElement != null)
                {
                    // Set competition name från EventHeader
                    foreach (XmlNode tableNode in indTA.DocumentElement.GetElementsByTagName("EventHeader"))
                    {// Set competition name
                        doc.DocumentElement.SetAttribute("Name", GetXMLElement(tableNode["EventName"], "", ""));
                    }

                    // Loop Person
                    foreach (XmlNode personNodeIndTA in indTA.DocumentElement.GetElementsByTagName("Person"))
                    {
                        string className = GetXMLElement(personNodeIndTA.ParentNode.ParentNode.ParentNode["Class"], "", "");

                        //Find the class in Competition
                        XmlNode classNode = null;
                        foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName("Class"))
                        {
                            if (node.Attributes.GetNamedItem("Name").Value == className)
                            {
                                classNode = node;
                            }
                        }

                        // If class not found, create a new class
                        if (classNode == null)
                        {//New class. Create structure
                            classNode = doc.CreateElement("Class");
                            XmlAttribute attributeName = doc.CreateAttribute("Name");
                            attributeName.Value = className;
                            classNode.Attributes.Append(attributeName);
                            //if (hasSHort??)
                            //{
                            //    XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                            //    attributeShort.Value = "true";
                            //    classNode.Attributes.Append(attributeShort);
                            //}
                            doc.DocumentElement.AppendChild(classNode);
                        }

                        // Locate skater for update or create a skater
                        string ID = personNodeIndTA.Attributes.GetNamedItem("Id").Value;

                        // Try to find if skater already present using ID numer från indTA and match with ID from Competition
                        XmlNode personNode = null;
                        if (classNode.HasChildNodes)
                        {
                            foreach (XmlNode personNodeDoc in classNode)
                            {
                                if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                {
                                    personNode = personNodeDoc;
                                }
                            }

                        }

                        // If person not found, create a new person
                        if (personNode == null)
                        {
                            personNode = doc.CreateElement("Skater");
                            XmlAttribute attributeID = doc.CreateAttribute("ID");
                            attributeID.Value = ID;
                            personNode.Attributes.Append(attributeID);

                            classNode.AppendChild(personNode);
                        }

                        // Make sure we have elements
                        if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                        if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                        if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));

                        // Update 
                        personNode["FirstName"].InnerText = GetXMLElement(personNodeIndTA["Firstname"], "", "");
                        personNode["LastName"].InnerText = GetXMLElement(personNodeIndTA["Lastname"], "", "");
                        personNode["Club"].InnerText = GetXMLElement(personNodeIndTA["Organization"], "", "");
                    }

                    doc.Save("competition.xml");

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading IndTA XML-file\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void LoadIndTA2(XmlDocument doc, string indTAFilename)
        {
            XmlDocument indTA = new XmlDocument();


            //Load XmlFile
            try
            {

                indTA.Load(indTAFilename);

                if (indTA.DocumentElement != null)
                {
                    // Set competition name från EventHeader
                    foreach (XmlNode tableNode in indTA.DocumentElement.GetElementsByTagName("EventHeader"))
                    {// Set competition name
                        doc.DocumentElement.SetAttribute("Name", GetXMLElement(tableNode["EventName"], "", ""));
                    }

                    // Loop Person
                    foreach (XmlNode personNodeIndTA in indTA.DocumentElement.GetElementsByTagName("Person"))
                    {
                        string className = GetXMLElement(personNodeIndTA.ParentNode.ParentNode["Class"], "", "");

                        //Find the class in Competition
                        XmlNode classNode = null;
                        foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName("Class"))
                        {
                            if (node.Attributes.GetNamedItem("Name").Value == className)
                            {
                                classNode = node;
                            }
                        }

                        // If class not found, create a new class
                        if (classNode == null)
                        {//New class. Create structure
                            classNode = doc.CreateElement("Class");
                            XmlAttribute attributeName = doc.CreateAttribute("Name");
                            attributeName.Value = className;
                            classNode.Attributes.Append(attributeName);
                            //if (hasSHort??)
                            //{
                            //    XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                            //    attributeShort.Value = "true";
                            //    classNode.Attributes.Append(attributeShort);
                            //}
                            doc.DocumentElement.AppendChild(classNode);
                        }

                        // Locate skater for update or create a skater
                        string ID = GetXMLElement(personNodeIndTA["IdrottOnlineID"], "", "");
                        string FirstName = GetXMLElement(personNodeIndTA["FirstName"], "", "");
                        string LastName = GetXMLElement(personNodeIndTA["SurName"], "", "");
                        string Club = GetXMLElement(personNodeIndTA["Organization"], "OrganizationName", "");

                        // Try to find if skater already present using ID number from indTA and match with ID from Competition
                        XmlNode personNode = null;
                        if (classNode.HasChildNodes && ID != string.Empty)
                        {
                            foreach (XmlNode personNodeDoc in classNode)
                            {
                                if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                {
                                    personNode = personNodeDoc;
                                }
                            }
                        }


                        // If we didn't find skater with ID, try to find if skater already present using First-, Lastname and Club to match from Competition
                        if (personNode == null)
                        {
                            if (classNode.HasChildNodes)
                            {
                                foreach (XmlNode personNodeDoc in classNode)
                                {
                                    if (GetXMLElement(personNodeDoc["FirstName"], "", "") == FirstName &&
                                        GetXMLElement(personNodeDoc["LastName"], "", "") == LastName &&
                                        GetXMLElement(personNodeDoc["Club"], "", "") == Club)
                                    {
                                        personNode = personNodeDoc;
                                    }
                                }

                            }
                        }


                        // If person not found, create a new person
                        if (personNode == null)
                        {
                            personNode = doc.CreateElement("Skater");
                            XmlAttribute attributeID = doc.CreateAttribute("ID");
                            attributeID.Value = ID;
                            personNode.Attributes.Append(attributeID);
                            XmlAttribute attributeBD = doc.CreateAttribute("BirthDate");
                            attributeBD.Value = GetXMLElement(personNodeIndTA["BirthDate"], "", ""); ;
                            personNode.Attributes.Append(attributeBD);

                            classNode.AppendChild(personNode);
                        }

                        // Make sure we have elements
                        if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                        if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                        if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));

                        // Update 
                        personNode["FirstName"].InnerText = FirstName;
                        personNode["LastName"].InnerText = LastName;
                        personNode["Club"].InnerText = Club;
                    }

                    doc.Save("competition.xml");

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading IndTA XML-file\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadClubStarComp(XmlDocument doc, string p)
        {
            //Try to find all XML files in the folder
            FileInfo[] fiArray = null;
            try
            {
                fiArray = new DirectoryInfo(p).GetFiles("*.xml");
            }
            catch (Exception)
            {
            }

            // Loop thru all files
            foreach (FileInfo fi in fiArray)
            {
                XmlDocument CCXML = new XmlDocument();
                string className = string.Empty;

                //Load XmlFile
                try
                {

                    CCXML.Load(fi.FullName);

                    if (CCXML.DocumentElement != null)
                    {
                        // Set competition name från EventHeader
                        foreach (XmlNode tableNode in CCXML.DocumentElement.GetElementsByTagName("CompetitionHeader"))
                        {// Set competition name
                            doc.DocumentElement.SetAttribute("CompetitionName", GetXMLElement(tableNode["EventName"], "", ""));
                            className = GetXMLElement(tableNode["Category"], "", "");
                        }

                        // Loop Person
                        foreach (XmlNode personNodeIndTA in CCXML.DocumentElement.GetElementsByTagName("Person"))
                        {

                            //Find the class in Competition
                            XmlNode classNode = null;
                            foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName("Class"))
                            {
                                if (node.Attributes.GetNamedItem("Name").Value == className)
                                {
                                    classNode = node;
                                }
                            }

                            // If class not found, create a new class
                            if (classNode == null)
                            {//New class. Create structure
                                classNode = doc.CreateElement("Class");
                                XmlAttribute attributeName = doc.CreateAttribute("Name");
                                attributeName.Value = className;
                                classNode.Attributes.Append(attributeName);
                                //if (hasSHort??)
                                //{
                                //    XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                                //    attributeShort.Value = "true";
                                //    classNode.Attributes.Append(attributeShort);
                                //}
                                doc.DocumentElement.AppendChild(classNode);
                            }

                            // Locate skater for update or create a skater
                            //string ID = ""; // No ID from Club/Starcomp  //personNodeIndTA.Attributes.GetNamedItem("Id").Value;
                            string FirstName = GetXMLElement(personNodeIndTA["FirstName"], "", "");
                            string LastName = GetXMLElement(personNodeIndTA["LastName"], "", "");
                            string Club = GetXMLElement(personNodeIndTA["Club"], "", "");

                            // Try to find if skater already present using First-, Lastname and Club to match from Competition
                            XmlNode personNode = null;
                            if (classNode.HasChildNodes)
                            {
                                foreach (XmlNode personNodeDoc in classNode)
                                {
                                    if (GetXMLElement(personNodeDoc["FirstName"], "", "") == FirstName &&
                                        GetXMLElement(personNodeDoc["LastName"], "", "") == LastName &&
                                        GetXMLElement(personNodeDoc["Club"], "", "") == Club)
                                    {
                                        personNode = personNodeDoc;
                                    }
                                }

                            }

                            // If person not found, create a new person
                            if (personNode == null)
                            {
                                personNode = doc.CreateElement("Skater");
                                //XmlAttribute attributeID = doc.CreateAttribute("ID");
                                //attributeID.Value = ID;
                                //personNode.Attributes.Append(attributeID);

                                classNode.AppendChild(personNode);
                            }

                            // Make sure we have elements
                            if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                            if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                            if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));

                            // Update 
                            personNode["FirstName"].InnerText = FirstName;
                            personNode["LastName"].InnerText = LastName;
                            personNode["Club"].InnerText = Club;
                        }

                        doc.Save("competition.xml");

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error loading Clubcomp/Starcomp XML-file\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void LoadClubStarComp2016(XmlDocument doc, string p)
        {
            //Try to find all XML files in the folder
            FileInfo[] fiArray = null;
            try
            {
                fiArray = new DirectoryInfo(p).GetFiles("*.xml");
            }
            catch (Exception)
            {
            }

            // Loop thru all files
            foreach (FileInfo fi in fiArray)
            {
                XmlDocument CCXML = new XmlDocument();
                string className = string.Empty;

                //Load XmlFile
                try
                {

                    CCXML.Load(fi.FullName);

                    if (CCXML.DocumentElement != null)
                    {
                        // Set competition name från EventHeader
                        foreach (XmlNode tableNode in CCXML.DocumentElement.GetElementsByTagName("CompetitionHeader"))
                        {// Set competition name
                            doc.DocumentElement.SetAttribute("CompetitionName", GetXMLElementAttribute(tableNode["Competition"], "", "Name", ""));
                            // try to get the class name from IndTA
                            className = GetXMLElementAttribute(tableNode["IndTA"], "", "IndTAClass", "");

                            // if we didn't find a classname from IndTA, get Clubcomp/Starcomp classname
                            if (className == "")
                            {
                                className = GetXMLElement(tableNode["Category"], "", "");
                                //Try to find subcategory
                                className = (className + " " + GetXMLElement(tableNode["SubCategory"], "", "")).Trim();
                                //Add Group if it exists
                                className = (className + " " + GetXMLElement(tableNode["GroupNo"], "", "")).Trim();
                            }
                        }


                        //Find the class in Competition
                        XmlNode classNode = null;
                        foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName("Class"))
                        {
                            if (node.Attributes.GetNamedItem("Name").Value == className)
                            {
                                classNode = node;
                            }
                        }

                        // If class not found, create a new class
                        if (classNode == null)
                        {//New class. Create structure
                            classNode = doc.CreateElement("Class");
                            XmlAttribute attributeName = doc.CreateAttribute("Name");
                            attributeName.Value = className;
                            classNode.Attributes.Append(attributeName);
                            //if (hasSHort??)
                            //{
                            //    XmlAttribute attributeShort = doc.CreateAttribute("HasShort");
                            //    attributeShort.Value = "true";
                            //    classNode.Attributes.Append(attributeShort);
                            //}
                            doc.DocumentElement.AppendChild(classNode);
                        }


                        // Loop Person
                        foreach (XmlNode personNodeIndTA in CCXML.DocumentElement.GetElementsByTagName("Person"))
                        {
                            // Locate skater for update or create a skater
                            string ID = GetXMLElementAttribute(personNodeIndTA["Skater"], "", "ID", "");
                            string StartNo1 = "";
                            string StartNo2 = GetXMLElementAttribute(personNodeIndTA["Skater"], "", "StartNo", "");
                            string FirstName = GetXMLElementAttribute(personNodeIndTA["Skater"], "", "Firstname", "");
                            string LastName = GetXMLElementAttribute(personNodeIndTA["Skater"], "", "Surname", "");
                            string Club = GetXMLElementAttribute(personNodeIndTA["Skater"], "", "Club", "");  //Startcomp

                            if (Club == "")
                            {// If we didn't get a Clubname, it's probably a Clubcomp file, read values from other places
                                StartNo1 = GetXMLElementAttribute(personNodeIndTA["Startno"], "", "Seg1", "");
                                StartNo2 = GetXMLElementAttribute(personNodeIndTA["Startno"], "", "Seg2", "");
                                Club = GetXMLElementAttribute(personNodeIndTA["Club"], "", "Name", "");  //Clubcomp
                            }

                            // Try to find if skater already present using ID number from indTA and match with ID from Competition
                            XmlNode personNode = null;
                            if (classNode.HasChildNodes && ID != string.Empty)
                            {
                                foreach (XmlNode personNodeDoc in classNode)
                                {
                                    if (personNodeDoc.Attributes.GetNamedItem("ID") != null && personNodeDoc.Attributes.GetNamedItem("ID").Value == ID)
                                    {
                                        personNode = personNodeDoc;
                                    }
                                }
                            }

                            // If we didn't find skater with ID, try to find if skater already present using First-, Lastname and Club to match from Competition
                            if (personNode == null)
                            {
                                if (classNode.HasChildNodes)
                                {
                                    foreach (XmlNode personNodeDoc in classNode)
                                    {
                                        if (GetXMLElement(personNodeDoc["FirstName"], "", "") == FirstName &&
                                            GetXMLElement(personNodeDoc["LastName"], "", "") == LastName &&
                                            GetXMLElement(personNodeDoc["Club"], "", "") == Club)
                                        {
                                            personNode = personNodeDoc;
                                        }
                                    }

                                }
                            }

                            // If person not found, create a new person
                            if (personNode == null)
                            {
                                personNode = doc.CreateElement("Skater");
                                XmlAttribute attributeID = doc.CreateAttribute("ID");
                                attributeID.Value = ID;
                                personNode.Attributes.Append(attributeID);
                                XmlAttribute attributeBD = doc.CreateAttribute("BirthDate");
                                attributeBD.Value = string.Empty;
                                personNode.Attributes.Append(attributeBD);

                                classNode.AppendChild(personNode);
                            }

                            // Make sure we have elements
                            if (personNode["FirstName"] == null) personNode.AppendChild(doc.CreateElement("FirstName"));
                            if (personNode["LastName"] == null) personNode.AppendChild(doc.CreateElement("LastName"));
                            if (personNode["Club"] == null) personNode.AppendChild(doc.CreateElement("Club"));
                            if (personNode["Short"] == null) personNode.AppendChild(doc.CreateElement("Short"));
                            if (personNode["Short"]["StartNo"] == null) personNode["Short"].AppendChild(doc.CreateElement("StartNo"));
                            if (personNode["Free"] == null) personNode.AppendChild(doc.CreateElement("Free"));
                            if (personNode["Free"]["StartNo"] == null) personNode["Free"].AppendChild(doc.CreateElement("StartNo"));

                            // Update 
                            personNode["FirstName"].InnerText = FirstName;
                            personNode["LastName"].InnerText = LastName;
                            personNode["Club"].InnerText = Club;
                            personNode["Short"]["StartNo"].InnerText = StartNo1.PadLeft(3, ' '); ;
                            personNode["Free"]["StartNo"].InnerText = StartNo2.PadLeft(3, ' '); ;
                        }

                        doc.Save("competition.xml");

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error loading Clubcomp/Starcomp XML-file\n" + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void autoconnectMusic()
        {
            string MissingFiles = string.Empty;

            foreach (XmlNode skaterNode in doc.DocumentElement.GetElementsByTagName("Skater"))
            {
                string BirthDate = "";
                if (skaterNode.Attributes.GetNamedItem("BirthDate") != null)
                {
                    BirthDate = skaterNode.Attributes.GetNamedItem("BirthDate").Value.Replace("-", "");
                }
                // Free music
                // Search for music
                FileInfo[] fiArray = null;
                try
                {
                    //fiArray = new DirectoryInfo(@"CompetitionMusic\" + skaterNode.ParentNode.Attributes.GetNamedItem("Name").Value.ToString().Replace(" ", "_") + @"\").GetFiles(GetXMLElement(skaterNode["FirstName"], "", "") + "_" + GetXMLElement(skaterNode["LastName"], "", "") + "_" + GetXMLElement(skaterNode["Club"], "", "") + "_Free*.*");
                    fiArray = new DirectoryInfo(@"CompetitionMusic\").GetFiles(GetXMLElement(skaterNode["FirstName"], "", "").Replace(" ", "_") + "_" + GetXMLElement(skaterNode["LastName"], "", "").Replace(" ", "_") + "_" + BirthDate + "*_Friåkning.*");
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

                    string MD5 = VerifyMusicFile(SearchFile);
                    if (MD5 == String.Empty)
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
                            if (skaterNode["Free"] == null) skaterNode.AppendChild(doc.CreateElement("Free"));
                            if (skaterNode["Free"]["MusicFile"] == null) skaterNode["Free"].AppendChild(doc.CreateElement("MusicFile"));

                            skaterNode["Free"]["MusicFile"].InnerText = SearchFile;
                            skaterNode["Free"]["MusicFile"].SetAttribute("MD5", MD5);
                        }
                    }
                }
                else
                {
                    MissingFiles = MissingFiles + @"CompetitionMusic\" + GetXMLElement(skaterNode["FirstName"], "", "").Replace(" ", "_") + "_" + GetXMLElement(skaterNode["LastName"], "", "").Replace(" ", "_") + "_" + BirthDate + "*_Friåkning.*" + "\n";
                }



                // Does the Class contain have Short?
                if (skaterNode.ParentNode.Attributes.GetNamedItem("HasShort") != null)
                {
                    // Short music
                    // Search for music
                    fiArray = null;
                    try
                    {
                        //fiArray = new DirectoryInfo(@"CompetitionMusic\" + skaterNode.ParentNode.Attributes.GetNamedItem("Name").Value.ToString().Replace(" ", "_") + @"\").GetFiles(GetXMLElement(skaterNode["FirstName"], "", "") + "_" + GetXMLElement(skaterNode["LastName"], "", "") + "_" + GetXMLElement(skaterNode["Club"], "", "") + "_Short*.*");
                        fiArray = new DirectoryInfo(@"CompetitionMusic\").GetFiles(GetXMLElement(skaterNode["FirstName"], "", "").Replace(" ", "_") + "_" + GetXMLElement(skaterNode["LastName"], "", "").Replace(" ", "_") + "_" + BirthDate + "*_Kortprogram.*");
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

                        string MD5 = VerifyMusicFile(SearchFile);
                        if (MD5 == String.Empty)
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
                                if (skaterNode["Short"] == null) skaterNode.AppendChild(doc.CreateElement("Short"));
                                if (skaterNode["Short"]["MusicFile"] == null) skaterNode["Short"].AppendChild(doc.CreateElement("MusicFile"));

                                skaterNode["Short"]["MusicFile"].InnerText = SearchFile;
                                skaterNode["Short"]["MusicFile"].SetAttribute("MD5", MD5);
                            }
                        }
                    }
                    else
                    {
                        //MissingFiles = MissingFiles + @"CompetitionMusic\" + skaterNode.ParentNode.Attributes.GetNamedItem("Name").Value.ToString().Replace(" ", "_") + @"\" + GetXMLElement(skaterNode["FirstName"], "", "") + "_" + GetXMLElement(skaterNode["LastName"], "", "") + "_" + GetXMLElement(skaterNode["Club"], "", "") + "_Short*.*" + "\n";
                        MissingFiles = MissingFiles + @"CompetitionMusic\" + GetXMLElement(skaterNode["FirstName"], "", "").Replace(" ", "_") + "_" + GetXMLElement(skaterNode["LastName"], "", "").Replace(" ", "_") + "_" + BirthDate + "*_Kortprogram.*" + "\n";
                    }
                }



            } // foreach skaterNode

            if (MissingFiles != string.Empty)
            {
                MessageBox.Show(MissingFiles, "Missing music files", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("All skaters music has been connected", "Autoconnect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            doc.Save("competition.xml");
        }

        private string VerifyMusicFile(string FileName)
        {
            string MD5 = string.Empty;
            AudioFileReader audioFileReaderTest = null;
            try
            {
                //Try to load the file to see if NAudio can read it. Gives an exception if we can't read it
                audioFileReaderTest = new AudioFileReader(FileName);
                MD5 = GetMD5HashFromFile(FileName);
                //TODO:Check MD5 for alla skaters to verify no dublicates
                foreach (XmlNode MusicFileNode in doc.DocumentElement.GetElementsByTagName("MusicFile"))
                {
                    if (FileName != MusicFileNode.InnerText)
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

    }
}
