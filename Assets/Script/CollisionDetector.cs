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
    private bool isSpace = false;
    private float proximityPercentage;
    private float tangentSpeed;
    private float enterVelocity;
    private float resistance;


	private bool isCanPlay = true;

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
            enterVelocity = rb.velocity.magnitude;
            resistance = enterVelocity + 4f;
            Debug.Log("Entered Trigger Range: " + other.gameObject.name + ", Velocity: " + enterVelocity);
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
            //Debug.Log("Exited Trigger Range: " + other.gameObject.name);
            isInTriggerRange = false;
            isShooting = false;
            tangentSpeed = 0f;
            if (!isSpace)
            {
                //Debug.Log(111);
                rb.drag = 0.2f;
                rb.gravityScale = 0.2f;
                rb.velocity = rb.velocity.normalized * enterVelocity;
                isSpace = false;
            }
            isSpace = false;
        }
    }

    private void Update()
    {
        if (isInTriggerRange && !isRotating && !isShooting)
        {
            CalculateProximityPercentage();
        }
		else
		{
			isCanPlay = true;
		}
        

        if (isInTriggerRange && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            isSpace = true;
            if (!isRotating)
            {
				AudioManager.Instance.PlayOneShot(AudioList.Instance.audioClips[3]);

                // ���������ϵ�Rigidbody���
                rb.isKinematic = true;
                rb.velocity = Vector2.zero; // ���ٶ�����Ϊ��
                RotateAroundColliderPosition();
                //Debug.Log(rb.velocity);
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
			AudioManager.Instance.PlayAudio(AudioList.Instance.audioClips[2]);

        }
		else
		{
			AudioManager.Instance.StopAudio(AudioList.Instance.audioClips[2]);
		}
    }

    private void RotateAroundColliderPosition()
    {
        // ��ȡ�����иýű�������
        Transform thisTransform = transform;


        if (proximityPercentage <= 0.35f)
        {
            rotationSpeed = 150f;
            tangentSpeed = 5f;
        }
        else if (proximityPercentage <= 0.55f)
        {
            rotationSpeed = 220f;
            tangentSpeed = 7f;
        }
        else
        {
            rotationSpeed = 280f;
            tangentSpeed = 9f;
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
		//if proximityPercentage > 0.35f, PlayOneShot(AudioList.Instance.audioClips[0]);
		if (proximityPercentage > 0.05f)
		{
			if (isCanPlay)
			{
				AudioManager.Instance.PlayOneShot(AudioList.Instance.audioClips[1]);
				isCanPlay = false;
			}
		} 

        // ����ٷֱ�
        //Debug.Log("Proximity Percentage: " + (proximityPercentage * 100f) + "%");

        float linearDrag = Mathf.Lerp(1f, resistance, proximityPercentage);
        //Debug.Log(linearDrag);
        rb.drag = linearDrag;
    }

    private void ApplyTangentVelocity()
    {
		AudioManager.Instance.PlayOneShot(AudioList.Instance.audioClips[0]);
        // �������߷���
        Vector2 tangentDirection = new Vector2(transform.position.y - colliderPosition.y, colliderPosition.x - transform.position.x).normalized;
        rb.gravityScale = 0.2f;
        // Ӧ�������ٶ�
        rb.velocity = -tangentDirection * tangentSpeed;
    }
}
