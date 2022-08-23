using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : Attacker
{
    public float moveSpeed;
    public float maxSpeed;
    public Slider hpSlider;
    Rigidbody rigid;
    PlayerInput inputSys;
    Animator animator;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        inputSys = FindObjectOfType<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!firing)
        {
            Vector3 dir = inputSys.mousePos - transform.position;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation,rot,0.2f);
            //transform.LookAt(inputSys.mousePos);
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
        if (Vector3.Distance(transform.position, inputSys.mousePos) >= 1.5f && inputSys.mouseRightDown)
        {
            animator.SetBool("Walk", true);
            if (!firing)
            {
                rigid.velocity = transform.rotation * new Vector3(0, 0, 1f * moveSpeed);
            }
        }
        else
        {
            rigid.velocity = Vector3.zero;
            animator.SetBool("Walk", false);
        }
        /*rigid.velocity = new Vector3(inputSys.hor * moveSpeed, 0, inputSys.ver * moveSpeed);
        if (!(inputSys.ver == 0f && inputSys.hor == 0f))
        {
            animator.SetBool("Walk", true);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, (Mathf.Atan2(-inputSys.ver, inputSys.hor) * Mathf.Rad2Deg) + 90f, 0), transform.rotation, 0.5f);
        }
        else
        {
            animator.SetBool("Walk", false);
        }*/
        if (inputSys.fire && !firing)
        {
            StartCoroutine(shot());
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        hpSlider.value = GetHp / maxhp;
    }

    public override void TakeHeal(float heal)
    {
        base.TakeHeal(heal);
        hpSlider.value = GetHp / maxhp;
    }

    protected void MulMoveSpeed(float val)
    {
        moveSpeed *= val;
        maxSpeed *= val;
    }

    public void specUp(ability val)
    {
        switch (val)
        {
            case ability.hp:
                AddMaxHp(25f);
                hpSlider.value = GetHp / maxhp;
                break;
            case ability.reloadSpeed:
                MulReloadSpeed(0.5f);
                break;
            case ability.damage:
                AddDamage(15f);
                break;
            case ability.moveSpeed:
                MulMoveSpeed(1.2f);
                break;
        }
    }

    protected override IEnumerator shot()
    {
        firing = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(base.shot());
    }

    /*protected override IEnumerator Die()
    {
        yield return null;
        StartCoroutine(base.Die());
    }*/
}
