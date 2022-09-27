
using System;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		OnClear(this, null);
	}

	string currentEntry = "";
	int currentState = 1;
	string mathOperator;
	double firstNumber, secondNumer;
	string decimalFormat = "N0";

	void OnSelectNumber(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		string pressed = button.Text;

		currentEntry += pressed;

		if((this.resultText.Text == "0" && pressed =="0")
			|| (currentEntry.Length <= 1 && pressed != "0")
			|| currentState < 0)
		{
			this.resultText.Text = "";
			if(currentState < 0)
				currentState *= -1;
		}

		if(pressed == "." && decimalFormat != "N2")
		{
			decimalFormat = "N2";
		}

		this.resultText.Text += pressed;
	}
	void OnSelectOperator(object sender, EventArgs e)
	{
		LockNumberValue(resultText.Text);

		currentState = -2;
		Button button = (Button)sender;
		string pressed = button.Text;
		mathOperator = pressed;
	}
	private void LockNumberValue(string text)
	{
		double number;
		if(double.TryParse(text, out number))
		{
			if(currentState == 1)
			{
				firstNumber = number;
			}
			else
			{
				secondNumer = number;
			}
			currentEntry = string.Empty;
		}
	}
	void OnClear(object sender, EventArgs e)
	{
		firstNumber = 0;
		secondNumer = 0;
		currentState = 1;
		decimalFormat = "N0";
		this.resultText.Text = "0";
		currentEntry = string.Empty;

	}
	void OnCalculate(object sender, EventArgs e)
	{
		if(currentState == 2)
		{
			if (secondNumer == 0)
				LockNumberValue(resultText.Text);

			double result = Calculator.Calculate(firstNumber, secondNumer, mathOperator);

			this.CurrentCalculation.Text = result.ToTrimmedString(decimalFormat);
			firstNumber = result;
			secondNumer = 0;
			currentState = -1;
			currentEntry = string.Empty;
		}
	}

	void OnNegative(object sender, EventArgs e)
	{
		if(currentState == 1)
		{
			secondNumer = -1;
			mathOperator = "*";
			currentState = 2;
			OnCalculate(this, null);
		}
	}
	void OnPercentage(object sender, EventArgs e)
	{
		if(currentState == 1)
		{
			LockNumberValue(resultText.Text);
			decimalFormat = "N2";
			secondNumer = 0.01;
			mathOperator = "*";
			currentState = 2;
			OnCalculate(this, null);
		}
	}
}

