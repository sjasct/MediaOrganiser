﻿using System.Linq;
using TagLib;

namespace MediaOrganiser.Model
{
    public class AudioFile : MediaFile
    {
        public AudioFile(string path) : base(path)
        {
            Title = TagFile.Tag.Title;
            Artist = TagFile.Tag.JoinedPerformers;
            Album = TagFile.Tag.Album;
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _artist;

        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        private string _album;

        public string Album
        {
            get { return _album; }
            set { _album = value; }
        }

        public override byte[] GetThumbnailBytes()
        {
            return TagFile.Tag.Pictures.SingleOrDefault(x => x.Type == PictureType.FrontCover)?.Data.Data;
        }
    }
}
