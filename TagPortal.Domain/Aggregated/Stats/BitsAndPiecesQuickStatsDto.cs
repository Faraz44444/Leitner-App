using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagPortal.Domain.Aggregated.Stats
{
    public class BitsAndPiecesQuickStatsDto
    {
        public int OrderCountCurrentDate { get; set; }
        public double ConfirmedSumCurrentDate { get; set; }
        public DateTime LastSynchERP { get; set; }
        public IntegrationLastErrorDto LastIntegrationError { get; set; }
    }
    public class IntegrationLastErrorDto
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
