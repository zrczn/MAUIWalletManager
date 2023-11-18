using MauiTransaction.Data;
using MauiTransaction.Models;
using System.Diagnostics;
//using Windows.Storage.Pickers;

namespace MauiTransaction.Views;

[QueryProperty(nameof(TransactionId), "Id")]
public partial class EditTransaction : ContentPage
{
    private TransactionDTO _transactionDTO;
    private readonly ICRUDService _crudService;
    private Transaction _transaction;

    public string TransactionId
    {
        set
        {
            SetTransaction(value);
        }
    }


    public EditTransaction(ICRUDService cRUDService)
    {
        _crudService = cRUDService;
        InitializeComponent();
    }

    private async void SetTransaction(string id)
    {
        try
        {
            int val = Int32.Parse(id);

            if (val == 0)
            {
                _transactionDTO = new TransactionDTO()
                {
                    Title = string.Empty
                };
            }
            else
            {
                _transactionDTO = await _crudService.GetTransactionAsync(val);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        _transaction = new Transaction()
        {
            Category = new Category
            {
                Id = 0,
                Name = "Harnaœ"
            },
            Value = _transactionDTO.Value,
            Title = _transactionDTO.Title,
        };

        InitAsyncCategories();
        ValueEntry.Text = _transaction.Value.ToString();
        TitleEntry.Text = _transaction.Title.ToString();
    }

    private async void InitAsyncCategories()
    {
        var categories = (System.Collections.IList)await _crudService.GetCategoriesAsync();
        picker.ItemsSource = categories;
    }

    private async void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(MainMenu)}");
    }

    private async void Button_Clicked_Saved(object sender, EventArgs e)
    {
        _transaction.Title = TitleEntry.Text;
        _transaction.Value = decimal.Parse(ValueEntry.Text);
        _transaction.Date = DateTime.Now;
        _transaction.CategoryId = ((Category)picker.SelectedItem).Id;

        await _crudService.SaveTransactionAsync(_transactionDTO.Id, _transaction);


        await Shell.Current.GoToAsync($"//{nameof(MainMenu)}");

    }
}