using TerminalRpg.Environment;
using TerminalRpg.Game;
using TerminalRpg.Game.Input;
using TerminalRpg.Game.Input.Actions;
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

// Construction du menu
Menu menu = new Menu();
foreach (Node node in tile.Nodes) {
    if (node is Chest) {
        menu.AddMenuItem(new IInteractiveMenuItem(
            "Ouvrir le coffre", (IInteractive) node
        ));
    } else if (node is NPC) {
        menu.AddMenuItem(new IInteractiveMenuItem(
            "Discuter avec le personnage non-joueur", (IInteractive) node
        ));
    } else if (node is Enemy) {
        Enemy enemyNode = (Enemy) node;
        menu.AddMenuItem(new PhysicalAttackMenuItem(enemyNode));
        menu.AddMenuItem(new MagicAttackMenuItem(enemyNode));
        menu.AddMenuItem(new RobMenuItem(enemyNode));
    } else if (node is Hero) {
        Hero heroNode = (Hero) node;
        menu.AddMenuItem(new UseLifePotionMenuItem(heroNode));
        menu.AddMenuItem(new UseManaPotionMenuItem(heroNode));
    }
}

menu.SelectMenuItem().Execute(hero);
