using UnityEngine;
using System.Collections;

public class archerMovement : MonoBehaviour {

    Transform MainTower;
    private Animator myAnimator;
    
    void Start () {
        myAnimator = GetComponent<Animator>();
    }

    void Awake()
    {
        MainTower = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(MainTower);
    }

    void MovingAI()
    {

    }

    void Update () {
	    
	}
}
