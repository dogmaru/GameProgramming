using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public float moveSpeed = 10f;  // �̵� �ӵ�
    public float mouseSensitivity = 300f;  // ���콺 ����

    public Rigidbody rb;
    private Transform cameraTransform;
    private float xRotation = 0f;

    // �÷��̾� �̵� ���� ���� ����, ���� ���� ����
    private float minY = -55.45f;
    private float maxY = 5f;
    private float minX = -382.5f;
    private float maxX = 379.7f;
    private float minZ = -382f;
    private float maxZ = 380.6f;

    // �� ��, �� �� skybox �޸� ���� ���� ����
    public Material originalSkybox; // ���� skybox(�� ��)
    public Material skyboxUnderWater; // �� ��
    public float waterLevel = -5.5f; // �� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Ŀ���� ��� ���·� ��ȯ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // �ʱ� skybox ����
        originalSkybox = RenderSettings.skybox;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // �÷��̾��� ȸ���� ���� (�¿�� ȸ��)
        transform.Rotate(Vector3.forward * mouseX);


        // ī�޶��� ȸ���� ���� (���Ʒ��� ȸ��)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -0f, 180f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �̵� �Է�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ī�޶��� forward�� right ���⿡ ���� ���� ���
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // �÷��̾��� �̵� ���� ���
        Vector3 moveDirection = (forward * z + right * x).normalized;

        // �̵� ������ ���� �̵�
        Vector3 velocity = moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + velocity);

        Vector3 newPosition = rb.position + velocity;

        // ���ο� ��ġ(newPostion)�� ���ѵ� ���� ���� �ִ��� Ȯ���Ͽ� �̵� ���� ������ ����
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        rb.MovePosition(newPosition);

        // �� �� �����Ͽ� skybox ����
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
        gameObject.SetActive(false);
        
        GameManager gameManager =FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }

    /*
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
    */
}
