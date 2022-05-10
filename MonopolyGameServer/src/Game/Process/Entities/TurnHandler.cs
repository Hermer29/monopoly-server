namespace MonopolyGameServer.Game.Process
{
    public partial class PlayerInGame
    {
        public class TurnHandler
        {
            private Process.PlayerInGame _player;
            private bool _moved = false;

            private const int MapSize = 40;

            public TurnHandler(Process.PlayerInGame player)
            {
                _player = player;
            }

            public bool HasActions => _moved == false;
            public int Position => _player._position;

            public bool TryRemoveCash(int amount)
            {
                if (_player._cashAmount - amount < 0)
                    return false;

                _player._cashAmount -= amount;
                return true;
            }

            public void MovePlayerForce(int position)
            {
                _player._position = position;
            }

            public void RemoveCashForce(int amount)
            {
                if (_player._cashAmount - amount < 0)
                {
                    _player.Bankrupted?.Invoke(_player, new EventArgs());
                    return;
                }

                _player._cashAmount -= amount;
                _player.CashChanged?.Invoke(_player, _player._cashAmount);
            }

            public void AddCash(int amount)
            {
                _player._cashAmount += amount;
            }

            public bool IsHasEnoughMoney(int amount)
            {
                throw new NotImplementedException();
            }

            public bool TryRollMove(out (int, int)? diceValues)
            {
                if (_moved)
                {
                    diceValues = null;
                    return false;
                }

                var firstCube = Random.Shared.Next(1, 7);
                var secondCube = Random.Shared.Next(1, 7);
                _player.DiceRolled?.Invoke(_player, (firstCube, secondCube));
                _moved = true;

                if (firstCube == secondCube)
                    _moved = false;

                diceValues = (firstCube, secondCube);
                Move(firstCube + secondCube);
                return true;
            }

            private void Move(int steps)
            {
                var changed = _player._position + steps;
                if (changed > MapSize)
                {
                    _player._position = changed - MapSize;
                }
                else
                {
                    _player._position = changed;
                }
                _player.PositionChanged?.Invoke(_player, _player._position);
            }

            public void TimeOutLost()
            {
                _player._lost = true;
                _player.Bankrupted?.Invoke(_player, new EventArgs());
            }
        }
    }
}
