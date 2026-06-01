public class RoomPanel:BasePanel
{
    public override void InitContent()
    {
        UIManager.Instance.RegisterSingletonPanel<RoomPanel>(this);
        EventManager.Instance.AddListener<LoadRoomDataEvent>(OnLoadRoomData);
    }

    void OnLoadRoomData(IEventMessage message)
    {
        var msg = message as LoadRoomDataEvent;
        // TODO: load room data
    }

}