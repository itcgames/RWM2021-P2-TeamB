using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection : MonoBehaviour
{
    private float _range;
    public delegate void ObjectDetected(GameObject obj);
    public event ObjectDetected OnObjectDetected;
    
    void Awake()
    {
        CircleCollider2D collider = this.gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true; 
    }

    public void setRange(float t_range)
    {
        this._range = t_range;
        CircleCollider2D collider = this.gameObject.GetComponent<CircleCollider2D>();
        collider.radius = this._range;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bloon"))
        {
            OnObjectDetected(collision.gameObject);
        }
    }
}
