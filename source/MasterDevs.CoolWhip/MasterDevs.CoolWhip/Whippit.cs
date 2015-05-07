using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.IO;

namespace MasterDevs.CoolWhip
{
    public class Whippit : Task
    {
        private readonly Git _git;

        public Whippit()
        {
            _git = new Git(Log);
        }

        [Required]
        public string TempAssemblyFile { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Owner { get; set; }
        [Required]
        public string Repo { get; set; }

        public bool UseLocal { get; set; }

        public override bool Execute()
        {
            try
            {
                UpdateCorrectVersion();

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

        public void UpdateCorrectVersion()
        {
            if (!string.IsNullOrEmpty(Version)) return;

            if (UseLocal)
            {
                Version = TryGetVersionFromFile(TempAssemblyFile) ?? "0.0.0.0";
            }
            else
            {
                Version = _git.GetLatestReleaseFromGithub(Owner, Repo);
            }
        }

        private static string TryGetVersionFromFile(string fileName)
        {
            try
            {
                var fileBody = File.ReadAllText(fileName);

                if (!string.IsNullOrEmpty(fileBody))
                {
                    var match = RegHelpers.GetAssemblyVersion.Match(fileBody);

                    if (match.Success)
                        return match.Groups[1].Value;
                }
            }
            catch { }

            return null;
        }
    }
}