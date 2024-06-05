using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 200f;  // 이동 속도
    public float mouseSensitivity = 300f;  // 마우스 감도

    private Rigidbody rb;
    private Transform cameraTransform;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // 커서를 잠금 상태로 전환
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 플레이어의 회전값 변경 (좌우로 회전)
        transform.Rotate(Vector3.forward * mouseY); // 위아래 이동을 위해 y와 z 축을 변경


        // 카메라의 회전값 변경 (위아래로 회전)
        xRotation -= mouseX; // 마우스 X를 사용하여 좌우 회전
        xRotation = Mathf.Clamp(xRotation, -0f, 180f);
        cameraTransform.localRotation = Quaternion.Euler(0f, xRotation, 0f); // y축을 기준으로 회전

        // 이동 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 플레이어의 이동 방향을 계산
        Vector3 moveDirection = transform.right * x + cameraTransform.forward * z;

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
    }

    public void Die()
    {
        gameObject.SetActive(false); // 자기 자신 오브젝트 비활성화
    }
}
