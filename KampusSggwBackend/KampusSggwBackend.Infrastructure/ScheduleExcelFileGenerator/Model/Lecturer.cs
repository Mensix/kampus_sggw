﻿namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;

public class Lecturer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AcademicDegree { get; set; }
    public string Email { get; set; }
}