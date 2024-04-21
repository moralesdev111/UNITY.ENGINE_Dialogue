using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnOnScene : MonoBehaviour
{
    [SerializeField] List<Spawner> spawnEntries = new List<Spawner>();
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        PositionPlayerSpawn();
    }

    private void PositionPlayerSpawn()
    {
        for (int i = 0; i < spawnEntries.Count; i++)
        {
            if(DataManager.Instance.previousSceneName == spawnEntries[i].PreviousSceneName) //if scenes match perform logic
            {
                player.position = spawnEntries[i].SpawnPosition;
                player.rotation = Quaternion.LookRotation(spawnEntries[i].SpawnDirection);
            }
        }
    }
}

[System.Serializable]
public class Spawner
{
    [SerializeField] string previousSceneName;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 spawnDirection;

    public string PreviousSceneName { get { return previousSceneName; } }
    public Vector3 SpawnPosition { get { return spawnPosition; } }
    public Vector3 SpawnDirection { get {  return spawnDirection; } }

}
