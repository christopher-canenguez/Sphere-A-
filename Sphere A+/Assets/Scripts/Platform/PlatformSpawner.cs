using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Distance between last spawned platform and the target.
    float targetDistance = 50f;

    float gap = 10f;
    Transform target;
    Transform platformPrefab;
    int difficultyInterval;

    Vector3 lastSpawnLocation;
    int platformNum = 0;
    float timeScale = 1f;

    bool isRevived = false;
    float lastTimeScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnLocation = Vector3.forward * gap;
    } // End start.

    // Update is called once per frame
    void Update()
    {
        if (lastSpawnLocation.z - target.position.z < targetDistance)
        {
            if (platformNum >= 10)
            {
                CalculateDifficulty();
            } // End if.
            SpawnPlatform(platformPrefab, lastSpawnLocation);
        } // End if.
    } // End update.

    public void Revive()
    {
        isRevived = true;
    } // End revive.

    public void OnGameOver()
    {
        lastTimeScale = timeScale;
        timeScale = 1f;
        Time.timeScale = timeScale;
    } // End OnGameOver.

    void SpawnPlatform(Transform prefab, Vector3 position)
    {
        Transform newPlatform = Instantiate(prefab, transform);
        newPlatform.position = position;

        lastSpawnLocation += Vector3.forward * gap;
        platformNum++;
    } // End SpawnPlatform.

    void CalculateDifficulty()
    {
        if (Mathf.Repeat(platformNum, difficultyInterval) == 0)
        {
            timeScale += .01f;
            Time.timeScale = Mathf.Clamp(timeScale, 1f, 2f);
        } // End if.

        if (isRevived && timeScale < lastTimeScale)
        {
            timeScale += .05f;
            if (timeScale < lastTimeScale) return;
            timeScale = lastTimeScale;
        } // End if.
    } // End CalcDiff.

} // End class.
