using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // �̵� �ӵ�
    public float mouseSensitivity = 300f;  // ���콺 ����

    private Rigidbody rb;
    private Transform cameraTransform;
    private float xRotation = 0f;





    /*
    public Rigidbody playerRigidbody; //�÷��̾� �̵� ������ٵ� ������Ʈ
    public float speed = 8f; // �̵� �ӷ�

    public float lookSensitivity; // �þ� ȸ�� ����

    public float cameraRotationLimit;
    public float currentCameraRotationX;

    public Camera theCamera;
    */


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Ŀ���� ��� ���·� ��ȯ
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

        // �÷��̾��� ȸ���� ���� (�¿�� ȸ��)
        transform.Rotate(Vector3.forward * mouseX);

        // ī�޶��� ȸ���� ���� (���Ʒ��� ȸ��)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �̵� �Է�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // �÷��̾��� �̵� ������ ���
        Vector3 moveDirection = transform.right * x + transform.forward * z;

        // �÷��̾��� �̵� ���� ���͸� ����ȭ�Ͽ� �ӵ��� ����
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // �̵� �Է¿� ���� �÷��̾��� ���� ���͸� ȸ�����Ѽ� �̵� ������ ����
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 desiredMoveDirection = forward * z + right * x;

        // �̵� ������ ���� �̵�
        Vector3 velocity = desiredMoveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + velocity);






        /*
        Move(); // ����Ű(wasd) �Է¿� ���� �̵�
        PlayerRotationUD(); // ���콺 ���Ʒ���(Y����) �����̸� �÷��̾� x�� ȸ��(UD: UpDown)
        PlayerRotationLR(); // ���콺 �¿��(X����) �����̸� �÷��̾� y�� ȸ��(LR: LeftRight)
        /*
        // ����Ű �Է� ���� - �̵�
        if (Input.GetKey(KeyCode.W) == true)  // wŰ �Է� �� z�������� �� �ֱ�(��)
        {
            playerRigidbody.AddForce(0f, 0f, speed);
        }
        if (Input.GetKey(KeyCode.S) == true)  // sŰ �Է� �� -z�������� �� �ֱ�(��)
        {
            playerRigidbody.AddForce(0f, 0f, -speed);
        }
        if (Input.GetKey(KeyCode.D) == true)  // dŰ �Է� �� x�������� �� �ֱ�(��)
        {
            playerRigidbody.AddForce(speed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A) == true)  // aŰ �Է� �� -x�������� �� �ֱ�(��)
        {
            playerRigidbody.AddForce(-speed, 0f, 0f);
        }
        */
    }

    





    /*
    public void Move()
    {
        // GetAxisRaw: -1, 0, 1�� ��, moveX & moveZ: �̵� ũ��
        float moveX = Input.GetAxisRaw("Horizontal"); // �¿��̵�(�����̵�), aŰ, �޹���Ű �Է�: -1, �Է¾���: 0, dŰ, �����Ű �Է�: 1
        float moveZ = Input.GetAxisRaw("Vertical"); // �����̵�(�����̵�, �յ�), sŰ, �Ϲ���Ű �Է�: -1, �Է¾���: 0, wŰ, �����Ű �Է�: 1

        Vector3 moveHorizontal = transform.right * moveX; // �¿��̵� ���Ͱ�(����*ũ��)
        Vector3 moveVertical = transform.forward * moveZ; // �����̵� ���Ͱ�(����*ũ��)

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed; // �ӵ� ���Ͱ�(����((�¿��̵����� + �յ��̵�����).normalized)*�ӵ�ũ��)

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
        gameObject.SetActive(false); // �ڱ� �ڽ� ������Ʈ ��Ȱ��ȭ
    }
}
