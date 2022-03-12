using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Brreg.Dto
{
    internal class BrregPagedResponseDto
    {
        public BrregEmbeddedContentDto _embedded { get; set; }
        public BrregPageDto Page { get; set; }
        public class BrregEmbeddedContentDto
        {
            public List<BrregEnhetDto> Enheter { get; set; }
        }

        public class BrregPageDto
        {
            public int Size { get; set; }
            public int TotalElements { get; set; }
            public int TotalPages { get; set; }
            public int Number { get; set; }
        }
    }
}
