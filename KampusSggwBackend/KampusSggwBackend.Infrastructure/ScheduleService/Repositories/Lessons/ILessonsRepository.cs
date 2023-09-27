namespace KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lessons;

using KampusSggwBackend.Domain.Schedule;
using System;

public interface ILessonsRepository
{
    List<Lesson> GetAll();
    Lesson Get(Guid id);
    void Create(Lesson lesson);
    void Delete(Guid id);
}
