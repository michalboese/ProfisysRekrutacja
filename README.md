
# Profisys Rekrutacja

Celem zadania jest stworzenie aplikacji desktopowej lub webowej opartej na platformie .NET, która powinna spełniać następujące zadania:

* Import danych z plików CSV umieszczonych w katalogu z zadaniem, a następnie zapisanie ich do bazy danych. Plik Documents.csv zawiera rekordy reprezentujące dokumenty, a plik DocumentItems.csv zawiera rekordy reprezentujące pozycje dokumentów z poprzedniego pliku.
* Pobranie danych z bazy danych i wyświetlenie ich w aplikacji. Najlepiej aby dokumenty zostały zaprezentowane w formie tabelarycznej, wraz z możliwością podejrzenia konkretnego dokumentu wraz z jego pozycjami.


## Tech Stack

**Client:** Vue, Vite, PrimeVue

**Server:** ASP.NET 8.0 Web API, EntityFramework

**Database:** Microsoft SQL Server


## Features

- Importowanie danych z plików CSV umieszczonych w katalogu
- Importowanie danych z plików CSV wysłanych na stronę
- Wyświetlanie dokumentów z bazy danych
- Sortowanie dokumentów
- Wyszukiwanie dokumentów
- Wybór ilości dokumentów wyświetlanych na stronę
- Usuwanie wszystkich rekordów z bazy danych


## Installation

Po uruchomieniu backendu .sln należy wpisać komendę

```bash
  dotnet ef database update
```
w Package Manager Console, aby utworzyć bazę danych.
W razie potrzeby można zmienić ConnectionString w appsettings.json.
Następnie należy włączyć rozwiązanie w trybie deweloperskim.


W folderze profisys_frontend w konsoli wpisujemy komendy
```bash
  npm install
  npm run dev
```
,które zainstalują wszystkie potrzebne elementy i włączą lokalny serwer z aplikacją dostępny pod adresem http://localhost:5173/
    
## Screenshots

![App Screenshot](https://i.imgur.com/hsvIkg4.png)

