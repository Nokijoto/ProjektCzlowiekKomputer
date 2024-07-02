# Czlowiek Komputer
# Ważne

![Bearer](Resources/Bearer.png , "Bearer")


Podczas podawania tokenu nietrzeba dodawać "Bearer" aplikacja samoistnie go doda.

## Jak uruchomić aplikację za pomocą wersji Release
Aplikacja uruchamia się na linku <br>
http://localhost:5000/swagger/index.html 

Aplikacja wymaga działania SQL servera na porcie 1433,
z aplikacją jest dołączony docker compose do utworzenia kontenera sql servera.
Migracje powinny wykonać się automatycznie.
W przypadku innego serwera należy zmienić connection string w [appsettings.json](appsettings.json)

```
"ConnectionStrings": {
    "ProjectDbContextConnection": "Server = <adres serwera>,<port>; Database = BookDb; user id = <nazwa użytkownika>; password = <hasło>; Encrypt = false; TrustServerCertificate = true; Integrated Security = false;"
  },
 ```


## Jak uruchomić aplikację  za pomocą SLN
1. Sklonuj repozytorium projektu:<br> 
- Klonowanie Repozytorium
- ```git clone https://github.com/Nokijoto/ProjektCzlowiekKomputer```
- Uruchom serwer baz danych za pomocą Docker Compose:
- ```docker-compose up -d```
2. Konfiguracja połączenia z bazą danych
o
Domyślnie aplikacja jest przygotowana dla serwera sql z docker compose , jednakże w przypadku chęci ustawienia własnej bazy ,zmień w pliku appsettings.json w folderze zmień na swój Server SQL na adres swojego serwera SQL Server.
```
"ConnectionStrings": {
    "ProjectDbContextConnection": "Server = <adres serwera>,<port>; Database = BookDb; user id = <nazwa użytkownika>; password = <hasło>; Encrypt = false; TrustServerCertificate = true; Integrated Security = false;"
  },
 ```
- Następnie uruchom migracje w konsole mienadżera pakietów:
- dotnet ef database update
3. W celu uruchomiena aplikacji należy wejść do folderu i uruchomić komendę:
   ```dotnet run```  lub uruchomić ją w programie Visual Studio 2022 Comunity Edition
