using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageGallery
{
    public partial class MainWindow : Window
    {
        private string[] imageFiles;
        private string[] descriptions;

        public MainWindow()
        {
            InitializeComponent();
            LoadImagesAndDescriptions();
            ClearRightColumn();  // Wyczyszczenie prawej kolumny po uruchomieniu
        }

        /********************************************************
        * nazwa funkcji: LoadImagesAndDescriptions
        * parametry wejściowe: brak
        * wartość zwracana: void - wczytuje obrazy i opisy z katalogu
        * autor: <PESEL>
        ********************************************************/
        private void LoadImagesAndDescriptions()
        {
            try
            {
                // Ścieżka do katalogu ze zdjęciami
                string imagesFolder = @"C:\Users\t4\Desktop\TB\Przykladowe egzaminy\Desktopowe\1\PESEL\Desktopowa\pliki\images";  // Zmień na właściwą ścieżkę
                string descriptionsFile = @"C:\Users\t4\Desktop\TB\Przykladowe egzaminy\Desktopowe\1\PESEL\Desktopowa\pliki\images\opisy.txt";  // Ścieżka do pliku z opisami

                // Wczytywanie zdjęć
                imageFiles = Directory.GetFiles(imagesFolder, "*.jpg"); // Przykład dla plików .jpg
                descriptions = File.ReadAllLines(descriptionsFile);

                // Wypełnienie ListBox miniaturek
                foreach (var imageFile in imageFiles)
                {
                    var image = new Image
                    {
                        Source = new BitmapImage(new Uri(imageFile, UriKind.Absolute)),
                        Width = 100,  // Ustawienie szerokości miniaturki
                        Height = 75,  // Ustawienie wysokości miniaturki
                        Margin = new System.Windows.Thickness(5)
                    };

                    var listItem = new ListBoxItem
                    {
                        Content = image
                    };

                    ThumbnailListBox.Items.Add(listItem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd przy wczytywaniu plików: " + ex.Message);
            }
        }

        /********************************************************
        * nazwa funkcji: ClearRightColumn
        * parametry wejściowe: brak
        * wartość zwracana: void - czyści prawą kolumnę (usuwa obraz i opis)
        * autor: <PESEL>
        ********************************************************/
        private void ClearRightColumn()
        {
            // Wyczyść zawartość prawej kolumny (brak obrazu i opisu)
            SelectedImage.Source = null;
            ImageDescription.Text = string.Empty;
        }

        /********************************************************
        * nazwa funkcji: SortImages
        * parametry wejściowe: brak
        * wartość zwracana: void - funkcja sortuje obrazy na podstawie nazw plików
        * autor: <PESEL>
        ********************************************************/
        private void SortImages()
        {
            // Sortowanie tablicy obrazów po nazwach plików
            Array.Sort(imageFiles);
        }

        /********************************************************
        * nazwa funkcji: SearchImage
        * parametry wejściowe: searchTerm - tekst, który będzie szukany w nazwach plików
        * wartość zwracana: string[] - tablica zawierająca ścieżki obrazów, które pasują do wyszukiwanego terminu
        * autor: <PESEL>
        ********************************************************/
        private string[] SearchImage(string searchTerm)
        {
            // Filtrowanie obrazów, aby pasowały do wyszukiwanego terminu
            return Array.FindAll(imageFiles, image => Path.GetFileName(image).Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        /********************************************************
        * nazwa funkcji: ThumbnailListBox_SelectionChanged
        * parametry wejściowe: event - obiekt typu SelectionChangedEventArgs
        * wartość zwracana: void - obsługuje kliknięcie na miniaturkę i ładowanie obrazu i opisu
        * autor: <PESEL>
        ********************************************************/
        private void ThumbnailListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obsługa kliknięcia na miniaturkę
            if (ThumbnailListBox.SelectedIndex != -1)
            {
                int selectedIndex = ThumbnailListBox.SelectedIndex;

                // Wczytanie obrazu
                string imagePath = imageFiles[selectedIndex];
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                SelectedImage.Source = bitmap;

                // Wczytanie opisu
                string description = descriptions[selectedIndex];
                ImageDescription.Text = description;
            }
        }
    }
}
