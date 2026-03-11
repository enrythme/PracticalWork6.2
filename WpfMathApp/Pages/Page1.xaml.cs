using System;
using System.Windows;

namespace WpfMathApp.Pages
{
    public partial class Page1 : Window
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = ParseDouble(Input_X.Text);
                double y = ParseDouble(Input_Y.Text);
                double z = ParseDouble(Input_Z.Text);

                double baseVal = Math.Abs(Math.Cos(x) - Math.Cos(y));
                double exponent = 1 + 2 * Math.Pow(Math.Sin(y), 2);

                double term1 = baseVal == 0 ? 0 : Math.Pow(baseVal, exponent);
                double term2 = 1 + z + (z * z) / 2.0 + (z * z * z) / 3.0 + (z * z * z * z) / 4.0;

                double w = term1 * term2;

                Result.Text = w.ToString("G10");
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
            Input_Z.Clear();
            Result.Clear();
        }

        private static double ParseDouble(string s) =>
            double.Parse(s.Replace(',', '.').Trim(),
                System.Globalization.CultureInfo.InvariantCulture);
    }
}
