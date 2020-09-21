using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform _characterBody;
    [SerializeField]
    private Transform _cameraArm;

    [SerializeField, Range(1, 25)]
    private int _mouseSensitivity;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = _characterBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        //_animator.SetBool("IsMove", isMove);

        float speed = Input.GetKey(KeyCode.LeftShift) ? 12f : 5f;
        
        if(isMove)
        {
            Vector3 lookForward = new Vector3(_cameraArm.forward.x, 0f, _cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(_cameraArm.right.x, 0f, _cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            transform.position += moveDir * Time.deltaTime * speed;
        }
    }

    private void Rotate()
    {
        float sensitivity = _mouseSensitivity / 10f;
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * sensitivity, Input.GetAxis("Mouse Y") * sensitivity);
        Vector3 camAngle = _cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180f ? Mathf.Clamp(x, -1f, 70f) : Mathf.Clamp(x, 345f, 361f);

        _characterBody.rotation = Quaternion.Euler(0, camAngle.y + mouseDelta.x, camAngle.z);
        _cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        Debug.Log(x);
    }
}
