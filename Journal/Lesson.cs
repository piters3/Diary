using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Journal
{
    public class Lesson : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private double _averageGroup;

        public DateTime Date { get; set; }
        public List<Presence> PresenceList { get; set; }
        

        public double AverageGroup
        {
            get
            {
                return _averageGroup;
            }
            set
            {
                _averageGroup = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(AverageGroup)));
            }
        }
    }
}
