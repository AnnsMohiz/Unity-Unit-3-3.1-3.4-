using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private float speed = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }
     void FixedUpdate()
    {
       playerBody.velocity = new Vector2(0,speed); 
    }

     void  OnTriggerEnter2D(Collider2D target)
     {
         
        if(target.tag == "Top")
        {
            Destroy(gameObject);
        }
        string[] name = target.name.Split();
        if(name.Length > 1)
        {
            if(name[1]== "Ball")
            {
              ScoreScript.scoreValue += 1;
              Destroy(gameObject);
            }
        
        
        }
     }
    // Update is called once per frame
    void Update()
    {
        
    }
}
