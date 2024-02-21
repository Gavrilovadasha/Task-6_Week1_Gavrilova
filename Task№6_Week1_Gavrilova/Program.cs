using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowMagician
{
    class Magician
    {
        public int HealthMagician { get; set; }
        public int AttackPower { get; set; }
        public bool IsInvisible { get; set; }
        public bool IsInPortal { get; set; }
        public Magician()
        {
            Random rnd = new Random();
            HealthMagician = rnd.Next(300, 700);
            AttackPower = rnd.Next(60, 100);
            IsInvisible = false; 
            IsInPortal = false; 
        }
        public void SpellAttack(Boss boss)
        {
            // Условие, при котором игрок не находится в портале.
            if (!IsInPortal)
            {
                boss.HealthBoss -= AttackPower;
                AttackPower += 20;
                Console.WriteLine("Вы применили заклинание \"конгён\" и нанесли {0} урона боссу.", AttackPower);
            }
            else
            {
                Console.WriteLine("Вы не можете атаковать из портала.");
            }
        }
        public void SpellInvisibleSpirit()
        {
            Random rnd = new Random();
            int invisible = rnd.Next(1, 10);

            if (!IsInPortal && HealthMagician >= 200)
            {
                HealthMagician -= 200;
                IsInvisible = true;
                Console.WriteLine("Вы применили заклинание \"поиджи анын сарам\" и стали невидимым на " + invisible + " хода(ов).");
            }
            else if (IsInPortal) // Если игрок находится в портале
            {
                Console.WriteLine("Вы не можете стать невидимым в портале.");
            }
            else
            {
                Console.WriteLine("У вас недостаточно здоровья для применения заклинания \"поиджи анын сарам\".");
            }
        }

        public void SpellPortal()
        {
            if (!IsInPortal && !IsInvisible && HealthMagician >= 100)
            {
                HealthMagician -= 100;
                IsInPortal = true;
                Console.WriteLine("Вы применили заклинание \"хёнгванэ кассо\" и вошли в портал.");
            }
            else if (IsInPortal)
            {
                Console.WriteLine("Вы уже находитесь в портале.");
            }
            else if (IsInvisible)
            {
                Console.WriteLine("Вы не можете войти в портал, будучи невидимым.");
            }
            else
            {
                Console.WriteLine("У вас недостаточно здоровья для применения заклинания \"хёнгванэ кассо\".");
            }
        }
        public void SpellКeturningPortal()
        {
            if (IsInPortal)
            {
                IsInPortal = false;
                Console.WriteLine("Вы применили заклинание \"хёнгванэсо торавассо\" и вышли из портала.");
            }
            else
            {
                Console.WriteLine("Вы не находитесь в портале.");
            }
        }

        public void SpellRestoringHealth()
        {
            if (!IsInPortal && HealthMagician >= 50)
            {
                for (int i = 0; i < 15; i++)
                {
                    HealthMagician += 39;
                }
                Console.WriteLine("Вы применили заклинание \"орэ ккынын хвэбок\" и восстановили свое здоровье на протяжении следующих 13 ходов.");
            }
            else if (IsInPortal)
            {
                Console.WriteLine("Вы не можете применять заклинание \"орэ ккынын хвэбок\" в портале."); // Выводим сообщение о том, что игрок не может применять заклинание восстановления здоровья в портале
            }
            else
            {
                Console.WriteLine("У вас недостаточно здоровья для применения заклинания \"орэ ккынын хвэбок\"."); // Выводим сообщение о том, что у игрока недостаточно здоровья
            }
        }

        public void Update()
        {
            if (IsInvisible)
            {
                IsInvisible = false;
            }

            HealthMagician += 39;
        }

        // Проверка жив ли игрок.
        public bool IsAlive
        {
            get { return HealthMagician > 0; }
        }

    }

    class Boss
    {
        public int HealthBoss { get; set; } 
        public int AttackPowerBoss { get; set; } 

        public Boss()
        {
            Random rnd = new Random();
            HealthBoss = rnd.Next(300, 700);
            AttackPowerBoss = rnd.Next(60, 100);
        }

        public void AttackBoss(Magician magician)
        {
            if (!magician.IsInPortal) 
            {
                magician.HealthMagician -= AttackPowerBoss; 
                AttackPowerBoss += 25;
                Console.WriteLine("Босс атаковал вас и нанес " + AttackPowerBoss + " урона.");
            }
        }

        public void Update()
        {
            // Нет обновлений для босса
        }

        public bool IsAlive
        {
            get { return HealthBoss > 0; }
        }
    }

    class Program
    {
        static void Spell()
        {
            Random rnd = new Random();

            int SpellAttack = rnd.Next(60, 100);
            Console.WriteLine("\nконгён - атака, может наноситься урон от " + SpellAttack + " единиц\n" +
                              "Сила атаки в дальнейшем может увеличиваться");

            int SpellInvisibleSpirit = rnd.Next(50, 150);
            int invisible = rnd.Next(1, 10);
            Console.WriteLine("\nпоиджи анын сарам - призыв духа Невидимости. Тратится " + SpellInvisibleSpirit + " единиц здоровья.\n" +
                              "Невидимость держится" + invisible + " игровых тактов");

            int SpellPortal = rnd.Next(10, 60);
            int health = rnd.Next(10, 40);
            int power = rnd.Next(10, 40);
            Console.WriteLine("\nхёнгванэ кассо - переход в портал. Тратится " + SpellPortal +
                              " единиц здоровья.\nБОСС не может наносить удары по игроку, который спрятался в портале.\n" +
                              "В портал нельзя уйти невидимым.\n" +
                              "В портале нельзя стать невидимым.\n" +
                              "Из портала нельзя атаковать БОССА.\n" +
                              "Каждый игровой такт увеличивает здоровье на " + health + " единиц\n" +
                              "Каждый игровой такт усиливает силу атаки на " + power + " единиц");

            Console.WriteLine("\nхёнгванэсо торавассо - возвращение из портала");

            int SpellКeturningPortal = rnd.Next(30, 60);
            int gameTact = rnd.Next(10, 20);
            int healthPortal = rnd.Next(30, 60);
            Console.WriteLine("\nорэ ккынын хвэбок - длительное восстановление.Тратится " + SpellКeturningPortal + " единиц здоровья,\n" +
                              "но следующие " + gameTact + " игровых тактов восстанавливается по " + healthPortal + " единиц здоровья за такт");

        }
        static void Main(string[] args)
        {
            // Создаем объекты игрока и босса
            Magician magician = new Magician();
            Boss boss = new Boss();

            Console.WriteLine("Игра - победи БОССА");
            Console.WriteLine("Условия: ");

            //Создание объекта для генерации чисел
            Random rnd = new Random();
            //Получить очередное (в данном случае - первое) случайное число
            int lifeBoss = rnd.Next(600, 1000);
            //Вывод полученного числа в консоль
            Console.WriteLine("Максимальный уровень жизни у БОССА - " + lifeBoss);

            int lifeMagician = rnd.Next(600, 1000);
            Console.WriteLine("Максимальный уровень жизни у МАГА - " + lifeMagician);

            Console.WriteLine("Случайным образом выбирается игрок, делающий первый ход\n" +
                              "Величина урона, наносимого БОССОМ, для каждого хода случайна\n" +
                              "Игрок может пользоваться следующими заклинаниями: ");
            Spell();

            // Отображаем состояние игры
            Console.WriteLine("Начальное здоровье у МАГА - " + magician.HealthMagician + " единиц");
            Console.WriteLine("Начальное здоровье у БОССА - " + boss.HealthBoss + " единиц");

            // Игровой цикл
            while (magician.IsAlive && boss.IsAlive)
            {
                // Получаем ввод игрока
                Console.WriteLine("Введите заклинание для применения:");
                string spell = Console.ReadLine();

                // Применяем заклинание
                switch (spell)
                {
                    case "конгён":
                        magician.SpellAttack(boss);
                        break;
                    case "поиджи анын сарам":
                        magician.SpellInvisibleSpirit();
                        break;
                    case "хёнгванэ кассо":
                        magician.SpellPortal();
                        break;
                    case "хёнгванэсо торавассо":
                        magician.SpellКeturningPortal();
                        break;
                    case "орэ ккынын хвэбок":
                        magician.SpellRestoringHealth();
                        break;
                    default:
                        Console.WriteLine("Неверное заклинание.");
                        break;
                }
                // Босс атакует игрока
                boss.AttackBoss(magician);

                // Обновляем состояние игры
                magician.Update();
                boss.Update();
            }

            // Игра окончена
            if (magician.IsAlive)
            {
                Console.WriteLine("Вы победили!");
            }
            else
            {
                Console.WriteLine("Вы проиграли!");
            }
        }
    }
}


