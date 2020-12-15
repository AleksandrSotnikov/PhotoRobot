using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoRobot.View.Pages
{
    /// <summary>   
    /// Логика взаимодействия для FrameMain.xaml
    /// </summary>
    public partial class FrameMain : Page
    {
        private string[] filesOne = { };//Хранение путей до jpg файла#1
        private int currentOne = 0;//Текущий jpg файл#1
        private string[] filesSecond = { };//Хранение путей до jpg файла#2
        private int currentSecond = 0;//Текущий jpg файл#2
        private string[] filesThird = { };//Хранение путей до jpg файла#3
        private int currentThird = 0;//Текущий jpg файл #3

        public FrameMain()//конструктор класса
        {
            InitializeComponent();//Инцииализация компонентов
            try//Обработка ошибок
            {
                init();//вызов метода init
            }
            catch//Обработка ошибок
            {
                Properties.Settings.Default.Reset();//Сброс прописанных путей при ошибке
            }
        }

        private string[] initSetting(int current,String PathDirection, string[] files, System.Windows.Controls.Image image)//Метод инициализации настроек
        {
            if(PathDirection != "Путь до файловой директории отсутствует")//Проверка на стандартную строку
            {
                files = Directory.GetFiles(PathDirection, "*.jpg", SearchOption.AllDirectories);//Поиск всех файлов с расширением .jpg в текущей директории
                if(files.Length>0) image.Source = BitmapFrame.Create(new Uri(@files[current]));//Если в списке присутствуют файлы то вывод на экран
            }
            return files;//Возврат массива путей до .jpg файла
        }

        private void init() {//метод инициализации настроек
            filesOne = initSetting(currentOne,Properties.Settings.Default.PathToDirectoryOne, filesOne, ImageOne);//Присваивание массиву путей до файлов
            filesSecond = initSetting(currentSecond, Properties.Settings.Default.PathToDirectorySecond, filesSecond, ImageSecond);//Присваивание массиву путей до файлов
            filesThird = initSetting(currentThird, Properties.Settings.Default.PathToDirectoryThird, filesThird, ImageThird);//Присваивание массиву путей до файлов
        }


        private int RightClick(int current, string[] files, System.Windows.Controls.Image image)//Обработчик событий нажатия на правую стрелочку
        {
            if (current + 1 < files.Length)//Если путь до файла существует то выполнить действие
            {
                image.Source = BitmapFrame.Create(new Uri(@files[current+1]));//Создание картинки в компоненте image
                return 1;//возврат 1
            }
            return 0;//возврат 0
        }

        private void ThirdRight_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия на 3 правую стрелочку
        {
           currentThird += RightClick(currentThird, filesThird, ImageThird);//Изменение значения переменной в зависимости от того что вернет обработчик событий
        }

        private void SecondRight_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия на 2 правую стрелочку
        {
            currentSecond += RightClick(currentSecond, filesSecond, ImageSecond);//Изменение значения переменной в зависимости от того что вернет обработчик событий
        }

        private void FirstRight_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия на 1 правую стрелочку
        {
            currentOne += RightClick(currentOne, filesOne, ImageOne);//Изменение значения переменной в зависимости от того что вернет обработчик событий
        }

        private void Create_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия кнопки создать
        {
            using (SaveFileDialog dialog = new SaveFileDialog()) {//Создание SaveFileDialog
                dialog.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";//Доступные расширения файла
                dialog.FilterIndex = 1;//выбор текущего расширения
                if (dialog.ShowDialog() == DialogResult.OK)//Если была нажата клавиша ок при сохранении
                {
                    RenderTargetBitmap renderTargetBitmap =
                        new RenderTargetBitmap((int)(Images.ActualWidth * 6), (int)(Images.ActualHeight * 6), 384, 384, PixelFormats.Pbgra32);//Приобразование элемента страницы в изображение
                    renderTargetBitmap.Render(Images);//Визуализирует
                    PngBitmapEncoder pngImage = new PngBitmapEncoder();//Кодировщик картинки
                    pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));//Создание картинки
                    using (Stream fileStream = File.Create(@dialog.FileName))//Создание потока записи
                    {
                        pngImage.Save(fileStream);//Сохранение картинки в файл
                    }
                }
            }
            init();//Вызов метода инициализации, на случай если файл сохранен в той же директории, где храняться фотографии
        }

        private int LeftClick(int current, string[] files, System.Windows.Controls.Image image)//Обработчик событий нажатия на левую стрелочку
        {
            if (current - 1 >= 0)//Если путь до файла существует то выполнить действие
            {
                image.Source = BitmapFrame.Create(new Uri(@files[current-1]));//Создание картинки в image
                return -1;//Возврат -1
            }
            return 0;//Возврат 0
        }
        private void ThirdLeft_Click(object sender, RoutedEventArgs e)//Обработчик событий нажатия на левую стрелочку #3
        {
            currentThird += LeftClick(currentThird, filesThird, ImageThird);//Изменение значения переменной в зависимости от того что вернет обработчик событий
        }

        private void SecondLeft_Click(object sender, RoutedEventArgs e)//Обработчик событий нажатия на левую стрелочку #2
        {
            currentSecond += LeftClick(currentSecond, filesSecond, ImageSecond);//Изменение значения переменной в зависимости от того что вернет обработчик событий
        }

        private void FirstLeft_Click(object sender, RoutedEventArgs e)//Обработчик событий нажатия на левую стрелочку #1
        {
            currentOne += LeftClick(currentOne, filesOne, ImageOne);//Изменение значения переменной в зависимости от того что вернет обработчик событий
        }

        private void Settings_Click(object sender, RoutedEventArgs e)//Обработчик событий нажатия на кнопку настроеек
        {
            Helper.FrameUpdater.frame.Navigate(new View.Pages.FrameSettings());//Обращение к frame для осуществления перехода на страницу
        }
    }
}
