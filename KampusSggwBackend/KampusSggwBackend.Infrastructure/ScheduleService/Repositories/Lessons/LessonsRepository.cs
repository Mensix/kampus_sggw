namespace KampusSggwBackend.Infrastructure.ScheduleService.Repositories.Lessons;

using KampusSggwBackend.Data;
using KampusSggwBackend.Domain.Schedule;

public class LessonsRepository : ILessonsRepository
{
    // Services
    private readonly DataContext dbContext;

    // Constructor
    public LessonsRepository(DataContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Methods
    public List<Lesson> GetAll()
    {
        return dbContext.Lessons.ToList();
    }

    public Lesson Get(Guid id)
    {
        return dbContext.Lessons.FirstOrDefault(l => l.Id == id);
    }

    public void Create(Lesson lesson)
    {
        dbContext.Lessons.Add(lesson);
        dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = dbContext.Lessons.FirstOrDefault(l => l.Id == id);
        dbContext.Lessons.Remove(entity);
        dbContext.SaveChanges();
    }
}
