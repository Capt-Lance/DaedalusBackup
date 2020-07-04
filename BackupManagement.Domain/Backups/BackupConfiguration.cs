namespace BackupManagement.Domain.Backups
{
    public class BackupConfiguration
    {
        public string BackupLocation { get; set; }
        public LocationType TargetLocationType { get; set; }
        // Keeping VirtualMachine in here as a VM has to be passed in everywhere this is used
        // Might revist this later and split it out if it makes sense to when working in BackupJobs
        //public VirtualMachine VirtualMachine { get; set; }
    }
}