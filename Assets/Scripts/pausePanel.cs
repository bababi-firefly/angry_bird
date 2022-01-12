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

    // 点击暂停按钮
    public void Pause()
    {
        // 1 播放pause动画
        Debug.Log("click on pause");
        anim.SetBool("isPause", true);
        pauseButton.SetActive(false);
        
    }

    public void Home()
    {

    }

    // 点击继续按钮
    public void Resume()
    {
        // 1 播放resume动画
        Debug.Log("resume click");
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    // 暂停动画播放完
    public void PauseAnimaEnd()
    {
        Time.timeScale = 0;
    }

    // 恢复动画播放完
    public void ResumeAnimaEnd()
    {
        pauseButton.SetActive(true);
    }
}
