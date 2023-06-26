using System;
using Code.Mobs;
using Code.Player;
using Code.Weapon.WeaponData;

namespace Code.Enemy.WeaponTypes.WeaponDecorators
{
    public abstract class WeaponDecorator : IWeapon
    {
        public WeaponData data { get; set; }
        public  IWeapon weapon { get; private set; }
        public  WeaponDecoratorType type { get; set; }

        public static string GetResourceFromType(WeaponDecoratorType type)
        {
            switch (type)
            {
                case WeaponDecoratorType.Choke:
                    return "Choke";
                case WeaponDecoratorType.Barrel:
                    return "Barrel";
                case WeaponDecoratorType.Scope:
                    return "Scope";
                default:
                    return String.Empty;
            }
        }
        public int Level
        {
            get { return _level;} 
            set { 
                OnLevelUp();
                _level = value;
            } 
        }
        private int _level;

        public WeaponDecorator(IWeapon weapon)
        {
            this.weapon = weapon;
            data = weapon.data;
        }

        public abstract void Attack(Target target);
        public abstract void OnLevelUp();
        
        public static WeaponDecorator GetAttachmentOfType<T>(IWeapon weapon,WeaponDecoratorType type)
        {
            var temp = weapon;
            while (true)
            {
                if (temp.GetType().IsSubclassOf(typeof(WeaponDecorator)))
                {
                    var decorator = temp as WeaponDecorator;
                    if (decorator.type == type)
                    {
                        return decorator;
                    }
                    temp = decorator.weapon;
                }
                else if(temp.GetType().IsSubclassOf(typeof(T)))
                {
                    return null;
                }
                else
                {
                    throw new Exception("This shouldn't happen");
                }
            }
        }
    }

    public enum WeaponDecoratorType
    {
        Choke, Scope, Barrel
    }
}