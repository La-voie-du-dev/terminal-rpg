using TerminalRpg.Game;

Node node = new Node('o', 2, 2);
Node nRef = node;

Console.WriteLine(
    "node Node['{0}', ({1}, {2})] / nRef Node['{3}', ({4}, {5})]",
    node.Representation, node.X, node.Y,
    nRef.Representation, nRef.X, nRef.Y
);

nRef.X = 1;
nRef.Y = 3;
nRef.Representation = '#';

Console.WriteLine(
    "node Node['{0}', ({1}, {2})] / nRef Node['{3}', ({4}, {5})]",
    node.Representation, node.X, node.Y,
    nRef.Representation, nRef.X, nRef.Y
);
