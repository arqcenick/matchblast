namespace Game.Util
{
    public interface IPlayer
    {
        int GetHealth();

        void SetHealth(int health);
        int GetStars();
        int GetCurrentLevel();
    }
}