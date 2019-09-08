namespace Prototype1
{
	public class BasicBlock : Block
	{
		private int strength = 1;

		protected override void OnHit()
		{
			strength--;

			// Destroy the gameobject when this block has no more hitpoints
			if (strength <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}