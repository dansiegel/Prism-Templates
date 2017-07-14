using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Company.MobileApp.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public bool Done { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}