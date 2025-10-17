
using RadialMenu.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace RadialMenu.ViewModel
{
    public class SystemUsageWindowViewModel : ViewModelBase
    {
        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _ramCounter;
        private DispatcherTimer _timer;

        private int _cpuUsage;
        public int CpuUsage
        {
            get => _cpuUsage;
            set
            {
                _cpuUsage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CpuUsageText));
            }
        }

        public string CpuUsageText
        {
            get
            {
                return $"{CpuUsage}%";
            }
            set
            {
                OnPropertyChanged();
            }
        }

        private int _ramUsage;
        public int RamUsage
        {
            get => _ramUsage;
            set
            {
                _ramUsage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RamUsageText));
            }
        }

        public string RamUsageText
        {
            get
            {
                return $"{RamUsage}%";
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public SystemUsageWindowViewModel()
        {
            try
            {
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log it or show a message
                Debug.WriteLine($"Error initializing performance counters: {ex.Message}");
                CpuUsageText = "N/A";
                RamUsageText = "N/A";
                return;
            }

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // Initial reading
            UpdateUsage();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateUsage();
        }

        private void UpdateUsage()
        {
            try
            {
                CpuUsage = (int)_cpuCounter.NextValue();
                RamUsage = (int)_ramCounter.NextValue();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading performance counters: {ex.Message}");
                // Stop the timer if counters fail
                _timer.Stop();
            }
        }
    }
}
