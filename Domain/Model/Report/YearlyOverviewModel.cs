using Domain.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Report
{
    public class YearlyOverviewModel : PagedModel
    {
        public double Price { get; set; }
        public bool IsDeposit { get; set; }
        public string FormattedPrice
        {
            get
            {
                return String.Format("{0:N}", Price);
            }
        }
        public Int32 Year { get; set; }
        public Int32 Month { get; set; }
    }
}
