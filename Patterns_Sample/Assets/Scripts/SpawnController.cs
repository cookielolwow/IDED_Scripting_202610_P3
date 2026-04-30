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

    // Hola proe esto es un apunte super experimental para poder explicar y reconocer los cambios de lo que estamos haciendo chajaja

    // SpawnObject antes de la implementación nueva de super TargetFacade,
    // ahora se obtiene el Target directamente desde la fokin fachada en lugar de crear una instancia a través de la fábrica.

    // Antes:
    /*private void SpawnObject()
    {
        GameObject spawnGO = TargetFactory.Instance.CreateInstance().gameObject;

        if (spawnGO != null)
        {
            spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));

            spawnGO.transform.position = spawnPoint;
            spawnGO.transform.rotation = Quaternion.identity;
        }
    }*/

    // Después chajaja:
    private void SpawnObject()
    {

        int tipoAleatorio = Random.Range(0, 3);

        Target spawnTarget = TargetFacade.Instance.GetTarget(tipoAleatorio);

        if (spawnTarget != null)
        {
            spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));

            spawnTarget.transform.position = spawnPoint;
            spawnTarget.transform.rotation = Quaternion.identity;
        }
    }

    private void StopSpawning() => CancelInvoke();
}