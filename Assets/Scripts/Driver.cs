using System.Diagnostics.Tracing;
using System.Reflection.Metadata;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class Driver : MonoBehaviour
{
    [SerializeField]float steerSpeed = 0.2f;
    [SerializeField]float currentCarSpeed = 5f;
    [SerializeField]float boostCarSpeed = 15f;
    [SerializeField]float normalCarSpeed = 5f;

    [SerializeField] TMP_Text boostText;
    [SerializeField] TMP_Text packagesUI;
    GameObject[] packagesToFind;
    float steer = 0f;
    float direction = 0f;
    String packagesUItext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        packagesUItext = packagesUI.text;
        packagesToFind = GameObject.FindGameObjectsWithTag ("");
        boostText.gameObject.SetActive(false);
        packagesUI.text = packagesUItext + packagesToFind.Length;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            currentCarSpeed = boostCarSpeed;
            boostText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        currentCarSpeed = normalCarSpeed;
        boostText.gameObject.SetActive(false);
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
        packagesToFind = GameObject.FindGameObjectsWithTag ("Package");
        packagesUI.text = packagesUItext + packagesToFind.Length;

        float moveAmmount = direction * currentCarSpeed * Time.deltaTime;
        float steerAmmount = steer * steerSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0, moveAmmount, 0), Space.Self);
        transform.Rotate(0, 0, steerAmmount);
    }

    


}
