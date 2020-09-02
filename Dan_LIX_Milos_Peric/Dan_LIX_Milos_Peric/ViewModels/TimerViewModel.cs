using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Dan_LIX_Milos_Peric.ViewModels
{
    public class TimerViewModel : ObservableObject
    {
        private DispatcherTimer _playedTimer;
        private TimeSpan _timePlayed;
        private static Stopwatch _stopwatch;
        //Game information scores, attempts etc
        public GameInfoViewModel GameInfo { get; private set; }

        //Collection of slides we are playing with
        public SlideCollectionViewModel Slides { get; private set; }

        public TimeSpan Time
        {
            get
            {
                return _timePlayed;
            }
            set
            {
                _timePlayed = value;
                OnPropertyChanged("Time");
            }
        }

        public TimerViewModel(TimeSpan time, GameInfoViewModel gameInfo, SlideCollectionViewModel slides)
        {
            _playedTimer = new DispatcherTimer();
            _playedTimer.Interval = time;
            _playedTimer.Tick += PlayedTimer_Tick;
            _timePlayed = new TimeSpan();
            _timePlayed = TimeSpan.FromSeconds(65);
            _stopwatch = new Stopwatch();
            GameInfo = gameInfo;
            Slides = slides;
        }

        public void Start()
        {
            _stopwatch.Start();
            _playedTimer.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _playedTimer.Stop();
        }

        private void PlayedTimer_Tick(object sender, EventArgs e)
        {
            Time = _timePlayed.Add(new TimeSpan(0, 0, -1));
            if (Time == TimeSpan.Zero)
            {
                GameInfo.GameStatus(false);
                Slides.RevealUnmatched();
                Stop();
            }
        }

        public static readonly string filePath = @"..\..\IgraPamcenja.txt";
        public static void LogVictoryToFile()
        {
            DateTime currentTime = DateTime.Now;
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("Game Date: " + currentTime.ToString());
                    sw.WriteLine("Game Duration: " + _stopwatch.Elapsed.TotalSeconds + " Seconds");
                    sw.WriteLine("Game Result: Won");
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine("Error: cannot write log to file.");
                Debug.WriteLine(e.Message);
            }
        }
    }
}
