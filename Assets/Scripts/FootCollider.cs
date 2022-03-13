using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") //same behaviour as obsticle
        {
            Destroy(other.gameObject);
            print("Enemy hit");
        }

    }
}
