using System.Windows;
using System.Windows.Controls;
using EAS_Desktop.Services;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.EntityFrameworkCore;

namespace EAS_Desktop.Pages;

public partial class ModulesPage : Page
{
    public ModulesPage()
    {
        InitializeComponent();
    }

    private async Task LoadData(int id)
    {
        try
        {
            List<ModuleForHr> modules = await ManageService.GetModules();
            if (id != 0)
                modules = modules
                    .Where(c => c.Positions.Any(p => p.Id == id))
                    .ToList();

            ModuleGrid.ItemsSource = modules;
            if (modules.Count == 0) MessageService.ShowInfo("Результатов нет");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageService.ShowError(e);
        }
    }

    private async void ModulesPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Position> positions = new() { new() { Name = "Все" } };
            positions.AddRange(await Db.Context.Positions.ToListAsync());
            PositionComboBox.ItemsSource = positions;
            PositionComboBox.SelectedIndex = 0;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void PositionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => await
        LoadData(((Position)PositionComboBox.SelectedItem ?? new()).Id);

    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new AddModulePage());
}