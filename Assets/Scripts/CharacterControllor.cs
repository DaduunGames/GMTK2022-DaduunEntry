using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterControllor : MonoBehaviour
{

    public int CollectableCount = 0;
    public TextMeshProUGUI CollectableDisplay;
    public GameObject DeathScreen;
    public GameObject WinScreen;
    public GameObject DeathSplat;

    // player values
    [Header("Player values")]
    public Camera mainCamera;
    public GameObject shotPos;
    public GameObject[] projectile;
    private bool alive = true;
    // movement

    [Header("Player Movement")]
    public float maxSpeed = 10f;
    public float moveLimiter = 0.7f;
    float horizontal;
    float vertical;


    // 2d values
    [Header("Rigbody & Collider")]
    private Rigidbody2D rig;
    private CapsuleCollider2D col;

    // values that doesn't need to be viewed
    private Vector3 cameraPos;
    private Transform t;
    private Transform l;

    private Animator anim;

    [SerializeField] private FieldOfView fieldOfView;

    public AudioSource ShootS;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Transform>();
        t = transform;
        rig = gameObject.GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        CollectableDisplay.text = $"Find All the Dice: {CollectableCount}/8";

        //fieldOfView.SetAimDirection();
        //fieldOfView.SetOrigin(transform.position);

        if(CollectableCount >= 8)
        {
            WinScreen.SetActive(true);
            alive = false;
        }

        if (alive == true)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");        
            
            // Distance from camera to object.  We need this to get the proper calculation.
            float camDis = mainCamera.transform.position.y - l.position.y;

            // Get the mouse position in world space. Using camDis for the Z axis.
            Vector3 mouse = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));
            float AngleRad = Mathf.Atan2(mouse.y - l.position.y, mouse.x - l.position.x);
            float angle = (180 / Mathf.PI) * AngleRad;
            rig.rotation = angle;

            // BANG!
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y, cameraPos.z);
        }



    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        rig.velocity = new Vector2(horizontal * maxSpeed, vertical * maxSpeed);

        if(rig.velocity.magnitude >= 0.1)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            rig.velocity = Vector2.zero;
            rig.isKinematic = true;
            alive = false;
            maxSpeed = 0;
            Instantiate(DeathSplat, transform.position, transform.rotation);
            Invoke("DisplayDeathScreen", 3);
        }

        if(col.gameObject.tag == "Collectable"){
            Destroy(col.gameObject);
            CollectableCount++;
        }
    }

    public void DisplayDeathScreen()
    {
        DeathScreen.SetActive(true);
    }
    public void Shoot()
    {
       
        //Instantiate(projectile[0], shotPos.transform.position, shotPos.transform.rotation);

        int value = BulletSelection.instance.DiceValue;

        if (value != 0)
        {
            ShootS.Play();
            Instantiate(projectile[value - 1], shotPos.transform.position, shotPos.transform.rotation);
            anim.SetTrigger("Shoot");
        }

        BulletSelection.instance.Shotbullet();
    }
}
