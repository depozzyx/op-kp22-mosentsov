using System;

class Percolation {
    enum CellState
    {
        Closed = 0,
        Open = 1, 
    }
    static int[,] intialMatrix;

    // creates n-by-n grid, with all sites initially blocked
    static public void init(int n) 
    {
        intialMatrix = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                intialMatrix[i, j] = (int) CellState.Closed;

        UnionFind.initSize(n);
    }


    // opens the site (row, col) if it is not open already
    static public void open(int row, int col)
    {
        intialMatrix[row - 1, col - 1] = (int) CellState.Open;
    }


    // is the site (row, col) open?
    static public bool isOpen(int row, int col)
    {
        return intialMatrix[row - 1, col - 1] == (int) CellState.Open;
    }

    // is the site (row, col) full?
    static public bool isFull(int row, int col)
    {
        UnionFind.main(intialMatrix);
        return UnionFind.connected4(0, (row - 1)*intialMatrix.GetLength(0) + (col - 1) + 1);
    }

    // returns the number of open sites
    static public int numberOfOpenSites()
    {
        int count = 0;
        for (int i = 0; i < intialMatrix.GetLength(0); i++)
            for (int j = 0; j < intialMatrix.GetLength(1); j++)
                count += isOpen(i + 1, j + 1) ? 1 : 0;
            
        return count;
    }

    // does the system percolate?
    static public bool percolates()
    {
        return UnionFind.main(intialMatrix);
    }

    // prints the matrix on the screen
    // The method should display different types of sites (open, blocked, full)
    static public void print()
    {
        for (int i = 0; i < intialMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < intialMatrix.GetLength(1); j++) {
                Console.Write(intialMatrix[i, j] + " ");
            }
            Console.Write("\n");
        }
    }

    // test client (optional)
    static void Main(String[] args)
    {
        Tests.main();
        init(5);

        open(1, 2);
        open(2, 1);
        open(2, 2);
        open(2, 3);
        open(2, 4);
        open(3, 1);
        open(3, 4);
        open(4, 1);
        open(4, 3);
        // open(5, 1);
        // open(5, 2);
        open(1, 5);
        open(2, 5);
        open(3, 5);
        open(4, 5);
        open(5, 5);

        print();

        Console.WriteLine("Percolates? " + (percolates() ? "Yes" : "No"));
    }
}

class Tests {
    static void testInit() 
    {
        head("Percolation.init()");

        test("no error", () => {
            Percolation.init(10);
            return true;
        });
        test("no error", () => {
            Percolation.init(1);
            return true;
        });

        passed();
    }

    static void testOpen()
    {
        head("Percolation.open()");

        test("no error", () => {
            Percolation.init(4);
            Percolation.open(1, 1);
            return true;
        });
        test("opened cell matches", () => {
            Percolation.init(4);
            Percolation.open(1, 1);
            return Percolation.isOpen(1, 1);
        });
        test("human index", () => {
            Percolation.init(4);
            Percolation.open(4, 4);
            return Percolation.isOpen(4, 4);
        });

        passed();
    }

    static void testIsOpen()
    {
        head("Percolation.isOpen()");

        test("no error", () => {
            Percolation.init(4);
            Percolation.isOpen(1, 1);
            return true;
        });
        test("is open false", () => {
            Percolation.init(4);
            return Percolation.isOpen(1, 1) == false;
        });
        test("is open true", () => {
            Percolation.init(4);
            Percolation.open(3, 1);
            return Percolation.isOpen(3, 1);
        });

        passed();
    }

    static void testIsFull()
    {
        head("Percolation.isFull()");

        test("no error", () => {
            Percolation.init(4);
            Percolation.isFull(1, 1);
            return true;
        });
        test("is full true", () => {
            Percolation.init(4);
            Percolation.open(1, 1);
            Percolation.open(2, 1);
            return Percolation.isFull(2, 1) == true;
        });
        test("is full false", () => {
            Percolation.init(4);
            Percolation.open(2, 1);
            return Percolation.isFull(2, 1) == false;
        });

        passed();
    }

    static void testNumberOfOpenSites()
    {
        head("Percolation.numberOfOpenSites()");

        test("no error", () => {
            Percolation.init(4);
            Percolation.numberOfOpenSites();
            return true;
        });
        test("number is zero", () => {
            Percolation.init(4);
            return Percolation.numberOfOpenSites() == 0;
        });
        test("2 cells opened", () => {
            Percolation.init(4);
            Percolation.open(2, 1);
            Percolation.open(1, 1);
            return Percolation.numberOfOpenSites() == 2;
        });
        test("one cell opened two times", () => {
            Percolation.init(4);
            Percolation.open(2, 1);
            Percolation.open(2, 1);
            return Percolation.numberOfOpenSites() == 1;
        });

        passed();
    }

    static void testPercolates()
    {
        head("Percolation.testPercolates()");
        test("no error", () => {
            Percolation.init(2);
            return Percolation.percolates() == false;
        });
        test("percolates", () => {
            Percolation.init(2);
            Percolation.open(1, 1);
            Percolation.open(2, 1);
            return Percolation.percolates() == true;
        });
        test("does not percolate", () => {
            Percolation.init(2);
            Percolation.open(1, 1);
            Percolation.open(2, 2);
            return Percolation.percolates() == false;
        });
        test("does not percolate", () => {
            Percolation.init(3);
            Percolation.open(1, 1);
            Percolation.open(2, 1);
            return Percolation.percolates() == false;
        });
        passed();
    }

    static void testPrint()
    {
        head("Percolation.testprint()");
        test("no error", () => {
            Percolation.init(4);
            Percolation.print();
            return true;
        });
        passed();
    }

    static void testInitSize()
    {
        head("UnionFind.initSize()");
        test("no error", () => {
            UnionFind.initSize(4);
            return true;
        });
        passed();
    }

    static void testRoot()
    {
        head("UnionFind.root()");
        test("no error", () => {
            UnionFind.initSize(4);
            UnionFind.root(3);
            return true;
        });
        passed();
    }

    static void testUnion4()
    {
        head("UnionFind.union4()");
        test("no error", () => {
            UnionFind.initSize(4);
            UnionFind.union4(2, 3);
            return true;
        });
        test("connected after union", () => {
            UnionFind.initSize(4);
            UnionFind.union4(1, 2);
            return UnionFind.connected4(1, 2);
        });
        passed();
    }

    static void testConnected4()
    {
        head("UnionFind.union4()");
        test("no error", () => {
            UnionFind.initSize(4);
            UnionFind.union4(2, 3);
            return true;
        });
        test("connected", () => {
            UnionFind.initSize(4);
            UnionFind.union4(2, 3);
            return UnionFind.connected4(2, 3);
        });
        test("not connected", () => {
            UnionFind.initSize(4);
            UnionFind.union4(2, 3);
            return UnionFind.connected4(1, 3) == false;
        });
        passed();
    }

    static public void main()
    {
        Console.WriteLine("###### Testing ######");

        testInit();
        testIsFull();
        testIsOpen();
        testNumberOfOpenSites();
        testOpen();
        testPercolates();
        testPrint();

        testInitSize();
        testRoot();
        testConnected4();
        testUnion4();

        Console.WriteLine("###### Testing ended ######");
        Console.WriteLine("###### Passed " + passedNumber + " tests ######");
    }

    static private int passedNumber = 0;
    static private void head(string testName) 
    {
        Console.WriteLine("### Test of " + testName + " ###");
        caseNumber = 0;
    }
    static private int caseNumber = 0;
    static private void test(string title, Func<bool> func) 
    {
        Console.WriteLine(" " + caseNumber + ": " + title);
        bool res = func();
        if (res) {
            Console.WriteLine(" " + caseNumber + ": PASSED");
            passedNumber += 1;
        } else {
            Console.WriteLine(" " + caseNumber + ": FAILED");
            Console.WriteLine("... Waiting for input to continue ...");
            Console.ReadLine();
        }
        caseNumber += 1;
    }
    static private void passed() 
    {
        Console.WriteLine("# # Passed  # #\n");
    }
}

class UnionFind 
{
    static int size;

    static int n;
    static int[] nodes;
    static int[] sizes;

    static public void initSize(int matrixSize)
    {
        size = matrixSize;
        n = size * size + 2;
        nodes = new int[n];
        sizes = new int[n];

        for (int i = 0; i < nodes.Length; i++)
            nodes[i] = i;
    }

    static public bool main(int[,] matrix)
    {
        initSize(matrix.GetLength(0));

        for (int i = 0; i < size; i++)
        {
            union4(0, i + 1);   
            union4(size*size + 1, size*size - i);   
        }
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (matrix[i, j] == 1) {
                    if (i + 1 != size && matrix[i + 1, j] == 1) {
                        union4(i * size + j + 1, (i+1) * size + j + 1);    
                    }
                } 
                if (matrix[i, j] == 1) {
                    if (j + 1 != size && matrix[i, j + 1] == 1) {
                        union4(i * size + j + 1, i * size + j + 1 + 1);    
                    }
                } 

            }
        }

        return connected4(0, size * size + 1);
    }


    static public int root(int x)
    {
        int res = nodes[x];
        while (res != nodes[res])
            res = nodes[res];

        return res;
    }

    static public void union4(int x, int y)
    {
        int componentY = nodes[y];
        int componentX = nodes[x];
        int rootX = root(x);
        int rootY = root(y);
        if (componentY == componentX)
        {
            return;
        }
        int sizeX = sizes[rootX];
        int sizeY = sizes[rootY];

        if (sizeX > sizeY)
        {
            sizes[rootX] += sizeY;
            nodes[rootY] = nodes[rootX];
        }
        else
        {
            sizes[rootY] += sizeX;
            nodes[rootX] = rootY;
        }
    }

    static public bool connected4(int x, int y)
    {
        int rootX = root(x);
        int rootY = root(y);

        return rootX == rootY;
    }
}