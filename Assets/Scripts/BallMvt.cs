using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMvt : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gyroEnabled;
    private Gyroscope gyroscope;
    public float speed;
    private Rigidbody rb;
    private bool objectIsMoving = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gyroEnabled = EnableGyro();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acc= Input.acceleration;
        Vector3 position = rb.transform.position;

        if (rb.transform.position.z > 10 || rb.transform.position.y < -10)
        {
            objectIsMoving = false;
        }
            if (acc.z > .6 && !objectIsMoving)
        {
            objectIsMoving = true;
            rb.AddForce(transform.forward * speed * 2700 * Time.deltaTime);
            SoundManager.playBallSound();
        }
        //Debug.Log(position.x);
        position.x = Mathf.Clamp(position.x, -1f, 1f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pin")
        {
            Handheld.Vibrate();
            SoundManager.playStrikeSound();
            objectIsMoving = false;
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            return true;
        }
        return false;
    }
}
