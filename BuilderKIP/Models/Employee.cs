namespace BuilderKIP.Models;

public class Employee
{
    public int Id { get; set; }
    public string INN { get; set; }
    public string PassportSeries { get; set; }
    public string PassportNumber { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; }
}