using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Media_Player
{
    /// <summary>
    /// Interaction logic for AddPlaylistWindow.xaml
    /// </summary>
    public partial class AddPlaylistWindow : Window
    {
        public Playlist newPlaylist { get; set; }
        public AddPlaylistWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if(nameTextBox.Text != "")
            {
                newPlaylist = new Playlist()
                {
                    playlistName = nameTextBox.Text,
                    items = new BindingList<UserControls.SongListItem>()
                };
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
