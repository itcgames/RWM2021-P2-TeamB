using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection : MonoBehaviour
{
    private float _range;
    public List<GameObject> targets;
    
    void Awake()
    {
        targets = new List<GameObject>();
        CircleCollider2D collider = this.gameObject.AddComponent<CircleCollider2D>();
        collider.radius = 1;
        collider.isTrigger = true; 
    }

    public void setRange(float t_range)
    {
        this._range = t_range;
        transform.localScale = new Vector2(_range, _range);
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
