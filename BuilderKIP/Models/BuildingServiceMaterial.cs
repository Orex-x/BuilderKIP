﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderKIP.Models
{
    public class BuildingServiceMaterial
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int BuildingServiceContractId { get; set; }
        public virtual BuildingServiceContract BuildingServiceContract { get; set; }

    }
}
