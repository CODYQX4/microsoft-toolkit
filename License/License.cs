﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Xml;
using ProductDetection;

namespace License
{
    /// <summary>
    /// Lookup the error description for a given Software Licensing error code
    /// </summary>
    public static class LicenseErrorCode
    {
        #region License State Error Constants
        public const string ErrBroken = "<No installed product keys installed or possible license error>";
        public const string ErrKeyless = "<No installed product keys detected>";
        public const string ErrNoKMS = "<Cannot Activate because this product is incapable of KMS Activation.>";
        #endregion
        #region SLError Constants
        /// <summary>
        /// SLError.xml obtained from Microsoft Office 2010 + Microsoft Office 2013 CP, with some additions.
        /// </summary>
        private const string Slerror = @"<?xml version=""1.0"" encoding=""utf-8""?><Strings>
        <err0xC004B001>The activation server determined that the license is invalid.</err0xC004B001>
        <err0xC004B002>The activation server determined that the license is invalid.</err0xC004B002>
        <err0xC004B003>The activation server determined that the license is invalid.</err0xC004B003>
        <err0xC004B004>The activation server determined that the license is invalid.</err0xC004B004>
        <err0xC004B005>The activation server determined that the license is invalid.</err0xC004B005>
        <err0xC004B006>The activation server determined that the license is invalid.</err0xC004B006>
        <err0xC004B007>The activation server reported that the computer could not connect to the activation server.</err0xC004B007>
        <err0xC004B008>The activation server determined that the product could not be activated.</err0xC004B008>
        <err0xC004B009>The activation server determined that the license is invalid.</err0xC004B009>
        <err0xC004B010>The activation server determined that required business token entry cannot be found.</err0xC004B010>
        <err0xC004B011>The activation server determined that your computer clock time is not correct. You must correct your clock before you can activate.</err0xC004B011>
        <err0xC004B100>The activation server determined that the product could not be activated.</err0xC004B100>
        <err0xC004C001>The activation server determined the specified product key is invalid.</err0xC004C001>
        <err0xC004C002>The activation server determined there is a problem with the specified product key.</err0xC004C002>
        <err0xC004C003>The activation server determined the specified product key has been blocked.</err0xC004C003>
        <err0xC004C004>The activation server determined the specified product key is invalid. </err0xC004C004>
        <err0xC004C005>The activation server determined the license is invalid.</err0xC004C005>
        <err0xC004C006>The activation server determined the license is invalid.</err0xC004C006>
        <err0xC004C007>The activation server determined the specified product key is invalid.</err0xC004C007>
        <err0xC004C008>The activation server reported that the product key has exceeded its unlock limit.</err0xC004C008>
        <err0xC004C009>The activation server determined the license is invalid.</err0xC004C009>
        <err0xC004C00A>The activation server determined the license is invalid.</err0xC004C00A>
        <err0xC004C00B>The activation server determined the license is invalid.</err0xC004C00B>
        <err0xC004C00C>The activation server experienced an error.</err0xC004C00C>
        <err0xC004C00D>The activation server determined the product key is not valid.</err0xC004C00D>
        <err0xC004C00E>The activation server determined the specified product key is invalid.</err0xC004C00E>
        <err0xC004C00F>The activation server determined the specified product key is invalid.</err0xC004C00F>
        <err0xC004C010>The activation server determined the specified product key is invalid.</err0xC004C010>
        <err0xC004C011>The activation server determined the license is invalid.</err0xC004C011>
        <err0xC004C012>The activation server experienced a network error.</err0xC004C012>
        <err0xC004C013>The activation server experienced an error.</err0xC004C013>
        <err0xC004C014>The activation server experienced an error.</err0xC004C014>
        <err0xC004C015>The activation server experienced an error.</err0xC004C015>        
        <err0xC004C016>The activation server reported that the specified product key cannot be used for online activation.</err0xC004C016>
        <err0xC004C017>The activation server determined the specified product key has been blocked for this geographic location.</err0xC004C017>
        <err0xC004C020>The activation server reported that the Multiple Activation Key has exceeded its limit.</err0xC004C020>
        <err0xC004C021>The activation server reported that the Multiple Activation Key extension limit has been exceeded.</err0xC004C021>
        <err0xC004C022>The activation server reported that the re-issuance limit was not found. </err0xC004C022>
        <err0xC004C023>The activation server reported that the override request was not found. </err0xC004C023>      
        <err0xC004C030>The activation server reported that time based activation attempted before start date.</err0xC004C030>
        <err0xC004C031>The activation server reported that time based activation attempted after end date.</err0xC004C031>
        <err0xC004C032>The activation server reported that new time based activation not available.</err0xC004C032>
        <err0xC004C033>The activation server reported that the time based product key is not configured for activation.</err0xC004C033>
        <err0xC004C04F>The activation server reported that no business rules available to activate specified product key.</err0xC004C04F>
        <err0xC004C050>The activation server experienced a general error.</err0xC004C050>
        <err0xC004C051>The activation server determined the license is invalid.</err0xC004C051>
        <err0xC004C052>The activation server determined there is a problem with the specified product key.</err0xC004C052>
        <err0xC004C060>The activation server determined the specified product key has been blocked.</err0xC004C060>
        <err0xC004C3FF>The activation server determined the VGA service response is not available in the expected format.</err0xC004C3FF>
        <err0xC004C700>The activation server reported that business rule cound not find required input.</err0xC004C700>
        <err0xC004C750>The activation server reported that NULL value specified for business property name and Id.</err0xC004C750>
        <err0xC004C751>The activation server reported that property name specifies unknown property.</err0xC004C751>
        <err0xC004C752>The activation server reported that property Id specifies unknown property.</err0xC004C752>
        <err0xC004C755>The activation server reported that it failed to update product key binding.</err0xC004C755>
        <err0xC004C756>The activation server reported that it failed to insert product key binding.</err0xC004C756>
        <err0xC004C757>The activation server reported that it failed to delete product key binding.</err0xC004C757>
        <err0xC004C758>The activation server reported that it failed to process input XML for product key bindings.</err0xC004C758>
        <err0xC004C75A>The activation server reported that it failed to insert product key property.</err0xC004C75A>
        <err0xC004C75B>The activation server reported that it failed to update product key property.</err0xC004C75B>
        <err0xC004C75C>The activation server reported that it failed to delete product key property.</err0xC004C75C>
        <err0xC004C764>The activation server reported that the product key type is unknown.</err0xC004C764>
        <err0xC004C770>The activation server reported that the product key type is being used by another user.</err0xC004C770>
        <err0xC004C780>The activation server reported that it failed to insert product key record.</err0xC004C780>
        <err0xC004C781>The activation server reported that it failed to update product key record.</err0xC004C781>
        <err0xC004C401>The Vista Genuine Advantage Service determined that the installation is not genuine. </err0xC004C401>
        <err0xC004C600>The Vista Genuine Advantage Service determined that the installation is not genuine. </err0xC004C600>
        <err0xC004C801>The activation server determined the specified product key is invalid.</err0xC004C801>
        <err0xC004C802>The activation server determined the specified product key is invalid.</err0xC004C802>
        <err0xC004C803>The activation server determined the specified product key has been revoked.</err0xC004C803>
        <err0xC004C804>The activation server determined the specified product key is invalid.</err0xC004C804>
        <err0xC004C805>The activation server determined the specified product key is invalid.</err0xC004C805>
        <err0xC004C810>The activation server determined the specified product key is invalid.</err0xC004C810>
        <err0xC004C811>The activation server determined the license is invalid.</err0xC004C811>
        <err0xC004C812>The activation server determined that the specified product key has exceeded its activation count.</err0xC004C812>
        <err0xC004C813>The activation server determined the license is invalid.</err0xC004C813>
        <err0xC004C814>The activation server determined the specified product key is invalid.</err0xC004C814>
        <err0xC004C815>The activation server determined the license is invalid.</err0xC004C815>
        <err0xC004C816>The activation server reported that the specified product key cannot be used for online activation.</err0xC004C816>
        <err0xC004C291>Genuine Validation determined the license state is invalid.</err0xC004C291>
        <err0xC004C292>Genuine Validation determined the license state is invalid.</err0xC004C292>
        <err0xC004C2F6>Genuine Validation determined the validation input template identifier is invalid.</err0xC004C2F6>
        <err0xC004C2FA>Genuine Validation determined the validation input data blob is invalid.</err0xC004C2FA>
        <err0xC004C327>Genuine Validation determined the validation input data blob parameter is invalid.</err0xC004C327>
        <err0xC004C328>Genuine Validation determined the client token data is invalid.</err0xC004C328>
        <err0xC004C329>Genuine Validation determined the offline data blob is invalid.</err0xC004C329>
        <err0xC004C32A>Genuine Validation determined the offline data blob parameter is invalid.</err0xC004C32A>
        <err0xC004C32B>Genuine Validation determined the validation template identifier is invalid for this version of the Windows operating system.</err0xC004C32B>
        <err0xC004C32C>Genuine Validation determined the offline genuine blob is revoked.</err0xC004C32C>
        <err0xC004C32D>Genuine Validation determined the offline genuine blob is not found.</err0xC004C32D>
        <err0xC004C401>Genuine Validation determined the product key is invalid for this version of the Windows operating system.</err0xC004C401>
        <err0xC004C4A1>Genuine Validation determined the file hash is invalid.</err0xC004C4A1>
        <err0xC004C4A2>Genuine Validation determined the product key has been blocked.</err0xC004C4A2>
        <err0xC004C4A4>Genuine Validation determined the product key type is invalid.</err0xC004C4A4>
        <err0xC004C4A5>Genuine Validation determined the product key is invalid.</err0xC004C4A5>
        <err0xC004C4A7>Genuine Validation determined the OEM or Volume binding data is invalid.</err0xC004C4A7>
        <err0xC004C4A8>Genuine Validation determined the license state is invalid.</err0xC004C4A8>
        <err0xC004C4A9>Genuine Validation determined the specified product key has been blocked for this geographic location.</err0xC004C4A9>
        <err0xC004C4AB>Genuine Validation detected Windows licensing exploits.</err0xC004C4AB>
        <err0xC004C4AC>Genuine Validation determined the token activation data is invalid.</err0xC004C4AC>
        <err0xC004C4AD>Genuine Validation detected tampered Windows binaries.</err0xC004C4AD>
        <err0xC004C4AE>Genuine Validation detected tampered Windows binaries.</err0xC004C4AE>
        <err0xC004C4AF>Genuine Validation determined the active directory activation data is invalid.</err0xC004C4AF>
        <err0xC004C4B0>Genuine Validation detected Windows licensing exploits.</err0xC004C4B0>
        <err0xC004C4B1>Genuine Validation detected Windows licensing exploits.</err0xC004C4B1>
        <err0xC004C600>Genuine Validation determined your copy of Windows is not genuine.</err0xC004C600>
        <err0xC004C531>Genuine Validation detected Windows licensing exploits.</err0xC004C531>
        <err0xC004C532>Genuine Validation determined the license state is in notification due to expired grace.</err0xC004C532>
        <err0xC004C533>Genuine Validation determined the license state is in notification.</err0xC004C533>
        <err0xC004E001>The Software Licensing Service determined that the specified context is invalid.</err0xC004E001>
        <err0xC004E002>The Software Licensing Service reported that the license store contains inconsistent data.</err0xC004E002>
        <err0xC004E003>The Software Licensing Service reported that license evaluation failed.</err0xC004E003>
        <err0xC004E004>The Software Licensing Service reported that the license has not been evaluated.</err0xC004E004>
        <err0xC004E005>The Software Licensing Service reported that the license is not activated.</err0xC004E005>
        <err0xC004E006>The Software Licensing Service reported that the license contains invalid data.</err0xC004E006>
        <err0xC004E007>The Software Licensing Service reported that the license store does not contain the requested license.</err0xC004E007>
        <err0xC004E008>The Software Licensing Service reported that the license property is invalid.</err0xC004E008>
        <err0xC004E009>The Software Licensing Service reported that the license store is not initialized.</err0xC004E009>
        <err0xC004E00A>The Software Licensing Service reported that the license store is already initialized.</err0xC004E00A>
        <err0xC004E00B>The Software Licensing Service reported that the license property is invalid.</err0xC004E00B>
        <err0xC004E00C>The Software Licensing Service reported that the license could not be opened or created.</err0xC004E00C>
        <err0xC004E00D>The Software Licensing Service reported that the license could not be written.</err0xC004E00D>
        <err0xC004E00E>The Software Licensing Service reported that the license store could not read the license file.</err0xC004E00E>
        <err0xC004E00F>The Software Licensing Service reported that the license property is corrupted.</err0xC004E00F>
        <err0xC004E010>The Software Licensing Service reported that the license property is missing.</err0xC004E010>
        <err0xC004E011>The Software Licensing Service reported that the license store contains an invalid license file.</err0xC004E011>
        <err0xC004E012>The Software Licensing Service reported that the license store failed to start synchronization properly.</err0xC004E012>
        <err0xC004E013>The Software Licensing Service reported that the license store failed to synchronize properly.</err0xC004E013>
        <err0xC004E014>The Software Licensing Service reported that the license property is invalid.</err0xC004E014>
        <err0xC004E015>The Software Licensing Service reported that license consumption failed.</err0xC004E015>
        <err0xC004E016>The Software Licensing Service reported that the product key is invalid.</err0xC004E016>
        <err0xC004E017>The Software Licensing Service reported that the product key is invalid.</err0xC004E017>
        <err0xC004E018>The Software Licensing Service reported that the product key is invalid.</err0xC004E018>
        <err0xC004E019>The Software Licensing Service determined that validation of the specified product key failed.</err0xC004E019>
        <err0xC004E01A>The Software Licensing Service reported that invalid add-on information was found. </err0xC004E01A>
        <err0xC004E01B>The Software Licensing Service reported that not all hardware information could be collected. </err0xC004E01B>
        <err0xC004E01C>This evaluation product key is no longer valid.</err0xC004E01C>
        <err0xC004E01D>The new product key cannot be used on this installation of Windows. Type a different product key. (CD-AB)</err0xC004E01D>
        <err0xC004E01E>The new product key cannot be used on this installation of Windows. Type a different product key. (AB-AB)</err0xC004E01E>
        <err0xC004E01F>The new product key cannot be used on this installation of Windows. Type a different product key. (AB-CD)</err0xC004E01F>
        <err0xC004E020>The Software Licensing Service reported that there is a mismatched between a policy value and information stored in the OtherInfo section.</err0xC004E020>
        <err0xC004E021>The Software Licensing Service reported that the Genuine information contained in the license is not consistent.</err0xC004E021>
        <err0xC004E022>The Software Licensing Service reported that the secure store id value in license does not match with the current value.</err0xC004E022>
        <err0xC004E023>The Software Licensing Service reported that the notification rules appear to be invalid.</err0xC004E023>
        <err0xC004E024>The Software Licensing Service reported that the reported machine data appears to be invalid.</err0xC004E024>
        <err0xC004E025>The Software Licensing Service reported that the data hash does not correspond to the data. </err0xC004E025>
        <err0x8004E101>The Software Licensing Service reported that the Token Store file version is invalid. </err0x8004E101>
        <err0x8004E102>The Software Licensing Service reported that the Token Store contains an invalid descriptor table. </err0x8004E102>
        <err0x8004E103>The Software Licensing Service reported that the Token Store contains a token with an invalid header/footer. </err0x8004E103>
        <err0x8004E104>The Software Licensing Service reported that a Token Store token has an invalid name. </err0x8004E104>
        <err0x8004E105>The Software Licensing Service reported that a Token Store token has an invalid extension. </err0x8004E105>
        <err0x8004E106>The Software Licensing Service reported that the Token Store contains a duplicate token. </err0x8004E106>
        <err0x8004E107>The Software Licensing Service reported that a token in the Token Store has a size mismatch. </err0x8004E107>
        <err0x8004E108>The Software Licensing Service reported that a token in the Token Store contains an invalid hash. </err0x8004E108>
        <err0x8004E109>The Software Licensing Service reported that the Token Store was unable to read a token. </err0x8004E109>
        <err0x8004E10A>The Software Licensing Service reported that the Token Store was unable to write a token. </err0x8004E10A>
        <err0x8004E10B>The Software Licensing Service reported that the Token Store attempted an invalid file operation. </err0x8004E10B>
        <err0x8004E10C>The Software Licensing Service reported that there is no active transaction. </err0x8004E10C>
        <err0x8004E10D>The Software Licensing Service reported that the Token Store file header is invalid. </err0x8004E10D>
        <err0x8004E10E>The Software Licensing Service reported that a Token Store token descriptor is invalid. </err0x8004E10E>
        <err0xC004F001>The Software Licensing Service reported an internal error.</err0xC004F001>
        <err0xC004F002>The Software Licensing Service reported that rights consumption failed.</err0xC004F002>
        <err0xC004F003>The Software Licensing Service reported that the required license could not be found.</err0xC004F003>
        <err0xC004F004>The Software Licensing Service reported that the product key does not match the range defined in the license.</err0xC004F004>
        <err0xC004F005>The Software Licensing Service reported that the product key does not match the product key for the license.</err0xC004F005>
        <err0xC004F006>The Software Licensing Service reported that the signature file for the license is not available.</err0xC004F006>
        <err0xC004F007>The Software Licensing Service reported that the license could not be found.</err0xC004F007>
        <err0xC004F008>The Software Licensing Service reported that the license could not be found.</err0xC004F008>
        <err0xC004F009>The Software Licensing Service reported that the grace period expired.</err0xC004F009>
        <err0xC004F00A>The Software Licensing Service reported that the application ID does not match the application ID for the license.</err0xC004F00A>
        <err0xC004F00B>The Software Licensing Service reported that the product identification data is not available.</err0xC004F00B>
        <err0x4004F00C>The Software Licensing Service reported that the application is running within the valid grace period.</err0x4004F00C>
        <err0x4004F00D>The Software Licensing Service reported that the application is running within the valid out of tolerance grace period.</err0x4004F00D>
        <err0xC004F00E>The Software Licensing Service determined that the license could not be used by the current version of the security processor component.</err0xC004F00E>
        <err0xC004F00F>The Software Licensing Service reported that the hardware ID binding is beyond the level of tolerance.</err0xC004F00F>
        <err0xC004F010>The Software Licensing Service reported that the product key is invalid.</err0xC004F010>
        <err0xC004F011>The Software Licensing Service reported that the license file is not installed.</err0xC004F011>
        <err0xC004F012>The Software Licensing Service reported that the call has failed because the value for the input key was not found.</err0xC004F012>
        <err0xC004F013>The Software Licensing Service determined that there is no permission to run the software.</err0xC004F013>
        <err0xC004F014>The Software Licensing Service reported that the product key is not available.</err0xC004F014>
        <err0xC004F015>The Software Licensing Service reported that the license is not installed.</err0xC004F015>
        <err0xC004F016>The Software Licensing Service determined that the request is not supported.</err0xC004F016>
        <err0xC004F017>The Software Licensing Service reported that the license is not installed.</err0xC004F017>
        <err0xC004F018>The Software Licensing Service reported that the license does not contain valid location data for the activation server.</err0xC004F018>
        <err0xC004F019>The Software Licensing Service determined that the requested event ID is invalid.</err0xC004F019>
        <err0xC004F01A>The Software Licensing Service determined that the requested event is not registered with the service.</err0xC004F01A>
        <err0xC004F01B>The Software Licensing Service reported that the event ID is already registered.</err0xC004F01B>
        <err0xC004F01C>The Software Licensing Service reported that the license is not installed.</err0xC004F01C>
        <err0xC004F01D>The Software Licensing Service reported that the verification of the license failed.</err0xC004F01D>
        <err0xC004F01E>The Software Licensing Service determined that the input data type does not match the data type in the license.</err0xC004F01E>
        <err0xC004F01F>The Software Licensing Service determined that the license is invalid.</err0xC004F01F>
        <err0xC004F020>The Software Licensing Service determined that the license package is invalid.</err0xC004F020>
        <err0xC004F021>The Software Licensing Service reported that the validity period of the license has expired.</err0xC004F021>
        <err0xC004F022>The Software Licensing Service reported that the license authorization failed.</err0xC004F022>
        <err0xC004F023>The Software Licensing Service reported that the license is invalid.</err0xC004F023>
        <err0xC004F024>The Software Licensing Service reported that the license is invalid.</err0xC004F024>
        <err0xC004F025>The Software Licensing Service reported that the action requires administrator privilege.</err0xC004F025>
        <err0xC004F026>The Software Licensing Service reported that the required data is not found.</err0xC004F026>
        <err0xC004F027>The Software Licensing Service reported that the license is tampered.</err0xC004F027>
        <err0xC004F028>The Software Licensing Service reported that the policy cache is invalid.</err0xC004F028>
        <err0xC004F029>The Software Licensing Service cannot be started in the current OS mode.</err0xC004F029>
        <err0xC004F02A>The Software Licensing Service reported that the license is invalid.</err0xC004F02A>
        <err0xC004F02C>The Software Licensing Service reported that the format for the offline activation data is incorrect.</err0xC004F02C>
        <err0xC004F02D>The Software Licensing Service determined that the version of the offline Confirmation ID (CID) is incorrect.</err0xC004F02D>
        <err0xC004F02E>The Software Licensing Service determined that the version of the offline Confirmation ID (CID) is not supported.</err0xC004F02E>
        <err0xC004F02F>The Software Licensing Service reported that the length of the offline Confirmation ID (CID) is incorrect.</err0xC004F02F>
        <err0xC004F030>The Software Licensing Service determined that the Installation ID (IID) or the Confirmation ID (CID) could not been saved.</err0xC004F030>
        <err0xC004F031>The Installation ID (IID) and the Confirmation ID (CID) do not match. Please confirm the IID and reacquire a new CID if necessary.</err0xC004F031>
        <err0xC004F032>The Software Licensing Service determined that the binding data is invalid.</err0xC004F032>
        <err0xC004F033>The Software Licensing Service reported that the product key is not allowed to be installed. Please see the eventlog for details.</err0xC004F033>
        <err0xC004F034>The Software Licensing Service reported that the license could not be found or was invalid.</err0xC004F034>
        <err0xC004F035>The Software Licensing Service reported that the computer could not be activated with a Volume license product key. Volume-licensed systems require upgrading from a qualifying operating system. Please contact your system administrator or use a different type of key.</err0xC004F035>
        <err0xC004F038>The Software Licensing Service reported that the product could not be activated. The count reported by your Key Management Service (KMS) is insufficient. Please contact your system administrator.</err0xC004F038>
        <err0xC004F039>The Software Licensing Service reported that the product could not be activated.  The Key Management Service (KMS) is not enabled.</err0xC004F039>
        <err0x4004F040>The Software Licensing Service reported that the product was activated but the owner should verify the Product Use Rights.</err0x4004F040>
        <err0xC004F041>The Software Licensing Service determined that the Key Management Service (KMS) is not activated. KMS needs to be activated. Please contact system administrator.</err0xC004F041>
        <err0xC004F042>The Software Licensing Service determined that the specified Key Management Service (KMS) cannot be used.</err0xC004F042>
        <err0xC004F047>The Software Licensing Service reported that the proxy policy has not been updated.</err0xC004F047>
        <err0xC004F04D>The Software Licensing Service determined that the Installation ID (IID) or the Confirmation ID (CID) is invalid.</err0xC004F04D>
        <err0xC004F04F>The Software Licensing Service reported that license management information was not found in the licenses.</err0xC004F04F>
        <err0xC004F050>The Software Licensing Service reported that the product key is invalid.</err0xC004F050>
        <err0xC004F051>The Software Licensing Service reported that the product key is blocked.</err0xC004F051>
        <err0xC004F052>The Software Licensing Service reported that the licenses contain duplicated properties. </err0xC004F052>
        <err0xC004F053>The Software Licensing Service determined that the license is invalid. The license contains an override policy that is not configured properly.</err0xC004F053>
        <err0xC004F054>The Software Licensing Service reported that license management information has duplicated data. </err0xC004F054>
        <err0xC004F055>The Software Licensing Service reported that the base SKU is not available.</err0xC004F055>
        <err0xC004F056>The Software Licensing Service reported that the product could not be activated using the Key Management Service (KMS).</err0xC004F056>
        <err0xC004F057>The Software Licensing Service reported that the computer BIOS is missing a required license.</err0xC004F057>
        <err0xC004F058>The Software Licensing Service reported that the computer BIOS is missing a required license.</err0xC004F058>
        <err0xC004F059>The Software Licensing Service reported that a license in the computer BIOS is invalid.</err0xC004F059>
        <err0xC004F060>The Software Licensing Service determined that the version of the license package is invalid.</err0xC004F060>
        <err0xC004F061>The Software Licensing Service determined that this specified product key can only be used for upgrading, not for clean installations.</err0xC004F061>
        <err0xC004F062>The Software Licensing Service reported that a required license could not be found.</err0xC004F062>
        <err0xC004F063>The Software Licensing Service reported that the computer is missing a required OEM license.</err0xC004F063>
        <err0xC004F064>The Software Licensing Service reported that the non-genuine grace period expired.</err0xC004F064>
        <err0xC004F065>The software Licensing Service reported that the application is running within the valid non-genuine period.</err0xC004F065>
        <err0x4004F065>The Software Licensing Service reported that the application is running within the valid non-genuine grace period.</err0x4004F065>
        <err0xC004F066>The Software Licensing Service reported that the genuine information property can not be set before dependent property been set.</err0xC004F066>
        <err0xC004F067>The Software Licensing Service reported that the non-genuine grace period expired (type 2).</err0xC004F067>
        <err0x4004F068>The Software Licensing Service reported that the application is running within the valid non-genuine grace period (type 2).</err0x4004F068>
        <err0xC004F069>The Software Licensing Service reported that the product SKU is not found.</err0xC004F069>
        <err0xC004F06A>The Software Licensing Service reported that the requested operation is not allowed.</err0xC004F06A>
        <err0xC004F06B>The Software Licensing Service determined that it is running in a virtual machine. The Key Management Service (KMS) is not supported in this mode.</err0xC004F06B>
        <err0xC004F06C>The Software Licensing Service reported that the product could not be activated. The Key Management Service (KMS) determined that the request timestamp is invalid.</err0xC004F06C>
        <err0xC004F071>The Software Licensing Service reported that the plug-in manifest file is incorrect.</err0xC004F071>
        <err0xC004F072>The Software Licensing Service reported that the license policies for fast query could not be found.</err0xC004F072>
        <err0xC004F073>The Software Licensing Service reported that the license policies for fast query have not been loaded.</err0xC004F073>
        <err0xC004F074>The Software Licensing Service reported that the product could not be activated. No Key Management Service (KMS) could be contacted. Please see the Application Event Log for additional information.</err0xC004F074>
        <err0xC004F075>The Software Licensing Service reported that the operation cannot be completed because the service is stopping.</err0xC004F075>
        <err0xC004F076>The Software Licensing Service reported that the requested plug-in cannot be found.</err0xC004F076>
        <err0xC004F077>The Software Licensing Service determined incompatible version of authentication data.</err0xC004F077>
        <err0xC004F078>The Software Licensing Service reported that the key is mismatched.</err0xC004F078>
        <err0xC004F079>The Software Licensing Service reported that the authentication data is not set.</err0xC004F079>
        <err0xC004F07A>The Software Licensing Service reported that the verification could not be done.</err0xC004F07A>
        <err0xC004F07B>The requested operation is unavailable while the Software Licensing Service is running.</err0xC004F07B>
        <err0xC004F07C>The Software Licensing Service determined that the version of the computer BIOS is invalid.</err0xC004F07C>
        <err0xC004F07D>The Software Licensing Service reported that the product key cannot be used for this type of activation.</err0xC004F07D>
        <err0xC004F07E>The Installation ID (IID) and the Confirmation ID (CID) do not match the product key.</err0xC004F07E>
        <err0xC004F07F>The Installation ID (IID) and the Confirmation ID (CID) are not bound to the current environment.</err0xC004F07F>
        <err0xC004F080>The Software Licensing Service reported that the license is not bound to the current environment.</err0xC004F080>
        <err0xC004F081>The Software Licensing Service reported that the Active Directory Activation Object could not be found or was invalid.</err0xC004F081>
        <err0xC004F082>The Software Licensing Service reported that the name specified for the Active Directory Activation Object is too long.</err0xC004F082>
        <err0xC004F200>The Software Licensing Service reported that current state is not genuine.</err0xC004F200>
        <err0xC004F210>The Software Licensing Service reported that the license edition does match the computer edition.</err0xC004F210>
        <err0xC004F301>The Software Licensing Service reported that the product could not be activated. The token-based activation challenge has expired.</err0xC004F301>
        <err0xC004F302>The Software Licensing Service reported that Silent Activation failed. The Software Licensing Service reported that there are no certificates found in the system that could activate the product without user interaction.</err0xC004F302>
        <err0xC004F303>The Software Licensing Service reported that the certificate chain could not be built or failed validation.</err0xC004F303>
        <err0xC004F304>The Software Licensing Service reported that required license could not be found.</err0xC004F304>
        <err0xC004F305>The Software Licensing Service reported that there are no certificates found in the system that could activate the product.</err0xC004F305>
        <err0xC004F306>The Software Licensing Service reported that this software edition does not support token-based activation.</err0xC004F306>
        <err0xC004F307>The Software Licensing Service reported that the product could not be activated. Activation data is invalid.</err0xC004F307>
        <err0xC004F308>The Software Licensing Service reported that the product could not be activated. Activation data is tampered.</err0xC004F308>
        <err0xC004F309>The Software Licensing Service reported that the product could not be activated. Activation challenge and response do not match.</err0xC004F309>
        <err0xC004F30A>The Software Licensing Service reported that the product could not be activated. The certificate does not match the conditions in the license.</err0xC004F30A>
        <err0xC004F30B>The Software Licensing Service reported that the inserted smartcard could not be used to activate the product.</err0xC004F30B>
        <err0xC004F30C>The Software Licensing Service reported that the token-based activation license content is invalid.</err0xC004F30C>
        <err0xC004F30D>The Software Licensing Service reported that the product could not be activated. The thumbprint is invalid.</err0xC004F30D>
        <err0xC004F30E>The Software Licensing Service reported that the product could not be activated. The thumbprint does not match any certificate.</err0xC004F30E>
        <err0xC004F30F>The Software Licensing Service reported that the product could not be activated. The certificate does not match the criteria specified in the issuance license.</err0xC004F30F>
        <err0xC004F310>The Software Licensing Service reported that the product could not be activated. The certificate does not match the trust point identifier (TPID) specified in the issuance license.</err0xC004F310>
        <err0xC004F311>The Software Licensing Service reported that the product could not be activated. A soft token cannot be used for activation.</err0xC004F311>
        <err0xC004F312>The Software Licensing Service reported that the product could not be activated. The certificate cannot be used because its private key is exportable.</err0xC004F312>
        <err0xC004F313>The Software Licensing Service reported that the CNG encryption library could not be loaded.  The current certificate may not be available on this version of Windows.</err0xC004F313>
        <err0xC004FC03>A networking problem has occurred while activating your copy of Windows.</err0xC004FC03>
        <err0x4004FC04>The Software Licensing Service reported that the application is running within the timebased validity period.</err0x4004FC04>
        <err0x4004FC05>The Software Licensing Service reported that the application has a perpetual grace period.</err0x4004FC05>
        <err0x4004FC06>The Software Licensing Service reported that the application is running within the valid extended grace period.</err0x4004FC06>
        <err0xC004FC07>The Software Licensing Service reported that the validity period expired.</err0xC004FC07>
        <err0xC004FE00>The Software Licensing Service reported that activation is required to recover from tampering of SL Service trusted store.</err0xC004FE00>
        <err0xC004D101>The security processor reported an initialization error.</err0xC004D101>
        <err0x8004D102>The security processor reported that the machine time is inconsistent with the trusted time.</err0x8004D102>
        <err0xC004D103>The security processor reported that an error has occurred.</err0xC004D103>
        <err0xC004D104>The security processor reported that invalid data was used.</err0xC004D104>
        <err0xC004D105>The security processor reported that the value already exists.</err0xC004D105>
        <err0xC004D107>The security processor reported that an insufficient buffer was used.</err0xC004D107>
        <err0xC004D108>The security processor reported that invalid data was used.</err0xC004D108>
        <err0xC004D109>The security processor reported that an invalid call was made.</err0xC004D109>
        <err0xC004D10A>The security processor reported a version mismatch error.</err0xC004D10A>
        <err0x8004D10B>The security processor cannot operate while a debugger is attached.</err0x8004D10B>
        <err0xC004D301>The security processor reported that the trusted data store was tampered.</err0xC004D301>
        <err0xC004D302>The security processor reported that the trusted data store was rearmed.</err0xC004D302>
        <err0xC004D303>The security processor reported that the trusted store has been recreated.</err0xC004D303>
        <err0xC004D304>The security processor reported that entry key was not found in the trusted data store.</err0xC004D304>
        <err0xC004D305>The security processor reported that the entry key already exists in the trusted data store.</err0xC004D305>
        <err0xC004D306>The security processor reported that the entry key is too big to fit in the trusted data store.</err0xC004D306>
        <err0xC004D307>The security processor reported that the maximum allowed number of re-arms has been exceeded.  You must re-install the OS before trying to re-arm again.</err0xC004D307>
        <err0xC004D308>The security processor has reported that entry data size is too big to fit in the trusted data store.</err0xC004D308>
        <err0xC004D309>The security processor has reported that the machine has gone out of hardware tolerance.</err0xC004D309>
        <err0xC004D30A>The security processor has reported that the secure timer already exists.</err0xC004D30A>
        <err0xC004D30B>The security processor has reported that the secure timer was not found.</err0xC004D30B>
        <err0xC004D30C>The security processor has reported that the secure timer has expired.</err0xC004D30C>
        <err0xC004D30D>The security processor has reported that the secure timer name is too long.</err0xC004D30D>
        <err0xC004D30E>The security processor reported that the trusted data store is full.</err0xC004D30E>
        <err0xC004D401>The security processor reported a system file mismatch error.</err0xC004D401>
        <err0xC004D402>The security processor reported a system file mismatch error.</err0xC004D402>
        <err0xC004D501>The security processor reported an error with the kernel data.</err0xC004D501>
        <!-- Not In SLERROR.XML -->
        <err0x4004F401>The Software Licensing Service reported that the application has a store license.</err0x4004F401>
        <err0x80070002>The system cannot find the file specified.</err0x80070002>
        <err0x5>Access denied: the requested action requires elevated privileges.</err0x5>
        <err0x00000005>Access denied: the requested action requires elevated privileges.</err0x00000005>
        <err0x80070005>Access denied: the requested action requires elevated privileges.</err0x80070005>
        <err0x80070057>The parameter is incorrect.</err0x80070057>
        <err0x8007232A>DNS server failure.</err0x8007232A>
        <err0x8007232B>DNS name does not exist.</err0x8007232B>
        <err0x8007232C>DNS request not supported by name server.</err0x8007232C>
        <err0x800706BA>The RPC server is unavailable.</err0x800706BA>
        <err0x8007251D>No records found for DNS query.</err0x8007251D>
        <err0x8007000D>The data is invalid.</err0x8007000D>
        <err0x8007007B>The filename, directory name, or volume label syntax is incorrect.</err0x8007007B>
        <err0x80070008>Not enough storage is available to process this command.</err0x80070008>
        <err0x800705B4>The operation returned because the timeout period expired.</err0x800705B4>
        <err0x8007000E>Not enough storage is available to complete this operation.</err0x8007000E>
        <err0x80072EE2>The operation timed out.</err0x80072EE2>
        <err0x80072EFD>A connection with the server could not be established.</err0x80072EFD>
        <err0x80072EE7>The server name or address could not be resolved.</err0x80072EE7>
        <err0x80072EEF>The login request was denied</err0x80072EEF>
        <err0x80072EFE>The connection with the server was terminated abnormally.</err0x80072EFE>
        <err0x80072F78>The server returned an invalid or unrecognized response.</err0x80072F78>
        </Strings>";
        #endregion
        /// <summary>
        /// Get an error description for an error code given by the Software Licensing Service
        /// </summary>
        /// <param name="errorCode">The error code to lookup, in hexidecimal format</param>
        /// <returns>An error description for the parameter errorCode</returns>
        public static string GetErrorDescription(string errorCode)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(Slerror)))
                {
                    reader.ReadToFollowing("err" + errorCode);
                    return reader.ReadElementContentAsString();
                }
            }
            catch (Exception)
            {
                return "Unable to Lookup Error";
            }
        }
    }

    /// <summary>
    /// Represent a license queried from the Software Licensing Service
    /// </summary>
    public class LicenseInstance
    {
        #region License Attributes Variables
        public string AppID;
        public string CMID;
        public string ExtendedPID;
        public string EvaluationEndDate;
        public string KMSServerExtendedPID;
        public string LicenseCode;
        public string LicenseDescription;
        public double LicenseDuration;
        public string LicenseErrorText;
        public string LicenseFamily;
        public string LicenseName;
        public string LicenseStatus;
        public string OfflineInstallationID;
        public string PartialProductKey;
        public string SKUID;
        #endregion
        /// <summary>
        /// Store the time we created this object so we can show decreasing grace period without a second query
        /// </summary>
        private readonly DateTime _creationTime;

        /// <summary>
        /// Initialize the creation time upon creation of a new LicenseInstance object
        /// </summary>
        public LicenseInstance()
        {
            _creationTime = DateTime.Now;
        }

        /// <summary>
        /// Property to calculate the remaining days, without having to requery WMI
        /// </summary>
        public int RemainingGraceDays
        {
            get
            {
                return RemainingGraceMinutes / 1440;
            }
        }

        /// <summary>
        /// Property to calculate the remaining minutes, without having to requery WMI
        /// </summary>
        public int RemainingGraceMinutes
        {
            get
            {
                TimeSpan timeSinceCreation = DateTime.Now - _creationTime;
                int minutes = Convert.ToInt32(LicenseDuration - timeSinceCreation.Minutes);
                if (minutes < 0)
                {
                    minutes = 0;
                }
                return minutes;
            }
        }

        /// <summary>
        /// Print all License attributes, except for CMID
        /// </summary>
        /// <returns>A column format including all attributes except for CMID</returns>
        public override string ToString()
        {
            using (StringWriter output = new StringWriter())
            {
                if (LicenseStatus != "Unlicensed")
                {
                    output.WriteLine("Name: " + LicenseName);
                    output.WriteLine("Description: " + LicenseDescription);
                    if (!String.IsNullOrWhiteSpace(LicenseFamily))
                    {
                        output.WriteLine("Family: " + LicenseFamily);
                    }
                    output.WriteLine("SKU ID: " + SKUID);
                    output.WriteLine("Application ID: " + AppID);
                    output.WriteLine("Installed Product Key ID: " + ExtendedPID);
                    if (!String.IsNullOrWhiteSpace(KMSServerExtendedPID))
                    {
                        output.WriteLine("KMS Server Product Key ID: " + KMSServerExtendedPID);
                    }
                    output.WriteLine("OID: " + OfflineInstallationID);
                    output.WriteLine("Last 5 characters of installed product key: " + PartialProductKey);
                    if (!String.IsNullOrWhiteSpace(EvaluationEndDate) && EvaluationEndDate != "1/1/1601")
                    {
                        output.WriteLine("License Expiry: " + EvaluationEndDate);
                    }
                    output.WriteLine("License Status: " + LicenseStatus);
                    output.WriteLine("Error Code: " + LicenseCode);
                    if (!String.IsNullOrWhiteSpace(LicenseErrorText))
                    {
                        output.WriteLine("Error Text: " + LicenseErrorText);
                    }
                }
                else
                {
                    output.WriteLine("Name: " + LicenseName);
                    output.WriteLine("Description: " + LicenseDescription);
                    if (!String.IsNullOrWhiteSpace(LicenseFamily))
                    {
                        output.WriteLine("Family: " + LicenseFamily);
                    }
                    output.WriteLine("SKU ID: " + SKUID);
                    output.WriteLine("License Status: " + LicenseStatus);
                    output.WriteLine("Error Code: " + LicenseCode);
                }

                // Don't show grace period if in Notifications mode or permanently activated.
                if (LicenseStatus != "Notifications Mode" && LicenseStatus != "Unlicensed")
                {
                    if ((LicenseStatus == "Activated" && RemainingGraceMinutes == 0) == false)
                    {
                        output.WriteLine("Remaining Grace: " + RemainingGraceDays + " days  " + "(" + RemainingGraceMinutes + " minute(s) before expiring)");
                    }
                }
                return output.ToString().Trim();
            }
        }
    }

    /// <summary>
    /// A List of LicenseInstance, with methods to filter Licenses based on license state or category
    /// </summary>
    public class LicenseList
    {
        /// <summary>
        /// A List that stores all LicenseInstances
        /// </summary>
        public List<LicenseInstance> List = new List<LicenseInstance>();

        /// <summary>
        /// Parameter-less Constructor to allow LicenseListOffice and Windows to choose the parameters based on checks
        /// </summary>
        protected LicenseList()
        {
        }

        /// <summary>
        /// Create a list of LicenseInstance to store all license data
        /// </summary>
        /// <param name="wmiInfo">WMI Provider and associated data to get all license instances</param>
        /// <param name="wmiProduct">WMI Name of the Software Licensing Service Product Provider</param>
        /// <param name="wmiService">WMI Name of the Software Licensing Service Main Provider</param>
        /// <returns>A List of all LicenseInstance</returns>
        protected List<LicenseInstance> EnumerateLicenses(string wmiInfo, string wmiProduct, string wmiService)
        {
            List<LicenseInstance> licenses = new List<LicenseInstance>();

            try
            {
                // Get All Licenses
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", wmiInfo))
                {
                    foreach (ManagementBaseObject queryObj in searcher.Get())
                    {
                        LicenseInstance newLicense = new LicenseInstance();

                        // Get License Status
                        int licenseState = Convert.ToInt32(queryObj["LicenseStatus"]);
                        string licStatus;
                        switch (licenseState)
                        {
                            case 0:
                                licStatus = "Unlicensed";
                                break;
                            case 1:
                                licStatus = "Activated";
                                break;
                            case 2:
                                licStatus = "Grace Period";
                                break;
                            case 3:
                                licStatus = "Out-of-Tolerance Grace Period";
                                break;
                            case 4:
                                licStatus = "Non-Genuine Grace Period";
                                break;
                            case 5:
                                licStatus = "Notifications Mode";
                                break;
                            case 6:
                                licStatus = "Extended Grace Period";
                                break;
                            default:
                                licStatus = "Unknown";
                                break;
                        }

                        // Get License Error Code
                        long licStatusReason = Convert.ToInt64(queryObj["LicenseStatusReason"]);
                        string errorCode;
                        if (licStatusReason == 0)
                        {
                            errorCode = "0 as licensed";
                        }
                        else
                        {
                            errorCode = "0x" + licStatusReason.ToString("X8");
                        }

                        // Get Error Text
                        string errorText = string.Empty;
                        if (errorCode != "0 as licensed")
                        {
                            errorText = LicenseErrorCode.GetErrorDescription(errorCode);
                        }

                        // Create new instance and add to License List
                        try
                        {
                            newLicense.LicenseName = queryObj["Name"].ToString();
                        }
                        catch (Exception)
                        {
                            newLicense.LicenseName = string.Empty;
                        }
                        try
                        {
                            newLicense.LicenseDescription = queryObj["Description"].ToString();
                        }
                        catch (Exception)
                        {
                            newLicense.LicenseDescription = string.Empty;
                        }
                        try
                        {
                            newLicense.LicenseFamily = queryObj["LicenseFamily"].ToString();
                        }
                        catch (Exception)
                        {
                            newLicense.LicenseFamily = string.Empty;
                        }

                        // Get CMID
                        try
                        {
                            if (!String.IsNullOrWhiteSpace(queryObj["ClientMachineID"].ToString()))
                            {
                                newLicense.CMID = queryObj["ClientMachineID"].ToString();
                            }
                            else
                            {
                                newLicense.CMID = string.Empty;
                            }
                        }
                        catch (Exception)
                        {
                            try
                            {
                                // Get Global CMID
                                using (ManagementObjectSearcher searcher2 = new ManagementObjectSearcher(@"root\CIMV2", "SELECT ClientMachineID FROM " + wmiService))
                                {
                                    foreach (ManagementBaseObject queryObj2 in searcher2.Get())
                                    {
                                        newLicense.CMID = queryObj2["ClientMachineID"].ToString();
                                        break;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                newLicense.CMID = string.Empty;
                            }
                        }

                        newLicense.SKUID = queryObj["ID"].ToString();
                        newLicense.AppID = queryObj["ApplicationID"].ToString();
                        if (licenseState != 0)
                        {
                            newLicense.ExtendedPID = queryObj["ProductKeyID"].ToString();
                            newLicense.OfflineInstallationID = queryObj["OfflineInstallationId"].ToString();
                            newLicense.PartialProductKey = queryObj["PartialProductKey"].ToString();
                            newLicense.LicenseDuration = Convert.ToDouble(queryObj["GracePeriodRemaining"]);
                            try
                            {
                                newLicense.EvaluationEndDate = queryObj["EvaluationEndDate"].ToString();
                            }
                            catch (Exception)
                            {
                                newLicense.EvaluationEndDate = "1/1/1601";
                            }
                            
                        }
                        else
                        {
                            newLicense.ExtendedPID = string.Empty;
                            newLicense.OfflineInstallationID = string.Empty;
                            newLicense.PartialProductKey = string.Empty;
                            newLicense.LicenseDuration = 0;
                        }
                        newLicense.LicenseStatus = licStatus;
                        newLicense.LicenseCode = errorCode;
                        newLicense.LicenseErrorText = errorText;

                        // Get KMS PID
                        try
                        {
                            // Special Handling of KMS PID due to Windows Vista
                            if (this is LicenseListOffice || OSVersion.GetWindowsNumber() > 6.0)
                            {
                                // Get KMS PID for this License
                                using (ManagementObjectSearcher searcher2 = new ManagementObjectSearcher(@"root\CIMV2", "SELECT KeyManagementServiceProductKeyID FROM " + wmiProduct + " WHERE ID = '" + newLicense.SKUID + "'"))
                                {
                                    foreach (ManagementBaseObject queryObj2 in searcher2.Get())
                                    {
                                        newLicense.KMSServerExtendedPID = queryObj2["KeyManagementServiceProductKeyID"].ToString();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // Get Global KMS PID
                                using (ManagementObjectSearcher searcher2 = new ManagementObjectSearcher(@"root\CIMV2", "SELECT KeyManagementServiceProductKeyID FROM " + wmiService))
                                {
                                    foreach (ManagementBaseObject queryObj2 in searcher2.Get())
                                    {
                                        newLicense.KMSServerExtendedPID = queryObj2["KeyManagementServiceProductKeyID"].ToString();
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            newLicense.KMSServerExtendedPID = string.Empty;
                        }

                        licenses.Add(newLicense);
                    }
                }
                return licenses;
            }
            catch (COMException ex)
            {
                // Try to lookup Error
                string errorCode = "0x" + ex.ErrorCode.ToString("X8");
                string errorDescription = LicenseErrorCode.GetErrorDescription(errorCode);
                if (errorDescription == "Unable to Lookup Error")
                {
                    throw;
                }
                throw new Exception(errorDescription);
            }
        }

        /// <summary>
        /// Get a List object containing all Licenses
        /// </summary>
        /// <returns>A List of LicenseInstance that has not been filtered</returns>
        public List<LicenseInstance> GetListFull()
        {
            return List;
        }

        /// <summary>
        /// Get a List of LicenseInstance and return a list of all Unlicensed licenses in that list
        /// </summary>
        /// <returns>A List of LicenseInstance where the License State is Unlicensed</returns>
        public List<LicenseInstance> GetListUnlicensed()
        {
            List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
            foreach (LicenseInstance licenseInstance in List)
            {
                if (licenseInstance.LicenseStatus == "Unlicensed")
                {
                    licenseFilter.Add(licenseInstance);
                }
            }
            return licenseFilter;
        }

        /// <summary>
        /// Get a List of LicenseInstance and return a list of all Licensed licenses in that list
        /// </summary>
        /// <returns>A List of LicenseInstance where the License State is not Unlicensed</returns>
        public List<LicenseInstance> GetListLicensed()
        {
            List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
            foreach (LicenseInstance licenseInstance in List)
            {
                if (licenseInstance.LicenseStatus != "Unlicensed")
                {
                    licenseFilter.Add(licenseInstance);
                }
            }
            return licenseFilter;
        }

        /// <summary>
        /// Get a List of LicenseInstance and return a list of all Unactivated licenses in that list
        /// </summary>
        /// <returns>A List of LicenseInstance where the License State is not Activated</returns>
        public List<LicenseInstance> GetListUnactivated()
        {
            List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
            foreach (LicenseInstance licenseInstance in List)
            {
                if (licenseInstance.LicenseStatus != "Activated")
                {
                    licenseFilter.Add(licenseInstance);
                }
            }
            return licenseFilter;
        }

        /// <summary>
        /// Get a List of LicenseInstance and return a list of all Activated licenses in that list
        /// </summary>
        /// <returns>A List of LicenseInstance where the License State is Activated</returns>
        public List<LicenseInstance> GetListActivated()
        {
            List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
            foreach (LicenseInstance licenseInstance in List)
            {
                if (licenseInstance.LicenseStatus == "Activated")
                {
                    licenseFilter.Add(licenseInstance);
                }
            }
            return licenseFilter;
        }

        /// <summary>
        /// Get a List of LicenseInstance and return a list of all Permanently Activated licenses in that list
        /// </summary>
        /// <returns>A List of LicenseInstance where the License State is Activated and the Activation never expires</returns>
        public List<LicenseInstance> GetListPermanentlyActivated()
        {
            List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
            foreach (LicenseInstance licenseInstance in GetListActivated())
            {
                if (Math.Abs(licenseInstance.LicenseDuration - 0) < Double.Epsilon)
                {
                    licenseFilter.Add(licenseInstance);
                }
            }
            return licenseFilter;
        }

        /// <summary>
        /// Get a List of LicenseInstance and return a list of all licenses in that list that are capable of KMS activation
        /// </summary>
        /// <returns>A List of LicenseInstance where the License Description indicates that the license is KMS capable</returns>
        public List<LicenseInstance> GetListKMS()
        {
            List<LicenseInstance> kmsOnly = new List<LicenseInstance>();

            foreach (LicenseInstance licenseInstance in List)
            {
                if (licenseInstance.LicenseDescription.ToUpper().Contains("VOLUME_KMSCLIENT"))
                {
                    kmsOnly.Add(licenseInstance);
                }
            }
            return kmsOnly;
        }

        /// <summary>
        /// Get Client Machine ID from Licenses
        /// </summary>
        /// <returns>String Representation of CMID if found, "Not Found" if there is no CMID.</returns>
        public string GetCMID()
        {
            foreach (LicenseInstance licenseInstance in List)
            {
                if (!String.IsNullOrWhiteSpace(licenseInstance.CMID))
                {
                    return licenseInstance.CMID;
                }
            }
            return "Not Found";
        }

        /// <summary>
        /// Clear LicenseList
        /// </summary>
        private void Clear()
        {
            List = new List<LicenseInstance>();
        }

        /// <summary>
        /// Clear LicenseList and Refresh Contents
        /// </summary>
        public void Refresh()
        {
            Clear();
            if (this is LicenseListOffice)
            {
                List = new LicenseListOffice().GetListFull();
            }
            else if (this is LicenseListWindows)
            {
                List = new LicenseListWindows().GetListFull();
            }
        }
    }

    /// <summary>
    /// A LicenseList class for obtaining Microsoft Office license information
    /// </summary>
    public class LicenseListOffice : LicenseList
    {
        /// <summary>
        /// Call base constructor with the needed WMI provider information for Office
        /// </summary>
        public LicenseListOffice()
        {
            // Query Windows for Microsoft Office licensing if it is using Windows Licensing Services
            if (OfficeVersion.IsOfficeSPP())
            {
                // Get All Licenses For Windows and Office
                List = EnumerateLicenses("SELECT LicenseStatus, LicenseStatusReason, Name, Description, LicenseFamily, ID, ApplicationID, ProductKeyID, OfflineInstallationId, PartialProductKey, GracePeriodRemaining FROM SoftwareLicensingProduct", "SoftwareLicensingProduct", "SoftwareLicensingService");

                // Remove Licenses that don't have "Office 15" ApplicationID
                List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
                foreach (LicenseInstance licenseInstance in List)
                {
                    if (licenseInstance.AppID == "0ff1ce15-a989-479d-af46-f275c6370663")
                    {
                        licenseFilter.Add(licenseInstance);
                    }
                }
                List = licenseFilter;
                return;
            }
            List = EnumerateLicenses("SELECT LicenseStatus, LicenseStatusReason, Name, Description, LicenseFamily, ID, ApplicationID, ProductKeyID, OfflineInstallationId, PartialProductKey, GracePeriodRemaining FROM OfficeSoftwareProtectionProduct", "OfficeSoftwareProtectionProduct", "OfficeSoftwareProtectionService");
        }
    }

    /// <summary>
    /// A LicenseList class for obtaining Microsoft Windows license information
    /// </summary>
    public class LicenseListWindows : LicenseList
    {
        /// <summary>
        /// Call base constructor with the needed WMI provider information for Windows
        /// </summary>
        public LicenseListWindows()
        {
            // Query Windows for Microsoft Office licensing (and filter it out) if it is using Windows Licensing Services
            if (OfficeVersion.IsOfficeSPP())
            {
                // Get All Licenses For Windows and Office
                List = EnumerateLicenses("SELECT LicenseStatus, LicenseStatusReason, Name, Description, LicenseFamily, ID, ApplicationID, ProductKeyID, OfflineInstallationId, PartialProductKey, GracePeriodRemaining FROM SoftwareLicensingProduct", "SoftwareLicensingProduct", "SoftwareLicensingService");

                // Remove Licenses that have Office 15 ApplicationID
                List<LicenseInstance> licenseFilter = new List<LicenseInstance>();
                foreach (LicenseInstance licenseInstance in List)
                {
                    if (licenseInstance.AppID != "0ff1ce15-a989-479d-af46-f275c6370663")
                    {
                        licenseFilter.Add(licenseInstance);
                    }
                }
                List = licenseFilter;
                return;
            }
            List = EnumerateLicenses("SELECT LicenseStatus, LicenseStatusReason, Name, Description, LicenseFamily, ID, ApplicationID, ProductKeyID, OfflineInstallationId, PartialProductKey, GracePeriodRemaining FROM SoftwareLicensingProduct", "SoftwareLicensingProduct", "SoftwareLicensingService");
        }
    }
}