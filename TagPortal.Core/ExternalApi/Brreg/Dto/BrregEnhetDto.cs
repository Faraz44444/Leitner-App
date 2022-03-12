namespace TagPortal.Core.ExternalApi.Brreg.Dto
{
    internal class BrregEnhetDto
    {
        public string Organisasjonsnummer { get; set; }
        public string Navn { get; set; }
        public BrregAdresseDto PostAdress { get; set; }
        public BrregAdresseDto Forretningsadresse { get; set; }

        public class BrregAdresseDto
        {
            public string Land { get; set; }
            public string Landkode { get; set; }
            public string Postnummer { get; set; }
            public string Poststed { get; set; }
            public string[] Adresse { get; set; }
            public string Kommune { get; set; }
            public string Kommunenummer { get; set; }

            public BrregAdresseDto()
            {
                Adresse = new string[1];
            }
        }
    }
}
