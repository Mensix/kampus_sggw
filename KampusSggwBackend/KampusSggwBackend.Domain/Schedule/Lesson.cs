namespace KampusSggwBackend.Domain.Schedule;

using System;

public class Lesson
{
    public Guid Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public LessonType Type { get; set; }
    public List<Lecturer> Lecturers { get; set; }
}
