using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField]
    [Tooltip("Number of enemies to spawn per wave")]
    private int _waveSize = 30;

    [SerializeField]
    [Tooltip("Time in seconds between spawning the first and last enemy of a wave")]
    [Range(0.1f,120f)]
    private float _waveDuration = 10f;

    // Whether or not the wave has started
    private bool _started = false;
    private bool _waveCleared = true;

    /// <summary>
    /// Try to begin a wave.
    /// Will fail if a wave is already in progress.
    /// </summary>
    /// <returns>True if wave was successfully started</returns>
    public bool StartWave()
    {
        if (!_started && _waveCleared)
            StartCoroutine(Wave());
        else
            return false;

        return true;
    }

    /// <summary>
    /// Once the wave has been started,
    /// Check to see when all the bloons are popped 
    /// </summary>
    void Update()
    {
        if (!_waveCleared && !_started)
        {
            if (transform.childCount <= 1)
            {
                _waveCleared = true;
                GameState state = GetComponent<GameState>();
                state.WaveCleared();
            }
        }
    }

    /// <summary>
    /// Controls the spawning of our current wave
    /// </summary>
    /// <returns></returns>
    public IEnumerator Wave()
    {
        _started = true;
        _waveCleared = false;

        int numSpawned = 0;

        while (numSpawned < _waveSize)
        {
            Spawn();

            ++numSpawned;

            yield return new WaitForSeconds(_waveDuration / _waveSize);
        }

        _started = false;
    }

    /// <summary>
    /// Spawns an enemy off-screen, to be moved to start of path by game update
    /// </summary>
    private void Spawn()
    {
        GameObject e = Instantiate(enemy, new Vector3(-50f, -50f, 0f), Quaternion.identity, transform);
        e.GetComponent<Enemy>().Spawn(0);
    }
}
