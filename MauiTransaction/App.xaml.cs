using MauiTransaction.Data;

namespace MauiTransaction
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
