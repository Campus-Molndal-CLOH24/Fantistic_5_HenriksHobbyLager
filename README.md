<<<<<<< Updated upstream
# HenriksHobbyLager
=======
# HenriksHobbyLager

## Medlemmar
- Anton
- Hugo
- Kenny
- Alireza
- Parham

## Kort Beskrivning av Projektet
**Henriks HobbyLager™** är ett grundläggande inventeringshanteringssystem för att lagra och hantera produktinformation.  
Programmet stödjer funktioner för att lägga till, uppdatera, ta bort och söka efter produkter.  
Det körs helt och hållet i minnet, vilket innebär att all data går förlorad när applikationen stängs.  
Applikationen är designad som en konsolapplikation för enkelhetens skull.

## Installationsinstruktioner
Följ dessa steg för att skapa en `.env`-fil i projektet och konfigurera MongoDB-anslutningssträngen:

1. **Navigera till målkatalogen**  
   Gå till följande katalog:
   Fantistic_5_HenriksHobbyLager\HenriksHobbyLager\bin\Debug\net8.0
 
2. **Skapa .env-filen**  
Skapa en ny fil i katalogen med namnet `.env`.

3. **Lägg till MongoDB-anslutningssträngen**  
Öppna `.env`-filen i en textredigerare och lägg till följande rad:
```env
MONGO_DB_CONNECTION=DIN ANSLUTNINGSSTRÄNG
```

## Hur man kör programmet
### Starta programmet Och Docker.
1. **Starta Docker**  
   Starta Docker Desktop för att köra MongoDB-databasen. Man kan välja att köra MongoDB eller SQLite.

2. **Add Product**  
   Lägg till en ny produkt i lagret genom att ange produktens namn, pris, kategori....

3. **Update Product**  
   Uppdatera information om en befintlig produkt, t.ex. ändra namn, pris eller lagerantal. Eller kan man byta hella produkten.

4. **Delete Product**  
   Ta bort en produkt från lagret genom att ange dess ID.

5. **List Products**  
   Visa en lista över alla produkter som finns lagrade i systemet.

6. **Search Products**  
   Sök efter produkter baserat på namn eller kategori.

7. **Exit**  
   Avsluta programmet.

Efter använder man DataBass att spara producter, kan man stänga programmet och öppna igen och se att alla producter finns kvar.

 
## Eventuella konfigurationsinställningar
**Säkerhetskopiering eller import/export**  
   - Om folk vill flytta sin produktdata till en annan dator, fixa så att data kan sparas i en fil (typ `.json` eller `.csv`) och läsas in igen.  
   - Det gör det också enklare om du vill testa nya saker utan att sabba den riktiga databasen.

## Lista över implementerade patterns
- **Repository-mönster**:  
  Definieras av gränssnittet `IRepository`, även om det inte är fullt utnyttjat i denna version.

- **Fasadmönster (Facade Pattern)**:  
  Representeras av gränssnittet `IProductFacade` för hantering av produktoperationer.

- **CRUD-operationer**:  
  Funktionalitet för att skapa, läsa, uppdatera och ta bort produkter är implementerad.

## Kort beskrivning av databasstrukturen
För närvarande simuleras databasen med en in-memory-struktur: `List<Product>`.  
Varje produkt innehåller följande egenskaper:

- **Id**: En unik identifierare för produkten.
- **Name**: Produktens namn.
- **Price**: Produktens pris (decimal).
- **Stock**: Tillgänglig lagerkvantitet (heltal).
- **Category**: Produktens kategori (sträng).
- **Created**: Tidsstämpeln när produkten skapades.
- **LastUpdated**: Tidsstämpeln för senaste uppdatering (nullable DateTime).

Framtida versioner kan inkludera en persistent databas, exempelvis SQLite.  
Data kan också lagras som ett mer strukturerat format.>>>>>>> Stashed changes
