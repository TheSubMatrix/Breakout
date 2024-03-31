using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject brickParticle;
    GameManager GM;
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void OnCollisionEnter(Collision other)
    {
        if(brickParticle != null)
        {
            Instantiate(brickParticle, transform.position, Quaternion.identity);
        }
        GM.DestroyBrick();
        Destroy(gameObject);
    }
}

