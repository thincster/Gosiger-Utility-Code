namespace Okuma_Monitor_Tools.Okuma.OkumaEvents
{
    public class VariableChangedEvent
    {
        public readonly double NewValue;
        public readonly double OldValue;
        public readonly int VariableNumber;
        public VariableChangedEvent(int VariableNumber, double NewValue, double OldValue)
        {
            this.NewValue = NewValue;
            this.OldValue = OldValue;
            this.VariableNumber = VariableNumber;
        }
    }
}
