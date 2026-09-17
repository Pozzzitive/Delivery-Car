using System.Diagnostics.Tracing;
using System.Reflection.Metadata;
using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField]float steerSpeed = 0.2f;
    [SerializeField]float currentCarSpeed = 5f;
    [SerializeField]float boostCarSpeed = 15f;
    [SerializeField]float normalCarSpeed = 5f;
    float steer = 0f;
    float direction = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            currentCarSpeed = boostCarSpeed;
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        currentCarSpeed = normalCarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, steerSpeed);
        
        #region carSteering
        if (!Keyboard.current.wKey.isPressed && !Keyboard.current.sKey.isPressed)
        {
            direction = 0f;
        }

        if ((!Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed) || direction ==0
            ||(!Keyboard.current.aKey.isPressed && direction == 1 && steer == 1) ||(!Keyboard.current.dKey.isPressed && direction == 1 && steer == -1) // anti steer lock when going forward and pressing the wrong key
             ||(!Keyboard.current.aKey.isPressed && direction == -1 && steer == -1) ||(!Keyboard.current.dKey.isPressed && direction == -1 && steer == 1)) // anti steer lock when going backwards and pressing the wrong key
        {
            steer = 0f;
        }
       
        if (Keyboard.current.wKey.isPressed && direction!=-1f)
        {
            direction = 1f;

            if (Keyboard.current.aKey.isPressed && steer != -1f)
            {
                steer = 1f;
            } 
            if (Keyboard.current.dKey.isPressed && steer != 1f)
            {
                steer = -1f;
            }

        } 
        
        if (Keyboard.current.sKey.isPressed && direction!=1f)
        {
            direction = -1f;
             if (Keyboard.current.aKey.isPressed && steer != 1f)
            {
                steer = -1f;
            }
            if (Keyboard.current.dKey.isPressed && steer != -1f)
            {
                steer = 1f;
            }
        }
        #endregion


        float moveAmmount = direction * currentCarSpeed * Time.deltaTime;
        float steerAmmount = steer * steerSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, moveAmmount, 0), Space.Self);
        transform.Rotate(0, 0, steerAmmount);
    }

    


}
