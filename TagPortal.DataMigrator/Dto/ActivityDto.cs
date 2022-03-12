namespace TagPortal.DataMigrator.Dto
{
    internal class ActivityDto
    {

        public int ClientId { get; set; } // FK T_CLIENT
        public string Project { get; set; }
        public string SFI { get; set; }
        public string Activity { get; set; }
        public decimal WorkHourSum { get; set; }
        public string Description { get; set; }
    }
}
