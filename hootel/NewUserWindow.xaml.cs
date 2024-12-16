using System.Windows;
using hootel.Models;

namespace hootel
{
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string lastname = txtLastname.Text;
            string firstname = txtFirstname.Text;
            string username = txtUsername.Text;
            string role = txtRole.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string password = txtPassword.Password;

            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(lastname) || string.IsNullOrWhiteSpace(firstname) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Все поля, кроме телефона, должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new HotelManagementContext())
                {
                    // Проверка на существующего пользователя по логину или email
                    if (context.Users.Any(u => u.Username == username || u.Email == email))
                    {
                        MessageBox.Show("Пользователь с таким логином или email уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Создание нового пользователя
                    var newUser = new User
                    {
                        Lastname = lastname,
                        Firstname = firstname,
                        Username = username,
                        Role = role,
                        Email = email,
                        Phone = string.IsNullOrWhiteSpace(phone) ? null : phone,
                        Password = password, // Важно: хешировать пароль в реальном приложении!
                        FailedLoginAttempts = false,
                        IsLocked = false,
                        IsFirstLogin = true,
                        LastLoginDate = null
                    };

                    // Добавление и сохранение пользователя
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    MessageBox.Show("Пользователь успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении пользователя: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
