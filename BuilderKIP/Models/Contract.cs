using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BuilderKIP.Models;

public enum ContractStatus
{
    NEW,
    ACCEPT,
    COMPLETED
}

public class Contract
{
    public int Id { get; set; }

    public string Number { get; set; }
    
    public string Address { get; set; }

    public DateTime DeadLine { get; set; }

    public int? ClientId { get; set; } = null;
    public virtual Client Client { get; set; }
    
    public int? EmployeeId { get; set; } = null;
    public virtual Employee? Employee { get; set; }

    public ContractStatus ContractStatus { get; set; }

    public DateTime? DateTimeNew { get; set; }
    public DateTime? DateTimeAccept { get; set; }
    public DateTime? DateTimeCompleted { get; set; }

    public BuildingServiceContract BuildingServiceContract { get; set; }
    
    public int? ReceiptId { get; set; } = null;
    public virtual Receipt? Receipt { get; set; }

    public ObservableCollection<string> GetContractInfo()
    {
        var list = new ObservableCollection<string>();
        list.Add($"Договор № {Number}");
        list.Add($"Статус договора: {ContractStatus}");
        list.Add($"Процесс выполнения: {GetProcces()}%");
        list.Add($"Дата создания: {DateTimeNew}");
        list.Add($"Дата принятия: {DateTimeAccept}");
        list.Add($"Дата завершения: {DateTimeCompleted}");
        list.Add($"Услуга: {BuildingServiceContract.BuildingService?.Name}");
        list.Add($"Тип грунта: {BuildingServiceContract.TypeGround?.Name}");
        list.Add($"Тип рильефа: {BuildingServiceContract.TypeRelief?.Name}");
        list.Add($"Тип климата: {BuildingServiceContract.TypeClimaticCondition?.Name}");
        list.Add($"Стоимость сметы: {GetSumMaterials()}$");
        list.Add($"Стоимость услуг: {GetSumServices()}$");
        list.Add($"Итого: {GetSum()}$");
        return list;
    }

    public int GetProcces()
    {
        return 10;
    }

    public int GetSumMaterials()
    {
        return 10;
    }  
    
    public int GetSumServices()
    {
        return 10;
    }  
    
    public int GetSum()
    {
        return 10;
    }
}