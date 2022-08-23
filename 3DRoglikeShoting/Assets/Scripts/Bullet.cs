using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool piercing;
    public bool ignoreMap;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Hitable>() != null && !(other.transform.CompareTag(transform.tag) || (other.transform.parent!=null && other.transform.parent.CompareTag(transform.tag))))
        {
            if (other.transform.parent != null)
            {
                Hit(other.transform.parent.gameObject);
            }
            else
            {
                Hit(other.transform.gameObject);
            }
            other.transform.GetComponentInParent<Hitable>().TakeDamage(damage);
            if (transform.CompareTag("Player"))
            {
                FindObjectOfType<EnemyHpSlider>().ShowEnemyHp(other.GetComponentInParent<Hitable>().gameObject);
            }
        }
        else if(!ignoreMap&&(other.transform.CompareTag("Map") || (other.transform.parent!=null && other.transform.parent.CompareTag("Map"))))
        {
            Die();
        }
    }

    void Hit(GameObject hitObject)
    {
        //hitObject.GetComponent<Hitable>().TakeDamage(damage);
        if (!piercing)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
