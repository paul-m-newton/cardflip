using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestUrho
{
    public class GameViewModel
    {
        public string Header { get; set; } = "HEADER";
        public string Footer { get; set; } = "FOOTER";
        public ObservableCollection<string> Properties { get; } = new ObservableCollection<string>(new List<string>{"Fred", "Flintstone"});

    }
}