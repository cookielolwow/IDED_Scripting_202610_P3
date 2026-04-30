using UnityEngine;

public class TargetFacade : MonoBehaviour
{
    private static TargetFacade instance;
    public static TargetFacade Instance => instance;

    public BasicTargetPool basicPool;
    public HighTargetPool highPool;
    public LowTargetPool lowPool;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public Target GetTarget(int tipo)
    {
        if (tipo == 1)
        {
            return highPool.GetTarget(); 
        }
        else if (tipo == 2)
        {
            return lowPool.GetTarget();
        }
        else
        {
            return basicPool.GetTarget();
        }
    }

    public void ReturnTarget(Target target, int tipo)
    {
        if (tipo == 1)
        {
            highPool.ReturnTargetToPool(target);
        }
        else if (tipo == 2)
        {
            lowPool.ReturnTargetToPool(target);
        }
        else
        {
            basicPool.ReturnTargetToPool(target);
        }
    }
}