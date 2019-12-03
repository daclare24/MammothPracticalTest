using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public float movementRadius = 5.0f;
    public float moveSpeed = 60.0f;
    public Color hitColour;
    public bool Clockwise;

    private Vector3 origin;
    private int direction;
    private Color originalColour;
    private float hitTime = 2.0f;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!Clockwise)
            direction = 1;
        else
            direction = -1;

        origin = transform.position;
        originalColour = GetComponent<Renderer>().material.color;

        transform.position = new Vector3(transform.position.x,movementRadius,transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(origin, new Vector3(0.0f, 0.0f, 1.0f * direction), moveSpeed * Time.deltaTime);
        if (transform.position.y < origin.y)
        {
            direction *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!hit)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            hit = true;
            StartCoroutine(revertColour());
        }
    }

    private IEnumerator revertColour()
    {
        yield return new WaitForSeconds(hitTime);
        GetComponent<Renderer>().material.SetColor("_Color", originalColour);
        hit = false;
    }
}
