using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;

namespace MasterDevs.CoolWhip
{
    public class Whippet : Task
    {
        [Required]
        public string TempAssemblyFile { get; set; }

        [Required]
        public string Version { get; set; }

        public override bool Execute()
        {
            try
            {
                var assemblyFileContent = string.Format(@"[assembly: System.Reflection.AssemblyVersion(""{0}"")]", Version ?? "0.0.0.0");
                File.WriteAllText(TempAssemblyFile, assemblyFileContent);

                return true;
            }
            catch (System.Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }
    }
}