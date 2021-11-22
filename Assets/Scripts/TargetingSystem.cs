using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Vector2 getVelocity(Vector3 t_from, Vector3 t_to)
    {
        Vector2 v = new Vector2(t_from.x- t_to.x, t_from.y - t_to.y);
        return v.normalized;  
    }
}
