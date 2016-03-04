using UnityEngine;
using System.Collections;

public class FPSCam : MonoBehaviour
{
    public GameObject _cam;
    public float speed = 6.0F;
    private CharacterController controller;
    private Vector3 mMoveDir;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        Cam_Move();
        xuanzhuanScene();
    }
    void xuanzhuanScene()
    {
        if (Input.GetMouseButton(1))
        {
            float mousX = Input.GetAxis("Mouse X");
            this.transform.transform.Rotate(0, mousX, 0);
            float mousY = Input.GetAxis("Mouse Y");
            _cam.transform.Rotate(-mousY * Time.timeScale, 0, 0);
            if (_cam.transform.eulerAngles.x > 30 && _cam.transform.eulerAngles.x < 180)
            {
                _cam.transform.eulerAngles = new Vector3(30, _cam.transform.eulerAngles.y, _cam.transform.eulerAngles.z);
            }
            if (_cam.transform.eulerAngles.x < 330 && _cam.transform.eulerAngles.x > 180)
            {
                _cam.transform.eulerAngles = new Vector3(330, _cam.transform.eulerAngles.y, _cam.transform.eulerAngles.z);
            }

        }
    }
    void Cam_Move()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= 20 * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
