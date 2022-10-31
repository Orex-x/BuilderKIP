using ReactiveUI;
using System.Windows.Input;

namespace BuilderKIP.Models
{
    public class EstimateViewModel : ReactiveObject
    {

        public ICommand OnClickIncrement { get; private set; }

        public ICommand OnClickDecrement { get; private set; }
        private IEstimateViewModel _i;

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _i.change();
                this.RaiseAndSetIfChanged(ref _count, value);
            }
        }

        private Material _selectedMaterial;
        public Material SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _i.change();
                this.RaiseAndSetIfChanged(ref _selectedMaterial, value);
            }
        }

        public EstimateViewModel(IEstimateViewModel i)
        {
            _i = i;
            OnClickIncrement = ReactiveCommand.Create(() =>
            {
                Count++;
            });

            OnClickDecrement = ReactiveCommand.Create(() =>
            {
                if (Count > 1)
                    Count--;
            });
        }
    }
}
