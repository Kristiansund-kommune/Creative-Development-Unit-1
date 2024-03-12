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
        new User{Name="Carson Alexander",Email="johndoe1234@emailfake.com",RegistrationDate=DateTime.Parse("2023-04-01")},
        new User{Name="Meredith Alonso",Email="sarahsmith789@emailfake.com",RegistrationDate=DateTime.Parse("2023-07-01")},
        new User{Name="Arturo Anand",Email="mikeanderson456@emailfake.com",RegistrationDate=DateTime.Parse("2022-02-10")},
        new User{Name="Gytis Barzdukas",Email="emilyjones321@emailfake.com",RegistrationDate=DateTime.Parse("2023-09-01")},
        new User{Name="Yan Li",Email="alexwilson789@emailfake.com",RegistrationDate=DateTime.Parse("2024-01-01")},
        new User{Name="Peggy Justice",Email="lindajohnson234@emailfake.com",RegistrationDate=DateTime.Parse("2023-12-01")},
        new User{Name="Laura Norman",Email="mattcooper123@emailfake.com",RegistrationDate=DateTime.Parse("2023-11-01")},
        new User{Name="Nino Olivetto",Email="amandawoods567@emailfake.com",RegistrationDate=DateTime.Parse("2023-12-05")},
        new User{Name="Denis Senokosov",Email="senokosovden@gmail.com",RegistrationDate=DateTime.Parse("2023-12-05")}
        };
        foreach (User u in users)
        {
            context.Users.Add(u);
        }
        context.SaveChanges();

        var stations = new BikeStation[]
        {
	        new BikeStation {  StationName = "Futura rv.70", Lat = 7.7827349, Lon = 63.110742},
	        new BikeStation {  StationName = "Campus Kristiansund", Lat = 7.7340521, Lon = 63.1147579 },
	        new BikeStation {  StationName = "Kristiansund lufthavn", Lat = 7.8258807, Lon = 63.1140906 },
	        new BikeStation {  StationName = "Monges plass", Lat = 7.7902447, Lon = 63.1074134 },
        };

        foreach (BikeStation c in stations)
        {
	        context.BikeStations.Add(c);
        }
        context.SaveChanges();


        /*
        var docks = new BikeStationDock[]
        {
	        new BikeStationDock { BikeStationId = 1, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 1, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 1, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 1, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 1, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 1, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 2, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 3, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 3, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 3, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 3, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 3, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 3, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 4, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 4, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 4, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 4, Status = StationDockStatus.Available },
	        new BikeStationDock { BikeStationId = 4, Status = StationDockStatus.Available },
        };

        foreach (BikeStationDock c in docks)
        {
	        context.Docks.Add(c);
        }
        context.SaveChanges();
        */




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
	        new Bike{OwnerId= 9, BikeType = BikeType.Hybrid},
	        new Bike{OwnerId= 9, BikeType = BikeType.Road},
        };
        foreach (Bike e in bikes)
        {
	        context.Bikes.Add(e);
        }
        context.SaveChanges();

        var level = new UserLevel[]
        {
	        new UserLevel{Title="Newcomer" , RequiredPoints = 0},
	        new UserLevel{Title="Apprentice", RequiredPoints = 100},
	        new UserLevel{Title="Journeyman", RequiredPoints = 500},
	        new UserLevel{Title="Specialist", RequiredPoints = 1000},
	        new UserLevel{Title="Adept", RequiredPoints = 2500},
	        new UserLevel{Title="Master", RequiredPoints = 5000},
	        new UserLevel{Title="Prodigy", RequiredPoints = 10000},
	        new UserLevel{Title="Elite", RequiredPoints = 20000},
	        new UserLevel{Title="Champion", RequiredPoints = 50000},
	        new UserLevel{Title="Legend", RequiredPoints = 100000},
        };
        foreach (UserLevel e in level)
        {
	        context.Levels.Add(e);
        }
        context.SaveChanges();

        var routes = new Route[]
        {
	        new Route {UserId= 9, StartPointLon = 63.1106918, EndPointLon = 63.118105, StartPointLat = 7.7630048, EndPointLat = 7.7494431, ActivityDistance = 12446.0f, },
        };
        foreach (Route e in routes)
        {
	        context.Routes.Add(e);
        }
        context.SaveChanges();


    }
}

