using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody sharkRB;

    // Start is called before the first frame update
    void Start()
    {
        sharkRB = GetComponent<Rigidbody>();

        sharkRB.velocity = transform.forward * speed;

        Destroy(gameObject, 30f);
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
                playerController.Die();
            }
        }
    }
}
