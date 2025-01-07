/*********************************************************
/                                                        /
/                                                        /
/        Tobiasz Borsyionek nr.2                         /
/        Klasa: 4TPM                                     /
/        Data: 24.10.2024r.                              /
/                                                        /
/                                                        /
/        Sprawdzian                                      /
/                                                        /
/                                                        /
/                                                        /
/                                                        /
*********************************************************/
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sprawdzian
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            // Pobieranie danych z formularza
            string imie = dImie.Text;
            string nazwisko = dNazwisko.Text;
            string email = dEmail.Text;

            string plec = dPlecM.IsChecked == true ? "Mężczyzna" : "Kobieta";
            string wiek = dWiekPelnoletni.IsChecked == true ? "Pełnoletni" : "Nieletni";

            // Walidacja danych - sprawdzanie czy wszystkie pola są wypełnione, jeżeli nie wsywietla się komunikat z błędem 
            if (!IsValidImieNazwisko(imie))
            {
                MessageBox.Show("Proszę wpisać poprawne imię (tylko litery).", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidImieNazwisko(nazwisko))
            {
                MessageBox.Show("Proszę wpisać poprawne nazwisko (tylko litery).", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Proszę wpisać poprawny email.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tworzenie SaveFileDialog do zapisu pliku dane.txt
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt",
                Title = "Zapisz dane do pliku",
                FileName = "dane.txt"
            };

            // Sprawdzenie czy użytkownik wybrał plik
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Sprawdzamy, ile linii jest w pliku
                    int lineCount = 0;

                    if (File.Exists(saveFileDialog.FileName))
                    {
                        // Jeśli plik istnieje, liczymy ilość już zapisanych linii
                        lineCount = File.ReadAllLines(saveFileDialog.FileName).Length;
                    }

                    // Zwiększamy licznik (bo nowa linia będzie kolejną)
                    lineCount++;

                    // Formatowanie danych z numerem porządkowym
                    string dane = $"{lineCount}. {imie} {nazwisko}, Email: {email}, [{plec}, {wiek}]";

                    // Zapis danych do wybranego pliku
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, true))
                    {
                        sw.WriteLine(dane);
                    }

                    MessageBox.Show("Dane zostały zapisane pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas zapisu pliku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Funkcja sprawdzająca poprawność imienia i nazwiska (tylko litery, w tym polskie znaki)
        private bool IsValidImieNazwisko(string text)
        {
            string pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ]+$";
            return Regex.IsMatch(text, pattern);
        }

        // Funkcja sprawdzająca poprawność emaila
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}