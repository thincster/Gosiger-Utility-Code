namespace Okuma_Monitor_Tools.Okuma.OkumaEvents
{

    public class OkumaIOChangedEvent
    {
        public readonly OkumaIOPoint IOPoint;
        public readonly bool NewStatus;
        public OkumaIOChangedEvent(OkumaIOPoint Point, bool Status)
        {
            this.IOPoint = Point;
            this.NewStatus = Status;
        }
    }
}
