using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherGuru.Weather
{
    internal class main
    {
        private double _temp;

        public double temp
        {
            get
            {   return _temp;  }

            set { _temp = value - 273.15; } // Из кельвинов в цельсии
        }

        private double _pressure;

        public double pressure
        {
            get { return _pressure; }
            set { _pressure = value / 1.3332239; } // в миллиметры ртутного столба из кПа
        }

        public double humidity;

        private double _temp_min;

        public double temp_min
        {
            get
            { return _temp_min; }

            set { _temp_min = value - 273.15; } // Из кельвинов в цельсии
        }


        private double _temp_max;

        public double temp_max
        {
            get
            { return _temp_max; }

            set { _temp_max = value - 273.15; } // Из кельвинов в цельсии
        }
    }
}
