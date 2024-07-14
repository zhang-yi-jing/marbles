using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToFirstScene();
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        // ��ȡ��ǰ����������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���¼��ص�ǰ����
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReturnToFirstScene()
    {
        Time.timeScale = 1f;
        // ���ص�һ������������Ϊ 0��
        SceneManager.LoadScene(0);
    }
}