using MauiTransaction.Data;
using MauiTransaction.Httph;
using System.Transactions;
using MauiTransaction.Models;

using Transaction = MauiTransaction.Models.Transaction;

namespace MauiTransaction.Views;

public partial class MainMenu : ContentPage
{
    private readonly ICRUDService _crudService;


    public MainMenu(ICRUDService crudService)
    {
        _crudService = crudService;
        InitializeItemSource();
        InitializeComponent();
    }

    private async void InitializeItemSource()
    {
        IEnumerable<TransactionDTO> dtos = await _crudService.GetTransactionsAsync();
        ListOfTransactions.ItemsSource = dtos;
    }
    

    //public async void Tester()
    //{
        //var elements = await _crudService.GetTransactionsAsync();
        //var singleElement = await _crudService.GetTransactionAsync(1);
        //var categories = await _crudService.GetCategoriesAsync();
        //var createTransaction = await _crudService.SaveTransactionAsync(3, newTransaction);
        //var editTransaction = await _crudService.SaveTransactionAsync(2, modifyTransaction);
    //}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NewTransaction)}");
    }

    private async void ListOfTransactions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(ListOfTransactions.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Id={((TransactionDTO)ListOfTransactions.SelectedItem).Id}");
        } 
    }

    private void ListOfTransactions_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        ListOfTransactions.SelectedItem = null;
    }
}