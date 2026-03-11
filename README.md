# Практическая работа №6.2

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-0078D4?style=for-the-badge&logo=windows&logoColor=white)
![MSTest](https://img.shields.io/badge/MSTest-FF6B35?style=for-the-badge&logo=microsoft&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)
![LiveCharts](https://img.shields.io/badge/LiveCharts-FF6B35?style=for-the-badge&logoColor=white)

Практическая работа №6 (Часть 2) по дисциплине **"Поддержка и тестирование программного обеспечения"**

**Автор:** Лобочкин М.В.  
**Преподаватель:** Аксенова Т.Г.

---

## Описание проекта

Расширение проекта из **практической работы №4** — десктопного WPF-приложения для вычисления трёх математических функций. Математическая логика вынесена в отдельную библиотеку `MathLogic` и покрыта автоматизированными unit-тестами на базе **MSTest**. Тестирование проводится методом **"белого ящика"** с использованием средств Microsoft Visual Studio.

### Основные возможности

- Полный оригинальный WPF-проект из практической работы №4 (без изменений)
- Математическая логика трёх функций вынесена в отдельную библиотеку `MathLogic`
- XML-документирование всех методов библиотеки
- Автоматизированные unit-тесты для функций `w`, `d` и `y`
- Проверка всех ветвей вычислений и граничных случаев
- Проверка выброса исключений при некорректных входных данных

---

## Вариант задания

**Вариант №4**

| Страница | Функция | Формула |
|---|---|---|
| Страница 1 | `w` | `w = \|cos(x) − cos(y)\|^(1 + 2·sin²(y)) · (1 + z + z²/2 + z³/3 + z⁴/4)` |
| Страница 2 | `d` | `d` — кусочная функция с ветвлением по `x`, `y` и виду `f(x)` |
| Страница 3 | `y` | `y = a·x³ + cos²(x³ − b)` — табуляция и график |

---

## Технологический стек

| Технология | Версия | Назначение |
|---|---|---|
| **C#** | 12 | Основной язык программирования |
| **.NET** | 8.0 (WPF) / 6.0 (тесты) | Платформа разработки |
| **WPF** | .NET 8 | Графический интерфейс |
| **LiveCharts.Wpf** | 0.9.7 | Построение интерактивных графиков |
| **MSTest** | 3.0.2 | Фреймворк unit-тестирования |
| **Microsoft.NET.Test.Sdk** | 17.5.0 | SDK для запуска тестов |
| **Visual Studio** | 2022 | IDE |

---

## Установка и запуск

### Требования

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/)
- ОС: Windows 10 / 11

### Клонирование репозитория

```bash
git clone https://github.com/ВашАккаунт/MathSolution.git
cd MathSolution
```

### Сборка и запуск

#### Через Visual Studio 2022

1. Распакуйте архив в папку, например `C:\Projects\MathSolution`
2. Откройте файл `MathSolution.sln`
3. Нажмите **Ctrl+Shift+B** для сборки всех проектов
4. Выберите `WpfMathApp` как стартовый проект и нажмите **F5** для запуска
5. Для запуска тестов: **Тест → Запустить все тесты**

#### Запуск тестов через командную строку

```bash
dotnet restore
dotnet test MathTests/MathTests.csproj
```

---

## Структура проекта

```
MathSolution/
│
├── WpfMathApp/                          # Оригинальный проект из практики №4
│   ├── Pages/
│   │   ├── Page1.xaml                   # UI страницы функции w
│   │   ├── Page1.xaml.cs                # Логика страницы функции w
│   │   ├── Page2.xaml                   # UI страницы функции d
│   │   ├── Page2.xaml.cs                # Логика страницы функции d
│   │   ├── Page3.xaml                   # UI страницы табуляции y + график
│   │   └── Page3.xaml.cs                # Логика табуляции y + график
│   ├── Images/
│   │   ├── formula1.png                 # Изображение формулы страницы 1
│   │   ├── formula2.png                 # Изображение формулы страницы 2
│   │   └── formula3.png                 # Изображение формулы страницы 3
│   ├── App.xaml                         # Глобальные стили приложения
│   ├── App.xaml.cs                      # Точка входа приложения
│   ├── MainWindow.xaml                  # Навигационная страница
│   ├── MainWindow.xaml.cs               # Логика навигации
│   └── WpfMathApp.csproj               # Файл проекта WPF
│
├── MathLogic/                           # Библиотека математической логики
│   ├── MathCalculator.cs                # Методы ComputeW, ComputeD, ComputeY, Tabulate
│   └── MathLogic.csproj                # Файл проекта библиотеки
│
├── MathTests/                           # Тестовый проект
│   ├── MathCalculatorTests.cs           # Unit-тесты для всех трёх функций
│   └── MathTests.csproj                # Файл тестового проекта (MSTest)
│
└── MathSolution.sln
```

---

## Описание библиотеки MathLogic

Класс `MathCalculator` находится в пространстве имён `MathLogic` и содержит математику всех трёх страниц, вынесенную из WPF-кода.

### Перечисление FunctionType

Определяет вид функции `f(x)` для страницы 2, соответствующий RadioButton в интерфейсе.

| Значение | Описание |
|---|---|
| `Sinh` | Гиперболический синус: `f(x) = sh(x)` |
| `Square` | Квадрат аргумента: `f(x) = x²` |
| `Exp` | Экспонента: `f(x) = eˣ` |

### Методы

| Метод | Страница | Описание |
|---|---|---|
| `ComputeW(x, y, z)` | 1 | Вычисляет функцию `w` |
| `ComputeFx(x, type)` | 2 | Вычисляет вспомогательную функцию `f(x)` |
| `ComputeD(x, y, type)` | 2 | Вычисляет функцию `d` с ветвлением по `x` и `y` |
| `ComputeY(x, a, b)` | 3 | Вычисляет значение `y` в одной точке |
| `Tabulate(x0, xk, dx, a, b)` | 3 | Выполняет табуляцию `y` по диапазону. Выбрасывает `ArgumentException` при некорректном `dx` |

---

## Тесты

Все тесты находятся в файле `MathCalculatorTests.cs` в классе `MathCalculatorTests`.

### Тесты функции w (Страница 1)

| Тест | Описание | Ожидаемый результат |
|---|---|---|
| `ComputeW_WhenXEqualsY_ReturnsZero` | x = y, основание равно нулю | `w = 0` |
| `ComputeW_WhenAllZero_ReturnsZero` | x = 0, y = 0, z = 0 | `w = 0` |
| `ComputeW_WithKnownValues_ReturnsExpectedResult` | x=0, y=π/2, z=1 | Совпадение с ручным расчётом |

### Тесты функции d (Страница 2)

| Тест | Описание | Ожидаемый результат |
|---|---|---|
| `ComputeD_WhenXEqualsY_UsesEqualBranch` | x = y, f(x) = x² | `d = (y + f(x))³ + 0.5` |
| `ComputeD_WhenXGreaterThanY_UsesXGreaterBranch` | x > y, f(x) = sh(x) | `d = (f(x) − y)³ + arctan(f(x))` |
| `ComputeD_WhenYGreaterThanX_UsesYGreaterBranch` | y > x, f(x) = eˣ | `d = (y − f(x))³ + arctan(f(x))` |

### Тесты функции y (Страница 3)

| Тест | Описание | Ожидаемый результат |
|---|---|---|
| `ComputeY_WhenXIsZero_ReturnsCosSqOfMinusB` | x = 0, a=1.35, b=−6.25 | `y = cos²(−b)` |
| `Tabulate_WhenDxIsZero_ThrowsArgumentException` | dx = 0 | Исключение `ArgumentException` |
| `Tabulate_WhenDxHasWrongSign_ThrowsArgumentException` | x₀ < xk, но dx < 0 | Исключение `ArgumentException` |

---

## Возможные проблемы

- Открывайте `MathSolution.sln` только после полной распаковки архива — не запускайте файл прямо из ZIP
- При запуске тестов убедитесь, что проект `MathTests` ссылается на `MathLogic` через `ProjectReference`
- Для сборки `WpfMathApp` требуется .NET 8.0 SDK, для тестов достаточно .NET 6.0 SDK

---

## Авторы

| Имя | Роль |
|---|---|
| **Лобочкин М.В.** | Разработчик |

---

## Контакты

По вопросам работы проекта обращайтесь к автору или преподавателю.

**Преподаватель:** Аксенова Т.Г. — [@TGAksenova](https://github.com/TGAksenova)

---

<div align="center">
  <sub>Практическая работа №6 — Создание автоматизированных Unit-тестов. Часть 2</sub>
</div>
