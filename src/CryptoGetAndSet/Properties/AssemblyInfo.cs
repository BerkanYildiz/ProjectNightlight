using System.Reflection;
using System.Runtime.InteropServices;

// 
// Metadata.
// 

[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// 
// Obfuscation.
// 

[assembly: Obfuscation(Feature = "merge with BouncyCastle.Crypto.dll", Exclude = false)]
[assembly: Obfuscation(Feature = "merge with TextCopy.dll", Exclude = false)]

[assembly: Obfuscation(Feature = "debug", Exclude = false)]
[assembly: Obfuscation(Feature = "encrypt resources [compress]", Exclude = false)]
[assembly: Obfuscation(Feature = "code control flow obfuscation", Exclude = false)]

[assembly: Obfuscation(Feature = "string encryption", Exclude = false)]

[assembly: Obfuscation(Feature = "encrypt symbol names with password RWIQCIGNBDGCHBXHREGJWGEGZPTTIDHFGYELJDMQ", Exclude = false)]
[assembly: Obfuscation(Feature = "encrypt serializable symbol names with password 'NDEUFFGZVWGTYDTTUGNXXDEDGNQBJIGJEIVKEPUK'", Exclude = false)]

[assembly: Obfuscation(Feature = "rename serializable symbols", Exclude = false)]
[assembly: Obfuscation(Feature = "rename symbol names with printable characters", Exclude = false)]

[assembly: Obfuscation(Feature = "Apply to type * : apply to member *: renaming", Exclude = false)]