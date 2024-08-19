public class TrashBinWorkStation : BaseWorkStation
{
    public override void Interact(Player player)
    {
        if (player.HasLabObject())
        {
            player.GetLabObject().DestroyLabObject();
            player.ClearLabObject();
        }
    }
}
