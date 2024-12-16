using System;
using System.Linq;
using System.Windows;
using hootel.Models;

namespace hootel
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly int _userId;

        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = txtCurrentPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmNewPassword = txtConfirmPassword.Password;

            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(currentPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmNewPassword))
            {
                MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка совпадения нового пароля и подтверждения
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Новый пароль и подтверждение не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new HotelManagementContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == _userId);

                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Проверка текущего пароля
                    if (user.Password != currentPassword)
                    {
                        MessageBox.Show("Текущий пароль неверен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    user.Password = newPassword;
                    user.IsFirstLogin = false;

                    context.SaveChanges();
                    MessageBox.Show("Пароль успешно изменён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении пароля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
