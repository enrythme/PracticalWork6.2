using System;
using System.Collections.Generic;

namespace MathLogic
{
    /// <summary>
    /// Статический класс содержит всю математическую логику трёх страниц
    /// из практической работы №4, вынесенную из WPF-кода для unit-тестирования.
    /// </summary>
    public static class MathCalculator
    {

        /// <summary>
        /// Вычисляет значение функции w по формуле:
        /// w = |cos(x) − cos(y)|^(1 + 2·sin²(y)) · (1 + z + z²/2 + z³/3 + z⁴/4)
        /// </summary>
        /// <param name="x">Значение переменной x (в радианах).</param>
        /// <param name="y">Значение переменной y (в радианах).</param>
        /// <param name="z">Значение переменной z.</param>
        /// <returns>Результат вычисления функции w.</returns>
        /// <example>
        /// <code>
        /// double w = MathCalculator.ComputeW(0, Math.PI / 2, 1);
        /// </code>
        /// </example>
        public static double ComputeW(double x, double y, double z)
        {
            double baseVal = Math.Abs(Math.Cos(x) - Math.Cos(y));
            double exponent = 1 + 2 * Math.Pow(Math.Sin(y), 2);

            double term1 = baseVal == 0 ? 0 : Math.Pow(baseVal, exponent);
            double term2 = 1 + z
                             + (z * z) / 2.0
                             + (z * z * z) / 3.0
                             + (z * z * z * z) / 4.0;

            return term1 * term2;
        }

        /// <summary>
        /// Перечисление видов функции f(x) для страницы 2,
        /// соответствующее RadioButton в интерфейсе.
        /// </summary>
        public enum FunctionType
        {
            /// <summary>Гиперболический синус: f(x) = sh(x)</summary>
            Sinh,
            /// <summary>Квадрат аргумента: f(x) = x²</summary>
            Square,
            /// <summary>Экспонента: f(x) = eˣ</summary>
            Exp
        }

        /// <summary>
        /// Вычисляет вспомогательную функцию f(x) по выбранному типу.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <param name="type">Вид функции f(x).</param>
        /// <returns>Значение f(x).</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается при неизвестном значении <paramref name="type"/>.
        /// </exception>
        public static double ComputeFx(double x, FunctionType type)
        {
            switch (type)
            {
                case FunctionType.Sinh:   return Math.Sinh(x);
                case FunctionType.Square: return x * x;
                case FunctionType.Exp:    return Math.Exp(x);
                default: throw new ArgumentOutOfRangeException("type");
            }
        }

        /// <summary>
        /// Вычисляет значение функции d по формуле с ветвлением:
        /// <list type="bullet">
        ///   <item>x &gt; y: d = (f(x) − y)³ + arctan(f(x))</item>
        ///   <item>y &gt; x: d = (y − f(x))³ + arctan(f(x))</item>
        ///   <item>y = x: d = (y + f(x))³ + 0.5</item>
        /// </list>
        /// </summary>
        /// <param name="x">Значение переменной x.</param>
        /// <param name="y">Значение переменной y.</param>
        /// <param name="type">Вид функции f(x).</param>
        /// <returns>Результат вычисления функции d.</returns>
        public static double ComputeD(double x, double y, FunctionType type)
        {
            double fx = ComputeFx(x, type);

            if (Math.Abs(x - y) < 1e-10)
                return Math.Pow(y + fx, 3) + 0.5;
            else if (x > y)
                return Math.Pow(fx - y, 3) + Math.Atan(fx);
            else
                return Math.Pow(y - fx, 3) + Math.Atan(fx);
        }

        /// <summary>
        /// Вычисляет значение функции y = a·x³ + cos²(x³ − b).
        /// </summary>
        /// <param name="x">Значение аргумента x.</param>
        /// <param name="a">Коэффициент a.</param>
        /// <param name="b">Коэффициент b.</param>
        /// <returns>Значение функции y в точке x.</returns>
        /// <example>
        /// <code>
        /// double y = MathCalculator.ComputeY(0, 1.35, -6.25);
        /// </code>
        /// </example>
        public static double ComputeY(double x, double a, double b)
        {
            double x3 = x * x * x;
            double cosVal = Math.Cos(x3 - b);
            return a * x3 + cosVal * cosVal;
        }

        /// <summary>
        /// Выполняет табуляцию функции y = a·x³ + cos²(x³ − b) на заданном диапазоне.
        /// Повторяет логику цикла из Page3.xaml.cs.
        /// </summary>
        /// <param name="x0">Начальное значение x.</param>
        /// <param name="xk">Конечное значение x.</param>
        /// <param name="dx">Шаг изменения x. Не может быть равен нулю.</param>
        /// <param name="a">Коэффициент a.</param>
        /// <param name="b">Коэффициент b.</param>
        /// <returns>
        /// Список пар (x, y) в виде <see cref="List{T}"/> кортежей.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается если <paramref name="dx"/> равен нулю
        /// или имеет неверный знак для заданного диапазона x₀ → xk.
        /// </exception>
        public static List<Tuple<double, double>> Tabulate(
            double x0, double xk, double dx, double a, double b)
        {
            if (Math.Abs(dx) < 1e-15)
                throw new ArgumentException("Шаг dx не может быть равен нулю.", "dx");

            if ((xk - x0) * dx < 0)
                throw new ArgumentException(
                    "Шаг dx имеет неверный знак для заданного диапазона.", "dx");

            var points = new List<Tuple<double, double>>();
            double xi = x0;
            int iter = 0;

            while ((dx > 0 ? xi <= xk + 1e-10 : xi >= xk - 1e-10) && iter < 50000)
            {
                points.Add(new Tuple<double, double>(xi, ComputeY(xi, a, b)));
                xi = Math.Round(xi + dx, 10);
                iter++;
            }

            return points;
        }
    }
}
