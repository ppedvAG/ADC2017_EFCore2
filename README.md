 
# ADC Core 2017 Entity Framework 2.0

In diesem Repository finden Sie die auf der ADC Core 2017 vorgestellten Beispiespielprojekte zum Entity Framework 2.0 Vortrag.

# Themen
 1. Beispsielprojekte
  * Global Query Filters & Shaddow Properties
  * Field Mapping
  * EF Functions
  * From Sql
  * Db Scalar Functions
  * Owned Types
 2. Preformance Messung
  * SampleData (ein Projekt um eine Datenbank zu erstellen und mit Testdaten zu befüllen)
  * EF 6 (mit und ohne ComplexProperties)
  * EF Core 1.1.2 
  * EF Core 2.0 (mit und ohne OwnedTypes)

# Messergebnisse
Die Messungen wurden alle auf einem SQL Server ausgeführt.

| Projektaufbau | Gemessene Zeit  | Gemessene Zeit (No Tracking) |
| ------ | ------ | ------ |
| EF 6   |  157   |  78    |
| EF 6 with Complex Properties   | 140    | 60     |
| EF Core 1.1.2   | 116    | 65     |
| EF Core 2.0   | 108    | 52     |
| EF Core 2.0 with Owned Properties   | 440    | 166     |

``` Alle Zeiten in ms. Benötigte Zeit zum Laden von 1200 Datensätzen. ```
