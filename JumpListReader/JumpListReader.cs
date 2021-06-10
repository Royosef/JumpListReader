using JumpList.Automatic;
using JumpList.Custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JumpListReader
{
    public class JumpListReader
    {
        public IAumidList AumidList { get; set; } = new AumidList();

        public JumpList ReadForExe(string exePath)
        {
            ValidatePath(exePath);

            var jumpList = new JumpList
            {
                ExePath = exePath,
                AppUserModelId = AumidList.GetAumid(exePath)
            };

            if (jumpList.AppUserModelId == null) return null;

            jumpList.AppIds = new List<string> { 
                AppIdCalculator.Calculate(jumpList.AppUserModelId),
                AppIdCalculator.Calculate(Path.GetFileNameWithoutExtension(exePath))
            };

            jumpList.AutomaticDestinations = jumpList.AppIds.Select(GetAutomaticDestinations).Where(x => x != null);
            jumpList.CustomDestinations = jumpList.AppIds.Select(GetCustomDestinations).Where(x => x != null);

            return jumpList;
        }

        private void ValidatePath(string exePath)
        {
            if (string.IsNullOrWhiteSpace(exePath))
            {
                throw new ArgumentException($"{nameof(exePath)} could not be null or whitespace.");
            }
        }

        private AutomaticDestination GetAutomaticDestinations(string appId)
        {
            try
            {
                var automaticListFile = GetAutomaticDestinationsFile(appId);

                if (automaticListFile != null)
                {
                    return global::JumpList.JumpList.LoadAutoJumplist(automaticListFile);
                }
            }
            catch { }

            return null;
        }

        private string GetAutomaticDestinationsFile(string appId)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var automaticDestinationsFolder = Path.Combine(appDataPath, @"Microsoft\Windows\Recent\AutomaticDestinations");
            var automaticDestinationsFile = Path.Combine(automaticDestinationsFolder, $"{appId}.automaticDestinations-ms");

            return File.Exists(automaticDestinationsFile) ? automaticDestinationsFile : null;
        }

        private CustomDestination GetCustomDestinations(string appId)
        {
            try
            {
                var customListFile = GetCustomDestinationsFile(appId);

                if (customListFile != null)
                {
                    return global::JumpList.JumpList.LoadCustomJumplist(customListFile);
                }
            }
            catch { }

            return null;
        }

        private string GetCustomDestinationsFile(string appId)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var customDestinationsFolder = Path.Combine(appDataPath, @"Microsoft\Windows\Recent\CustomDestinations");
            var customDestinationsFile = Path.Combine(customDestinationsFolder, $"{appId}.customDestinations-ms");

            return File.Exists(customDestinationsFile) ? customDestinationsFile : null;
        }
    }
}
