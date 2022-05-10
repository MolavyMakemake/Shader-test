using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] public short gravityScale;
    [SerializeField] ParticleType[] simulatedParticles;
    [SerializeField, Range(1, ushort.MaxValue)] int width = 100;
    [SerializeField, Range(1, ushort.MaxValue)] int height = 100;

    public bool[,] isOccupied;
    List<Particle> particles = new List<Particle>();
    Texture2D texture;


    // Start is called before the first frame update
    void Start()
    {
        isOccupied = new bool[width, height];
        
        // Create and apply texture
        texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        GetComponent<MeshRenderer>().material.SetTexture("main_tex", texture);

        // Reference this simulation to all particle types
        foreach (var particleType in simulatedParticles) {
            particleType.SetSimulation(this);
        }
    }

    void FixedUpdate()
    {
        Color[] color = new Color[width * height];
        particles.Add(new Particle(50, 99, 0));
        particles.Add(new Particle(70, 99, 1));
        particles.Add(new Particle(72, 99, 1));

        foreach (var particle in particles) {
            ParticleType particleType = simulatedParticles[particle.type];

            isOccupied[particle.x, particle.y] = false;

            particleType.PhysicsStep(particle);

            isOccupied[particle.x, particle.y] = true;
            color[particle.y * width + particle.x] = particleType.color;
        } 

        texture.SetPixels(color);
        texture.Apply(false);
    }
    

    public bool IsPositionValid(int x, int y) 
        => x >= 0 && y >= 0 && x < width && y < height && !isOccupied[x, y];
}
