using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    // to reference the animator
    // Start is called before the first frame update
    [SerializeField] private int speed = 5;
    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float fireRate = 0.25f; //slow down
    [SerializeField] private float canFire = 0.05f; //time passed
    public bool facingRight; //empty variable
    [SerializeField] public bool canTripleShot = false;
    [SerializeField] public bool canSpeedBoost = false;
    [SerializeField] public bool canShield = false;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject shield;
    private GameManager GM;
    private SpawnManager SM;
    private AudioSource audioSource;
    private UIManager UI;
    [SerializeField] private GameObject dust;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (UI != null)
        {
            UI.UpdateLives(lives);
        }

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        facingRight = true;

        if (SM != null)
        {
            SM.StartSpawnRoutines();
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Movement(horizontalInput);
        shoot();
        Flip(horizontalInput);
    }
    private void Movement(float horizontalInput)
    {

        float horizontalt = Input.GetAxis("Horizontal") * speed;
        //left to right
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        //horizontal movement
        //float verticalInput = Input.GetAxis("Vertical");
        if (horizontalt != 0)
            dust.SetActive(true);
        else
            dust.SetActive(false);
        //transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        //vertical movement
        //top bound
        //if (transform.position.y > -1.44f)
        {
            //transform.position = new Vector3(transform.position.x, -1.44f, 0);
        }
        //bottom bound 
        //if (transform.position.y < -1.44f)
        {
            //transform.position = new Vector3(transform.position.x, -1.44f, 0);
        }
        //left bound
        if (transform.position.x < -12.45f)
        {
            transform.position = new Vector3(-12.45f, transform.position.y, 0);
        }
        //right bound
        if (transform.position.x > 12.45f)
        {
            transform.position = new Vector3(12.45f, transform.position.y, 0);
        }

        //animator
        animator.SetFloat("Speed", Mathf.Abs(horizontalt));

    }
    private void shoot()
    {
        if (Time.time > canFire)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                audioSource.Play();
                //plays the laser sound
                if (canTripleShot)
                    Instantiate(tripleShotPrefab, transform.position+ new Vector3 (0,2.3f,0), Quaternion.identity);
                else
                    Instantiate(firePrefab, transform.position, Quaternion.identity);
                //clones the fireball
                //at the player position
                //at the original rotation
                canFire = Time.time + fireRate; //update to next shot deadline
            }
        }
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        //count for 5 secs
        //separate from the game run
        canTripleShot = false;
    }
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
        speed = 5;
    }
    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedBoostPowerUpOn()
    {
        canSpeedBoost = true;
        speed = 8;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public void ShieldPowerUpOn()
    {
        canShield = true;
        shield.SetActive(true); //turn on the shield
    }
    public void Damage()
    {
        if (canShield) //canShield = true
        {
            canShield = false;
            shield.SetActive(false); //turn off the shield
        }
        else
        {
            if (lives > 1)
            {
                lives = lives - 1;
                UI.UpdateLives(lives);
            }
            else
            {
                lives = lives - 1;
                UI.UpdateLives(lives);
                GM.gameOver = true;
                UI.ShowTitleScreen();
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject); //remove ship
            }
        }
    }
    private void Flip(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
} //end class
