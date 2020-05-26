using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameraScript : MonoBehaviour {

    #region PROPERTIES
    //Tag to define the targets
    public string targetTag = null;

    //Single target
    public Transform target;

    //Max distance to detect using Raycast
    public float maxDistance = 10;

    //FOV angle
    public float viewAngle = 120;

    //Number of ray in FOV angle
    public int rayNumber = 25;

    //Enabled raycast tecnique
    public bool enableRayCast = false;

    //Enabled debug visualizzation
    public bool enableDebug = true;
    
    private float angleRay = 0;
    private float halfAngle = 60;
    #endregion

    #region FUCNTIONS
    // Use this for initialization
    void Start ()
    {
        angleRay = viewAngle / rayNumber;
        halfAngle = viewAngle / 2.0f;
    }


    //Check if the target is visible by the object
    //return bool True if the target is visible 
    public bool isTargetVisible()
    {
        if(enableRayCast)
        {
            return isTargetVisibleByRayCast();
        }
        else
        {
            return isTargetVisibleOnFOV();
        }
    }

    //Get distance from target
    //return float -> the distance from the target
    public float getDistanceFromTarget()
    {
        if (target != null)
        {
            return Vector3.Distance(transform.position, target.position);
        }

        return -1;
    }

    #region PRIVATE FUNCTION

    //Check if the target is visible by the object using RayCast
    //return bool True if the target is visible 
    private bool isTargetVisibleByRayCast()
    {
        bool result = false;
        RaycastHit hitInfo;
        for (int i = 0; i <= rayNumber; ++i)
        {
            Vector3 ray = Quaternion.AngleAxis(-halfAngle + (angleRay * i), transform.up) * transform.forward;
            Physics.Raycast(transform.position, ray, out hitInfo, maxDistance);
            if (!result)
            {
                if (String.Empty == targetTag)
                {
                    result = (hitInfo.rigidbody != null && hitInfo.transform == target);
                }
                else
                {
                    result = (hitInfo.rigidbody != null && hitInfo.transform.gameObject.tag == targetTag);
                }
            }

            if (enableDebug)
                drawDebugRay(i, hitInfo);
        }

        return result;
    }

    //Check if the target is visible by the object
    //return bool True if the target is visible 
    private bool isTargetVisibleOnFOV()
    {
        GameObject[] targets = null;
        if (target == null)
        {
            targets = GameObject.FindGameObjectsWithTag(targetTag);

            for (int i = 0; i < targets.Length; ++i)
            {
                var heading = targets[i].transform.position - transform.position;
                heading.Normalize();
                float dot = Vector3.Dot(heading, transform.forward);
                if (dot > System.Math.Cos(halfAngle))
                {
                    return true;
                }
            }
        }
        else
        {
            var heading = target.position - transform.position;
            heading.Normalize();
            float dot = Vector3.Dot(heading, transform.forward);
            if (dot > System.Math.Cos(viewAngle))
            {
                return true;
            }
        }
        return false;

    }

    #endregion

    #endregion

    #region DEBUG
    //Draw the rays 
    //return void
    void drawDebugRay(int rayNum, RaycastHit hitInfo)
    {
        bool hit = false;
        if (String.Empty == targetTag)
        {
            hit = hitInfo.rigidbody != null && hitInfo.transform == target;
        }
        else
        {
            hit = hitInfo.rigidbody != null  && hitInfo.transform.gameObject.tag == targetTag;

        }
        Vector3 ray = Quaternion.AngleAxis(-halfAngle + (angleRay * rayNum), transform.up) * transform.forward;
        Debug.DrawLine(transform.position, transform.position + Vector3.Scale(ray, new Vector3(maxDistance, 0, maxDistance)), (hit) ? new Color(1, 0, 0, 0.5f) : new Color(0, 1, 1, 0.5f));
    }
    #endregion
}
