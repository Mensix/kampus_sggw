namespace KampusSggwBackend.Controllers.ScheduleTest;

using KampusSggwBackend.Domain.Schedule;
using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Classes;
using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lecturers;
using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lessons;
using KampusSggwBackend.Services.RequestingUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SchedulteTestController : ControllerBase
{
    // Services
    private readonly IRequestingUserService requestingUserService;
    private readonly ILecturersRepository lecturersRepository;
    private readonly IClassesRepository classesRepository;
    private readonly ILessonsRepository lessonsRepository;

    // Constructor
    public SchedulteTestController(
        IRequestingUserService requestingUserService,
        ILecturersRepository lecturersRepository,
        IClassesRepository classesRepository,
        ILessonsRepository lessonsRepository)
    {
        this.requestingUserService = requestingUserService;
        this.lecturersRepository = lecturersRepository;
        this.classesRepository = classesRepository;
        this.lessonsRepository = lessonsRepository;
    }

    // Methods
    [AllowAnonymous]
    [HttpPost("create-test-schedule")]
    public async Task<ActionResult> CreatePlan()
    {
        var TMGroup = new Group() { Id = new Guid("6a72f7ac-12a5-4c6b-8e96-6f52c42c6aa1"), Name = "Techniki Multimedialne" };
        var ISK1Group = new Group() { Id = new Guid("e0c6d299-1e57-4a32-8df2-0f7c8c2b28c5"), Name = "Inżynieria Systemów Komputerowych" };
        var ISI1Group = new Group() { Id = new Guid("71b0f0e3-18e0-4a02-8f7b-7c0743e70e51"), Name = "Inżynieria Systemów Informacyjnych I" };
        var ISI2Group = new Group() { Id = new Guid("b8e8e7b0-36f3-41e1-8445-184d6ad769ef"), Name = "Inzynieria Systemów Informacyjnych II" };

        var lecturers = new List<Lecturer>()
        {
            new Lecturer() { FirstName = "Izabella", Surname = "Antoniuk", AcademicDegree = "dr inż.", Email = "izabellajoanna@gmail.com" },
            new Lecturer() { FirstName = "Marcin", Surname = "Bator", AcademicDegree = "dr inż.", Email = "marcin_bator@sggw.pl" },
            new Lecturer() { FirstName = "Agata", Surname = "Binderman", AcademicDegree = "dr inż.", Email = "agata_binderman@sggw.pl" },
            new Lecturer() { FirstName = "Michał", Surname = "Szymański", AcademicDegree = "dr hab. inż.", Email = "michal_szymanski@sggw.pl" },
            new Lecturer() { FirstName = "Jarosław", Surname = "Kurek", AcademicDegree = "dr inż.", Email = "jaroslaw_kurek@sggw.pl" },
            new Lecturer() { FirstName = "Andrzej", Surname = "Jodłowski", AcademicDegree = "dr inż.", Email = "andrzej_jodlowski@sggw.pl" },
            new Lecturer() { FirstName = "Zygmunt Jacek", Surname = "Zawistowski", AcademicDegree = "dr hab. inż.", Email = "zygmunt_zawistowski@sggw.pl" },
            new Lecturer() { FirstName = "Alexander", Surname = "Prokopenya", AcademicDegree = "dr inż. prof. SGGW", Email = "alexander_prokopenya@sggw.pl" },
            new Lecturer() { FirstName = "Maciej", Surname = "Janowicz", AcademicDegree = "dr hab.", Email = "Maciej_Janowicz@sggw.pl" },
            new Lecturer() { FirstName = "Mikołaj", Surname = "Olszewski", AcademicDegree = "mgr.", Email = "" },
            new Lecturer() { FirstName = "Marian", Surname = "Rusek", AcademicDegree = "dr hab. inż.", Email = "marian_rusek@sggw.pl" },
            new Lecturer() { FirstName = "Dariusz", Surname = "Strzęciwilk", AcademicDegree = "dr inż.", Email = "dariusz_strzeciwilk@sggw.pl" },
            new Lecturer() { FirstName = "Beata", Surname = "Jackowska-Zduniak", AcademicDegree = "dr inż.", Email = "beata_zduniak@sggw.pl" },
        };

        foreach (var lecturer in lecturers)
        {
            if (lecturer.Id == Guid.Empty)
            {
                lecturer.Id = Guid.NewGuid();
            }
        }

        var classes = new List<ScheduleClass>()
        {
            new ScheduleClass() { Name = "3/84", Building = "34" },
            new ScheduleClass() { Name = "3/19", Building = "34" },
            new ScheduleClass() { Name = "3/11", Building = "34" },
            new ScheduleClass() { Name = "3/85", Building = "34" },
            new ScheduleClass() { Name = "3/24", Building = "34" },
            new ScheduleClass() { Name = "3/3", Building = "34" },
            new ScheduleClass() { Name = "3/82", Building = "34" },
            new ScheduleClass() { Name = "3/6", Building = "34" },
            new ScheduleClass() { Name = "Aula IV", Building = "34" },
            new ScheduleClass() { Name = "3/31", Building = "34" },
            new ScheduleClass() { Name = "3/20", Building = "34" },
            new ScheduleClass() { Name = "3/8", Building = "34" }
        };

        foreach (var @class in classes)
        {
            if (@class.Id == Guid.Empty)
            {
                @class.Id = Guid.NewGuid();
            }
        }

        var lessons = new List<Lesson>
        {
            new Lesson() { GroupId = TMGroup.Id, Name = "Sieci komputerowe", StartTime = DateTime.Parse("08:45"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = TMGroup.Id, Name = "Programowanie komponentowe", StartTime = DateTime.Parse("10:30"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = ISK1Group.Id, Name = "Podstawy teleinformatyki", StartTime = DateTime.Parse("08:15"), Duration = TimeSpan.FromMinutes(30), Type = LessonType.Lecture },
            new Lesson() { GroupId = ISK1Group.Id, Name = "Podstawy teleinformatyki", StartTime = DateTime.Parse("09:00"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = ISK1Group.Id, Name = "Techniki cyfrowe i podstawy systemow wbudowanych", StartTime = DateTime.Parse("10:45"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = ISI1Group.Id, Name = "Architektura oprogramowania", StartTime = DateTime.Parse("10:30"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = ISI1Group.Id, Name = "Architektura oprogramowania", StartTime = DateTime.Parse("13:15"), Duration = TimeSpan.FromMinutes(45), Type = LessonType.Lecture },
            new Lesson() { GroupId = ISI2Group.Id, Name = "Architektura oprogramowania", StartTime = DateTime.Parse("08:45"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = ISI2Group.Id, Name = "Sieci komputerowe", StartTime = DateTime.Parse("10:30"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories },
            new Lesson() { GroupId = ISI2Group.Id, Name = "Techniki cyfrowe i podstawy systemow wbudowanych", StartTime = DateTime.Parse("13:15"), Duration = TimeSpan.FromMinutes(45), Type = LessonType.Lecture },
            new Lesson() { GroupId = ISI2Group.Id, Name = "Techniki cyfrowe i podstawy systemow wbudowanych", StartTime = DateTime.Parse("14:15"), Duration = TimeSpan.FromMinutes(90), Type = LessonType.Laboratories }
        };

        lecturers.ForEach(l => this.lecturersRepository.Create(l));
        classes.ForEach(c => this.classesRepository.Create(c));
        lessons.ForEach(l => this.lessonsRepository.Create(l));

        return Ok();
    }

    //[HttpDelete]
    //public ActionResult ClearDatabase()
    //{
        
    //}
}
