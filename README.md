![.NET Core](https://github.com/EmilianoMusso/db2ent/workflows/.NET%20Core/badge.svg)

## db2ent
###### Database table records to List<> of new POCO with properties

**db2ent** is a tool useful to read a table from a database, translating the read records to a C# List<>, composed by
objects with type derived from table name, and properties setting from the record itself.
It can be used to quickly generate seed instructions, to be used for example in ORMs like Entity Framework.

###### Usage
```
db2ent --connectionstring="Data Source=INSTANCE;Initial Catalog=MYDB;User Id=MYUSER;Password=MYPASS" --tablename=MYTABLE [--numrecords=99999] [--where=CONDITIONAL_CLAUSE]
```

_tablename_ parameter accept also a list of tables, with comma-separated names, in the form of:
```
--tablename=MYTABLE01,MYTABLE02,MYTABLE03 [...]
```

Currently, db2ent outputs to console only. To save its results to file, you can pipe the execution towards a file path

###### Example
Lets consider a SQL table, named _Comuni_ with the following fields:

```sql
[CodStato] [varchar](3) NOT NULL,
[CodProvincia] [varchar](2) NOT NULL,
[Comune] [varchar](25) NOT NULL,
[CodRegione] [varchar](6) NOT NULL,
[DesEstesa] [varchar](40) NOT NULL,
[Cap] [varchar](10) NOT NULL,
[PrefTelefonico] [varchar](4) NOT NULL,
[CodiceIrpef] [varchar](4) NOT NULL,
[CodiceIstat] [int] NOT NULL,
[FirmaUltVarData] [date] NOT NULL,
[FirmaUltVarOra] [int] NOT NULL,
[FirmaUltVarStazione] [varchar](2) NOT NULL,
[FirmaUltVarOperatore] [varchar](20) NOT NULL,
[CodLatitudine] [decimal](15, 4) NOT NULL,
[CodLongitudine] [decimal](15, 4) NOT NULL,
[DataInizioValidita] [date] NOT NULL,
[DataFineValidita] [date] NOT NULL
```

We can execute db2ent to filter only those records which have field CodProvincia = 'AT', like this

```
db2ent --connectionstring="CONNECTION_STRING" --tablename=Comuni --where=CodProvincia='AT'
```

And our output will be like the following:
```csharp
var comuniList = new List<Comuni>()
{
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "AGLIANO",
                CodRegione = "13",
                DesEstesa = "AGLIANO",
                Cap = "14041",
                PrefTelefonico = "",
                CodiceIrpef = "A072",
                CodiceIstat = 5001,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 44.7912,
                CodLongitudine = 8.2515,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "ALBUGNANO",
                CodRegione = "13",
                DesEstesa = "ALBUGNANO",
                Cap = "14022",
                PrefTelefonico = "",
                CodiceIrpef = "A173",
                CodiceIstat = 5002,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 45.0785,
                CodLongitudine = 7.9722,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "ANTIGNANO",
                CodRegione = "13",
                DesEstesa = "ANTIGNANO",
                Cap = "14010",
                PrefTelefonico = "",
                CodiceIrpef = "A312",
                CodiceIstat = 5003,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 44.8463,
                CodLongitudine = 8.1360,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "ARAMENGO",
                CodRegione = "13",
                DesEstesa = "ARAMENGO",
                Cap = "14020",
                PrefTelefonico = "",
                CodiceIrpef = "A352",
                CodiceIstat = 5004,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 45.1018,
                CodLongitudine = 8.0011,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "ASTI",
                CodRegione = "13",
                DesEstesa = "ASTI",
                Cap = "14100",
                PrefTelefonico = "",
                CodiceIrpef = "A479",
                CodiceIstat = 5005,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 44.8989,
                CodLongitudine = 8.2079,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "AZZANO D'ASTI",
                CodRegione = "13",
                DesEstesa = "AZZANO D'ASTI",
                Cap = "14030",
                PrefTelefonico = "",
                CodiceIrpef = "A527",
                CodiceIstat = 5006,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 44.8744,
                CodLongitudine = 8.2679,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        new Comuni()
        {
                CodStato = "IT",
                CodProvincia = "AT",
                Comune = "BALDICHIERI D'ASTI",
                CodRegione = "13",
                DesEstesa = "BALDICHIERI D'ASTI",
                Cap = "14011",
                PrefTelefonico = "",
                CodiceIrpef = "A588",
                CodiceIstat = 5007,
                FirmaUltVarData = new DateTime(1800, 1, 1, 0, 0, 0),
                FirmaUltVarOra = 0,
                FirmaUltVarStazione = "",
                FirmaUltVarOperatore = "",
                CodLatitudine = 44.9069,
                CodLongitudine = 8.0927,
                DataInizioValidita = new DateTime(1800, 1, 1, 0, 0, 0),
                DataFineValidita = new DateTime(1800, 1, 1, 0, 0, 0),
        },
        // Truncated data for demonstrative purposes
}
```
