using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Astrup.Dto
{

    public class PagedAstrupProductDto
    {
        public int ItemsPerPage { get; } = 100;
        public int ArticleCount { get; set; }
        public int CurrentPage { get; set; }
        public List<AstrupProductDto> Items { get; set; }
    }

    public class AstrupProductDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Gtin { get; set; }
        public string UnitText { get; set; }
        public bool HasCertificate { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Area { get; set; }
        public decimal Length { get; set; }
        public int? CustomerProdNo { get; set; }
        public decimal UnitWeight { get; set; }
        public AstrupAdditionalUnitsDto AdditionalUnits { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal Stock { get; set; }


    }

    public class AstrupAdditionalUnitsDto
    {
        public decimal? kg { get; set; }
        public decimal? m { get; set; }
        public decimal? pcs { get; set; }
        public decimal? m3 { get; set; }
        public decimal? m2 { get; set; }
        public decimal? Plater { get; set; }
        public decimal? Meter { get; set; }
        public decimal? Stykk { get; set; }
        public decimal? Lengde { get; set; }
        public decimal? Kvadratmeter { get; set; }
        public decimal? Rull { get; set; }
        public decimal? Eske { get; set; }
        public decimal? Boks { get; set; }
        public decimal? Pakker { get; set; }
        public decimal? Par { get; set; }
    }

}
