namespace KampusSggwBackend.Controllers.Lecturers;

using KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lecturers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class LecturersController : ControllerBase
{
    // Services
    private readonly ILecturersRepository lecturersRepository;

    // Constructor
    public LecturersController(
        ILecturersRepository lecturersRepository)
    {
        this.lecturersRepository = lecturersRepository;
    }

    // Methods
    [HttpGet]
    public ActionResult GetLecturers()
    {
        var lecturers = lecturersRepository.GetAll();

        return Ok(lecturers);
    }

    [HttpGet("{lecturerId}")]
    public ActionResult GetLecturer(Guid lecturerId)
    {
        var lecturer = lecturersRepository.Get(lecturerId);

        return Ok(lecturer);
    }
}
