using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Pool instance;

    public Bullet bulletBase;

    [SerializeField]
    private int poolSize = 10;

    private List<Bullet> bullets = new List<Bullet>();

    public static Pool Instance => instance;

    public Bullet GetBullet()
    {
        if (bullets.Count == 0)
        {
            AddBulletToPool();
        }

        Bullet bullet = bullets[0];
        bullets.RemoveAt(0);
        bullet.ResetBullet(true);
        bullet.transform.SetParent(null);
        return bullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = Vector3.zero;
        bullet.ResetBullet(active: false);
        bullets.Add(bullet);
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddBulletToPool();
        }
    }

    private void AddBulletToPool()
    {
        Bullet bullet = BulletFactory.Instance.CreateInstance();
        bullet.transform.SetParent(transform);
        ReturnBullet(bullet);
    }
}