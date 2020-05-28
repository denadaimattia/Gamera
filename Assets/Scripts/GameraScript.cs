using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameraScript : MonoBehaviour {

    #region PROPERTIES
    //Tag to define the targets
    public string targetTag = null;

    //FOV angle
    public float viewAngle = 120;
    
    //Enable 3 dimensions check
    public bool enable3DCheck = true;

    //Enabled debug visualizzation
    public bool enableDebug = true;

    //Bound size correction
    public float boundCorrection = 2.0f;

    private GameObject[] targets;
    private double cosineAngle = 0;
    #endregion

    #region FUCNTIONS
    // Use this for initialization
    void Start ()
    {
        cosineAngle = System.Math.Cos(viewAngle / 2.0f * Mathf.Deg2Rad);
        
        targets = GameObject.FindGameObjectsWithTag(targetTag);
    }

    //Check if the target is visible by the object
    //return List<Transform> List of targets visible 
    public List<KeyValuePair<GameObject, bool>> getTargetsinfo()
    {
        List<KeyValuePair<GameObject, bool>> targetVisible = new List<KeyValuePair<GameObject, bool>>();
        for (int i = 0; i < targets.Length; ++i)
        {
            var heading = targets[i].transform.position - transform.position;
            heading.Normalize();
            float dot = Vector3.Dot(heading, transform.forward);
            if (dot > cosineAngle)
            {
                RaycastHit hitInfo;
                if (enableDebug)
                {
                    Debug.DrawLine(transform.position, targets[i].transform.position);
                }
                if (Physics.Linecast(transform.position, targets[i].transform.position, out hitInfo))
                {
                    if (hitInfo.transform != null && hitInfo.transform != targets[i].transform)
                    {
                        DetailedCheck(targets[i], out hitInfo);
                    }
                    targetVisible.Add(new KeyValuePair<GameObject, bool>(targets[i], (hitInfo.transform == null || hitInfo.transform == targets[i].transform)));
                }
            }
        }

        return targetVisible;
    }

    //Get distance from target
    //return float -> the distance from the target
    public List<KeyValuePair<GameObject, float>> getTargetDistanceInfo()
    {
        List<KeyValuePair<GameObject, float>> targetDistance = new List<KeyValuePair<GameObject, float>>();
        foreach (var obj in targets)
        
            {
            targetDistance.Add(new KeyValuePair<GameObject, float>(obj, Vector3.Distance(transform.position, obj.transform.position)));
        }

        return targetDistance;
    }

    #endregion

    #region PRIVATE_FUNCTION
    private void DetailedCheck(GameObject target, out RaycastHit hitInfo)
    {
        Vector3 left = target.transform.position - (transform.right * target.GetComponent<Renderer>().bounds.size.x* boundCorrection);

        if (enableDebug)
        {
            Debug.DrawLine(transform.position, left);
        }

        Physics.Linecast(transform.position, left, out hitInfo);
        if (hitInfo.transform != null && hitInfo.transform != target.transform)
        {
            Vector3 right = target.transform.position + (transform.right * target.GetComponent<Renderer>().bounds.size.x * boundCorrection);

            if (enableDebug)
            {
                Debug.DrawLine(transform.position, right);
            }
            Physics.Linecast(transform.position, right, out hitInfo);
            if (enable3DCheck)
            {
                if (hitInfo.transform != null && hitInfo.transform != target.transform)
                {
                    Vector3 top = target.transform.position - (transform.up * target.GetComponent<Renderer>().bounds.size.y * boundCorrection);

                    if (enableDebug)
                    {
                        Debug.DrawLine(transform.position, top);
                    }

                    Physics.Linecast(transform.position, top, out hitInfo);
                    if (hitInfo.transform != null && hitInfo.transform != target.transform)
                    {
                        Vector3 bottom = target.transform.position + (transform.up * target.GetComponent<Renderer>().bounds.size.y * boundCorrection);

                        if (enableDebug)
                        {
                            Debug.DrawLine(transform.position, bottom);
                        }
                        Physics.Linecast(transform.position, bottom, out hitInfo);
                    }
                }
            }
        }
    }

    private float getDistanceInfo(GameObject _obj)
    {
          return Vector3.Distance(transform.position, _obj.transform.position);
    }
    #endregion

}
