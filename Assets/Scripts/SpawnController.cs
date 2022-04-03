using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField]
    [Tooltip("Number of enemies to spawn first wave")]
    private int _startingWaveSize = 30;

    [SerializeField]
    [Tooltip("By what factor should our waves increase in size by each wave")]
    [Range(1.0f, 2.0f)]
    private float _waveIncrement = 1.5f;

    [SerializeField]
    [Tooltip("Time in seconds between spawning the first and last enemy of a wave")]
    [Range(0.1f,120f)]
    private float _waveDuration = 10f;

    // Whether or not the wave has started
    private bool _started = false;
    private bool _waveCleared = true;

    // What wave are we currently on
    private int _wave = 0;

    // How many types of enemies do we have?
    private int _numEnemyTypes = 5;


    /// <summary>
    /// Try to begin a wave.
    /// Will fail if a wave is already in progress.
    /// </summary>
    /// <returns>True if wave was successfully started</returns>
    public bool StartWave()
    {
        if (!_started && _waveCleared)
        {
            StartCoroutine(Wave());
            _wave++;
        }
        else
        {
            return false;
        }

        return true;
    }


    /// <summary>
    /// Once the wave has been started,
    /// Check to see when all the bloons are popped 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_started)
            StartWave();

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

        // Total size of this wave
        int waveSize = (int)(_startingWaveSize * Mathf.Pow(_waveIncrement, (float)_wave));

        // Distribution of enemy types
        List<int> distribution = determineStrength(waveSize);

        // For each enemy type in our distribution
        for (int i = 0; i < distribution.Count; ++i)
        {
            // If we've run out of enemies to spawn, break
            if (distribution[i] <= 0) break;

            // Spawn each enemy of this denomination
            while (distribution[i] >= 0)
            {
                Spawn(i);
                --distribution[i];
                yield return new WaitForSeconds(_waveDuration / waveSize);
            }

            // 1 second pause between enemy types
            yield return new WaitForSeconds(1f);
        }

        _started = false;
    }


    /// <summary>
    /// Given a total strength for a wave, this function will determine the distribution
    /// of enemy types, and return this as a list of size N where N is the number of enemy types,
    /// and N[0] -> N[i] represents the number of enemies of strength i to spawn.
    /// </summary>
    /// <param name="t_totalStrength">The total strength of enemies that should be spawned</param>
    /// <returns>A list containing the number of each enemy type to spawn</returns>
    private List<int> determineStrength(int t_totalStrength)
    {
        List<int> distribution = new List<int>();

        // Allocate half of the remaining strength to this tier and continue
        for (int i = 0; i < _numEnemyTypes && t_totalStrength > i; ++i)
        {
            t_totalStrength = t_totalStrength >> 1;
            distribution.Add(t_totalStrength / (i+1));
        }

        return distribution;
    }


    /// <summary>
    /// Spawns an enemy off-screen, to be moved to start of path by game update
    /// </summary>
    private void Spawn(int t_strength)
    {
        GameObject e = Instantiate(enemy, new Vector3(-50f, -50f, 0f), Quaternion.identity, transform);
        e.GetComponent<Enemy>().Spawn(t_strength);
    }
}
