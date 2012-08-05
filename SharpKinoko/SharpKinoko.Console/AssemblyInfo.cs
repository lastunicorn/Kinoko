using System.Reflection;
using System.Runtime.CompilerServices;
using CommandLine.Text;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("SharpKinoko Console")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dust in the Wind")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Copyright (C) 2012 Dust in the Wind")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyInformationalVersionAttribute("1.0")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: InternalsVisibleTo("SharpKinoko.Tests")]


// from CommandLineParser.Text
[assembly: AssemblyLicense(
  "This is free software. You may redistribute copies of it under the terms of",
  "the MIT License <http://www.opensource.org/licenses/mit-license.php>.")]
[assembly: AssemblyUsage("Usage: SharpKinokoConsole -assembly <assemblyName>")]