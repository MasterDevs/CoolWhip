using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;

namespace MasterDevs.CoolWhip
{
    public class Whippet : Task
    {
        [Required]
        public string Version { get; set; }

        [Required]
        public string TempAssemblyFile { get; set; }

        public override bool Execute()
        {
            File.WriteAllText(TempAssemblyFile, GetAssemblyVersion());
            
            return true;
        }           

        private string GetAssemblyVersion()
        {
            return string.Format(@"[assembly: AssemblyVersion(""{0}"")]", Version ?? "0.0.0.0");
        }   
    }
}