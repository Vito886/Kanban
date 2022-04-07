using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Platform;
using System.Reflection;
using System.IO;

namespace Kanban.Models
{
    public class TaskCard : INotifyPropertyChanged
    {
        string name;
        string description;
        Bitmap image;
        public event PropertyChangedEventHandler PropertyChanged;
        public TaskCard(string _status, string _name = "Новая задача", string _description = "Описание задачи",
            string _imagePath = @"Assets\cat.jpg")
        {
            Status = _status;
            name = _name;
            description = _description;
            ImagePath = _imagePath;

            if (_imagePath == @"Assets\cat.jpg")
            {
                string directory = Directory.GetCurrentDirectory();
                directory = directory.Remove(directory.LastIndexOf("bin"));
                _imagePath = directory + _imagePath;
            }
            image = new Bitmap(_imagePath);
        }

        private void PropertyChangeNotification([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public string Status { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChangeNotification();
            }
        }
        public string ImagePath { get; set; }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                PropertyChangeNotification();
            }
        }

        public Bitmap Image
        {
            get => image;
            set
            {
                image = value;
                PropertyChangeNotification();
            }
        }
    }
}
