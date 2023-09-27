namespace KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Classes;

using KampusSggwBackend.Domain.Schedule;
using System;

public interface IClassesRepository
{
    List<ScheduleClass> GetAll();
    ScheduleClass Get(Guid id);
    void Create(ScheduleClass scheduleClass);
    void Delete(Guid id);
}
