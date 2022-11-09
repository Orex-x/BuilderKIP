namespace BuilderKIP.Models
{
    public class Stage
    {
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string Status { get; set; }
        public string Ellipse { get; set; }

        public int BuildingServiceContractId { get; set; }
        public virtual BuildingServiceContract BuildingServiceContract { get; set; }
    }
}
