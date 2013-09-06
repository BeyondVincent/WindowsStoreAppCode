using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureView
{
    public class ImageData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ImageData(string name, string imagePath, string description)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
        }
    }
}
