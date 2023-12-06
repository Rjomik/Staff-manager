using GrpcApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class MainWindowViewModel
    {
        ObservableCollection<Worker> Workers = new ObservableCollection<Worker>();

        Worker selectedWorker;
        public Worker SelectedWorker { get => selectedWorker; set => selectedWorker = value; }

    }
}
