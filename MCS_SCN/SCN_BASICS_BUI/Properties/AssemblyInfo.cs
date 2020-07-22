using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("HTN.BITS.MSC.SCN.UIL")]
[assembly: AssemblyDescription("Application Scanner")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Hi-Tech Nittsu (Thailand) Co., Ltd.")]
[assembly: AssemblyProduct("HTN.BITS.MSC.SCN.UIL")]
[assembly: AssemblyCopyright("@2014 Hi-Tech Nittsu. All rights reserved")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("26db80e4-fdb9-4821-9fae-9f59affa93c3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.0.0.0")]

// Below attribute is to suppress FxCop warning "CA2232 : Microsoft.Usage : Add STAThreadAttribute to assembly"
// as Device app does not support STA thread.
//[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2232:MarkWindowsFormsEntryPointsWithStaThread")]
// Version : 1.0.0.2 [09-Mar-2014]: Add Stock Checking Process on Scanner. 
// Version : 1.0.0.3 [18-Dec-2014]: Modified add line_no when scan pick check location
