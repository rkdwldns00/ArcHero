using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpSlider : MonoBehaviour
{
    float timer = 0f;
    public Slider hpSlider;
    public GameObject backGround;
    public Text nameText;
    Hitable hp;
    public float TimerSet
    {
        get { return timer; }
        set {
            if (0f < value)
            {
                timer = value;
            }
            else
            {
                timer = 0f;
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerSet-=Time.deltaTime;
        if(TimerSet > 0f && hp!=null)
        {
            hpSlider.value = hp.GetHp/hp.maxhp;
            hpSlider.gameObject.SetActive(true);
            backGround.SetActive(true);
        }
        else
        {
            hpSlider.gameObject.SetActive(false);
            backGround?.SetActive(false);
        }
    }

    public void ShowEnemyHp(GameObject gameObject)
    {
        nameText.text = gameObject.name;
        hp = gameObject.GetComponentInParent<Hitable>();
        TimerSet = 1f;
    }
}
