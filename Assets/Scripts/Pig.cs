using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite hurtPig;
    public GameObject boom;
    public GameObject score;

    public bool isPig = false;
    public AudioClip pigDead;
    public AudioClip pigCollision;
    public AudioClip birdCollision;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            AudioPlay(birdCollision);
        }

        print(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > maxSpeed)
        { // Ö±½ÓËÀÍö
            Dead();
        }
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            AudioPlay(pigCollision);
            render.sprite = hurtPig;
        }
    }

    void Dead() {
        if (isPig) {
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        
        GameObject go = Instantiate(score, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(go, 1.5f);
        AudioPlay(pigDead);
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
