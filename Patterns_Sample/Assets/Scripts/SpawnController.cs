using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    private Vector3 spawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        if (TargetFactory.Instance != null)
        {
            InvokeRepeating("SpawnObject", firstSpawnDelay, spawnRate);

            if (Player.Instance != null)
            {
                Player.Instance.OnPlayerDied += StopSpawning;
            }
        }
    }

    private void SpawnObject()
    {
        GameObject spawnGO = TargetFactory.Instance.CreateInstance().gameObject;

        if (spawnGO != null)
        {
            spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));

            spawnGO.transform.position = spawnPoint;
            spawnGO.transform.rotation = Quaternion.identity;
        }
    }

    private void StopSpawning() => CancelInvoke();
}