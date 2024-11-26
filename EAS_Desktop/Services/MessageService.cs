using System.Windows;

namespace EAS_Desktop.Services;

public class MessageService
{
    public static void ShowOk(string message) =>
        MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Asterisk);

    public static void ShowError(Exception exception) =>
        MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

    public static void ShowInfo(string message) =>
        MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

    public static void ShowWarning(string message) =>
        MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Warning);
}