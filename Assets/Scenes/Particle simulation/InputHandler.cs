using UnityEngine;

public class InputHandler : MonoBehaviour 
{
    [SerializeField] Simulation simulation;
    [SerializeField] CursorUI cursor;

    int inp;

    void Update() {
      
        inp |= 
                (Input.GetMouseButton(0) ? 1 : 0) + 
                (Input.GetMouseButton(1) ? 2 : 0);
    }    

    void FixedUpdate() {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(v.x) > 1 || Mathf.Abs(v.y) > 1) {
            Cursor.visible = true;
            transform.position = new Vector3(0, 10, 0);
        }
        else {
            Cursor.visible = false;
            transform.position = v + Vector3.forward;
        }

        if (inp != 0) {
            SpawnParticles(v);
            inp = 0;
        }
    }

    void SpawnParticles(Vector2 v) {

        int x = (int)((v.x + 1) * simulation.width / 2f);
        int y = (int)((v.y + 1) * simulation.height / 2f);

        float sqrWidth = transform.localScale.x * transform.localScale.x * cursor.width * cursor.width;
        float sqrHeight = transform.localScale.y * transform.localScale.y * cursor.height * cursor.height;

        float stepSize = 1 / (simulation.width * transform.localScale.x);

        for (float i = -cursor.width / 2; i < cursor.width / 2; i += stepSize) {
            for (float j = -cursor.height / 2; j < cursor.height / 2; j += stepSize) {
                
                if (
                    // If point is outside ellipse
                    i * i / sqrWidth + j * j / sqrHeight > 1

                    // Density is achieved by randomness 
                    // This also contributes to the realism of the simulation
                    || Random.value > cursor.density) continue;

                int _x = x + (int)(i / stepSize);
                int _y = y + (int)(j / stepSize);

                if (simulation.IsPositionValid(_x, _y)) {
                    simulation.particles.Add(new Particle((ushort)_x, (ushort)_y, 
                        (byte)(inp == 1 ? 0 : 1)));
                }

            }
        }
    }
}