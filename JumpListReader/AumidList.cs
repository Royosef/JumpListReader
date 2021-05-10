using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;

namespace JumpListReader
{
    public class AumidList : IAumidList
    {
        private Dictionary<string, string> list;

        public string GetAumid(string path)
        {
            ValidatePath(path);

            if (list == null) Refresh();

            return list.GetValueOrDefault(path.ToLower());
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"{nameof(path)} could not be null or whitespace.");
            }
        }

        /*
         * The following function was originally wrote by user1969903  -
         * https://stackoverflow.com/questions/56265062/programmatically-get-list-of-installed-application-executables-windows10-c/57195686#57195686
        */
        public IReadOnlyDictionary<string, string> GetList()
        {
            var FODLERID_AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
            var appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(FODLERID_AppsFolder);
            var apps = new Dictionary<string, string>();

            foreach (var app in (IKnownFolder)appsFolder)
            {
                var appUserModelID = app.ParsingName;
                var appPath = app.Properties.System.Link.TargetParsingPath.Value?.ToLower();
                var packagePath = app.Properties.GetProperty<string>("System.AppUserModel.PackageInstallPath").Value?.ToLower();

                if (appPath != null && !apps.ContainsKey(appPath))
                {
                    apps.Add(appPath, appUserModelID);
                }
                else if (packagePath != null && !apps.ContainsKey(packagePath))
                {
                    apps.Add(packagePath, appUserModelID);
                }
            }

            list = apps;

            return apps;
        }

        public void Refresh()
        {
            GetList();
        }
    }
}
