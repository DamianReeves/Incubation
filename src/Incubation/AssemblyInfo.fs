namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Incubation")>]
[<assembly: AssemblyProductAttribute("Incubation")>]
[<assembly: AssemblyDescriptionAttribute("A sandbox of ideas.")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
