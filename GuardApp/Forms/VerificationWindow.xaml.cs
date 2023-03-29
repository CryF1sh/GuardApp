using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity.Migrations;
using System.ComponentModel;
using System.Data.Entity;

namespace GuardApp.Forms
{
    /// <summary>
    /// Логика взаимодействия для VerificationWindow.xaml
    /// </summary>
    public partial class VerificationWindow : Window
    {
        private User _user = new User();
        public VerificationWindow(User user)
        {
            InitializeComponent();
            tblSotrudnik.Text = user.SurName + " " + user.Name[0] + ". " + user.Patronymic[0] + ".";
            _user = user;
            DataContext = _user;
            UpdateData();
        }
        private void UpdateData()
        {
            List<User> users = GuardianEntities.GetContext().Users.ToList();
            List<TypeUser> type = GuardianEntities.GetContext().TypeUsers.ToList();
            CbxType.ItemsSource = type;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].ApprovalStatus != false)
                {
                    users.RemoveAt(i);
                }
            }
            DGVer.ItemsSource = users;
        }
        private void BtnApproval_Click(object sender, RoutedEventArgs e)
        {
            GuardianEntities.GetContext().Users.AddOrUpdate(_user);
            GuardianEntities.GetContext().SaveChanges();
            UpdateData();
        }
    }
}
