using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isInTriggerRange = false;
    private Vector2 colliderPosition;
    private float rotationSpeed; // 每秒旋转的角度
    private bool isRotating = false;
    private float triggerRadius = 0f; // 触发区域的半径
    private Rigidbody2D rb;
    private bool isShooting = false;
    private float proximityPercentage;

    private void Start()
    {
        // 获取挂载该脚本的物体的Rigidbody2D组件
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trigger range"))
        {
            isInTriggerRange = true;
            colliderPosition = other.transform.position;
            triggerRadius = other.bounds.extents.x; // 获取触发物体的半径
            Debug.Log("Entered Trigger Range: " + other.gameObject.name + " at position: " + colliderPosition + ", Radius: " + triggerRadius);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trigger range"))
        {
            isInTriggerRange = false;
            
        }
    }

    private void Update()
    {
        if (isInTriggerRange && !isRotating && !isShooting)
        {
            CalculateProximityPercentage();
        }
        

        if (isInTriggerRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRotating)
            {
                // 禁用物体上的Rigidbody组件
                rb.isKinematic = true;
                rb.velocity = Vector2.zero; // 将速度设置为零
                RotateAroundColliderPosition();
                Debug.Log(rb.velocity);
                isShooting = false;
            }

            isRotating = !isRotating;

            if (!isRotating)
            {
                rb.isKinematic = false;
                rb.drag = 0.2f;
                ApplyTangentVelocity();
                isShooting = !isShooting;
            }   
        }

        if (isRotating)
        {
            RotateAroundColliderPosition();            
        }
    }

    private void RotateAroundColliderPosition()
    {
        // 获取挂载有该脚本的物体
        Transform thisTransform = transform;

        if (proximityPercentage <= 0.35f)
        {
            rotationSpeed = 120f;
        }
        else if (proximityPercentage <= 0.55f)
        {
            rotationSpeed = 180f;
        }
        else
        {
            rotationSpeed = 240f;
        }


        // 计算每帧应该旋转的角度
        float deltaAngle = rotationSpeed * Time.deltaTime;

        // 进行旋转
        thisTransform.RotateAround(colliderPosition, Vector3.forward, deltaAngle);
    }

    private void CalculateProximityPercentage()
    {
        // 计算物体与colliderPosition之间的距离
        float distance = Vector2.Distance(transform.position, colliderPosition);

        // 计算百分比
        proximityPercentage = 1f - (distance / triggerRadius);

        // 将百分比限制在0-100%之间
        proximityPercentage = Mathf.Clamp(proximityPercentage, 0f, 1f);

        // 输出百分比
        //Debug.Log("Proximity Percentage: " + (proximityPercentage * 100f) + "%");

        float linearDrag = Mathf.Lerp(5f, 15f, proximityPercentage);
        //Debug.Log(linearDrag);
        rb.drag = linearDrag;
    }

    private void ApplyTangentVelocity()
    {
        // 计算切线方向
        Vector2 tangentDirection = new Vector2(transform.position.y - colliderPosition.y, colliderPosition.x - transform.position.x).normalized;

        // 应用切线速度
        float tangentSpeed = 5f; // 切线速度大小
        rb.velocity = -tangentDirection * tangentSpeed;
    }
}
