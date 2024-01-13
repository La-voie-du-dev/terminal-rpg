using TerminalRpg.Environment;
using TerminalRpg.Game;
using TerminalRpg.Role;
using TerminalRpg.Role.Fighters;

Tile tile = new Tile();

Hero hero = new Hero(100, 100, 50, 25, 2, 2);

NPC npc = new NPC(1, 0);
Chest chest = new Chest(1, 2, 4, 0);

tile.AddNode(npc);
tile.AddNode(hero);
tile.AddNode(chest);
tile.AddNode(new Rock(1, 1));
tile.AddNode(new Rock(3, 4));
tile.AddNode(new Tree(3, 2));
tile.AddNode(new Tree(0, 3));
tile.AddNode(new Node('†', 1, 4));

tile.Display();

// Tester des interactions
IInteractive interactiveInstance;

interactiveInstance = npc;
interactiveInstance.InteractWith(hero);

interactiveInstance = chest;
interactiveInstance.InteractWith(hero);
