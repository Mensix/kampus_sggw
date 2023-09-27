namespace KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Classes;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.Schedule;

public class ClassesRepository : IClassesRepository
{
    // Services
    private readonly DataContext dbContext;

    // Constructor
    public ClassesRepository(DataContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Methods
    public List<ScheduleClass> GetAll()
    {
        return dbContext.Classes.ToList();
    }

    public ScheduleClass Get(Guid id)
    {
        return dbContext.Classes.FirstOrDefault(l => l.Id == id);
    }

    public void Create(ScheduleClass scheduleClass)
    {
        dbContext.Classes.Add(scheduleClass);
        dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = dbContext.Classes.FirstOrDefault(l => l.Id == id);
        dbContext.Classes.Remove(entity);
        dbContext.SaveChanges();
    }
}
