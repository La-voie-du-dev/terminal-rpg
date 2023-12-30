using TerminalRpg.Environment;
using TerminalRpg.Game;

Tile tile = new Tile();

tile.AddNode(new Node('o', 1, 0));
tile.AddNode(new Node('o', 2, 2));
tile.AddNode(new Node('#', 4, 0));
tile.AddNode(new Rock(1, 1));
tile.AddNode(new Rock(3, 4));
tile.AddNode(new Tree(3, 2));
tile.AddNode(new Tree(0, 3));
tile.AddNode(new Node('†', 1, 4));

tile.Display();
