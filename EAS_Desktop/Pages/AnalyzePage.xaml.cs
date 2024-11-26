using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using EAS_Desktop.Services;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using EAS_Hub.Services;
using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace EAS_Desktop.Pages;

public partial class AnalyzePage : Page
{
    public AnalyzePage()
    {
        InitializeComponent();
    }

    private async Task ShowEvent(int position = 0)
    {
        try
        {
            List<EventAnalyze> eventAnalyzes = await ManageService.GetEventAnalyzes(position);

            PlotModel model = new PlotModel();

            BarSeries barSeries = new();
            foreach (var eventAnalyze in eventAnalyzes)
            {
                barSeries.Items.Add(new BarItem(eventAnalyze.Count));
            }

            model.Axes.Add(new CategoryAxis()
            {
                ItemsSource = eventAnalyzes
                    .Select(c => c.Name)
                    .ToList(),
                Position = AxisPosition.Left
            });
            model.Series.Add(barSeries);
            EventPlot.Model = model;

            EventGrid.ItemsSource = null;
            EventGrid.ItemsSource = eventAnalyzes;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async Task ShowMap(int position = 0)
    {
        try
        {
            List<ModuleAnalyze> analyzes = await ManageService.GetModuleAnalyze(position);

            PlotModel model = new(){Title = "Ошибки"};
            BarSeries barSeries = new();
            foreach (var moduleAnalyze in analyzes)
            {
                barSeries.Items.Add(new()
                {
                    Value = moduleAnalyze.Errors
                });
            }

            model.Axes.Add(new CategoryAxis()
            {
                Position = AxisPosition.Left,
                ItemsSource = analyzes
                    .Select(c => c.Name)
                    .ToList()
            });

            model.Series.Add(barSeries);
            ErrorPlot.Model = model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageService.ShowError(e);
        }
    }

    private async void FilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            await ShowEvent(FilterComboBox.SelectedIndex);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void AnalyzePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }

    private async void AdaptFilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            await ShowMap(AdaptFilterComboBox.SelectedIndex);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageService.ShowError(exception);
        }
    }
}