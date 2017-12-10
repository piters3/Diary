using System;
using System.Windows;

namespace Journal
{
    /// <summary>
    /// Interaction logic for AddGrade.xaml
    /// </summary>
    public partial class AddGradeDialog : Window
    {
        public AddGradeDialog()
        {
            InitializeComponent();
        }

        private void BtnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            NewGrade.SelectAll();
            NewGrade.Focus();
        }
        public int InsertedGrade
        {
            get { return Convert.ToInt32(NewGrade.Text); }
        }
    }
}
