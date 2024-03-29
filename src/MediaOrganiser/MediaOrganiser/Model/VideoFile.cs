﻿using System.Linq;
using TagLib;

namespace MediaOrganiser.Model
{
    public class VideoFile : MediaFile
    {
        public VideoFile(string path) : base(path)
        {
            Resolution = $"{TagFile.Properties.VideoWidth}x{TagFile.Properties.VideoHeight}";
            Title = TagFile.Tag.Title;
            Year = TagFile.Tag.Year;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private uint _year;
        public uint Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _resolution;
        public string Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public override byte[] GetThumbnailBytes()
        {
            return TagFile.Tag.Pictures.SingleOrDefault(x => x.Type == PictureType.MovieScreenCapture)?.Data.Data;
        }

    }
}
