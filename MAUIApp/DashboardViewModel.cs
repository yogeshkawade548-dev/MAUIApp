using System.ComponentModel;

namespace MAUIApp;

public class DashboardViewModel : INotifyPropertyChanged
{
    private int _users = 1234;
    private int _orders = 567;
    private decimal _revenue = 12345;
    private int _products = 89;

    public int Users
    {
        get => _users;
        set { _users = value; OnPropertyChanged(); }
    }

    public int Orders
    {
        get => _orders;
        set { _orders = value; OnPropertyChanged(); }
    }

    public decimal Revenue
    {
        get => _revenue;
        set { _revenue = value; OnPropertyChanged(); }
    }

    public int Products
    {
        get => _products;
        set { _products = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void UpdateStats()
    {
        var random = new Random();
        Users += random.Next(-10, 20);
        Orders += random.Next(-5, 15);
        Revenue += random.Next(-100, 500);
        Products += random.Next(-2, 5);
    }
}