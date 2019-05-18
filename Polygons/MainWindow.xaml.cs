/* 
    Вариант 2-1    

    Вывести с помощью ListView информацию о многоугольниках, что описаны в текстовом файле: в каждой строке 
    указываются координаты всех вершин одного многоугольника. В компоненте должно быть три столбца: 
    количество вершин, периметр, площадь. Проверять на то, что многоугольник является корректным многоугольником не требуется.

    Абдуллоев Парвиз
    Май 2019.
    */



using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace Polygons
{
    /// <summary>
    /// Многоугольник
    /// </summary>
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
            var list = new List<Polygon>(); // список многоугольников
            string[] numbers; // массив координат, которые записаны в 1 строке
            double perimeter; // периметр
            Polygon polygon;

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show("Числа в файле должны быть" 
                                + Environment.NewLine
                                + "записаны в следующем формате:"
                                + Environment.NewLine
                                + "x1 y1 x2 y2 x3 y3", "Формат");
            }

            try
            {
                using (var streamReader = new StreamReader(openFileDialog.FileName)) // открываем файл на чтения
                {
                    while (!streamReader.EndOfStream)
                    {
                        string str = streamReader.ReadLine(); // читаем 1 строку
                        perimeter = 0; // обнуляем периметр для нового многоугольника

                        numbers = str.Split(' ');  // добавляем каждую коориднату в массив с помощью Split

                        if ((numbers.Length >= 3) && (numbers.Length % 2 == 0)) // если количество вершин >=3 и количество вершин четное количество
                        {

                            for (byte i = 0; i <= (numbers.Length - 3); i += 2) // находим периметр
                            {
                                int numberX1 = int.Parse(numbers[i]);
                                int numberY1 = int.Parse(numbers[i + 1]);

                                int numberX2 = int.Parse(numbers[i + 2]);
                                int numberY2 = int.Parse(numbers[i + 3]);

                                double distance = Math.Sqrt(((numberX2 - numberX1) * (numberX2 - numberX1)) + ((numberY2 - numberY1) * (numberY2 - numberY1))); // длина 

                                perimeter += distance;
                            }

                            // переменные для нахожения площади многоугольника
                            int x1 = int.Parse(numbers[0]);
                            int y1 = int.Parse(numbers[1]);
                            double sum = 0;
                            int x2 = 0;
                            int y2 = 0;

                            for (byte i = 0; i < (numbers.Length - 4); i++) // находим площадь
                            {
                                x2 = int.Parse(numbers[i + 2]);
                                y2 = int.Parse(numbers[i + 3]);
                                sum += (x1 + x2) * (y2 - y1);
                                x1 = x2;
                                y1 = y2;
                            }

                            sum += (int.Parse(numbers[0]) + x2) * (int.Parse(numbers[1]) - y2);



                            polygon = new Polygon()
                            {
                                CountOfPoints = Convert.ToString(numbers.Length / 2),
                                Perimeter = Convert.ToString(perimeter),
                                Square = Convert.ToString(Math.Abs(sum / 2))
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
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Вы не выбрали файл!");
            }
            ListView.ItemsSource = list;
        }
    }
}
