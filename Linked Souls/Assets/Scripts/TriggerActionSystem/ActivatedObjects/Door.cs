using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivatedObjectMaster
{
    public GameObject target;
    protected GameObject posOrigin;
    public GameObject posEnd;
    public float activespeed;
    public float inactivespeed;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        posOrigin = new GameObject();
        posOrigin.transform.position = target.transform.position;
        posOrigin.transform.rotation = target.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (target.transform.position != posEnd.transform.position)
                target.transform.position = Vector3.Lerp(target.transform.position, posEnd.transform.position,
                    (activespeed / Vector2.Distance(target.transform.position, posEnd.transform.position)) * Time.deltaTime);
        }
        else
        {
            if (target.transform.position != posOrigin.transform.position)
                target.transform.position = Vector3.Lerp(target.transform.position, posOrigin.transform.position, activespeed * Time.deltaTime);
        }
    }

    public override void setActive(bool isActive)
    {
        Debug.Log("setting door active");
        active = isActive;
    }
}
