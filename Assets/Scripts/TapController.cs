using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    public float tapForce = 200;
    public float tiltSmooth = 5;
    public static Vector3 startPos;

    Rigidbody2D _rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    public AudioSource playerDieMusic;
    public AudioSource pointMusic;
    private void Start()
    {
        startPos = this.transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0 ,0 ,-90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
    }
    private void Update()
    {
     if(Input.GetMouseButtonDown(0) && Time.timeScale!=0 && !GameManager.instance.GameOver())
        {
            transform.rotation = forwardRotation;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, Time.deltaTime * tiltSmooth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DeadZone")
        {
            GameManager.instance.GameOverFn();
            playerDieMusic.Play();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.ColliderArray.Add(collision.gameObject.GetComponent<BoxCollider2D>());
            this.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if(collision.gameObject.tag == "ScoreZone")
        {
            pointMusic.Play();
            GameManager.instance.score++;
            GameManager.instance.scoreText.text = "" + GameManager.instance.score;
            GameManager.instance.PipeGenerator();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.ColliderArray.Add(collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
