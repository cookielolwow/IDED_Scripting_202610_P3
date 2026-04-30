using System.Collections;
using UnityEngine;

public abstract class ShootDecorator : ICommand
{
    public ICommand commandBase;

    public ShootDecorator(ICommand command)
    {
        commandBase = command;
    }

    public virtual void Execute()
    {
        if (commandBase != null)
        {
            commandBase.Execute();
        }
    }
}

public class NormalShootDecorator : ShootDecorator
{
    public NormalShootDecorator(ICommand command) : base(command)
    {
    }

    public override void Execute()
    {
        commandBase.Execute();
    }
}

public class TripleShootDecorator : ShootDecorator
{
    private Player playerScript;

    public TripleShootDecorator(ICommand command, Player player) : base(command)
    {
        playerScript = player;
    }

    public override void Execute()
    {
        playerScript.StartCoroutine(ShootSequence());
    }

    private IEnumerator ShootSequence()
    {
        commandBase.Execute();
        yield return new WaitForSeconds(0.15f);

        commandBase.Execute();
        yield return new WaitForSeconds(0.15f);

        commandBase.Execute(); 
    }
}