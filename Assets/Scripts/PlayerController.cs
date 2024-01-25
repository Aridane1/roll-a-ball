using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject continueButtonObject;
    public GameObject exitButtonObject;
    public GameObject temporalTextObject;


    private float powerJump;
    private bool jumpPressed;
    private bool isGrounded;
    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        powerJump = 7.5f;
        jumpPressed = false;
        isGrounded = true;
        rb = GetComponent<Rigidbody>();

        SetCountText();
        winTextObject.SetActive(false);
        continueButtonObject.SetActive(false);
        exitButtonObject.SetActive(false);
        temporalTextObject.SetActive(false);

    }
    void Update()
    {
        if (count == 4)
        {
            DestroyWalls();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movement =movementValue.Get<Vector2>(); 

        movementX = movement.x;
        movementY = movement.y;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 7)
        {
            winTextObject.SetActive(true);
            continueButtonObject.SetActive(true);
            exitButtonObject.SetActive(true);

            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX,0.0f,movementY);
        rb.AddForce(movement*speed);

    }
    void OnJump()
    {   
        jumpPressed = true;

        if (jumpPressed)
        {
            Vector3 jump = new Vector3(0.0f, powerJump, 0.0f);
            if (isGrounded)
            {
                rb.AddForce(jump, ForceMode.Impulse);
            }
        }
        jumpPressed = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void DestroyWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");

        foreach (GameObject wall in walls)
        {
            Destroy(wall);
            MostrarMensajeTemporal(2f);
        }
    }

    void MostrarMensajeTemporal(float duracion)
    {
        temporalTextObject.SetActive(true);

        Invoke("OcultarMensaje", duracion);
    }

    void OcultarMensaje()
    {
        temporalTextObject.SetActive(false);
    }
}
