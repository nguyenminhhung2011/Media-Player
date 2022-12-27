﻿using MahApps.Metro.IconPacks;
using Media_Player.UserControls;
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
        private int _number = 0;
        BindingList<UserControls.SongListItem> _songList = new BindingList<UserControls.SongListItem>();

        public MainWindow()
        {
            InitializeComponent();
            SongList.ItemsSource = _songList;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }


        private void AddMedia(string name)
        {
            UserControls.SongListItem temp = new UserControls.SongListItem();
            temp.Title = _currentPlaying;
            temp.Number = _number.ToString();
            _number++;
            _songList.Add(temp);
            Uri pathMedia = new Uri(_currentPlaying, UriKind.Absolute);
            player.Source = pathMedia;
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

        private void nextMedia_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chooseMedia(object sender, MouseEventArgs e)
        {
            int i = SongList.SelectedIndex;
            _currentPlaying = _songList[i].Title;
            _playing = false;

            Uri pathMedia = new Uri(_currentPlaying, UriKind.Absolute);
            player.Source = pathMedia;
        }
    }
}
