namespace Breakin
{
	public class BasicBlock : Block
	{
		public int Value { get; set; }
		
		private int strength = 1;

		protected override void OnHit()
		{
			strength--;

			// Destroy the gameobject when this block has no more hitpoints
			if (strength <= 0)
			{
				ScoreManager.Instance.Score += Value;
				IsActive = false;
			}
		}
	}
}