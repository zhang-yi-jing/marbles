using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isInTriggerRange = false;
    private Vector2 colliderPosition;
    private float rotationSpeed; // ÿ����ת�ĽǶ�
    private bool isRotating = false;
    private float triggerRadius = 0f; // ��������İ뾶
    private Rigidbody2D rb;
    private bool isShooting = false;
    private float proximityPercentage;
    private float tangentSpeed;

    private void Start()
    {
        // ��ȡ���ظýű��������Rigidbody2D���
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trigger range"))
        {
            isInTriggerRange = true;
            colliderPosition = other.transform.position;
            triggerRadius = other.bounds.extents.x; // ��ȡ��������İ뾶
            //Debug.Log("Entered Trigger Range: " + other.gameObject.name + " at position: " + colliderPosition + ", Radius: " + triggerRadius);
            //Debug.Log(isInTriggerRange);
            //Debug.Log(isRotating);
            //Debug.Log(isShooting);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trigger range"))
        {
            Debug.Log("Exited Trigger Range: " + other.gameObject.name);
            isInTriggerRange = false;
            isShooting = false;
            tangentSpeed = 0f;
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
                // ���������ϵ�Rigidbody���
                rb.isKinematic = true;
                rb.velocity = Vector2.zero; // ���ٶ�����Ϊ��
                RotateAroundColliderPosition();
                Debug.Log(rb.velocity);
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
        // ��ȡ�����иýű�������
        Transform thisTransform = transform;

        if (proximityPercentage <= 0.35f)
        {
            rotationSpeed = 90f;
            tangentSpeed = 3f;
        }
        else if (proximityPercentage <= 0.55f)
        {
            rotationSpeed = 150f;
            tangentSpeed = 5f;
        }
        else
        {
            rotationSpeed = 240f;
            tangentSpeed = 8f;
        }


        // ����ÿ֡Ӧ����ת�ĽǶ�
        float deltaAngle = rotationSpeed * Time.deltaTime;

        // ������ת
        thisTransform.RotateAround(colliderPosition, Vector3.forward, deltaAngle);
    }

    private void CalculateProximityPercentage()
    {
        rb.gravityScale = 0;
        // ����������colliderPosition֮��ľ���
        float distance = Vector2.Distance(transform.position, colliderPosition);

        // ����ٷֱ�
        proximityPercentage = 1f - (distance / triggerRadius);

        // ���ٷֱ�������0-100%֮��
        proximityPercentage = Mathf.Clamp(proximityPercentage, 0f, 1f);

        // ����ٷֱ�
        //Debug.Log("Proximity Percentage: " + (proximityPercentage * 100f) + "%");

        float linearDrag = Mathf.Lerp(1f, 5f, proximityPercentage);
        //Debug.Log(linearDrag);
        rb.drag = linearDrag;
    }

    private void ApplyTangentVelocity()
    {
        // �������߷���
        Vector2 tangentDirection = new Vector2(transform.position.y - colliderPosition.y, colliderPosition.x - transform.position.x).normalized;
        rb.gravityScale = 0.2f;
        // Ӧ�������ٶ�
        rb.velocity = -tangentDirection * tangentSpeed;
    }
}
