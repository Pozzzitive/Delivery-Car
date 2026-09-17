using System.Data.Common;
using NUnit.Framework;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField]float packageDestroyDelay = .6f;
    bool hasPackage;
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("Bumped");
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !hasPackage)
        {
            GetComponent<ParticleSystem>().Play(); 
            Destroy(collision.gameObject, packageDestroyDelay);
            Debug.Log("Package touched");
            hasPackage = true;
        }
        if (collision.CompareTag("Customer") && hasPackage)
        {
            GetComponent<ParticleSystem>().Stop(); 
            Debug.Log("Package delivered");
            hasPackage = false;
        }

    }
}
