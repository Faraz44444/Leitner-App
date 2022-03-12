using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagPortal.Report.Excel
{
    public abstract class ExcelFileBase
    {
        private int ColumnStartIndex { get; set; } = 1;
        protected int CurrentRowIndex { get; set; } = 1;
        private int EndRowIndex { get; set; }

        protected void CreateTable(SLDocument sl, List<TableColumnDetails> columns, string defaultSheetName,
            List<Dictionary<string, object>> dataList = null, List<TableDropdownItem> dropdowns = null,
            List<TableGroupedHeader> groupedHeaderColumns = null, List<TableRowDetails> rows = null,
            List<TableCellDetails> cells = null,
            int xOffset = 1, int yOffset = 1, int maxNumberOfItems = 3000)
        {
            ColumnStartIndex = xOffset;
            CurrentRowIndex = yOffset;
            EndRowIndex = maxNumberOfItems + yOffset;

            if (dataList == null) dataList = new List<Dictionary<string, object>>();
            else if (dataList.Count() > 0) EndRowIndex = (groupedHeaderColumns == null ? 0 : 1) + dataList.Count() + yOffset;


            if (rows == null) rows = new List<TableRowDetails>();
            if (cells == null) cells = new List<TableCellDetails>();
            if (dropdowns == null) dropdowns = new List<TableDropdownItem>();

            if (!dropdowns.Any(x => x.ColumnName == "BooleanDropdownList"))
                dropdowns.Add(new TableDropdownItem("BooleanDropdownList", "Yes / No", new List<string> { "Yes", "No" }));


            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, defaultSheetName);

            // CREATE THE TABLE COLUMN HEADER
            CreateTableHeader(sl, columns, groupedHeaderColumns);
            CurrentRowIndex++;

            CreateTableDropDowns(sl, dropdowns);
            sl.SelectWorksheet(defaultSheetName);

            ApplyTableDataValidation(sl, columns, dropdowns);

            // DISABLE DISABLED COLUMNS 
            var xPos = ColumnStartIndex;
            var yPos = CurrentRowIndex;

            CreateTableBody(sl, columns, dataList);


            foreach (var column in columns)
            {
                var style = new SLStyle();
                style.Protection.Locked = false;

                if (column.Disabled) style = CellStyle.Disabled;
                if (column.BackgroundColor.HasValue) CellStyle.SetBackground(style, column.BackgroundColor.Value);
                if (column.BodyBackgroundColor.HasValue) CellStyle.SetBackground(style, column.BodyBackgroundColor.Value);

                sl.SetCellStyle(yPos, xPos, EndRowIndex, xPos, style);
                sl.AutoFitColumn(xPos);
                xPos++;
            }

            foreach (var row in rows)
            {
                var style = new SLStyle();
                if (row.BackgroundColor.HasValue) CellStyle.SetBackground(style, row.BackgroundColor.Value);

                sl.SetCellStyle(yPos + row.RowIndex, ColumnStartIndex, yPos + row.RowIndex, ColumnStartIndex + columns.Count - 1, style);
            }

            foreach (var cell in cells)
            {
                var style = new SLStyle();
                if (cell.TextColor.HasValue) CellStyle.SetTextColor(style, cell.TextColor.Value);

                sl.SetCellStyle(yPos + cell.yPos, cell.xPos, yPos + cell.yPos, cell.xPos, style);
            }

            // Enable DataProtection
            sl.ProtectWorksheet(new SLSheetProtection()
            {
                AllowSelectLockedCells = true,
                AllowFormatColumns = true,
                AllowFormatRows = true,
                AllowInsertRows = true,
            });
        }

        private void CreateTableHeader(SLDocument sl, List<TableColumnDetails> columns, List<TableGroupedHeader> groupedHeaderColumns)
        {
            var xPos = ColumnStartIndex;
            if (groupedHeaderColumns != null)
            {
                //if (groupedHeaderColumns.Any(x =>
                //     groupedHeaderColumns.Any(y => y.FromColumnIndex <= x.FromColumnIndex && y.FromColumnIndex >= x.ToColumnIndex) || 
                //     groupedHeaderColumns.Any(y => y.ToColumnIndex >= x.FromColumnIndex && y.ToColumnIndex <= x.ToColumnIndex)))
                //    throw new NotImplementedException("There is currently only support for one level of grouped table headers");

                foreach (var group in groupedHeaderColumns)
                {
                    var style = CellStyle.HeaderGroup;
                    if (group.BackgroundColor.HasValue) CellStyle.SetBackground(style, group.BackgroundColor.Value);

                    sl.SetCellValue(CurrentRowIndex, ColumnStartIndex + group.FromColumnIndex, group.GroupName);
                    sl.MergeWorksheetCells(CurrentRowIndex, ColumnStartIndex + group.FromColumnIndex,
                        CurrentRowIndex, ColumnStartIndex + group.ToColumnIndex, style);
                }

                CurrentRowIndex++;
            }

            foreach (var column in columns)
            {
                var style = CellStyle.Header;
                if (column.BackgroundColor.HasValue) CellStyle.SetBackground(style, column.BackgroundColor.Value);
                if (column.HeaderBackgroundColor.HasValue) CellStyle.SetBackground(style, column.HeaderBackgroundColor.Value);

                sl.SetCellValue(CurrentRowIndex, xPos, column.ColumnText + (column.Required ? " *" : ""));
                sl.AutoFitColumn(xPos);

                sl.SetCellStyle(CurrentRowIndex, xPos, style);
                xPos++;
            }

        }

        private void CreateTableDropDowns(SLDocument sl, List<TableDropdownItem> dropdowns)
        {
            string dropdownSheetName = "Dropdowns";

            if (!sl.SelectWorksheet(dropdownSheetName))
                if (!sl.AddWorksheet(dropdownSheetName))
                    throw new InvalidDataException();

            var textBold = new SLStyle();
            textBold.SetFontBold(true);

            var letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int xPos = ColumnStartIndex;

            foreach (var dropdown in dropdowns)
            {
                int yPos = 1;
                xPos++;

                // DROPDOWN FOR ITEM
                sl.SetCellValue(yPos, xPos, dropdown.ColumnText);
                sl.SetCellStyle(yPos, xPos, textBold);

                // DROPDOWN VALUES
                foreach (var value in dropdown.Values)
                {
                    yPos++;
                    sl.SetCellValue(yPos, xPos, value);
                }

                sl.AutoFitColumn(xPos);

                sl.SetDefinedName(dropdown.ColumnName, $"{dropdownSheetName}!${letter[xPos - 1]}${2}:${letter[xPos - 1]}${yPos}");
            }

            sl.ProtectWorksheet(new SLSheetProtection());
        }

        private void ApplyTableDataValidation(SLDocument sl, List<TableColumnDetails> columns, List<TableDropdownItem> dropdowns)
        {
            // APPLY DATA RESTRICTIONS
            var xPos = ColumnStartIndex;

            foreach (var column in columns)
            {
                var dv = sl.CreateDataValidation(CurrentRowIndex, xPos, EndRowIndex, xPos);

                if (column.Type == ColumnTypeEnum.Dropdown)
                {
                    // MAKE SURE THAT NONE OF THE OPTIONS OVERFLOWS THE CHECKBOX
                    var items = dropdowns.Find(x => x.ColumnName == column.ColumnName);
                    var longestItem = items.Values.OrderByDescending(x => x.Length).First();

                    sl.SetCellValue(CurrentRowIndex, xPos, longestItem);
                    sl.AutoFitColumn(xPos);
                    sl.ClearCellContent(CurrentRowIndex, xPos, CurrentRowIndex, xPos);

                    // ADD THE DATA VALIDATION
                    dv.AllowList(column.ColumnName, !column.Required, true);
                }
                else if (column.Type == ColumnTypeEnum.Boolean)
                {
                    // MAKE SURE THAT NONE OF THE OPTIONS OVERFLOWS THE CHECKBOX
                    var items = dropdowns.Find(x => x.ColumnName == "BooleanDropdownList");
                    var longestItem = items.Values.OrderByDescending(x => x.Length).First();

                    sl.SetCellValue(CurrentRowIndex, xPos, longestItem);
                    sl.AutoFitColumn(xPos);
                    sl.ClearCellContent(CurrentRowIndex, xPos, CurrentRowIndex, xPos);

                    // ADD THE DATA VALIDATION
                    dv.AllowList("BooleanDropdownList", !column.Required, true);
                }
                else if (column.Type == ColumnTypeEnum.Date)
                    dv.AllowDate(true, DateTime.MinValue, DateTime.MaxValue, !column.Required);
                else if (column.Type == ColumnTypeEnum.WholeNumber)
                    dv.AllowWholeNumber(true, int.MinValue, int.MaxValue, !column.Required);
                else if (column.Type == ColumnTypeEnum.Decimal)
                    dv.AllowDecimal(true, float.MinValue, float.MaxValue, !column.Required);
                else if (column.Type == ColumnTypeEnum.String)
                    dv.AllowTextLength(true, 0, 5000, !column.Required);

                sl.AddDataValidation(dv);


                xPos++;
            }
        }

        private void CreateTableBody(SLDocument sl, List<TableColumnDetails> columns, List<Dictionary<string, object>> dataList)
        {
            foreach (var item in dataList)
            {
                var xPos = ColumnStartIndex;
                foreach (var column in columns)
                {
                    object value;
                    if (item.TryGetValue(column.ColumnName, out value) && value != null)
                    {
                        switch (column.Type)
                        {

                            case ColumnTypeEnum.Date:
                                sl.SetCellValue(CurrentRowIndex, xPos, ((DateTime)value).ToString("dd.MM.yyyy"));
                                break;

                            case ColumnTypeEnum.Decimal:
                                sl.SetCellValue(CurrentRowIndex, xPos, decimal.Parse(value.ToString()));
                                sl.SetCellStyle(CurrentRowIndex, xPos, CellStyle.Decimal);
                                break;

                            case ColumnTypeEnum.WholeNumber:
                                sl.SetCellValue(CurrentRowIndex, xPos, long.Parse(value.ToString()));
                                break;

                            case ColumnTypeEnum.Boolean:
                                sl.SetCellValue(CurrentRowIndex, xPos, (bool)value ? "Yes" : "No");
                                break;

                            default:
                                sl.SetCellValue(CurrentRowIndex, xPos, value.ToString());
                                break;
                        }
                    }
                    xPos++;
                }

                CurrentRowIndex++;
            }
        }

        protected static class CellStyle
        {
            public static readonly System.Drawing.Color Gray100 = System.Drawing.Color.FromArgb(240, 242, 244);
            public static readonly System.Drawing.Color Gray200 = System.Drawing.Color.FromArgb(233, 236, 239);
            public static readonly System.Drawing.Color Gray300 = System.Drawing.Color.FromArgb(222, 226, 230);
            public static readonly System.Drawing.Color Gray400 = System.Drawing.Color.FromArgb(220, 220, 220);
            public static readonly System.Drawing.Color Gray900 = System.Drawing.Color.FromArgb(120, 120, 120);

            public static readonly System.Drawing.Color Blue100 = System.Drawing.Color.FromArgb(191, 209, 223);

            public static readonly System.Drawing.Color White = System.Drawing.Color.FromArgb(255, 255, 255);
            public static readonly System.Drawing.Color Danger = System.Drawing.Color.FromArgb(199, 116, 124);
            public static readonly System.Drawing.Color Success = System.Drawing.Color.FromArgb(113, 158, 115);


            public static SLStyle Disabled
            {
                get
                {
                    var style = new SLStyle();
                    style.Protection.Locked = true;

                    SetBorder(style, Gray400, BorderStyleValues.Thin);
                    SetBackground(style, Gray100);

                    return style;
                }
            }
            public static SLStyle Header
            {
                get
                {
                    var style = new SLStyle();
                    style.Protection.Locked = true;
                    style.Font.Bold = true;

                    SetBorder(style, Gray900, BorderStyleValues.Thin);
                    SetBackground(style, Blue100);

                    return style;
                }
            }

            public static SLStyle HeaderGroup
            {
                get
                {
                    var style = new SLStyle();
                    style.Protection.Locked = true;
                    style.Font.Italic = true;
                    style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                    SetBorder(style, Gray900, BorderStyleValues.Thin);
                    SetBackground(style, Gray100);

                    return style;
                }
            }

            /// <summary>
            /// Text is bold and locked
            /// </summary>
            public static SLStyle Bold
            {
                get
                {
                    var style = new SLStyle();
                    style.Protection.Locked = true;
                    style.Font.Bold = true;

                    return style;
                }
            }

            public static SLStyle Locked
            {
                get
                {
                    var style = new SLStyle();
                    style.Protection.Locked = true;

                    return style;
                }
            }

            public static SLStyle Decimal
            {
                get
                {
                    var style = new SLStyle();
                    style.Protection.Locked = true;
                    style.FormatCode = "# ### ##0.00";

                    return style;
                }
            }

            public static void SetBorder(SLStyle style, System.Drawing.Color borderColor, BorderStyleValues borderStyle)
            {
                style.Border.SetTopBorder(borderStyle, borderColor);
                style.Border.SetRightBorder(borderStyle, borderColor);
                style.Border.SetBottomBorder(borderStyle, borderColor);
                style.Border.SetLeftBorder(borderStyle, borderColor);
            }

            public static void SetBackground(SLStyle style, System.Drawing.Color backgroundColor)
            {
                style.SetPatternFill(PatternValues.Solid, backgroundColor, System.Drawing.Color.Transparent);
            }
            public static void SetTextColor(SLStyle style, System.Drawing.Color color)
            {
                style.SetFontColor(color);
            }

        }

        protected class TableRowDetails
        {
            public int RowIndex { get; set; }
            public System.Drawing.Color? BackgroundColor { get; set; }

            public TableRowDetails(int rowIndex, System.Drawing.Color? backgroundColor = null)
            {
                RowIndex = rowIndex;
                BackgroundColor = backgroundColor;
            }
        }

        protected class TableCellDetails
        {
            public int xPos { get; set; }
            public int yPos { get; set; }
            public System.Drawing.Color? TextColor { get; set; }

            public TableCellDetails(int xPos, int yPos, System.Drawing.Color? textColor = null)
            {
                this.xPos = xPos;
                this.yPos = yPos;
                TextColor = textColor;
            }
        }

        protected class TableColumnDetails
        {
            public string ColumnName { get; set; } // The Object Key
            public string ColumnText { get; set; }
            public ColumnTypeEnum Type { get; set; }
            public bool Required { get; set; }
            public bool Disabled { get; set; }
            public System.Drawing.Color? BackgroundColor { get; set; }
            public System.Drawing.Color? HeaderBackgroundColor { get; set; }
            public System.Drawing.Color? BodyBackgroundColor { get; set; }

            public TableColumnDetails(string columnName, string columnText,
                ColumnTypeEnum type = ColumnTypeEnum.String, bool required = false, bool disabled = false,
                System.Drawing.Color? backgroundColor = null, System.Drawing.Color? headerBackgroundColor = null, System.Drawing.Color? bodyBackgroundColor = null)
            {
                ColumnName = columnName;
                ColumnText = columnText;
                Type = type;
                Required = required;
                Disabled = disabled;
                BackgroundColor = backgroundColor;
                HeaderBackgroundColor = headerBackgroundColor;
                BodyBackgroundColor = bodyBackgroundColor;
            }
        }

        protected class TableDropdownItem
        {
            public string ColumnName { get; set; }
            public string ColumnText { get; set; }
            public List<string> Values { get; set; }

            public TableDropdownItem(string columnName, string columnText, List<string> values)
            {
                ColumnName = columnName;
                ColumnText = columnText;
                Values = values;
            }
        }

        protected class TableGroupedHeader
        {
            public int FromColumnIndex { get; set; }
            public int ToColumnIndex { get; set; }
            public string GroupName { get; set; }
            public System.Drawing.Color? BackgroundColor { get; set; }

            public TableGroupedHeader(int fromColumnIndex, int toColumnIndex, string groupName, System.Drawing.Color? backgroundColor = null)
            {
                FromColumnIndex = fromColumnIndex;
                ToColumnIndex = toColumnIndex;
                GroupName = groupName;
                BackgroundColor = backgroundColor;
            }
        }

        public enum ColumnTypeEnum
        {
            String = 1,
            WholeNumber = 2,
            Date = 4,
            Dropdown = 5,
            Boolean = 6,
            Decimal = 7,
        }
    }
}
