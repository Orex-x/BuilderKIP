using ReactiveUI;
using System.Windows.Input;

namespace BuilderKIP.Models
{
    public class EstimateViewModel : ReactiveObject
    {

        public ICommand OnClickIncrement { get; private set; }

        public ICommand OnClickDecrement { get; private set; }


        private int _count;
        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }

        private Material _selectedMaterial;
        public Material SelectedMaterial
        {
            get => _selectedMaterial;
            set => this.RaiseAndSetIfChanged(ref _selectedMaterial, value);
        }

        public EstimateViewModel()
        {
            OnClickIncrement = ReactiveCommand.Create(() =>
            {
                Count++;
            });

            OnClickDecrement = ReactiveCommand.Create(() =>
            {
                if(Count > 1)
                    Count--;
            });
        }
    }
}
