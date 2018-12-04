using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bat Wave Config")]
public class BatWaveConfig : ScriptableObject {

    [SerializeField] GameObject batPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;  // need to check if needed .
    [SerializeField] int numberOfBats = 1;
    [SerializeField] float moveSpeed = 0.3f;


    public GameObject GetBatPrefab() { return batPrefab; }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public int GetNumberOfbats() { return numberOfBats; }
    public float GetMoveSpeed() { return moveSpeed; }




}
