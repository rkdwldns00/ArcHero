using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Hitable
{
    public Transform firePosition;
    public GameObject[] bullets;
    public float reloadSpeed;
    public float bulletDamage;
    protected bool firing = false;
    protected bool firingMotion=false;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected virtual IEnumerator Shot()
    {
        firing = true;
        firingMotion = true;
        for (int i = 0; i < bullets.Length; i++)
        {
            GameObject firedBullet = Instantiate(bullets[i], firePosition.position, transform.rotation);
            firedBullet.tag = transform.tag;
            firedBullet.GetComponentInParent<Bullet>().damage = bulletDamage;
            yield return new WaitForSeconds(0.05f);
        }
        firingMotion = false;
        if (reloadSpeed > bullets.Length * 0.05f)
        {
            yield return new WaitForSeconds(reloadSpeed - bullets.Length * 0.05f);
        }
        firing = false;
    }

    protected void MulReloadSpeed(float val)
    {
        reloadSpeed *= val;
    }

    protected void AddDamage(float val)
    {
        bulletDamage += val;
    }
}
