﻿namespace CountdownTimer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = null;

        int secondsDue;

        public List<Stage> Stages { get; set; }
        public Stage CurrentStage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Stages = JsonSerializer.Deserialize<List<Stage>>(File.ReadAllText("config.json"));
            CurrentStage = this.Stages.First(s => s.Default);

            this.KeyDown += MainWindow_KeyDown;
            titleLabel.Content = "";
        }

        private void timerStep(object state)
        {
            secondsDue--;

            if (secondsDue == 0)
            {
                timer.Dispose();
                timer = null;
                titleLabel.Dispatcher.Invoke(() =>
                {
                    titleLabel.Content = this.CurrentStage.After;
                });
            }

            WriteTime();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ResetTimer();
            }
            else if (e.Key == Key.Enter)
            {
                StartTimer();
            }
            else if (e.Key == Key.Space)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                }
                else
                {
                    WindowState = WindowState.Maximized;
                    WindowStyle = WindowStyle.None;
                }
            }
            else if (e.Key == Key.Insert)
            {
                var setup = new Setup(this);
                setup.ShowDialog();
            }
        }

        private void StartTimer()
        {
            if (timer != null)
            {
                return;
            }

            titleLabel.Content = this.CurrentStage.During;
            timer = new Timer(timerStep, null, 1000, 1000);
        }

        public void ResetTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }

            titleLabel.Content = "";
            secondsDue = this.CurrentStage.Seconds;
            WriteTime();
        }

        private void WriteTime()
        {
            var minutes = secondsDue / 60 ;
            var seconds = secondsDue % 60;

            timerText.Dispatcher.Invoke(() =>
            {
                timerText.Text = minutes.ToString().PadLeft(2, '0') + ':' + seconds.ToString().PadLeft(2, '0');
            });
        }
    }
}
