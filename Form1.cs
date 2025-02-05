using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using WeatherGuru.Weather;
using Microsoft.Extensions.Configuration;


namespace WeatherGuru
{
    public partial class Form1 : Form
    {

        private readonly string apiKey;

        public Form1()
        {
            InitializeComponent();

            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()) .AddJsonFile("appsettings.json").Build();

            apiKey = config["WeatherAPIKey"];
        }

        private async void Get_Set_Info(string Place)
        {
            try
            {
                string requestStr = $"http://api.openweathermap.org/data/2.5/weather?q={Place}&APPID={apiKey}";
                WebRequest request = WebRequest.Create(requestStr);

                request.Method = "POST";
                request.ContentType = "application/x-www-urlencoded";

                WebResponse response = await request.GetResponseAsync();

                string answer = String.Empty;

                using (Stream s = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(s))
                    {
                        answer = await reader.ReadToEndAsync();
                    }
                }
                response.Close();

                richTextBox1.Text = answer;

                Weather.OpenWeather obj = JsonConvert.DeserializeObject<OpenWeather>(answer);

                groupBox1.Text = $"Погода в {Place}";
                panel1.BackgroundImage = obj.weather[0].Icon;
                label1.Text = obj.weather[0].main;
                label2.Text = obj.weather[0].description;
                label3.Text = "Средняя температура (°C): " + obj.main.temp.ToString("0.##");
                label4.Text = "Скорость (м/с): " + obj.wind.speed.ToString();
                label5.Text = "Направление: " + obj.wind.degToNav;
                label6.Text = "Влажность воздуха (%): " + obj.main.humidity.ToString();
                label7.Text = "Атмосферное давление (мм): " + ((int)obj.main.pressure).ToString();

            }


            catch (WebException ex)
            {
                using (Stream s = ex.Response?.GetResponseStream())
                {
                    if (s != null)
                    {
                        using (StreamReader reader = new StreamReader(s))
                        {
                            string errorResponse = reader.ReadToEnd();
                            richTextBox1.Text = $"Ошибка запроса: {errorResponse}";
                        }
                    }
                    else
                    {
                        richTextBox1.Text = $"Ошибка сети: {ex.Message}";
                    }
                }
            }
            catch (JsonException ex)
            {
                richTextBox1.Text = $"Ошибка обработки JSON: {ex.Message}";
            }
            catch (Exception ex)
            {
                richTextBox1.Text = $"Произошла ошибка: {ex.Message}";
            }

            button1.BackColor = Color.White;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q=Moscow&APPID={apiKey}");

            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = await request.GetResponseAsync();

            string answer = String.Empty;

            using (Stream s = response.GetResponseStream())
            { 
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();

            richTextBox1.Text = answer;

            Weather.OpenWeather obj = JsonConvert.DeserializeObject<OpenWeather>(answer);

            panel1.BackgroundImage = obj.weather[0].Icon;
            label1.Text = obj.weather[0].main;
            label2.Text = obj.weather[0].description;
            label3.Text = "Средняя температура (°C): " + obj.main.temp.ToString("0.##");
            label4.Text = "Скорость (м/с): " + obj.wind.speed.ToString();
            label5.Text = "Направление: " + obj.wind.degToNav;
            label6.Text = "Влажность воздуха (%): " + obj.main.humidity.ToString();
            label7.Text = "Атмосферное давление (мм): " + ((int)obj.main.pressure).ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                string Place = textBox1.Text.ToString();
                Get_Set_Info(Place);
                button1.BackColor = Color.Green;
            }

            else
            {
                button1.BackColor = Color.Red;
                textBox1.Text = "Введите корректную локацию!";
            }
        }

    }
}
