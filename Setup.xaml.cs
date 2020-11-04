namespace CountdownTimer
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        readonly MainWindow target;

        public Setup(MainWindow target)
        {
            this.target = target;
            InitializeComponent();

            foreach(var stage in target.Stages)
            {
                ComboBoxStages.Items.Add(stage);
                if(target.CurrentStage == stage)
                {
                    ComboBoxStages.SelectedItem = stage;
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            target.CurrentStage = (Stage)ComboBoxStages.SelectedItem;
            target.ResetTimer();
            this.Close();
        }
    }
}
