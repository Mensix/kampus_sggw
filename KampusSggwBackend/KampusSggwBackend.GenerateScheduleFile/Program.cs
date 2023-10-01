namespace KampusSggwBackend.GenerateScheduleFile;

using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;
using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public class Program
{
    public static void Main(string[] args)
    {
        var group1 = new Group() { Id = new Guid("8b0c5d41-858d-4d63-9a17-5e166c785baa"), Name = "G1", Index = 1 };
        var group2 = new Group() { Id = new Guid("a9f1e0e2-7f77-4d98-b2ce-492ba48bf8e5"), Name = "G2", Index = 2 };
        var group3 = new Group() { Id = new Guid("687c1cb0-7381-4489-b818-7f25ed4a40dc"), Name = "G3", Index = 3 };

        var lecturer1 = new Lecturer() { Id = new Guid("7cd6f69d-60a6-4c6c-b7f2-9cfbe65c7746"), FirstName = "Jarosław", LastName = "Kaczyński" };
        var lecturer2 = new Lecturer() { Id = new Guid("db4f5ea2-8ea6-44a9-996f-265f9d6a3ff3"), FirstName = "Donald", LastName = "Tusk" };

        var class1 = new Classroom() { Id = new Guid("4a5dbbb4-3d42-4f8e-97b6-8d84a20c68ec"), Name = "40", FloorName = "3", BuildingName = "34" };

        var model = new ScheduleExcelFileModel()
        {
            PlanName = "Plan XXX",
            Groups = new List<Group>() { group1, group2, group3 },
            Lecturers = new List<Lecturer>() { lecturer1, lecturer2, },
            Classrooms = new List<Classroom>() { class1 },
            Lessons = new List<Lesson>()
            {
                new Lesson() { 
                    Id = new Guid("ce503532-0c9a-4e54-8e4b-7086d04c5c5e"), 
                    Name = "Kaczologia",
                    ClassroomId = class1.Id, 
                    GroupsIds = new List<Guid>() { group2.Id, group3.Id }, 
                    LecturersIds = new List<Guid>() { lecturer1.Id }, 
                    Day = "Poniedziałek", 
                    StartTime = new TimeOnly(8, 30), 
                    EndTime = new TimeOnly(9, 15), 
                    Type = LessonType.Laboratory
                },
                new Lesson() {
                    Id = new Guid("ce503532-0c9a-4e54-8e4b-7086d04c5c5e"),
                    Name = "Tuskologia",
                    ClassroomId = class1.Id,
                    GroupsIds = new List<Guid>() { group1.Id, group3.Id },
                    LecturersIds = new List<Guid>() { lecturer2.Id },
                    Day = "Poniedziałek",
                    StartTime = new TimeOnly(12, 30),
                    EndTime = new TimeOnly(14, 15),
                    Type = LessonType.Laboratory
                }
            }
        };

        IScheduleExcelFileGeneratorService service = new ScheduleExcelFileGeneratorService();

        var bytes = service.GenerateExcelFile(model);

        File.WriteAllBytes("Result.xlsx", bytes);
    }
}