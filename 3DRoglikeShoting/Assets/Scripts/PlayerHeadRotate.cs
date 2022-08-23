using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadRotate : MonoBehaviour
{
    PlayerInput inputSys;
    // Start is called before the first frame update
    void Start()
    {
        inputSys = FindObjectOfType<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - inputSys.mousey, transform.parent.rotation.eulerAngles.y, 0);
    }
}
