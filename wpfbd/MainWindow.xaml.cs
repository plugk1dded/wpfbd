using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace wpfbd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HotelsPage());
            Manager.MainFrame = MainFrame;

            // ImportHotels();
        }

        private void ImportHotels()
        {
            var filedata = File.ReadAllLines(@"C:\Users\user\Desktop\Отели.txt");
            var images = Directory.GetFiles(@"C:\Users\user\Desktop\Отели фото");

            foreach (var line in filedata)
            {
                var data = line.Split('\t');

                var tempHotel = new Hotel
                {
                    Name = data[0].Replace("\"", ""),
                    Country = data[2], // хз че делать
                    CountOfStars = int.Parse(data[1]),
                };
                
                try
                {
                    tempHotel.HotelImage = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempHotel.Name))); // тоже хз по идее надо тянуть из другой таблицы изначально в нее записав
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                ToursEntities.GetContext().Hotel.Add(tempHotel);
                ToursEntities.GetContext().SaveChanges();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
