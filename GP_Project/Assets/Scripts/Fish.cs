using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 70f;
    public Rigidbody fishRB;
    GameManager gameManager;
    AudioSource eatAS;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        fishRB = GetComponent<Rigidbody>();

        fishRB.velocity = transform.forward * speed;

        Destroy(gameObject, 40f);

        eatAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
 
            if (playerController != null)
            {
                eatAS.Play();
                gameManager.AddScore();
                Destroy(gameObject);
            }
        }
    }
}
