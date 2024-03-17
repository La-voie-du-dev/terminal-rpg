using TerminalRpg.Environment;
using TerminalRpg.Game;
using TerminalRpg.Game.Logging;
using TerminalRpg.Game.Engine;
using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

LogFactory.Install();

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

TileManager manager = new TileManager(hero);

string endMessage;
try {
    manager.Play(tile);

    endMessage = "WIN";
} catch (GameOverException) {
    endMessage = "GAME OVER";
}

// Affichage de la carte et du résultat
tile.Display();
Console.WriteLine(endMessage);
