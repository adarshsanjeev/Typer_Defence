using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    public float speed;
    // Use this for initialization
    void Start()
    {
        speed = 20.0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        anim.SetFloat("speed", speed);

        if (Input.GetButtonDown("Die"))
        {
            Debug.Log("D is pressed");
            speed = -0.1f;
            //Animation anim = GetComponent<Animation>();
            //newanim.animation.Stop();
            //anim.IsPlaying("jump");
        }
    }
}
