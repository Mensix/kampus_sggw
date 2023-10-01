namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;

using System.Data;
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
        worksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        var scheduleNameCell = worksheet.Cells[1, 1];
        scheduleNameCell.Value = model.PlanName;
        scheduleNameCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        scheduleNameCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        scheduleNameCell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        scheduleNameCell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
        scheduleNameCell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
        scheduleNameCell.Style.Border.Top.Style = ExcelBorderStyle.Medium;
        scheduleNameCell.Value = model.PlanName;

        WriteDays(worksheet, model);

        var column = worksheet.Columns[3];
        worksheet.Calculate();

        return package.GetAsByteArray();
    }

    private void WriteDays(ExcelWorksheet worksheet, ScheduleExcelFileModel model)
    {
        var daysCount = 5; // From Monday to Friday
        var currentRow = 2;
        var marginEmtpyRows = 1;

        for (int i = 0; i < daysCount; i++)
        {
            var dayOfWeek = (DayOfWeek)(i + 1);

            var dayRow = worksheet.Cells[currentRow, 1, currentRow + model.Groups.Count - 1 + 1, 1];
            dayRow.Value = DayOfWeekToString(dayOfWeek);
            dayRow.Merge = true;
            dayRow.Style.TextRotation = 180;
            dayRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            dayRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            dayRow.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            dayRow.Style.Border.Left.Style = ExcelBorderStyle.Medium;
            dayRow.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            dayRow.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            dayRow.EntireRow.Height = 60;

            WriteHours(worksheet, currentRow);
            WriteGroups(worksheet, model, currentRow, dayOfWeek);

            currentRow += model.Groups.Count + 1;

            WriteEmptyRow(worksheet, currentRow);

            currentRow += marginEmtpyRows;
        }
    }

    private void WriteHours(ExcelWorksheet worksheet, int startRow)
    {
        worksheet.Rows[startRow].Height = 16;

        var startTime = new TimeSpan(hours: 8, minutes: 0, seconds: 0);
        var endTime = new TimeSpan(hours: 20, minutes: 0, seconds: 0);
        var currentTime = startTime;
        int currentColumn = 0;

        while (currentTime <= endTime)
        {
            var hourCell = worksheet.Cells[startRow, currentColumn + 3];

            hourCell.Value = currentTime.ToString("h\\:mm");
            hourCell.Style.Font.Bold = true;
            hourCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            hourCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            hourCell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            hourCell.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            worksheet.Columns[currentColumn + 3].Width = 6;

            if (currentColumn == 0) // is first cell in hours line
            {
                hourCell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                hourCell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            else if (currentTime.Add(TimeSpan.FromMinutes(15)) > endTime) // is last cell in hours line
            {

                hourCell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                hourCell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            }
            else // any other cell
            {
                hourCell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                hourCell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            currentColumn++;
            currentTime = currentTime.Add(TimeSpan.FromMinutes(15));
        }
    }

    private void WriteGroups(ExcelWorksheet worksheet, ScheduleExcelFileModel model, int startRow, DayOfWeek day)
    {
        var groupsCell = worksheet.Cells[startRow, 2];
        groupsCell.Value = model.PlanName;
        groupsCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        groupsCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        groupsCell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        groupsCell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
        groupsCell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
        groupsCell.Style.Border.Top.Style = ExcelBorderStyle.Medium;
        groupsCell.Value = "Grupy";

        for (int groupIndex = 0; groupIndex < model.Groups.Count; groupIndex++)
        {
            var group = model.Groups[groupIndex];

            var groupNameCell = worksheet.Cells[startRow + groupIndex + 1, 2];
            groupNameCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            groupNameCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            groupNameCell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            groupNameCell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
            groupNameCell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            groupNameCell.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            groupNameCell.Value = group.Name;

            WriteGroupLessons(worksheet, model, group.Id, day, startRow + groupIndex + 1);
        }
    }

    private void WriteEmptyRow(ExcelWorksheet worksheet, int rowIdenx)
    {
        var emptyRow = worksheet.Cells[rowIdenx, 1, rowIdenx, worksheet.Dimension.Columns];
        emptyRow.Value = "";
        emptyRow.Merge = true;
        emptyRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
        emptyRow.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 204, 143));
        emptyRow.EntireRow.Height = 4;
        emptyRow.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        emptyRow.Style.Border.Left.Style = ExcelBorderStyle.Medium;
        emptyRow.Style.Border.Right.Style = ExcelBorderStyle.Medium;
        emptyRow.Style.Border.Top.Style = ExcelBorderStyle.Medium;
    }

    private void WriteGroupLessons(ExcelWorksheet worksheet, ScheduleExcelFileModel model, Guid currentGroupId, DayOfWeek day, int rowIndex)
    {
        var startTime = new TimeSpan(hours: 8, minutes: 0, seconds: 0);
        var endTime = new TimeSpan(hours: 20, minutes: 0, seconds: 0);
        var currentTime = startTime;
        int currentColumn = 0;

        while (currentTime <= endTime)
        {
            var contentCell = worksheet.Cells[rowIndex, currentColumn + 3];

            contentCell.Style.Font.Bold = true;
            contentCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            contentCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            contentCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            contentCell.Style.Border.Top.Style = ExcelBorderStyle.Thin;

            if (currentColumn == 0) // is first cell in hours line
            {
                contentCell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                contentCell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            else if (currentTime.Add(TimeSpan.FromMinutes(15)) > endTime) // is last cell in hours line
            {
                contentCell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                contentCell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            }
            else // any other cell
            {
                contentCell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                contentCell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            currentColumn++;
            currentTime = currentTime.Add(TimeSpan.FromMinutes(15));
        }

        var lessons = model.Lessons.Where(l => l.Day == day && l.GroupsIds.Contains(currentGroupId)).ToList();

        foreach (Lesson lesson in lessons)
        {
            var lessonClassroom = model.Classrooms.First(c => c.Id == lesson.ClassroomId);
            var lessonLecturers = model.Lecturers.Where(l => lesson.LecturersIds.Contains(l.Id)).ToList();

            var lessonStartTimeColumn = (lesson.StartTime.Hour * 60 + lesson.StartTime.Minute - 480) / 15 + 3;
            var lessonEndTimeColumn = (lesson.EndTime.Hour * 60 + lesson.EndTime.Minute - 480) / 15 + 3 - 1;

            var lessonCells = worksheet.Cells[rowIndex, lessonStartTimeColumn, rowIndex, lessonEndTimeColumn];
            lessonCells.Merge = true;
            lessonCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            lessonCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            lessonCells.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            lessonCells.Style.Border.Left.Style = ExcelBorderStyle.Medium;
            lessonCells.Style.Border.Right.Style = ExcelBorderStyle.Medium;
            lessonCells.Style.Border.Top.Style = ExcelBorderStyle.Medium;

            if (lesson.Type == LessonType.Laboratory)
            {
                lessonCells.Style.Fill.SetBackground(Color.Silver);
            }

            lessonCells.Style.WrapText = true;
            lessonCells.IsRichText = true;
            var lessonNameText = lessonCells.RichText.Add($"{lesson.Name} ({LessonTypeToString(lesson.Type)})" + GetNewLineCharacter());
            lessonNameText.Bold = true;
            string lecturersRaw = string.Join(", ", lessonLecturers.Select(ll => $"{ll.AcademicDegree} {ll.FirstName} {ll.LastName}"));
            var lecturerText = lessonCells.RichText.Add(lecturersRaw + GetNewLineCharacter());
            var classroomText = lessonCells.RichText.Add($"[s. {lessonClassroom.Name} b. {lessonClassroom.BuildingName}]");
            classroomText.Bold = false;
        }
    }

    private string LessonTypeToString(LessonType type)
    {
        switch (type)
        {
            case LessonType.Laboratory: return "lab";
            case LessonType.Lecture: return "w";
            case LessonType.Exercise: return "ćw";
            case LessonType.Project: return "proj";
            case LessonType.Seminar: return "s";
            case LessonType.Other: return "o";
            default: throw new ArgumentException("Incorrect LessonType: " + type);
        }
    }

    private string DayOfWeekToString(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return "Poniedziałek";
            case DayOfWeek.Tuesday: return "Wtorek";
            case DayOfWeek.Wednesday: return "Środa";
            case DayOfWeek.Thursday: return "Czwartek";
            case DayOfWeek.Friday: return "Piątek";
            case DayOfWeek.Saturday: return "Sobota";
            case DayOfWeek.Sunday: return "Niedziela";
            default: throw new ArgumentException("Incorrect DayOfWeek: " + day);
        }
    }

    private string GetNewLineCharacter()
    {
        return ((char)10).ToString();
    }
}