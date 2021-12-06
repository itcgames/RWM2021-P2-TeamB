using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection : MonoBehaviour
{
    private float _range;
    public delegate void ObjectDetected();
    public event ObjectDetected OnObjectDetected;
    public List<GameObject> targets;
    
    void Awake()
    {
        targets = new List<GameObject>();
        CircleCollider2D collider = this.gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true; 
    }

    public void setRange(float t_range)
    {
        this._range = t_range;
        CircleCollider2D collider = this.gameObject.GetComponent<CircleCollider2D>();
        collider.radius = this._range;
    }

    void Update()
    {
        if (targets.Count > 0)
            OnObjectDetected();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bloon")
            targets.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "bloon")
            targets.Remove(collision.gameObject);
    }
}
