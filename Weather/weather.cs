using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WeatherGuru.Weather
{
    internal class weather
    {
        public int id;

        public string main ;

        public string description;

        public string icon;

        private readonly string projectRoot =  Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public Bitmap Icon
        { 

            get { return new Bitmap(Image.FromFile(Path.Combine(projectRoot, "icons", $"{icon}.png"))); }
        }
    }
}
