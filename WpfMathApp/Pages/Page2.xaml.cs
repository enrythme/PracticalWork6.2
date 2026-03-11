using System;
using System.Windows;

namespace WpfMathApp.Pages
{
    public partial class Page2 : Window
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = ParseDouble(Input_X.Text);
                double y = ParseDouble(Input_Y.Text);

                double fx;
                if (Radio_Sh.IsChecked == true)
                    fx = Math.Sinh(x);
                else if (Radio_X2.IsChecked == true)
                    fx = x * x;
                else
                    fx = Math.Exp(x);

                double d;

                if (Math.Abs(x - y) < 1e-10)
                    d = Math.Pow(y + fx, 3) + 0.5;
                else if (x > y)
                    d = Math.Pow(fx - y, 3) + Math.Atan(fx);
                else
                    d = Math.Pow(y - fx, 3) + Math.Atan(fx);

                Result.Text = d.ToString("G10");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вычисления: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Input_X.Clear();
            Input_Y.Clear();
            Result.Clear();
            Radio_Sh.IsChecked = true;
        }

        private static double ParseDouble(string s) =>
            double.Parse(s.Replace(',', '.').Trim(),
                System.Globalization.CultureInfo.InvariantCulture);
    }
}
