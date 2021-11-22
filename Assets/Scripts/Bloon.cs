using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bloon : MonoBehaviour
{
    private struct Tier
    {
        public Tier(float t_speed, Color t_color)
        {
            speed = t_speed;
            color = t_color;
        }

        public float speed;
        public Color color;
    }

    private static Dictionary<int, Tier> _bloonTiers = new Dictionary<int, Tier>()
    {
        { 0, new Tier(1.0f, Color.red) },
        { 1, new Tier(1.4f, new Color(0.4f,0.4f,1.0f)) },
        { 2, new Tier(1.8f, Color.green) },
        { 3, new Tier(3.2f, Color.yellow) },
        { 4, new Tier(3.5f, new Color(1.0f,0.5f,0.75f)) }
    };

    [Tooltip("Sets the tier of the bloon: MIN 0, MAX 4")]
    [Range(0,4)]
    public int _currentTier;
    private Rigidbody2D _rb;
    private SpriteRenderer _spr;

    // Unit vector representing our velocity
    private Vector2 _velocity = new Vector2 { x = 0.5f, y = 0.0f }; // TODO: Change this; set to 0.5,0 for testing.

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spr = GetComponent<SpriteRenderer>();

        Spawn(_currentTier);
    }

    ////////////////////////////////////////////////////////////

    public void Spawn(int t_tier)
    {
        SetTier(t_tier);
    }

    ////////////////////////////////////////////////////////////

    private void SetTier(int t_tier)
    {
        _currentTier = t_tier;
        _spr.color = _bloonTiers[_currentTier].color;
    }

    ////////////////////////////////////////////////////////////

    private void FixedUpdate()
    {
        float speed = _bloonTiers[_currentTier].speed;
        _rb.velocity = _velocity * speed;

        Debug.Log(_rb.velocity);
    }

    ////////////////////////////////////////////////////////////

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            Hit();
        }
    }

    ////////////////////////////////////////////////////////////
    
    private void Hit()
    {
        // TODO: Make call to audio manager here

        if (--_currentTier >= 0)
            SetTier(_currentTier);
        else
            Pop();
    }

    ////////////////////////////////////////////////////////////

    private void Pop()
    {
        // TODO: Bloon Destroyed
        Destroy(gameObject);
    }
}
