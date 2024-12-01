# Individuell Rapport

## Hur fungerade gruppens arbete?
Grupparbetet sköttes fint, och alla var delaktiga även om det fanns en viss skillnad i kunskapsnivå.

---
## Gruppens Databasimplementation

Vi implementerade både **SQLite** och **MongoDB**. Vid start av applikationen får användaren välja databas genom att skriva antingen `mongodb` eller `sqlite`.

#### SQLite
- Vi använde **Entity Framework** för att hantera SQLite-databasen.
- Kenny föreslog att lägga till `EnsureProductsTableExists` för att säkerställa att tabellen skapas korrekt.

#### MongoDB
- Vi använde **MongoDB.Driver NuGet-paketet**, vilket förenklade hanteringen av funktionalitet, exempelvis automatisk stängning av connection.
- För connectionhantering skapade vi en `.env`-fil som placerades i `bin -> net8.0`.

---
## SOLID-principer

### **Single Responsibility Principle (SRP)**
Varje klass har ett enda ansvarsområde. Exempelvis ansvarar `ProductRepositoryFactory` endast för att skapa rätt repository baserat på vald databas.

### **Open/Closed Principle (OCP)**
Klasser kan utökas utan att förändra befintlig kod. Genom att använda interface som `IRepository` möjliggörs olika implementationer utan att påverka den existerande strukturen.

### **Liskov Substitution Principle (LSP)**
Alla implementationer av `IRepository` kan användas på samma sätt utan att ändra klientkoden. Detta säkerställer att olika repositories kan bytas ut utan problem.

### **Interface Segregation Principle (ISP)**
Interface delades upp så att varje klass endast implementerar de metoder som är relevanta för dess funktion. På så sätt undveks onödiga metoder i klasserna.

### **Dependency Inversion Principle (DIP)**
Programmet bygger på abstraktioner snarare än konkreta implementationer. Exempelvis använder repositories `IRepository`, vilket gör systemet modulärt och enkelt att utöka.

---
## Designmönster

### **Factory Pattern**
Användes för att hantera skapandet av repositories baserat på vald databas (SQLite eller MongoDB). Detta bidrog till ökad flexibilitet och en mer modulär kodbas.

### **Facade Pattern**
En facad introducerades för att ge en enkel och enhetlig ingångspunkt till affärslogiken, vilket underlättade interaktionen med mer komplex funktionalitet.

### **Repository Pattern**
Repository-mönstret isolerade databaslogiken från resten av applikationen. Detta gjorde hanteringen av CRUD-operationer mer strukturerad och förbättrade möjligheten till enhetstester.

---
## Tekniska Utmaningar

- **Entity Framework:** Detta orsakade en del problem i början. Gruppen spenderade en dag på att studera material och Kenny lyckades lösa problemen till kvällen.
- **Menylogik och Felmeddelanden:** Vi stötte på mindre utmaningar med att hantera menyn och skapa användarvänliga felmeddelanden.

---
## Min Arbetsplanering
Vi följde en planering som var baserad på projektets "EPICS". Vi använde Trello för att organisera uppgifterna och arbetade i en strukturerad ordning.

---
## Mina Bidrag
Vi arbetade med mob-programmering och roterade uppgifterna inom gruppen. Jag försökte bidra med energi och ett gott humör för att hålla stämningen positiv.

---
## Utmaningar och Lösningar
Att arbeta med mycket kunniga medlemmar gjorde processen smidig. Det hade dock varit värdefullt att få mer lektionstid för att gå igenom områden som vi inte redan behärskade.

---
## Vad skulle jag göra annorlunda nästa gång?
Jag är nöjd med vårt arbetssätt och känner att jag gav 100 % fokus under projektet. Nästa gång skulle jag vilja fortsätta på samma sätt och hoppas kunna utveckla mina kunskaper ytterligare.
