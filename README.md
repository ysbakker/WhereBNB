# WhereBNB
InsideAirBNB visualisation and management application with Blazor and .NET Core 6

## Dataset

This project uses the [InsideAirBNB dataset](http://insideairbnb.com/get-the-data.html) for the Amsterdam region.

## Configuration

### API

1. Make sure you have a database with the InsideAirBNB data
1. Copy the `appsettings.json` to `appsettings.Development.json` (or any other environment)
1. Change the `ConnectionStrings:WhereBNB` entry to match your connection (for example `Server=localhost;Database=WhereBNB;User=SA;Password=password123`)
1. Run `dotnet ef database update` to confirm that everything works and to apply the migrations.