using System;
using System.Reflection;
using System.Windows.Forms;

namespace AboutBox
{
    partial class aboutBox : Form
    {
        public aboutBox()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            Text = $"About {assemblyTitle}";
            labelProductName.Text = assemblyProduct;
            labelVersion.Text = $"Version {assemblyVersion}";
            labelCopyright.Text = assemblyCopyright;
            labelCompanyName.Text = assemblyCompany;
            textBoxDescription.Text = assemblyDescription + "\n\n" + assemblyTrademark;
        }

        #region Assembly Attribute Accessors

        public static string assemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (string.IsNullOrEmpty(titleAttribute.Title))
                    {
                        return titleAttribute.Title;
                    }
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string assemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string assemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string assemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string assemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string assemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public static string assemblyTrademark
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);
                // If there aren't any Trademark attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;
                // If there is a Trademark attribute, return its value
                return ((AssemblyTrademarkAttribute)attributes[0]).Trademark;
            }
        }
        #endregion
    }
}
