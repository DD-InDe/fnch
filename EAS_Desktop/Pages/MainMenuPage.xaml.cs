using System.Windows;
using System.Windows.Controls;

namespace EAS_Desktop.Pages;

public partial class MainMenuPage : Page
{
    public MainMenuPage()
    {
        InitializeComponent();
    }

    private void ModulesButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ModulesPage());

    private void ConstructorButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ConstructorPage());

    private void AnalyzeButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new AnalyzePage());
}