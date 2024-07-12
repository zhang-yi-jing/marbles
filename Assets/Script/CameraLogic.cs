using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    GameObject player;
    Animator m_animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.z -= 10;
        transform.position = playerPosition;

        if (Input.GetButtonDown("Fire1"))
        {
            m_animator.SetTrigger("Shake");
        }
    }

    public void Shake()
    {
        m_animator.SetTrigger("Shake");
    }
}
