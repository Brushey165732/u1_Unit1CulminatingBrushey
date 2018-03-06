//u1_Unit1BrusheyCulminating
//Jonathan Brushey
//2/23/2018
//Load an image that contains a powercube in it and identify the location of the powercube.

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




namespace canvasImage
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double t = 20;
        double l = 20;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetFile_Click(object sender, RoutedEventArgs e)
        {
            if (canvas.Children.Count > 0)
            {
                canvas.Children.RemoveAt(0);
            }

            //import image

            Random rnd = new Random();

            Microsoft.Win32.OpenFileDialog openFileD = new Microsoft.Win32.OpenFileDialog();
            openFileD.ShowDialog();

            BitmapImage bi = new BitmapImage(new Uri(openFileD.FileName));
            System.Windows.Media.ImageBrush ib = new ImageBrush(bi);
            canvas.Background = ib;

            //int x = bi.PixelHeight;
            //int y = bi.PixelHeight;



            MessageBox.Show("Image is " + bi.PixelHeight + " pixels tall.");
            MessageBox.Show("Image is " + bi.PixelWidth + " pixels wide.");

            //get pixel
            int stride = bi.PixelWidth * 4;
            int size = bi.PixelHeight * stride;
            byte[] pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);

            //int X = rnd.Next(0, bi.PixelWidth-1);
            //int Y = rnd.Next(0, bi.PixelWidth - 1);

            SolidColorBrush color = new SolidColorBrush();

            int i = 0;
            int n = 0;
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;

            int top_of_cube = 0;
            int bottom_of_cube = 0;
            int left_side_of_cube = 0;
            int right_side_of_cube = 0;

            Console.WriteLine("Image Size = " + bi.PixelWidth + bi.PixelHeight);

            //cycles through x-axis; every 25th of the image is checked
            for (i = 0; i <= bi.PixelWidth; i = i + (bi.PixelWidth / 25))
            {
                Console.WriteLine("   ");
                int x_pixel = i;
                Console.WriteLine("New Column, x = " + i);
                //MessageBox.Show("x_pixel = " + x_pixel);

                //cycles through y-axis; every 25th of the image is checked
                for (n = 0; n <= bi.PixelHeight; n = n + (bi.PixelHeight / 25))
                {
                    int y_pixel = n;

                    Console.WriteLine("pixel is at " + x_pixel + " ," + y_pixel);

                    int index = y_pixel * stride + 4 * x_pixel;
                    Console.WriteLine(index);

                    //Console.WriteLine(pixels);
                    Console.WriteLine("index = " + index + " Location " + x_pixel + ", " + y_pixel + " stride = " + stride);
                    //MessageBox.Show("index = " + index + "Location " + x_pixel + ", " + y_pixel + "stride = " + stride);

                    //MessageBox.Show("..");

                    byte blue = pixels[index];
                    byte green = pixels[index + 1];
                    byte red = pixels[index + 2];
                    byte alpha = pixels[index + 3];

                    Console.WriteLine("Colour " + red + ", " + blue + ", " + green);

                    if (red > 149 && blue <= 100 && green > 149)
                    {
                        Console.WriteLine("Yellow Detected");


                        //goes up from pixel until yellow is not found
                        for (a = 0; a < y_pixel && a >= 0; a = a + (bi.PixelHeight / 100))
                        {
                            index = a * stride + 4 * x_pixel;

                            blue = pixels[index];
                            green = pixels[index + 1];
                            red = pixels[index + 2];
                            alpha = pixels[index + 3];

                            if (red > 150 && blue < 100 && green > 150)
                            //if (red > 0 && blue > 0 && green > 0)
                            {

                                top_of_cube = a;
                                break;
                            }
                        }
                        //goes left from pixel until yellow is not found
                        for (b = 0; b < x_pixel && b >= 0; b = b + (bi.PixelWidth / 100))
                        {
                            index = y_pixel * stride + 4 * b;

                            blue = pixels[index];
                            green = pixels[index + 1];
                            red = pixels[index + 2];
                            alpha = pixels[index + 3];

                            if (red > 150 && blue < 100 && green > 150)
                            //if (red > 0 && blue > 0 && green > 0)
                            {

                                left_side_of_cube = b;
                                break;
                            }
                        }
                        //goes down from pixel until yellow is not found
                        for (c = y_pixel + 1; c > y_pixel && c <= bi.PixelHeight; c = c + (bi.PixelHeight / 100))
                        {
                            //Console.WriteLine("In loop at 163");
                            index = c * stride + 4 * x_pixel;

                            blue = pixels[index];
                            green = pixels[index + 1];
                            red = pixels[index + 2];
                            alpha = pixels[index + 3];



                            if (red > 150 && blue < 100 && green > 150)
                            //if (red > 0 && blue > 0 && green > 0)
                            {

                                bottom_of_cube = c;
                                break;
                            }
                            else
                            {
                                bottom_of_cube = y_pixel;
                                break;
                            }
                        }
                        //goes right from pixel until yellow is not found
                        for (d = x_pixel + 1; d > x_pixel && d <= bi.PixelWidth; d = d + (bi.PixelWidth / 100))
                        {
                            index = y_pixel + stride + 4 * d;

                            blue = pixels[index];
                            green = pixels[index + 1];
                            red = pixels[index + 2];
                            alpha = pixels[index + 3];

                            Console.WriteLine("loop entered, d = " + d);
                            Console.WriteLine("colours = " + red + " " + green + " " + blue);

                            if (red > 150 && blue < 100 && green > 150)
                            //if (red > 0 && blue > 0 && green > 0)
                            {
                                Console.WriteLine("if statement entered");

                                right_side_of_cube = d;
                                break;
                            }
                            else
                            {
                                right_side_of_cube = x_pixel;
                                Console.WriteLine("if statement bypassed");
                                break;
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("No Yellow Found.");
                    }

                    Console.WriteLine("Top= " + top_of_cube + " | Bottom= " + bottom_of_cube + " | Left Side= " + left_side_of_cube + " | Right Side= " + right_side_of_cube);
                    Console.WriteLine("     ");


                    //Defines colour for rectangle overlay (purple)
                    color.Color = Color.FromArgb(255, 255, 0, 255);

                    //creates rectangle and displays around yellow area

                    Rectangle r = new Rectangle()
                    {
                        Height = bottom_of_cube - top_of_cube,
                        Width = right_side_of_cube - left_side_of_cube,
                        Fill = color,
                    };

                    double cube_top = top_of_cube;
                    double cube_left = left_side_of_cube;

                    canvas.Children.Add(r);
                    Canvas.SetLeft(r, cube_left);
                    Canvas.SetTop(r, cube_top);

                }
            }

            MessageBox.Show("Rectangle's Top Left Corner = " + left_side_of_cube + ", " + top_of_cube + "Rectangle's Bottom Right Corner = " + right_side_of_cube + ", " + bottom_of_cube);
        }
    }
}
