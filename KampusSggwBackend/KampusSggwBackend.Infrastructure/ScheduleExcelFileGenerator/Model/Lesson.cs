namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public class Lesson
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<Guid> LecturersIds { get; set; }
    public List<Guid> GroupsIds { get; set; }
    public Guid ClassroomId { get; set; }
}
