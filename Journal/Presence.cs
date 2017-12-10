using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Journal
{
    public class Presence
    {
        public Student Student { get; set; }
        public bool IsPresent { get; set; }
        public ObservableCollection<int> Grades { get; set; }

        public string GradesString
        {
            get
            {
                return string.Join(", ", Grades);
            }
        }

        public double AverageGrade
        {
            get
            {
                if (Grades.Count > 0)
                {
                    return Math.Round(Grades.Average(), 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public Presence(){}

        public Presence(Student st, bool isPresent)
        {
            Student = st;
            IsPresent = isPresent;

            if (isPresent == false)
            {
                st.AbsencesNumber++;
            }
        }
    }
}
