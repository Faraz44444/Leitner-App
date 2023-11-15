namespace Web.Dto.Report.Dashboard
{
    public class DashboardsSumDto
    {
        public MonthlySumsDto Incomes { get; set; }
        public MonthlySumsDto Expenditures { get; set; }
        public MonthlySumsDto Savings => 
            new(Incomes.ThisMonth - Expenditures.ThisMonth, Incomes.LastMonth - Expenditures.LastMonth);

        public DashboardsSumDto(MonthlySumsDto incomes, MonthlySumsDto expenditures)
        {
            Incomes = incomes;
            Expenditures = expenditures;
        }
    }
}
