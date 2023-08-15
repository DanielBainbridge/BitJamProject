using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// player controller notes
    /// 
    /// wasd controller
    /// save last jump point as spawn point
    /// grounded checks
    /// </summary>



    [Header("Jump Variables")]
    public float jumpHeight = 6f;
    public float jumpTime = 0.8f;
    public float gravity = -9.81f;

    [Header("Run Variables")]
    public float maxSpeed = 4;
    public float maxAcceleration = 10;
    public float accelerationTime = 1.7f;
    public AnimationCurve accelCurve;
    private float currentAnimationStep;

    [Header("Collision")]
    [Range(0, 1)] public float collisionBounciness;
    public Vector2 playerSize;

    [Header("Camera")]
    Transform camera = null;

    private float accelerationStep
    {
        get { return 1 / accelerationTime * Time.fixedDeltaTime; }
    }


    //game variables
    Vector2 spawnPosition;

    Vector2 velocity;

    private float currentAccelerationStep = 0;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        camera = Camera.main.transform;
        spawnPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        bool moving = Movement();
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        if (!moving && currentAccelerationStep > 0)
        {
            currentAccelerationStep -= accelerationStep;
            currentAccelerationStep = Mathf.Clamp(currentAccelerationStep, 0, 1);
        }


    }
    private void LateUpdate()
    {
        float size = Camera.main.orthographicSize;
        Vector3 newCamPos = new Vector3(transform.position.x, camera.position.y, -10);
        if (isGrounded)
        {
            newCamPos.y = transform.position.y + size - 2 * playerSize.y;
        }
        camera.position = Vector3.Lerp(camera.position, newCamPos, Vector3.Distance(camera.position, newCamPos) * Time.deltaTime);
    }

    bool Movement()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;

        float effectiveSpeed;

        Vector2 desiredInput = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
        {
            desiredInput.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            desiredInput.x -= 1;
        }
        Vector2 desiredVelocity = desiredInput * maxSpeed * accelCurve.Evaluate(currentAccelerationStep);
        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        //move smoothly towards our desired velocity from our current veolicty
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y += gravity * Time.deltaTime;

        CheckCollisions();
        if (desiredInput == Vector2.zero)
        {
            return false;
        }
        currentAccelerationStep += accelerationStep;
        currentAccelerationStep = Mathf.Clamp(currentAccelerationStep, 0, 1);

        return true;

    }

    private void CheckCollisions()
    {
        RaycastHit2D hitRight = Physics2D.BoxCast(transform.position, playerSize, 0, Vector2.right, playerSize.x / 2.0f);
        RaycastHit2D hitLeft = Physics2D.BoxCast(transform.position, playerSize, 0, Vector2.left, playerSize.x / 2.0f);
        RaycastHit2D hitDown = Physics2D.BoxCast(transform.position, playerSize, 0, Vector2.down, (playerSize.y / 2.0f) * 1.01f);
        if (hitRight && velocity.x > 0)
        {
            velocity.x = -velocity.x * collisionBounciness;
        }
        else if (hitLeft && velocity.x < 0)
        {
            velocity.x = -velocity.x * collisionBounciness;
        }

        if (hitDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (hitDown && velocity.y < 0)
        {
            velocity.y = -velocity.y * collisionBounciness;
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
        //check for jump pads
        RaycastHit2D hitDown = Physics2D.BoxCast(transform.position, playerSize, 0, Vector2.down, playerSize.y * 1.01f);
        if (hitDown.transform.GetComponent<Spring>() != null)
        {
            jumpMultiplier = hitDown.transform.GetComponent<Spring>().jumpMultiplier;
        }

        velocity.y = (1.5f * jumpHeight / jumpTime) * jumpMultiplier;
    }

    void Respawn()
    {
        transform.position = spawnPosition;
    }
}
