using System;
using System.Linq;

namespace MasterDevs.CoolWhip.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var versions = new string[]
            {
                "1.07.2",
                "1.4.3.99",
                "1.5.2",
            };
            foreach (var v in versions.Select(v => new Version(v)).OrderByDescending(o => o))
            {
                Console.WriteLine(v);
            }

            var git = new Git();
            var gitVersion = git.GetLatestReleaseFromGithub("MasterDevs", "CoolWhip");
            Console.WriteLine($"Git Version: {gitVersion}");

            //Git.GetLatestTag(@"C:\github\CoolWhip\source\MasterDevs.CoolWhip\MasterDevs.CoolWhip.Sample\MasterDevs.CoolWhip.Sample.csproj");
            //Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
