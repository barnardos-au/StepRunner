using System.Collections.Generic;
using CommandLine;

namespace NUnitSelfRunner
{
    internal class Options
    {
        [Option('s', "settings", Required = false, HelpText = "Settings")]
        public IEnumerable<string> SettingArgs { get; set; }

        [Option("teamcity",
            Default = false,
            HelpText = "Use TeamCity event listener")]
        public bool TeamCity { get; set; }

        public Dictionary<string, object> GetSettings()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var arg in SettingArgs)
            {
                var parts = arg.Split('=');
                var left = parts[0];
                var right = parts.Length > 1 ? parts[1] : string.Empty;
                if (int.TryParse(right, out var num))
                {
                    dictionary[left] = num;
                }
                else
                {
                    dictionary[left] = right;
                }
            }

            return dictionary;
        }
    }
}