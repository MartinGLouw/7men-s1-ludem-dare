using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public GameObject player;

    Camera mainCam;

    public NavMeshAgent agent;

    public Vector3 CameraPos;

    private PlayerInputActions inputActions;

    private InputAction walk;
    private InputAction fire;
    private InputAction dash;

    Vector2 moveDirecton;

    public float speed;

    bool isDashing = false;

    public float dashSpeed;
    public float dashDuration;

    public ProjectileSpawner projectileSpawner;

    bool canInvoke = true;

    int hp = 3;

    const int maxHP = 3;

    bool isDead = false;

    public PlayerHealth health;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        walk = inputActions.Player.Walk;
        walk.Enable();

        dash = inputActions.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        fire = inputActions.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        walk.Disable();

        dash.Disable();

        fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        health.UpdateHealthBar(hp, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();

        moveDirecton = walk.ReadValue<Vector2>();   
    }

    void FixedUpdate()
    {
        PlayerRotation();

        if (!isDashing)
        {
            MovePlayer();
        }
    }

    void PlayerRotation()
    {
        RaycastHit hit;

        //casts ray to mous position
        Ray mousePos = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        //if there was a ray cast the player will look at it
        if (Physics.Raycast(mousePos, out hit, 100f))
        {
            player.transform.LookAt(new Vector3(hit.point.x, 0f, hit.point.z));
        }

    }

    void MovePlayer()
    {
        agent.velocity = new Vector3(moveDirecton.x * speed, 0f, moveDirecton.y * speed);
    }

    void MoveCamera()
    {
        mainCam.transform.position = new Vector3(player.transform.position.x + CameraPos.x, player.transform.position.y + CameraPos.y, player.transform.position.z + CameraPos.z);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        StartCoroutine(performDash());
    }

    private IEnumerator performDash()
    {
        isDashing = true;

        player.GetComponent<Collider>().enabled = false;

        agent.velocity = new Vector3(moveDirecton.x * dashSpeed, 0f, moveDirecton.y * dashSpeed);

        yield return new WaitForSeconds(dashDuration);

        player.GetComponent<Collider>().enabled = true;

        isDashing = false;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (!isDashing && canInvoke)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canInvoke = false;

        projectileSpawner.SpawnPlayerProjectiles(player.transform.position, player.transform.forward);

        yield return new WaitForSeconds(0.2f);

        canInvoke = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyProjectile")
        {
            TakeDamage();
        }
        
    }

    public void TakeDamage()
    {
        hp--;

        health.UpdateHealthBar(hp, maxHP);

        if (hp <= 0)
        {
            isDead = true;
        }
    }
}
