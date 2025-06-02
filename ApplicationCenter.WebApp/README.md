## Entity Framework

```
dotnet ef migrations add InitialCreate --context UserDataContext --output-dir Migrations/UserData
dotnet ef database update --context UserDataContext

dotnet ef migrations add Test --context ConfigurationDataContext --output-dir Migrations/ConfigurationData
dotnet ef database update --context ConfigurationDataContext
```