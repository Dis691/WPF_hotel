using System.Windows;
using hootel.Models;
using hootel;
using Microsoft.EntityFrameworkCore;

namespace D0225
{
    public partial class Admin : Window
    {
        private HotelManagementContext _context;

        public Admin()
        {
            InitializeComponent();
            _context = new HotelManagementContext();

            LoadUsers();
        }

        // Загрузка данных пользователей из базы данных
        private void LoadUsers()
        {
            try
            {
                var users = _context.Users.ToList();
                Users.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Добавление нового пользователя
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var newUserWindow = new NewUserWindow(); // Окно для добавления пользователя
            newUserWindow.ShowDialog();

            // После закрытия окна перезагружаем список пользователей
            LoadUsers();
        }

        // Разблокировка выбранного пользователя
        private void UnlockUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = Users.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для разблокировки.", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Разблокировка пользователя
                selectedUser.IsLocked = false;
                _context.Entry(selectedUser).State = EntityState.Modified;
                _context.SaveChanges();

                MessageBox.Show("Пользователь разблокирован.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadUsers(); // Обновляем список
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при разблокировке пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Сохранение изменений, внесённых в DataGrid
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Очистка ресурсов при закрытии окна
        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }
    }
}
