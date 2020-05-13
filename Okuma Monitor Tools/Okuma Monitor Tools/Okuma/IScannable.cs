namespace Okuma_Monitor_Tools.Okuma
{
    public interface IScannable
    {
        void Update();
        System.DateTime LastScan { get; set; }
        ScanPriority Priority { get; set; }
    }

    public enum ScanPriority
    {
        Normal = 1,
        High = 2
    }
}
