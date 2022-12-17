using MahApps.Metro.IconPacks;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Threading;

namespace Media_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentPlaying = string.Empty;
        private bool _playing = false;
        private string _playorPause = "Play";
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;    
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Selct_file_button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if(screen.ShowDialog() == true)
            {
                _currentPlaying = screen.FileName;
                
            }
        }
        private void playMedia_Click(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding("Kind");
            binding.Source = playMedia;
            if(_playing == true)
            {
                iconPlayMedia.Kind = PackIconMaterialKind.Pause;
                _playing= false;
                player.Pause();
            }
            else
            {
                iconPlayMedia.Kind = PackIconMaterialKind.Play;
                _playing= true;
                player.Play();
            }
        }
    }
}
