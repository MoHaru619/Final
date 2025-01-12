using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{

    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private float lifetime;
    [SerializeField]  private float resetTime;
    //  private Animator anim;


    private void Awake()
    {

        anim = GetComponent<Animator>();
       // boxCollider = GetComponent<BoxCollider2D>();

    }
    public void ActivateProjectile ()
    {
        lifetime = 0;
        
        gameObject.SetActive(true);
        

    }
    private void Update()
    {

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;

        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);

        if (anim != null)
            anim.SetTrigger("explode");
        else
            gameObject.SetActive(false);
    }
    
   
}