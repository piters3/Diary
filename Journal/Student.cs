using System.ComponentModel;

namespace Journal
{
    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public int Index { get; set; }
        private string _name;
        private string _surname;
        private int _absencesNumber;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        public int AbsencesNumber
        {
            get
            {
                return _absencesNumber;
            }
            set
            {
                _absencesNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(AbsencesNumber)));
            }
        }
    }
}
