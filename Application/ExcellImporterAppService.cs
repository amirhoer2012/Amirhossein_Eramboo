using Core;
using Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using OfficeOpenXml;
using System.Data;
using System.Text;

namespace Application
{
    public class ExcellImporterAppService
    {
        private readonly DBContext_EF _context;
        public ExcellImporterAppService(DBContext_EF context)
        {
            _context = context;
        }

        public async Task SaveExcel(byte[] bytes, string fileName)
        {
            try
            {
                if (bytes == null) throw new ArgumentNullException("bytes");

                var excelContets = new List<ExcelContent>();

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        foreach (var worksheet in package.Workbook.Worksheets)
                        {
                            var rowCount = worksheet.Dimension?.Rows ?? 0;
                            if (rowCount > 0)
                            {
                                ExcelContent excelContent = GenerateExcelContentFromWorkSheet(fileName, worksheet,rowCount);
                                excelContets.Add(excelContent);
                            }
                        }
                    }
                }

                _context.AddRange(excelContets);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ExcelContent GenerateExcelContentFromWorkSheet(string fileName, ExcelWorksheet worksheet, int rowCount)
        {
            var excelContent = new ExcelContent(fileName + worksheet.Name);

            for (int rowNumber = 1; rowNumber <= rowCount; rowNumber++)
            {
                ExcelContentlRow excelContentRow = GenerateRowContent(worksheet, excelContent, rowNumber);
                excelContent.Rows.Add(excelContentRow);
            }

            return excelContent;
        }

        private ExcelContentlRow GenerateRowContent(ExcelWorksheet worksheet, ExcelContent excelContent, int rowIndex)
        {
            StringBuilder rowContent = new StringBuilder();

            for (int colNumber = 1; colNumber <= worksheet.Dimension.Columns; colNumber++)
            {
                var columnValue = worksheet.Cells[rowIndex, colNumber].Text;
                rowContent.Append(columnValue + "|;|");
            }

            var excelContentRow = new ExcelContentlRow(rowIndex, excelContent.ExcelFileName, rowContent.ToString());
            return excelContentRow;
        }
    }
}