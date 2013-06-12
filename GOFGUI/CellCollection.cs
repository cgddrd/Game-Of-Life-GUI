using System;

namespace GOFGUI
{
    class CellCollection
    {
        //Size of the grid (40)
        private int _size;

        //Multi-dimensional array of cells (40 x 40)
        private CellOfLife[,] _cells;

        /* 
         * Private boolean alue representing a true or false value FOR EACH cell in the grid,
         * (i.e multi-dimensional array of true/false values for each cell in the grid).
         */
        private bool[,] _nextGeneration;

        public CellCollection(int size)
        {
            _size = size;
            _cells = new CellOfLife[_size, _size];
            _nextGeneration = new bool[_size, _size];

            //Randomly select cells to become alive on the first generation (i.e. UI load)
            Random rand = new Random();
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    _cells[row, column] = new CellOfLife();
                    if (rand.NextDouble() <= 0.3)
                    {
                        _cells[row, column].IsAlive = true;
                    }
                }
            }
        }

       //Getter for '_cells' variable
       public CellOfLife this[int row, int column]
       {
          get
          {
             if (row < 0 || row >= _size ||
                column < 0 || column >= _size)
             {
                throw new ArgumentOutOfRangeException();
             }

             return _cells[row, column];
          }
       }

       public int Size
       {
          get { return _size; }
          set { _size = value; }
       }

        //Update the GOL cells for a new generation.
        public void UpdateLife()
        {
            //Loop through each cell in the grid
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    //Count the number of neighbours around the current cell.
                    int neighbors = CountNeighbors(row, column);


                    if (!_cells[row, column].IsAlive && neighbors == 3)
                    {
                        //A dead cell with three neighbors comes alive.
                        _nextGeneration[row, column] = true;
                    }
                    else if (_cells[row, column].IsAlive && 
                            (neighbors == 3 || neighbors == 2))
                    {
                        //A live cell with two or three neighbors stays alive.
                        _nextGeneration[row, column] = true;
                    }
                    else
                    {
                        //A cell dies because of loneliness or overcrowding.
                        _nextGeneration[row, column] = false;
                    }
                }
            } 

      /*  public void UpdateLife()
        {
            //Loop through each cell in the grid
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    //Count the number of neighbours around the current cell.
                    int neighbors = CountNeighbors(row, column);

                    if (!_cells[row, column].IsAlive && neighbors == 3)
                    {
                        //A dead cell with three neighbors comes alive.
                        _cells[row, column].IsAlive = true;
                    }
                    else if (_cells[row, column].IsAlive && 
                            (neighbors == 3 || neighbors == 2))
                    {
                        //A live cell with two or three neighbors stays alive.
                        _cells[row, column].IsAlive = true;
                    }
                    else
                    {
                        //A cell dies because of loneliness or overcrowding.
                        _cells[row, column].IsAlive = false;
                    }
                } 
            } */

            /* 
             * Re-allocate/set the boolean results for each of the cells 
             * using the multi-dimensional array of boolean values '_nextGeneration'.
             */
          for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    _cells[row, column].IsAlive = _nextGeneration[row, column];
                }
            } 
        }
       
        // a brute force counting that examines every neighboring cell.
        // if a cell is off the edge of the grid, the cell "on the other side"
        // is checked, i.e. the grid wraps around
        private int CountNeighbors(int row, int column)
        {
            // total up the number of neighbors who are alive
            int neighbors = 0;

            // check cell to upper left
            if (_cells[WrapMinusOne(row), WrapMinusOne(column)].IsAlive)
                neighbors++;

            //check cell above
            if (_cells[WrapMinusOne(row), column].IsAlive)
                neighbors++;

            // check cell to upper right
            if (_cells[WrapMinusOne(row), WrapPlusOne(column)].IsAlive)
                neighbors++;

            // check cell to left
            if (_cells[row, WrapMinusOne(column)].IsAlive)
                neighbors++;

            // check cell to right
            if (_cells[row, WrapPlusOne(column)].IsAlive)
                neighbors++;

            // check cell to bottom left
            if (_cells[WrapPlusOne(row), WrapMinusOne(column)].IsAlive)
                neighbors++;

            // check cell below
            if (_cells[WrapPlusOne(row), column].IsAlive)
                neighbors++;

            // check cell to bottom right
            if (_cells[WrapPlusOne(row), WrapPlusOne(column)].IsAlive)
                neighbors++;

            return neighbors;
        }

        private int WrapMinusOne(int value)
        {
            if (value == 0)
                value = _size;
            return value - 1;
        }

        private int WrapPlusOne(int value)
        {
            if (value >= _size - 1)
                value = -1;
            return value + 1;
        }

    }
}
