﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using EZMedia.Resources;
using EZMedia.ViewModels;
using System.Windows.Media.Imaging;
using System.IO;

namespace EZMedia.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _library = new MediaLibrary();
            _Songs = new List<SongInfo>();
            _Albums = new List<AlbumInfo>();
            _Artists = new List<ArtistInfo>();
            this.Playlists = new ObservableCollection<EZPlaylist>();
        }

        /// <summary>
        /// A collection for SongInfo objects.
        /// </summary>

        private List<SongInfo> _Songs;
        public ReadOnlyCollection<SongInfo> Songs{
            get { return _Songs.AsReadOnly();  }
        }

        private List<StringKeyGroup<IMediaInfo>> _GroupedSongList;
        public List<StringKeyGroup<IMediaInfo>> GroupedSongList
        {
            get { return _GroupedSongList; }
        }

        private List<AlbumInfo> _Albums;
        public ReadOnlyCollection<AlbumInfo> Albums 
        {
            get { return _Albums.AsReadOnly(); }
        }

        private List<ArtistInfo> _Artists;
        public ReadOnlyCollection<ArtistInfo> Artists
        {
            get { return _Artists.AsReadOnly(); }
        }

        public ObservableCollection<EZPlaylist> Playlists { get; private set; }

        private SongPlayingViewModel songPlayingVM = null;
        public SongPlayingViewModel SongPlayingVM 
        {
            get
            {
                if (songPlayingVM == null)
                {
                    songPlayingVM = new SongPlayingViewModel();
                }
                return songPlayingVM;
            }
        }
        public SongsForArtistViewModel SongsForArtistVM { get; set; }
        public SongsInAlbumViewModel SongsInAlbumVM { get; set; }

        private MediaLibrary _library;

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads Songs, Albums, Artists and Playlists from the phone into the app
        /// </summary>
        public void LoadData()
        {
            foreach (Song song in _library.Songs)
            {
                _Songs.Add(new SongInfo(song));
            }

            foreach (Album album in _library.Albums)
            {
                _Albums.Add(new AlbumInfo(album));
            }

            foreach (Artist artist in _library.Artists)
            {
                _Artists.Add(new ArtistInfo(artist));
            }

            _GroupedSongList = StringKeyGroup<IMediaInfo>.CreateGroups(_Songs);

            this.IsDataLoaded = true;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}