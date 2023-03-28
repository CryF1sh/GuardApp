using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GuardApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AccessControl : Window
    {
        private DispatcherTimer _timer;
        private int _count = 0;

        private User _currentUser = new User();

        public AccessControl(User user)
        {
            InitializeComponent();
            DataContext = _currentUser;
            tblSotrudnik.Text = user.SurName + " " + user.Name[0] + ". " + user.Patronymic[0] +".";
            List<Gender> genders = GuardianEntities.GetContext().Genders.ToList();
            cmbGender.ItemsSource = genders;
        }
        int chek = 0;
       
        private void butSave_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbGender.SelectedItem == null) || (textSurName == null) || (textName == null) || (textPatronymic == null) || (textPost == null)) 
            {
                chek ++;
                if (chek == 2)
                {
                    MessageBox.Show("Блокировка окна на 5 минут!");
                    _timer = new DispatcherTimer();
                    _timer.Interval = TimeSpan.FromSeconds(1);
                    _timer.Tick += Timer_Tick;
                    _timer.Start();
                    chek = 0;
                    textSurName.IsReadOnly = true;
                    textPatronymic.IsReadOnly = true;
                    textPost.IsReadOnly = true;
                    textName.IsReadOnly = true;
                    cmbGender.IsEditable = false;
                    butClear.IsEnabled = false;
                    butSave.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данные были добавлены!");
                GuardianEntities.GetContext().Users.Add(_currentUser);
                GuardianEntities.GetContext().SaveChanges();
                ClearText();
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _count++;

            if (_count == 360) // Блокировка окна после 5 минут
            {
                _timer.Stop(); // Остановка таймера
                textSurName.IsReadOnly = false;
                textPatronymic.IsReadOnly = false;
                textPost.IsReadOnly = false;
                textName.IsReadOnly = false;
                cmbGender.IsEditable = true;
                butClear.IsEnabled = true;
                butSave.IsEnabled = true;
            }
        }

        private void ClearText()
        {
            textSurName.Text = null;
            textPatronymic.Text = null;
            textPost.Text = null;
            textName.Text = null;
            cmbGender = null;
        }
        private void butClear_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
        }


    }
}
