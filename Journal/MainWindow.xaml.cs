using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Journal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> _studentsList { get; set; }
        private ObservableCollection<Lesson> _lessons { get; set; }
        private ObservableCollection<DateTime> _lessonDates { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _studentsList = new ObservableCollection<Student>
            {
                new Student {Index =1, Name = "Piotr", Surname="Strzelecki"},
                new Student {Index =2, Name = "Mateusz", Surname="Szpinda"},
                new Student {Index =3, Name = "Piotr", Surname="Sobiborowicz"},
                new Student {Index =4, Name = "Agata", Surname="Szarpak"},
                new Student {Index =5, Name = "Daniel", Surname="Szlaski"},
            };
            StudentsListView.ItemsSource = _studentsList;

            _lessons = new ObservableCollection<Lesson>();
            _lessonDates = new ObservableCollection<DateTime>(_lessons.Select(l => l.Date).ToList());

            FillLessons();
            AddLesson(new DateTime(2017, 08, 05));

            PresenceListView.ItemsSource = _lessons.Select(l => l.PresenceList).ToList()[0];
            ChooseLessonComboBox.ItemsSource = _lessonDates;
            ChooseLessonComboBox.SelectedItem = _lessons.Select(l => l.Date).ToList()[0];

            //var lekcja = _lessons.Select(l => l.PresenceList).ToList()[0];
            //foreach(var presence in lekcja)
            //{
            //    if(presence.Student.Index == 1){

            //    }
            //}



            //test.ItemsSource = _lessons[0].PresenceList[0].Grades;

        }

        private double ComputeGroupAverage(List<Presence> presenceList)
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

        private void FillLessons()
        {
            var firstLesson = new Lesson
            {
                Date = new DateTime(2017, 01, 01),
                PresenceList = new List<Presence>()
                {
                    new Presence(_studentsList[0], true) { Grades = new ObservableCollection<int> { 3, 5, 4 }  },
                    new Presence(_studentsList[1], true) { Grades = new ObservableCollection<int> {2, 2, 3} },
                    new Presence(_studentsList[2], true) { Grades = new ObservableCollection<int> {3, 5, 4} },
                    new Presence(_studentsList[3], false) { Grades = new ObservableCollection<int> {2, 5, 5} },
                    new Presence(_studentsList[4], true) { Grades = new ObservableCollection<int> {2,3} },
                },
            };
            _lessons.Add(firstLesson);
            _lessonDates.Add(firstLesson.Date);

            var secondLesson = new Lesson
            {
                Date = new DateTime(2017, 03, 22),
                PresenceList = new List<Presence>()
                {
                    new Presence(_studentsList[0], false) { Grades = new ObservableCollection<int> {2, 2, 2} },
                    new Presence(_studentsList[1], true) { Grades = new ObservableCollection<int> {2, 2} },
                    new Presence(_studentsList[2], true) { Grades = new ObservableCollection<int> {2, 2, 2, 2} },
                    new Presence(_studentsList[3], true) { Grades = new ObservableCollection<int> {2, 2, 2} },
                    new Presence(_studentsList[4], true) { Grades = new ObservableCollection<int> {2} },
                },
            };
            _lessons.Add(secondLesson);
            _lessonDates.Add(secondLesson.Date);
        }

        private void AddLesson(DateTime data)
        {
            _lessons.Add(
                new Lesson
                {
                    Date = data,
                    PresenceList = new List<Presence>()
                    {
                        new Presence(_studentsList[0], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(_studentsList[1], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(_studentsList[2], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(_studentsList[3], true) { Grades = new ObservableCollection<int> {} },
                        new Presence(_studentsList[4], true) { Grades = new ObservableCollection<int> {} },
                    }
                });
            _lessonDates.Add(data);
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (_studentsList.Any(s => s.Surname == AddStudentSurname.Text && s.Name == AddStudentName.Text))
            {
                MessageBox.Show("Podany student już istnieje", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var s = new Student
                {
                    Index = _studentsList.Last().Index + 1,
                    Name = AddStudentName.Text,
                    Surname = AddStudentSurname.Text
                };

                _studentsList.Add(s);
            }
        }

        private void BtnRemoveStudent_click(object sender, RoutedEventArgs e)
        {
            if (StudentsListView.SelectedItem is Student studentToRemove)
            {
                _studentsList.Remove(studentToRemove);
            }
        }

        private void BtnEditStudent_click(object sender, RoutedEventArgs e)
        {
            if (StudentsListView.SelectedItem is Student editedStudent)
            {
                EditStudentDialog esd = new EditStudentDialog(editedStudent.Name, editedStudent.Surname);
                if (esd.ShowDialog() == true)
                {
                    editedStudent.Name = esd.InsertedName;
                    editedStudent.Surname = esd.InsertedSurname;
                }
            }
        }

        private void ChooseLessonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLesson = _lessons.Where(l => l.Date == (DateTime)ChooseLessonComboBox.SelectedItem).Select(l => l.PresenceList).ToList()[0];
            PresenceListView.ItemsSource = selectedLesson;
            AverageGroupTextBlock.Text = ComputeGroupAverage(selectedLesson).ToString();
        }

        private void BtnAddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddLesson(DateTime.Today);
            MessageBox.Show($"Pomyślnie utworzono spotkanie na dzien: {DateTime.Today.ToShortDateString()}", "Dodano spotkanie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAddGrade_click(object sender, RoutedEventArgs e)
        {
            if (PresenceListView.SelectedItem is Presence editedPresence)
            {
                AddGradeDialog esd = new AddGradeDialog();
                if (esd.ShowDialog() == true)
                {
                    editedPresence.Grades.Add(esd.InsertedGrade);
                }
            }
        }

        private void BtnEditGrades_click(object sender, RoutedEventArgs e)
        {
            if (PresenceListView.SelectedItem is Presence editedPresence)
            {
                EditGradesDialog egd = new EditGradesDialog(editedPresence);
                if (egd.ShowDialog() == true)
                {
                }
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            var presence = checkbox.DataContext as Presence;

            if (checkbox.IsChecked == false)
            {
                presence.Student.AbsencesNumber++;
            }
            else
            {
                if (presence.Student.AbsencesNumber > 0)
                {
                    presence.Student.AbsencesNumber--;
                }
                else
                {
                    presence.Student.AbsencesNumber = 0;
                }
            }
        }
    }
}
