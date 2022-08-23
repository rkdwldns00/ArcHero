using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Elemental
{
    archer,
    knight
}

public enum ability
{
    hp,
    damage,
    moveSpeed,
    reloadSpeed
}

public class GameManager : MonoBehaviour
{
    public GameObject[] Stages;
    public GameObject[] BossStages;
    public GameObject Potal;
    public Text StageUi;
    public Text LevelUi;
    public Text AbilityText;
    public Slider ExpSlider;
    public float[] levelUpExpList;
    const int NUMBEROFABILITY = 4;
    int[] StagesChildren = new int[100];
    int[] BossStagesChildren = new int[100];
    int mapIndex;
    int stage;
    int levelValue;
    float expValue;
    bool keyDown = false;
    bool choosingAbility=false;
    int key = 0;
    ability[] choosePool = new ability[3] { ability.hp,ability.hp,ability.hp};
    public float Exp
    {
        get { return expValue; }
        protected set
        {
            expValue = value;
        }
    }


    public int Level
    {
        get { return levelValue; }
        protected set
        {
            if (value > levelValue || value == 1)
            {
                levelValue = value;
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(true);
        }

        for (int i = 0; i < Stages.Length; i++)
        {
            /*int j;
            for (j = 0; Stages[stage].transform.GetChild(j) != null; j++)
            {

            }*/
            StagesChildren[i] = Stages[i].transform.childCount;
        }

        for (int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }
        Stages[0].SetActive(true);



        for (int i = 0; i < BossStages.Length; i++)
        {
            BossStages[i].SetActive(true);
        }

        for (int i = 0; i < BossStages.Length; i++)
        {
            /*int j;
            for (j = 0; Stages[stage].transform.GetChild(j) != null; j++)
            {

            }*/
            BossStagesChildren[i] = BossStages[i].transform.childCount;
        }

        for (int i = 0; i < BossStages.Length; i++)
        {
            BossStages[i].SetActive(false);
        }

        stage = 0;
        mapIndex = 0;
        Exp = 0f;
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Potal.SetActive(true);
        }
        if (choosingAbility)
        {
            keyDown = Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3);
            if (Input.GetKeyDown(KeyCode.Alpha1)) { key = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { key = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { key = 3; }
            if (keyDown)
            {
                AbilityText.gameObject.SetActive(false);
                Time.timeScale = 1f;
                FindObjectOfType<PlayerControl>().specUp(choosePool[key - 1]);
                FindObjectOfType<Light>().intensity = 1f;
            }
        }


    }

    public void NextStage()
    {
        FindObjectOfType<PlayerControl>().TakeHeal(10f);
        Stages[mapIndex].SetActive(false);
        stage++;
        int j = mapIndex;
        if (stage % 10 == 0)
        {
            mapIndex = Random.Range(0, BossStages.Length);
            BossStages[mapIndex].SetActive(true);
            Spawn(BossStages[mapIndex], BossStagesChildren[mapIndex]);
        }
        else
        {
            do
            {
                mapIndex = Random.Range(1, Stages.Length);
            } while (mapIndex == j);
            Stages[mapIndex].SetActive(true);
            Spawn(Stages[mapIndex], StagesChildren[mapIndex]);
        }
        if (stage % 10 == 1 && stage != 1)
        {
            chooseAbility();
        }
        StageUi.text = "Stage : " + stage.ToString();
    }

    void Spawn(GameObject slectedMap, int haveChildren)
    {
        FindObjectOfType<PlayerControl>().transform.position = new Vector3(0, 0, -8);
        for (int i = 0; i < haveChildren; i++)
        {
            slectedMap.transform.GetChild(i).gameObject.SetActive(true);
            if (slectedMap.transform.GetChild(i).gameObject.GetComponentInParent<Hitable>() != null)
            {
                slectedMap.transform.GetChild(i).gameObject.GetComponentInParent<Hitable>().SpawnSet();
            }
        }
    }

    public void GetExp(float mobExp)
    {
        Exp += mobExp;
        while (Level < levelUpExpList.Length && Exp >= levelUpExpList[Level - 1])
        {
            LevelUp();
        }
        ExpSlider.value = Exp / levelUpExpList[Level - 1];
    }

    void LevelUp()
    {
        chooseAbility();
    }

    void chooseAbility()
    {
        FindObjectOfType<Light>().intensity = 0.5f;
        Exp -= levelUpExpList[Level - 1];
        Level++;
        LevelUi.text = "Level : " + Level.ToString();
        do
        {
            for (int i = 0; i < 3; i++)
            {
                choosePool[i] = AbilityIndex(-1);
            }
        } while (choosePool[0] == choosePool[1] || choosePool[1] == choosePool[2] || choosePool[2] == choosePool[0]);
        AbilityText.gameObject.SetActive(true);
        AbilityText.text = "1:" + choosePool[0].ToString() + ", 2:" + choosePool[1].ToString() + ", 3:" + choosePool[2].ToString();
        Debug.Log(choosePool[0].ToString() + ", " + choosePool[1].ToString() + ", " + choosePool[2].ToString());
        Time.timeScale = 0f;
        choosingAbility = true;
    }

    public static ability AbilityIndex(int index)
    {
        if(index == -1)
        {
            index = Random.Range(0, NUMBEROFABILITY);
        }
        switch (index)
        {
            case 0: return ability.hp;
            case 1: return ability.damage;
            case 2: return ability.moveSpeed;
            case 3: return ability.reloadSpeed;
            default: return ability.hp;
        }
    }
}
