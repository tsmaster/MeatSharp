using System.Collections.Generic;
using UnityEngine;

namespace MeatSharp.Physics
{
	public class BDG_ParticlePhysicsManager
	{
		public Vector2 anchorPos;

		public List<BDG_Particle2D> particles;
		public List<BDG_ConstraintDistance> constraints;
		public List<BDG_ConstraintWall> walls;

		public BDG_ParticlePhysicsManager()
		{
			particles = new List<BDG_Particle2D>();
			constraints = new List<BDG_ConstraintDistance>();
			walls = new List<BDG_ConstraintWall>();
		}

		public void AddParticle(BDG_Particle2D particle)
		{
			particles.Add(particle);
		}

		public void AddConstraint(BDG_ConstraintDistance constraint)
		{
			constraints.Add(constraint);
		}

		public void AddWall(BDG_ConstraintWall wall)
		{
			walls.Add(wall);
		}

		public void Update(float dt)
		{
			Vector2 gravity = new Vector2(0.0f, -0.4f);
			// Accum Forces
			for (int i = 0; i < particles.Count; ++i)
			{
				particles[i].ClearForces();
				particles[i].AddForce(gravity);
			}

			// Verlet integration step
			for (int i = 0; i < particles.Count; ++i)
			{
				//Debug.Log("integrating " + i);
				particles[i].VerletIntegrate(dt); 
			}

			Vector2 mousePosInWorld = anchorPos; //GameCamera.ScreenToWorldPoint(Input.mousePosition);
			//Vector2 mousePosInWorld = new Vector2(4.0f, 3.0f);
			particles[0].CurrentPosition = mousePosInWorld;
			particles[0].PreviousPosition = mousePosInWorld;
			particles[0].Acceleration = Vector2.zero;

			// Satisfy constraints
			for (int iterationCount = 0; iterationCount < 4; ++iterationCount)
			{

				for (int i = 0; i < constraints.Count; ++i)
				{
					constraints[i].Satisfy(); 
				}

				for (int i = 0; i < walls.Count; ++i)
				{
					for (int p = 0; p < particles.Count; ++p)
					{
						walls[i].ConstrainParticle(particles[p]);
					}
				}
			}
		}

		public void Reset()
		{
			particles.Clear();
			constraints.Clear();
			walls.Clear();
		}
	}
}

