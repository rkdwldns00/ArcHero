using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode interactionKey;
    public KeyCode fireKey;
    public float dpi;
    //float mx;
    //float my;
    public float Hor { get; private set; }
    public float Ver { get; private set; }
    public bool Inter { get; private set; }
    public bool Fire { get; private set; }
    public bool MouseLeftStay { get; private set; }
    public bool MouseLeftDown { get; private set; }
    public bool MouseRightStay { get; private set; }
    
    public Vector3 MouseRightDownPos { get; private set; }
    public Vector3 MouseLeftDownPos { get; private set; }
    public Vector3 MousePos { get; private set; }

    public float Mousex { get; private set; }
    public float Mousey { get; private set; }

    Camera gameCamera;
    GameObject player;

    void Start()
    {
        gameCamera = FindObjectOfType<Camera>();
        MouseRightDownPos = FindObjectOfType<PlayerControl>().transform.position;
        player = FindObjectOfType<PlayerControl>().gameObject;
        MousePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Hor = Input.GetAxis("Horizontal");
        Ver = Input.GetAxis("Vertical");
        Inter = Input.GetKeyDown(interactionKey);
        Fire = Input.GetKey(fireKey);
        Mousex = Input.GetAxis("Mouse X");
        Mousey = Input.GetAxis("Mouse Y");
        Ray mouseRay = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit)) {
            MouseLeftStay = Input.GetMouseButton(0);
            MouseLeftDown = Input.GetMouseButtonDown(0);
            MouseRightStay = Input.GetMouseButton(1);
            Vector3 v = hit.point;
            v.y = 0f;
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 30f, Color.red, 0.1f);
            Debug.DrawLine(v, v + new Vector3(0, 10f, 0));
            MousePos = v;
            if (MouseLeftDown)
            {
                MouseLeftDownPos = v;
                MouseRightDownPos = player.transform.position + (v-player.transform.position).normalized;
            }
            if (MouseRightStay)
            {
                MouseRightDownPos = v;
            }
        }
    }
}
