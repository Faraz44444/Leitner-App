namespace TbxPortal.Web.Dto
{
    public class LookupItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class WarehouseLocationLookupItemDto : LookupItemDto
    {
        public long WarehouseId { get; set; }
    }

    public class CountryLookUpItemDto : LookupItemDto
    {
        public int PhoneCountryCode { get; set; }
        public string CountryCode { get; set; }
    }

}