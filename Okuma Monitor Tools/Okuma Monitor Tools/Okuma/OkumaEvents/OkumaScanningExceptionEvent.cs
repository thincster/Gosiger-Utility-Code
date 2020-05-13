using System;

namespace Okuma_Monitor_Tools.Okuma.OkumaEvents
{
    public class OkumaScanningExceptionEvent
    {
        public readonly Exception Ex;
        public readonly string TargetIndex;
        public OkumaScanningExceptionEvent(Exception Exception, string TargetIndex)
        {
            Ex = Exception;
            this.TargetIndex = TargetIndex;
        }
    }
}
