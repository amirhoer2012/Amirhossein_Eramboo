using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class ExcelContent
    {
        public ExcelContent(string excelFileName)
        {
            ExcelFileName = excelFileName;
            Rows = new List<ExcelContentlRow>();
        }
        public string ExcelFileName { get; set; }
        public IList<ExcelContentlRow> Rows { get; set; }

    }
}
