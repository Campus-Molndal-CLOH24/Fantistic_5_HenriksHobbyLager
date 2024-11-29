# Individuell Rapport - Kenny Sohl

## Hur fungerade gruppens arbete?

Gruppens arbete fungerade mycket bra. Vi hade dagliga samlingar varje morgon kl. 9 och använde oss av "mob-programmering" som arbetssätt. Detta bidrog till effektiv kommunikation och samarbete, vilket förbättrade både kodkvalitet och problemlösning.

---

## Beskriv gruppens databasimplementation

Vi implementerade stöd för både **SQLite** och **MongoDB** i projektet. Vid programmets start kan användaren välja vilken databas som ska användas. Vi använde oss av en *factory*-design för att hantera detta val.

### SQLite

- I SQLite använde vi **Entity Framework** för att interagera med databasen.  
- För att säkerställa att tabellen för produkter (Products) existerar implementerade vi metoden `EnsureProductsTableExists`.  
- Vi lade även till indexering för fälten **Name**, **Category** och **Price** för att optimera sökprestandan.  

### MongoDB

- För MongoDB använde vi **MongoDB Driver** som databasbibliotek.  
- Vi lagrade anslutningssträngen (connection string) i en **.env-fil** för att kunna hantera olika utvecklingsmiljöer och inställningar på ett flexibelt sätt.  
- Precis som i SQLite implementerade vi `EnsureProductsTableExists` för att säkerställa att en motsvarande struktur finns.  
- Även här skapade vi indexering för **Name**, **Category** och **Price** för bättre prestanda.  

---

## Vilka SOLID-principer implementerade ni och hur?

### **Single Responsibility Principle (SRP)**  
Varje klass ansvarar endast för en specifik uppgift. Exempel: `ProductRepositoryFactory` ansvarar för att skapa rätt typ av repository baserat på databasen.  

### **Open/Closed Principle (OCP)**  
Klasser är öppna för utbyggnad men stängda för modifiering. Detta uppnådde vi genom att använda interface, till exempel `IRepository`, för att möjliggöra flexibel implementation utan att ändra befintlig kod.  

### **Liskov Substitution Principle (LSP)**  
Alla implementationer av `IRepository` kan användas på samma sätt, utan att ändra klientkoden. Detta säkerställer att alla repositories är utbytbara.  

### **Interface Segregation Principle (ISP)**  
Vi delade upp interface så att varje klass endast implementerar de metoder som den faktiskt behöver.  

### **Dependency Inversion Principle (DIP)**  
Högre nivåer av programmet är beroende av abstraktioner istället för konkreta implementationer. Repository-klasserna är beroende av `IRepository`, vilket gör systemet mer flexibelt och lätt att utöka.  

---

## Vilka designmönster använde ni och varför?

### **Repository Pattern**  
Vi använde detta mönster för att separera databaslogiken från resten av applikationen, vilket förenklade hanteringen av CRUD-operationer och förbättrade testbarheten.

### **Facade Pattern**  
Facaden förenklade interaktionen med affärslogiken och gav en enhetlig ingångspunkt till komplex funktionalitet.

### **Factory Pattern**  
Factory-mönstret användes för att skapa rätt repository baserat på användarens val av databas (SQLite eller MongoDB). Detta ökade flexibiliteten och gjorde koden mer modulär.

---

## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?

Vi stötte på flera utmaningar, särskilt vid integrationen av databaserna eftersom vi hade begränsad erfarenhet av detta.  
- **Lösning**: Genom **mob-programmering**, gemensam läsning av dokumentation och diskussioner hittade vi de bästa lösningarna. Vi testade oss också fram för att säkerställa att alla kunde bidra och lära sig av processen.  

---

## Hur planerade du ditt arbete?

- Vi skapade en **Trello-board** där vi strukturerade projektets uppgifter som kort i en backlog.  
- Varje morgon gick vi igenom vad vi hade gjort och satte upp målen för dagen.  
- Vid arbete på specifika uppgifter skapade vi brancher baserade på kortens ID från Trello, för att enkelt koppla brancher till deras uppgifter.  
- Arbetet utfördes huvudsakligen genom **mob-programmering**, där en person kodade medan resten bidrog med feedback och förslag i realtid.

---

## Vilka delar gjorde du?

- Forkade projektets repository.  
- Skapade interfacet `IProductFacade`.  
- Kopplade `SQLit
- Implementerade CRUD-operationer för SQLite.  
- Optimerade sökningen i SQLite med indexering.  
- Utvecklade `ProductRepositoryFactory` för att möjliggöra växling mellan MongoDB och SQLite.  

---

## Vilka utmaningar stötte du på och hur löste du dem?

Den största utmaningen var att integrera databaserna. Detta var en ny erfarenhet för mig, men jag löste det genom att:  
1. Arbeta tillsammans med gruppen under mob-programmering.  
2. Läsa dokumentation för både **Entity Framework** och **MongoDB Driver**.  
3. Experimentera och testa olika lösningar för att hitta det som fungerade bäst.  

---

## Vad skulle du göra annorlunda nästa gång?

- Planera projektet med en längre deadline för att ha mer tid för varje implementation.  
- Fördjupa mig i förväg i verktyg som **Entity Framework** och **MongoDB Driver** för att effektivisera arbetet.  
- Dela upp uppgifter bättre för att kunna jobba separerat och minska beroenden mellan teammedlemmar.  