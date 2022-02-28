using System.Windows;
using System.Windows.Controls;

namespace CashpointWPF.Controls
{
    /// <summary>
    /// Interaction logic for BulletTextBox.xaml
    /// </summary>
    public partial class BulletTextBox : UserControl
    {
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public string Body
        {
            get => (string)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }

        public static DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header),
                typeof(string),
                typeof(BulletTextBox));

        public static DependencyProperty BodyProperty =
            DependencyProperty.Register(nameof(Body),
                typeof(string),
                typeof(BulletTextBox));

        public BulletTextBox()
        {
            InitializeComponent();
        }
    }
}