using System.Text.RegularExpressions;
using MAUIApp.Services;

namespace MAUIApp;

public partial class RegisterPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public RegisterPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        bool isValid = ValidateInputs();
        
        if (isValid)
        {
            bool registerSuccess = await _databaseService.RegisterUserAsync(NameEntry.Text, EmailEntry.Text, PasswordEntry.Text);
            if (registerSuccess)
            {
                await DisplayAlert("Success", "Registration successful! Please login.", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await DisplayAlert("Error", "Email already exists", "OK");
            }
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private bool ValidateInputs()
    {
        bool isValid = true;

        // Name validation
        if (NameEntry == null || string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            if (NameError != null)
            {
                NameError.Text = "Name is required";
                NameError.IsVisible = true;
            }
            isValid = false;
        }
        else if (NameError != null)
        {
            NameError.IsVisible = false;
        }

        // Email validation
        if (EmailEntry == null || string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            if (EmailError != null)
            {
                EmailError.Text = "Email is required";
                EmailError.IsVisible = true;
            }
            isValid = false;
        }
        else if (!IsValidEmail(EmailEntry.Text))
        {
            if (EmailError != null)
            {
                EmailError.Text = "Invalid email format";
                EmailError.IsVisible = true;
            }
            isValid = false;
        }
        else if (EmailError != null)
        {
            EmailError.IsVisible = false;
        }

        // Password validation
        if (PasswordEntry == null || string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            if (PasswordError != null)
            {
                PasswordError.Text = "Password is required";
                PasswordError.IsVisible = true;
            }
            isValid = false;
        }
        else if (PasswordEntry.Text.Length < 6)
        {
            if (PasswordError != null)
            {
                PasswordError.Text = "Password must be at least 6 characters";
                PasswordError.IsVisible = true;
            }
            isValid = false;
        }
        else if (PasswordError != null)
        {
            PasswordError.IsVisible = false;
        }

        return isValid;
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}