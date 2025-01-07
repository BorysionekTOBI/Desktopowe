using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Canvasy
{
    public partial class MainWindow : Window
    {
        private SolidColorBrush wybranyKolor = Brushes.Blue;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void cbKolor_Zmiana(object sender, SelectionChangedEventArgs e)
        {
            if (cbKolor.SelectedItem is ComboBoxItem wybranyElement)
            {
                wybranyKolor = (SolidColorBrush)new BrushConverter().ConvertFromString((string)wybranyElement.Tag);
            }
        }

        private void Okno_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pozycjaKlikniecia = e.GetPosition(cnv);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Rectangle prostokąt = new Rectangle
                {
                    Width = 100,
                    Height = 50,
                    Fill = wybranyKolor
                };
                Canvas.SetLeft(prostokąt, pozycjaKlikniecia.X);
                Canvas.SetTop(prostokąt, pozycjaKlikniecia.Y);
                cnv.Children.Add(prostokąt);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Ellipse elipsa = new Ellipse
                {
                    Width = 80,
                    Height = 50,
                    Fill = wybranyKolor
                };
                Canvas.SetLeft(elipsa, pozycjaKlikniecia.X);
                Canvas.SetTop(elipsa, pozycjaKlikniecia.Y);
                cnv.Children.Add(elipsa);
            }
        }
    }
}