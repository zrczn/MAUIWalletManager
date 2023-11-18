//using Android.Media;
using MauiTransaction.Views;

namespace MauiTransaction
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainMenu), typeof(MainMenu));
            Routing.RegisterRoute(nameof(EditTransaction), typeof(EditTransaction));
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        }
    }
}
