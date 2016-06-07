using UnityEngine;
using System.Collections;
using System;

public class ShootArrow : MonoBehaviour {

    int thrust = 80;
    public GameObject arrowobj;
    Vector3 placeArrow = new Vector3(0,4,0);
    GameObject tower;
    GameObject projectile;
    Rigidbody arrow;
    Transform MainTower;

    void Awake()
    {
        tower = GameObject.FindGameObjectWithTag("Player");
        MainTower = GameObject.FindGameObjectWithTag("Player").transform;
        //  isCollided = arrowobj.GetComponent<arrowCollision>();
    }
    void shootArrow()
    {
        var rnd = new System.Random(DateTime.Now.Millisecond);
        Vector3 target_height = new Vector3(0, rnd.Next(0, 3), 0);
        projectile = Instantiate(arrowobj) as GameObject;

        arrow = projectile.GetComponent<Rigidbody>();
        projectile.transform.position = transform.position + placeArrow;

        var lookPos = tower.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(0,0, 0);
        projectile.transform.rotation = Quaternion.Slerp(transform.rotation, rotation,0);

        Vector3 direction = tower.transform.position - transform.position + target_height;
        arrow.AddForce(direction * thrust);
        arrow.velocity = transform.forward * 20;
    }

}
