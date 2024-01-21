using TerminalRpg.Environment;
using TerminalRpg.Game;
using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

// Préparation de la tuile
Tile tile = new Tile();

Hero hero = new Hero(100, 100, 50, 25, 2, 2);
Enemy enemy = new Enemy(100, 60, 10, 1, 4);

NPC npc = new NPC(1, 0);
Chest chest = new Chest(1, 2, 4, 0);

tile.AddNode(npc);
tile.AddNode(hero);
tile.AddNode(chest);
tile.AddNode(new Rock(1, 1));
tile.AddNode(new Rock(3, 4));
tile.AddNode(new Tree(3, 2));
tile.AddNode(new Tree(0, 3));
tile.AddNode(enemy);

tile.Display();

// Scénario de combat
for (int loop = 0; loop < 3; loop++) {
    hero.MagicAttack(enemy);
    enemy.Attack(hero);
}

hero.Attack(enemy);
enemy.Attack(hero);

hero.Attack(enemy);
tile.Display();

// Fouiller la carte
chest.InteractWith(hero);
npc.InteractWith(hero);

// Restauration
hero.UseLifePotion();
hero.UseManaPotion();
hero.UseManaPotion();

tile.Display();

Console.WriteLine("WIN");
