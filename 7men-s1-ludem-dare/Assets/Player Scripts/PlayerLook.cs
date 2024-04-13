using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLook : MonoBehaviour
{
    public Transform player;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Create Ray
        RaycastHit hit;

        Ray mousePos = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mousePos,out hit, 100f))
        {
            Vector3 lookAtPos = new Vector3(hit.point.x, player.position.y, hit.point.z);
            player.LookAt(lookAtPos);
        }
    }
}
