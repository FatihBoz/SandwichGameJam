

public class MainTower : Tower
{
    private void OnDestroy()
    {
        BeastPlayerCombat.OnDed?.Invoke();
    }
}
