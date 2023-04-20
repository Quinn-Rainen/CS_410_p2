using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public static ColorLerp Instance
    {
        get
        {
            return s_Instance;
        }
    }
    private static ColorLerp s_Instance;

    MeshRenderer sphereMeshRenderer;
    [SerializeField] [Range(0f,1f)] float lerpTime;

    [SerializeField] Color[] myColor;
    public float amplitude = 2.0f;
    public float frequency = 1.0f;
    public float speed = 1.0f;
    int colorIndex = 0;
    float t = 0f;
    int len;
    bool playerInRange = false;

    // Start position of the ball
    private Vector3 startPosition;

    // Speed of the ball movement
    [SerializeField] float moveSpeed = 5f;

    // Direction of the ball movement
    private int moveDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        sphereMeshRenderer = GetComponent<MeshRenderer>();
        len = myColor.Length;

        // Save the start position of the ball
        startPosition = transform.position;
        s_Instance = this;

        // Make sure the pickup collider is set to trigger
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        Vector3 newPosition = startPosition + new Vector3(x, 0, 0);
        transform.position = newPosition;

        if (playerInRange)
        {
            sphereMeshRenderer.material.color = Color.Lerp(sphereMeshRenderer.material.color, myColor[colorIndex], lerpTime*Time.deltaTime);

            t = Mathf.Lerp(t,1f, lerpTime*Time.deltaTime);
            if (t>.9f){
                t = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= len) ? 0 : colorIndex;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle pickup logic here
            Debug.Log("Picked up the sphere!");

            playerInRange = true;
        }

    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
