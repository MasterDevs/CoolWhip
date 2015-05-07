using Octokit;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MasterDevs.CoolWhip
{
    public static class Git
    {
        private static readonly Regex _tagSearch = new Regex(@"(\d+.\d+.\d+(.[\d+])?)", RegexOptions.Compiled);

        /// <summary>
        /// Gets the latest tag name from the specified repo
        /// </summary>
        /// <param name="owner">Username of repo owner</param>
        /// <param name="repo">Name of the repository</param>
        /// <param name="csprojPath">Path to the project file. This method will walk up from here looking for the root git folder</param>
        /// <param name="versionRegex">Optional regex used to pull the version from the tag. If none is specified, (\d.\d.\d) is used.</param>
        /// <returns>null if there's an error or no tag is found, otherwise just the tag is returned</returns>
        public static async Task<string> GetLatestReleaseFromGithubAsync(string owner, string repo, Regex versionRegex = null)
        {
            try
            {
                versionRegex = versionRegex ?? _tagSearch;

                var client = new GitHubClient(new ProductHeaderValue("CoolWhip"));
                var releases = await client.Release.GetAll(owner, repo);

                var latestTag = releases
                    .Select(t => versionRegex.Match(t.TagName))
                    .Where(m => m.Success)
                    .Select(m => new Version(m.Groups[0].Value))
                    .OrderByDescending(t => t)
                    .FirstOrDefault();

                return latestTag?.ToString();
            }
            catch { }

            return null;
        }

        public static string GetLatestReleaseFromGithub(string owner, string repo, Regex versionRegex = null)
        {
            var t = GetLatestReleaseFromGithubAsync(owner, repo, versionRegex);

            t.Wait();

            return t.Result;
        }

    }
}
