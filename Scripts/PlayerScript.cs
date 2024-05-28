using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
  
    public GameObject bullet;
    public AudioClip shootSound;
    private float speed = 8f;
    private float maxVelocity = 4f;

    private Rigidbody2D playerBody;
    private Animator anim;
    private bool canShoot;
    private bool canWalk;

    // Start is called before the first frame update
    void Awake()
    {
      
        
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
     void FixedUpdate()
    {
        PlayerWalk();
    }
    void Shoot()
    {
       if(Input.GetMouseButtonDown(0))
       {
        if(canShoot)
        {
          canShoot = false;
          StartCoroutine(ShootTheBullet());
        }
       }
    }

    IEnumerator ShootTheBullet()
    {
        canWalk = false;
        anim.Play("Shoot");

        Vector3 temp = transform.position; 
        temp.y += 1f;
        Instantiate (bullet, temp, Quaternion.identity);

       AudioSource.PlayClipAtPoint(shootSound,transform.position);

        yield return  new WaitForSeconds(0.2f);
        anim.SetBool("Shoot",false);
        canWalk = true;
        yield return new WaitForSeconds(0.3f);
        canShoot = true;

    }

    void InitializeVariables()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canShoot = true;
        canWalk = true;
    }

    void PlayerWalk()
    {
        var force = 0f;
        var velocity = Mathf.Abs(playerBody.velocity.x);
        float h = Input.GetAxis("Horizontal");
        if(canWalk)
        {
          // Move Right Side
        if(h>0)
        {
          if(velocity < maxVelocity)
          force = speed;
          Vector3 scale = transform.localScale;
          scale.x = 1;
          transform.localScale = scale;
          anim.SetBool("Walk",true);
        }
        // Move Left Side
        else if(h<0)
        {
           if(velocity < maxVelocity)
          force = -speed;
          Vector3 scale = transform.localScale;
          scale.x = -1;
          transform.localScale = scale;
          anim.SetBool("Walk",true);
        }
        // Player Idle State
        else if(h==0)
        {
          anim.SetBool("Walk",false);
        }
        playerBody.AddForce(new Vector2(force,0));
        }
        
    }
    IEnumerator KillThePlayerAndRestartTheGame()
        {
          transform.position = new Vector3(200,200,0);
          yield return new WaitForSeconds(1.5f);

          SceneManager.LoadScene("GameOver");
           //Application.LoadLevel (SceneManager.LoadScene("Restart"));
        }
        void OnTriggerEnter2D(Collider2D target)
        {
         
          string[] name = target.name.Split();
          if(name.Length>1)
          {
            if(name[1]=="Ball")
            {
             
              StartCoroutine(KillThePlayerAndRestartTheGame());
              ScoreScript.scoreValue = 0;
            }
             
          }
         
         
        }
}
