using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Player
{
    public class Playlist : INotifyPropertyChanged, ICloneable
    {
        public string playlistName { get; set; }
        public BindingList<UserControls.SongListItem> items { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public object Clone()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"{playlistName}";
        }
    }
}
