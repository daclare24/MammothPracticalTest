using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float cameraSensitivity = 2.0f;
    public float moveSensitivity = 0.5f;
    public float ballSpeed = 10.0f;
    public float fireRate = 0.3f;
    public GameObject ball;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool armed = true;

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical")) * moveSensitivity;
        transform.Translate(move, Space.Self);

        yaw += cameraSensitivity * Input.GetAxis("Mouse X");
        pitch -= cameraSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetButton("Cancel"))
            Application.Quit();
    }

    private void Update()
    {
        if (armed)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire2"))
            {
                Fire();
                armed = false;
                StartCoroutine(rearm());
            }
        }
    }

    private void Fire()
    {
        GameObject newBall = Instantiate(ball);
        newBall.transform.position = transform.position + transform.forward * 1.5f + transform.up * -1.0f;
        newBall.GetComponent<Rigidbody>().velocity = transform.forward * ballSpeed;
    }

    private IEnumerator rearm()
    {
        yield return new WaitForSeconds(fireRate);
        armed = true;
    }

}