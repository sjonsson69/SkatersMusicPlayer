using MySqlConnector;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static SkatersMusicPlayer.formMusicPlayer;

namespace SkatersMusicPlayer
{
    public partial class formMusicPlayer : Form
    {
        // Define the root object representing the entire JSON file
#nullable enable
        public class competitionEvent
        {
            // The main name of the competition
            [JsonProperty("competitionName")]
            public string competitionName { get; set; } = string.Empty;

            // A list of all segments (e.g., "Junior Men - Short Program") in the competition
            [JsonProperty("categoriesAndSegments")]
            public List<categorySegment> categoriesAndSegments { get; set; } = new List<categorySegment>();

            // --- Example Usage of Deserialization (Optional, for testing) ---
            /*
            public static CompetitionEvent? FromJson(string json)
            {
                return JsonConvert.DeserializeObject<CompetitionEvent>(json);
            }
            */
        }

        // Represents a single category/segment within the competition
        public class categorySegment
        {
            [JsonProperty("discipline")]
            public string discipline { get; set; } = string.Empty;

            [JsonProperty("category")]
            public string category { get; set; } = string.Empty;

            [JsonProperty("segment")]
            public string segment { get; set; } = string.Empty;

            // The list of participants registered for this specific segment
            [JsonProperty("participants")]
            public List<participant> participants { get; set; } = new List<participant>();
        }

        // Represents a single athlete/participant
        public class participant
        {
            // Using Guid for the ID, which is a good type for UUID strings
            [JsonProperty("id")]
            public Guid? id { get; set; }

            // Using nullable integer (int?) because 'startNumber' can be null in the JSON
            [JsonProperty("startNumber")]
            public int? startNumber { get; set; }

            [JsonProperty("firstName")]
            public string? firstName { get; set; }

            [JsonProperty("lastName")]
            public string? lastName { get; set; }

            // Using DateTime to correctly parse the date string (e.g., "2007-03-22")
            [JsonProperty("birthDate")]
            public DateTime? birthDate { get; set; }

            [JsonProperty("club")]
            public string? club { get; set; }

            // Nested object for music details
            [JsonProperty("music")]
            public competitionMusic? music { get; set; }
        }

        // Represents the music details for a participant's segment
        public class competitionMusic
        {
            // Using nullable string (string?) because 'file', 'md5', and 'title' can be null
            [JsonProperty("file")]
            public string? file { get; set; }

            [JsonProperty("md5")]
            public string? md5 { get; set; }

            [JsonProperty("title")]
            public string? title { get; set; }
        }
#nullable disable

        #region SportTAJO Classes
        public class SportTAJO
        {
            [JsonProperty("ExportedAt")]
            public DateTime? ExportedAt { get; set; }

            [JsonProperty("ExportedBy")]
            public Guid? ExportedBy { get; set; }

            [JsonProperty("Event")]
            public Event Event { get; set; }
        }

        public class Event
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Organizer")]
            public Organization Organizer { get; set; }

            [JsonProperty("Location")]
            public string Location { get; set; }

            [JsonProperty("Competitions")]
            public List<Competition> Competitions { get; set; }
        }

        public class Organization
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }
        }

        public class Competition
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Category")]
            public string Category { get; set; }

            [JsonProperty("StartDate")]
            public DateTime? StartDate { get; set; }

            [JsonProperty("EndDate")]
            public DateTime? EndDate { get; set; }

            [JsonProperty("Classes")]
            public List<Class> Classes { get; set; }
        }

        public class Class
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Discipline")]
            public string Discipline { get; set; }

            [JsonProperty("Type")]
            public string Type { get; set; }

            [JsonProperty("Groups")]
            public List<Group> Groups { get; set; }

            [JsonProperty("Reserves")]
            public List<Reserve> Reserves { get; set; }
        }

        public class Group
        {
            [JsonProperty("Index")]
            public int Index { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Persons")]
            public List<Person> Persons { get; set; }

            [JsonProperty("Pairs")]
            public List<Pair> Pairs { get; set; } // For "Type": "Par"

            [JsonProperty("Teams")]
            public List<Team> Teams { get; set; } // For "Type": "Lag"

            [JsonProperty("Officials")]
            public List<Official> Officials { get; set; }
        }

        public class Reserve
        {
            [JsonProperty("Persons")]
            public List<Person> Persons { get; set; }

            [JsonProperty("Pairs")]
            public List<Pair> Pairs { get; set; } // For "Type": "Par"

            [JsonProperty("Teams")]
            public List<Team> Teams { get; set; } // For "Type": "Lag"

        }

        public class Person
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("FirstName")]
            public string FirstName { get; set; }

            [JsonProperty("LastName")]
            public string LastName { get; set; }

            [JsonProperty("BirthDate")]
            public DateTime? BirthDate { get; set; }

            [JsonProperty("Sex")]
            public string Sex { get; set; }

            [JsonProperty("Nationality")]
            public string Nationality { get; set; }

            [JsonProperty("Organization")]
            public Organization Organization { get; set; }

            [JsonProperty("District")]
            public District District { get; set; }

            [JsonProperty("EntryDate")]
            public DateTime? EntryDate { get; set; }

            [JsonProperty("EntryId")]
            public Guid? EntryId { get; set; }

            [JsonProperty("Ppcs")]
            public List<Ppc> Ppcs { get; set; }

            [JsonProperty("Role")]
            public string Role { get; set; } // Only present in "Teams"

            [JsonProperty("Result")]
            public Result Result { get; set; }

        }

        public class Pair
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Organization")]
            public Organization Organization { get; set; }

            [JsonProperty("District")]
            public District District { get; set; }

            [JsonProperty("EntryDate")]
            public DateTime EntryDate { get; set; }

            [JsonProperty("EntryId")]
            public Guid EntryId { get; set; }

            [JsonProperty("Ppcs")]
            public List<Ppc> Ppcs { get; set; }

            [JsonProperty("Persons")]
            public List<Person> Persons { get; set; }
        }

        public class Team
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Organization")]
            public Organization Organization { get; set; }

            [JsonProperty("District")]
            public District District { get; set; }

            [JsonProperty("EntryDate")]
            public DateTime EntryDate { get; set; }

            [JsonProperty("EntryId")]
            public Guid EntryId { get; set; }

            [JsonProperty("Ppcs")]
            public List<Ppc> Ppcs { get; set; }

            [JsonProperty("Persons")]
            public List<Person> Persons { get; set; }
        }

        public class Result
        {
            [JsonProperty("Placement")]
            public int? Placement { get; set; }

            [JsonProperty("TotalPoints")]
            public string TotalPoints { get; set; }

            [JsonProperty("Type")]
            public string Type { get; set; } //1=normal result, 2=Withdrawn
        }

        public class District
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }
        }

        public class Ppc
        {
            [JsonProperty("Type")]
            public string Type { get; set; }

            [JsonProperty("Discipline")]
            public string Discipline { get; set; }

            [JsonProperty("Ppc")] // Note: This property name is "Ppc" in JSON
            public string PpcValue { get; set; } // Changed to PpcValue to avoid name conflict with class

            [JsonProperty("Coach")]
            public string Coach { get; set; }

            [JsonProperty("Music")]
            public Music Music { get; set; }
        }

        public class Music
        {
            [JsonProperty("Compositor")]
            public string Compositor { get; set; }

            [JsonProperty("Title")]
            public string Title { get; set; }
        }

        public class Official
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("FirstName")]
            public string FirstName { get; set; }

            [JsonProperty("LastName")]
            public string LastName { get; set; }

            [JsonProperty("Role")]
            public string Role { get; set; }

            [JsonProperty("Sex")]
            public string Sex { get; set; }

            [JsonProperty("Nationality")]
            public string Nationality { get; set; }

            [JsonProperty("Representing")]
            public string Representing { get; set; }

            [JsonProperty("Organization")]
            public Organization Organization { get; set; }

            [JsonProperty("District")]
            public District District { get; set; }
        }
        #endregion



        public static void serializeToFile(competitionEvent compEvent, string filePath)
        {
            var json = JsonConvert.SerializeObject(compEvent, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

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

        private void loadJsonFile()
        {
            // Load JsonFile
            try
            {
                if (File.Exists(Properties.Resources.JSON_FILENAME))
                {
                    string json = File.ReadAllText(Properties.Resources.JSON_FILENAME);
                    compEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<competitionEvent>(json);
                }
                else
                {
                    compEvent = new competitionEvent();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading Event JSON-file\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                compEvent = new competitionEvent();
            }

            // Set competitionname in form header
            this.Text = "Skaters MusicPlayer - " + compEvent.competitionName;

            // Empty Listview
            listViewParticipants.Items.Clear();
            listViewParticipants.Tag = null;

            // Load categories
            loadCategories(compEvent, comboBoxCategory);

        }

        public static void loadCategories(competitionEvent compEvent, ComboBox CB)
        {
            if (compEvent != null && CB != null)
            {
                try
                {
                    // Remove all old data
                    CB.Items.Clear();

                    if (compEvent.categoriesAndSegments.Count != 0)
                    {
                        //Loop for all categories
                        foreach (categorySegment cat in compEvent.categoriesAndSegments)
                        {
                            CB.Items.Add(combinedCategoryDisciplineSegmentName(cat));
                        }
                    }
                }
                catch (Exception e)
                {
                    _ = MessageBox.Show("Error loading categories\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public static string combinedCategoryDisciplineSegmentName(categorySegment cat)
        {
            //Build a name for the category with discipline, category and segment
            string categoryName = cat.category;

            // Add discipline if not singelåkning :TODO Fix for other languages
            if (!string.IsNullOrEmpty(cat.discipline) && cat.discipline != "Singelåkning")
            {
                categoryName = categoryName + " (" + cat.discipline + ")";
            }
            if (!string.IsNullOrEmpty(cat.segment))
            {
                categoryName = categoryName + " - " + cat.segment;
            }

            return categoryName;
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

        private void loadParticipants(competitionEvent compEvent, string selected, ListView LV)
        {
            // Remove all participants, and also remove Tag so we dont colorcode from last category/segment.
            LV.Items.Clear();
            LV.Tag = null;

            // Initialize audioreader so we can verify if the files found are playable
            AudioFileReader audioFileReaderTest = null;

            if (compEvent != null)
            {
                // Loop throu all Categories to find the selected one
                foreach (categorySegment cat in compEvent.categoriesAndSegments)
                {
                    // Is it the correct category?
                    if (combinedCategoryDisciplineSegmentName(cat) == selected)
                    {
                        foreach (participant par in cat.participants)
                        {
                            // Update infotext that we are working...
                            toolStripStatusLabel1.Text = "Loading participant:" + par?.firstName + " " + par?.lastName;
                            statusStrip1.Update();

                            ListViewItem I = new ListViewItem(par?.startNumber.ToString().PadLeft(3, ' ').TrimEnd());
                            I.SubItems.Add(par?.firstName + " " + par?.lastName);
                            I.SubItems.Add(par?.club);
                            // Returns the music file path or string.Empty if not available
                            string musicFile = par?.music?.file ?? string.Empty;
                            string MD5Value = par?.music?.md5 ?? string.Empty;
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
                                    I.SubItems.Add($"{(int)audioFileReaderTest.TotalTime.TotalMinutes:00}:{audioFileReaderTest.TotalTime.Seconds:00}");
                                    I.SubItems.Add(musicFile);
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

                            I.SubItems.Add(par?.music?.title ?? string.Empty);

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

        public static void loadParticipantsDV(competitionEvent compEvent, string selected, DataGridView DV)
        {
            if (compEvent != null && selected != null && DV != null)
            {
                DV.Rows.Clear();

                // Initialize audioreader so we can verify if the files found are playable
                AudioFileReader audioFileReaderTest = null;

                if (compEvent.categoriesAndSegments.Count != 0)
                {
                    // Loop throu all Categorieses to find the selected one
                    foreach (categorySegment cat in compEvent.categoriesAndSegments)
                    {
                        // Is it the correct category?
                        if (combinedCategoryDisciplineSegmentName(cat) == selected)
                        {
                            foreach (participant par in cat.participants)
                            {

                                DV.Rows.Add();
                                int rownr = DV.Rows.Count - 2;
                                DV[0, rownr].Value = par.startNumber.ToString().PadLeft(3, ' ').TrimEnd();
                                DV[1, rownr].Value = par?.firstName ?? string.Empty;
                                DV[2, rownr].Value = par?.lastName ?? string.Empty;
                                DV[3, rownr].Value = par?.club ?? string.Empty;
                                DV[4, rownr].Value = par?.id;
                                DV[5, rownr].Value = par?.birthDate;

                                DV[6, rownr].Value = par?.music?.title ?? string.Empty;

                                // Load music file and information, and check the file information
                                string musicFile = par?.music?.file ?? string.Empty;
                                DV[9, rownr].Value = musicFile;
                                string MD5Value = par?.music?.md5 ?? string.Empty;
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

        // The default list of segments to return if no specific rule is found.
        private static readonly List<string> _defaultSegments = new List<string> { "Free skating" };
        private static readonly Dictionary<string, List<string>> _segmentMap = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
    {
        // Add all your specific rules here.
        // Key: "Discipline/Category"
        // Value: List<string> of segments
        
        { "Singelåkning/Senior Damer", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Senior Herrar", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Senior Nationell Damer", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Junior Damer", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Junior Herrar", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Ungdom 16 Flickor", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Ungdom 16 Pojkar", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Ungdom 13 Flickor", new List<string> { "Short program", "Free skating" } },
        { "Singelåkning/Ungdom 13 Pojkar", new List<string> { "Short program", "Free skating" } },
        { "Paråkning/Senior", new List<string> { "Short program", "Free skating" } },
        { "Paråkning/Junior", new List<string> { "Short program", "Free skating" } },
        { "Paråkning/Advanced Novice", new List<string> { "Short program", "Free skating" } },
        { "Isdans/Senior", new List<string> { "Rhythm dance","Free dance" } },
        { "Isdans/Junior", new List<string> { "Rhythm dance","Free dance" } },
        { "Isdans/Advanced Novice", new List<string> { "Pattern dance 1","Pattern dance 2","Free dance" } },
        { "Isdans/Intermediate Novice", new List<string> { "Pattern dance 1","Pattern dance 2","Free dance" } },
        { "Isdans/Basic Novice", new List<string> { "Pattern dance 1","Pattern dance 2","Free dance" } },
        { "Isdans/Juvenile", new List<string> { "Pattern dance 1","Free dance" } },
        { "Isdans/Vit", new List<string> { "Free dance" } },
        { "Soloisdans/Senior", new List<string> { "Rhythm dance","Free dance" } },
        { "Soloisdans/Junior", new List<string> { "Rhythm dance","Free dance" } },
        { "Soloisdans/Advanced Novice", new List<string> { "Pattern dance 1","Pattern dance 2","Free dance" } },
        { "Soloisdans/Intermediate Novice", new List<string> { "Pattern dance 1","Pattern dance 2","Free dance" } },
        { "Soloisdans/Basic Novice", new List<string> { "Pattern dance 1","Pattern dance 2","Free dance" } },
        { "Soloisdans/Juvenile", new List<string> { "Pattern dance 1","Free dance" } },
        { "Soloisdans/Vit", new List<string> { "Free dance" } },
        { "Synkroniserad konståkning/Senior Elite 12", new List<string> { "Short program","Free skating" } },
        { "Synkroniserad konståkning/Senior", new List<string> { "Short program","Free skating" } },
        { "Synkroniserad konståkning/Junior", new List<string> { "Short program","Free skating" } },
        
        // Example for a category with 0 segments
        //{ "Synchro/Pre-Juvenile", new List<string>() }
    };

        /// <summary>
        /// Gets the list of segments for a given discipline and category.
        /// </summary>
        /// <param name="discipline">The event discipline (e.g., "Icedance")</param>
        /// <param name="category">The event category (e.g., "Senior")</param>
        /// <returns>A list of segment names. Returns ["Free skating"] if no specific rule is found.</returns>
        public static List<string> getSegments(string competitionType, string discipline, string category)
        {
            // Create the combined key. Using a '/' separator as in your example.
            string key = $"{discipline}/{category}";

            // TryGetValue is the most efficient way to look in a dictionary.
            // It tries to find the key and, if successful, puts the value into the 'segments' variable.
            if (_segmentMap.TryGetValue(key, out List<string> segments))
            {
                // We found a specific rule for this key.
                // If CompetitionType="Systme 1" only return the last value from segments
                if (competitionType.Equals("System 1", StringComparison.OrdinalIgnoreCase) && segments.Count > 0)
                {
                    return new List<string> { segments[segments.Count - 1] };
                }
                else
                {
                    return segments;
                }
            }
            else
            {
                // No rule was found. Return the default list.
                return _defaultSegments;
            }
        }

        private void loadSportTA(competitionEvent compEvent, string filename)
        {
            //Load Json
            try
            {
                //Number of competitions in file, to be able to inform the user if there are none.
                int nrOfCompetitions = 0;

                //Import and deserialize SportTAJO data
                SportTAJO sportTAJOData = JsonConvert.DeserializeObject<SportTAJO>(File.ReadAllText(filename));

                //Store Event/Competition name
                compEvent.competitionName = sportTAJOData.Event.Name;

                //-------------------------------------------------------------------------------
                //Loop for all competitions
                foreach (Competition comp in sportTAJOData.Event.Competitions)
                {
                    //Get Competition data
                    string CompetitionType = comp.Category.Trim();
                    string CompetitionName = comp.Name.Trim();

                    //Count number of competitions in file
                    nrOfCompetitions++;




                    //Loop categories(classes)
                    foreach (Class cat in comp.Classes)
                    {
                        //Loop for all groups in category
                        foreach (Group grp in cat.Groups)
                        {
                            string categoryName = grp.Name;
                            string discipline = cat.Discipline;

                            //Get a list of segments to process for the current discipline and category
                            List<string> segmentsToProcess = getSegments(CompetitionType, discipline, categoryName);

                            //Loop for all segments defined for the current discipline and category
                            foreach (string segment in segmentsToProcess)
                            {
                                //Find category in competition object compEvent
                                categorySegment category = null;
                                foreach (categorySegment catSeg in compEvent.categoriesAndSegments)
                                {
                                    if (catSeg.discipline == discipline &&
                                        catSeg.category == categoryName &&
                                        catSeg.segment == segment)
                                    {
                                        category = catSeg;
                                    }
                                }
                                // If category not found, create a new category
                                if (category == null)
                                {//New category. Create structure
                                    category = new categorySegment
                                    {
                                        discipline = discipline,
                                        category = categoryName,
                                        segment = segment,
                                        participants = new List<participant>()
                                    };
                                    compEvent.categoriesAndSegments.Add(category);
                                }


                                //Loop for all persons in group
                                if (grp.Persons != null)
                                {
                                    foreach (Person person in grp.Persons)
                                    {
                                        Guid? ID = person.Id;
                                        DateTime? Birthdate = person.BirthDate;
                                        string FirstName = person.FirstName.Trim();
                                        string LastName = person.LastName.Trim();
                                        string ClubName = string.Empty;
                                        string MusicTitle = string.Empty;

                                        //Get organization name
                                        if (person.Organization != null && person.Organization.Name != null)
                                        {
                                            ClubName = person.Organization.Name.Trim();
                                        }

                                        //Get PPC for Friåkning for music and coach
                                        foreach (var ppc in from Ppc ppc in person.Ppcs
                                                            where ppc.Type == translateSegmentToSWE(segment)
                                                            select ppc)
                                        {
                                            if (ppc.Music != null && ppc.Music.Title != null)
                                            {
                                                MusicTitle = ppc.Music.Title.Trim();
                                            }
                                        }

                                        // Locate participant in category via ID
                                        participant participant = null;
                                        if (ID != null) //Must have ID to search for participant
                                        {
                                            foreach (participant par in category.participants)
                                            {
                                                if (par.id == ID)
                                                {
                                                    participant = par;
                                                }
                                            }
                                        }
                                        // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                        if (participant == null)
                                        {
                                            foreach (participant par in category.participants)
                                            {
                                                if (par.firstName == FirstName &&
                                                    par.lastName == LastName &&
                                                    par.club == ClubName &&
                                                    par.birthDate == Birthdate)
                                                {
                                                    participant = par;
                                                }
                                            }
                                        }
                                        // If person not found, create a new person
                                        if (participant == null)
                                        {
                                            participant = new participant
                                            {
                                                id = ID,
                                                birthDate = Birthdate,
                                                music = new competitionMusic()
                                            };
                                            category.participants.Add(participant);
                                        }
                                        // Update
                                        participant.firstName = FirstName;
                                        participant.lastName = LastName;
                                        participant.club = ClubName;
                                        participant.music.title = MusicTitle;
                                    } //foreach Person in Group
                                } //if grp.Persons != null

                                //Loop for all pairs in group
                                if (grp.Pairs != null)
                                {
                                    foreach (Pair pair in grp.Pairs)
                                    {
                                        Guid? ID = pair.Id;
                                        string FirstName = pair.Persons?[0].FirstName.Trim() + " " + pair.Persons?[0].LastName.Trim() + ", " +
                                            pair.Persons?[1].FirstName.Trim() + " " + pair.Persons?[1].LastName.Trim();
                                        string ClubName = string.Empty;
                                        string MusicTitle = string.Empty;

                                        //Get organization name
                                        if (pair.Organization != null && pair.Organization.Name != null)
                                        {
                                            ClubName = pair.Organization.Name.Trim();
                                        }

                                        //Get PPC for Friåkning for music and coach
                                        foreach (var ppc in from Ppc ppc in pair.Ppcs
                                                            where ppc.Type == translateSegmentToSWE(segment)
                                                            select ppc)
                                        {
                                            if (ppc.Music != null && ppc.Music.Title != null)
                                            {
                                                MusicTitle = ppc.Music.Title.Trim();
                                            }
                                        }

                                        // Locate participant in category via ID
                                        participant participant = null;
                                        if (ID != null) //Must have ID to search for participant
                                        {
                                            foreach (participant par in category.participants)
                                            {
                                                if (par.id == ID)
                                                {
                                                    participant = par;
                                                }
                                            }
                                        }
                                        // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                        if (participant == null)
                                        {
                                            foreach (participant par in category.participants)
                                            {
                                                if (par.firstName == FirstName &&
                                                    par.club == ClubName)
                                                {
                                                    participant = par;
                                                }
                                            }
                                        }
                                        // If person not found, create a new person
                                        if (participant == null)
                                        {
                                            participant = new participant
                                            {
                                                id = ID,
                                                music = new competitionMusic()
                                            };
                                            category.participants.Add(participant);
                                        }
                                        // Update
                                        participant.firstName = FirstName;
                                        participant.club = ClubName;
                                        participant.music.title = MusicTitle;
                                    } //foreach Pair in Group
                                } //if grp.Pairs != null

                                //Loop for all pairs in group
                                if (grp.Teams != null)
                                {
                                    foreach (Team team in grp.Teams)
                                    {
                                        Guid? ID = team.Id;
                                        string FirstName = team.Name.Trim();
                                        string ClubName = string.Empty;
                                        string MusicTitle = string.Empty;

                                        //Get organization name
                                        if (team.Organization != null && team.Organization.Name != null)
                                        {
                                            ClubName = team.Organization.Name.Trim();
                                        }

                                        //Get PPC for Friåkning for music and coach
                                        foreach (var ppc in from Ppc ppc in team.Ppcs
                                                            where ppc.Type == translateSegmentToSWE(segment)
                                                            select ppc)
                                        {
                                            if (ppc.Music != null && ppc.Music.Title != null)
                                            {
                                                MusicTitle = ppc.Music.Title.Trim();
                                            }
                                        }

                                        // Locate participant in category via ID
                                        participant participant = null;
                                        if (ID != null) //Must have ID to search for participant
                                        {
                                            foreach (participant par in category.participants)
                                            {
                                                if (par.id == ID)
                                                {
                                                    participant = par;
                                                }
                                            }
                                        }
                                        // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                        if (participant == null)
                                        {
                                            foreach (participant par in category.participants)
                                            {
                                                if (par.firstName == FirstName &&
                                                    par.club == ClubName)
                                                {
                                                    participant = par;
                                                }
                                            }
                                        }
                                        // If person not found, create a new person
                                        if (participant == null)
                                        {
                                            participant = new participant
                                            {
                                                id = ID,
                                                music = new competitionMusic()
                                            };
                                            category.participants.Add(participant);
                                        }
                                        // Update
                                        participant.firstName = FirstName;
                                        participant.club = ClubName;
                                        participant.music.title = MusicTitle;
                                    } //foreach Team in Group
                                } //if grp.Teams != null
                            } //foreach Segments
                        } //foreach Groups
                    } //foreach Categories
                }//foreach competition

                //Save updates Musicplayer JSON file
                serializeToFile(compEvent, Properties.Resources.JSON_FILENAME);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading IndTA XML-file\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void loadFSM(competitionEvent compEvent, string database)
        {
            //Load FSM database
            try
            {
                var conString = new MySqlConnectionStringBuilder
                {
                    Server = settings.FSMServer,
                    Port = settings.FSMPort,
                    UserID = settings.FSMUsername,
                    Password = settings.FSMPassword,
                    Database = database
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
                                compName = compName + (string.IsNullOrEmpty(compName) ? "" : ", ") + getDBString(reader, "Name", string.Empty);
                            }
                            reader.Close();
                        }

                        //Store Event/Competition name
                        compEvent.competitionName = compName;

                        //Get Participants
                        cmd.CommandText = Properties.Resources.SQL_FSM_PARTICIPANTS;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Locate participant for update or create a participant
                                string discipline = getDBString(reader, "Discipline", string.Empty);
                                string categoryName = getDBString(reader, "Category", string.Empty);
                                string segment = getDBString(reader, "Segment", string.Empty);
                                Guid? ID = null;
                                if (Guid.TryParse(getDBString(reader, "FederationId", string.Empty), out Guid tempId))
                                {
                                    ID = tempId;
                                }

                                DateTime? Birthdate = null;
                                if (DateTime.TryParse(getDBString(reader, "BirthDate", string.Empty), out DateTime tempBD))
                                {
                                    Birthdate = tempBD;
                                }
                                string FirstName = getDBString(reader, "FirstName", string.Empty);
                                string LastName = getDBString(reader, "LastName", string.Empty);
                                string Club = getDBString(reader, "Club", string.Empty);
                                int? StartNo = null;
                                if (int.TryParse(getDBString(reader, "StartNumber", string.Empty), out int tempSN))
                                {
                                    StartNo = tempSN;
                                }
                                string MusicTitle = getDBString(reader, "Title", string.Empty);


                                //Find category in competition object compEvent
                                categorySegment category = null;
                                foreach (categorySegment cat in compEvent.categoriesAndSegments)
                                {
                                    if (cat.discipline == discipline &&
                                        cat.category == categoryName &&
                                        cat.segment == segment)
                                    {
                                        category = cat;
                                    }
                                }
                                // If category not found, create a new category
                                if (category == null)
                                {//New category. Create structure
                                    category = new categorySegment
                                    {
                                        discipline = discipline,
                                        category = categoryName,
                                        segment = segment,
                                        participants = new List<participant>()
                                    };
                                    compEvent.categoriesAndSegments.Add(category);
                                }

                                // Locate participant in category via ID
                                participant participant = null;
                                if (ID != null)
                                {
                                    foreach (participant par in category.participants)
                                    {
                                        if (par.id == ID)
                                        {
                                            participant = par;
                                        }
                                    }
                                }
                                // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                if (participant == null)
                                {
                                    foreach (participant par in category.participants)
                                    {
                                        if (par.firstName == FirstName &&
                                            par.lastName == LastName &&
                                            par.club == Club)
                                        {
                                            participant = par;
                                        }
                                    }
                                }
                                // If person not found, create a new person
                                if (participant == null)
                                {
                                    participant = new participant
                                    {
                                        id = ID,
                                        birthDate = Birthdate,
                                        music = new competitionMusic()
                                    };
                                    category.participants.Add(participant);
                                }
                                // Update
                                participant.startNumber = StartNo;
                                participant.firstName = FirstName;
                                participant.lastName = LastName;
                                participant.club = Club;
                                participant.music.title = MusicTitle;
                            }  //while reader-Participants

                            //Save updates Musicplayer JSON file
                            serializeToFile(compEvent, Properties.Resources.JSON_FILENAME);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading FS Manager database\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void loadStarFS(competitionEvent compEvent, string filename)
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
                        //Get competition name from database
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
                        compEvent.competitionName = compName;

                        //Get Participants
                        cmd.CommandText = "SELECT P.INDTA_ID, BIRTHDATE, FIRSTNAME, LASTNAME, CLUBNAME, CAST(START_NO AS TEXT) AS START_NO, MUSIC, DISCIPLINE, CATEGORY_NAME\n"
                                          + "FROM PARTICIPANT P\n"
                                          + "INNER JOIN BASEDATA B ON B.CATEGORY_ID = P.CATEGORY_ID\n"
                                          + "INNER JOIN CATEGORY C ON C.CATEGORY_ID = P.CATEGORY_ID\n"
                                          + "ORDER BY IFNULL(START_TIME, 'xxxxxx'), B.CATEGORY_ID, P.START_NO";
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Locate participant for update or create a participant
                                Guid? ID = null;
                                if (Guid.TryParse(getDBString(reader, "INDTA_ID", string.Empty), out Guid tempId))
                                {
                                    ID = tempId;
                                }

                                DateTime? Birthdate = null;
                                if (DateTime.TryParse(getDBString(reader, "BIRTHDATE", string.Empty), out DateTime tempBD))
                                {
                                    Birthdate = tempBD;
                                }
                                string FirstName = getDBString(reader, "FIRSTNAME", string.Empty);
                                string LastName = getDBString(reader, "LASTNAME", string.Empty);
                                string Club = getDBString(reader, "CLUBNAME", string.Empty);
                                int? StartNo = null;
                                if (int.TryParse(getDBString(reader, "START_NO", string.Empty), out int tempSN))
                                {
                                    StartNo = tempSN;
                                }
                                string MusicTitle = getDBString(reader, "MUSIC", string.Empty);
                                string discipline = getDBString(reader, "DISCIPLINE", string.Empty);
                                string categoryName = getDBString(reader, "CATEGORY_NAME", string.Empty);
                                string segment = "Free Skating";
                                if (discipline == "Isdans" || discipline == "Soloisdans")
                                {
                                    segment = "Free Dance";
                                }

                                //Find category in competition object compEvent
                                categorySegment category = null;
                                foreach (categorySegment cat in compEvent.categoriesAndSegments)
                                {
                                    if (cat.discipline == discipline &&
                                        cat.category == categoryName &&
                                        cat.segment == segment)
                                    {
                                        category = cat;
                                    }
                                }
                                // If category not found, create a new category
                                if (category == null)
                                {//New category. Create structure
                                    category = new categorySegment
                                    {
                                        discipline = discipline,
                                        category = categoryName,
                                        segment = segment,
                                        participants = new List<participant>()
                                    };
                                    compEvent.categoriesAndSegments.Add(category);
                                }

                                // Locate participant in category via ID
                                participant participant = null;
                                if (ID != null) //Must have ID to search for participant
                                {
                                    foreach (participant par in category.participants)
                                    {
                                        if (par.id == ID)
                                        {
                                            participant = par;
                                        }
                                    }
                                }
                                // If we didn't find participant with ID, try to find if participant already present using First-, Lastname and Club to match from Competition
                                if (participant == null)
                                {
                                    foreach (participant par in category.participants)
                                    {
                                        if (par.firstName == FirstName &&
                                            par.lastName == LastName &&
                                            par.club == Club)
                                        {
                                            participant = par;
                                        }
                                    }
                                }
                                // If person not found, create a new person
                                if (participant == null)
                                {
                                    participant = new participant
                                    {
                                        id = ID,
                                        birthDate = Birthdate,
                                        music = new competitionMusic()
                                    };
                                    category.participants.Add(participant);
                                }
                                // Update
                                participant.startNumber = StartNo;
                                participant.firstName = FirstName;
                                participant.lastName = LastName;
                                participant.club = Club;
                                participant.music.title = MusicTitle;
                            }  //while reader-Participants

                            //Save updates Musicplayer JSON file
                            serializeToFile(compEvent, Properties.Resources.JSON_FILENAME);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading StarFS database\n" + e.Message, Properties.Resources.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Translate segment from ENG to SWE
        private static string translateSegmentToSWE(string segmentENG)
        {
            switch (segmentENG.ToUpper())
            {
                case "FREE SKATING":
                    return "Friåkning";
                case "SHORT PROGRAM":
                    return "Kortprogram";
                case "PATTERN DANCE 1":
                    return "Mönsterdans 1";
                case "PATTERN DANCE 2":
                    return "Mönsterdans 2";
                case "RHYTHM DANCE":
                    return "Rytmdans";
                case "FREE DANCE":
                    return "Fridans";
                default:
                    return segmentENG;
            }
        }

        //Translate segment from SWE to ENG
        private static string translateSegmentToENG(string segmentSWE)
        {
            switch (segmentSWE)
            {
                case "Friåkning":
                    return "Free skating";
                case "Kortprogram":
                    return "Short program";
                case "Mönsterdans 1":
                    return "Pattern Dance 1";
                case "Mönsterdans 2":
                    return "Pattern Dance 2";
                case "Rytmdans":
                    return "Rhythm Dance";
                case "Fridans":
                    return "Free Dance";
                default:
                    return segmentSWE;
            }
        }

        private static Dictionary<string, string> GetDataMap(categorySegment catSeg, participant p)
        {
            string normalize(string s) => new string(s?.Where(c => !Path.GetInvalidFileNameChars().Contains(c) && !char.IsWhiteSpace(c)).ToArray() ?? Array.Empty<char>());
            string normalizeDate(DateTime? dt) => dt.HasValue ? dt.Value.ToString("yyyy-MM-dd") : string.Empty;
            string normalizeDateYYYYMMDD(DateTime? dt) => dt.HasValue ? dt.Value.ToString("yyyyMMdd") : string.Empty;
            string normalizeGuid(Guid? g) => g.HasValue ? g.Value.ToString() : string.Empty;

            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "{Discipline}", normalize(catSeg.discipline) },
                { "{Category}", normalize(catSeg.category) },
                { "{Segment}", normalize(catSeg.segment) },
                { "{SegmentNoSpace}", normalize(catSeg.segment.Replace(" ","")) },
                { "{SegmentSWE}", translateSegmentToSWE(catSeg.segment) },
                { "{FirstName}", normalize(p.firstName.Trim()) },
                { "{FirstNameDash}", normalize(p.firstName?.Trim().Replace(" ","-")) },
                { "{LastName}", normalize(p.lastName?.Trim()) },
                { "{LastNameDash}", normalize(p.lastName?.Trim().Replace(" ","-")) },
                { "{Club}", normalize(p.club?.Trim()) },
                { "{Birthdate}", normalizeDate(p.birthDate) },
                { "{BirthdateYYYYMMDD}", normalizeDateYYYYMMDD(p.birthDate) },
                { "{ID}", normalizeGuid(p.id) }
            };
        }

        private void autoconnectMusic()
        {
            string MissingFiles = string.Empty;

            // Loop all competition participants and try to find music files for them
            foreach (categorySegment catSeg in compEvent.categoriesAndSegments)
            {
                string discipline = catSeg.discipline;
                string category = catSeg.category;
                string segment = catSeg.segment;

                foreach (participant part in catSeg.participants)
                {
                    // Update infotext that we are working...
                    toolStripStatusLabel1.Text = "Connecting music for " + part.firstName + " " + part.lastName;
                    statusStrip1.Update();

                    // Map values for lookup patterns
                    var datamap = GetDataMap(catSeg, part);

                    //Loop trough all lookup patterns
                    foreach (var pattern in settings.musicLookup)
                    {
                        string FileToFind = pattern;
                        foreach (var kvp in datamap)
                        {
                            FileToFind = FileToFind.Replace(kvp.Key, kvp.Value);
                        }

                        //Look for file in music directory
                        FileInfo[] fiArray = null;
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
                                {//Store music file and MD5 in participant, and exit loop
                                    part.music.file = SearchFile;
                                    part.music.md5 = MD5;
                                    break;
                                }
                            }
                        }
                    }
                    //Did we find a file?
                    if (string.IsNullOrEmpty(part.music.file))
                    {
                        MissingFiles = MissingFiles + "No music found for " + part.firstName + " " + part.lastName + "," + part.club + ": " + catSeg.category + "," + catSeg.segment + "\n";
                    }

                }
            }

            //Save updates Musicplayer JSON file
            serializeToFile(compEvent, Properties.Resources.JSON_FILENAME);


            // Inform user of missing files or if all participants are connected
            if (!string.IsNullOrEmpty(MissingFiles))
            {
                _ = MessageBox.Show(MissingFiles, Properties.Resources.CAPTION_MISSING_MUSIC_FILES, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _ = MessageBox.Show(Properties.Resources.ALL_PARTICIPANTS_AUTOCONNECTED, Properties.Resources.CAPTION_AUTOCONNECT, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
                //Check MD5 for alla participants to verify no dublicates
                foreach (categorySegment catSeg in compEvent.categoriesAndSegments)
                {
                    foreach (participant part in catSeg.participants)
                    {
                        if (part.music.md5 == MD5 && part.music.file != fileName)
                        {
                            MD5 = "MD5:" + part.music.file; // Mark as duplicate
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
                audioFileReaderTest?.Dispose();
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
