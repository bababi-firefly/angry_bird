using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject pauseButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Retry()
    {
        Debug.Log("retry clicked");
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    // �����ͣ��ť
    public void Pause()
    {
        // 1 ����pause����
        Debug.Log("click on pause");
        anim.SetBool("isPause", true);
        pauseButton.SetActive(false);
        
    }

    public void Home()
    {

    }

    // ���������ť
    public void Resume()
    {
        // 1 ����resume����
        Debug.Log("resume click");
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    // ��ͣ����������
    public void PauseAnimaEnd()
    {
        Time.timeScale = 0;
    }

    // �ָ�����������
    public void ResumeAnimaEnd()
    {
        pauseButton.SetActive(true);
    }
}
