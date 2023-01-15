using MahApps.Metro.IconPacks;
using Media_Player.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
        private int _number = 0;
        private int _currentIndex = 0;
        private bool _shuffleMode = false;

        BindingList<UserControls.SongListItem> _songList = new BindingList<UserControls.SongListItem>();
        List<BindingList<UserControls.SongListItem>> _listPlaylistSong = new List<BindingList<UserControls.SongListItem>>();

        public MainWindow()
        {
            InitializeComponent();
            _listPlaylistSong.Add(new BindingList<UserControls.SongListItem>());
            SongList.ItemsSource = _listPlaylistSong[0];
            SongList.ItemsSource = _songList;
            mediaSlider.Value = 0;
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void currentMedia()
        {
            Uri pathMedia = new Uri(_currentPlaying, UriKind.Absolute);
            player.Source = pathMedia;
        }

        private void AddMedia(string name)
        {
            UserControls.SongListItem temp = new UserControls.SongListItem();
            _currentIndex = _number;
            //console.log(_currentIndex.)
            temp.Title = _currentPlaying;
            temp.Number = _number.ToString();
            _number++;
            _songList.Add(temp);
            currentMedia();
        }

        private String getNameMedia(String str)
        {
            String result = "";
            for(int i  = 0; i < str.Length; i++) { 
                if (str[i] == '/')
                {
                    break;
                }
                result+= str[i].ToString();
            }
            return result;
        }

        private void Selct_file_button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if(screen.ShowDialog() == true)
            {
                _currentPlaying = screen.FileName;
                AddMedia(_currentPlaying);
            }
        }
        private void playMedia_Click(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding("Kind");
            binding.Source = playMedia;
            MediaState mediaState = new MediaState();

            if (_playing == true)
            {
                iconPlayMedia.Kind = PackIconMaterialKind.Play;
                _playing= false;
                mediaState = MediaState.Pause;
                player.LoadedBehavior = mediaState;
            }
            else
            {
                iconPlayMedia.Kind = PackIconMaterialKind.Pause;
                _playing= true;
                mediaState = MediaState.Play;
                player.LoadedBehavior = mediaState;
            }
        }

        private void chooseMedia(object sender, MouseEventArgs e)
        {
            //var newIndex = SongList.SelectedIndex + ;
            int i = SongList.SelectedIndex;
            if( i == -1)
            {
                i = 0;
            }
            _currentIndex = i;
            _currentPlaying = _songList[i].Title;
            currentMediaString.Text =  i.ToString();
            _playing = false;
            currentMedia();
        }

        void skipPrevious1()
        {
            int count = _songList.Count();

            if (count <= 1)
            {
                return;
            }

            if (_currentIndex == 0)
            {
                _currentIndex = count - 1;
            }
            else
            {
                _currentIndex = _currentIndex - 1;
            }

            _currentPlaying = _songList[_currentIndex].Title;
            currentMediaString.Text = _songList[_currentIndex].Title;
            iconPlayMedia.Kind = PackIconMaterialKind.Play;
            currentMedia();
        }

        private void skipPrevious_Click(object sender, RoutedEventArgs e)
        {
            skipPrevious1();
        }
        private void skipNext_Click(object sender, RoutedEventArgs e)
        {
            int count = _songList.Count();

            if (count <= 1)
            {
                return;
            }

            if (_currentIndex == (count - 1))
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }
            currentMediaString.Text = _songList[_currentIndex].Title;
            _currentPlaying = _songList[_currentIndex].Title;
            iconPlayMedia.Kind = PackIconMaterialKind.Play;
            currentMedia();

        }

        private void progressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = mediaSlider.Value;
            TimeSpan newPosition = TimeSpan.FromSeconds(value);
            //currentMediaString.Text = value.ToString();
        
            if(player  != null)
            {
                player.Position = newPosition;

            }
        }

        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            int hours = player.NaturalDuration.TimeSpan.Hours;
            int minutes = player.NaturalDuration.TimeSpan.Minutes;
            int seconds = player.NaturalDuration.TimeSpan.Seconds;
            totalPosition.Text = $"{hours}:{minutes}:{seconds}";
            mediaSlider.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;

        }

        private void shuffle_Click(object sender, RoutedEventArgs e)
        {
            _shuffleMode = !_shuffleMode;
            if (_shuffleMode)
            {
                iconShuffle.Kind = PackIconMaterialKind.Shuffle;
            }
            else
            {
                iconShuffle.Kind = PackIconMaterialKind.Replay;

            }
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaSlider.Value = 0;
            if (!_shuffleMode)
            {
                if (_currentIndex < _songList.Count - 1)
                {
                    _currentIndex += 1;
                    _currentPlaying = _songList[_currentIndex].Title;
                    currentMediaString.Text = _songList[_currentIndex].Title;
                    currentMedia();
                }
            }
            else
            {
                Random rnd = new Random();
                int len = _songList.Count();
                if (len < 1)
                {
                    return;
                }
                _currentIndex = rnd.Next(0, len);
                _currentPlaying = _songList[_currentIndex].Title;
                currentMediaString.Text = _songList[_currentIndex].Title;
                currentMedia();
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                skipPrevious.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if(e.Key == Key.D)
            {
                skipNext.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if(e.Key == Key.X)
            {
                shuffle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if(e.Key == Key.S)
            {
                playMedia.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void VSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = (double)VSlider.Value;   
        }
    }
}
