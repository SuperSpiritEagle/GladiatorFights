using System;
using System.Collections.Generic;

namespace GladiatorFights
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;

            while (isWork)
            {
                Arena arena = new Arena();

                if (arena.SelectionFighters())
                {
                    Console.Clear();
                    arena.Battle();
                    arena.ShowBattleResult();
                }
            }
        }
    }

    class Arena
    {
        private Fighter _firstFighter;
        private Fighter _secondFighter;
        private List<Fighter> _fighters = new List<Fighter>();

        public Arena()
        {
            _fighters.Add(new Ninja(100, 50, 5, 40));
            _fighters.Add(new Knight(100, 50, 10));
            _fighters.Add(new Archer(100, 50, 3));
            _fighters.Add(new Barbarian(100, 50, 1));
            _fighters.Add(new Terminator(100, 50, 15));
        }

        public bool SelectionFighters()
        {
            Console.WriteLine("Боец 1");
            GetFigter(out _firstFighter);

            Console.WriteLine("Боец 2");
            GetFigter(out _secondFighter);

            if (_firstFighter == null || _secondFighter == null)
            {
                Console.WriteLine("Ошибка выбора бойца");
                return false;
            }
            else
            {
                Console.WriteLine("Бойцы выбраны");
                return true;
            }
        }

        public void Battle()
        {
            while (_firstFighter.Health > 0 && _secondFighter.Health > 0)
            {
                _firstFighter.ShowInfo();
                _secondFighter.ShowInfo();
                _firstFighter.TakeDamage(_secondFighter.Damage);
                _secondFighter.TakeDamage(_firstFighter.Damage);
                _firstFighter.ApplySuperAttack();
                _secondFighter.ApplySuperAttack();
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void ShowBattleResult()
        {
            if (_firstFighter.Health <= 0 && _secondFighter.Health <= 0)
            {
                Console.WriteLine("Ничья, оба погибли");
            }
            else if (_firstFighter.Health <= 0)
            {
                Console.WriteLine($"{_secondFighter} победил!");
            }
            else if (_secondFighter.Health <= 0)
            {
                Console.WriteLine($"{_firstFighter} победил!");
            }
        }

        private void GetFigter(out Fighter fighter)
        {
            ShowFighters();
            Console.WriteLine("Выберите бойца");
            bool isTry = int.TryParse(Console.ReadLine(), out int userInput);

            if (isTry == false)
            {
                Console.WriteLine("Hе коректные данные");
                fighter = null;
            }

            else if (userInput > 0 && userInput - 1 < _fighters.Count)
            {
                fighter = _fighters[userInput - 1];
                _fighters.Remove(fighter);
                Console.WriteLine("Боец успешно выбран.");
            }
            else
            {
                Console.WriteLine("Боец с таким номером отсутствует.");
                fighter = null;
            }
        }

        private void ShowFighters()
        {
            Console.WriteLine("Список бойцов");
            int number = 1;

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write(i + number + " ");
                _fighters[i].ShowInfo();
            }
        }
    }

    class Fighter
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }

        public Fighter(int health, int damage, int armor)
        {
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public void TakeDamage(int damage)
        {
            Random random = new Random();

            int minDamage = 15;
            int randomDamage = random.Next(minDamage, damage);
            int actualDamage = randomDamage - Armor;

            Health -= actualDamage;

            Console.WriteLine($"{GetType().Name} получил {actualDamage} урона");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{GetType().Name} Здоровье: {Health} Урон: {Damage} Броня: {Armor}");
        }

        public void ApplySuperAttack()
        {
            Random random = new Random();

            int rangeMaximalNumbers = 100;
            int chanceUsingAbility = random.Next(rangeMaximalNumbers);
            int chanceAbility = 50;

            if (chanceUsingAbility < chanceAbility)
            {
                SuperPower();
            }
        }

        protected virtual void SuperPower() { }
    }

    class Ninja : Fighter
    {
        private int _speedAttak;

        public Ninja(int health, int damage, int armor, int speedAttak) : base(health, damage, armor)
        {
            _speedAttak = speedAttak;
        }

        protected override void SuperPower()
        {
            Console.WriteLine($"{GetType().Name} Применил дымовую шашку и выполнил скоростную атаку");
            Damage += _speedAttak;
        }
    }

    class Knight : Fighter
    {
        private int _prayer = 20;

        public Knight(int health, int damage, int armor) : base(health, damage, armor) { }

        protected override void SuperPower()
        {
            Console.WriteLine($"{GetType().Name} Помолился и увеличил свою силу и здоровье ");

            Health += _prayer;
            Damage += _prayer;
        }
    }

    class Archer : Fighter
    {
        private int _sniperStance = 50;

        public Archer(int health, int damage, int armor) : base(health, damage, armor) { }

        protected override void SuperPower()
        {
            Console.WriteLine($"{GetType().Name} Использовал стоику снаипера у величил свой урон");
            Damage += _sniperStance;
        }
    }

    class Barbarian : Fighter
    {
        private int _berserking = 60;

        public Barbarian(int health, int damage, int armor) : base(health, damage, armor) { }

        protected override void SuperPower()
        {
            Console.WriteLine($"{GetType().Name} Использовал berserking ");
            Damage += _berserking;
        }
    }

    class Terminator : Fighter
    {
        int minigun = 60;

        public Terminator(int health, int damage, int armor) : base(health, damage, armor) { }

        protected override void SuperPower()
        {
            Console.WriteLine($"{GetType().Name} Применил миниган ");
            Damage += minigun;
        }
    }
}
