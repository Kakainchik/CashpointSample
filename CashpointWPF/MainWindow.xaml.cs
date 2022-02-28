using CashpointWPF.DB;
using CashpointWPF.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace CashpointWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            //Implement a simple dependency injection
            db = new ApplicationContext();

            base.DataContext = new MainViewModel(db);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            db.Dispose();
            base.OnClosing(e);
        }
    }
}