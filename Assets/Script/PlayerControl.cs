using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerControl : MonoBehaviour
{
    
    public float speed = 5f;
    public GameObject powerRings;
    private Rigidbody plyerRb;
    private GameObject focalPoint;
    private float powerUpStrength=15.0f;
    public bool hasPowerUp = false;
    // Start is called before the first frame update
    void Start()
    {

        plyerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()

    {
        powerRings.transform.position = transform.position + new Vector3(0, -0.65f, 0);
        float fowardInput = Input.GetAxis("Vertical");
        plyerRb.AddForce(focalPoint.transform.forward * speed * fowardInput);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCount());
            powerRings.SetActive(true);

        }
    }
    IEnumerator PowerUpCount()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerRings.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb=collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
}
