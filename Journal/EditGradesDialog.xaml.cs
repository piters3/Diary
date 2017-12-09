using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Journal
{
    /// <summary>
    /// Interaction logic for EditGradesDialog.xaml
    /// </summary>
    public partial class EditGradesDialog : Window
    {
        public Presence PresenceToEdit { get; set; }

        public EditGradesDialog(Presence presenceToEdit)
        {
            InitializeComponent();
            GradesListView.ItemsSource = presenceToEdit.Grades;
            PresenceToEdit = presenceToEdit;
        }   

        private void BtnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnEditGrade_click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRemoveGrade_click(object sender, RoutedEventArgs e)
        {
            if (GradesListView.SelectedItem is int grade)
            {
                PresenceToEdit.Grades.Remove(grade);
            }
        }
    }
}
