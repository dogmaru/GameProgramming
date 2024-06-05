using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 속도
    public float mouseSensitivity = 300f;  // 마우스 감도

    private Rigidbody rb;
    private Transform cameraTransform;
    private float xRotation = 0f;





    /*
    public Rigidbody playerRigidbody; //플레이어 이동 리지드바디 컴포넌트
    public float speed = 8f; // 이동 속력

    public float lookSensitivity; // 시야 회전 감도

    public float cameraRotationLimit;
    public float currentCameraRotationX;

    public Camera theCamera;
    */


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // 커서를 잠금 상태로 전환
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;




        /*
        playerRigidbody = GetComponent<Rigidbody>();
        */
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 플레이어의 회전값 변경 (좌우로 회전)
        transform.Rotate(Vector3.forward * mouseX);

        // 카메라의 회전값 변경 (위아래로 회전)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 이동 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 플레이어의 이동 방향을 계산
        Vector3 moveDirection = transform.right * x + transform.forward * z;

        // 플레이어의 이동 방향 벡터를 정규화하여 속도를 유지
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // 이동 입력에 따라 플레이어의 전방 벡터를 회전시켜서 이동 방향을 결정
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 desiredMoveDirection = forward * z + right * x;

        // 이동 방향을 향해 이동
        Vector3 velocity = desiredMoveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + velocity);






        /*
        Move(); // 방향키(wasd) 입력에 따라 이동
        PlayerRotationUD(); // 마우스 위아래로(Y방향) 움직이면 플레이어 x축 회전(UD: UpDown)
        PlayerRotationLR(); // 마우스 좌우로(X방향) 움직이면 플레이어 y축 회전(LR: LeftRight)
        /*
        // 방향키 입력 감지 - 이동
        if (Input.GetKey(KeyCode.W) == true)  // w키 입력 시 z방향으로 힘 주기(상)
        {
            playerRigidbody.AddForce(0f, 0f, speed);
        }
        if (Input.GetKey(KeyCode.S) == true)  // s키 입력 시 -z방향으로 힘 주기(하)
        {
            playerRigidbody.AddForce(0f, 0f, -speed);
        }
        if (Input.GetKey(KeyCode.D) == true)  // d키 입력 시 x방향으로 힘 주기(우)
        {
            playerRigidbody.AddForce(speed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A) == true)  // a키 입력 시 -x방향으로 힘 주기(좌)
        {
            playerRigidbody.AddForce(-speed, 0f, 0f);
        }
        */
    }

    





    /*
    public void Move()
    {
        // GetAxisRaw: -1, 0, 1의 값, moveX & moveZ: 이동 크기
        float moveX = Input.GetAxisRaw("Horizontal"); // 좌우이동(수평이동), a키, 왼방향키 입력: -1, 입력없음: 0, d키, 우방향키 입력: 1
        float moveZ = Input.GetAxisRaw("Vertical"); // 전후이동(수직이동, 앞뒤), s키, 하방향키 입력: -1, 입력없음: 0, w키, 상방향키 입력: 1

        Vector3 moveHorizontal = transform.right * moveX; // 좌우이동 벡터값(방향*크기)
        Vector3 moveVertical = transform.forward * moveZ; // 전후이동 벡터값(방향*크기)

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed; // 속도 벡터값(방향((좌우이동벡터 + 앞뒤이동벡터).normalized)*속도크기)

        playerRigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    public void PlayerRotationUD()
    {
        float xRotation = Input.GetAxis("Mouse Y");
        Vector3 playerRotationX = new Vector3(xRotation, 0f, 0f) * lookSensitivity;
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(playerRotationX));
    }

    public void PlayerRotationLR()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 playerRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(playerRotationY));
    }
    */





    public void Die()
    {
        gameObject.SetActive(false); // 자기 자신 오브젝트 비활성화
    }
}
