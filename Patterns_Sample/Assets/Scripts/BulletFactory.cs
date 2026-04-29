public class BulletFactory : FactoryBase<Bullet>
{
    public override Bullet CreateInstance()
        => Instantiate(spawnObjects[0]);
}