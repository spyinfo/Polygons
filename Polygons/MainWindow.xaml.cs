using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.IO;

namespace Polygons
{
    class Polygon
    {
        public int CountOfPoints { get; set; }
        public int Perimeter { get; set; }
        public int Square { get; set; }
    }



    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var list = new List<Polygon>();
            string[] numbers;
            int perimeter;
            Polygon polygon;

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(openFileDialog.FileName);
            }
            

            using (var streamReader = new StreamReader(openFileDialog.FileName))
            {
                while (!streamReader.EndOfStream)
                {
                    string str = streamReader.ReadLine();
                    perimeter = 0;

                    numbers = str.Split(' ');
                    

                    for (int i = 0; i <= numbers.Length; i += 2)
                    {
                        int numberInt = int.Parse(numbers[i]);
                        perimeter += numberInt;
                    }

                    polygon = new Polygon()
                    {
                        CountOfPoints = (numbers.Length / 2) + 1,
                        Perimeter = perimeter
                    };

                    list.Add(polygon);

                }


            }
            ListView.ItemsSource = list;
        }
    }
}
