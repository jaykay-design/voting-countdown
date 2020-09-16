using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = null;
        const int timeoutSeconds = 120;
        int secondsDue;

        public MainWindow()
        {
            InitializeComponent();

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
                    titleLabel.Content = "The vote has ended";
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
        }

        private void StartTimer()
        {
            if (timer != null)
            {
                return;
            }

            titleLabel.Content = "The vote will end in";
            timer = new Timer(timerStep, null, 1000, 1000);
        }

        private void ResetTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }

            titleLabel.Content = "";
            secondsDue = timeoutSeconds;
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
