using System;
using System.Windows;

namespace Journal
{
    /// <summary>
    /// Interaction logic for EditStudentWindow.xaml
    /// </summary>
    public partial class EditStudentDialog : Window
    {
        public EditStudentDialog(string name, string surname)
        {
            InitializeComponent();
            StudentName.Text = name;
            StudentSurname.Text = surname;

        }

        private void BtnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            StudentName.SelectAll();
            StudentName.Focus();
        }

        public string InsertedName
        {
            get { return StudentName.Text; }
        }

        public string InsertedSurname
        {
            get { return StudentSurname.Text; }
        }
    }
}
