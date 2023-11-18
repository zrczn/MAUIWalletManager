using System.Diagnostics;
using System.Transactions;
using MauiTransaction.Data;
using MauiTransaction.Models;
//using Windows.ApplicationModel.Activation;

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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(MainMenu)}");
    }

    private async void Button_Cliced_Delete(object sender, EventArgs e)
    {
		bool ans = await DisplayAlert("Warning","You are about to delete choosen Transaction\nAre you sure?", "Yes", "No");

		if (ans)
		{
			await _crudService.DeleteTransactionAsync(_transaction.Id);
			await Shell.Current.GoToAsync("..");
		}
    }

    private async void Button_Clicked_Edit(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(EditTransaction)}?Id={_transaction.Id}");
    }
}