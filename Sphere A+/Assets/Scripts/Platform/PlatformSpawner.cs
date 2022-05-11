using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Distance between last spawned platform and the target.
    float _targetDistance = 50f;

    [SerializeField] float gap = 8f;
    [SerializeField] Transform _target;
    [SerializeField] Transform _platformPrefab;
    [SerializeField] int _difficultyInterval;

    Vector3 _lastSpawnLocation;
    int _platformNum = 0;
    float _timeScale = 1f;

    bool _isRevived = false;
    float _lastTimeScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _lastSpawnLocation = Vector3.forward * gap;
    } // End start.

    // Update is called once per frame
    void Update()
    {
        if (_lastSpawnLocation.z - _target.position.z < _targetDistance)
        {
            if (_platformNum >= 10)
            {
                CalculateDifficulty();
            } // End if.
            SpawnPlatform(_platformPrefab, _lastSpawnLocation);
        } // End if.
    } // End update.

    public void Revive()
    {
        _isRevived = true;
    } // End revive.

    public void OnGameOver()
    {
        _lastTimeScale = _timeScale;
        _timeScale = 1f;
        Time.timeScale = _timeScale;
    } // End OnGameOver.

    void SpawnPlatform(Transform prefab, Vector3 position)
    {
        Transform newPlatform = Instantiate(prefab, transform);
        newPlatform.position = position;

        _lastSpawnLocation += Vector3.forward * gap;
        _platformNum++;
    } // End SpawnPlatform.

    void CalculateDifficulty()
    {
        if (Mathf.Repeat(_platformNum, _difficultyInterval) == 0)
        {
            _timeScale += .01f;
            Time.timeScale = Mathf.Clamp(_timeScale, 1f, 2f);
        } // End if.

        if (_isRevived && _timeScale < _lastTimeScale)
        {
            _timeScale += .05f;
            if (_timeScale < _lastTimeScale) return;
            _timeScale = _lastTimeScale;
        } // End if.
    } // End CalcDiff.
} // End class.
