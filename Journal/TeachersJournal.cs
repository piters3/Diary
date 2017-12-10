using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Journal
{
    public class TeachersJournal
    {
        public ObservableCollection<Student> StudentsList { get; set; }
        public ObservableCollection<Lesson> Lessons { get; set; }
        public ObservableCollection<DateTime> LessonDates { get; set; }

        public TeachersJournal()
        {
            StudentsList = new ObservableCollection<Student>()
            {
                //new Student {Index =1, Name = "Piotr", Surname="Strzelecki"},
                //new Student {Index =2, Name = "Mateusz", Surname="Szpinda"},
                //new Student {Index =3, Name = "Piotr", Surname="Sobiborowicz"},
                //new Student {Index =4, Name = "Agata", Surname="Szarpak"},
                //new Student {Index =5, Name = "Daniel", Surname="Szlaski"},
            };


            Lessons = new ObservableCollection<Lesson>();
            LessonDates = new ObservableCollection<DateTime>(Lessons.Select(l => l.Date).ToList());

            //FillLessons();
            //AddLesson(new DateTime(2017, 08, 05));           
        }

        public void FillLessons()
        {
            var firstLesson = new Lesson
            {
                Date = new DateTime(2017, 01, 01),
                PresenceList = new List<Presence>()
                {
                    new Presence(StudentsList[0], true) { Grades = new ObservableCollection<int> { 3, 5, 4 }  },
                    new Presence(StudentsList[1], true) { Grades = new ObservableCollection<int> {2, 2, 3} },
                    new Presence(StudentsList[2], true) { Grades = new ObservableCollection<int> {3, 5, 4} },
                    new Presence(StudentsList[3], false) { Grades = new ObservableCollection<int> {2, 5, 5} },
                    new Presence(StudentsList[4], true) { Grades = new ObservableCollection<int> {2,3} },
                },
            };
            Lessons.Add(firstLesson);
            LessonDates.Add(firstLesson.Date);

            var secondLesson = new Lesson
            {
                Date = new DateTime(2017, 03, 22),
                PresenceList = new List<Presence>()
                {
                    new Presence(StudentsList[0], false) { Grades = new ObservableCollection<int> {2, 2, 2} },
                    new Presence(StudentsList[1], true) { Grades = new ObservableCollection<int> {2, 2} },
                    new Presence(StudentsList[2], true) { Grades = new ObservableCollection<int> {2, 2, 2, 2} },
                    new Presence(StudentsList[3], true) { Grades = new ObservableCollection<int> {2, 2, 2} },
                    new Presence(StudentsList[4], true) { Grades = new ObservableCollection<int> {2} },
                },
            };
            Lessons.Add(secondLesson);
            LessonDates.Add(secondLesson.Date);
        }

        public double ComputeGroupAverage(List<Presence> presenceList)
        {
            int suma = 0;
            int ile = 0;
            double avg = 0.0;

            foreach (var presence in presenceList)
            {
                suma += presence.Grades.Sum();
                ile += presence.Grades.Count;
            }

            if (ile == 0)
            {
                avg = 0;
            }
            else
            {
                avg = (double)suma / (double)ile;
            }

            return Math.Round(avg, 2);
        }

        public void AddStudent(string inputedSurname, string inputedName)
        {
            if (StudentsList.Any(s => s.Surname == inputedSurname && s.Name == inputedName))
            {
                MessageBox.Show("Podany student już istnieje", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var s = new Student
                {
                    Index = StudentsList.Last().Index + 1,
                    Name = inputedName,
                    Surname = inputedSurname
                };

                StudentsList.Add(s);
            }
        }

        public void EditStudent(Student editedStudent)
        {
            EditStudentDialog esd = new EditStudentDialog(editedStudent.Name, editedStudent.Surname);
            if (esd.ShowDialog() == true)
            {
                editedStudent.Name = esd.InsertedName;
                editedStudent.Surname = esd.InsertedSurname;
            }
        }

        public void AddLesson(DateTime data)
        {
            Lessons.Add(
                new Lesson
                {
                    Date = data,
                    PresenceList = new List<Presence>()
                    {
                        new Presence(StudentsList[0], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(StudentsList[1], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(StudentsList[2], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(StudentsList[3], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(StudentsList[4], true) { Grades = new ObservableCollection<int> {} },
                    }
                });
            LessonDates.Add(data);
        }

        public void AddGrade(Presence editedPresence)
        {
            AddGradeDialog esd = new AddGradeDialog();
            if (esd.ShowDialog() == true)
            {
                editedPresence.Grades.Add(esd.InsertedGrade);
            }
        }

        public string SaveToFile()
        {
            string lessonsListPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LessonsList.txt";
            string lessonsListJson = JsonConvert.SerializeObject(Lessons.ToArray(), Formatting.Indented);
            File.WriteAllText(lessonsListPath, lessonsListJson);
            return lessonsListPath;
        }

        public string ReadFromFile()
        {
            string lessonsListPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LessonsList.txt";
            var lessonsListFromFile = JsonConvert.DeserializeObject<ObservableCollection<Lesson>>(File.ReadAllText(lessonsListPath));
            Lessons = lessonsListFromFile;

            var studentsFromFile = new ObservableCollection<Student>();
            foreach (var item in lessonsListFromFile[0].PresenceList)
            {
                studentsFromFile.Add(item.Student);
            }
            StudentsList = studentsFromFile;

            LessonDates = new ObservableCollection<DateTime>(Lessons.Select(l => l.Date).ToList());

            return lessonsListPath;
        }
    }
}
