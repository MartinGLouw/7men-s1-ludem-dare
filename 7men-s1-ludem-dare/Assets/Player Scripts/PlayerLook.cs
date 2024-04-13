using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform player;

    Camera mainCam;

    public NavMeshAgent agent;

    public Vector3 CameraPos;

    private PlayerInputActions inputActions;

    private InputAction fire;

    private InputAction dash;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        dash = inputActions.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        fire = inputActions.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        dash.Disable();

        fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        RaycastHit hit;

        //casts ray to mous position
        Ray mousePos = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        //if there was a ray cast the player will look at it
        if (Mouse.current.leftButton.isPressed && Physics.Raycast(mousePos, out hit, 100f))
        {
            player.LookAt(new Vector3(hit.point.x, 0f, hit.point.z));
        }

        //if there was a ray cast  and the player pressed the left mouse button the player's navmesh agent's destination will be set to it
        if (Mouse.current.leftButton.isPressed && Physics.Raycast(mousePos, out hit, 100f))
        {
            agent.SetDestination(hit.point);
        }
    }

    void MoveCamera()
    {
        mainCam.transform.position = new Vector3(player.transform.position.x + CameraPos.x, player.transform.position.y + CameraPos.y, player.transform.position.z + CameraPos.z);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Debug.Log("dash");
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("fire");
    }
}
