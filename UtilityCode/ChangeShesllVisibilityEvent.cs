using System.Windows;

namespace UtilityCode
{
    public class ChangeShellVisibilityEvent
    {

        public Visibility Visibility { get; private set; }
        public bool OnTop { get; private set; }
        public bool ShowMain { get; private set; }
        public ChangeShellVisibilityEvent(Visibility visibility, bool OnTop = false, bool ShowMain = false)
        {


            Visibility = visibility;
            this.OnTop = OnTop;
            this.ShowMain = ShowMain;
        }


    }
}
