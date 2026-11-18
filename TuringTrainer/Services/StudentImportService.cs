using System.Net.Mail;
using ClosedXML.Excel;

namespace TuringTrainer.Services;

public class StudentImportService
{
    public List<string> ReadExcel(Stream stream)
    {
        using var workbook = new XLWorkbook(stream);
        var sheet = workbook.Worksheet(1);
        var header = sheet.FirstRowUsed();
        if (header == null)
        {
            throw new InvalidOperationException("The spreadsheet is empty.");
        }

        int emailColumn = 0;
        foreach (var cell in header.CellsUsed())
        {
            if (cell.GetString().Trim().Equals("email", StringComparison.OrdinalIgnoreCase))
            {
                emailColumn = cell.Address.ColumnNumber;
            }
        }
        if (emailColumn == 0)
        {
            throw new InvalidOperationException("The first used row must contain an email column header.");
        }

        var emails = new List<string>();
        var lastRow = sheet.LastRowUsed();
        if (lastRow == null)
        {
            return emails;
        }
        if (lastRow.RowNumber() > 5001)
        {
            throw new InvalidOperationException("Please use a spreadsheet with at most 5,000 rows.");
        }
        for (int row = header.RowNumber() + 1; row <= lastRow.RowNumber(); row++)
        {
            emails.Add(sheet.Cell(row, emailColumn).GetString());
        }
        return emails;
    }

    public List<string> Validate(IEnumerable<string> emails)
    {
        var preview = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string value in emails)
        {
            string email = value.Trim();
            if (email.Length == 0)
            {
                continue;
            }
            if (!MailAddress.TryCreate(email, out var address) || address.Address != email || !address.Host.Contains('.'))
            {
                preview.Add("Invalid: " + email);
            }
            else if (!seen.Add(email))
            {
                preview.Add("Duplicate: " + email);
            }
            else
            {
                preview.Add("Valid: " + email);
            }
        }
        return preview;
    }
}
