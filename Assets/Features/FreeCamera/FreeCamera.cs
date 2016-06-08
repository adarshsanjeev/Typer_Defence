using UnityEngine;

public class FreeCamera : MonoBehaviour {

    Transform cam_tfm;
    public Transform target_tfm;
    public float rot_speed = 5f;

    void Start()
    {
        cam_tfm = GetComponent<Transform>();
    }

    void Update()
    {
        //transform.LookAt(target_tfm);
        //transform.Translate(Vector3.right * Time.deltaTime);
        transform.RotateAround(target_tfm.position, Vector3.up, rot_speed * Time.deltaTime);
    }
}
