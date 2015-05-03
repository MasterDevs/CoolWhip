using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;

namespace MasterDevs.CoolWhip
{
    public class Whippit : Task
    {
        [Required]
        public string TempAssemblyFile { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Owner { get; set; }
        [Required]
        public string Repo { get; set; }

        public override bool Execute()
        {
            try
            {
                RefreshVersion();

                var assemblyBody = string.Format(@"
[assembly: System.Reflection.AssemblyVersion(""{0}"")]
[assembly: System.Reflection.AssemblyFileVersion(""{0}"")]", Version ?? "0.0.0.0");

                File.WriteAllText(TempAssemblyFile, assemblyBody);

                return true;
            }
            catch (System.Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }

        private static string _LastGithubVersion;
        private void RefreshVersion()
        {
            if (!string.IsNullOrEmpty(Version)) return;

            if (!string.IsNullOrEmpty(_LastGithubVersion))
            {
                Version = _LastGithubVersion;
                return;
            }

            Version = _LastGithubVersion = Git.GetLatestReleaseFromGithub(Owner, Repo);

            if (string.IsNullOrEmpty(_LastGithubVersion))
                Version = _LastGithubVersion = "0.0.0.0";
        }
    }
}