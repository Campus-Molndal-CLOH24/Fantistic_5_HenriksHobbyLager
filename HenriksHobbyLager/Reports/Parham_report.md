# Individuell Rapport

Svara på frågorna nedan och lämna in det som en del av din inlämning.

## Hur fungerade gruppens arbete?

Det fungerade smidigt, vi *mob programmerade* där vi hade en person som arbetade med att lösa en uppgift i taget och använde oss av Trello dels för att dela in uppgiften i mindre bitar och ansvarsfördela lite.
Vi jobbade alla i vår egna bransch och genförde pull requests gemensamt innan det pushades in i dev.
Vi genomförde alla beslut enligt konsensus .

## Beskriv gruppens databasimplementation

Vi har implementerat stöd för både SQLite och MongoDB i vår applikation, vilket möjliggör flexibilitet för användaren att välja databas vid programmets start. En factory-design användes för att hantera valet av databas.

SQLite:

Använde Entity Framework för databasinteraktion.
Databasen skapas och lagras lokalt på användarens hårddisk.
En metod, EnsureProductsTableExists, implementerades för att säkerställa att en produkt-tabell finns.
Indexering för fälten Name, Category, och Price lades till för att optimera sökprestanda.
MongoDB:

Använde MongoDB Driver för databasinteraktion.
Databasen körs i en Docker-container och anslutningssträngen hanteras via en .env-fil, vilket underlättar konfigurationen för olika miljöer.
En motsvarande EnsureProductsTableExists-metod och indexering för Name, Category, och Price implementerades här också för bättre prestanda.


## Vilka SOLID-principer implementerade ni och hur?
Vi säkerställde att databasdesignen följer SOLID principerna:
Single Responsibility Principle (SRP)
Där vi såg till så att varje klass endast har en huvudsaklig uppgift. T.ex ConsoleMenuHandler.

Open/Closed Principle (OCP)
Vi såg till så att klasserna är låsta men ändå öppna för modifiering. För att lyckas med detta använde vi oss av interface, se IProductFacade exempelvis.

Liskov Substitution Principle (LSP)
Alla implementationer av IRepository kan användas på samma sätt, utan  att ändra koden/dess beteende.

Interface Segregation Principle (ISP)
Ingen av våra klasser implementerar enbart de metoder som faktiskt behövs.

Dependency Inversion Principle (DIP)
Vi följer Dependency Inversion Principle (DIP) genom att högre nivåer i programmet är
beroende av abstraktioner istället för konkreta implementationer. Repository-klasserna 
är beroende av IRepository, vilket skapar en lös koppling mellan komponenter och gör 
systemet flexibelt och enkelt att utöka.

## Vilka patterns använde ni och varför?
DRY (Don't Repeat Yourself)
Varför?
Återkommande kod som att visa produktinformation har abstraherats till en metod (DisplayProduct),vilket minskar duplicering och gör koden mer modulär och lättare att uppdatera.

Factory Pattern
Vi har använt en factory-klass (ProductRepositoryFactory) för att skapa och hantera instanser av repositories
(t.ex. SQLiteProductRepository och MongoDBProductRepository). 
Detta gör att rätt repository skapas baserat på användarens val och koden blir mer flexibel och skalbar.

Facade Pattern
ProductFacade fungerar som en fasad som döljer komplexiteten hos logiken i repositories och andra lager.

Dependency injection:

Vi har använt oss av Dependency injection genom att ha skickat in en instans av IProductFacade till ConsoleMenuHandler klassen. Vilket skapar en mer lös koppling och gör det enklare att byta ut
implementationer av IProductFacade utan att ändra koden i ConsoleMenuHandler.


## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?
Det fanns delar i uppgiften där jag kände att min kunskap var väldigt bristfällig, vilket gjorde att uppgiften drogs ut på tiden något, vi har ett konkret exempel där vi alla påverkades:
vid databas integration och implementationen, där vi tog en paus för att självständigt läsa oss in i ämnet, för att ha lite mer kött på benen inför nästa stämning.

## Hur planerade du ditt arbete?
Vi planerade det mesta tillsammans i grupp, vi hade en trello board där vi delat in uppgiften i mycket mindre och övergripliga bitar.
Varje dag så stämde vi av för att utvärdera statusen av hela arbetet, säkerställa att vi inte ligger efter i vår planering.

## Vilka dela gjorde du?
Lite svårt att välja ut specifika områden, vi hade kort på trello boarden men även dessa genomfördes till stor del gemensamt, där jag delade skärm och utvecklade i min dator vid vissa specifika delar så som:
Skapandet av Sqlite databas kontext, skapa mgigreringsfiler för en initial databasstruktur,skapat produktfacade klass, men även varit med och felsökt applikationen.
## Vilka utmaningar stötte du på och hur löste du dem?
Största utmaningen var kunskapsgap, där jag kände att det fanns så mycket brister i min kunskap och det hade varit svårt att slutföra det på egen hand utan att ha tagit hjälp av Chatgpt som hjälpte mig 
mycket konceptuellt, hur ett visst avsnitt kan göras, jag simplifierar en viss uppgift och frågar om råd från Chatgpt t.ex hur kan jag implementera denna klassen och samtidigt inte bryta mot någon SOLID princip.
Det gav mig värdefulla inputs men även rätt tänk för att tackla uppgiften. Hade mycket stöd från gruppen som helhet vilket jag tror resulterade i att uppgiften nästan aldrig krävde något mer än ett gemensamt tänk.
## Vad skulle du göra annorlunda nästa gång?
Först och främst kanske fortsätta utveckla sina kunskaper inte minst inom Mongodb och databasintegrationer i C# som helhet.