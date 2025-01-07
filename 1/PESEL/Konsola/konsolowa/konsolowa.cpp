#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>
#include <unordered_map>

using namespace std;

// Struktura do przechowywania danych z pliku
struct Data {
    int number;
    string word;
};

// Klasa aplikacji
class ConsoleApp {
private:
    vector<Data> data;

public:
    // Metoda wczytująca dane z pliku
    void wczytaj(const string& filename) {
        ifstream file(filename);
        if (!file.is_open()) {
            cerr << "Nie mozna otworzyc pliku: " << filename << endl;
            return;
        }

        int number;
        string word;
        while (file >> number >> word) {
            data.push_back({ number, word });
        }

        file.close();
    }

    /********************************************************
    * nazwa funkcji: pary
    * parametry wejściowe: brak
    * wartość zwracana: brak (wyświetla listę par w konsoli)
    * autor: <PESEL>
    * ****************************************************/
    void pary() const {
        for (const auto& entry : data) {
            if (entry.word.length() == entry.number) {
                cout << "Wyraz: " << entry.word << ", Dlugosc: " << entry.word.length() << endl;
            }
        }
    }

    /********************************************************
    * nazwa funkcji: blizniaki
    * parametry wejściowe: brak
    * wartość zwracana: brak (wyświetla listę bliźniaków w konsoli)
    * autor: <PESEL>
    * ****************************************************/
    void blizniaki() const {
        unordered_map<string, vector<string>> sorted_words;

        for (const auto& entry : data) {
            string sorted_word = entry.word;
            sort(sorted_word.begin(), sorted_word.end());
            sorted_words[sorted_word].push_back(entry.word);
        }

        for (const auto& pair : sorted_words) {
            if (pair.second.size() > 1) {
                cout << "Blizniaki: ";
                for (const auto& word : pair.second) {
                    cout << word << " ";
                }
                cout << endl;
            }
        }
    }
};

int main() {
    ConsoleApp app;

    // Wczytanie danych z pliku
    app.wczytaj("dane.txt");

    // Rozwiązanie pierwszego problemu
    cout << "--- Lista par ---" << endl;
    app.pary();

    // Rozwiązanie drugiego problemu
    cout << "\n--- Lista blizniakow ---" << endl;
    app.blizniaki();

    return 0;
}
