using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<redBird> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 originPos; // ��ʼλ��
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0) {
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        NextBird();
    }

    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++) {
            if (i == 0) {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            } else {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    public void NextBird()
    {
        if (pigs.Count > 0) {
            if (birds.Count > 0) {
                // ��������һֻ��
                Initialized();
            } else {
                // ����
                lose.SetActive(true);
                Debug.Log("You Lose");
            }
        } else { // Ӯ��
            win.SetActive(true);
            ShowStar();
            Debug.Log("You Win");
        }
    }

    public void ShowStar()
    {
        StartCoroutine("show");
    }

    IEnumerator show() // Э��
    {
        for (int i = 0; i < birds.Count; i++) {
            yield return new WaitForSeconds(1.0f);
            stars[i].SetActive(true);
        }
    }

    public void Replay()
    {
        Debug.Log("retry clicked");
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        Debug.Log("home clicked");
        SceneManager.LoadScene(1);
    }
}
