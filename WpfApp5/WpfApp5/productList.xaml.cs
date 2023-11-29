using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfApp5
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value < 10 ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    /// <summary>
    /// Логика взаимодействия для productList.xaml
    /// </summary>
    public partial class productList : Window
    {
        private goodsEntities _context = new goodsEntities();
        Goods goods = new Goods();
        public productList()
        {
            InitializeComponent();
            data.ItemsSource = goodsEntities.GetContext().Goods.ToList();
            Update();
        }
        int b = 0; /// <summary>
        /// b является переменной для проверки текущего индекса у сортировки
        /// </summary>
        public void Update()
        {
            var query = goodsEntities.GetContext().Goods.ToList();
            int count = query.Count();
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    data.ItemsSource = query;
                    break;
                case 1:
                    data.ItemsSource = query.OrderBy(p => p.price);
                    break;
                case 2:
                    data.ItemsSource = query.OrderByDescending(p => p.price);
                    break;
            }
            //фильтрация по скидке
            switch (cmbFiltr.SelectedIndex)
            {
                case 0: //все диапазоны
                    data.ItemsSource = query;
                    if (b == 1) data.ItemsSource = query.OrderBy(p => p.price);
                    if (b == 2) data.ItemsSource = query.OrderByDescending(p => p.price);
                    break;
                case 1: // до 10%
                    data.ItemsSource = query.Where(p => p.discount >= 0 && p.discount < 10).ToList();
                    if (b == 1) data.ItemsSource = query.OrderBy(p => p.price);
                    if (b == 2) data.ItemsSource = query.OrderByDescending(p => p.price);
                    break;
                case 2: //до 15%
                    data.ItemsSource = query.Where(p => p.discount >= 10 && p.discount < 15).ToList();
                    if (b == 1) data.ItemsSource = query.OrderBy(p => p.price);
                    if (b == 2) data.ItemsSource = query.OrderByDescending(p => p.price);
                    break;
                case 3: // от 15%
                    data.ItemsSource = query.Where(p => p.discount >= 15).ToList();
                    if (b == 1) data.ItemsSource = query.OrderBy(p => p.price);
                    if (b == 2) data.ItemsSource = query.OrderByDescending(p => p.price);
                    break;
            }
            decimal sum = 0m;
            decimal disc = 0m;
            for (int i = 0; i < data.Items.Count; i++)
            {
                sum = (int)decimal.Parse(_context.Goods.ToList().Where(p => p.id_goods > 0).Sum(p => p.price * p.count).ToString());
            }
            for (int i = 0; i < data.Items.Count; i++)
            {
                disc = (int)decimal.Parse(_context.Goods.ToList().Where(p => p.id_goods > 0).Sum(p => p.discount * p.price / 100).ToString());
            }
            int result = Convert.ToInt32(sum - disc);
            lblSum.Content = "Сумма" + sum.ToString();
            lblDisc.Content = "Скидка" + disc.ToString();
            lblRes.Content = "Итог" + result.ToString();
            lblCount.Content = "Количество записей: " + data.Items.Count + " из " + count;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Хотите вернуться в меню авторизации?", "Возврат", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Update();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            b = 1;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            b = 2;
        }
    }
}
