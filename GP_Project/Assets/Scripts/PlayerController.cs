using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody; //�÷��̾� �̵� ������ٵ� ������Ʈ
    public float speed = 8f; // �̵� �ӷ�

    public float lookSensitivity; // �þ� ȸ�� ����

    public float cameraRotationLimit;
    public float currentCameraRotationX;

    public Camera theCamera;



    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
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

    public void Die()
    {
        gameObject.SetActive(false); // �ڱ� �ڽ� ������Ʈ ��Ȱ��ȭ
    }
}
