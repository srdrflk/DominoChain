
var dominos = new List<(int, int)> { (1, 2), (4, 1), (2, 4) };

var result = FindCircularDominoChain(dominos);

if (result != null && result.Count != 0)
{
    Console.WriteLine("Circular Domino Chain:");
    foreach (var domino in result)
    {
        Console.Write($"[{domino.Item1}|{domino.Item2}] ");
    }
}
else
{
    Console.WriteLine("Good luck and enjoy!");
}

static List<(int, int)> FindCircularDominoChain(List<(int, int)> dominos)
{
    var chain = new List<(int, int)>();
    var used = new bool[dominos.Count];

    // Start backtracking to find a solution
    bool Backtrack(int currentEnd)
    {
        if (chain.Count == dominos.Count)
        {
            // Check if the chain is circular
            return chain[0].Item1 == chain[^1].Item2;
        }

        for (int i = 0; i < dominos.Count; i++)
        {
            if (!used[i])
            {
                var domino = dominos[i];

                // Try both orientations of the domino
                if (domino.Item1 == currentEnd)
                {
                    used[i] = true;
                    chain.Add(domino);

                    if (Backtrack(domino.Item2)) return true;

                    chain.RemoveAt(chain.Count - 1);
                    used[i] = false;
                }
                else if (domino.Item2 == currentEnd)
                {
                    used[i] = true;
                    chain.Add((domino.Item2, domino.Item1));

                    if (Backtrack(domino.Item1)) return true;

                    chain.RemoveAt(chain.Count - 1);
                    used[i] = false;
                }
            }
        }

        return false;
    }

    // Try to start with each domino in both orientations
    for (int i = 0; i < dominos.Count; i++)
    {
        var domino = dominos[i];

        used[i] = true;
        chain.Add(domino);
        if (Backtrack(domino.Item2)) return chain;
        chain.RemoveAt(chain.Count - 1);

        chain.Add((domino.Item2, domino.Item1));
        if (Backtrack(domino.Item1)) return chain;
        chain.RemoveAt(chain.Count - 1);
        used[i] = false;
    }

    return new List<(int, int)>();
}