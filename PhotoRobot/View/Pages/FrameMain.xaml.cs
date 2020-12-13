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
        private string[] filesOne = { };
        private int currentOne = 0;
        private string[] filesSecond = { };
        private int currentSecond = 0;
        private string[] filesThird = { };
        private int currentThird = 0;

        public FrameMain()
        {
            InitializeComponent();
            Properties.Settings.Default.Reset();
            try
            {
                if (Properties.Settings.Default.PathToDirectoryOne != "Путь до файловой директории отсутствует")
                {
                    filesOne = Directory.GetFiles(Properties.Settings.Default.PathToDirectoryOne, "*.jpg", SearchOption.AllDirectories);
                    if (filesOne.Length > 0) ImageOne.Source = BitmapFrame.Create(new Uri(@filesOne[0]));
                }
                if (Properties.Settings.Default.PathToDirectorySecond != "Путь до файловой директории отсутствует")
                {
                    filesSecond = Directory.GetFiles(Properties.Settings.Default.PathToDirectorySecond, "*.jpg", SearchOption.AllDirectories);
                    if (filesSecond.Length > 0) ImageSecond.Source = BitmapFrame.Create(new Uri(@filesSecond[0]));
                }
                if (Properties.Settings.Default.PathToDirectoryThird != "Путь до файловой директории отсутствует")
                {
                    filesThird = Directory.GetFiles(Properties.Settings.Default.PathToDirectoryThird, "*.jpg", SearchOption.AllDirectories);
                    if (filesThird.Length > 0) ImageThird.Source = BitmapFrame.Create(new Uri(@filesThird[0]));
                }
            }
            catch
            {
                Properties.Settings.Default.Reset();
            }
        }

        private void ThirdRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentThird + 1 < filesThird.Length)
            {
                currentThird++;
                ImageThird.Source = BitmapFrame.Create(new Uri(@filesThird[currentThird]));
            }
        }

        private void SecondRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentSecond + 1 < filesSecond.Length)
            {
                currentSecond++;
                ImageSecond.Source = BitmapFrame.Create(new Uri(@filesSecond[currentSecond]));
            }
        }

        private void FirstRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentOne + 1 < filesOne.Length)
            {
                currentOne++;
                ImageOne.Source = BitmapFrame.Create(new Uri(@filesOne[currentOne]));
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                {                
                RenderTargetBitmap renderTargetBitmap =
                    new RenderTargetBitmap((int)(Images.ActualWidth*6),(int)(Images.ActualHeight * 6), 384, 384, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(Images);
                PngBitmapEncoder pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                using (Stream fileStream = File.Create(@dialog.SelectedPath+"/file.jpg"))
                {
                   pngImage.Save(fileStream);
                }
            }
        }

        private void ThirdLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentThird - 1 >= 0)
            {
                currentThird--;
                ImageThird.Source = BitmapFrame.Create(new Uri(@filesThird[currentThird]));
            }
        }

        private void SecondLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentSecond - 1 >= 0)
            {
                currentSecond--;
                ImageSecond.Source = BitmapFrame.Create(new Uri(@filesSecond[currentSecond]));
            }
        }

        private void FirstLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentOne - 1 >= 0)
            {
                currentOne--;
                ImageOne.Source = BitmapFrame.Create(new Uri(@filesOne[currentOne]));
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Helper.FrameUpdater.frame.Navigate(new View.Pages.FrameSettings());
        }
    }
}
