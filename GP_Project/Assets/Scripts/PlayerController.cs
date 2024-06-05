using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;  // 이동 속도
    public float mouseSensitivity = 300f;  // 마우스 감도

    private Rigidbody rb;
    private Transform cameraTransform;
    private float xRotation = 0f;

    // 플레이어 이동 가능 영역 제한, 제한 영역 범위
    private float minY = -55.45f;
    private float maxY = 5f;
    private float minX = -382.5f;
    private float maxX = 379.7f;
    private float minZ = -382f;
    private float maxZ = 380.6f;

    // 물 밖, 물 속 skybox 달리 설정 관련 변수
    public Material originalSkybox; // 기존 skybox(물 밖)
    public Material skyboxUnderWater; // 물 속
    public float waterLevel = -5.5f; // 물 높이

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // 커서를 잠금 상태로 전환
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 초기 skybox 저장
        originalSkybox = RenderSettings.skybox;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 플레이어의 회전값 변경 (좌우로 회전)
        transform.Rotate(Vector3.forward * mouseX);


        // 카메라의 회전값 변경 (위아래로 회전)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -0f, 180f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 이동 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 카메라의 forward와 right 방향에 대한 벡터 계산
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 플레이어의 이동 방향 계산
        Vector3 moveDirection = (forward * z + right * x).normalized;

        // 이동 방향을 향해 이동
        Vector3 velocity = moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + velocity);

        Vector3 newPosition = rb.position + velocity;

        // 새로운 위치(newPostion)가 제한된 영역 내에 있는지 확인하여 이동 가능 영역을 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        rb.MovePosition(newPosition);

        // 물 속 감지하여 skybox 변경
        if (newPosition.y < waterLevel)
        {
            RenderSettings.skybox = skyboxUnderWater;
        }
        else
        {
            RenderSettings.skybox = originalSkybox;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false); // 자기 자신 오브젝트 비활성화
    }
}
