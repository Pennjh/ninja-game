using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int PowerUpId;
    [SerializeField] AudioClip clip;
    //0 triple
    //1 speed
    //2 shield
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject); //remove once it leaves the screen
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //parameter, variable sent into the method
        Debug.Log(other);
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, GameObject.Find("Main Camera").transform.position, 1f);

            //script communication
            Player P = other.GetComponent<Player>();
            // reach ou to the other item we collided with
            // get a link to the script called player
            // save link in a variable called P
            if (P != null) //if it exists
            {
                //P.canTripleShot = true; //turn on tripleshot
                //StartCoroutine(P.TripleShotPowerDownRoutine());
                //composite function call
                //calls the routine in a separate thread from our game
                //so the time doesnt pause the whole game
                if (PowerUpId == 0)
                {
                    P.TripleShotPowerUpOn();
                } //has the player start the timer
                else if (PowerUpId == 1)
                {
                    P.SpeedBoostPowerUpOn();
                    //speed boost
                }
                else if (PowerUpId == 2)
                {
                    P.ShieldPowerUpOn(); //shield on
                    //shield on
                }

            }
            Destroy(this.gameObject); //destroy the powerup
            //destroys the coroutine as well
        }//end of tag
    }
}








