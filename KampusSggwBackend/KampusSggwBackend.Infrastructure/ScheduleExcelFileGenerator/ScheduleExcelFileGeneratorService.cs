namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;

using System.Drawing;
using System.Globalization;
using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;

/// <summary>
/// EPPlus library. Documentation: https://www.epplussoftware.com/en/Developers/
/// </summary>
public class ScheduleExcelFileGeneratorService : IScheduleExcelFileGeneratorService
{
    public byte[] GenerateExcelFile(ScheduleExcelFileModel model)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add(model.PlanName);
        worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        WritePlanName(worksheet, model.PlanName);
        WriteGroupsCell(worksheet);
        WriteHours(worksheet);
        WriteDays(worksheet, model);
        WriteGroups(worksheet, model);
        WriteLessons(worksheet, model);
        worksheet.Calculate();

        return package.GetAsByteArray();
    }

    private static void WritePlanName(ExcelWorksheet worksheet, string planName)
    {
        worksheet.Cells[1, 1].Value = planName;
    }

    private static void WriteGroupsCell(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, 2].Value = "Grupy";
    }

    private static void WriteHours(ExcelWorksheet worksheet)
    {
        var currentTime = new TimeSpan(hours: 8, minutes: 0, seconds: 0);
        int currentColumn = 0;
        while (currentTime < new TimeSpan(hours: 20, minutes: 0, seconds: 0))
        {
            worksheet.Cells[1, currentColumn + 3].Value = currentTime.ToString("h\\:mm");
            worksheet.Cells[1, currentColumn + 3].Style.Font.Bold = true;

            currentColumn++;
            currentTime = currentTime.Add(TimeSpan.FromMinutes(15));
        }

    }

    private static void WriteDays(ExcelWorksheet worksheet, ScheduleExcelFileModel model)
    {
        string[] days = { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek" };
        var currentRow = 2;

        for (int i = 0; i < days.Length; i++)
        {
            worksheet.Cells[i + currentRow, 1, i + currentRow + model.Groups.Count - 1, 1].Value = days[i];
            worksheet.Cells[i + currentRow, 1, i + currentRow + model.Groups.Count - 1, 1].Merge = true;
            worksheet.Cells[i + currentRow, 1, i + currentRow + model.Groups.Count - 1, 1].Style.TextRotation = 180;
            currentRow += model.Groups.Count;
        }
    }

    private static void WriteGroups(ExcelWorksheet worksheet, ScheduleExcelFileModel model)
    {
        string[] days = { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek" };
        var currentRow = 2;

        for (int i = 0; i < days.Length; i++)
        {
            foreach (var group in model.Groups)
            {
                worksheet.Cells[currentRow, 2].Value = group.Name;
                currentRow++;
            }

            currentRow++;
        }
    }

    private static void WriteLessons(ExcelWorksheet worksheet, ScheduleExcelFileModel model)
    {
        for (var i = 0; i < model.Lessons.Count; i++)
        {
            var lessonDayColumn = worksheet.Cells[1, 1, worksheet.Dimension.Rows, 1]
                .FirstOrDefault(cell => cell.Value.ToString() == model.Lessons[i].Day)
                .Start.Row;

            var lessonStartTimeRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns]
          .FirstOrDefault(cell => cell.Value.ToString() == model.Lessons[i].StartTime.ToString("H:mm", CultureInfo.InvariantCulture))
          .Start.Column;

            var lessonEndTimeRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns]
                .FirstOrDefault(cell => cell.Value.ToString() == model.Lessons[i].EndTime.ToString("H:mm", CultureInfo.InvariantCulture))
                .Start.Column;

            for (var j = 0; j < model.Groups.Count; j++)
            {
                var lessonGroupRow = worksheet.Cells[lessonDayColumn + j, 1, lessonDayColumn + j, worksheet.Dimension.Columns]
                    .FirstOrDefault(cell => cell.Value.ToString() == model.Groups[j].Name)
                    .Start.Row;

                worksheet.Cells[lessonGroupRow, lessonStartTimeRow, lessonGroupRow, lessonEndTimeRow].Value = model.Lessons[i].Name;
                worksheet.Cells[lessonGroupRow, lessonStartTimeRow, lessonGroupRow, lessonEndTimeRow].Merge = true;
                worksheet.Cells[lessonGroupRow, lessonStartTimeRow, lessonGroupRow, lessonEndTimeRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[lessonGroupRow, lessonStartTimeRow, lessonGroupRow, lessonEndTimeRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
        }
    }
}