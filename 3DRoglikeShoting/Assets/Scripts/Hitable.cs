using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : MonoBehaviour
{
    public float maxhp;
    public float haveExp;
    float hp;
    public float GetHp
    {
        get { return hp; }
    }

    public bool isDead { get; protected set; }

    protected virtual void Start()
    {
        SpawnSet();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if (collision.transform.GetComponent<Bullet>() != null)
            {
                
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isDead)
        {
            if (damage > 0f)
            {
                if (hp < damage)
                {
                    hp = 0f;
                }
                else
                {
                    hp -= damage;
                }
            }
            if (hp <= 0f)
            {
                StartCoroutine(Die());
            }
        }
    }

    public virtual void TakeHeal(float heal)
    {
        if (!isDead)
        {
            if (heal > 0f)
            {
                if (maxhp < hp + heal)
                {
                    hp = maxhp;
                }
                else
                {
                    hp += heal;
                }
            }
        }
    }

    protected void AddMaxHp(float val)
    {
        if (!isDead)
        {
            maxhp += val;
            hp += val;
        }
    }

    public virtual void SpawnSet()
    {
        isDead = false;
        hp = maxhp;

    }

    protected virtual IEnumerator Die()
    {
        yield return null;
        isDead = true;
        FindObjectOfType<GameManager>().GetExp(haveExp);
        gameObject.SetActive(false);
    }
}
