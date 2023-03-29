using GuardApp.Forms;
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
using System.Windows.Shapes;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace GuardApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        GuardianEntities DB = new GuardianEntities();
        public LoginWindow()
        {
            InitializeComponent();
            CBTypeUser.ItemsSource = DB.TypeUsers.ToList();
            CBTypeUser.SelectedItem = CBTypeUser.Items[0];
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string pass = textPassword.Text;
            string log = textLogin.Text;
            string sec = SecretWord.Text;

            using (var db = new GuardianEntities())
            {
                
                var login = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == log);
                var password = db.Users.AsNoTracking().FirstOrDefault(u => u.Password == pass);
                var secretword = db.Users.AsNoTracking().FirstOrDefault(u => u.SecretWord == sec);

                if ((login == null) || (password == null) || (secretword == null))
                {
                    MessageBox.Show("Данные введены неверно!");
                    return;
                }
                else
                {
                    if (CBTypeUser.SelectedItem == CBTypeUser.Items[0])
                    {
                        AccessControl accessControl = new AccessControl(login);
                        accessControl.Show();
                        this.Close();
                    }
                    if (CBTypeUser.SelectedItem == CBTypeUser.Items[3])
                    {
                        VerificationWindow verificationWindow = new VerificationWindow(login);
                        verificationWindow.Show();
                        this.Close();
                    }
                }

            }
        }
        private void ForgottenData_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void GeneratePass()
        {
            string iPass = "";
            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                iPass = iPass + arr[rnd.Next(0, 57)];
            }
        }

    }
}
