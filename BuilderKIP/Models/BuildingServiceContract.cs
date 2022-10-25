﻿using System.Collections.Generic;

namespace BuilderKIP.Models;

public class BuildingServiceContract
{
    public int Id { get; set; }
    public int? Grade { get; set; }

    public TypeClimaticCondition? TypeClimaticCondition { get; set; }

    public TypeGround? TypeGround { get; set; }

    public TypeRelief? TypeRelief { get; set; }


    public int BuildingServiceId { get; set; }
    public virtual BuildingService? BuildingService { get; set; }

    public int ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    public int PriceEstimate { get; set; }

    public virtual ICollection<Material> Materials { get; set; }
}