using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class SpawnDebugGizmos : MonoBehaviour
{
    Transform[] children;
    // Start is called before the first frame update
    void Start()
    {
        children = transform.GetComponentsInChildren<Transform>();
    }

    private void OnDrawGizmos()
    {
        foreach(var tran in children)
        {
            if (tran != transform)
                Gizmos.DrawSphere(tran.position, 0.5f);
        }
    }
}
