using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] public short gravityScale;
    [SerializeField, Min(1)] int updatesPerSecond = 24;
    [SerializeField] ParticleType[] simulatedParticles;
    [SerializeField, Range(1, ushort.MaxValue)] public int width = 100;
    [SerializeField, Range(1, ushort.MaxValue)] public int height = 100;

    [SerializeField] InputHandler input;

    public bool[,] isOccupied;
    [HideInInspector] public List<Particle> particles = new List<Particle>();
    Texture2D texture;


    void SimulationStep()
    {
        input.ReadInput();

        Color[] color = new Color[width * height];


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
    
    void Start() {
        InvokeRepeating("SimulationStep", 0, 1f / updatesPerSecond);
    }

    void OnEnable() {
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

    public bool IsPositionValid(int x, int y) 
        => x >= 0 && y >= 0 && x < width && y < height && !isOccupied[x, y];
}
