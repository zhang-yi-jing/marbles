using UnityEngine;

public class Camerainto : MonoBehaviour
{
    public Camera mainCamera; // 主摄像机
    public GameObject targetObject;
    private CollisionDetector collisionDetector; // CollisionDetector脚本的引用
    private Vector3 targetPosition; // 目标位置

    private float cameraSizeTarget; // 目标摄像机的orthographic size
    private float cameraSizeTransitionSpeed = 5f; // 摄像机orthographic size的渐变速度
    private float positionTransitionSpeed = 20f; // 位置的渐变速度

    private bool isTransitioning = false; // 是否正在进行渐变

    private Vector3 initialCameraPosition; // 摄像机的初始位置

    private void Start()
    {
        // 获取CollisionDetector脚本的引用
        collisionDetector = targetObject.GetComponent<CollisionDetector>();
    }

    private void Update()
    {
        // 检查CollisionDetector脚本中的isCamerainto值
        if (collisionDetector != null && collisionDetector.isCamerainto)
        {
            // 开始渐变摄像机的orthographic size和位置
            StartTransition();
        }
        else if(collisionDetector != null && !collisionDetector.isCamerainto)
        {
            EndTransition();
        }

        // 进行摄像机渐变
        if (isTransitioning)
        {
            TransitionCamera();
        }
    }

    private void StartTransition()
    {
        isTransitioning = true;
        targetPosition = collisionDetector.colliderPosition; // 使用CollisionDetector脚本中的colliderPosition作为目标位置
        cameraSizeTarget = 2.5f;
    }

    private void EndTransition()
    {
        isTransitioning = true;
        targetPosition = initialCameraPosition; // 使用CollisionDetector脚本中的colliderPosition作为目标位置
        cameraSizeTarget = 5f; // 摄像机的初始大小

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

        // 渐变摄像机的orthographic size
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraSizeTarget, sizeLerpFactor * cameraSizeTransitionSpeed * Time.deltaTime);

        // 计算摄像机orthographic size的插值系数
        

        // 渐变摄像机的位置
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, positionTransitionSpeed * Time.deltaTime);

        // 检查是否完成渐变
        if (Mathf.Approximately(mainCamera.orthographicSize, cameraSizeTarget) && mainCamera.transform.position == targetPosition)
        {
            // 渐变完成，停止渐变
            isTransitioning = false;
        }
    }
}