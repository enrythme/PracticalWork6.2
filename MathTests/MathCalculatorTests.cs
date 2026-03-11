using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLogic;
using System;

namespace MathTests
{
    /// <summary>
    /// Класс содержит автоматизированные unit-тесты для класса <see cref="MathCalculator"/>.
    /// Тестирование проводится методом "белого ящика" с использованием MSTest.
    /// Охватывает три математические функции из практической работы №4.
    /// </summary>
    [TestClass]
    public class MathCalculatorTests
    {

        /// <summary>
        /// Тест 1: при x = y основание |cos(x) - cos(y)| = 0, поэтому w = 0
        /// независимо от значения z.
        /// </summary>
        [TestMethod]
        public void ComputeW_WhenXEqualsY_ReturnsZero()
        {
            double x = 1.0, y = 1.0, z = 5.0;

            double result = MathCalculator.ComputeW(x, y, z);

            Assert.AreEqual(0.0, result, 1e-10,
                "При x = y основание |cos(x) - cos(y)| = 0, поэтому w должно быть равно 0.");
        }

        /// <summary>
        /// Тест 2: при x = 0, y = 0, z = 0 оба множителя дают w = 0.
        /// </summary>
        [TestMethod]
        public void ComputeW_WhenAllZero_ReturnsZero()
        {
            double x = 0, y = 0, z = 0;

            double result = MathCalculator.ComputeW(x, y, z);

            Assert.AreEqual(0.0, result, 1e-10,
                "При x=0, y=0 основание равно нулю, w должно быть равно 0.");
        }

        /// <summary>
        /// Тест 3: проверка вычисления w при x=0, y=π/2, z=1.
        /// Ручной расчёт:
        /// baseVal = |cos(0) - cos(π/2)| = |1 - 0| = 1;
        /// exponent = 1 + 2·sin²(π/2) = 1 + 2·1 = 3;
        /// term1 = 1³ = 1;
        /// term2 = 1 + 1 + 0.5 + 1/3 + 0.25 ≈ 3.0833...
        /// w = 1 · 3.0833... ≈ 3.0833...
        /// </summary>
        [TestMethod]
        public void ComputeW_WithKnownValues_ReturnsExpectedResult()
        {
            double x = 0.0, y = Math.PI / 2.0, z = 1.0;
            double expected = 1.0 * (1 + 1 + 0.5 + 1.0 / 3.0 + 0.25);

            double result = MathCalculator.ComputeW(x, y, z);

            Assert.AreEqual(expected, result, 1e-9,
                "Результат вычисления w не совпадает с ожидаемым значением.");
        }

        /// <summary>
        /// Тест 1: при x = y используется ветка d = (y + f(x))³ + 0.5.
        /// Проверяется с f(x) = x²: x=2, y=2 → f(2)=4 → d = (2+4)³ + 0.5 = 216.5.
        /// </summary>
        [TestMethod]
        public void ComputeD_WhenXEqualsY_UsesEqualBranch()
        {
            double x = 2.0, y = 2.0;
            double expected = Math.Pow(y + x * x, 3) + 0.5; // (2 + 4)^3 + 0.5 = 216.5

            double result = MathCalculator.ComputeD(x, y, MathCalculator.FunctionType.Square);

            Assert.AreEqual(expected, result, 1e-9,
                "При x = y должна использоваться ветка (y + f(x))³ + 0.5.");
        }

        /// <summary>
        /// Тест 2: при x > y используется ветка d = (f(x) - y)³ + arctan(f(x)).
        /// Проверяется с f(x) = sh(x).
        /// </summary>
        [TestMethod]
        public void ComputeD_WhenXGreaterThanY_UsesXGreaterBranch()
        {

            double x = 3.0, y = 1.0;
            double fx = Math.Sinh(x);
            double expected = Math.Pow(fx - y, 3) + Math.Atan(fx);

            double result = MathCalculator.ComputeD(x, y, MathCalculator.FunctionType.Sinh);

            Assert.AreEqual(expected, result, 1e-9,
                "При x > y должна использоваться ветка (f(x) - y)³ + arctan(f(x)).");
        }

        /// <summary>
        /// Тест 3: при y > x используется ветка d = (y - f(x))³ + arctan(f(x)).
        /// Проверяется с f(x) = eˣ.
        /// </summary>
        [TestMethod]
        public void ComputeD_WhenYGreaterThanX_UsesYGreaterBranch()
        {
            double x = 1.0, y = 5.0;
            double fx = Math.Exp(x);
            double expected = Math.Pow(y - fx, 3) + Math.Atan(fx);

            double result = MathCalculator.ComputeD(x, y, MathCalculator.FunctionType.Exp);

            Assert.AreEqual(expected, result, 1e-9,
                "При y > x должна использоваться ветка (y - f(x))³ + arctan(f(x)).");
        }


        /// <summary>
        /// Тест 1: при x = 0 функция y = cos²(0 - b) = cos²(-b), не зависит от a.
        /// Используются параметры по умолчанию: a=1.35, b=-6.25.
        /// </summary>
        [TestMethod]
        public void ComputeY_WhenXIsZero_ReturnsCosSqOfMinusB()
        {
            double x = 0, a = 1.35, b = -6.25;
            double expected = Math.Pow(Math.Cos(0.0 - b), 2);

            double result = MathCalculator.ComputeY(x, a, b);

            Assert.AreEqual(expected, result, 1e-10,
                "При x = 0 результат должен быть равен cos²(-b).");
        }

        /// <summary>
        /// Тест 2: табуляция с нулевым шагом dx должна выбросить
        /// <see cref="ArgumentException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Tabulate_WhenDxIsZero_ThrowsArgumentException()
        {
            MathCalculator.Tabulate(0, 10, 0, 1.35, -6.25);
        }

        /// <summary>
        /// Тест 3: табуляция с неверным знаком dx (x0 &lt; xk, но dx &lt; 0)
        /// должна выбросить <see cref="ArgumentException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Tabulate_WhenDxHasWrongSign_ThrowsArgumentException()
        {
            MathCalculator.Tabulate(0, 10, -0.5, 1.35, -6.25);
        }
    }
}
