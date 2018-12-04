using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour {

    [SerializeField] List<BatWaveConfig> batWaveConfigs; // how many bat waves we have
    int startingBatWave = 0;
    [SerializeField] bool looping = true;  // loop bat waves

    
    // Use this for initialization
	IEnumerator Start ()
    {
        do
        {
            yield return StartCoroutine(SpawnAllBatWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllBatWaves()
    {
        for (int waveIndex = startingBatWave;waveIndex < batWaveConfigs.Count; waveIndex++)
        {
            var currentBatWave = batWaveConfigs[waveIndex];
            yield return StartCoroutine(SpawnBatWave(currentBatWave));
        }
        
        
    }
	
	private IEnumerator SpawnBatWave(BatWaveConfig batWaveConfig)
    {
        var newBat = Instantiate(batWaveConfig.GetBatPrefab(), batWaveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
        newBat.GetComponent<BatPathing>().SetWaveConfig(batWaveConfig);
        yield return new WaitForSeconds(batWaveConfig.GetTimeBetweenSpawns());
    }
}
