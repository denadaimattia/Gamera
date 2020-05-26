using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour {

    #region PROPERTIES
    //Enabeld focus on target, when detect
    public bool focusOnTarget = false;
        
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
            
                if (gameraScript.isTargetVisible())
                {
                    GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    if (focusOnTarget)
                    {
                        updateFocusToTarget();
                    }
                }
        }
    }
    
    //Rotate the view of the object to the target
    //return void
    public void updateFocusToTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(gameraScript.target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
    }

    //Update the color of the object by the distance from the target
    //return void
    void updateColorByDistance()
    {
        float distance = (System.Math.Max(0, (gameraScript.maxDistance - gameraScript.getDistanceFromTarget())) / gameraScript.maxDistance);
        GetComponent<Renderer>().material.SetColor("_Color", new Color(distance, distance, distance));
    }

    #endregion
}
