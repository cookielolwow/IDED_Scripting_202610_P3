using UnityEngine;

public abstract class FactoryBase<T> : MonoBehaviour
    where T : IFactoryProduct
{
    private static FactoryBase<T> instance;

    public static FactoryBase<T> Instance => instance;

    [SerializeField]
    protected T[] spawnObjects;

    protected void Awake()
    {
        if (instance == null)

        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public abstract T CreateInstance();
}