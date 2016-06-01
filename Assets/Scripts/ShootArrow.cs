using UnityEngine;
using System.Collections;

public class ShootArrow : MonoBehaviour {

    public int thrust;
    public Rigidbody arrow;
    public GameObject target;
    public Vector3 target_height = new Vector3(0, 100, 0);
	// Use this for initialization
	void Start () {

        Debug.Log(Time.deltaTime);
        arrow = GetComponent<Rigidbody>();
        thrust = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetButtonDown("Jump"))
        {
            Vector3 direction = target.transform.position - this.transform.position + target_height ;
            arrow.AddForce(direction * thrust);
            Debug.Log("Space is pressed");
            //this.gameObject.transform.Translate(Vector3.down * Time.deltaTime * thrust);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Plane" || col.gameObject.name == "Guard02")
        {
            if(col.gameObject.name == "Guard02")
            {
                Debug.Log("Collided with dude");
            }
            Debug.Log("Collided with plane");
            Vector3 nuller = new Vector3(0, 0, 0);
            arrow.velocity = nuller;
            thrust = 0;
        }
    }
}
