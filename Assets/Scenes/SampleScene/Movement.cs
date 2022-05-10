using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    [SerializeField, Range(0, 100)] float speed;
    [SerializeField] Animator animator;
    [SerializeField] Volume volume;
    [SerializeField] Color left;
    [SerializeField] Color right;

    Material material;

    [SerializeField] Transform ballGO;
    Vector4 ball;
    Vector4 velocity = new Vector4(1, 0.5f, 0, 0);

    void Update()
    {
        material.SetVector("Ball", ball);
    }

    void FixedUpdate()
    {
        ball += velocity * speed * Time.fixedDeltaTime;

        float x = 1, y = 1;

        if ((velocity.x > 0 && ball.x > x) || (velocity.x < 0 && ball.x < -x)) {
            velocity.x *= -1;
            material.SetVector("Collision", ball);
            CollisionEffect();
        }
        if ((velocity.y > 0 && ball.y > y) || (velocity.y < 0 && ball.y < -y)) {
            velocity.y *= -1;
            material.SetVector("Collision", ball);
        }
    }

    void CollisionEffect() {
        material.SetFloat("CollisionTime", Time.time);
        animator.Play("Collision");

        Bloom bloom;
        volume.profile.TryGet<Bloom>(out bloom);
        bloom.tint.value = velocity.x > 0 ? right : left;
    }

    void OnEnable(){
        material = GetComponent<Image>().material;
    }
}
