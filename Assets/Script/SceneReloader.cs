using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        // ��ȡ��ǰ����������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���¼��ص�ǰ����
        SceneManager.LoadScene(currentSceneIndex);

        
    }
}