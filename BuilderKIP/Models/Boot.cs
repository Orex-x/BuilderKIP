using System.Collections.ObjectModel;

namespace BuilderKIP.Models
{
    public static class Boot
    {
        public static ObservableCollection<Material> Materials { get; set; } = new ObservableCollection<Material>();
    }
}
