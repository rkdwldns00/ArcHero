using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElemental : Attacker
{
    public Elemental unitType;
    Animator animator;
    GameObject player;
    Vector3 spawnPoint;
    protected override void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerControl>().gameObject;
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (player != null && !isDead)
        {
            transform.LookAt(player.transform);
        }
        if (firing == false)
        {
            if (unitType == Elemental.archer || Vector3.Distance(transform.position, player.transform.position) <= 3f)
            {
                StartCoroutine(Attack());
            }
        }
    }

    protected void FixedUpdate()
    {
        if (unitType == Elemental.knight)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 3f)
            {
                animator.SetBool("Walk", true);
                if (!firingMotion && !isDead)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * 3);
                }
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    protected IEnumerator Attack()
    {
        if (!isDead)
        {
            firing = true;
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(shot());
        }
    }

    public override void SpawnSet()
    {
        base.SpawnSet();
        firing = false;
        spawnPoint = transform.position;
    }

    protected override IEnumerator Die()
    {
        animator.SetTrigger("Die");
        isDead = true;
        yield return new WaitForSeconds(1.075f);
        transform.position = spawnPoint;
        StartCoroutine(base.Die());
    }
}
