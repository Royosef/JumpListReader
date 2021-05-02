using JumpList.Automatic;
using JumpList.Custom;

namespace JumpListReader
{
    public class JumpList
    {
        public string ExePath { get; set; }
        public string AppUserModelId { get; set; }
        public string AppId { get; set; }
        public AutomaticDestination AutomaticDestination { get; set; }
        public CustomDestination CustomDestination { get; set; }
    }
}
