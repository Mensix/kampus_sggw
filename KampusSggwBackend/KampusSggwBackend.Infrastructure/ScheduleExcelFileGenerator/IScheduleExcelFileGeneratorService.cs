namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;

using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public interface IScheduleExcelFileGeneratorService
{
    byte[] GenerateExcelFile(ScheduleExcelFileModel model);
}
