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
        public FrameSettings()
        {
            InitializeComponent();
            PathOne.Text = Properties.Settings.Default.PathToDirectoryOne;
            PathSecond.Text = Properties.Settings.Default.PathToDirectorySecond;
            PathThird.Text = Properties.Settings.Default.PathToDirectoryThird;
        }

        private void DirOne_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathToDirectoryOne = dialog.SelectedPath;
                    PathOne.Text = Properties.Settings.Default.PathToDirectoryOne;
                    Properties.Settings.Default.Save();
                }
        }

        private void DirThird_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathToDirectoryThird = dialog.SelectedPath;
                    PathThird.Text = Properties.Settings.Default.PathToDirectoryThird;
                    Properties.Settings.Default.Save();
                }
        }

        private void DirSecond_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathToDirectorySecond = dialog.SelectedPath;
                    PathSecond.Text = Properties.Settings.Default.PathToDirectorySecond;
                    Properties.Settings.Default.Save();
                }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Helper.FrameUpdater.frame.Navigate(new View.Pages.FrameMain());
        }
    }
}
