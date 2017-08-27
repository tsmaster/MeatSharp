using UnityEngine;

namespace MeatSharp.Physics
{
	public class BDG_Particle2D
	{
		public Vector2 CurrentPosition;
		public Vector2 PreviousPosition;
		public Vector2 Acceleration;
		public float InverseMass;
		public string GUID;

		public BDG_Particle2D(Vector2 position, float inverseMass, string GUID)
		{
			this.CurrentPosition = position;
			this.PreviousPosition = position;
			this.Acceleration = Vector2.zero;
			this.InverseMass = inverseMass;
			this.GUID = GUID;
		}

		public void VerletIntegrate(float dt)
		{
			//Debug.Log("Verlet Integrate " + dt);

			float dtSqr = dt * dt;

			Vector2 TempPos = CurrentPosition;
			CurrentPosition += CurrentPosition - PreviousPosition + Acceleration * dtSqr;
			PreviousPosition = TempPos;

			/*
			Debug.Log(string.Format("before integrate: curPos {0} prevPos {1} acc {2}", CurrentPosition, PreviousPosition, Acceleration));

			Vector2 newPos = 1.9f * CurrentPosition - PreviousPosition + Acceleration * dtSqr;
			PreviousPosition = CurrentPosition;
			CurrentPosition = newPos;

			Debug.Log(string.Format("after integrate: curPos {0} prevPos {1}", CurrentPosition, PreviousPosition));
*/
		}

		public void ClearForces()
		{
			Acceleration = Vector2.zero;
		}

		public void AddForce(Vector2 force)
		{
			Acceleration = force;
		}
	}
}

