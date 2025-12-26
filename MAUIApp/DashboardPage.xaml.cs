namespace MAUIApp;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;
    private Timer _timer;

    public DashboardPage()
    {
        InitializeComponent();
        _viewModel = new DashboardViewModel();
        BindingContext = _viewModel;
        StartAutoRefresh();
    }

    private void StartAutoRefresh()
    {
        _timer = new Timer(UpdateData, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
    }

    private void UpdateData(object state)
    {
        MainThread.BeginInvokeOnMainThread(() => _viewModel.UpdateStats());
    }



    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        _timer?.Dispose();
        await Shell.Current.GoToAsync("//LoginPage");
    }
}