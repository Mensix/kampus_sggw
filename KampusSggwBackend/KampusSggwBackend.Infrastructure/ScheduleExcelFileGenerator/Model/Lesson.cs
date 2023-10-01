namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public enum LessonType
{
    Lecture,
    Exercise,
    Laboratory,
    Project,
    Seminar,
    Other
}

public class Lesson
{
    /*Techniki cyfrowe i podstawy systemow wbudowanych (lab) D.Strzęciwilk [s.3/85 b.34]*/
    public Guid Id { get; set; }
    public string Name { get; set; }
    public LessonType Type { get; set; }
    public List<Guid> LecturersIds { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<Guid> GroupsIds { get; set; }
    public Guid ClassroomId { get; set; }
}
