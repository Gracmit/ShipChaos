public class ClearWorkStation : BaseWorkStation
{
    public override void Interact(Player player)
    {
        if (!HasLabObject()) //Not Lab object in the workstation
        {
            if (player.HasLabObject())
            {
                player.GetLabObject().SetLabObjectParent(this);
            }
        }
        else // Lab Object is in the workstation
        {
            if (player.HasLabObject()) return;
            GetLabObject().SetLabObjectParent(player);
        }
        
    }
}
