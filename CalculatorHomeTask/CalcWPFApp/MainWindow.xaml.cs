using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _inputValue = "0";
        private double _result;
        private double _previousNumber = 0.0f;
        private Operations _operation = Operations.None;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            _inputValue = ((Button)sender).Content.ToString();
            UpdateUI();
        }

        private void UpdateUI()
        {
            if(_inputValue.Equals(".") && !resultLabel.Content.ToString().Contains('.'))
            {
                resultLabel.Content += _inputValue;
                return;
            }
            if (resultLabel.Content.ToString().Equals("0"))
            {
                resultLabel.Content = _inputValue;
            }
            else if(!_inputValue.Equals("."))
            {
                resultLabel.Content += _inputValue;
            }
        }

        private void UpdateUI(string value)
        {
            resultLabel.Content = value;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var operation = ((Button)sender).Content.ToString();
            switch (operation)
            {
                case "C":
                    DeleteLastContent();
                    break;
                case "+/-":
                    SwitchSign();
                    break;
                case "-":
                    _operation = Operations.Subtract;
                    ChangePreviousNumber();
                    break;
                case "+":
                    _operation = Operations.Add;
                    ChangePreviousNumber();
                    break;
                case "/":
                    _operation = Operations.Divide;
                    ChangePreviousNumber();
                    break;
                case "=":
                    Calculate();
                    break;
                case "X":
                    _operation = Operations.Multiply;
                    ChangePreviousNumber();
                    break;
                case "%":
                    CalculatePercentage();
                    break;
                case "x²":
                    CalculateSquare();
                    break;
                case "√": 
                    CalculateSquareRoot();
                    break;


            }
        }

        private void ChangePreviousNumber()
        {
            _previousNumber = Convert.ToDouble(resultLabel.Content.ToString(), CultureInfo.InvariantCulture);
            resultLabel.Content = "0";
        }

        private void Calculate()
        {
            string content = resultLabel.Content.ToString();
            double numericContent = Convert.ToDouble(content, CultureInfo.InvariantCulture);
            switch (_operation)
            {
                case Operations.None:
                    break;
                case Operations.Add:
                    var currentNumber = Convert.ToDouble(resultLabel.Content, CultureInfo.InvariantCulture);
                    var result = MathOperations.Sum(_previousNumber, currentNumber);
                    UpdateUI(result);
                    break;
                case Operations.Subtract:
                    var current = Convert.ToDouble(resultLabel.Content, CultureInfo.InvariantCulture);
                    var res = MathOperations.Substract(_previousNumber, current);
                    UpdateUI(res);
                    break;
                case Operations.Divide:
                    current = Convert.ToDouble(resultLabel.Content, CultureInfo.InvariantCulture);
                    res = MathOperations.Divide(_previousNumber, current);
                    UpdateUI(res);
                    break;
                case Operations.Multiply:
                    current = Convert.ToDouble(resultLabel.Content, CultureInfo.InvariantCulture);
                    res = MathOperations.Multiply(_previousNumber, current);
                    UpdateUI(res);
                    break;

                
            }
        }

        private void CalculateSquareRoot()
        {
            if (resultLabel.Content == null || string.IsNullOrWhiteSpace(resultLabel.Content.ToString()))
                return;

            if (!double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
            {
                MessageBox.Show("Ошибка: некорректное число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (number < 0)
            {
                MessageBox.Show("Ошибка: нельзя вычислить корень из отрицательного числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double squareRoot = Math.Sqrt(number); // Вычисляем квадратный корень
            resultLabel.Content = squareRoot.ToString(CultureInfo.InvariantCulture);
        }

        private void CalculateSquare()
        {
            if (resultLabel.Content == null || string.IsNullOrWhiteSpace(resultLabel.Content.ToString()))
                return;

            if (!double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
            {
                MessageBox.Show("Ошибка: некорректное число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double squaredValue = Math.Pow(number, 2); // Возводим в квадрат
            resultLabel.Content = squaredValue.ToString(CultureInfo.InvariantCulture);
        }


        private void CalculatePercentage()
        {
            if (resultLabel.Content == null || string.IsNullOrWhiteSpace(resultLabel.Content.ToString()))
                return;

            if (!double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double currentNumber))
            {
                MessageBox.Show("Ошибка: некорректное число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Сохраняем текущий номер в процентном виде, но не обновляем UI
            currentNumber = currentNumber / 100.0;
        }

        private void SwitchSign()
        {
            double number = Convert.ToDouble(resultLabel.Content.ToString(), CultureInfo.InvariantCulture);
            number *= -1;
            resultLabel.Content = number.ToString();
        }

        private void DeleteLastContent()
        {
            var contentString = resultLabel.Content.ToString();
            if (contentString.Equals("0"))
            {
                return;
            }
            if (contentString.Length > 0)
            {
                var resultString = resultLabel.Content.ToString();
                resultLabel.Content = resultString.Remove(resultString.Length - 1);
            }
            if(resultLabel.Content.ToString().Length == 0)
            {
                resultLabel.Content = "0";
            }
        }
    }
}