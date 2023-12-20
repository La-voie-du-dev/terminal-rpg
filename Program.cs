using TerminalRpg.Game;

Node node = new Node();

node.X = 2;
node.Y = 2;
node.Representation = 'o';

Console.WriteLine(
    "At ({0}, {1}) '{2}'", node.X, node.Y, node.Representation
);
