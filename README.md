## db2ent
###### Database table records to List<> of new POCO with properties

**db2ent** is a tool useful to read a table from a database, translating the read records to a C# List<>, composed by
objects with type derived from table name, and properties setting from the record itself.
It can be used to quickly generate seed instructions, to be used for example in ORMs like Entity Framework.

Usage:
```
db2ent --connectionstring="Data Source=INSTANCE;Initial Catalog=MYDB;User Id=MYUSER;Password=MYPASS" --tablename=MYTABLE
```

Currently, **db2ent** outputs to console only. To save its results to file, you can pipe the execution towards a file path