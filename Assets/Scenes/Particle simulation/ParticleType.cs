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

        int d = DistanceY(p, p.vY);
        if (d != p.vY) p.vY = 0;

        p.y += (ushort)d;
    }

    protected int DistanceX(Particle p, int x) {
        int sign = (int)Mathf.Sign(x);
        for (int i = 1; i <= Mathf.Abs(x); i ++) {

            int dX = sign * i;
            if (!simulation.IsPositionValid(p.x + dX, p.y)) {
                return dX - sign;
            }
        }
        return x;
    }

    protected int DistanceY(Particle p, int y) {
        int sign = (int)Mathf.Sign(y);
        for (int i = 1; i <= Mathf.Abs(y); i ++) {

            int dY = sign * i;
            if (!simulation.IsPositionValid(p.x, p.y + dY)) {
                return dY - sign;
            }
        }
        return y;
    }
    public abstract void PhysicsStep(Particle p);
}
