namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;

using System.Drawing;
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

        worksheet.Cells[1, 1].Value = model.PlanName;
        worksheet.Cells[1, 2].Value = "Grupy";
        WriteHours(worksheet);
        WriteDays(worksheet, model);
        WriteGroups(worksheet, model);
        WriteLessons(worksheet, model);
        worksheet.Calculate();

        return package.GetAsByteArray();
    }

    private static void WriteHours(ExcelWorksheet worksheet)
    {
        var currentTime = new TimeSpan(hours: 8, minutes: 0, seconds: 0);
        int currentColumn = 0;
        while (currentTime <= new TimeSpan(hours: 20, minutes: 0, seconds: 0))
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
            var dayRow = worksheet.Cells[currentRow, 1, currentRow + model.Groups.Count - 1, 1];
            dayRow.Value = days[i];
            dayRow.Merge = true;
            dayRow.Style.TextRotation = 180;

            currentRow += model.Groups.Count;
            var emptyRow = worksheet.Cells[currentRow, 1, currentRow, worksheet.Dimension.Columns];
            emptyRow.Value = ""; // what the hell?
            emptyRow.Merge = true;
            emptyRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
            emptyRow.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 204, 143));
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
        var days = new string[] { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek" };
        foreach (Lesson v in model.Lessons)
        {
            /* Start of the algorithm */
            var dayIndex = Array.IndexOf(days, v.Day);
            var lessonDayColumn = 2 + dayIndex * model.Groups.Count + (v.GroupsIds.Count - 1) * 2;

            var lessonStartTimeRow = (v.StartTime.Hour * 60 + v.StartTime.Minute - 480) / 15 + 3;
            var lessonEndTimeRow = (v.EndTime.Hour * 60 + v.EndTime.Minute - 480) / 15 + 3;

            foreach (var group in model.Groups.Where(g => v.GroupsIds.Contains(g.Id)))
            {
                var lessonGroupRow = lessonDayColumn + model.Groups.IndexOf(group);
                var lessonRange = worksheet.Cells[lessonGroupRow, lessonStartTimeRow, lessonGroupRow, lessonEndTimeRow];
                /* End of the algorithm */

                lessonRange.Value = $@"
                    {v.Name} ({v.Type})
                    [s. {v.ClassroomId} b. {v.Classroom.Name}]
                ".Trim();
                lessonRange.Merge = true;
                lessonRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                lessonRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
        }
    }
}