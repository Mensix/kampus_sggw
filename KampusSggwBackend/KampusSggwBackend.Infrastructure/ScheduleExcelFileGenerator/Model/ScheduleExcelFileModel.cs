namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public class ScheduleExcelFileModel
{
    public string PlanName { get; set; }
    public List<Group> Groups { get; set; }
    public List<Lecturer> Lecturers { get; set; }
    public List<Lesson> Lessons { get; set; }
}
