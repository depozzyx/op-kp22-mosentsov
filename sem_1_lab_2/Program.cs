using System;

class Percolation {
    enum CellState
    {
        Closed = 0,
        Open = 1, 
    }
    static int[,] intialMatrix;

    // creates n-by-n grid, with all sites initially blocked
    static void init(int n) 
    {
        intialMatrix = new int[(int) CellState.Closed, (int) CellState.Closed];
    }

    // opens the site (row, col) if it is not open already
    static void open(int row, int col)
    {
    }


    // is the site (row, col) open?
    static bool isOpen(int row, int col)
    {
        return true;
    }

    // is the site (row, col) full?
    static bool isFull(int row, int col)
    {
        return true;
    }

    // returns the number of open sites
    static int numberOfOpenSites()
    {
        return 1;
    }

    // does the system percolate?
    static bool percolates()
    {
        return true;
    }

    // prints the matrix on the screen
    // The method should display different types of sites (open, blocked, full)
    static void print()
    {}

    // test client (optional)
    static void Main(String[] args)
    {
        init(4);
    }
}
