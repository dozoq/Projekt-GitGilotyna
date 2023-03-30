

using System.Collections.Generic;

namespace Code.Weapon.WeaponTypes.Player
{
    public abstract class PlayerWeapon : WeaponType
    {
        public override string Name => this.GetType().Name;
        public Code.Player.Player player { get; set; }

    }
}