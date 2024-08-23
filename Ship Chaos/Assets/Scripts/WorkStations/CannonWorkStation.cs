using UnityEngine;

public class CannonWorkStation : BaseWorkStation
{
    public override void Interact(Player player)
    {
        if (HasLabObject()) return;

        var labObject = player.GetLabObject();
        if (labObject.GetType() != typeof(AmmunitionObject)) return;
        
        SetLabObject(labObject);
        player.ClearLabObject();
        labObject.DestroyLabObject();
    }
}
