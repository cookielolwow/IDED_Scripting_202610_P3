using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour, IFactoryProduct
{
    public int tipoDeTarget = 0;

    private const float TIME_TO_DESTROY = 10F;

    [SerializeField]
    private int maxHP = 1;

    private int currentHP;

    [SerializeField]
    private int scoreAdd = 10;

    public delegate void OnTargetDestroyed(int scoreAdd);

    public static event OnTargetDestroyed onTargetDestroyed;

    private void OnEnable()
    {
        currentHP = maxHP;
        Invoke("DestroyTarget", TIME_TO_DESTROY);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void DestroyTarget()
    {
        TargetFacade.Instance.ReturnTarget(this, tipoDeTarget);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int collidedObjectLayer = collision.gameObject.layer;

        if (collidedObjectLayer.Equals(Utils.BulletLayer))
        {
            Pool.Instance.ReturnBullet(collision.gameObject.GetComponent<Bullet>());

            currentHP -= 1;

            if (currentHP <= 0)
            {
                onTargetDestroyed?.Invoke(scoreAdd);
                TargetFacade.Instance.ReturnTarget(this, tipoDeTarget);
            }
        }
        else if (collidedObjectLayer.Equals(Utils.PlayerLayer) ||
            collidedObjectLayer.Equals(Utils.KillVolumeLayer))
        {
            Player.Instance.OnPlayerHit?.Invoke();
            TargetFacade.Instance.ReturnTarget(this, tipoDeTarget);
        }
    }
}