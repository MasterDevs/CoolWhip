using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using System;
using System.Text.RegularExpressions;

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
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }

        private static string _LastGithubVersion;

        public void RefreshVersion()
        {
            if (!string.IsNullOrEmpty(Version)) return;

            if (!string.IsNullOrEmpty(_LastGithubVersion))
            {
                Version = _LastGithubVersion;
                return;
            }

            Version = _LastGithubVersion = Git.GetLatestReleaseFromGithub(Owner, Repo);

            //-- If we can't get a version from Git (working offline) get the version from the file
            if (string.IsNullOrEmpty(Version))
            {
                Version = TryGetVersionFromFile(TempAssemblyFile) ?? "0.0.0.0";
            }
        }

        private static string TryGetVersionFromFile(string fileName)
        {
            try
            {
                var fileBody = File.ReadAllText(fileName);

                if (!string.IsNullOrEmpty(fileBody))
                {
                    var regex = new Regex(@"\[assembly:\s+AssemblyVersion\(""(\d+.\d+.\d+(.[\d+])?)""\)]", RegexOptions.Compiled | RegexOptions.Multiline);

                    var match = regex.Match(fileBody);

                    if (match.Success)
                        return match.Groups[1].Value;
                }
            }
            catch { }

            return null;
        }
    }
}