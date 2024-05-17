using _Project.Scripts.GameResources;

namespace _Project.Scripts
{
	public interface ICollectible
	{
		public (ResourceType, int) Collect();
	}
}
