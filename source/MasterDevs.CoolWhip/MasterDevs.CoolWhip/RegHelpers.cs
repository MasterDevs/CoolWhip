using System.Text.RegularExpressions;

namespace MasterDevs.CoolWhip
{
    public static class RegHelpers
    {
        private const string _Version = @"(\d+.\d+.\d+(.[\d+])?)";

        public static Regex GetAssemblyVersion { get; } = 
            new Regex($@"\[assembly:\s+AssemblyVersion\(""{_Version}""\)]", RegexOptions.Compiled);

        public static Regex TagSearch { get; } = new Regex(_Version, RegexOptions.Compiled);


    }
}
