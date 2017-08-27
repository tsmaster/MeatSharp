using UnityEngine;

namespace MeatSharp.Physics
{
	public class BDG_ConstraintDistance
	{
		public float Distance;
		public BDG_Particle2D P0;
		public BDG_Particle2D P1;
		public string GUID;

		public BDG_ConstraintDistance(float distance, BDG_Particle2D p0, BDG_Particle2D p1, string GUID)
		{
			this.Distance = distance;
			this.P0 = p0;
			this.P1 = p1;
			this.GUID = GUID;
		}

		public void Satisfy()
		{
			Vector2 v0 = P0.CurrentPosition;
			Vector2 v1 = P1.CurrentPosition;
			//Debug.Log(string.Format("P0 {0} P1 {1}", P0.GUID, P1.GUID));
			Vector2 delta = v1 - v0;
			float dist = delta.magnitude;
			float smallThresh = 0.01f;

			while (dist < smallThresh)
			{
				v0 += smallThresh * Random.insideUnitCircle;
				v1 += smallThresh * Random.insideUnitCircle;
				delta = v1 - v0;
				dist = delta.magnitude;
			}

			float diff = (dist - Distance) / dist;

			float moveFrac0 = P0.InverseMass / (P0.InverseMass + P1.InverseMass);
			float moveFrac1 = P1.InverseMass / (P0.InverseMass + P1.InverseMass);
			Vector2 newV0 = v0 + delta * (moveFrac0 * diff);
			Vector2 newV1 = v1 - delta * (moveFrac1 * diff);

			//Debug.Log(string.Format("v0 {0} v1 {1} delta {2} dist {3} diff {4} newV0 {5} newV1 {6}", v0, v1, delta, dist, diff, newV0, newV1));

			P0.CurrentPosition = newV0;
			P1.CurrentPosition = newV1;
		}
	}
}

