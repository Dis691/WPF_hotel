using hootel.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace hootel
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            // Проверка, что поля заполнены
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            // Проверка пользователя в базе данных
            using (var context = new HotelManagementContext())
            {
                var user = await context.Users
                    .Where(u => u.Username == username && u.Password == password)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    MessageBox.Show("Вы успешно авторизовались.");
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные.");
                }
            }
        }
    }
}
