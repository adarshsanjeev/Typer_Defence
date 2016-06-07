using UnityEngine;
using System.Collections;

public class arrowCollision : MonoBehaviour
{
    GameObject tower;
    Rigidbody arrow;

    void Awake()
    {
        tower = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == tower)
        {
            arrow = GetComponent<Rigidbody>();
            Vector3 nuller = new Vector3(0, 0, 0);
            arrow.velocity = nuller;
            arrow.AddForce(nuller);
            arrow.isKinematic = true;
        }
    }
}