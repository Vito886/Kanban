using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using Kanban.Models;
using Kanban.ViewModels;
using Avalonia.Media.Imaging;
namespace Kanban.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("NewBtn").Click += delegate
            {

                var context = this.DataContext as MainWindowViewModel;
                context.Planned.Clear();
                context.Processing.Clear();
                context.Ended.Clear();
            };

            this.FindControl<MenuItem>("LoadBtn").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.LoadFile(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("SaveBtn").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.SaveFile(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("ExitBtn").Click += delegate
            {
                Close();
            };
        }

        public async void AddImage(object sender, RoutedEventArgs e)
        {
            TaskCard task = (TaskCard)((Button)sender).DataContext;
            var taskPath = new OpenFileDialog()
            {
                Title = "Search File",
                Filters = null
            }.ShowAsync((Window)this.VisualRoot);

            string[]? filePath = await taskPath;

            if (filePath != null)
            {
                task.ImagePath = filePath[0];
                task.Image = new Bitmap(filePath[0]);
            }
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            TaskCard task = (TaskCard)((Button)sender).DataContext;
            if (context != null)
            {
                switch (task.Status)
                {
                    case "Plan":
                        context.Planned.Remove(task);
                        break;
                    case "Processing":
                        context.Processing.Remove(task);
                        break;
                    case "Ended":
                        context.Ended.Remove(task);
                        break;
                }
            }
        }

        private async void AboutPage(object control, RoutedEventArgs arg)
        {
            await new Window1().ShowDialog((Window)this.VisualRoot);
        }
    }
}
