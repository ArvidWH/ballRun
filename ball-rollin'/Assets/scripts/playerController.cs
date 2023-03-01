using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour
{
    private double jumpDelay = 0;
    private float Edelay = 0;
    private int jumpcount = 2;
    public float jumpheight = 0;
    public float speed = 0;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI timeText;
    public GameObject winTextObject;
    public GameObject dieCanvas;
    public GameObject menu;
    private bool gameOver = false;
    private bool win = false;
    private float winDelay;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Vector3 launch;
    private Vector3 currentGrapple;
    private float Qdelay;
    private bool Q = false;
    private float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        dieCanvas.SetActive(false);
        menu.SetActive(false);
        timer = Time.time;
    }

    private void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        CountText.text = "Score: " + count.ToString();
        
    }
    void SetTimeText()
    {
        timeText.text = "Time: " + Mathf.FloorToInt(Time.time - timer).ToString();
        if (Time.time - timer > 200)
        {
            gameOver = true;
        }
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && jumpcount > 0 && Time.time > jumpDelay)
        {
            if (jumpcount == 1)
            {
                rb.velocity = Vector3.zero;
            }
            Vector3 jump = new Vector3(0.0f, jumpheight, 0.0f);
            rb.AddForce(jump);
            jumpcount -= 1;
            jumpDelay = Time.time + 0.5;

        }
        if (Input.GetKey(KeyCode.E) && Time.time > Edelay)
        {
            speed = 15;
            Edelay = Time.time + 10;

        }
        if (Edelay < Time.time + 7 && speed != 50)
        {
            speed = 5;
        }
        if (Input.GetKey(KeyCode.Q) && Qdelay < Time.time && Q)
        {
            rb.velocity = Vector3.zero;
            launch = currentGrapple - rb.transform.position;
            rb.AddForce(launch * 200);
            Qdelay = Time.time + 2;
            jumpcount = 1;
        }
        if (Input.GetKey(KeyCode.R) && gameOver){
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKey(KeyCode.F) && win)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
        }


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent.name == "ground")
        {
            jumpcount = 2;

        }
        if (collision.gameObject.CompareTag("Boost"))
        {
            speed = 50;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            speed = 5;
        }
    }



    private void FixedUpdate()
    {
        Vector3 movement = new Vector4(movementX, 0.0f, movementY );
        rb.AddForce(movement * speed);
        SetTimeText();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Death") && win == false)
        {
            gameOver = true;
            dieCanvas.SetActive(true);
        }

        if (other.gameObject.transform.parent.CompareTag("Grapple"))
        {
            currentGrapple = other.gameObject.transform.position;
            Qdelay = -10;
            Q = true;
            
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            win = true;
            winTextObject.SetActive(true);  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Q = false;
    }
}