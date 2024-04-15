using System.Collections;
using System.Collections.Generic;
using Managers.Lawyer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour, IDamageable<Projectiles>
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

    int hp = 100;

    const int maxHP = 100;

    bool isDead = false;

    public PlayerHealth health;

    public Animator playerAnimator;

    public GameObject playerParticles;

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
        playerParticles.GetComponent<ParticleSystem>().Stop();

        mainCam = Camera.main;
        //hp = maxHP;
        health.UpdateHealthBar(hp, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();

        LegAnimations();

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

        playerParticles.transform.position = agent.transform.position;

        playerParticles.transform.LookAt(agent.transform.position);

        playerParticles.GetComponent<ParticleSystem>().Play();

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

        playerAnimator.SetTrigger("Throw");

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
        // hp--;
        //
        // health.UpdateHealthBar(hp, maxHP);
        //
        // if (hp <= 0)
        // {
        //     isDead = true;
        // }
    }

    public void TakeDamage(Projectiles value)
    {
        hp -= value.damage;
        
        health.UpdateHealthBar(hp, maxHP);
        Debug.Log($"JP: {hp}");
        if (hp <= 0)
        {
            EventManager.Instance.PlayerEvents.FirePlayerDeathEvent();
            isDead = true;
        }
    }

    void LegAnimations()
    {
        if (agent.velocity.magnitude > 0 && !isDashing)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }
}
