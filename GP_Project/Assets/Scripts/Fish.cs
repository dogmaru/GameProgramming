using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody fishRB;

    // Start is called before the first frame update
    void Start()
    {
        fishRB = GetComponent<Rigidbody>();

        fishRB.velocity = transform.forward * speed;

        Destroy(gameObject, 70f);
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
                playerController.AddScore(100);
                Destroy(gameObject);
            }
        }
    }
}
