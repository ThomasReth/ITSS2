﻿using System.Reflection;
using System.Runtime.InteropServices;

using Reth.Protocols.Versioning;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle( "Reth.Protocols.Versioning" )]
[assembly: AssemblyDescription( "Version Information" )]
[assembly: AssemblyConfiguration( VersionInfoProvider.Configuration )]
[assembly: AssemblyCompany( VersionInfoProvider.Company )]
[assembly: AssemblyProduct( VersionInfoProvider.Product )]
[assembly: AssemblyCopyright( VersionInfoProvider.Copyright )]
[assembly: AssemblyTrademark( VersionInfoProvider.Trademark )]
[assembly: AssemblyCulture( VersionInfoProvider.Culture )]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid( "44CB7A5C-7B10-4F49-A274-8995A96B1FDC" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion( VersionInfoProvider.AssemblyVersion )]
[assembly: AssemblyFileVersion( VersionInfoProvider.FileVersion )]
[assembly: AssemblyInformationalVersion( VersionInfoProvider.ProductVersion )]
