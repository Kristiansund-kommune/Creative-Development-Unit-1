using System.ComponentModel.DataAnnotations;
using Core.Concepts.Entities;

namespace Core.Concepts.Repository.DbContext;

public static class DbInitializer
{
    public static void Initialize(StationsContext context)
    {
        context.Database.EnsureCreated();

        // Look for any users.
        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }

        var users = new User[]
        {
	        /*johndoe1234@emailfake.com
	        sarahsmith789@emailfake.com
	        mikeanderson456@emailfake.com
	        emilyjones321@emailfake.com
	        alexwilson789@emailfake.com
	        lindajohnson234@emailfake.com
	        chrisbrown876@emailfake.com
	        amandawoods567@emailfake.com
	        mattcooper123@emailfake.com
	        jenniferlee456@emailfake.com*/
        new User{Name="Carson Alexander",Email="johndoe1234@emailfake.com",RegistrationDate=DateTime.Parse("2005-09-01")},
        new User{Name="Meredith Alonso",Email="sarahsmith789@emailfake.com",RegistrationDate=DateTime.Parse("2002-09-01")},
        new User{Name="Arturo Anand",Email="mikeanderson456@emailfake.com",RegistrationDate=DateTime.Parse("2003-09-01")},
        new User{Name="Gytis Barzdukas",Email="emilyjones321@emailfake.com",RegistrationDate=DateTime.Parse("2002-09-01")},
        new User{Name="Yan Li",Email="alexwilson789@emailfake.com",RegistrationDate=DateTime.Parse("2002-09-01")},
        new User{Name="Peggy Justice",Email="lindajohnson234@emailfake.com",RegistrationDate=DateTime.Parse("2001-09-01")},
        new User{Name="Laura Norman",Email="mattcooper123@emailfake.com",RegistrationDate=DateTime.Parse("2003-09-01")},
        new User{Name="Nino Olivetto",Email="amandawoods567@emailfake.com",RegistrationDate=DateTime.Parse("2005-09-01")}
        };
        foreach (User u in users)
        {
            context.Users.Add(u);
        }
        context.SaveChanges();

        var stations = new BikeStation[]
        {
	        new BikeStation { StationName = "Station 1", Lat = 40.712776, Lon = -74.005974},
	        new BikeStation { StationName = "Station 2", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 3", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 4", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 5", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 6", Lat = 40.712776, Lon = -74.005974},
	        new BikeStation { StationName = "Station 7", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 8", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 9", Lat = 40.712776, Lon = -74.005974 },
	        new BikeStation { StationName = "Station 10", Lat = 40.712776, Lon = -74.005974 },
        };
        foreach (BikeStation c in stations)
        {
	        context.BikeStations.Add(c);
        }
        context.SaveChanges();

        var bikes = new Bike[]
        {
	        new Bike{OwnerId= 1, BikeType = BikeType.Hybrid},
	        new Bike{OwnerId= 2, BikeType = BikeType.Mountain},
	        new Bike{OwnerId= 3, BikeType = BikeType.Road},
	        new Bike{OwnerId= 2, BikeType = BikeType.Hybrid},
	        new Bike{OwnerId= 5, BikeType = BikeType.Hybrid},
	        new Bike{OwnerId= 4, BikeType = BikeType.Road},
	        new Bike{OwnerId= 6, BikeType = BikeType.Road},
	        new Bike{OwnerId= 1, BikeType = BikeType.Mountain},
	        new Bike{OwnerId= 2, BikeType = BikeType.Road},
	        new Bike{OwnerId= 5, BikeType = BikeType.Mountain},
	        new Bike{OwnerId= 4, BikeType = BikeType.Mountain},
	        new Bike{OwnerId= 3, BikeType = BikeType.Hybrid},
	        new Bike{OwnerId= 7, BikeType = BikeType.Road},
	        new Bike{OwnerId= 8, BikeType = BikeType.Hybrid},

        };
        foreach (Bike e in bikes)
        {
	        context.Bikes.Add(e);
        }
        context.SaveChanges();
    }
}

