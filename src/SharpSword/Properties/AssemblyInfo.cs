using SharpSword;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(SharpSwordConsts.FwName)]
[assembly: AssemblyDescription(SharpSwordConsts.FwDescription)]
[assembly: AssemblyConfiguration(SharpSwordConsts.Author)]
[assembly: AssemblyCompany(SharpSwordConsts.Company)]
[assembly: AssemblyProduct(SharpSwordConsts.FwName)]
[assembly: AssemblyCopyright(SharpSwordConsts.Copyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ec1ff365-1878-4b7c-a5a6-9fa21e6620c4")]

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
[assembly: AssemblyVersion(SharpSwordConsts.CurrentVersion)]
[assembly: AssemblyFileVersion(SharpSwordConsts.CurrentVersion)]
[assembly: PreApplicationStartMethod(typeof(PreApplicationStartCode), "Start")]
