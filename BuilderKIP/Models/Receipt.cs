using System;

namespace BuilderKIP.Models;

public class Receipt
{
    public int Id { get; set; }
    public DateTime  DateTime { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int BankAccountNumber { get; set; } 
    public int Sum { get; set; }
}