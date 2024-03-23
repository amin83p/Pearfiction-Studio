using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    internal class Program
    {
        public static class MainData
        {
            /// <summary>
            /// To show the assistant print outs
            /// </summary>
            public static bool Development = false;

            private static List<List<string>> _reels = new List<List<string>>();
            /// <summary>
            /// Add or edit the reels, the code hadles the rest
            /// </summary>
            /// <returns>The reels and their contents</returns>
            public static List<List<string>> Reels()
            {
                if (_reels.Count == 0)
                {
                    List<string> Band1 = new List<string>() { "sym2", "sym7", "sym7", "sym1", "sym1", "sym5", "sym1", "sym4", "sym5", "sym3", "sym2", "sym3", "sym8", "sym4", "sym5", "sym2", "sym8", "sym5", "sym7", "sym2" };
                    List<string> Band2 = new List<string>() { "sym1", "sym6", "sym7", "sym6", "sym5", "sym5", "sym8", "sym5", "sym5", "sym4", "sym7", "sym2", "sym5", "sym7", "sym1", "sym5", "sym6", "sym8", "sym7", "sym6" };
                    List<string> Band3 = new List<string>() { "sym5", "sym2", "sym7", "sym8", "sym3", "sym2", "sym6", "sym2", "sym2", "sym5", "sym3", "sym5", "sym1", "sym6", "sym3", "sym2", "sym4", "sym1", "sym6", "sym8" };
                    List<string> Band4 = new List<string>() { "sym2", "sym6", "sym3", "sym6", "sym8", "sym8", "sym3", "sym6", "sym8", "sym1", "sym5", "sym1", "sym6", "sym3", "sym6", "sym7", "sym2", "sym5", "sym3", "sym6" };
                    List<string> Band5 = new List<string>() { "sym7", "sym8", "sym2", "sym3", "sym4", "sym1", "sym3", "sym2", "sym2", "sym4", "sym4", "sym2", "sym6", "sym4", "sym1", "sym6", "sym1", "sym6", "sym4", "sym8" };
                    //List<string> Band6 = new List<string>() { "sym5", "sym2", "sym7", "sym8", "sym3", "sym2", "sym6", "sym2", "sym2", "sym5", "sym3", "sym5", "sym1", "sym6", "sym3", "sym2", "sym4", "sym1", "sym6", "sym8" };
                    _reels = new List<List<string>>();
                    _reels.AddRange(new List<List<string>>() { Band1, Band2, Band3, Band4, Band5 });

                }
                return _reels;
            }
            /// <summary>
            /// Change the number of rows and let the code do the rest.
            /// </summary>
            public static int NuemberOfRows = 3;
            //
        }

        static void Main(string[] args)
        {
            //Edit MainData for more options such as add new reels and rows.
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                string _tlt = $"Slot Machine With {MainData.Reels().Count} Reels and {MainData.NuemberOfRows} Rows";
                string repeatedChar = new string('=', _tlt.Length);
                Console.WriteLine($"{repeatedChar}");
                Console.WriteLine($"{_tlt}");
                Console.WriteLine($"{repeatedChar}");
                Console.WriteLine("Please enter any key to spin or;\n   e/E to exit\n   r/R to see total results\n   z/Z to restart");
                Console.WriteLine($"{repeatedChar}");
                ConsoleKeyInfo _inp = Console.ReadKey(true);
                Console.ResetColor();
                //
                if (_inp.Key == ConsoleKey.E)
                    System.Environment.Exit(1);
                else if (_inp.Key == ConsoleKey.R)
                {
                    Console.Clear();
                    Console.WriteLine($"Total number of spin(s): {Turns.AllUserTurns.Count}");
                    Console.WriteLine($"Total number of win(s): {Turns.AllUserTurns.Sum(n => n.TotalWins.Symbol.Count)}");
                    Console.WriteLine($"Total payout: {Turns.AllUserTurns.Sum(n => n.TotalWins.Payout.Sum(w => w.Sum()))}");
                }
                else if (_inp.Key == ConsoleKey.Z)
                {
                    Turns.AllUserTurns.Clear();
                    Console.Clear();
                }
                else
                {
                    //Console.Clear();
                    Spin();
                    Console.WriteLine($"");
                    //Console.WriteLine("Press any key to return to menu...");
                    //Console.ReadKey();
                }
            }

        }

        private static void Spin()
        {
            Turns.AllUserTurns.Add(new Turns());
            int _nTurn = Turns.AllUserTurns.Count - 1;
            int _nRows = Turns.AllUserTurns[_nTurn].Elements[0].Count;
            int _nColumns = Turns.AllUserTurns[_nTurn].Elements.Count;
            //
            Console.WriteLine($"Positions: {string.Join(", ", Turns.AllUserTurns[_nTurn].Positions.Select(n=>n.ToString()).ToArray())}");
            Console.WriteLine($"Screen:");
            List<string> _prn = new List<string>();
            List<string> _prn1 = new List<string>();
            for (int j = 0; j < _nRows; j++)
            {
                string _str = "  ";
                string _str1 = string.Empty;
                for (int i = 0; i < _nColumns; i++)
                {
                    _str += Turns.AllUserTurns[_nTurn].Elements[i][j].Val + " ";
                    _str1 += Turns.AllUserTurns[_nTurn].Elements[i][j].Idx.ToString("D2") + " ";
                }
                _prn.Add(_str);
                _prn1.Add(_str1);
                Console.WriteLine($"{_prn[_prn.Count - 1]} - {_prn1[_prn1.Count - 1]}");
            }
            if (MainData.Development)
                Console.WriteLine($"-------------------------------------------");
            //TotalWins tW = new TotalWins(Turns.AllUserTurns[_nTurn]);
            Turns.AllUserTurns[_nTurn].TotalWins.PrintWin();
        }

        public class Element
        {
            public int Idx { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
            public string Val { get; set; }

            public Dictionary<string, int> Neighbours = new Dictionary<string, int>();

            public Element(int iIdx, string iSymbol, int iRows, int iColumns)
            {
                Idx = iIdx;
                Val = iSymbol;
                Row = iRows;
                Column = iColumns;
                //
                int _rc = MainData.Reels().Count;
                int _rr = MainData.NuemberOfRows;
                //Left side
                List<int> _idxs = new List<int>();
                if (iColumns > 0)
                {
                    int cntr = 0;
                    for (int i = iRows; i > iRows - _rr; i--)
                    {
                        Neighbours.Add($"L{cntr}", Idx - (_rc * i) - 1);
                        cntr++;
                    }
                }
                //Right side
                if (iColumns + 1 < _rc)
                {
                    int cntr = 0;
                    for (int i = iRows; i > iRows - _rr; i--)
                    {
                        Neighbours.Add($"R{cntr}", Idx - (_rc * i) + 1);
                        cntr++;
                    }
                }

            }
        }
        public class Turns
        {
            public static List<Turns> AllUserTurns = new List<Turns>();
            //
            private List<int> positions = new List<int>();
            public List<int> Positions { get => positions; }

            private List<List<Element>> elements = new List<List<Element>>();
            public List<List<Element>> Elements { get => elements; }

            private List<Element> elementsSerial = new List<Element>();
            public List<Element> ElementsSerial { get => elementsSerial; }

            private TotalWins totalWins;
            public TotalWins TotalWins { get => totalWins; }
            public Turns()
            {
                int _rc = MainData.Reels().Count;
                positions.Clear();
                Random rand = new Random();
                //positions = new List<int>() {  14, 7, 9, 17, 3 };
                for (int i = 0; i < _rc; i++)
                {
                    positions.Add(rand.Next(0, MainData.Reels()[i].Count));
                    int _r = positions[i];
                    //
                    List<Element> _cElements = new List<Element>();
                    _cElements.Add(new Element(i, MainData.Reels()[i][_r], 0, i));
                    for (int j = 1; j < MainData.NuemberOfRows; j++)
                        _cElements.Add(new Element(i + (_rc * j), MainData.Reels()[i][Idx_Caller(_r + j - 1, MainData.Reels()[i].Count)], j, i));
                    elements.Add(_cElements);
                    //
                }
                //Create Serila Elemnts
                for (int j = 0; j < MainData.NuemberOfRows; j++)
                {
                    for (int i = 0; i < _rc; i++)
                        elementsSerial.Add(Elements[i][j]);
                }
                //
                totalWins = new TotalWins(this);
            }
            private static int Idx_Caller(int iIdx, int iCount)
            {
                if (iIdx + 1 >= iCount)
                    return iIdx + 1 - iCount;
                else
                    return iIdx + 1;
            }
        }

        public class TotalWins
        {
            public List<string> Symbol { get; }
            public List<List<List<int>>> Indexs { get; }
            public List<List<int>> Payout { get; }
            public string Indexs_String
            {
                get => string.Join("-", Indexs.Select(n=>n.ToString()).ToArray());
            }

            public TotalWins(Turns iThisTurn)
            {
                Symbol = new List<string>();
                Indexs = new List<List<List<int>>>();
                Payout = new List<List<int>>();
                AnalysePossibleWins(iThisTurn);
            }

            private void AnalysePossibleWins(Turns iThisTurn)
            {
                string _sym = string.Empty;
                for (int i = 0; i < iThisTurn.ElementsSerial.Count; i++)
                {
                    _sym = iThisTurn.ElementsSerial[i].Val;
                    if (Symbol.Count(n => n == iThisTurn.ElementsSerial[i].Val) > 0)
                        continue;
                    if (MainData.Development)
                        Console.WriteLine($"{_sym}");
                    List<List<int>> _dmy0 = new List<List<int>>();
                    List<List<int>> _dmy1 = new List<List<int>>();
                    List<int> _dmy3 = new List<int>();
                    //export all identical sysmbols
                    for (int j = 0; j < MainData.Reels().Count(); j++)
                    {
                        List<int> _dmy2 = new List<int>();
                        for (int k = 0; k < MainData.NuemberOfRows; k++)
                        {
                            if (iThisTurn.Elements[j][k].Val == _sym)
                                _dmy2.Add(iThisTurn.Elements[j][k].Idx);
                        }
                        _dmy1.Add(_dmy2);
                    }
                    if (MainData.Development)
                    {
                        for (int k = 0; k < _dmy1.Count; k++)
                        {
                            if (_dmy1[k].Count == 0)
                                Console.WriteLine($"-");
                            else
                                Console.WriteLine($"{string.Join(", ", _dmy1[k].Select(n => n.ToString()).ToArray())}");
                        }
                    }
                    //check if there is atleast 3 identical symbols in a way
                    List<List<int>> _dmy4 = new List<List<int>>();
                    List<List<List<int>>> _dmy5 = new List<List<List<int>>>();
                    for (int k = 0; k < _dmy1.Count; k++)
                    {
                        //Console.WriteLine($"{string.Join(", ", _dmy1[k])}");
                        if (_dmy1[k].Count != 0)
                        {
                            _dmy4.Add(_dmy1[k]);
                            continue;
                        }
                        //
                        if (_dmy4.Count < 3)
                            _dmy4.Clear();
                        else
                        {
                            _dmy5.Add(_dmy4);
                            _dmy4 = new List<List<int>>();
                        }
                    }
                    if (_dmy5.Count == 0 && _dmy4.Count >= 3)
                        _dmy5.Add(_dmy4);
                    if (MainData.Development)
                    {
                        Console.WriteLine($"_dmy5: {_dmy5.Count}, _dmy4: {_dmy4.Count}");
                        for (int k = 0; k < _dmy5.Count; k++)
                        {
                            Console.WriteLine($"_dmy5:[{k}] Count: {_dmy5[k].Count}");
                            for (int kk = 0; kk < _dmy5[k].Count; kk++)
                            {
                                Console.WriteLine($"_dmy5:[{k}][{kk}]:");
                                Console.WriteLine($"Review _dmy5:[{k}][{kk}] :{string.Join(", ", _dmy5[k][kk].Select(n => n.ToString()).ToArray())}");
                            }
                        }

                        Console.WriteLine($"----------");
                    }
                    //check if there is at least one winning way
                    //calc. the payout
                    if (_dmy5.Count > 0)
                    {
                        for (int p = 0; p < _dmy5.Count; p++)
                        {
                            _dmy4 = _dmy5[p];
                            _dmy0.AddRange(GenerateAllPossibleLists(_dmy4));
                        }
                        if (MainData.Development)
                        {
                            for (int k = 0; k < _dmy0.Count; k++)
                                Console.WriteLine($"{string.Join(", ", _dmy0[k].Select(n => n.ToString()).ToArray())}");
                        }
                        Symbol.Add(_sym);
                        Indexs.Add(_dmy0);
                        for (int k = 0; k < _dmy0.Count; k++)
                            _dmy3.Add(PointCals(_sym, _dmy0[k].Count));
                        Payout.Add(_dmy3);
                    }
                    if (MainData.Development)
                        Console.WriteLine($"------------------------------");
                }
            }
            public void PrintWin()
            {
                int totalWins = 0;
                totalWins = Payout.Sum(n => n.Sum(m => m));
                Console.WriteLine($"Total wins: { totalWins }");

                for (int i = 0; i < Indexs.Count; i++)
                {
                    for (int j = 0; j < Indexs[i].Count; j++)
                        Console.WriteLine($"- Ways win {string.Join("-", Indexs[i][j].Select(n => n.ToString()).ToArray())}, {Symbol[i]} x{Indexs[i][j].Count}, {Payout[i][j]}");
                }
            }

            private static List<List<int>> GenerateAllPossibleLists(List<List<int>> iInputLists, int iIndex = 0)
            {
                if (iIndex == iInputLists.Count)
                {
                    return new List<List<int>> { new List<int>() };
                }

                var currentList = iInputLists[iIndex];
                var subLists = GenerateAllPossibleLists(iInputLists, iIndex + 1);
                var result = new List<List<int>>();

                foreach (var item in currentList)
                {
                    foreach (var subList in subLists)
                    {
                        var newList = new List<int> { item };
                        newList.AddRange(subList);
                        result.Add(newList);
                    }
                }

                return result;
            }

            private static int PointCals(string iSymbol, int iRepeat)
            {
                int points = 0;
                switch (iSymbol.Trim())
                {
                    case "sym1":
                    case "sym2":
                        if (iRepeat == 3)
                            points = 1;
                        else if (iRepeat == 4)
                            points = 2;
                        if (iRepeat == 5)
                            points = 3;
                        break;
                    case "sym3":
                        if (iRepeat == 3)
                            points = 1;
                        else if (iRepeat == 4)
                            points = 2;
                        if (iRepeat == 5)
                            points = 5;
                        break;
                    case "sym4":
                        if (iRepeat == 3)
                            points = 2;
                        else if (iRepeat == 4)
                            points = 5;
                        if (iRepeat == 5)
                            points = 10;
                        break;
                    case "sym5":
                    case "sym6":
                        if (iRepeat == 3)
                            points = 2;
                        else if (iRepeat == 4)
                            points = 5;
                        if (iRepeat == 5)
                            points = 15;
                        break;
                    case "sym7":
                        if (iRepeat == 3)
                            points = 5;
                        else if (iRepeat == 4)
                            points = 10;
                        if (iRepeat == 5)
                            points = 20;
                        break;
                    case "sym8":
                        if (iRepeat == 3)
                            points = 10;
                        else if (iRepeat == 4)
                            points = 20;
                        if (iRepeat == 5)
                            points = 50;
                        break;
                }
                return points;
            }
        }

    }
}
