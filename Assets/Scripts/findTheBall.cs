using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findTheBall : MonoBehaviour
{
    public float detectionRadius = 5.0f;
    public float detectionAngle = 90.0f;
    private bool ballFound = false;

    private void Update()
    {
        LookForBall();
    }

    private ColorLerp LookForBall()
    {
        if (ColorLerp.Instance == null)
        {
            return null;
        }

        Vector3 playerPosition = transform.position;
        Vector3 toBall = ColorLerp.Instance.transform.position - playerPosition;
        toBall.y = 0;

        if (toBall.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toBall.normalized, transform.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad) & ballFound == false) 
            {
                Debug.Log("Ball has been found!");
                ballFound = true;
                return ColorLerp.Instance;
            }
        }


        return null;
    }


    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 0, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);

    }
#endif
}
