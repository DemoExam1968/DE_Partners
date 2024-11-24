using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace Мастер_пол
{
    /// <summary>
    /// Логика взаимодействия для PartnerWindow.xaml
    /// </summary>
    public partial class PartnerWindow : Window
    {
        Model.Partner partner;
        int mode;   //0-добавлениеб 1- редактирование
        public PartnerWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Переданнный партнер или null и режим работы окна
        /// </summary>
        /// <param name="partner"></param>
        public PartnerWindow(Model.Partner partner, int mode)
        {
            InitializeComponent();
            this.partner = partner;
            this.mode = mode;
        }

        /// <summary>
        /// Возврат на окно списка партеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// При загрузке отображать данные выбранного парнера или пустые поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Настроить список типов парнеров
            comboTypePartner.ItemsSource = App.DB.PartnerType.ToList();
            comboTypePartner.DisplayMemberPath = "PartnerTypeName";
            comboTypePartner.SelectedValuePath = "PartnerTypeId";
            comboTypePartner.SelectedIndex = 0;
            textBoxRating.IsEnabled = false;
            if (mode == 1)      //Есть выбранный партнер
            {
                //Заполняем его данными элементы интерфейса
                comboTypePartner.SelectedValue = partner.PartnerTypeId;
                textBoxName.Text = partner.PartnerName;
                textBoxDir.Text = partner.PartnerDirector;
                textBoxEmail.Text = partner.PartnerEmail;
                textBoxPhone.Text = partner.PartnerPhone;
                textBoxAdress.Text = partner.PartnerAdress;
                textBoxINN.Text = partner.PartnerINN;
                textBoxRating.Text = partner.PartnerRating.ToString();
            }
            else
            {
                textBoxRating.Text = "0";
            }
        }
        /// <summary>
        /// Обновить БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (mode == 0)      //Новый - сначала его надо создать
            {
                partner = new Model.Partner();
            }
            partner.PartnerTypeId = (int)comboTypePartner.SelectedValue;
            partner.PartnerName = textBoxName.Text;
            partner.PartnerDirector = textBoxDir.Text ;
            partner.PartnerEmail = textBoxEmail.Text;
            partner.PartnerPhone = textBoxPhone.Text;
            partner.PartnerAdress = textBoxAdress.Text;
            partner.PartnerINN = textBoxINN.Text;
            //Контроль над допустимым значением рейтинга
            int resRating=0;
            try
            {
                resRating = int.Parse(textBoxRating.Text);
                if (resRating < 0) throw new Exception("Вы ввели отрицательное число");
            }
            catch (FormatException)
            {
                MessageBox.Show("Вы ввели не число", "Контроль ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch(Exception ex)   
            {
                MessageBox.Show(ex.Message, "Контроль ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            partner.PartnerRating = resRating;
            //Для нового надо добавить в список
            if (mode == 0)
            {
                App.DB.Partner.Add(partner);
            }
            //Обновляем БД с контролем
            try
            {

                App.DB.SaveChanges();
                MessageBox.Show("Информация упешно обновлена!", "Информация об обновлении", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
