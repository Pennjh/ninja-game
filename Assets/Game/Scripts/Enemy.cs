using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private GameObject explosion;
    private UIManager UI; //empty variable
    [SerializeField] AudioClip clip;
    void Start()
    {
        //link to UI
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            float xPos = Random.Range(-6, +6);
            transform.position = new Vector3(xPos, 7.0f, 0); //jump to random position
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            AudioSource.PlayClipAtPoint(clip, GameObject.Find("Main Camera").transform.position, 1f);
            Destroy(collision.transform.gameObject); //blow up the laser
            Instantiate(explosion, transform.position, Quaternion.identity);
            UI.UpdateScore();
            Destroy(this.gameObject); //blow up the enemy
        }
        else if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, GameObject.Find("Main Camera").transform.position, 1f);
            Player P = collision.GetComponent<Player>();
            //hook to the player code
            if (P != null)
            {
                P.Damage();
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            UI.UpdateScore();
            Destroy(this.gameObject);
        }
    }
}








