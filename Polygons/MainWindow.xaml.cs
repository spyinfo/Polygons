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
    /// <summary>
    /// Многоугольник
    /// </summary>
    class Polygon
    {
        public string CountOfPoints { get; set; } // количество вершин
        public string Perimeter { get; set; } // периметр
        public string Square { get; set; } // площадь
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

        /// <summary>
        /// Кнопка "Выбрать файл"
        /// </summary>
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
                                + "X1 Y1 X2 Y2 X3 Y3", "Формат");
            }

            try
            {
                using (var streamReader = new StreamReader(openFileDialog.FileName)) // открываем файл на чтение
                {
                    while (!streamReader.EndOfStream)
                    {
                        string str = streamReader.ReadLine(); // читаем 1 строку
                        perimeter = 0; // обнуляем периметр для нового многоугольника

                        int X1 = 0;
                        int X2 = 0;
                        int Y1 = 0;
                        int Y2 = 0;

                        numbers = str.Split(' ');  // добавляем каждую коориднату в массив с помощью Split

                        if ((numbers.Length >= 3) && (numbers.Length % 2 == 0)) // если количество вершин >=3 и количество вершин четное количество
                        {
                            for (int i = 0; i <= (numbers.Length - 3); i += 2) // находим периметр
                            {
                                X1 = int.Parse(numbers[i]);
                                Y1 = int.Parse(numbers[i + 1]);

                                X2 = int.Parse(numbers[i + 2]);
                                Y2 = int.Parse(numbers[i + 3]);

                                double distance = Math.Sqrt(Math.Pow((X2 - X1), 2) + Math.Pow((Y2 - Y1), 2)); // длина отрезка (стороны)

                                perimeter += distance;
                            }

                            perimeter += Math.Sqrt(Math.Pow(X2 - int.Parse(numbers[0]), 2) + Math.Pow((Y2 - int.Parse(numbers[1])),2));

                            // переменные для нахожения площади многоугольника
                            double sum = 0;

                            for (int i = 0; i <= (numbers.Length - 4); i++) // находим площадь
                            {
                                X1 = int.Parse(numbers[i]);
                                Y1 = int.Parse(numbers[i + 1]);

                                X2 = int.Parse(numbers[i + 2]);
                                Y2 = int.Parse(numbers[i + 3]);

                                sum += X1 * Y2 - X2 * Y1;
                            }

                            //int Y = int.Parse(numbers[3]);

                            //for (int i = 0; i <= (numbers.Length - 4); i += 2)
                            //{
                            //    int X = int.Parse(numbers[i]);
                            //    sum1 += X * Y;
                            //    Y = int.Parse(numbers[i + 2]);
                            //}

                            //int x1 = int.Parse(numbers[2]);

                            //for (int i = 1; i <= (numbers.Length - 4); i += 2)
                            //{
                            //    int y1 = int.Parse(numbers[i]);
                            //    sum2 += y1 * x1;
                            //    x1 = int.Parse(numbers[i + 2]);
                            //}


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
            // обработка исключений
            catch (System.OverflowException)
            {
                MessageBox.Show("Скорее всего произошло переполнение!");
                list = null; // обнуляем список
            }

            catch (System.FormatException)
            {
                MessageBox.Show("Файл не является текстовым!");
                list = null; 
            }

            catch (System.ArgumentException)
            {
                MessageBox.Show("Вы не выбрали файл!");
            }

            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Директория не найдена!");
            }

            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не найден!");
            }

            catch (IOException)
            {
                MessageBox.Show("Ошибка при вводе-выводе");
            }

            ListView.ItemsSource = list;

        }
    }
}