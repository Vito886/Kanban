using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Kanban.Models
{
    public class ProcessingFile
    {
        public static void SaveFile(string path, List<ObservableCollection<TaskCard>> tasks)
        {
            File.WriteAllText(path, "");
            List<string> data = new List<string>();
            foreach (ObservableCollection<TaskCard> taskList in tasks)
            {
                foreach (TaskCard task in taskList)
                {
                    data.Add(task.Status);
                    data.Add(task.Name);
                    data.Add(task.Description);
                    data.Add(task.ImagePath);
                }
                data.Add("");
            }
            File.WriteAllLines(path, data);
        }

        public static List<ObservableCollection<TaskCard>> LoadFile(string path)
        {
            List<ObservableCollection<TaskCard>> tasks = new List<ObservableCollection<TaskCard>>
            {
                new ObservableCollection<TaskCard>(),
                new ObservableCollection<TaskCard>(),
                new ObservableCollection<TaskCard>()
            };

            StreamReader file = new StreamReader(path);

            try
            {
                for (int i = 0; i < tasks.Count(); i++)
                {
                    ObservableCollection<TaskCard> tmp = new ObservableCollection<TaskCard>();
                    while (true)
                    {
                        string status = file.ReadLine();
                        if (status == "")
                            break;
                        string name = file.ReadLine();
                        string description = file.ReadLine();
                        string imagePath = file.ReadLine();

                        tmp.Add(new TaskCard(status, name, description, imagePath));
                    }
                    tasks[i] = tmp;
                }
                file.Close();
                return tasks;
            }
            catch
            {
                file.Close();
                return new List<ObservableCollection<TaskCard>>
                {
                    new ObservableCollection<TaskCard>(),
                    new ObservableCollection<TaskCard>(),
                    new ObservableCollection<TaskCard>()
                };
            }
        }
    }
}
