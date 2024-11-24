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

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Model.Partner> partners = new List<Model.Partner>();

        public MainWindow()
        {
            InitializeComponent();
            //Установка связи с БД с проверкой
            try
            {
                App.DB = new Model.DBPartner();
                MessageBox.Show("Связь с БД установлена", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Связь с БД установлена", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }
        }
        /// <summary>
        /// Завершение приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// При активизации окна показать список партнеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            partners=App.DB.Partner.ToList();
            //Заполнение расчетного свойства в цикле
            foreach (Model.Partner partner in partners)
            {
                //Сначала получить список продуктов
                var listProduct = App.DB.PartnerProduct.Where(pp => pp.PartnerId == partner.PartnerId).ToList();
                if (listProduct==null)      //Если список путой, то сразу 0
                {
                    partner.PartnerDiscount = 0;
                }
                else
                {
                    //int sumProductCount = App.DB.PartnerProduct.Where(pp => pp.PartnerId == partner.PartnerId).Sum(pp => pp.PartnerProductCount);
                    int sumProductCount = listProduct.Sum(pp => pp.PartnerProductCount);
                    if (sumProductCount < 10000)
                    {
                        partner.PartnerDiscount = 0;
                    }
                    else
                    {
                        if (sumProductCount < 50000)
                        {
                            partner.PartnerDiscount = 5;
                        }
                        else
                        {
                            if (sumProductCount < 300000)
                            {
                                partner.PartnerDiscount = 10;
                            }
                            else
                            {
                                partner.PartnerDiscount = 15;
                            }
                        }
                    }
                }
            }
            //Отобразить результаты
            //listViewPartners.ItemsSource = partners;
            //listBoxPartners.ItemsSource = partners;
            gridPartners.ItemsSource = partners;
        }
        /// <summary>
        /// Добавить нового партнера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            PartnerWindow partnerWindow = new PartnerWindow(null, 0);
            this.Hide();
            partnerWindow.ShowDialog();
            this.Show();
        }
        /// <summary>
        /// Выбор партнера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxPartners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Model.Partner partner = listViewPartners.SelectedItem as Model.Partner;
            //Model.Partner partner = listBoxPartners.SelectedItem as Model.Partner;
            //PartnerWindow partnerWindow = new PartnerWindow(partner, 1);
            //this.Hide();
            //partnerWindow.ShowDialog();
            //this.Show();
        }
        /// <summary>
        /// Выбор парнера в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridPartners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Partner partner = gridPartners.SelectedItem as Model.Partner;
            PartnerWindow partnerWindow = new PartnerWindow(partner, 1);
            this.Hide();
            partnerWindow.ShowDialog();
            this.Show();
        }
    }
}
