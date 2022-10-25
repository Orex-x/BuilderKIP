using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderKIP.Models
{
    public static class Boot
    {
        public static ObservableCollection<Material> Materials { get; set; } = new ObservableCollection<Material>();
    }
}
