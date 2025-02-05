using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherGuru.Weather
{
    internal class wind
    {

        public double speed;

        public double deg;

        public double gust;

        public string degToNav
        {
            get
            {
                if (this.deg == 0.0)
                    return "Север";
                else if(this.deg == 180.0)
                    return "Юг";
                else if(this.deg == 90.0)
                    return "Восток";
                else if(this.deg == 270.0)
                    return "Запад";

                else if(this.deg > 0 && this.deg < 90) return "Северо-Восток";
                else if(this.deg > 90 && this.deg < 180) return "Юго-Восток";
                else if(this.deg > 180 && this.deg < 270) return "Юго-Запад";
                else if(this.deg > 270 && this.deg < 360) return "Северо-Запад";

                else
                    return null;
            }
        }
    }
}
