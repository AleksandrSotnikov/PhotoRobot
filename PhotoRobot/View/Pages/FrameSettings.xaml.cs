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
using System.Windows.Forms;

namespace PhotoRobot.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для FrameSettings.xaml
    /// </summary>
    public partial class FrameSettings : Page
    {
        public FrameSettings()//конструктор класса
        {
            InitializeComponent();//Инициализация компонентов
            PathOne.Text = Properties.Settings.Default.PathToDirectoryOne;//Вывод текущей директории 1 файла
            PathSecond.Text = Properties.Settings.Default.PathToDirectorySecond;//Вывод текущей директории 2 файла
            PathThird.Text = Properties.Settings.Default.PathToDirectoryThird;//Вывод текущей директории 3 файла
        }

        private void DirEdit( System.Windows.Controls.TextBlock tb)//Обработчик изменения директории
        {
            string path;//Создание строковой переменной path
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())//Создание FolderBrowserDialog
                if (dialog.ShowDialog() == DialogResult.OK)//Если была нажата клавиша ок при выборе директории
                {
                    path = dialog.SelectedPath;//Присваивания пути до файла в переменную path
                    tb.Text = path;//Вывод измененной директории
                    Properties.Settings.Default.PathToDirectoryOne = Properties.Settings.Default.PathToDirectoryOne != PathOne.Text ? path : Properties.Settings.Default.PathToDirectoryOne;// Изменения настроек пути
                    Properties.Settings.Default.PathToDirectorySecond = Properties.Settings.Default.PathToDirectorySecond != PathSecond.Text ? path : Properties.Settings.Default.PathToDirectorySecond;// Изменения настроек пути
                    Properties.Settings.Default.PathToDirectoryThird = Properties.Settings.Default.PathToDirectoryThird != PathThird.Text ? path : Properties.Settings.Default.PathToDirectoryThird;// Изменения настроек пути
                    Properties.Settings.Default.Save();//Сохранения путей
                }    
        }

        private void DirOne_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия на кнопку выбора директории #1
        {
            DirEdit(PathOne);//Вызов метода DirEdit
        }

        private void DirThird_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия на кнопку выбора директории #2
        {
            DirEdit(PathThird);//Вызов метода DirEdit
        }

        private void DirSecond_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия на кнопку выбора директории #3
        {
            DirEdit(PathSecond);//Вызов метода DirEdit
        }

        private void Settings_Click(object sender, RoutedEventArgs e)//Обработчик событий нажатия на кнопку настроеек 
        {
            Helper.FrameUpdater.frame.Navigate(new View.Pages.FrameMain());//Обращение к frame для осуществления перехода на страницу
        }
    }
}
