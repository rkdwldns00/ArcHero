using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode interactionKey;
    public KeyCode fireKey;
    public float dpi;
    float mx;
    float my;
    public float hor { get; private set; }
    public float ver { get; private set; }
    public bool inter { get; private set; }
    public bool fire { get; private set; }
    public bool mouseLeftDown { get; private set; }
    public bool mouseRightDown { get; private set; }
    
    public Vector3 mousePos { get; private set; }

    public float mousex { get; private set; }
    public float mousey { get; private set; }

    Camera gameCamera;

    void Start()
    {
        gameCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        inter = Input.GetKeyDown(interactionKey);
        fire = Input.GetKey(fireKey);
        mouseLeftDown = Input.GetMouseButton(0);
        mouseRightDown = Input.GetMouseButton(1);
        mousex = Input.GetAxis("Mouse X");
        mousey = Input.GetAxis("Mouse Y");
        Ray mouseRay = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit)) {
            Vector3 v = hit.point;
            v.y = 0f;
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 30f, Color.red, 0.1f);
            Debug.DrawLine(v, v + new Vector3(0, 10f, 0));
            mousePos = v;
        }
    }
}
