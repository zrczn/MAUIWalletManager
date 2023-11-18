using MauiTransaction.Data;
using MauiTransaction.Httph;
using System.Transactions;
using MauiTransaction.Models;

using Transaction = MauiTransaction.Models.Transaction;
using System.Collections.ObjectModel;

namespace MauiTransaction.Views;

public partial class MainMenu : ContentPage
{
    private readonly ICRUDService _crudService;


    public MainMenu(ICRUDService crudService)
    {
        _crudService = crudService;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        IEnumerable<TransactionDTO> dtos = await _crudService.GetTransactionsAsync();
        IEnumerable<TransactionRender> toRender = new ObservableCollection<TransactionRender>(dtos.Select(x => new TransactionRender()
        {
            CategoryName = x.CategoryName,
            Date = x.Date,
            Id = x.Id,
            Title = x.Title,
            Value = x.Value,
            Image = x.Value >= 0 ? "arrow_up.png" : "arrow_down.png"
        }));

        ListOfTransactions.ItemsSource = toRender;

        decimal totalval = await _crudService.GetTotal(3);
        decimal incomeval = await _crudService.GetTotal(1);
        decimal outcomeval = await _crudService.GetTotal(2);

        TotalLabel.Text = totalval.ToString();
        IncomeLabel.Text = $"+{incomeval.ToString()}";
        OutcomeLabel.Text = $"{outcomeval.ToString()}";
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(EditTransaction)}?Id=0");
    }

    private async void ListOfTransactions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (ListOfTransactions.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Id={((TransactionDTO)ListOfTransactions.SelectedItem).Id}");
        }
    }

    private void ListOfTransactions_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        ListOfTransactions.SelectedItem = null;
    }
}