using JumpList.Automatic;
using JumpList.Custom;
using System.Collections.Generic;

namespace JumpListReader
{
    public class JumpList
    {
        public string ExePath { get; set; }
        public string AppUserModelId { get; set; }
        public IEnumerable<string> AppIds { get; set; }
        public IEnumerable<AutomaticDestination> AutomaticDestinations { get; set; }
        public IEnumerable<CustomDestination> CustomDestinations { get; set; }
    }
}
