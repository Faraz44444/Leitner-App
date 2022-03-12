using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Tools.Dto
{
    public class PagedToolsProductDto
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public long TransactionId { get; set; }

        public List<ToolsProductListDto> Items { get; set; }
    }

    public class ToolsProductListDto
    {
        public ToolsProductDto NewItem { get; set; }
    }

    public class ToolsProductDto
    {
        public string Ext_Product_Id { get; set; }
        public string Ext_Category_Id { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string Minorder_Qty { get; set; }
        public List<ToolsProductDescriptionDto> Description { get; set; }
        public List<ToolsProductAttachmentDto> Attachments { get; set; }
    }

    public class ToolsProductAttachmentDto
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Type { get; set; }
        public List<ToolsProductDescriptionDto> Descriptions { get; set; }
    }

    public class ToolsProductDescriptionDto
    {
        public string Description { get; set; }
        public string LanguageCode { get; set; }
    }

}
