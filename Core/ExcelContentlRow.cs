namespace Core
{
    public class ExcelContentlRow
    {
        public ExcelContentlRow(int id, string excelFileName, string columnsValue)
        {
            Id = id;
            ExcelFileName = excelFileName;
            ColumnsValue = columnsValue;
        }

        public int Id { get; set; }
        public string ExcelFileName { get; set; }
        public string ColumnsValue { get; set; }
        public ExcelContent ExcelContent { get; set; }
    }
}
