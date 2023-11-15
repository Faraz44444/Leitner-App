namespace Web.Dto.Report.Dashboard
{
    public class MonthlySumsDto
    {
        public float ThisMonth { get; set; }
        public float LastMonth { get; set; }
        public string ThisMonthFormatted
        {
            get
            {
                return ThisMonth.ToString("## ##0.00");
            }
        }
        public string LastMonthFormatted
        {
            get
            {
                return LastMonth.ToString("## ##0.00");
            }
        }
        public MonthlySumsDto(float thisMonth, float lastMonth)
        {
            ThisMonth = thisMonth;
            LastMonth = lastMonth;
        }
    }
}
