using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{

    public float thrust;
    public float turningThrust;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    public AudioClip laserSound;


    public Rigidbody laserBoltPrefab;
    public Transform spawnPoint;

    public float laserBoltImpulse = 1000;

    // Start is called before the first frame update
    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            if (mouseX != 0)
            {
                //this is only executed if mouseX is anything other than 0
                rigidBody.AddRelativeTorque(0, mouseX * turningThrust * Time.deltaTime, 0);
            }

            if (mouseY != 0)
            {
                rigidBody.AddRelativeTorque(mouseY * turningThrust * Time.deltaTime, 0, 0);

            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(transform.forward * thrust * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(transform.forward * -thrust * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaserBolt();
        }


    }

    void FireLaserBolt()
    {
        Rigidbody newLaserBolt = Instantiate(laserBoltPrefab, spawnPoint.position, spawnPoint.rotation);

        newLaserBolt.velocity = rigidBody.velocity;
        newLaserBolt.AddForce(transform.forward * laserBoltImpulse);

        audioSource.PlayOneShot(laserSound);
    }

}
