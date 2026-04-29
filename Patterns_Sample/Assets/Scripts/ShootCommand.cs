using UnityEngine;

public class ShootCommand : MonoBehaviour, ICommand
{
    #region Bullet

    [Header("Bullet")]
    [SerializeField]
    private Rigidbody bullet;

    [SerializeField]
    private float bulletSpeed = 3F;

    #endregion Bullet

    private Transform BulletSpawnPoint => Player.Instance.BulletSpawnPoint;

    private bool CanShoot => BulletSpawnPoint != null && bullet != null;

    public void Execute()
    {
        if (CanShoot)
        {
            Bullet bullet = Pool.Instance.GetBullet();
            bullet.transform.position = BulletSpawnPoint.position;
            bullet.transform.rotation = BulletSpawnPoint.rotation;
            bullet.Rigidbody.AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
        }
    }
}