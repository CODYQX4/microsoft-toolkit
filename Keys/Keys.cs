using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using License;
using Microsoft.Win32;
using ProductDetection;

namespace Keys
{
    /// <summary>
    /// Methods for all other Key Classes
    /// </summary>
    public static class KeyBase
    {
        /// <summary>
        /// Regular Expression to match a Full Product Key with Dashes
        /// </summary>
        public static readonly Regex KeyRegex = new Regex("([A-Z1-9]{5})-([A-Z1-9]{5})-([A-Z1-9]{5})-([A-Z1-9]{5})-([A-Z1-9]{5})", RegexOptions.Multiline);

        /// <summary>
        /// Validate that a Product Key in in valid format
        /// </summary>
        /// <param name="key">The key to have its format validated</param>
        /// <returns>True if valid format, False if invalid format</returns>
        public static bool IsValidKey(string key)
        {
            // Determine if entered key is in valid format
            return KeyRegex.Match(key).Success;
        }

        /// <summary>
        /// Regular Expression to match a Full Product Key ID with Dashes
        /// </summary>
        public static readonly Regex PKEYIDRegex = new Regex("^([0-9]{5})-([0-9]{5})-([0-9]{3})-([0-9]{6})-([0-9]{2})-([0-9]{4,5})-([0-9]{4,5}).([0-9]{4})-([0-9]{7})$", RegexOptions.Multiline);

        /// <summary>
        /// Validate that a Product Key ID in in valid format
        /// </summary>
        /// <param name="key">The key to have its format validated</param>
        /// <returns>True if valid format, False if invalid format</returns>
        public static bool IsValidPKEYID(string key)
        {
            // Determine if entered key is in valid format
            return PKEYIDRegex.Match(key).Success;
        }

        /// <summary>
        /// Get a KeyList for a specific Product
        /// </summary>
        /// <param name="productType">Product KeyList to Use</param>
        /// <returns>KeyList containing Product Keys for the desired Product</returns>
        public static KeyList GetKeyListFromSelection(string productType)
        {
            KeyList keyList = new KeyList();

            // Access KeyList based on chosen product
            switch (productType)
            {
                case OfficeVersion.Office2010:
                    keyList = new KeyListOffice2010VL();
                    break;
                case OfficeVersion.Office2013:
                    keyList = new KeyListOffice2013VL();
                    break;
                case OfficeVersion.Office2016:
                    keyList = new KeyListOffice2016VL();
                    break;
                case OfficeVersion.Office2019:
                    keyList = new KeyListOffice2019VL();
                    break;
                case OSVersion.WinVista:
                    keyList = new KeyListWinVistaVL();
                    break;
                case OSVersion.WinServer2008:
                    keyList = new KeyListServer2008VL();
                    break;
                case OSVersion.Win7:
                    keyList = new KeyListWin7VL();
                    break;
                case OSVersion.Win7Embedded:
                    keyList = new KeyListWin7EmbeddedVL();
                    break;
                case OSVersion.WinServer2008R2:
                    keyList = new KeyListServer2008R2VL();
                    break;
                case OSVersion.Win8:
                    keyList = new KeyListWin8VL();
                    break;
                case OSVersion.Win8Embedded:
                    keyList = new KeyListWin8EmbeddedVL();
                    break;
                case OSVersion.WinServer2012:
                    keyList = new KeyListServer2012VL();
                    break;
                case OSVersion.Win81:
                    keyList = new KeyListWin81VL();
                    break;
                case OSVersion.Win81Embedded:
                    keyList = new KeyListWin81EmbeddedVL();
                    break;
                case OSVersion.WinServer2012R2:
                    keyList = new KeyListServer2012R2VL();
                    break;
                case OSVersion.Win10:
                    keyList = new KeyListWin10VL();
                    break;
                case OSVersion.Win10Embedded:
                    keyList = new KeyListWin10EmbeddedVL();
                    break;
                case OSVersion.WinServer2016:
                    keyList = new KeyListServer2016VL();
                    break;
                case OSVersion.WinServer2019:
                    keyList = new KeyListServer2019VL();
                    break;
            }
            return keyList;
        }

		/// <summary>
        /// Generate Merged KeyList of all applicable KeysList Keys for a product
        /// </summary>
        /// <param name="productType">Microsoft Office or Windows Edition</param>
        /// <returns>KeyList containing Product Keys for the desired Product</returns>
        public static KeyList GetApplicableKeysList(string productType)
        {
            // Global List of Keys
            KeyList keys = new KeyList();

            // Temporarily store KeyLists in List of KeyLists
            List<KeyList> applicableKeyLists = new List<KeyList>();

            // Choose KeyLists based on Microsoft Product Edition
            switch (productType)
            {
                case OfficeVersion.Office2010:
                {
                    applicableKeyLists.Add(new KeyListOffice2010VL());
                    break;
                }
                case OfficeVersion.Office2013:
                {
                    applicableKeyLists.Add(new KeyListOffice2013VL());
                    break;
                }
                case OfficeVersion.Office2016:
                {
                    applicableKeyLists.Add(new KeyListOffice2016VL());
                    break;
                }
                case OfficeVersion.Office2019:
                    {
                        applicableKeyLists.Add(new KeyListOffice2019VL());
                        break;
                    }
                case OSVersion.WinVista:
                {
                    applicableKeyLists.Add(new KeyListWinVistaVL());
                    break;
                }
                case OSVersion.WinServer2008:
                {
                    applicableKeyLists.Add(new KeyListServer2008VL());
                    break;
                }
                case OSVersion.Win7:
                case OSVersion.Win7Embedded:
                {
                    applicableKeyLists.Add(new KeyListWin7VL());
                    applicableKeyLists.Add(new KeyListWin7EmbeddedVL());
                    break;
                }
                case OSVersion.WinServer2008R2:
                {
                    applicableKeyLists.Add(new KeyListServer2008R2VL());
                    break;
                }
                case OSVersion.Win8:
                case OSVersion.Win8Embedded:
                {
                    applicableKeyLists.Add(new KeyListWin8VL());
                    applicableKeyLists.Add(new KeyListWin8EmbeddedVL());
                    break;
                }
                case OSVersion.WinServer2012:
                {
                    applicableKeyLists.Add(new KeyListServer2012VL());
                    break;
                }
                case OSVersion.Win81:
                case OSVersion.Win81Embedded:
                {
                    applicableKeyLists.Add(new KeyListWin81VL());
                    applicableKeyLists.Add(new KeyListWin81EmbeddedVL());
                    break;
                }
                case OSVersion.WinServer2012R2:
                {
                    applicableKeyLists.Add(new KeyListServer2012R2VL());
                    break;
                }
                case OSVersion.Win10:
                case OSVersion.Win10Embedded:
                {
                    applicableKeyLists.Add(new KeyListWin10VL());
                    applicableKeyLists.Add(new KeyListWin10EmbeddedVL());
                    break;
                }
                case OSVersion.WinServer2016:
                {
                    applicableKeyLists.Add(new KeyListServer2016VL());
                    break;
                }
                case OSVersion.WinServer2019:
                    {
                        applicableKeyLists.Add(new KeyListServer2019VL());
                        break;
                    }
                default:
                    throw new ApplicationException("No KeyLists Matching this Product!");
            }

            // Merge KeyLists into one KeyList
            foreach (KeyList applicableKeyList in applicableKeyLists)
            {
                foreach (KeyData keyData in applicableKeyList)
                {
                    keys.AddProductKey(keyData.Product, keyData.ProductInternal, keyData.Key, keyData.SKUID);
                }
            }
            return keys;
        }
    }

    /// <summary>
    /// Methods for installing and removing Product Keys
    /// </summary>
    public static class KeyInstaller
    {
        /// <summary>
        /// Call InstallKey() with the needed WMI provider for Office
        /// </summary>
        /// <param name="key">Product Key to Install</param>
        public static void InstallKeyOffice(string key)
        {
            // Call Windows if Microsoft Office is using Windows Licensing Services
            if (OfficeVersion.IsOfficeSPP())
            {
                InstallKeyWindows(key);
                return;
            }
            InstallKey(key, "OfficeSoftwareProtectionService.Version=" + "'" + OfficeVersion.GetOSPPSVCVersion() + "'");
        }

        /// <summary>
        /// Call InstallKey() with the needed WMI provider for Windows
        /// </summary>
        /// <param name="key">Product Key to Install</param>
        public static void InstallKeyWindows(string key)
        {
            InstallKey(key, "SoftwareLicensingService.Version=" + "'" + OSVersion.GetSPPSVCVersion() + "'");
        }

        /// <summary>
        /// Install a Product Key
        /// </summary>
        /// <param name="key">Product Key to Install</param>
        /// <param name="wmiInfo">WMI Info to call the Software Licensing Service to install a Product Key</param>
        private static void InstallKey(string key, string wmiInfo)
        {
            if (key == "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX")
            {
                throw new ApplicationException("<There is no provided key for this version. you will have to enter one yourself.>");
            }
            if (KeyBase.IsValidKey(key) == false)
            {
                throw new ApplicationException("<Invalid Product Key.>");
            }
            try
            {
                using (ManagementObject classInstance = new ManagementObject(@"root\CIMV2", wmiInfo, null))
                {
                    using (ManagementBaseObject inParams = classInstance.GetMethodParameters("InstallProductKey"))
                    {
                        inParams["ProductKey"] = key;

                        // Execute the method and obtain the return values.
                        classInstance.InvokeMethod("InstallProductKey", inParams, null);
                    }
                }
            }
            catch (COMException ex)
            {
                string errorCode = "0x" + ex.ErrorCode.ToString("X8");
                string errorDescription = LicenseErrorCode.GetErrorDescription(errorCode);
                throw new Exception("<Product key installation failed>" + Environment.NewLine + errorDescription);
            }
        }

        /// <summary>
        /// Call UnInstallKeyByKey() with the needed WMI provider for Office
        /// </summary>
        /// <param name="key">Product Key to Uninstall</param>
        public static void UnInstallKeyByKeyOffice(string key)
        {
            // Call Windows if Microsoft Office is using Windows Licensing Services
            if (OfficeVersion.IsOfficeSPP())
            {
                UnInstallKeyByKeyWindows(key);
                return;
            }
            UnInstallKeyByKey(key, "SELECT ID, PartialProductKey FROM OfficeSoftwareProtectionProduct", "OfficeSoftwareProtectionProduct.ID='SKUID'");
        }

        /// <summary>
        /// Call UnInstallKeyByKey() with the needed WMI provider for Windows
        /// </summary>
        /// <param name="key">Product Key to Uninstall</param>
        public static void UnInstallKeyByKeyWindows(string key)
        {
            UnInstallKeyByKey(key, "SELECT ID, PartialProductKey FROM SoftwareLicensingProduct", "SoftwareLicensingProduct.ID='SKUID'");
        }

        /// <summary>
        /// Uninstall a Product Key
        /// </summary>
        /// <param name="key">Product Key to Uninstall</param>
        /// <param name="wmiInfo1">WMI Info to call the Software Licensing Service to find the SKUID for this Product Key</param>
        /// /// <param name="wmiInfo2">WMI Info to call the Software Licensing Service to uninstall this Product Key</param>
        private static void UnInstallKeyByKey(string key, string wmiInfo1, string wmiInfo2)
        {
            if (key == "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX")
            {
                throw new ApplicationException("<There is no provided key for this version. you will have to enter one yourself.>");
            }
            if (KeyBase.IsValidKey(key) == false && key.Length != 5)
            {
                throw new ApplicationException("<Invalid Product Key.>");
            }

            // If the key is more than 5 characters long it removes the characters at the begining.
            string lastFiveChars;
            if (key.Length == 5)
            {
                lastFiveChars = key;
            }
            else
            {
                lastFiveChars = key.Substring(key.Length - 5, 5);
            }

            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", wmiInfo1))
                {
                    foreach (ManagementBaseObject queryObj in searcher.Get())
                    {
                        string partialProductKey = Convert.ToString(queryObj["PartialProductKey"]);
                        if (partialProductKey != lastFiveChars)
                        {
                            continue;
                        }

                        using (ManagementObject classInstance = new ManagementObject(@"root\CIMV2", wmiInfo2.Replace("SKUID", queryObj["ID"].ToString()), null))
                        {
                            classInstance.InvokeMethod("UninstallProductKey", null, null);
                        }

                        return;
                    }
                    throw new ApplicationException("<Product key not found>");
                }
            }
            catch (COMException ex)
            {
                // Try to lookup Error
                string errorCode = "0x" + ex.ErrorCode.ToString("X8");
                string errorDescription = LicenseErrorCode.GetErrorDescription(errorCode);
                throw new Exception("<Product key uninstall failed>" + Environment.NewLine + errorDescription);
            }
        }
    }

    /// <summary>
    /// Struct to Hold Identifiable Info to Obtain a Product Key Via Product Name or SKUID
    /// </summary>
    public struct KeyData
    {
        public string Product;
        public string ProductInternal;
        public string Key;
        public string SKUID;

        public KeyData(string product, string productInternal, string key, string skuid)
        {
            Product = product;
            ProductInternal = productInternal;
            Key = key;
            SKUID = skuid;
        }
    }

    /// <summary>
    /// Base KeyList class and inherited classes for storing Keys in a list and retrieving them by product name
    /// </summary>
    public class KeyList : IEnumerable
    {
        /// <summary>
        /// List of KeyData Objects
        /// </summary>
        internal List<KeyData> KeyTable = new List<KeyData>();

        /// <summary>
        /// Get a Product Key by Product Name or SKUID
        /// </summary>
        /// <param name="identifier">Product Name or SKUID of the Key</param>
        /// <returns>The Product Key in the desired index, or NULL if not found.</returns>
        public string GetProductKey(string identifier)
        {
            foreach (KeyData keyData in KeyTable)
            {
                if (keyData.Product == identifier || keyData.ProductInternal == identifier || keyData.SKUID == identifier)
                {
                    return keyData.Key;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get an Internal Product Name by Product Key or SKUID
        /// </summary>
        /// <param name="identifier">Product Key or SKUID of the Internal Product Name</param>
        /// <returns>The Internal Product Name in the desired index, or NULL if not found.</returns>
        public string GetInternalProductName(string identifier)
        {
            foreach (KeyData keyData in KeyTable)
            {
                if (keyData.Key == identifier || keyData.SKUID == identifier || keyData.Product == identifier)
                {
                    return keyData.ProductInternal;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get a Product Name by Product Key or SKUID
        /// </summary>
        /// <param name="identifier">Product Key or SKUID of the Product Name</param>
        /// <returns>The Product Name in the desired index, or NULL if not found.</returns>
        public string GetProductName(string identifier)
        {
            foreach (KeyData keyData in KeyTable)
            {
                if (keyData.Key == identifier || keyData.SKUID == identifier || keyData.ProductInternal == identifier)
                {
                    return keyData.Product;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get a SKUID by Product Key or Product Name
        /// </summary>
        /// <param name="identifier">Product Key or Product Name</param>
        /// <returns>The SKUID in the desired index, or NULL if not found.</returns>
        public string GetSKUID(string identifier)
        {
            foreach (KeyData keyData in KeyTable)
            {
                if (keyData.Key == identifier || keyData.Product == identifier || keyData.ProductInternal == identifier)
                {
                    return keyData.SKUID;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Add or Replace a Product Key in the Hashtable
        /// </summary>
        /// <param name="product">Product Name to create or replace</param>
        /// <param name="productInternal">Internal Product Name to create or replace</param>
        /// <param name="key">Key to Add or Replace</param>
        /// <param name="skuid">SKUID of the Product Key</param>
        public void AddProductKey(string product, string productInternal, string key, string skuid)
        {
            if (KeyBase.IsValidKey(key) == false)
            {
                throw new ApplicationException("<Invalid Product Key.>");
            }
            KeyTable.Add(new KeyData(product, productInternal, key, skuid));
        }

        /// <summary>
        /// Get a List of all Internal Products in the KeyList
        /// </summary>
        /// <param name="filter">Match KeyList Properties against Filter String</param>
        /// <returns>String List of all Products in the KeyList</returns>
        public List<string> GetInternalProducts(string filter = "")
        {
            List<string> internalProducts = new List<string>();
            foreach (KeyData keyData in KeyTable)
            {
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    if (keyData.Key == filter || keyData.Product == filter || keyData.SKUID == filter)
                    {
                        internalProducts.Add(keyData.ProductInternal);
                    }
                }
                else
                {
                    internalProducts.Add(keyData.ProductInternal);
                }
            }
            return internalProducts;
        }

        /// <summary>
        /// Get a List of all Products in the KeyList
        /// </summary>
        /// <param name="filter">Match KeyList Properties against Filter String</param>
        /// <returns>String List of all Products in the KeyList</returns>
        public List<string> GetProducts(string filter = "")
        {
            List<string> products = new List<string>();
            foreach (KeyData keyData in KeyTable)
            {
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    if (keyData.Key == filter || keyData.ProductInternal == filter || keyData.SKUID == filter)
                    {
                        products.Add(keyData.Product);
                    }
                }
                else
                {
                    products.Add(keyData.Product);
                }
            }
            return products;
        }

        /// <summary>
        /// Get a List of all Keys in the KeyList
        /// </summary>
        /// <param name="filter">Match KeyList Properties against Filter String</param>
        /// <returns>String List of all Keys in the KeyList</returns>
        public List<string> GetKeys(string filter = "")
        {
            List<string> keys = new List<string>();
            foreach (KeyData keyData in KeyTable)
            {
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    if (keyData.Product == filter || keyData.ProductInternal == filter || keyData.SKUID == filter)
                    {
                        keys.Add(keyData.Key);
                    }
                }
                else
                {
                    keys.Add(keyData.Key);
                }
            }
            return keys;
        }

        /// <summary>
        /// Get a List of all SKUIDs in the KeyList
        /// </summary>
        /// <param name="filter">Match KeyList Properties against Filter String</param>
        /// <returns>String List of all SKUIDs in the KeyList</returns>
        public List<string> GetSKUIDs(string filter = "")
        {
            List<string> skuids = new List<string>();
            foreach (KeyData keyData in KeyTable)
            {
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    if (keyData.Key == filter || keyData.Product == filter || keyData.ProductInternal == filter)
                    {
                        skuids.Add(keyData.SKUID);
                    }
                }
                else
                {
                    skuids.Add(keyData.SKUID);
                }
            }
            return skuids;
        }

        /// <summary>
        /// Iterate through the Key Data Table
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return KeyTable.GetEnumerator();
        }
    }

    /// <summary>
    /// Create a KeyList for Microsoft Office 2010 Keys that are Volume License
    /// </summary>
    public class KeyListOffice2010VL : KeyList
    {
        public KeyListOffice2010VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Access", "AccessVL", "V7Y44-9T38C-R2VJK-666HK-T7DDX", "8ce7e872-188c-4b98-9d90-f8f90b7aad02");
            AddProductKey("Excel", "ExcelVL", "H62QG-HXVKF-PP4HP-66KMR-CW9BM", "cee5d470-6e3b-4fcc-8c2b-d17428568a9f");
            AddProductKey("InfoPath", "InfoPathVL", "K96W8-67RPQ-62T9Y-J8FQJ-BT37T", "ca6b6639-4ad6-40ae-a575-14dee07f6430");
            AddProductKey("Mondo 1", "MondoVL", "YBJTT-JG6MD-V9Q7P-DBKXJ-38W9R", "09ed9640-f020-400a-acd8-d7d867dfd9c2");
            AddProductKey("Mondo 2", "MondoVL", "7TC2V-WXF6P-TD7RT-BQRXR-B8K32", "ef3d4e49-a53d-4d81-a2b1-2ca6c2556b2c");
            AddProductKey("OneNote", "OneNoteVL", "Q4Y4M-RHWJM-PY37F-MTKWH-D3XHX", "ab586f5c-5256-4632-962f-fefd8b49e6f4");
            AddProductKey("Outlook", "OutlookVL", "7YDC2-CWM8M-RRTJC-8MDVC-X3DWQ", "ecb7c192-73ab-4ded-acf4-2399b095d0cc");
            AddProductKey("PowerPoint", "PowerPointVL", "RC8FX-88JRY-3PF7C-X8P67-P4VTT", "45593b1d-dfb1-4e91-bbfb-2d5d0ce2227a");
            AddProductKey("Professional Plus", "ProPlusVL", "VYBBJ-TRJPB-QFQRF-QFT4D-H3GVB", "6f327760-8c5c-417c-9b61-836a98287e0c");
            AddProductKey("Project Professional", "ProjectProVL", "YGX6F-PGV49-PGW3J-9BTGG-VHKC6", "df133ff7-bf14-4f95-afe3-7b48e7e331ef");
            AddProductKey("Project Standard", "ProjectStdVL", "4HP3K-88W3F-W2K3D-6677X-F9PGB", "5dc7bf61-5ec9-4996-9ccb-df806a2d0efe");
            AddProductKey("Publisher", "PublisherVL", "BFK7F-9MYHM-V68C7-DRQ66-83YTP", "b50c4f75-599b-43e8-8dcd-1081a7967241");
            AddProductKey("SharePoint Designer", "SPDR", "H48K6-FB4Y6-P83GH-9J7XG-HDKKX", "b78df69e-0966-40b1-ae85-30a5134dedd0"); // Not KMS, Bypass Activation
            AddProductKey("SharePoint Workspace", "GrooveVL", "QYYW6-QP4CB-MBV6G-HYMCJ-4T3J4", "8947d0b8-c33b-43e1-8c56-9b674c052832");
            AddProductKey("Small Business Basics", "SmallBusBasicsVL", "D6QFG-VBYP2-XQHM7-J97RH-VVRCK", "ea509e87-07a1-4a45-9edc-eba5a39f36af");
            AddProductKey("Standard", "StandardVL", "V7QKV-4XVVR-XYV4D-F7DFM-8R6BM", "9da2a678-fb6b-4e67-ab84-60dd6a9c819a");
            AddProductKey("Starter", "StarterR", "VXHHB-W7HBD-7M342-RJ7P8-CHBD6", "2745e581-565a-4670-ae90-6bf7c57ffe43"); // Not KMS, Bypass Activation
            AddProductKey("Visio Premium", "VisioSIVL", "D9DWC-HPYVV-JGF4P-BTWQB-WX8BJ", "92236105-bb67-494f-94c7-7f7a607929bd");
            AddProductKey("Visio Professional", "VisioSIVL", "7MCW8-VRQVK-G677T-PDJCM-Q8TCP", "e558389c-83c3-4b29-adfe-5e4d7f46c358");
            AddProductKey("Visio Standard", "VisioSIVL", "767HD-QGMWX-8QTDB-9G3R2-KHFGJ", "9ed833ff-4f92-4f36-b370-8683a4f13275");
            AddProductKey("Word", "WordVL", "HVHB3-C6FV7-KQX9W-YQG79-CRY7T", "2d0882e7-a4e7-423b-8ccc-70d91e0158b1");            
        }
    }

    /// <summary>
    /// Create a KeyList for Microsoft Office 2013 Keys that are Volume License
    /// </summary>
    public class KeyListOffice2013VL : KeyList
    {
        public KeyListOffice2013VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Access", "AccessVolume", "NG2JY-H4JBT-HQXYP-78QH9-4JM2D", "6ee7622c-18d8-4005-9fb7-92db644a279b");
            AddProductKey("Excel", "ExcelVolume", "VGPNG-Y7HQW-9RHP7-TKPV3-BG7GB", "f7461d52-7c2b-43b2-8744-ea958e0bd09a");
            AddProductKey("InfoPath", "InfoPathVolume", "DKT8B-N7VXH-D963P-Q4PHY-F8894", "a30b8040-d68a-423f-b0b5-9ce292ea5a8f");
            AddProductKey("Lync", "LyncVolume", "2MG3G-3BNTT-3MFW9-KDQW3-TCK7R", "1b9f11e3-c85c-4e1b-bb29-879ad2c909e3");
            AddProductKey("Mondo", "MondoVolume", "42QTK-RN8M7-J3C4G-BBGYM-88CYV", "dc981c6b-fc8e-420f-aa43-f8f33e5c0923");
            AddProductKey("OneDrive for Business", "GrooveVL", "H7R7V-WPNXQ-WCYYC-76BGV-VT7GH", "fb4875ec-0c6b-450f-b82b-ab57d8d1677f");
            AddProductKey("OneNote", "OneNoteVolume", "TGN6P-8MMBC-37P2F-XHXXK-P34VW", "efe1f3e6-aea2-4144-a208-32aa872b6545");
            AddProductKey("Outlook", "OutlookVolume", "QPN8Q-BJBTJ-334K3-93TGY-2PMBT", "771c3afa-50c5-443f-b151-ff2546d863a0");
            AddProductKey("PowerPoint", "PowerPointVolume", "4NT99-8RJFH-Q2VDH-KYG2C-4RD4F", "8c762649-97d1-4953-ad27-b7e2c25b972e");
            AddProductKey("Professional Plus", "ProPlusVolume", "YC7DK-G2NP3-2QQC3-J6H88-GVGXT", "b322da9c-a2e2-4058-9e4e-f59a6970bd69");
            AddProductKey("Project Professional", "ProjectProVolume", "FN8TT-7WMH6-2D4X9-M337T-2342K", "4a5d124a-e620-44ba-b6ff-658961b33b9a");
            AddProductKey("Project Standard", "ProjectStdVolume", "6NTH3-CW976-3G3Y2-JK3TX-8QHTT", "427a28d1-d17c-4abf-b717-32c780ba6f07");
            AddProductKey("Publisher", "PublisherVolume", "PN2WF-29XG2-T9HJ7-JQPJR-FCXK4", "00c79ff1-6850-443d-bf61-71cde0de305f");
            AddProductKey("SharePoint Designer", "SPDRetail", "GYJRG-NMYMF-VGBM4-T3QD4-842DW", "ba3e3833-6a7e-445a-89d0-7802a9a68588"); // Not KMS, Bypass Activation
            AddProductKey("Standard", "StandardVolume", "KBKQT-2NMXY-JJWGP-M62JB-92CD4", "b13afb38-cd79-4ae5-9f7f-eed058d750ca");
            AddProductKey("Visio Professional", "VisioProVolume", "C2FG9-N6J68-H8BTJ-BW3QX-RM3B3", "e13ac10e-75d0-4aff-a0cd-764982cf541c");
            AddProductKey("Visio Standard", "VisioStdVolume", "J484Y-4NKBF-W2HMG-DBMJC-PGWR7", "ac4efaf0-f81f-4f61-bdf7-ea32b02ab117");
            AddProductKey("Word", "WordVolume", "6Q7VD-NX8JD-WJ2VH-88V73-4GBJ7", "d9f5b1c6-5386-495a-88f9-9ad6b41ac9b3");
        }
    }

    /// <summary>
    /// Create a KeyList for Microsoft Office 2016 Keys that are Volume License
    /// </summary>
    public class KeyListOffice2016VL : KeyList
    {
        public KeyListOffice2016VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Access", "AccessVolume", "GNH9Y-D2J4T-FJHGG-QRVH7-QPFDW", "67c0fc0c-deba-401b-bf8b-9c8ad8395804");
            AddProductKey("Excel", "ExcelVolume", "9C2PK-NWTVB-JMPW8-BFT28-7FTBF", "c3e65d36-141f-4d2f-a303-a842ee756a29");
            AddProductKey("Mondo", "MondoVolume", "HFTND-W9MK4-8B7MJ-B6C4G-XQBR2", "9caabccb-61b1-4b4b-8bec-d10a3c3ac2ce");
            AddProductKey("Mondo (Retail)", "MondoRetail", "DMTCJ-KNRKX-26982-JYCKT-P7KB6", "e914ea6e-a5fa-4439-a394-a9bb3293ca09");
            AddProductKey("OneNote", "OneNoteVolume", "DR92N-9HTF2-97XKM-XW2WJ-XW3J6", "d8cace59-33d2-4ac7-9b1b-9b72339c51c8");
            AddProductKey("Outlook", "OutlookVolume", "R69KK-NTPKF-7M3Q4-QYBHW-6MT9B", "ec9d9265-9d1e-4ed0-838a-cdc20f2551a1");
            AddProductKey("PowerPoint", "PowerPointVolume", "J7MQP-HNJ4Y-WJ7YM-PFYGF-BY6C6", "d70b1bba-b893-4544-96e2-b7a318091c33");
            AddProductKey("Professional Plus", "ProPlusVolume", "XQNVK-8JYDB-WJ9W3-YJ8YR-WFG99", "d450596f-894d-49e0-966a-fd39ed4c4c64");
            AddProductKey("Project Professional", "ProjectProVolume", "YG9NW-3K39V-2T3HJ-93F3Q-G83KT", "4f414197-0fc2-4c01-b68a-86cbb9ac254c");
            AddProductKey("Project Professional C2R-P", "ProjectProXVolume", "WGT24-HCNMF-FQ7XH-6M8K7-DRTW9", "829b8110-0e6f-4349-bca4-42803577788d");
            AddProductKey("Project Standard", "ProjectStdVolume", "GNFHQ-F6YQM-KQDGJ-327XX-KQBVC", "da7ddabc-3fbe-4447-9e01-6ab7440b4cd4");
            AddProductKey("Project Standard C2R-P", "ProjectStdXVolume", "D8NRQ-JTYM3-7J2DX-646CT-6836M", "cbbaca45-556a-4416-ad03-bda598eaa7c8");
            AddProductKey("Publisher", "PublisherVolume", "F47MM-N3XJP-TQXJ9-BP99D-8K837", "041a06cb-c5b8-4772-809f-416d03d16654");
            AddProductKey("Skype for Business", "SkypeforBusinessVolume", "869NQ-FJ69K-466HW-QYCP2-DDBV6", "83e04ee1-fa8d-436d-8994-d31a862cab77");
            AddProductKey("Standard", "StandardVolume", "JNRGM-WHDWX-FJJG3-K47QV-DRTFM", "dedfa23d-6ed1-45a6-85dc-63cae0546de6");
            AddProductKey("Visio Professional", "VisioProVolume", "PD3PC-RHNGV-FXJ29-8JK7D-RJRJK", "6bf301c1-b94a-43e9-ba31-d494598c47fb");
            AddProductKey("Visio Professional C2R-P", "VisioProXVolume", "69WXN-MBYV6-22PQG-3WGHK-RM6XC", "b234abe3-0857-4f9c-b05a-4dc314f85557");
            AddProductKey("Visio Standard", "VisioStdVolume", "7WHWN-4T7MP-G96JF-G33KR-W8GF4", "aa2a7821-1827-4c2c-8f1d-4513a34dda97");
            AddProductKey("Visio Standard C2R-P", "VisioStdXVolume", "NY48V-PPYYH-3F4PX-XJRKJ-W4423", "361fe620-64f4-41b5-ba77-84f8e079b1f7");
            AddProductKey("Word", "WordVolume", "WXY84-JN2Q9-RBCCQ-3Q3J3-3PFJ6", "bb11badf-d8aa-470e-9311-20eaf80fe5cc");
        }
    }

    /// <summary>
    /// Create a KeyList for Microsoft Office 2019 Keys that are Volume License
    /// </summary>
    public class KeyListOffice2019VL : KeyList
    {
        public KeyListOffice2019VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Access", "Access2019Volume", "9N9PT-27V4Y-VJ2PD-YXFMF-YTFQT", "9e9bceeb-e736-4f26-88de-763f87dcc485");
            AddProductKey("Excel", "Excel2019Volume", "TMJWT-YYNMB-3BKTF-644FC-RVXBD", "237854e9-79fc-4497-a0c1-a70969691c6b");
            AddProductKey("Outlook", "Outlook2019Volume", "7HD7K-N4PVK-BHBCQ-YWQRW-XW4VK", "c8f8a301-19f5-4132-96ce-2de9d4adbd33");
            AddProductKey("PowerPoint", "PowerPoint2019Volume", "RRNCX-C64HY-W2MM7-MCH9G-TJHMQ", "3131fd61-5e4f-4308-8d6d-62be1987c92c");
            AddProductKey("Professional Plus", "ProPlus2019Volume", "NMMKJ-6RK4F-KMJVX-8D9MJ-6MWKP", "85dd8b5f-eaa4-4af3-a628-cce9e77c9a03");
            AddProductKey("Project Professional", "ProjectPro2019Volume", "B4NPR-3FKK7-T2MBV-FRQ4W-PKD2B", "2ca2bf3f-949e-446a-82c7-e25a15ec78c4");
            AddProductKey("Project Standard", "ProjectStd2019Volume", "C4F7P-NCP8C-6CQPT-MQHV9-JXD2M", "1777f0e3-7392-4198-97ea-8ae4de6f6381");
            AddProductKey("Publisher", "Publisher2019Volume", "G2KWX-3NW6P-PY93R-JXK2T-C9Y9V", "9d3e4cca-e172-46f1-a2f4-1d2107051444");
            AddProductKey("Skype for Business", "SkypeforBusiness2019Volume", "NCJ33-JHBBY-HTK98-MYCV8-HMKHJ", "734c6c6e-b0ba-4298-a891-671772b2bd1b");
            AddProductKey("Standard", "Standard2019Volume", "6NWWJ-YQWMR-QKGCB-6TMB3-9D9HK", "6912a74b-a5fb-401a-bfdb-2e3ab46f4b02");
            AddProductKey("Visio Professional", "VisioPro2019Volume", "9BGNQ-K37YR-RQHF2-38RQ3-7VCBB", "5b5cf08f-b81a-431d-b080-3450d8620565");
            AddProductKey("Visio Standard", "VisioStd2019Volume", "7TQNQ-K3YQQ-3PFH7-CCPPM-X4VQ2", "e06d7df3-aad0-419d-8dfb-0ac37e2bdf39");
            AddProductKey("Word", "Word2019Volume", "PBX3G-NWMT6-Q7XBW-PYJGG-WXD33", "059834fe-a8ea-4bff-b67b-4d006b5447d3");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Vista Keys that are Volume License
    /// </summary>
    public class KeyListWinVistaVL : KeyList
    {
        public KeyListWinVistaVL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Business", "Business", "YFKBB-PQJJV-G996G-VWGXY-2V3X8", "4f3d1606-3fea-4c01-be3c-8d671c401e3b");
            AddProductKey("Business N", "BusinessN", "HMBQG-8H2RH-C77VX-27R82-VMQBT", "2c682dc2-8b68-4f63-a165-ae291d4cf138");
            AddProductKey("Enterprise", "Enterprise", "VKK3X-68KWM-X2YGT-QR4M6-4BWMV", "cfd8ff08-c0d7-452b-9f60-ef5c70c32094");
            AddProductKey("Enterprise N", "EnterpriseN", "VTC42-BM838-43QHV-84HX6-XJXKV", "d4f54950-26f2-4fb4-ba21-ffab16afcade");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Server 2008 Keys that are Volume License
    /// </summary>
    public class KeyListServer2008VL : KeyList
    {
        public KeyListServer2008VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Compute Cluster (HPC)", "ServerComputeCluster", "RCTX3-KWVHP-BR6TB-RB6DM-6X7HP", "7afb1156-2c1d-40fc-b260-aab7442b62fe");
            AddProductKey("Datacenter", "ServerDatacenter", "7M67G-PC374-GR742-YH8V4-TCBY3", "68b6e220-cf09-466b-92d3-45cd964b9509");
            AddProductKey("Datacenter (No Hyper-V)", "ServerDatacenterV", "22XQ2-VRXRG-P8D42-K34TD-G3QQC", "fd09ef77-5647-4eff-809c-af2b64659a45");
            AddProductKey("Enterprise", "ServerEnterprise", "YQGMW-MPWTJ-34KDK-48M3W-X4Q6V", "c1af4d90-d1bc-44ca-85d4-003ba33db3b9");
            AddProductKey("Enterprise (No Hyper-V)", "ServerEnterpriseV", "39BXF-X8Q23-P2WWT-38T2F-G3FPG", "8198490a-add0-47b2-b3ba-316b12d647b4");
            AddProductKey("Itanium", "ServerEnterpriseIA64", "4DWFP-JF3DJ-B7DTH-78FJB-PDRHK", "01ef176b-3e0d-422a-b4f8-4ea880035e8f");
            AddProductKey("Standard", "ServerStandard", "TM24T-X9RMF-VWXK6-X8JC9-BFGM2", "ad2542d4-9154-4c6d-8a44-30f11ee96989");
            AddProductKey("Standard (No Hyper-V)", "ServerStandardV", "W7VD6-7JFBR-RX26B-YKQ3Y-6FFFJ", "2401e3d0-c50a-4b58-87b2-7e794b7d2607");
            AddProductKey("Web", "ServerWeb", "WYR28-R7TFJ-3X2YQ-YCY4H-M249D", "ddfa9f7c-f09e-40b9-8c1a-be877a9a7f4b");            
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 7 Keys that are Volume License
    /// </summary>
    public class KeyListWin7VL : KeyList
    {
        public KeyListWin7VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Enterprise", "Enterprise", "33PXH-7Y6KF-2VJC9-XBBR8-HVTHH", "ae2ee509-1b34-41c0-acb7-6d4650168915");
            AddProductKey("Enterprise E", "EnterpriseE", "C29WB-22CC8-VJ326-GHFJW-H9DH4", "46bbed08-9c7b-48fc-a614-95250573f4ea");
            AddProductKey("Enterprise N", "EnterpriseN", "YDRBP-3D83W-TY26F-D46B2-XCKRJ", "1cb6d605-11b3-4e14-bb30-da91c8e3983a");
            AddProductKey("Professional", "Professional", "FJ82H-XT6CR-J8D7P-XQJJ2-GPDD4", "b92e9980-b9d5-4821-9c94-140f632f6312");
            AddProductKey("Professional E", "ProfessionalE", "W82YF-2Q76Y-63HXB-FGJG9-GF7QX", "5a041529-fef8-4d07-b06f-b59b573b32d2");
            AddProductKey("Professional N", "ProfessionalN", "MRPKT-YTG23-K7D7T-X2JMM-QY7MG", "54a09a0d-d57b-4c10-8b69-a842d6590ad5");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 7 Embedded Keys that are Volume License
    /// </summary>
    public class KeyListWin7EmbeddedVL : KeyList
    {
        public KeyListWin7EmbeddedVL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Embedded POSReady", "Embedded", "YBYF6-BHCR3-JPKRB-CDW7B-F9BK4", "db537896-376f-48ae-a492-53d0547773d0");
            AddProductKey("Embedded Standard", "Embedded", "XGY72-BRBBT-FF8MH-2GG8H-W7KCW", "e1a8296a-db37-44d1-8cce-7bc961d59c54"); // Not KMS, Volume:BA
            AddProductKey("Thin PC", "Embedded", "73KQT-CD9G6-K7TQG-66MRP-CQ22C", "aa6dd3aa-c2b4-40e2-a544-a6bbb3f5c395");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Server 2008 R2 Keys that are Volume License
    /// </summary>
    public class KeyListServer2008R2VL : KeyList
    {
        public KeyListServer2008R2VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Compute Cluster (HPC)", "ServerHPC", "TT8MH-CG224-D3D7Q-498W2-9QCTX", "cda18cf3-c196-46ad-b289-60c072869994");
            AddProductKey("Datacenter", "ServerDatacenter", "74YFP-3QFB3-KQT8W-PMXWJ-7M648", "7482e61b-c589-4b7f-8ecc-46d455ac3b87");
            AddProductKey("Enterprise", "ServerEnterprise", "489J6-VHDMP-X63PK-3K798-CPX3Y", "620e2b3d-09e7-42fd-802a-17a13652fe7a");
            AddProductKey("Itanium", "ServerEnterpriseIA64", "GT63C-RJFQ3-4GMB6-BRFB9-CB83V", "8a26851c-1c7e-48d3-a687-fbca9b9ac16b");
            AddProductKey("MultiPoint", "ServerEmbeddedSolution", "736RG-XDKJK-V34PF-BHK87-J6X3K", "f772515c-0e87-48d5-a676-e6962c3e1195");
            AddProductKey("Standard", "ServerStandard", "YC6KT-GKW9T-YTKYR-T4X34-R7VHC", "68531fb9-5511-4989-97be-d11a0f55633f");
            AddProductKey("Web", "ServerWeb", "6TPJF-RBVHG-WBW2R-86QPH-6RTM4", "a78b8bd9-8017-4df5-b86a-09f756affa7c");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 8 Keys that are Volume License
    /// </summary>
    public class KeyListWin8VL : KeyList
    {
        public KeyListWin8VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Core", "Core", "BN3D2-R7TKB-3YPBD-8DRP2-27GG4", "c04ed6bf-55c8-4b47-9f8e-5a1f31ceee60");
            AddProductKey("Core N", "CoreN", "8N2M2-HWPGY-7PGT9-HGDD8-GVGGY", "197390a0-65f6-4a95-bdc4-55d58a3b0253");
            AddProductKey("Core ARM", "CoreARM", "DXHJF-N9KQX-MFPVR-GHGQK-Y7RKV", "af35d7b7-5035-4b63-8972-f0b747b9f4dc");
            AddProductKey("Core Country Specific", "CoreCountrySpecific", "4K36P-JN4VD-GDC6V-KDT89-DYFKP", "9d5584a2-2d85-419a-982c-a00888bb9ddf");
            AddProductKey("Core Single Language", "CoreSingleLanguage", "2WN2H-YGCQR-KFX6K-CD6TF-84YXQ", "8860fcd4-a77b-4a20-9045-a150ff11d609");
            AddProductKey("Enterprise", "Enterprise", "32JNW-9KQ84-P47T8-D8GGY-CWCK7", "458e1bec-837a-45f6-b9d5-925ed5d299de");
            AddProductKey("Enterprise N", "EnterpriseN", "JMNMF-RHW7P-DMY6X-RF3DR-X2BQT", "e14997e7-800a-4cf7-ad10-de4b45b578db");
            AddProductKey("Pro", "Professional", "NG4HW-VH26C-733KW-K6F98-J8CK4", "a98bcd6d-5343-4603-8afe-5908e4611112");
            AddProductKey("Pro N", "ProfessionalN", "XCVCF-2NXM9-723PB-MHCB7-2RYQQ", "ebf245c1-29a8-4daf-9cb1-38dfc608a8c8");
            AddProductKey("Pro WMC", "ProfessionalWMC", "GNBB8-YVD74-QJHX6-27H4K-8QHDG", "a00018a3-f20f-4632-bf7c-8daa5351c914");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 8 Embedded Keys that are Volume License
    /// </summary>
    public class KeyListWin8EmbeddedVL : KeyList
    {
        public KeyListWin8EmbeddedVL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Industry Enterprise", "EmbeddedIndustryE", "NKB3R-R2F8T-3XCDP-7Q2KW-XWYQ2", "18db1848-12e0-4167-b9d7-da7fcda507db");
            AddProductKey("Industry Pro", "EmbeddedIndustry", "RYXVT-BNQG7-VD29F-DBMRY-HT73M", "10018baf-ce21-4060-80bd-47fe74ed4dab");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Server 2012 Keys that are Volume License
    /// </summary>
    public class KeyListServer2012VL : KeyList
    {
        public KeyListServer2012VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Datacenter", "ServerDatacenter", "48HP8-DN98B-MYWDG-T2DCC-8W83P", "d3643d60-0c42-412d-a7d6-52e6635327f6");
            AddProductKey("MultiPoint Premium", "ServerMultiPointPremium", "XNH6W-2V9GX-RGJ4K-Y8X6F-QGJ2G", "95fd1c83-7df5-494a-be8b-1300e1c9d1cd");
            AddProductKey("MultiPoint Standard", "ServerMultiPointStandard", "HM7DN-YVMH3-46JC3-XYTG7-CYQJJ", "7d5486c7-e120-4771-b7f1-7b56c6d3170c");
            AddProductKey("Standard", "ServerStandard", "XC9B7-NBPP2-83J2H-RHMBY-92BT4", "f0f5ec41-0d55-4732-af02-440a44a3cf0f");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 8.1 Keys that are Volume License
    /// </summary>
    public class KeyListWin81VL : KeyList
    {
        public KeyListWin81VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Core", "Core", "M9Q9P-WNJJT-6PXPY-DWX8H-6XWKK", "fe1c3238-432a-43a1-8e25-97e7d1ef10f3");
            AddProductKey("Core N", "CoreN", "7B9N3-D94CG-YTVHR-QBPX3-RJP64", "78558a64-dc19-43fe-a0d0-8075b2a370a3");
            AddProductKey("Core ARM", "CoreARM", "XYTND-K6QKT-K2MRH-66RTM-43JKP", "ffee456a-cd87-4390-8e07-16146c672fd0");
            AddProductKey("Core Bing", "CoreConnected", "3PY8R-QHNP9-W7XQD-G6DPH-3J2C9", "e9942b32-2e55-4197-b0bd-5ff58cba8860");
            AddProductKey("Core Bing N", "CoreConnectedN", "Q6HTR-N24GM-PMJFP-69CD8-2GXKR", "c6ddecd6-2354-4c19-909b-306a3058484e");
            AddProductKey("Core Bing Country Specific", "CoreConnectedCountrySpecific", "R962J-37N87-9VVK2-WJ74P-XTMHR", "ba998212-460a-44db-bfb5-71bf09d1c68b");
            AddProductKey("Core Bing Single Language", "CoreConnectedSingleLanguage", "KF37N-VDV38-GRRTV-XH8X6-6F3BB", "b8f5e3a3-ed33-4608-81e1-37d6c9dcfd9c");
            AddProductKey("Core Country Specific", "CoreCountrySpecific", "NCTT7-2RGK8-WMHRF-RY7YQ-JTXG3", "db78b74f-ef1c-4892-abfe-1e66b8231df6");
            AddProductKey("Core Single Language", "CoreSingleLanguage", "BB6NG-PQ82V-VRDPW-8XVD2-V8P66", "c72c6a1d-f252-4e7e-bdd1-3fca342acb35");
            AddProductKey("Enterprise", "Enterprise", "MHF9N-XY6XB-WVXMC-BTDCT-MKKG7", "81671aaf-79d1-4eb1-b004-8cbbe173afea");
            AddProductKey("Enterprise N", "EnterpriseN", "TT4HM-HN7YT-62K67-RGRQJ-JFFXW", "113e705c-fa49-48a4-beea-7dd879b46b14");
            AddProductKey("Pro", "Professional", "GCRJD-8NW9H-F2CDX-CCM8D-9D6T9", "c06b6981-d7fd-4a35-b7b4-054742b7af67");
            AddProductKey("Pro N", "ProfessionalN", "HMCNV-VVBFX-7HMBH-CTY9B-B4FXY", "7476d79f-8e48-49b4-ab63-4d0b813a16e4");
            AddProductKey("Pro Student", "ProfessionalStudent", "MX3RK-9HNGX-K3QKC-6PJ3F-W8D7B", "e58d87b5-8126-4580-80fb-861b22f79296");
            AddProductKey("Pro Student N", "ProfessionalStudentN", "TNFGH-2R6PB-8XM3K-QYHX2-J4296", "cab491c7-a918-4f60-b502-dab75e334f40"); 
            AddProductKey("Pro WMC", "ProfessionalWMC", "789NJ-TQK6T-6XTH8-J39CJ-J8D3P", "096ce63d-4fac-48a9-82a9-61ae9e800e5f");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 8.1 Embedded Keys that are Volume License
    /// </summary>
    public class KeyListWin81EmbeddedVL : KeyList
    {
        public KeyListWin81EmbeddedVL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Industry Automotive", "EmbeddedIndustryA", "VHXM3-NR6FT-RY6RT-CK882-KW2CJ", "f7e88590-dfc7-4c78-bccb-6f3865b99d1a");
            AddProductKey("Industry Enterprise", "EmbeddedIndustryE", "FNFKF-PWTVT-9RC8H-32HB2-JB34X", "cd4e2d9f-5059-4a50-a92d-05d5bb1267c7");
            AddProductKey("Industry Pro", "EmbeddedIndustry", "NMMPB-38DD4-R2823-62W8D-VXKJB", "0ab82d54-47f4-4acb-818c-cc5bf0ecb649");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Server 2012 R2 Keys that are Volume License
    /// </summary>
    public class KeyListServer2012R2VL : KeyList
    {
        public KeyListServer2012R2VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Cloud Storage", "ServerCloudStorage", "3NPTF-33KPT-GGBPR-YX76B-39KDD", "b743a2be-68d4-4dd3-af32-92425b7bb623");
            AddProductKey("Datacenter", "ServerDatacenter", "W3GGN-FT8W3-Y4M27-J84CP-Q3VJ9", "00091344-1ea4-4f37-b789-01750ba6988c");
            AddProductKey("Essentials", "ServerSolution", "KNC87-3J2TX-XB4WP-VCPJV-M4FWM", "21db6ba4-9a7b-4a14-9e29-64a60c59301d");
            AddProductKey("Standard", "ServerStandard", "D2N9P-3P6X9-2R39C-7RTCD-MDVJX", "b3ca044e-a358-4d68-9883-aaa2941aca99");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 10 Keys that are Volume License
    /// </summary>
    public class KeyListWin10VL : KeyList
    {
        public KeyListWin10VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("Education", "Education", "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2", "e0c42288-980c-4788-a014-c080d2e1926e");
            AddProductKey("Education N", "EducationN", "2WH4N-8QGBV-H22JP-CT43Q-MDWWJ", "3c102355-d027-42c6-ad23-2e7ef8a02585");
            AddProductKey("Enterprise", "Enterprise", "NPPR9-FWDCX-D2C8J-H872K-2YT43", "73111121-5638-40f6-bc11-f1d7b0d64300");
            AddProductKey("Enterprise N", "EnterpriseN", "DPH2V-TTNVB-4X9Q3-TJR4H-KHJW4", "e272e3e2-732f-4c65-a8f0-484747d0d947");
            AddProductKey("Enterprise China Government", "EnterpriseG", "YYVX9-NTFWV-6MDM3-9PT4T-4M68B", "e0b2d383-d112-413f-8a80-97f373a5820c");
            AddProductKey("Enterprise China Government N", "EnterpriseGN", "44RPN-FTY23-9VTTB-MP9BX-T84FV", "e38454fb-41a4-4f59-a5dc-25080e354730");
            AddProductKey("Enterprise LTSB 2015", "EnterpriseS", "WNMTR-4C88C-JK8YV-HQ7T2-76DF9", "7b51a46c-0c04-4e8f-9af4-8496cca90d5e");
            AddProductKey("Enterprise LTSB 2015 N", "EnterpriseSN", "2F77B-TNFGY-69QQF-B8YKP-D69TJ", "87b838b7-41b6-4590-8318-5797951d8529");
            AddProductKey("Enterprise LTSB 2016", "EnterpriseS", "DCPHK-NFMTC-H88MJ-PFHPY-QJ4BJ", "2d5a5a60-3040-48bf-beb0-fcd770c20ce0");
            AddProductKey("Enterprise LTSB 2016 N", "EnterpriseSN", "QFFDN-GRT3P-VKWWX-X7T3R-8B639", "9f776d83-7156-45b2-8a5c-359b9c9f22a3");
            AddProductKey("Enterprise LTSC 2019", "EnterpriseS", "M7XTQ-FN8P6-TTKYV-9D4CC-J462D", "32d2fab3-e4a8-42c2-923b-4bf4fd13e6ee");
            AddProductKey("Enterprise LTSC 2019 N", "EnterpriseSN", "92NFX-8DJQP-P6BBQ-THF9C-7CG2H", "7103a333-b8c8-49cc-93ce-d37c09687f92");
            AddProductKey("Enterprise Remote Server", "ServerRdsh", "7NBT4-WGBQX-MP4H7-QXFF8-YP3KX", "e4db50ea-bda1-4566-b047-0ca50abc6f07");
            AddProductKey("Enterprise Virtual Desktops", "ServerRdsh", "CPWHC-NT2C7-VYW78-DHDB2-PG3GK", "ec868e65-fadf-4759-b23e-93fe37f2cc29");
            AddProductKey("Home", "Core", "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99", "58e97c99-f377-4ef1-81d5-4ad5522b5fd8");
            AddProductKey("Home N", "CoreN", "3KHY7-WNT83-DGQKR-F7HPR-844BM", "7b9e1751-a8da-4f75-9560-5fadfe3d8e38");
            AddProductKey("Home Country Specific", "CoreCountrySpecific", "PVMJN-6DFY6-9CCP6-7BKTT-D3WVR", "a9107544-f4a0-4053-a96a-1479abdef912");
            AddProductKey("Home Single Language", "CoreSingleLanguage", "7HNRX-D7KGG-3K4RQ-4WPJ4-YTDFH", "cd918a57-a41b-4c82-8dce-1a538e221a83");
            AddProductKey("Lean", "CloudE", "NBTWJ-3DR69-3C4V8-C26MC-GQ9M6", "0df4f814-3f57-4b8b-9a9d-fddadcd69fac");
            AddProductKey("Pro", "Professional", "W269N-WFGWX-YVC9B-4J6C9-T83GX", "2de67392-b7a7-462a-b1ca-108dd189f588");
            AddProductKey("Pro N", "ProfessionalN", "MH37W-N47XK-V7XM9-C7227-GCQG9", "a80b5abf-76ad-428b-b05d-a47d2dffeebf");
            AddProductKey("Pro Education", "ProfessionalEducation", "6TP4R-GNPTD-KYYHQ-7B7DP-J447Y", "3f1afc82-f8ac-4f6c-8005-1d233e606eee");
            AddProductKey("Pro Education N", "ProfessionalEducationN", "YVWGF-BXNMC-HTQYQ-CPQ99-66QFC", "5300b18c-2e33-4dc2-8291-47ffcec746dd");        
            AddProductKey("Pro Workstation", "ProfessionalWorkstation", "NRG8B-VKK3Q-CXVCJ-9G2XF-6Q84J", "82bbc092-bc50-4e16-8e18-b74fc486aec3");
            AddProductKey("Pro Workstation N", "ProfessionalWorkstationN", "9FNHH-K3HBT-3W4TD-6383H-6XYWF", "4b1571d3-bafb-4b40-8087-a961be2caf65");
            AddProductKey("S", "Cloud", "V3WVW-N2PV2-CGWC3-34QGF-VMJ2C", "d4ef7282-3d2c-4cf0-9976-8854e64a8d1e");
            AddProductKey("S N", "CloudN", "NH9J3-68WK7-6FB93-4K3DF-DJ4F6", "af5c9381-9240-417d-8d35-eb40cd03e484");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows 10 Embedded Keys that are Volume License
    /// </summary>
    public class KeyListWin10EmbeddedVL : KeyList
    {
        public KeyListWin10EmbeddedVL()
        {
            KeyTable = new List<KeyData>();
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Server 2016 Keys that are Volume License
    /// </summary>
    public class KeyListServer2016VL : KeyList
    {
        public KeyListServer2016VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("ARM", "ServerARM64", "K9FYF-G6NCK-73M32-XMVPY-F9DRR", "43d9af6e-5e86-4be8-a797-d072a046896c");
            AddProductKey("ARM (Alternate)", "ServerARM64", "HNGCC-Y38KG-QVK8D-WMWRK-X86VK", "f7af7d09-40e4-419c-a49b-eae366689ebd");
            AddProductKey("Azure Core", "ServerAzureCor", "VP34G-4NPPG-79JTQ-864T4-R3MQX", "3dbf341b-5f6c-4fa7-b936-699dce9e263f");
            AddProductKey("Cloud Storage", "ServerCloudStorage", "QN4C6-GBJD2-FB422-GHWJK-GJG2R", "7b4433f4-b1e7-4788-895a-c45378d38253");
            AddProductKey("Datacenter LTSB", "ServerDatacenter", "CB7KF-BWN84-R7R2Y-793K2-8XDDG", "21c56779-b449-4d20-adfc-eece0e1ad74b");
            AddProductKey("Datacenter SAC (1709)", "ServerDatacenterACor", "6Y6KB-N82V8-D8CQV-23MJW-BWTG6", "e49c08e7-da82-42f8-bde2-b570fbcae76c");
            AddProductKey("Datacenter SAC (1803)", "ServerDatacenterACor", "2HXDN-KRXHB-GPYC7-YCKFJ-7FVDG", "e49c08e7-da82-42f8-bde2-b570fbcae76c");
            AddProductKey("Essentials", "ServerSolution", "JCKRF-N37P4-C2D82-9YXRT-4M63B", "2b5a1b0f-a5ab-4c54-ac2f-a6d94824a283");
            AddProductKey("Standard LTSB", "ServerStandard", "WC2BQ-8NRM3-FDDYY-2BFGV-KHKQY", "8c1c5410-9f39-4805-8c9d-63a07706358f");
            AddProductKey("Standard SAC (1709)", "ServerStandardACor", "DPCNP-XQFKJ-BJF7R-FRC8D-GF6G4", "61c5ef22-f14f-4553-a824-c4b31e84b100");
            AddProductKey("Standard SAC (1803)", "ServerStandardACor", "PTXN8-JFHJM-4WC78-MPCBR-9W4KR", "61c5ef22-f14f-4553-a824-c4b31e84b100");
        }
    }

    /// <summary>
    /// Create a KeyList for Windows Server 2019 Keys that are Volume License
    /// </summary>
    public class KeyListServer2019VL : KeyList
    {
        public KeyListServer2019VL()
        {
            KeyTable = new List<KeyData>();
            AddProductKey("ARM", "ServerARM64", "GRFBW-QNDC4-6QBHG-CCK3B-2PR88", "8de8eb62-bbe0-40ac-ac17-f75595071ea3");
            AddProductKey("Azure Core", "ServerAzureCor", "FDNH6-VW9RW-BXPJ7-4XTYG-239TB", "a99cc1f0-7719-4306-9645-294102fbff95");
            AddProductKey("Datacenter LTSC", "ServerDatacenter", "WMDGN-G9PQG-XVVXX-R3X43-63DFG", "34e1ae55-27f8-4950-8877-7a03be5fb181");
            AddProductKey("Datacenter SAC (1809)", "ServerDatacenterACor", "6NMRW-2C8FM-D24W7-TQWMY-CWH2D", "90c362e5-0da1-4bfd-b53b-b87d309ade43");
            AddProductKey("Essentials", "ServerSolution", "WVDHN-86M7X-466P6-VHXV7-YY726", "034d3cbb-5d4b-4245-b3f8-f84571314078");
            AddProductKey("Standard LTSC", "ServerStandard", "N69G4-B89J2-4G8F4-WWYCC-J464C", "de32eafd-aaee-4662-9444-c1befb41bde2");
            AddProductKey("Standard SAC (1809)", "ServerStandardACor", "N2KJX-J94YW-TQVFB-DG9YT-724CC", "73e3957c-fc0c-400d-9184-5f7b6f2eb409");
        }
    }

    /// <summary>
    /// Methods for decoding Keys from their DigitalProductID stored in the registry
    /// </summary>
    public static class KeyFinder
    {
		/// <summary>
		/// Get the OEM:DM Product Key from the System Firmware MSDM Table
		/// </summary>
        /// <returns>List of all discovered Product Keys</returns>
		public static List<string> GetKeysFromMSDMWindows()
		{
			List<string> detectedKeys = new List<string>();

            // Return if OS is not at least Windows 8
		    if (OSVersion.GetWindowsNumber() < 6.2)
		    {
		        return detectedKeys;
		    }

			// Read System Firmware
			const uint firmwareTableProviderSignature = 0x41435049; // 'ACPI' in Hexadecimal
			uint bufferSize = NativeMethods.EnumSystemFirmwareTables(firmwareTableProviderSignature, IntPtr.Zero, 0);
			IntPtr pFirmwareTableBuffer = Marshal.AllocHGlobal((int)bufferSize);
			byte[] buffer = new byte[bufferSize];
			NativeMethods.EnumSystemFirmwareTables(firmwareTableProviderSignature, pFirmwareTableBuffer, bufferSize);
			Marshal.Copy(pFirmwareTableBuffer, buffer, 0, buffer.Length);
			Marshal.FreeHGlobal(pFirmwareTableBuffer);

			// Check for MSDM Table
			if (Encoding.ASCII.GetString(buffer).Contains("MSDM"))
			{
				// Get MSDM Table
				const uint firmwareTableID = 0x4d44534d; // Reversed 'MSDM' in Hexadecimal
				bufferSize = NativeMethods.GetSystemFirmwareTable(firmwareTableProviderSignature, firmwareTableID, IntPtr.Zero, 0);
				buffer = new byte[bufferSize];
				pFirmwareTableBuffer = Marshal.AllocHGlobal((int)bufferSize);
				NativeMethods.GetSystemFirmwareTable(firmwareTableProviderSignature, firmwareTableID, pFirmwareTableBuffer, bufferSize);
				Marshal.Copy(pFirmwareTableBuffer, buffer, 0, buffer.Length);
				Marshal.FreeHGlobal(pFirmwareTableBuffer);

				// Get OEM:DM Key
				Encoding encoding = Encoding.GetEncoding(0x4e4);
				detectedKeys.Add(encoding.GetString(buffer, 56, 29));
			}

			return detectedKeys;
		}

        /// <summary>
        /// Call GetKeysFromRegistry() with the needed Registry Path for Office
        /// </summary>
        /// <returns>List of all discovered Product Keys</returns>
        public static List<string> GetKeysFromRegistryOffice()
        {
            int officeNumber = OfficeVersion.GetOfficeNumber();
            switch (officeNumber)
            {
                case 14:
                case 15:
                case 16:
                    if (!OfficeVersion.IsOfficeVirtual())
                    {
                        // Traditional Check
                        if (Architecture.GetOfficeArch() != Architecture.WOW)
                        {
                            return GetKeysFromRegistry("SOFTWARE\\Microsoft\\Office\\" + officeNumber + ".0\\Registration", true);
                        }
                        return GetKeysFromRegistry("SOFTWARE\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration", true);
                    }
                    else
                    {
                        // Virtual Check
                        if (Architecture.GetOfficeArch() != Architecture.WOW)
                        {
                            return GetKeysFromRegistry("SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Microsoft\\Office\\" + officeNumber + ".0\\Registration", true);
                        }
                        return GetKeysFromRegistry("SOFTWARE\\Microsoft\\Office\\ClickToRun\\REGISTRY\\MACHINE\\Software\\Wow6432Node\\Microsoft\\Office\\" + officeNumber + ".0\\Registration", true);
                    }
                default:
                    throw new ApplicationException("Unsupported Microsoft Office Edition!");
            }
        }

        /// <summary>
        /// Call GetKeysFromRegistry() with the needed Registry Path for Windows Current Product Key
        /// </summary>
        /// <returns>List of all discovered Product Keys</returns>
        public static List<string> GetKeysFromRegistryWindows()
        {
            return GetKeysFromRegistry("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
        }

        /// <summary>
        /// Call GetKeysFromRegistry() with the needed Registry Path for Windows Default Product Key
        /// </summary>
        /// <returns>List of all discovered Product Keys</returns>
        public static List<string> GetDefaultKeysFromRegistryWindows()
        {
            return GetKeysFromRegistry("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\DefaultProductKey");
        }

        /// <summary>
        /// Go through all needed registry keys and determine if they have a DigitalProductID
        /// </summary>
        /// <param name="registryPath">Registry Key to search for DigitalProductID</param>
        /// <param name="recursiveSearch">Search all Subkeys for DigitalProductID</param>
        /// <returns>List of all discovered Product Keys</returns>
        private static List<string> GetKeysFromRegistry(string registryPath, bool recursiveSearch = false)
        {
            List<string> detectedKeys = new List<string>();

            using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath, false))
            {
                if (registry != null)
                {
                    // Windows
                    if (registry.GetValue("DigitalProductID") != null)
                    {
                        detectedKeys.Add(DecodeProductKey(GetRegistryDigitalProductId(registryPath), 52));
                    }
                    // Office
                    if (recursiveSearch)
                    {
                        foreach (string subKeyName in registry.GetSubKeyNames())
                        {
                            using (RegistryKey registrySubKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath + Path.DirectorySeparatorChar + subKeyName, false))
                            {
                                if (registrySubKey == null || registrySubKey.GetValue("DigitalProductID") == null)
                                {
                                    continue;
                                }
                            }
                            detectedKeys.Add(DecodeProductKey(GetRegistryDigitalProductId(registryPath + Path.DirectorySeparatorChar + subKeyName), 808)); // 0x328 = 808
                        }
                    }
                }
            }

            return detectedKeys;
        }

        /// <summary>
        /// Get DigitalProductID from a Registry Key
        /// </summary>
        /// <param name="registryPath">Registry Key containing a DigitalProductID</param>
        /// <returns>Byte array containing DigitalProductID data</returns>
        private static byte[] GetRegistryDigitalProductId(string registryPath)
        {
            byte[] digitalProductId = null;
            using (RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath, false))
            {
                if (registry != null)
                {
                    digitalProductId = registry.GetValue("DigitalProductID") as byte[];
                }
            }
            return digitalProductId;
        }

        /// <summary>
        /// Decode Data read in from a DigitalProductID
        /// </summary>
        /// <param name="digitalProductId">Byte array containing DigitalProductID data</param>
        /// <param name="keyStartIndex">Where in the byte array does the Product Key start</param>
        /// <returns>Product Key contained in the DigitalProductID</returns>
        private static string DecodeProductKey(IList<byte> digitalProductId, int keyStartIndex)
        {
            int keyEndIndex = keyStartIndex + 15;

            const int numLetters = 24;
            // Possible alpha-numeric characters in product key.
            char[] digits = {'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9'};

            // Check if Windows 8/Office 2013 Style Key (Can contain the letter "N")
            int containsN = (digitalProductId[keyStartIndex + 14] / 6) & 1;
            digitalProductId[keyStartIndex + 14] = (byte)((digitalProductId[keyStartIndex + 14] & 0xF7) | ((containsN & 2) * 4));

            // Length of decoded product key
            const int decodeLength = 29;

            // Length of decoded product key in byte-form.
            // Each byte represents 2 chars.
            const int decodeStringLength = 15;

            // Array of containing the decoded product key.
            char[] decodedChars = new char[decodeLength];

            // Extract byte 52 to 67 inclusive.
            List<byte> hexPid = new List<byte>();
            for (int i = keyStartIndex; i <= keyEndIndex; i++)
            {
                hexPid.Add(digitalProductId[i]);
            }
            for (int i = decodeLength - 1; i >= 0; i--)
            {
                // Every sixth char is a separator.
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    // Do the actual decoding.
                    int digitMapIndex = 0;
                    for (int j = decodeStringLength - 1; j >= 0; j--)
                    {
                        int byteValue = (digitMapIndex << 8) | hexPid[j];
                        hexPid[j] = (byte)(byteValue / numLetters);
                        digitMapIndex = byteValue % numLetters;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            // Remove first character and put N in the right place
            if (containsN != 0)
            {
                int firstLetterIndex = 0;
                for (int index = 0; index < numLetters; index++)
                {
                    if (decodedChars[0] != digits[index])
                    {
                        continue;
                    }
                    firstLetterIndex = index;
                    break;
                }
                string keyWithN = new string(decodedChars);

                keyWithN = keyWithN.Replace("-", string.Empty).Remove(0, 1);
                keyWithN = keyWithN.Substring(0, firstLetterIndex) + "N" + keyWithN.Remove(0, firstLetterIndex);
                keyWithN = keyWithN.Substring(0, 5) + "-" + keyWithN.Substring(5, 5) + "-" + keyWithN.Substring(10, 5) + "-" + keyWithN.Substring(15, 5) + "-" + keyWithN.Substring(20, 5);

                return keyWithN;
            }

            return new string(decodedChars);
        }
    }
}
