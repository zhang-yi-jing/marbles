using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bounce");
            AudioManager.Instance.PlayOneShot(AudioList.Instance.audioClips[4]);
        }
    }
}
