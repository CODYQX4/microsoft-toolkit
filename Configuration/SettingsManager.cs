using System;
using System.Configuration;
using System.IO;
using System.Xml;
using Common;

namespace Configuration
{
    /// <summary>
    /// Group of Methods for Loading and Saving Application Settings using any Settings File Location
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// Path to Settings File
        /// </summary>
        public static string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Microsoft Toolkit\\";
        /// <summary>
        /// Settings File Name
        /// </summary>
        public static string SettingsFile = "Settings.xml";

        /// <summary>
        /// Load Application Settings from a Settings File
        /// </summary>
        /// <param name="settings">Application Settings Object for the running Application</param>
        public static void Load(ref ApplicationSettingsBase settings)
        {
            // Create new XmlDocument for Settings File
            XmlDocument settingsDoc = new XmlDocument();
            settingsDoc.Load(SettingsPath + SettingsFile);

            // Get a list of all XmlNodes with the Tag Setting
            foreach (XmlNode node in settingsDoc.GetElementsByTagName("setting"))
            {
                // Change Setting
                if (node.Attributes != null)
                {
                    string name = node.Attributes[0].Value;
                    string type = node.Attributes[1].Value;
                    string value = node.FirstChild.InnerText;

                    if (settings[name] != null)
                    {
                        // ReSharper disable AssignNullToNotNullAttribute
                        settings[name] = Convert.ChangeType(value, Type.GetType(type));
                        // ReSharper restore AssignNullToNotNullAttribute
                    }
                }
            }
        }

        /// <summary>
        /// Save Application Settings to a Settings File
        /// </summary>
        /// <param name="settings">Application Settings Object for the running Application</param>
        public static void Save(ApplicationSettingsBase settings)
        {
            // Create Directory
            if (!Directory.Exists(SettingsPath))
            {
                Directory.CreateDirectory(SettingsPath);
            }

            // Delete Old File
            if (File.Exists(SettingsPath + SettingsFile))
            {
                CommonUtilities.FileDelete(SettingsPath + SettingsFile);
            }

            // Open an XMLReader on the XML File
            using (XmlWriter writer = XmlWriter.Create(SettingsPath + SettingsFile))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("configuration");
                writer.WriteStartElement("userSettings");
                writer.WriteStartElement("Settings");
                foreach (SettingsProperty setting in settings.Properties)
                {
                    writer.WriteStartElement("setting");
                    writer.WriteAttributeString("name", setting.Name);
                    writer.WriteAttributeString("serializeAs", setting.PropertyType.FullName);
                    writer.WriteStartElement("value");
                    if (settings[setting.Name] != null)
                    {
                        writer.WriteString(settings[setting.Name].ToString());
                    }
                    else
                    {
                        writer.WriteString(setting.DefaultValue.ToString());
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // Reformat XML
            XmlDocument settingsDoc = new XmlDocument();
            settingsDoc.Load(SettingsPath + SettingsFile);
            settingsDoc.Load(new StringReader(CommonUtilities.FormatXml(settingsDoc.OuterXml)));

            // Delete Old File
            CommonUtilities.FileDelete(SettingsPath + SettingsFile);
           
            // Save Formatted File
            settingsDoc.Save(SettingsPath + SettingsFile);
        }
    }
}
