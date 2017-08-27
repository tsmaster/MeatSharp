using UnityEngine;

namespace MeatSharp.Physics
{
	public class BDG_ConstraintWall
	{
		public enum WhichWall
		{
			North,
			West,
			South,
			East
		}

		WhichWall wall;
		float dist;

		public BDG_ConstraintWall(WhichWall w, float dist)
		{
			this.wall = w;
			this.dist = dist;
		}

		public void ConstrainParticle(BDG_Particle2D p)
		{
			float px = p.CurrentPosition.x;
			float py = p.CurrentPosition.y;

			switch (wall)
			{
				case WhichWall.North:
					py = Mathf.Min(dist, py);
					break;
				case WhichWall.East:
					px = Mathf.Min(dist, px);
					break;
				case WhichWall.West:
					px = Mathf.Max(-dist, px);
					break;
				case WhichWall.South:
					py = Mathf.Max(-dist, py);
					break;
			}

			p.CurrentPosition = new Vector2(px, py);
		}
	}
}

