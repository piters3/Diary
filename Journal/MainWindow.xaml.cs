using System;
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
        public TeachersJournal Jrl { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Jrl = new TeachersJournal();
            SetItemSources();
        }

        private void SetItemSources()
        {
            try
            {
                StudentsListView.ItemsSource = Jrl.StudentsList;
                PresenceListView.ItemsSource = Jrl.Lessons.Select(l => l.PresenceList).ToList()[0];
                ChooseLessonComboBox.ItemsSource = Jrl.LessonDates;
                ChooseLessonComboBox.SelectedItem = Jrl.Lessons.Select(l => l.Date).ToList()[0];
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }


        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            Jrl.AddStudent(AddStudentSurname.Text, AddStudentName.Text);
        }


        private void BtnRemoveStudent_click(object sender, RoutedEventArgs e)
        {
            if (StudentsListView.SelectedItem is Student studentToRemove)
            {
                Jrl.StudentsList.Remove(studentToRemove);
            }
        }

        private void BtnEditStudent_click(object sender, RoutedEventArgs e)
        {
            if (StudentsListView.SelectedItem is Student editedStudent)
            {
                Jrl.EditStudent(editedStudent);
            }
        }

        private void ChooseLessonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLesson = Jrl.Lessons.Where(l => l.Date == (DateTime)ChooseLessonComboBox.SelectedItem).Select(l => l.PresenceList).ToList()[0];
            PresenceListView.ItemsSource = selectedLesson;
            AverageGroupTextBlock.Text = Jrl.ComputeGroupAverage(selectedLesson).ToString();
        }

        private void BtnAddLesson_Click(object sender, RoutedEventArgs e)
        {
            Jrl.AddLesson(DateTime.Today);
            MessageBox.Show($"Pomyślnie utworzono spotkanie na dzien: {DateTime.Today.ToShortDateString()}", "Dodano spotkanie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAddGrade_click(object sender, RoutedEventArgs e)
        {
            if (PresenceListView.SelectedItem is Presence editedPresence)
            {
                Jrl.AddGrade(editedPresence);
            }
        }


        private void BtnEditGrades_click(object sender, RoutedEventArgs e)          //Nie skończone
        {
            if (PresenceListView.SelectedItem is Presence editedPresence)
            {
                EditGradesDialog egd = new EditGradesDialog(editedPresence);
                if (egd.ShowDialog() == true)
                {
                }
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)              //Tego nie wiem jak przenieść
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

        private void BtnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            string lessonsListPath = Jrl.SaveToFile();
            MessageBox.Show($"Pomyślnie zapisano dane w pliku: {lessonsListPath}", "Zapisano dane", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void BtnReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            string lessonsListPath = Jrl.ReadFromFile();

            SetItemSources();
            MessageBox.Show($"Pomyślnie wczytano dane z pliku: {lessonsListPath}", "Wczytano dane", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
