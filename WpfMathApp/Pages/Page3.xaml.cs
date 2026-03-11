using System;
using System.Text;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WpfMathApp.Pages
{
    public partial class Page3 : Window
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                double x0 = ParseDouble(Input_X0.Text);
                double xk = ParseDouble(Input_Xk.Text);
                double dx = ParseDouble(Input_Dx.Text);
                double a  = ParseDouble(Input_A.Text);
                double b  = ParseDouble(Input_B.Text);

                if (Math.Abs(dx) < 1e-15)
                {
                    MessageBox.Show("Шаг dx не может быть равен нулю.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if ((xk - x0) * dx < 0)
                {
                    MessageBox.Show("Шаг dx имеет неверный знак для заданного диапазона x₀ → xk.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine($"{"x",12}  {"y",20}");
                sb.AppendLine(new string('-', 36));

                var chartValues = new ChartValues<ObservablePoint>();

                double xi = x0;
                int iter = 0;

                while ((dx > 0 ? xi <= xk + 1e-10 : xi >= xk - 1e-10) && iter < 50000)
                {
                    double x3 = xi * xi * xi;
                    double cosVal = Math.Cos(x3 - b);
                    double yi = a * x3 + cosVal * cosVal;

                    sb.AppendLine($"{xi,12:F4}  {yi,20:F6}");
                    chartValues.Add(new ObservablePoint(xi, yi));

                    xi = Math.Round(xi + dx, 10);
                    iter++;
                }

                Table.Text = sb.ToString();
                Result.Text = $"Вычислено {iter} точек";

                Chart.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "y = ax³ + cos²(x³ − b)",
                        Values = chartValues,
                        PointGeometrySize = 5,
                        StrokeThickness = 2,
                        Fill = System.Windows.Media.Brushes.Transparent
                    }
                };

                AxisX.LabelFormatter = val => val.ToString("F2");
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
            Input_X0.Text = "-1.5";
            Input_Xk.Text = "10.3";
            Input_Dx.Text = "0.25";
            Input_A.Text  = "1.35";
            Input_B.Text  = "-6.25";
            Result.Clear();
            Table.Clear();
            Chart.Series = new SeriesCollection();
        }

        private static double ParseDouble(string s) =>
            double.Parse(s.Replace(',', '.').Trim(),
                System.Globalization.CultureInfo.InvariantCulture);
    }
}
