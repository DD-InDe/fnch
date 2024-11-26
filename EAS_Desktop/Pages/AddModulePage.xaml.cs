using System.Windows;
using System.Windows.Controls;
using EAS_Desktop.Services;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.EntityFrameworkCore;
using Xceed.Wpf.Toolkit.Primitives;

namespace EAS_Desktop.Pages;

public partial class AddModulePage : Page
{
    public AddModulePage()
    {
        InitializeComponent();
    }

    private async void AddModulePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Employee> employees = await Db.Context.Employees.ToListAsync();
            DevelopersComboBox.ItemsSource = employees;
            AccessorsComboBox.ItemsSource = employees;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private void AccessorsComboBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
    {
        MainComboBox.ItemsSource = AccessorsComboBox.SelectedItems;
    }

    private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string daysS = DaysBox.Text;
            string name = NameBox.Text;
            var developers = DevelopersComboBox.SelectedItems;
            var accessors = AccessorsComboBox.SelectedItems;
            Employee? main = (Employee)MainComboBox.SelectedItem;
            int.TryParse(daysS, out var days);

            if (!String.IsNullOrEmpty(name) && days != 0 && developers.Count != 0 &&
                accessors.Count != 0 && main != null)
            {
                List<Employee> eAccessors = new();
                List<Employee> eDevelopers = new();

                foreach (var developer in developers)
                    eDevelopers.Add((Employee)developer);
                foreach (var accessor in accessors)
                    eAccessors.Add((Employee)accessor);

                NewModule newModule = new()
                {
                    Name = name,
                    Deadline = days,
                    Accessors = eAccessors,
                    Developers = eDevelopers,
                    Main = main
                };

                bool isSuccess = await ManageService.AddModule(newModule);

                if (isSuccess)
                {
                    MessageService.ShowInfo("Данные добавлены");
                    NavigationService.GoBack();
                }
                else
                    MessageService.ShowWarning("Данные не добавлены");
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

    private void AccessorsComboBox_OnItemSelectionChanging(object? sender, ItemSelectionChangingEventArgs e)
    {
        if (AccessorsComboBox.SelectedItems.Count == 3 && e.NewIsSelected) e.Cancel = true;
    }
}