using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 200f;  // �̵� �ӵ�
    public float mouseSensitivity = 300f;  // ���콺 ����

    private Rigidbody rb;
    private Transform cameraTransform;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Ŀ���� ��� ���·� ��ȯ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // �÷��̾��� ȸ���� ���� (�¿�� ȸ��)
        transform.Rotate(Vector3.forward * mouseY); // ���Ʒ� �̵��� ���� y�� z ���� ����


        // ī�޶��� ȸ���� ���� (���Ʒ��� ȸ��)
        xRotation -= mouseX; // ���콺 X�� ����Ͽ� �¿� ȸ��
        xRotation = Mathf.Clamp(xRotation, -0f, 180f);
        cameraTransform.localRotation = Quaternion.Euler(0f, xRotation, 0f); // y���� �������� ȸ��

        // �̵� �Է�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // �÷��̾��� �̵� ������ ���
        Vector3 moveDirection = transform.right * x + cameraTransform.forward * z;

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
    }

    public void Die()
    {
        gameObject.SetActive(false); // �ڱ� �ڽ� ������Ʈ ��Ȱ��ȭ
    }
}
