using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleType : ScriptableObject
{
    protected Simulation simulation;
    public Color color;

    public void SetSimulation(Simulation simulation) {
        this.simulation = simulation;
    }

    protected void ApplyGravity(Particle p) {
        p.vY -= simulation.gravityScale;
        int sign = (int)Mathf.Sign(p.vY);
        for (int i = 1; i < Mathf.Abs(p.vY); i ++) {

            int dY = sign * i;
            if (!simulation.IsPositionValid(p.x, p.y + dY)) {
                p.y = (ushort)(p.y + dY - sign);
                p.vY = 0;
                return;
            }
        }
        p.y = (ushort)(p.y + p.vY);
    }

    public abstract void PhysicsStep(Particle p);
}
