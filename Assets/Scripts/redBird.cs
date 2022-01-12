using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBird : MonoBehaviour
{
    private bool isClick = false;
    
    public float maxDis = 3;
    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rd;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;
    public GameObject birdBoom;

    private TestMyTrail myTrail;

    private bool canMove = true;
    public float smooth = 3;

    public AudioClip birdSelect;
    public AudioClip birdFly;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rd = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
    }

    private void OnMouseDown()
    {
        if (!canMove) {
            return;
        }
        AudioPlay(birdSelect);
        isClick = true;
        rd.isKinematic = true;
        Debug.Log("yes");
        canMove = false;
    }

    private void OnMouseUp()
    {
        isClick = false;
        rd.isKinematic = false;
        Invoke("Fly", 0.1f);
        // 禁用划线组件
        right.enabled = false;
        left.enabled = false;

    }

    private void Update()
    {
        if (isClick) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized; // 单位化向量
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }

            DrawLine();
        }

        // 相机跟随
        float posx = transform.position.x;
        Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position,
                        new Vector3(Mathf.Clamp(posx, 1.27f, 15), Camera.main.transform.position.y, Camera.main.transform.position.z),
                        smooth * Time.deltaTime);
    }
    void Fly() {
        myTrail.StartTrails();
        AudioPlay(birdFly);
        sp.enabled = false;
        Invoke("NextBird", 5.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myTrail.ClearTrails();
    }

    void DrawLine() // 每一帧都执行
    {
        right.enabled = true;
        left.enabled = true;
        right.sortingLayerName = "player";
        right.sortingOrder = 1;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.sortingLayerName = "player";
        left.sortingOrder = 1;
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    void NextBird()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(birdBoom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
