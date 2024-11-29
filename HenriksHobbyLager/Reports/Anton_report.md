# Individuell Rapport

Svara på frågorna nedan och lämna in det som en del av din inlämning.

## Hur fungerade gruppens arbete?

Gruppens arbete har fungerat mycket bra. Vi har hittat ett bra sätt att samarbeta
genom scrum m.h.a. Trello och tydliga bestämmelser för arbetsflödet i Git. Vi har också utfört mycket av arbetet
genom s.k. 'mob-programmering' där en person streamar utvecklingen i sin branch medan övriga medlemmar kan hjälpa till och diskutera koden.

## Beskriv gruppens databasimplementation

Vi har använt Entity Framework och Mongodb.Driver för att hantera en SQLite-databas samt en MongoDB-databas.
SQLite-databasen skapas och lagras på användarens hårddisk i samma mapp som applikationen körs medan
MongoDB-databasen skapas och lagras i en Docker-container. Användaren konfigurerar vid 'installationen' en .env-fil
i vilken anslutningssträngen till containern lagras och .env-filen läses av applikationen för att
koppla till databasen.

## Vilka SOLID-principer implementerade ni och hur?

### Single responsibility principle
Vi har i mesta möjliga mån försökt att hålla klasserna minimala. D.v.s. att t.ex. ConsoleMenuHandler
endast hanterar användarinput samt meddelanden och gör vid behov funktionsanrop hos andra klasser som sköter databas-logik.

### Open–closed principle
Vi har använt interfaces för nästan alla klasser. Vi skulle kunna utöka dessa klasser genom att implementera fler interfaces.

### Liskov substitution principle
Vi har minst en funktionsparameter som tar emot ett objekt av interface-typ och därmed kan klasser som implementerar
interfacet användas här. Polymorfism.

### Interface segregation principle
Vi har inte gjort detta medvetet men ingen klass i programmet implementerar fler interfaces än de behöver.

### Dependency inversion principle
Vi använder dependency injection där 'mottagaren' endast är beroende av interface-typen och inte den konkreta klassen
som implementerar interfacet.

## Vilka patterns använde ni och varför?
Vi har använt 'repository pattern' för att på ett smidigt sätt kunna separera databasförfrågningar och annan logik.

## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?
Vi hade en del problem med databasintegrationen. Lösningarna dök upp löpande när vi samarbetade i videomöte.

## Hur planerade du ditt arbete?
Jag planerade löpande tillsammans med gruppen. Vi använde oss av Trello och scrum-metoden för att beta av backloggen
steg för steg. Vi skapade kort för varje moment och delade ut korten mellan gruppens medlemmar. För varje kort
skapade vi en ny branch och sedan raderade vi branchen efter vi mergat och alla genomfört en code review på GitHub.
Eftersom vi gjorde många av momenten samtidigt som vi satt i videomöte kunde vi effektivt planera dag för dag.

## Vilka dela gjorde du?
Eftersom vi alla samarbetade på en majoritet av 'korten' är det svårt att säga exakt vem som gjort vad.
Jag har arbetat med alla delar i projektet. En av de större uppgifterna jag utförde var att se till att
alla asynkrona databasanrop hade en tidsgräns och gav ett felmeddelande vid överskriden tid.

## Vilka utmaningar stötte du på och hur löste du dem?
Inga större utmaningar. Det största problemet genom hela projektet var att vi inte riktigt hade koll på Entity Framework eller
MongoDB.Driver. Vi löste det genom att läsa på om dessa samt att Kenny lyckades få igång SQLite-databasen m.h.a. Entity Framework.

## Vad skulle du göra annorlunda nästa gång?
I detta projektet fick vi en färdig backlog. Om vi får det igen skulle jag vilja gå igenom den i förväg och dela upp den
och modifiera efter hur vi tror arbetet kommer se ut. Denna gången gjorde vi det löpande under projektets gång men
det hade varit bra att ha en tydligare TO-DO-lista att börja med. 