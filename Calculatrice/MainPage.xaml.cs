using System.Globalization;

namespace Calculatrice;

public partial class MainPage : ContentPage
{
    double storedValue = 0;
    string currentOperator = "";
    bool isNewNumber = true;

    public MainPage()
    {
        InitializeComponent();
    }

    void OnNumber(object sender, EventArgs e)
    {
        var b = (Button)sender;

        if (isNewNumber)
        {
            Display.Text = b.Text;
            isNewNumber = false;
        }
        else
        {
            Display.Text = Display.Text == "0" ? b.Text : Display.Text + b.Text;
        }
    }

    void OnDecimal(object sender, EventArgs e)
    {
        if (!Display.Text.Contains("."))
            Display.Text += ".";
    }

    void OnOperator(object sender, EventArgs e)
    {
        storedValue = double.Parse(Display.Text, CultureInfo.InvariantCulture);
        currentOperator = ((Button)sender).Text;
        isNewNumber = true;
    }

    void OnEqual(object sender, EventArgs e)
    {
        double currentValue = double.Parse(Display.Text, CultureInfo.InvariantCulture);
        double result = currentValue;

        try
        {
            result = currentOperator switch
            {
                "+" => storedValue + currentValue,
                "−" => storedValue - currentValue,
                "×" => storedValue * currentValue,
                "÷" => currentValue == 0 ? throw new Exception() : storedValue / currentValue,
                _ => currentValue
            };

            Display.Text = result.ToString(CultureInfo.InvariantCulture);
            storedValue = result;
            isNewNumber = true;
        }
        catch
        {
            Display.Text = "Erreur";
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        Display.Text = "0";
        storedValue = 0;
        currentOperator = "";
        isNewNumber = true;
    }

    void OnSign(object sender, EventArgs e)
    {
        double v = double.Parse(Display.Text, CultureInfo.InvariantCulture);
        Display.Text = (-v).ToString(CultureInfo.InvariantCulture);
    }

    void OnPercent(object sender, EventArgs e)
    {
        double v = double.Parse(Display.Text, CultureInfo.InvariantCulture);
        Display.Text = (v / 100).ToString(CultureInfo.InvariantCulture);
    }

    void OnInverse(object sender, EventArgs e)
    {
        double v = double.Parse(Display.Text, CultureInfo.InvariantCulture);
        if (v == 0)
            Display.Text = "Erreur";
        else
            Display.Text = (1 / v).ToString(CultureInfo.InvariantCulture);
    }
}
