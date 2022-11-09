using ReactiveUI;
using System.ComponentModel;

namespace BuilderKIP.Models
{
    public class Stage : INotifyPropertyChanged
    {
        public int Id { get; set; }


        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _actionName { get; set; }
        public string ActionName
        {
            get { return _actionName; }
            set
            {
                _actionName = value;
                OnPropertyChanged("ActionName");
            }
        }
 

        private string _status { get; set; }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }



        private string _ellipse { get; set; }
        public string Ellipse
        {
            get { return _ellipse; }
            set
            {
                _ellipse = value;
                OnPropertyChanged("Ellipse");
            }
        }


        public int BuildingServiceContractId { get; set; }
        public virtual BuildingServiceContract BuildingServiceContract { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
