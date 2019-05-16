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
        public string CountOfPoints { get; set; }
        public string Perimeter { get; set; }
        public string Square { get; set; }
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
            double perimeter;
            Polygon polygon;

            if (openFileDialog.ShowDialog() == true)
            {
                //MessageBox.Show(openFileDialog.FileName, "Файл");
                MessageBox.Show("Числа в файле должны быть" 
                                + Environment.NewLine
                                + "записаны в следующем формате:"
                                + Environment.NewLine
                                + "x1 y1 x2 y2 x3 y3", "Формат");
            }
            

            using (var streamReader = new StreamReader(openFileDialog.FileName))
            {
                while (!streamReader.EndOfStream)
                {
                    string str = streamReader.ReadLine(); // читаем 1 строку
                    perimeter = 0; // обнуляем периметр для нового многоугольника

                    numbers = str.Split(' '); 

                    if ((numbers.Length >= 3) && (numbers.Length % 2 == 0)) // если количество вершин > 1 и нечетные
                    {
                        for (int i = 0; i <= (numbers.Length - 3); i += 2)
                        {
                            int numberX1 = int.Parse(numbers[i]);
                            int numberY1 = int.Parse(numbers[i + 1]);

                            int numberX2 = int.Parse(numbers[i + 2]);
                            int numberY2 = int.Parse(numbers[i + 3]);

                            double distance = Math.Sqrt(((numberX2 - numberX1) * (numberX2 - numberX1)) + ((numberY2 - numberY1) * (numberY2 - numberY1)));

                            perimeter += distance;
                        }

                        polygon = new Polygon()
                        {
                            CountOfPoints = Convert.ToString(numbers.Length / 2),
                            Perimeter = Convert.ToString(perimeter)
                        };

                        list.Add(polygon);
                    }                  

                    else
                    {
                        polygon = new Polygon()
                        {
                            CountOfPoints = "Ошибка",
                            Perimeter = "Ошибка",
                            Square = "Ошибка"
                        };
                        list.Add(polygon);
                    }
                }
            }
            ListView.ItemsSource = list;
        }
    }
}
