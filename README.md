# System prognozujący szansę wygrania meczu piłki nożnej

## Opis

Projekt ten został stworzony w ramach mojej pracy inżynierskiej na Politechnice Świętokrzyskiej. Celem tego projektu jest opracowanie systemu predykcji wyniku meczu przy użyciu nowoczesnych technologii i narzędzi. W projekcie zastosowano język C#, ML.NET, React, ASP.NET Core. Algorytmy One-Versus-All (OVA) i Fast Tree Binary były kluczowe dla tworzenia modelu predykcyjnego. Analiza rezultatów uzyskanych podczas testowania modelu wykazała, że jego skuteczność wyniosła 50%. 

### Funkcjonalności

- **Predykcja wyników meczów**
- **Analiza i wizualizacja danych**
- **Intuicyjny interfejs użytkownika**

## Technologie

Projekt został zrealizowany z wykorzystaniem następujących technologii:
- C#
- ML.NET
- React
- ASP.NET Core

## Instrukcja uruchomienia
Pobrany plik .zip, zawierający kody źródłowe, należy rozpakować, a następnie otworzyć ten folder. Dalej należy otworzyć plik z rozwiązaniem projektu - KickCraze.sln. Do otwarcia tego projektu potrzebny jest Visual Studio 2022, który zacznie konfigurowanie i pobieranie niezbędnych narzędzi. Następnie z menu "Eksplorator rozwiązań" znaleźć projekt o nazwie KickCraze.Api, wybrać go prawym przyciskiem myszy i ustawić jako projekt startowy. Dalej w zakładce "Narzędzia", wybrać pozycję menadżer pakietów NuGet oraz konsola menadżera pakietów i w terminalu wpisać komendę "Update-Database", wymagana jest baza danych Microsoft SQL. Po wykonaniu tej operacji, należy rozwinąć listę przy przycisku od uruchomienia aplikacji ( zielony trójkąt w górnym panelu), wybrać pozycję IIS Express i uruchomić aplikację. Po uruchomieniu powinna otworzyć się przeglądarka wyświetlająca punkty dostępowe serwera.
Aby włączyć stronę internetową należy uruchomić program Visual Studio Code oraz przy jego pomocy otworzyć folder "kickcraze". Następnie uruchomić terminal, wbudowany w program i wpisać komendę "npm install", aby zainstalować wszystkie biblioteki. Po wykonaniu tego procesu, w tym samym terminalu, wpisać komendę "npm start". W tym momencie powinna się otworzyć przeglądarka z adresem "localhost:3000", która wyświetla stronę główną aplikacji.
Aby uruchomić program do pobierania danych z zewnętrznego serwera, w programie Visual Studio 2022 należy wybrać w "Eksploratorze rozwiązań" projekt o nazwie "FetchData" i ustawić go jako projekt startowy. Następnie wystarczy uruchomić go. Podobnie jest z projektami od wybrania najlepszych parametrów uczenia, nazwa projektu "TrainingModel" oraz do analizy modelu z danymi parametrami, nazwa projektu "TestingModel". 
