using UnityEngine;

public class FreeCamera : MonoBehaviour {

    Transform cam_tfm;
    public Transform target_tfm;//lookAt
    private float currentX = 0f;
    private float currentY = 0f;
    private const float yminAngle = -70f;
    private const float ymaxAngle = -30f;

    void Start()
    {
        cam_tfm = GetComponent<Transform>();
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        if (Input.GetKey("up"))
        {
            currentY += 2f;
        }
        if (Input.GetKey("down"))
        {
            currentY -= 2f;
        }
        currentY = Mathf.Clamp(currentY,yminAngle,ymaxAngle);
        if (Input.GetKey("left"))
        {
            currentX += 2f;
        }
        if (Input.GetKey("right"))
        {
            currentX -= 2f;
        }
    }
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0,20,-1f);
        Quaternion rotation = Quaternion.Euler(currentY, currentX,0);
        transform.position = target_tfm.position + rotation*dir;
        transform.LookAt(target_tfm.position);
        //transform.Translate(Vector3.right * Time.deltaTime);
//        transform.RotateAround(target_tfm.position, Vector3.up, rot_speed * Time.deltaTime);
    }
}
