public interface IPlayerState
{
    void Enter(LinkController link);
    void Exit();
    void Update();
    void HandleInput();
}
