using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Particles/Water")]
public class Water : ParticleType
{
    [SerializeField, Min(0)] float spread = 5;

    public override void PhysicsStep(Particle p)
    {
        if (simulation.IsPositionValid(p.x, p.y - 1)) {
            ApplyGravity(p);
        } 
        else if (simulation.IsPositionValid(p.x - 1, p.y - 1)) {
            p.x --;
            p.y --;
        }
        else if (simulation.IsPositionValid(p.x + 1, p.y - 1)) {
            p.x ++;
            p.y --;
        }
        else if (simulation.IsPositionValid(p.x - 1, p.y)) {
            p.x --;
        }
        else if (simulation.IsPositionValid(p.x + 1, p.y)) {
            p.x ++;
        }
    }
}
