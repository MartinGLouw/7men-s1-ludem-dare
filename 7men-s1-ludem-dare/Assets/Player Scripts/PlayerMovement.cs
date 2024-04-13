using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public Rigidbody rb;

    public InputAction moveControls;

    Vector2 moveDirections;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        moveControls.Enable();
    }

    private void OnDisable()
    {
        moveControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirections = moveControls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(moveDirections.x, 0f, moveDirections.y);

        rb.velocity = moveDirection * speed * Time.deltaTime;
    }

}
