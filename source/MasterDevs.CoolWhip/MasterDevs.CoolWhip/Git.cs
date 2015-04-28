using LibGit2Sharp;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

namespace MasterDevs.CoolWhip
{
    public static class Git
    {
        private static readonly Regex _tagSearch = new Regex(@"(\d.\d.\d)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the latest tag name from the local git repository
        /// </summary>
        /// <param name="csprojPath">Path to the project file. This method will walk up from here looking for the root git folder</param>
        /// <returns>null if there's an error or no tag is found, otherwise just the tag is returned</returns>
        public static string GetLatestTag(string csprojPath)
        {
            if (!File.Exists(csprojPath)) return null;

            var folder = Path.GetDirectoryName(csprojPath);

            if (string.IsNullOrEmpty(folder)) return null;

            var gitRoot = FindGitRoot(new DirectoryInfo(folder));

            if (null == gitRoot) return null;

            return GetLatestReleaseTag(gitRoot);
        }

        private static string FindGitRoot(DirectoryInfo folder)
        {
            if (Directory.Exists(Path.Combine(folder.FullName, @".git")))
                return folder.FullName;

            try
            {
                if (folder.Parent != null)
                {
                    return FindGitRoot(folder.Parent);
                }
            }
            catch (SecurityException)
            {
            }

            return null;
        }

        private static string GetLatestReleaseTag(string repoPath)
        {
            using (var repo = new Repository(repoPath))
            {
                var tag_match = repo.Tags
                    .Select(t => _tagSearch.Match(t.Name))
                    .Where(m => m.Success)
                    .LastOrDefault();

                if (null != tag_match)
                {
                    return tag_match.Groups[0].Value;
                }
            }

            return null;
        }
    }
}
