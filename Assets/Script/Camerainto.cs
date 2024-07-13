using UnityEngine;

public class Camerainto : MonoBehaviour
{
    public Camera mainCamera; // �������
    public GameObject targetObject;
    private CollisionDetector collisionDetector; // CollisionDetector�ű�������
    private Vector3 targetPosition; // Ŀ��λ��

    private float cameraSizeTarget; // Ŀ���������orthographic size
    private float cameraSizeTransitionSpeed = 5f; // �����orthographic size�Ľ����ٶ�
    private float positionTransitionSpeed = 20f; // λ�õĽ����ٶ�

    private bool isTransitioning = false; // �Ƿ����ڽ��н���

    private Vector3 initialCameraPosition; // ������ĳ�ʼλ��

    private void Start()
    {
        // ��ȡCollisionDetector�ű�������
        collisionDetector = targetObject.GetComponent<CollisionDetector>();
    }

    private void Update()
    {
        // ���CollisionDetector�ű��е�isCameraintoֵ
        if (collisionDetector != null && collisionDetector.isCamerainto)
        {
            // ��ʼ�����������orthographic size��λ��
            StartTransition();
        }
        else if(collisionDetector != null && !collisionDetector.isCamerainto)
        {
            EndTransition();
        }

        // �������������
        if (isTransitioning)
        {
            TransitionCamera();
        }
    }

    private void StartTransition()
    {
        isTransitioning = true;
        targetPosition = collisionDetector.colliderPosition; // ʹ��CollisionDetector�ű��е�colliderPosition��ΪĿ��λ��
        cameraSizeTarget = 2.5f;
    }

    private void EndTransition()
    {
        isTransitioning = true;
        targetPosition = initialCameraPosition; // ʹ��CollisionDetector�ű��е�colliderPosition��ΪĿ��λ��
        cameraSizeTarget = 5f; // ������ĳ�ʼ��С

    }
    private void TransitionCamera()
    {
        float sizeLerpFactor;
        if (cameraSizeTarget == 5f)
        {
            float sizeProgress = Mathf.InverseLerp(cameraSizeTarget * 0.5f, cameraSizeTarget, mainCamera.orthographicSize);
            sizeLerpFactor =1f - Mathf.SmoothStep(0f, 1f, sizeProgress);
        }
        else
        {
            sizeLerpFactor = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(cameraSizeTarget * 0.5f, cameraSizeTarget, mainCamera.orthographicSize));
        }

        // �����������orthographic size
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraSizeTarget, sizeLerpFactor * cameraSizeTransitionSpeed * Time.deltaTime);

        // ���������orthographic size�Ĳ�ֵϵ��
        

        // �����������λ��
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, positionTransitionSpeed * Time.deltaTime);

        // ����Ƿ���ɽ���
        if (Mathf.Approximately(mainCamera.orthographicSize, cameraSizeTarget) && mainCamera.transform.position == targetPosition)
        {
            // ������ɣ�ֹͣ����
            isTransitioning = false;
        }
    }
}