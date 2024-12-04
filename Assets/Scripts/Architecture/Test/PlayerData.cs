
using System;

namespace Assets.Scripts.Architecture.Test
{
    [Serializable]
    public class PlayerData
    {
        public int _level;
        public int _health;
        public float[] _position;

        public PlayerData(Player player)
        {
            _level = player._level;
            _health = player._health;

            _position = new float[3];
            _position[0] = 0;
            _position[1] = 0;
            _position[2] = 0;
        }
    }
}
