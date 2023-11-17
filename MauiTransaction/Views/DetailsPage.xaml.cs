using System.Diagnostics;
using System.Transactions;
using MauiTransaction.Data;
using MauiTransaction.Models;

namespace MauiTransaction.Views;

[QueryProperty(nameof(TransactionId), "Id")]
public partial class DetailsPage : ContentPage
{
	private readonly ICRUDService _crudService;
    private TransactionDTO _transaction;

    public string TransactionId {
		set
		{
			InitializeTransaction(value);
		}
	}

    public DetailsPage(ICRUDService cRUDService)
	{
        _crudService = cRUDService;
        InitializeComponent();
	}

	private async void InitializeTransaction(string value)
	{
		try
		{
            int id = Int32.Parse(value);
            _transaction = await _crudService.GetTransactionAsync(id);

            TitleLabel.Text = _transaction.Title;
			ValueLabel.Text = _transaction.Value.ToString();
			DateLabel.Text = _transaction.Date.ToString();
			CategoryLabel.Text = _transaction.CategoryName;
        }
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }
}