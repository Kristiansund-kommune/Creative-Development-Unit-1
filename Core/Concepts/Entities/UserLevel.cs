using System.ComponentModel.DataAnnotations;

namespace Core.Concepts.Entities;

public class UserLevel
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }
	[Required]
	public long RequiredPoints { get; set; }


	public static List<UserLevel> GetLevels()
	{
		var level = new List<UserLevel>()
		{
			new UserLevel{Title="Newcomer" , RequiredPoints = 0},
			new UserLevel{Title="Apprentice", RequiredPoints = 100},
			new UserLevel{Title="Journeyman", RequiredPoints = 500},
			new UserLevel{Title="Specialist", RequiredPoints = 1000},
			new UserLevel{Title="Adept", RequiredPoints = 2500},
			new UserLevel{Title="Master", RequiredPoints = 10000},
			new UserLevel{Title="Prodigy", RequiredPoints = 25000},
			new UserLevel{Title="Elite", RequiredPoints = 100000},
			new UserLevel{Title="Champion", RequiredPoints = 250000},
			new UserLevel{Title="Legend", RequiredPoints = 100000},
		};

		return level;
	}

}


