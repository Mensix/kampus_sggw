namespace KampusSggwBackend.GenerateScheduleFile;

using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;
using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public class Program
{
    public static void Main(string[] args)
    {
        var tmGroup = new Group() { Id = new Guid("8b0c5d41-858d-4d63-9a17-5e166c785baa"), Name = "TM", };
        var isk1Group = new Group() { Id = new Guid("a9f1e0e2-7f77-4d98-b2ce-492ba48bf8e5"), Name = "ISK_1", };
        var isi1Group = new Group() { Id = new Guid("687c1cb0-7381-4489-b818-7f25ed4a40dc"), Name = "ISI_1", };
        var isi2Group = new Group() { Id = new Guid("687c1cb0-7381-4489-b818-7f25ed4a40dc"), Name = "ISI_2", };

        var lecturers = new List<Lecturer>()
        {
            new Lecturer() { Id = new Guid("c2efc337-cda0-4b28-a921-9b065972a3ff"), FirstName = "Izabella", LastName = "Antoniuk", AcademicDegree = "dr inż.", Email = "izabellajoanna@gmail.com" },
            new Lecturer() { Id = new Guid("a22cc5e7-d751-443c-9fe9-ccf96a192c14"), FirstName = "Marcin", LastName = "Bator", AcademicDegree = "dr inż.", Email = "marcin_bator@sggw.pl" },
            new Lecturer() { Id = new Guid("1d84cc52-12af-4cd4-a7f1-6cac62e787da"), FirstName = "Agata", LastName = "Binderman", AcademicDegree = "dr inż.", Email = "agata_binderman@sggw.pl" },
            new Lecturer() { Id = new Guid("e3777eb2-5cf2-4983-ab09-4ae5f7e37996"), FirstName = "Michał", LastName = "Szymański", AcademicDegree = "dr hab. inż.", Email = "michal_szymanski@sggw.pl" },
            new Lecturer() { Id = new Guid("dd5e9d1a-250e-4649-9f8b-ec2c05dce93c"), FirstName = "Jarosław", LastName = "Kurek", AcademicDegree = "dr inż.", Email = "jaroslaw_kurek@sggw.pl" },
            new Lecturer() { Id = new Guid("3a050519-2d19-49a2-a4fb-9f061740e7e4"), FirstName = "Andrzej", LastName = "Jodłowski", AcademicDegree = "dr inż.", Email = "andrzej_jodlowski@sggw.pl" },
            new Lecturer() { Id = new Guid("db4ece97-1509-45a5-be13-95201729ad6f"), FirstName = "Zygmunt Jacek", LastName = "Zawistowski", AcademicDegree = "dr hab. inż.", Email = "zygmunt_zawistowski@sggw.pl" },
            new Lecturer() { Id = new Guid("de45f4ee-d310-46f6-90bb-dcd0ce57ed93"), FirstName = "Alexander", LastName = "Prokopenya", AcademicDegree = "dr inż. prof. SGGW", Email = "alexander_prokopenya@sggw.pl" },
            new Lecturer() { Id = new Guid("23cfc02c-0834-4f9d-bf66-d845091cce5b"), FirstName = "Maciej", LastName = "Janowicz", AcademicDegree = "dr hab.", Email = "Maciej_Janowicz@sggw.pl" },
            new Lecturer() { Id = new Guid("05bcfd72-a1e6-481f-9b0d-caeda79ffe7b"), FirstName = "Mikołaj", LastName = "Olszewski", AcademicDegree = "mgr.", Email = "" },
            new Lecturer() { Id = new Guid("56e24909-4934-4516-8b08-683e17505a2d"), FirstName = "Marian", LastName = "Rusek", AcademicDegree = "dr hab. inż.", Email = "marian_rusek@sggw.pl" },
            new Lecturer() { Id = new Guid("74f6a45e-b15f-4874-9c48-9fb54e926a3f"), FirstName = "Dariusz", LastName = "Strzęciwilk", AcademicDegree = "dr inż.", Email = "dariusz_strzeciwilk@sggw.pl" },
            new Lecturer() { Id = new Guid("dbf82b5d-5862-4043-8c1a-8e29091fe178"), FirstName = "Beata", LastName = "Jackowska-Zduniak", AcademicDegree = "dr inż.", Email = "beata_zduniak@sggw.pl" },
            new Lecturer() { Id = new Guid("427e845a-8c93-4210-852b-fa3c2e714fc1"), FirstName = "Maciej", LastName = "Janowicz", AcademicDegree = "dr inż.", Email = "" },
        };

        var room84 = new Classroom() { Id = new Guid("4a5dbbb4-3d42-4f8e-97b6-8d84a20c68ec"), Name = "84", FloorName = "3", BuildingName = "34" };
        var room19 = new Classroom() { Id = new Guid("9feb2a69-10af-4c5c-a4d3-bfd54ef69218"), Name = "19", FloorName = "3", BuildingName = "34" };
        var room11 = new Classroom() { Id = new Guid("9feb2a69-10af-4c5c-a4d3-bfd54ef69218"), Name = "11", FloorName = "3", BuildingName = "34" };

        var classrooms = new List<Classroom>() { room84, room19, room11 };

        var model = new ScheduleExcelFileModel()
        {
            PlanName = "INF Inż R3 S5",
            Groups = new List<Group>() { tmGroup, isk1Group, isi1Group, isi2Group },
            Lecturers = lecturers,
            Classrooms = classrooms,
            Lessons = new List<Lesson>()
            {
                // Monday
                new Lesson() { 
                    Id = new Guid("ce503532-0c9a-4e54-8e4b-7086d04c5c5e"), 
                    Name = "Sieci komputerowe",
                    ClassroomId = room84.Id,
                    GroupsIds = new List<Guid>() { tmGroup.Id,}, 
                    LecturersIds = new List<Guid>() { lecturers.Get("Dariusz", "Strzęciwilk").Id }, 
                    Day = DayOfWeek.Monday, 
                    StartTime = new TimeOnly(8, 45), 
                    EndTime = new TimeOnly(10, 15), 
                    Type = LessonType.Laboratory
                },
                new Lesson() {
                    Id = new Guid("1922d6d4-5d09-4b03-9408-297f21040043"),
                    Name = "Programowanie komponentowe",
                    ClassroomId = room19.Id,
                    GroupsIds = new List<Guid>() { tmGroup.Id },
                    LecturersIds = new List<Guid>() { lecturers.Get("Maciej", "Janowicz").Id, lecturers.Get("Dariusz", "Strzęciwilk").Id },
                    Day = DayOfWeek.Monday,
                    StartTime = new TimeOnly(10, 30),
                    EndTime = new TimeOnly(12, 00),
                    Type = LessonType.Laboratory
                },
                new Lesson() {
                    Id = new Guid("4a693fb5-8f97-4aab-b0a4-7ce25f03fd11"),
                    Name = "Podstawy teleinformatyki",
                    ClassroomId = room11.Id,
                    GroupsIds = new List<Guid>() { isk1Group.Id },
                    LecturersIds = new List<Guid>() { lecturers.Get("Michał", "Szymański").Id },
                    Day = DayOfWeek.Monday,
                    StartTime = new TimeOnly(8, 15),
                    EndTime = new TimeOnly(9, 00),
                    Type = LessonType.Lecture
                },
                new Lesson() {
                    Id = new Guid("4a693fb5-8f97-4aab-b0a4-7ce25f03fd11"),
                    Name = "Podstawy teleinformatyki",
                    ClassroomId = room11.Id,
                    GroupsIds = new List<Guid>() { isk1Group.Id },
                    LecturersIds = new List<Guid>() { lecturers.Get("Michał", "Szymański").Id },
                    Day = DayOfWeek.Monday,
                    StartTime = new TimeOnly(8, 15),
                    EndTime = new TimeOnly(9, 00),
                    Type = LessonType.Lecture
                },
            }
        };

        IScheduleExcelFileGeneratorService service = new ScheduleExcelFileGeneratorService();

        var bytes = service.GenerateExcelFile(model);

        File.WriteAllBytes("Result.xlsx", bytes);
    }
}

public static class LecturerExtension
{
    public static Lecturer Get(this List<Lecturer> lecturers, string firstName, string lastName)
    {
        return lecturers.FirstOrDefault(l => l.FirstName == firstName && l.LastName == lastName);
    }
}