using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// player controller notes
    /// 
    /// using rigidbody so do physics
    /// do spring controller
    /// wasd controller
    /// save last jump point as spawn point
    /// grounded checks
    /// </summary>

    Vector2 spawnPosition = Vector2.zero;
    float playerHeight = 1.5f;
    float jumpHeight = 4f;
    float jumpTime = 0.8f;
    Rigidbody2D thisRigidbody;

    float stepWidth = 0.4f;

    bool isGrounded
    {
        get { return Physics2D.CircleCast(transform.position, stepWidth,  Vector2.down, playerHeight); }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!isGrounded)
        {
            return;
        }
        spawnPosition = transform.position;

        float jumpMultiplier = 1;
        //if(Physics2D.CircleCast(transform.position, stepWidth, Vector2.down, playerHeight, out RaycastHit2D[] result)

        thisRigidbody.AddForce((2 * Vector2.up * jumpHeight) / jumpTime, ForceMode2D.Impulse);
    }

    void Respawn()
    {
        transform.position = spawnPosition;
    }
}
