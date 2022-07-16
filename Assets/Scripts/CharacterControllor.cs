using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterControllor : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Transform>();
        t = transform;
        rig = gameObject.GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(alive == true)
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
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Bullet")
        {
            rig.velocity = Vector2.zero;
            alive = false;
        }
    }

    public void Shoot()
    {
        //Instantiate(projectile[0], shotPos.transform.position, shotPos.transform.rotation);

        int value = BulletSelection.instance.DiceValue;

        if (value != 0)
        {
            Instantiate(projectile[value - 1], shotPos.transform.position, shotPos.transform.rotation);
        }

        BulletSelection.instance.Shotbullet();
    }
}
