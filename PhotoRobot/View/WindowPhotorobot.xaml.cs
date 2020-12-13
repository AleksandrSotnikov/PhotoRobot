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
using System.Windows.Shapes;

namespace PhotoRobot.View
{
    /// <summary>
    /// Логика взаимодействия для WindowPhotorobot.xaml
    /// </summary>
    public partial class WindowPhotorobot : Window
    {
        public WindowPhotorobot()
        {
            InitializeComponent();
            Helper.FrameUpdater.frame = CurrentFrame;
            Helper.FrameUpdater.frame.Navigate(new View.Pages.FrameMain());
        }
    }
}
