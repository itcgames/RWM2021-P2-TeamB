using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static Dictionary<int, Tier> m_bloonTiers = new Dictionary<int, Tier>()
    {
        { 0, new Tier(1.0f, Color.red) },
        { 1, new Tier(1.4f, Color.blue) },
        { 2, new Tier(1.8f, Color.green) },
        { 3, new Tier(3.2f, Color.yellow) },
        { 4, new Tier(3.5f, new Color(1.0f,0.5f,0.5f)) }
    };

    private int m_currentTier;

    ////////////////////////////////////////////////////////////

    public void Spawn(int t_tier)
    {
        SetTier(t_tier);
    }

    ////////////////////////////////////////////////////////////

    private void SetTier(int t_tier)
    {
        m_currentTier = t_tier;
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

        if (--m_currentTier >= 0)
            SetTier(m_currentTier);
        else
            Pop();
    }

    ////////////////////////////////////////////////////////////

    private void Pop()
    {
        // TODO: Bloon Destroyed
    }
}
