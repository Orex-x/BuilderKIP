using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BuilderKIP.Models;

public enum ContractStatus
{
    NEW,
    NOT_ACCEPT,
    ACCEPT,
    PENDING,
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

    public int BuildingServiceContractId { get; set; }
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
        if(BuildingServiceContract.Stages != null)
        {
            int sum = BuildingServiceContract.Stages.Count;
            int sumAccept = BuildingServiceContract.Stages.ToList().Where(x => x.Status == "Выполнен").ToList().Count;
            return sumAccept * 100 / sum;
        }
        return 0;
    }

    public int GetSumMaterials()
    {
        if(BuildingServiceContract.Materials != null)
        {
            int sum = 0;
            foreach(var item in BuildingServiceContract.Materials)
            {
                sum += item.Material.Price * item.Amount;
            }
            return sum;
        }
        return 0;
    }  
    
    public int GetSumServices()
    {
        if (BuildingServiceContract.BuildingService != null)
        {
            return BuildingServiceContract.BuildingService.Price;
        }
        return 0;
    }  
    
    public int GetSum()
    {
        return GetSumMaterials() + GetSumServices();
    }
}