using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IFactoryProduct
{
    private new Rigidbody rigidbody;

    public Rigidbody Rigidbody => rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetBullet(bool active = false)
    {
        if (active)
        {
            Invoke("ReturnToPool", 5F);
        }
        else
        {
            CancelInvoke("ReturnToPool");
        }

        rigidbody.linearVelocity = Vector3.zero;
        gameObject.SetActive(active);
    }

    private void ReturnToPool()
    {
        print("Returning bullet to pool");
        Pool.Instance.ReturnBullet(this);
    }
}