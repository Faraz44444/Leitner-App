using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.Destination;
using TagPortal.Domain.Model.PurchaseOrder;
using TagPortal.Domain.Model.Tag;
using TagPortal.Domain.Model.Tag.Model;
using TagPortal.Domain.Model.Unit;

namespace TagPortal.Report.Excel.HandleMultipleTags
{
    public class UpdateMultipleTagsExcelFile : BaseExcelFile
    {
        public MemoryStream GetSheet(PurchaseOrderModel order, List<PurchaseOrderLineModel> orderLines, List<TagModelFieldTypeModel> tagAdditionalFields, 
            List<UnitModel> units, List<DestinationModel> destinations, List<TagModel> tags = null)
        {
            var tagModel = new TagModel();
            var columns = new List<TableColumnDetailsDto>
            {
                new TableColumnDetailsDto(nameof(tagModel.TagNo), "Tag number", disabled: true),
                new TableColumnDetailsDto(nameof(tagModel.Description), "Description", required: true),
                new TableColumnDetailsDto(nameof(tagModel.PurchaseOrderLineNo), "Order Line No", type: ColumnTypeEnum.Dropdown),
                new TableColumnDetailsDto(nameof(tagModel.Quantity), "Quantity", type: ColumnTypeEnum.Decimal, required: true),
                new TableColumnDetailsDto(nameof(tagModel.UnitCode), "Unit", type: ColumnTypeEnum.Dropdown, required: true),
                new TableColumnDetailsDto(nameof(tagModel.PlannedDeliveryDate), "Delivery Date", type: ColumnTypeEnum.Date, required: true),
                new TableColumnDetailsDto(nameof(tagModel.SupplierArticleNo), "Supplier's article No", required: true),
                new TableColumnDetailsDto(nameof(tagModel.DestinationName), "Destination", type: ColumnTypeEnum.Dropdown, required: true),
                new TableColumnDetailsDto(nameof(tagModel.IHMRequired), "Requires IHM", type: ColumnTypeEnum.Boolean, required: false),
                new TableColumnDetailsDto(nameof(tagModel.CertificateRequired), "Requires Certificate", type: ColumnTypeEnum.Boolean, required: false),

                new TableColumnDetailsDto(nameof(tagModel.TimeInterval), "Time Interval", type: ColumnTypeEnum.String, required: false),
                new TableColumnDetailsDto(nameof(tagModel.MaintenanceDescription), "Maintenance Description", type: ColumnTypeEnum.String, required: false),

                new TableColumnDetailsDto(nameof(tagModel.PackagingList), "Packaging List", type: ColumnTypeEnum.String, required: false),
                new TableColumnDetailsDto(nameof(tagModel.PackageNo), "Package No", type: ColumnTypeEnum.String, required: false),
                new TableColumnDetailsDto(nameof(tagModel.TransportDimension), "Transport dimension (HxBxD)", type: ColumnTypeEnum.String, required: false),
            };

            foreach (var field in tagAdditionalFields)
            {
                if (field.ValueType == EnumTagModelFieldType.String)
                    columns.Add(new TableColumnDetailsDto(field.TagModelFieldTypeId.ToString(), field.Name, type: ColumnTypeEnum.String));
                else if (field.ValueType == EnumTagModelFieldType.Decimal)
                    columns.Add(new TableColumnDetailsDto(field.TagModelFieldTypeId.ToString(), field.Name, type: ColumnTypeEnum.Decimal));
                else if (field.ValueType == EnumTagModelFieldType.WholeNumber)
                    columns.Add(new TableColumnDetailsDto(field.TagModelFieldTypeId.ToString(), field.Name, type: ColumnTypeEnum.WholeNumber));
                else if (field.ValueType == EnumTagModelFieldType.Boolean)
                    columns.Add(new TableColumnDetailsDto(field.TagModelFieldTypeId.ToString(), field.Name, type: ColumnTypeEnum.Boolean));
            }



            var dropdowns = new List<TableDropdownItemDto> {
                new TableDropdownItemDto(nameof(tagModel.PurchaseOrderLineNo), "Order Line No", orderLines.Select(x=> x.PurchaseOrderLineNo.ToString()).ToList()),
                new TableDropdownItemDto(nameof(tagModel.UnitCode), "Unit", units.Select(x=> x.UnitCode).ToList()),
                new TableDropdownItemDto(nameof(tagModel.DestinationName), "Destination", destinations.Select(x=> x.Name).ToList())
            };

            var groupedHeaderColumns = new List<TableGroupedHeaderDto> {
                new TableGroupedHeaderDto(0, 8, "General Information"),
                new TableGroupedHeaderDto(9, 11, "Maintenance"),
                new TableGroupedHeaderDto(12, 14, "Transport"),
                new TableGroupedHeaderDto(15, columns.Count - 1, "Additional Data")
            };
            

            // FORMAT THE DATALIST
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

            if (tags != null)
            {
                foreach(var tag in tags)
                {
                    var item = new Dictionary<string, object>();

                    item.Add(nameof(tag.TagNo), tag.TagNo);
                    item.Add(nameof(tag.Description), tag.Description);
                    item.Add(nameof(tag.PurchaseOrderLineNo), tag.PurchaseOrderLineNo);
                    item.Add(nameof(tag.Quantity), tag.Quantity);
                    item.Add(nameof(tag.UnitCode), tag.UnitCode);
                    item.Add(nameof(tag.PlannedDeliveryDate), tag.PlannedDeliveryDate);
                    item.Add(nameof(tag.SupplierArticleNo), tag.SupplierArticleNo);
                    item.Add(nameof(tag.DestinationName), tag.DestinationName);
                    item.Add(nameof(tag.IHMRequired), tag.IHMRequired);
                    item.Add(nameof(tag.CertificateRequired), tag.CertificateRequired);

                    if(tag.AdditionalData != null)
                        foreach (var field in tag.AdditionalData)
                            item.Add(field.Key.ToString(), field.Value);

                    dataList.Add(item);
                }
            }

            // CREATE THE TABLE PREFIXED VALUES
            var documentDetails = new List<(string Name, string Value)>
                {
                    { ("Purchaser:", order.CreatedByFullName) },
                    { ("Supplier:", order.SupplierName) },
                    { ("Project:", "") },
                    { ("Order no:", order.OrderNo) },
                    { ("Export date:", DateTime.Now.ToString("dd.MM.yyyy")) },
                };

            var importantInformation = new List<(string value, SLStyle style)>
                {
                    ("NB! Important information:", CellStyle.Bold),
                    ("Columns marked in gray are read only informational fields that will not be will not be updated if modified.", CellStyle.Locked),
                    ("All dates must be in the format: DD.MM.YYYY", CellStyle.Locked),
                    ("Fields marked with * are mandatory and must be filled in before importing the data to Tagit.", CellStyle.Locked),
                    ("If the tag has status '30 - Confirmed' or higher (check this on Tagit), fields in the 'General information'-section will not be updated.", CellStyle.Locked),
                    ("IHM - Inventory of haszardous materials.", CellStyle.Locked),
                };



            var ms = new MemoryStream();
            using (SLDocument sl = new SLDocument())
            {
                // 1. Create Document Details
                // 2. Create Table (Will Auto format the cell width)
                // 3. Create Important Information (after 2. to not interfere with the auto formatting) 

                int xPos = 1, yPos = 1;

                sl.SetCellValue(yPos, xPos, "EXPORT FROM WWW.TAGIT.NO");
                sl.SetCellStyle(yPos, xPos, CellStyle.Bold);
                sl.MergeWorksheetCells(yPos, xPos, yPos, xPos + 4);
                yPos++;
                
                var cyPos = yPos;
                foreach(var item in documentDetails)
                {
                    sl.SetCellValue(cyPos, xPos, item.Name);
                    sl.SetCellStyle(cyPos, xPos, CellStyle.Bold);
                    sl.SetCellValue(cyPos, xPos + 1, item.Value);
                    sl.SetCellStyle(cyPos, xPos + 1, CellStyle.Locked);

                    cyPos++;
                }
                
                cyPos = yPos;
                yPos += (documentDetails.Count > importantInformation.Count ? documentDetails.Count : importantInformation.Count);

                CreateTable(sl, columns, "Register Tags", dropdowns: dropdowns, groupedHeaderColumns: groupedHeaderColumns,
                    dataList: dataList, xOffset: xPos, yOffset: yPos + 1);


                foreach(var item in importantInformation)
                {
                    sl.SetCellValue(cyPos, xPos + 4, item.value);
                    sl.SetCellStyle(cyPos, xPos + 4, item.style);
                    cyPos++;
                }

                sl.SetCellValue(cyPos - 1, xPos + 7, "Valid values for <strong>IHM</strong> and Certificate: Yes/No, True/False, 1/0");
                sl.SetCellStyle(cyPos - 1, xPos + 7, CellStyle.Locked);

                sl.SaveAs(ms);
            }

            return ms;
        }
    }
}
