Started at around 3:30 PM
- Using C# and .NET Core with scaffolding to speed up the process
- Starting out on VSCode and then moving to Rider - more robust.
  - Having VSCode as editor (git, md, etc.), but Rider as IDE
- Log of initial tasks + setup and sketching the structure took about 50 minutes.
Some coding
- Adding models, db and packages needed, etc. ~20 minutes
- Scaffolded the MVC cars and fixed issues ~5 minutes
- Testing

```bash
gh repo create
dotnet new mvc -n GoodCarRentals
dotnet new sln -n GoodCarRentals
dotnet new xunit -o . -n GoodCarRentals.Tests
dotnet sln add .\src\GoodCarRentals.csproj
dotnet sln add .\tests\GoodCarRentals.Tests.csproj
dotnet dev-certs https -t
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-ef
dotnet add .\src\GoodCarRentals.csproj package Microsoft.EntityFrameworkCore.Sqlite

dotnet ef dbcontext scaffold \
  "Data Source=goodcarrentals.db" \
  Microsoft.EntityFrameworkCore.Sqlite \
  -o Data \
  -c CarRentalsContext

dotnet aspnet-codegenerator \
  --project src\GoodCarRentals.csproj \
  --target-framework net8.0 \
  --no-build \
  controller  \
  --controllerName CustomersController \
  --relativeFolderPath "Controllers" \
  --model GoodCarRentals.Data.Models.Customer \
  --dataContext GoodCarRentals.Data.CarRentalsContext \
  --useDefaultLayout \
  --controllerNamespace GoodCarRentals.Controllers
```
