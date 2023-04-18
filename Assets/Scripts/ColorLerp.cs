using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    MeshRenderer sphereMeshRenderer;
    [SerializeField] [Range(0f,1f)] float lerpTime;

    [SerializeField] Color[] myColor;

    int colorIndex = 0;
    float t = 0f;
    int len;
    bool playerInRange = false;
    bool playerPressedE = false;

    // Start is called before the first frame update
    void Start()
    {
        sphereMeshRenderer = GetComponent<MeshRenderer>();
        len = myColor.Length;
    }

    // Update is called once per frame
void Update()
{
    //Debug.Log("E key pressed: " + Input.GetKeyDown(KeyCode.E));
    if (playerInRange)
    {
        sphereMeshRenderer.material.color = Color.Lerp(sphereMeshRenderer.material.color, myColor[colorIndex], lerpTime*Time.deltaTime);

        t = Mathf.Lerp(t,1f, lerpTime*Time.deltaTime);
        if (t>.9f){
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }

        if (colorIndex == 0 && t < 0.01f)
        {
            playerPressedE = false;
        }
    }
}

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Player left trigger");
            playerInRange = false;
        }
    }

}