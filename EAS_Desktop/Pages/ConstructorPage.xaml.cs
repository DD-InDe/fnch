using System.IO;
using System.Windows;
using System.Windows.Controls;
using Aspose.Cells;
using EAS_Desktop.Services;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.EntityFrameworkCore;

namespace EAS_Desktop.Pages;

public partial class ConstructorPage : Page
{
    private List<NewMapModule> _modulesList;

    public ConstructorPage()
    {
        InitializeComponent();
    }

    private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var tempList = _modulesList
                .Where(c => c.IsChecked)
                .ToList();
            Employee employee = (Employee)EmployeeComboBox.SelectedItem;
            Position position = (Position)PosComboBox.SelectedItem;
            Department department = (Department)DepComboBox.SelectedItem;
            if (employee != null && position != null && department != null &&
                tempList.All(c => c.Employee != null))
            {
                NewAdaptationMap map = new()
                {
                    EmployeeId = employee.Id,
                    PositionId = position.Id,
                    DepartmentId = department.Id,
                    ModuleList = _modulesList
                        .Where(c => c is { IsChecked: true, Employee: not null })
                        .ToList()
                };

                bool isSuccess = await ManageService.AddMap(map);
                if (isSuccess)
                {
                    MessageService.ShowInfo("Данные добавлены");

                    string dir = "Адаптационные_программы";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    Workbook workbook = new Workbook();
                    Worksheet worksheet = workbook.Worksheets[0];
                    worksheet.Cells["A1"].Value = "Сотрудник";
                    worksheet.Cells["A2"].Value = "Должность";
                    worksheet.Cells["A3"].Value = "Подразделение";
                    worksheet.Cells["A4"].Value = "Модули";

                    worksheet.Cells["B1"].Value = employee.FullName;
                    worksheet.Cells["B2"].Value = position.Name;
                    worksheet.Cells["B3"].Value = department.Name;

                    for (int i = 4; i < map.ModuleList.Count + 4; i++)
                    {
                        worksheet.Cells[$"B{i}"].Value = map.ModuleList[i - 4].Module.Name;
                        worksheet.Cells[$"C{i}"].Value = map.ModuleList[i - 4].Employee.FullName;
                    }

                    workbook.Save(
                        dir +
                        $"/{department.Name}_{position.Name}_{employee.FullName}_{DateTime.Now.ToString("MM-dd-yy")}.xlsx",
                        SaveFormat.Xlsx);
                    NavigationService.GoBack();
                }
                else
                    MessageService.ShowWarning("Данные не добавлены");
            }
            else
                MessageService.ShowWarning("Заполните все поля и выберите хотя бы 1 модуль");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void ConstructorPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            EmployeeComboBox.ItemsSource = await Db.Context.Employees.ToListAsync();
            DepComboBox.ItemsSource = await Db.Context.Departments.ToListAsync();
            PosComboBox.ItemsSource = await Db.Context.Positions.ToListAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void PosComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            List<Employee> employees = await Db.Context.Employees.ToListAsync();
            _modulesList = new();
            List<Module> modules = await Db
                .Context.ModuleAccesses.Where(c => c.PositionId == ((Position)PosComboBox.SelectedItem).Id)
                .Select(c => c.Module)
                .ToListAsync();

            foreach (var module in modules)
                _modulesList.Add(new() { IsChecked = false, Module = module, Employees = employees });

            ModulesControl.ItemsSource = _modulesList;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    #region ModuleMove

    private void Down_OnClick(object sender, RoutedEventArgs e)
    {
        NewMapModule module = ((Button)sender).DataContext as NewMapModule;
        int movableIndex = _modulesList.IndexOf(module);
        if (movableIndex != _modulesList.Count - 1)
        {
            var temp = _modulesList[movableIndex + 1];
            _modulesList[movableIndex] = temp;
            _modulesList[movableIndex + 1] = module;
        }

        ModulesControl.ItemsSource = null;
        ModulesControl.ItemsSource = _modulesList;
        UpdateLayout();
    }

    private void Top_OnClick(object sender, RoutedEventArgs e)
    {
        NewMapModule module = ((Button)sender).DataContext as NewMapModule;
        int movableIndex = _modulesList.IndexOf(module);
        if (movableIndex != 0)
        {
            var temp = _modulesList[movableIndex - 1];
            _modulesList[movableIndex] = temp;
            _modulesList[movableIndex - 1] = module;
        }

        ModulesControl.ItemsSource = null;
        ModulesControl.ItemsSource = _modulesList;
        UpdateLayout();
    }

    #endregion
}