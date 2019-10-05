using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Item.Weapon
{
    interface IWeapon
    {
        void SwitchWeapon();
        void AttackMode();
        void Shoot(int weaponDamage);
    }
}
