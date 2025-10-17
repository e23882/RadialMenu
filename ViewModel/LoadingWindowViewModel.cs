
using RadialMenu.Utility;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace RadialMenu.ViewModel
{
    public class BlockViewModel : ViewModelBase
    {
        private bool _isLit;
        public bool IsLit
        {
            get { return _isLit; }
            set
            {
                _isLit = value;
                OnPropertyChanged(nameof(IsLit));
            }
        }
    }

    public class LoadingWindowViewModel : ViewModelBase
    {
        private const int BlockCount = 25;
        public ObservableCollection<BlockViewModel> Blocks { get; } = new ObservableCollection<BlockViewModel>();

        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(ProgressText));
            }
        }

        public string ProgressText => $"{_progress}%";

        private DispatcherTimer _timer;

        public LoadingWindowViewModel()
        {
            for (int i = 0; i < BlockCount; i++)
            {
                Blocks.Add(new BlockViewModel());
            }

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50); // 5000ms / 25 blocks = 200ms per block
            _timer.Tick += (s, e) =>
            {
                var nextBlock = Blocks.FirstOrDefault(b => !b.IsLit);
                if (nextBlock != null)
                {
                    nextBlock.IsLit = true;
                    int litCount = Blocks.Count(b => b.IsLit);
                    Progress = (int)((double)litCount / BlockCount * 100);
                }
                else
                {
                    _timer.Stop();
                }
            };
            _timer.Start();
        }
    }
}
