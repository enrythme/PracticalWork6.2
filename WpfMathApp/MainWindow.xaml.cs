using System.Windows;
using WpfMathApp.Pages;

namespace WpfMathApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPage1(object sender, RoutedEventArgs e)
        {
            new Page1().Show();
        }

        private void OpenPage2(object sender, RoutedEventArgs e)
        {
            new Page2().Show();
        }

        private void OpenPage3(object sender, RoutedEventArgs e)
        {
            new Page3().Show();
        }
    }
}
