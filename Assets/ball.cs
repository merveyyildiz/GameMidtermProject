using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
   
    void Start()
    {
        Invoke("basla", 2f);
       
    }

    void basla()
    {
        rb.velocity = new Vector2(1, 0) * 5f;
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("sagduvar"))
        {
            rb.velocity = new Vector2(-1, 0) * 5f;
            Ses();
        }
        if (collision.gameObject.tag.Equals("solduvar"))
        {
            rb.velocity = new Vector2(1, 0) * 5f;
            Ses();
        }
       

    }
  
    private void OnTriggerEnter2D(Collider2D other)//bomba topa değince bomba yok olucak
    {
        if (other.gameObject.tag.Equals("bomba"))
        {
            player.Score();
            Destroy(other.gameObject);
        }
    }
    private void Ses()
    {
        GetComponent<AudioSource>().Play();
    }
}
