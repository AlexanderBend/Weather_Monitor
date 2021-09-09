using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Windows.Interop;


namespace Weather_Monitor
{
   
    public partial class MainWindow : Window
    {
        Point old;
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public const int HWND_BOTTOM = 0x1;
        public const uint SWP_NOSIZE = 0x1;
        public const uint SWP_NOMOVE = 0x2;
        public const uint SWP_SHOWWINDOW = 0x40;
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public static void HideFromAltTab(IntPtr Handle)
        {
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }
        private IntPtr Handle
        {
            get
            {
                return new WindowInteropHelper(this).Handle;
            }
        }
        private void ShoveToBackground()
        {
            SetWindowPos((int)this.Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }
        public void TryOutputWeather(string city)
        {
            var response = WeatherApi.GetRespone(city);
            var wetherFacade = JsonConvert.DeserializeObject<WeaterFacade>(response);

            try
            {
             
                city_Weather.Content = wetherFacade.Request.query;
                date_Weather.Content = wetherFacade.Location.localtime;
                day_temp.Content = wetherFacade.Current.temperature + " C°";
                wind_speed.Content = Convert.ToString(wetherFacade.Current.wind_speed + " м/c"); 
                humidity.Content = wetherFacade.Current.humidity + " %";
                weather_status.Content = wetherFacade.Current.weather_descriptions[0];

                day_image.Source = BitmapFrame.Create(new Uri(Convert.ToString(wetherFacade.Current.weather_icons[0])));
                
                
                wind_dir.Content = Convert.ToString(wetherFacade.Current.wind_dir);
                cloud_proc.Content = wetherFacade.Current.cloudcover + " %";
                precipitation_proc.Content = wetherFacade.Current.precip + " см";
                uv_index.Content = wetherFacade.Current.uv_index;
                day_temp_fells.Content = wetherFacade.Current.feelslike + " C°";
                lat.Content = wetherFacade.Location.lat + " °";
                lon.Content = wetherFacade.Location.lon + " °";
            }
            catch
            {
                MessageBox.Show("Введите корректные данные");
            }        
        }

        public MainWindow()
        {
            InitializeComponent();
            TryOutputWeather("Лондон");

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TryOutputWeather(searchCity.Text);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideFromAltTab(Handle);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShoveToBackground();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            old = e.GetPosition(null);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point cur = e.GetPosition(null);
                this.Left += cur.X - old.X;
                this.Top += cur.Y - old.Y;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("https://c.stocksy.com/a/bK6000/z9/24341.jpg", UriKind.Absolute));
            this.Background = myBrush;
        }
    }
}
