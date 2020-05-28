using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour {

    #region PROPERTIES
    //Enabeld focus on target, when detect
    public bool focusOnTarget = false;

    //max distance
    public float maxDistance = 10;

    private GameraScript gameraScript = null;

    #endregion


    #region FUNCTIONS
    // Use this for initialization
    void Start () {
        gameraScript = GetComponent<GameraScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameraScript)
        {
            updateColorByDistance();

            foreach (var obj in gameraScript.getTargetsinfo())
            {
                if (obj.Value)
                {
                    if (focusOnTarget)
                    {
                        updateFocusToTarget(obj.Key.transform);
                    }
                    obj.Key.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }

            }
        }
    }
    
    //Rotate the view of the object to the target
    //return void
    public void updateFocusToTarget(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
    }

    //Update the color of the object by the distance from the target
    //return void
    void updateColorByDistance()
    {
        foreach (var obj in gameraScript.getTargetDistanceInfo())
        {
            float distance = (System.Math.Max(0, (maxDistance - obj.Value)) / maxDistance);
            obj.Key.GetComponent<Renderer>().material.SetColor("_Color", new Color(distance, distance, distance));
        }
    }

    #endregion
}
