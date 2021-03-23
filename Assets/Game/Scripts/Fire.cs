using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 10.0f;
    //instance field
    float horizontalt = 0;
    bool right; //empty variable in hopes to flip firing

    void Start()
    {
        horizontalt = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalt < 0)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    
        else
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > 13.0f || transform.position.x < -13.0f)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(this.gameObject); //deletes fire clone
        }
    }
}
