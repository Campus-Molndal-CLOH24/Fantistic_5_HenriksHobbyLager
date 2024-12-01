# Individuell Rapport - Kenny Sohl

## Hur fungerade gruppens arbete?

Gruppens arbete fungerade mycket bra. Vi hade dagliga samlingar varje morgon kl. 9 och anv�nde oss av "mob-programmering" som arbetss�tt. Detta bidrog till effektiv kommunikation och samarbete, vilket f�rb�ttrade b�de kodkvalitet och probleml�sning.

---

## Beskriv gruppens databasimplementation

Vi implementerade st�d f�r b�de **SQLite** och **MongoDB** i projektet. Vid programmets start kan anv�ndaren v�lja vilken databas som ska anv�ndas. Vi anv�nde oss av en *factory*-design f�r att hantera detta val.

### SQLite

- I SQLite anv�nde vi **Entity Framework** f�r att interagera med databasen.  
- F�r att s�kerst�lla att tabellen f�r produkter (Products) existerar implementerade vi metoden `EnsureProductsTableExists`.  
- Vi lade �ven till indexering f�r f�lten **Name**, **Category** och **Price** f�r att optimera s�kprestandan.  

### MongoDB

- F�r MongoDB anv�nde vi **MongoDB Driver** som databasbibliotek.  
- Vi lagrade anslutningsstr�ngen (connection string) i en **.env-fil** f�r att kunna hantera olika utvecklingsmilj�er och inst�llningar p� ett flexibelt s�tt.  
- Precis som i SQLite implementerade vi `EnsureProductsTableExists` f�r att s�kerst�lla att en motsvarande struktur finns.  
- �ven h�r skapade vi indexering f�r **Name**, **Category** och **Price** f�r b�ttre prestanda.  

---

## Vilka SOLID-principer implementerade ni och hur?

### **Single Responsibility Principle (SRP)**  
Varje klass ansvarar endast f�r en specifik uppgift. Exempel: `ProductRepositoryFactory` ansvarar f�r att skapa r�tt typ av repository baserat p� databasen.  

### **Open/Closed Principle (OCP)**  
Klasser �r �ppna f�r utbyggnad men st�ngda f�r modifiering. Detta uppn�dde vi genom att anv�nda interface, till exempel `IRepository`, f�r att m�jligg�ra flexibel implementation utan att �ndra befintlig kod.  

### **Liskov Substitution Principle (LSP)**  
Alla implementationer av `IRepository` kan anv�ndas p� samma s�tt, utan att �ndra klientkoden. Detta s�kerst�ller att alla repositories �r utbytbara.  

### **Interface Segregation Principle (ISP)**  
Vi delade upp interface s� att varje klass endast implementerar de metoder som den faktiskt beh�ver.  

### **Dependency Inversion Principle (DIP)**  
H�gre niv�er av programmet �r beroende av abstraktioner ist�llet f�r konkreta implementationer. Repository-klasserna �r beroende av `IRepository`, vilket g�r systemet mer flexibelt och l�tt att ut�ka.  

---

## Vilka designm�nster anv�nde ni och varf�r?

### **Repository Pattern**  
Vi anv�nde detta m�nster f�r att separera databaslogiken fr�n resten av applikationen, vilket f�renklade hanteringen av CRUD-operationer och f�rb�ttrade testbarheten.

### **Facade Pattern**  
Facaden f�renklade interaktionen med aff�rslogiken och gav en enhetlig ing�ngspunkt till komplex funktionalitet.

### **Factory Pattern**  
Factory-m�nstret anv�ndes f�r att skapa r�tt repository baserat p� anv�ndarens val av databas (SQLite eller MongoDB). Detta �kade flexibiliteten och gjorde koden mer modul�r.

---

## Vilka tekniska utmaningar st�tte ni p� och hur l�ste ni dem?

Vi st�tte p� flera utmaningar, s�rskilt vid integrationen av databaserna eftersom vi hade begr�nsad erfarenhet av detta.  
- **L�sning**: Genom **mob-programmering**, gemensam l�sning av dokumentation och diskussioner hittade vi de b�sta l�sningarna. Vi testade oss ocks� fram f�r att s�kerst�lla att alla kunde bidra och l�ra sig av processen.  

---

## Hur planerade du ditt arbete?

- Vi skapade en **Trello-board** d�r vi strukturerade projektets uppgifter som kort i en backlog.  
- Varje morgon gick vi igenom vad vi hade gjort och satte upp m�len f�r dagen.  
- Vid arbete p� specifika uppgifter skapade vi brancher baserade p� kortens ID fr�n Trello, f�r att enkelt koppla brancher till deras uppgifter.  
- Arbetet utf�rdes huvudsakligen genom **mob-programmering**, d�r en person kodade medan resten bidrog med feedback och f�rslag i realtid.

---

## Vilka delar gjorde du?

- Forkade projektets repository.  
- Skapade interfacet `IProductFacade`.  
- Kopplade `SQLit
- Implementerade CRUD-operationer f�r SQLite.  
- Optimerade s�kningen i SQLite med indexering.  
- Utvecklade `ProductRepositoryFactory` f�r att m�jligg�ra v�xling mellan MongoDB och SQLite.  

---

## Vilka utmaningar st�tte du p� och hur l�ste du dem?

Den st�rsta utmaningen var att integrera databaserna. Detta var en ny erfarenhet f�r mig, men jag l�ste det genom att:  
1. Arbeta tillsammans med gruppen under mob-programmering.  
2. L�sa dokumentation f�r b�de **Entity Framework** och **MongoDB Driver**.  
3. Experimentera och testa olika l�sningar f�r att hitta det som fungerade b�st.  

---

## Vad skulle du g�ra annorlunda n�sta g�ng?

- Planera projektet med en l�ngre deadline f�r att ha mer tid f�r varje implementation.  
- F�rdjupa mig i f�rv�g i verktyg som **Entity Framework** och **MongoDB Driver** f�r att effektivisera arbetet.  
- Dela upp uppgifter b�ttre f�r att kunna jobba separerat och minska beroenden mellan teammedlemmar.  