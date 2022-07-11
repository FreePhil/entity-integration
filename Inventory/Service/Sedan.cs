namespace Inventory.Service;

public class Sedan: ICar
{
    private const int MaxPower = 300;
    
    private IEngine _engine;

    public Sedan(IEngine engine)
    {
        _engine = engine;
    }
    
    public int HorsePower
    {
        get
        {
            if (_engine.Power >= MaxPower)
            {
                return MaxPower;
            }
            else
            {
                return _engine.Power;
            }
        }
    }
}