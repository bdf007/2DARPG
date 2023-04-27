using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    private Character.Team team;
    private Rigidbody2D rig;

    void Awake ()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate ()
    {
        rig.velocity = transform.up * speed;
    }

    public void SetTeam ( Character.Team t)
    {
        team = t;
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();

        if(damagable != null && damagable.GetTeam() != team)
        {
            damagable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
