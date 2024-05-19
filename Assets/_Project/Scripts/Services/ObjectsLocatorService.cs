using System.Collections.Generic;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic;

namespace _Project.Scripts.Services
{
	public class ObjectsLocatorService
	{
		public Player Player { get; set; }

		public List<Enemy> Enemies { get; set; } = new();

		public MainBuilding MainBuilding { get; set; }

		public List<GameResource> GameResources { get; set; } = new();
	}
}