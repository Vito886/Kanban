using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using Kanban.Models;

namespace Kanban.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ObservableCollection<TaskCard> planned;
        ObservableCollection<TaskCard> processing;
        ObservableCollection<TaskCard> ended;

        public MainWindowViewModel()
        {
            planned = new ObservableCollection<TaskCard>();
            processing = new ObservableCollection<TaskCard>();
            ended = new ObservableCollection<TaskCard>();
        }

        public ObservableCollection<TaskCard> Planned
        {
            get => planned;
            set
            {
                this.RaiseAndSetIfChanged(ref planned, value);
            }
        }

        public ObservableCollection<TaskCard> Processing
        {
            get => processing;
            set
            {
                this.RaiseAndSetIfChanged(ref processing, value);
            }
        }

        public ObservableCollection<TaskCard> Ended
        {
            get => ended;
            set
            {
                this.RaiseAndSetIfChanged(ref ended, value);
            }
        }

        public void AddTask(string taskType)
        {
            switch (taskType)
            {
                case "Plan":
                    Planned.Add(new TaskCard("Plan"));
                    break;
                case "Processing":
                    Processing.Add(new TaskCard("Processing"));
                    break;
                case "Ended":
                    Ended.Add(new TaskCard("Ended"));
                    break;
            }
        }

        public void SaveFile(string path)
        {
            List<ObservableCollection<TaskCard>> tasks = new List<ObservableCollection<TaskCard>> { Planned, Processing, Ended };
            ProcessingFile.SaveFile(path, tasks);
        }

        public void LoadFile(string path)
        {
            List<ObservableCollection<TaskCard>> tasks = ProcessingFile.LoadFile(path);
            Planned = tasks[0];
            Processing = tasks[1];
            Ended = tasks[2];
        }
    }
}
