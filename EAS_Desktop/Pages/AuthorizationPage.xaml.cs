using System.Windows;
using System.Windows.Controls;
using EAS_Desktop.Services;
using EAS_Hub.DbModels;
using EAS_Hub.Services;

namespace EAS_Desktop.Pages;

public partial class AuthorizationPage : Page
{
    public AuthorizationPage()
    {
        InitializeComponent();
    }

    private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password))
            {
                Employee? employee = await AuthorizationService.LogIn(login, password);
                if (employee != null)
                {
                    if (employee.PositionId is 6 or 7)
                    {
                        MessageService.ShowOk("Добро пожаловать!");
                        NavigationService.Navigate(new MainMenuPage());
                    }
                    else
                        MessageService.ShowWarning("У вас нет доступа");
                }
                else
                    MessageService.ShowWarning("Пользователь не найден");
            }
            else
                MessageService.ShowWarning("Заполните все поля");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }
}