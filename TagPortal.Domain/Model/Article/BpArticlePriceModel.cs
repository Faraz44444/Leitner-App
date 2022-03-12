using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagPortal.Domain.Model.Article
{
    public class BpArticlePriceModel : PagedModel
    {
        public long ArticleId { get; set; }
        public string ArticleNo { get; set; }
        public decimal Price { get; set; }
        public long ClientId { get; set; }
        public bool IsDownloaded { get; set; }
        public DateTime? IsDownloadedAt { get; set; }
    }
}
