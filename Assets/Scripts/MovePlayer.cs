using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum gamemode
{
    Cube,
    Ship,
    Ball
}
public class MovePlayer : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float rotationSpeed = 1.9f;

    public LayerMask groundLayer;
    public float groundCheckRadius = 5f;
    public Transform groundCheck, groundcheck2;


    bool isGrounded = false;
    bool isJumping = false;

    public float height = 1f;

    Animator anim;

    public GameObject playerObject;
    GameObject spike;
    public LayerMask obstacleLayer;

    public GameObject obstacleCollisionCheck;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    [Range(1, 5)]
    public float shipYVelocity = 2.5f;

    public bool isUpsideDown;

    public Transform sprite;
    public enum direction
    {
        up,
        down,
        back,
        forward,
        left,
        right
    }
    

    public direction moveDirection;
    public direction rotateDirection;
    public Vector3 moveDir;

    public gamemode gameMode;

    public AudioSource aus;

    
    public GameObject cubeModel;

    public LevelEditorManager editor;
    float velocity;
    bool isCollidingWithTrigger = false;

    public EventSO onTriggerEnter;
    public EventSO onPlayerSpawned;
    GUIContent content;
    GUIStyle style = new GUIStyle();
    [SerializeField] Texture debugTex;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = playerObject.GetComponent<Animator>();
        spike = GameObject.FindGameObjectWithTag("Obstacle");
        Time.timeScale = 1;
        velocity = rb.velocity.y;
        editor = (SceneManager.GetActiveScene().ToString() == "LevelEditor") ? GameObject.Find("LevelEditorManager").GetComponent<LevelEditorManager>() : (LevelEditorManager)null;
        
        Physics.gravity = new Vector3(0, -9.81f, 0);
        gameMode = gamemode.Cube;
    }
    public void Awake()
    {
        style.alignment = TextAnchor.MiddleCenter;
        style.imagePosition = ImagePosition.ImageAbove;
        onPlayerSpawned.raise(this, GetComponent<MovePlayer>());
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 20), content, style);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        switch (moveDirection)
        {
            case direction.up:
                transform.position += Vector3.up * speed * Time.deltaTime;
                moveDir = Vector3.up;
                break;
            case direction.down:
                transform.position += Vector3.down * speed * Time.deltaTime;
                moveDir = Vector3.down;
                break;
            case direction.back:
                transform.position += Vector3.back * speed * Time.deltaTime;
                moveDir = Vector3.back;
                break;
            case direction.forward:
                transform.position += Vector3.forward * speed * Time.deltaTime;
                moveDir = Vector3.forward;
                break;
            case direction.left:
                transform.position += Vector3.left * speed * Time.deltaTime;
                moveDir = Vector3.left;
                break;
            case direction.right:
                transform.position += Vector3.right * speed * Time.deltaTime;
                moveDir = Vector3.right;
                break;

        }
        Invoke(gameMode.ToString(), 0);
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        RaycastHit hit;

        content = new GUIContent($"pos: {transform.position}", debugTex, "This is a tooltip");

        
    }

    void DebugPath()
    {
        

    }

    public void Cube()
    {
        
            
        if (Input.GetMouseButton(0) && isGrounded && !isUpsideDown)
        {
            rb.velocity = Vector3.up * jumpHeight;
            isGrounded = false;
            isJumping = true;

        }
        else if (Input.GetMouseButton(0) && isGrounded && isUpsideDown)
        {
            rb.velocity = Vector3.down * jumpHeight;
            isGrounded = false;
            isJumping = true;
        }
        if ((isJumping && !isGrounded) || (!isJumping && !isGrounded))
        {
            switch (rotateDirection)
            {
                case direction.up:
                    cubeModel.transform.Rotate(0,rotationSpeed * Time.deltaTime, 0);
                    break;
                case direction.down:
                    cubeModel.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                    break;
                case direction.back:
                    cubeModel.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
                    break;
                case direction.forward:
                    cubeModel.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
                    break;
                case direction.left:
                    cubeModel.transform.Rotate(0, 0,rotationSpeed * Time.deltaTime);
                    break;
                case direction.right:
                    cubeModel.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                    break;

            }
        }
        else if(isGrounded)
        {
            cubeModel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && isJumping == true)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Ship()
    {
        // gravity changing values (-1,5 * gravity) are subject to change
        cubeModel.transform.rotation = Quaternion.Euler(rb.velocity.y * -2, 0, 0);
        if(!isUpsideDown)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            if (Input.GetMouseButton(0))
            {
                Physics.gravity = new Vector3(0, -shipYVelocity * -9.81f, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, shipYVelocity* -9.81f, 0);
            }
        }
        else if (isUpsideDown)
        {
            Physics.gravity = new Vector3(0, 9.81f, 0);
            if (Input.GetMouseButton(0))
            {
                Physics.gravity = new Vector3(0, -shipYVelocity * 9.81f, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, shipYVelocity * 9.81f, 0);
            }
        }
        
    }
    public void Ball()
    {
        cubeModel.transform.Rotate(0, 0, -2f);
        if (Input.GetMouseButton(0) && !isUpsideDown && isGrounded)
        {
            isUpsideDown = true;
            isGrounded = false;
            Physics.gravity = new Vector3(0, 9.81f, 0);
            transform.rotation = Quaternion.Euler(0, 0, 180);

        }
        else if (Input.GetMouseButton(0) && isUpsideDown && isGrounded) 
        {
            isUpsideDown = false;
            isGrounded = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = true;
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 8)
        {
            isCollidingWithTrigger = true;
            if(isCollidingWithTrigger)
            {
                onTriggerEnter.raise(this, GetComponent<MovePlayer>(), isUpsideDown, 6, other.tag, 2);
                isCollidingWithTrigger = false;
            }
        }
        
        
        Debug.Log(other.tag);
        
        
        
    
    }
    private void OnTriggerExit(Collider other)
    {
        isCollidingWithTrigger = false;
    }


}
