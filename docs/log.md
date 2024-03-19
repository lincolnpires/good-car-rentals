Started at around 3:30 PM on Sunday.
- Using C# and .NET Core with scaffolding to speed up the process
- Starting out on VSCode and then moving to Rider - more robust.
  - Having VSCode as editor (git, md, etc.), but Rider as IDE
- Log of initial tasks + setup and sketching the structure took about 50 minutes.
Some coding
- Adding models, db and packages needed, etc. ~20 minutes
- Scaffolded the MVC cars and fixed issues ~5 minutes
- Testing - everything good.

Just a side note: the scaffolding is great to speed up the process, but it's not the best way to build a "good" product.
The Controllers are doing too much, and the models are not properly structured. The database is not normalized, and the code is not tested.
However all that can be refactored and improved later on.
My idea is that there should be a service layer, so that if we need to move to be API-accessible, we can.
And most people would prefer to have a persistence layer/repository as well.

- Monday: didn't do much, mostly just reviewed everything and scaffolded Rentals. Also noticed the screens are not that nice, but not the focus. Changed the models a bit - I don't know what exactly I want for the rentals. ~20 minutes

- Tuesday: checked the DB, the models, added viewModels, and started the app/service part ... Lost a bunch of time reconfiguring EF. ~1h
- I started using copilot to speed me up as well, it does know how I code, so it's a bit faster. Doing some refactorings.

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
Sumup: ~155m (2h35m)
