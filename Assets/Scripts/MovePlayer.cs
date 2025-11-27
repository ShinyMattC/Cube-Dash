using System.Collections;
using System.Collections.Generic;
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
    public Transform groundCheck;


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

    


    public LevelEditorManager editor;
    float velocity;
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
        
    }
    public void OnEnable()
    {
        GameObject[] spikes = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject s in spikes)
        {
            Spike _S = s.GetComponent<Spike>();
            _S.SetPlayer(this.gameObject);
        }
        //AudioSource.PlayClipAtPoint(aus.clip, transform.position);
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
        Invoke(gameMode.ToString(), Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        RaycastHit hit;

        
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
        if (isJumping && !isGrounded || !isJumping && !isGrounded)
        {
            switch (rotateDirection)
            {
                case direction.up:
                    transform.Rotate(0,rotationSpeed, 0);
                    break;
                case direction.down:
                    transform.Rotate(0, rotationSpeed, 0);
                    break;
                case direction.back:
                    transform.Rotate(rotationSpeed, 0, 0);
                    break;
                case direction.forward:
                    transform.Rotate(rotationSpeed, 0, 0);
                    break;
                case direction.left:
                    transform.Rotate(0, 0,rotationSpeed);
                    break;
                case direction.right:
                    transform.Rotate(0, 0, -rotationSpeed);
                    break;

            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
        transform.rotation = Quaternion.Euler(rb.velocity.y * -2, 0, 0);
        if(!isUpsideDown)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            if (Input.GetMouseButton(0))
            {
                //rb.velocity += new Vector3(0, shipYVelocity, 0);
                //Mathf.Clamp(rb.velocity.y, 0, 10);
                Physics.gravity = new Vector3(0, -shipYVelocity * -9.81f, 0);
            }
            else
            {
                //rb.velocity += new Vector3(0, -shipYVelocity &, 0);
                Physics.gravity = new Vector3(0, shipYVelocity* -9.81f, 0);
            }
        }
        else if (isUpsideDown)
        {
            Physics.gravity = new Vector3(0, 9.81f, 0);
            if (Input.GetMouseButton(0))
            {
                //rb.velocity += new Vector3(0, -shipYVelocity, 0);
                //Mathf.Clamp(rb.velocity.y, 0, -10);
                Physics.gravity = new Vector3(0, -shipYVelocity * 9.81f, 0);
            }
            else
            {
                //rb.velocity += new Vector3(0, shipYVelocity, 0);
                Physics.gravity = new Vector3(0, shipYVelocity * 9.81f, 0);
            }
        }
        
    }
    public void Ball()
    {
        transform.Rotate(0, 0, -2f);
        if (Input.GetMouseButton(0) && !isUpsideDown)
        {
            isUpsideDown = true;
            Physics.gravity = new Vector3(0, 9.81f, 0);
        }
        else if (Input.GetMouseButton(0) && isUpsideDown) 
        {
            isUpsideDown = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = true;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Yellow Jump Pad":
                other.gameObject.GetComponent<JumpPad>().JumpPadFunction(5.6f);
                break;
            case "Obstacle":
                other.gameObject.GetComponent<Spike>().SetPlayer(this.gameObject);
                other.gameObject.GetComponent<Spike>().Die();
                break;
            case "Yellow Jump Orb":
            
                    //other.gameObject.GetComponent<JumpOrb>().JumpOrbFunction(8);
                if (Input.GetMouseButton(0))
                {
                    JumpOrbFunction(8);
                }
                break;
            case "Level End":
                SceneManager.LoadScene(2);
                break;
            case "Yellow Portal":
                other.gameObject.GetComponent<YellowPortal>().ReverseGravity();
                isUpsideDown = true;
                break;
            case "Blue Portal":
                isUpsideDown = false;
                other.gameObject.GetComponent<BluePortal>().RevertGravity();
                break;
            case "Ship Portal":
                other.gameObject.GetComponent<GamemodePortal>().ChangeGamemode(gamemode.Ship);
                break;
            case "Cube Portal":
                other.gameObject.GetComponent<GamemodePortal>().ChangeGamemode(gamemode.Cube);
                break;
            case "Ball Portal":
                other.gameObject.GetComponent<GamemodePortal>().ChangeGamemode(gamemode.Ball);
                break;
        }
    }
    public void JumpOrbFunction(float jumpForce)
    {
        /*if(playerMove.isUpsideDown == false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            playerRb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }*/
        if(Input.GetMouseButton(0))
        {
            rb.AddForce(((isUpsideDown) ? Vector3.down : Vector3.up) * jumpForce, ForceMode.Impulse);
        }
        
    }
}
