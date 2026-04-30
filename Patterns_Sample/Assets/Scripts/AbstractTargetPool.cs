using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractTargetPool : MonoBehaviour
{
    public Target targetBase;
    public int poolSize = 10;


    protected List<Target> targets = new List<Target>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddTargetToPool();
        }
    }

    private void AddTargetToPool()
    {
        Target target = Instantiate(targetBase, transform);
        ReturnTargetToPool(target);
    }

    public Target GetTarget()
    {
        if (targets.Count == 0)
        {
            AddTargetToPool();
        }

        Target target = targets[0];
        targets.RemoveAt(0);

        target.gameObject.SetActive(true);
        target.transform.SetParent(null);

        return target;
    }

    public void ReturnTargetToPool(Target target)
    {
        target.transform.SetParent(transform);
        target.transform.localPosition = Vector3.zero;
        target.gameObject.SetActive(false);
        targets.Add(target);
    }
}