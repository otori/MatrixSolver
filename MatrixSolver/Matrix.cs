using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSolver
{
    class Matrix
    {
        public uint m {get; private set;}
        public uint n {get; private set;}
        public double[,] _matrix {get;set;}
        private bool isB;
    
        public Matrix(uint m, uint n, bool isB)
        {
            if (isB) n++;
            this.m = m;
            this.n = n;            
            this.isB = isB;
            _matrix = new double[m , n];

            for(int mm = 0; mm < m; mm++)
            {
                for(int nn = 0; nn < n; nn++)    
                {
                    _matrix[mm, nn] = .0;
                }
            }
        }

        private uint GetHeadPos(uint line)
        {
            if (line >= m) return 0;
            for(uint i = 0; i < n; i++)
            {
                if (_matrix[line,i] != .0)
                    return i;
            }
            return n-1;
        }

        private void ChangeLines(uint l1, uint l2)
        {
            if (l1 >= m || l2 >= m) return;
            if (l1 == l2) return;

            double tmp;

            for (int i = 0; i < n; i++)
            { 
                tmp = _matrix[l1, i];
                _matrix[l1, i] = _matrix[l2, i];
                _matrix[l2, i] = tmp;
            }
        }

        private void MultLine(uint line, double d)
        {
            if (line >= m) return;
            for (int i = 0; i < n; i++)
            {
                _matrix[line, i] *= d;
            }
        }

        private void LineAdd(uint l1, uint l2, uint lRes, double f1, double f2)
        {
            if (l1 >= m || l2 >= m) return;
            if (l1 == l2) return;
            if (lRes != l1 && lRes != l2) return;

            for(int i = 0; i < n; i++)
            {
                _matrix[lRes, i] = _matrix[l1, i] * f1 + _matrix[l2, i] * f2; 
            }
        }

        private void SortLines(uint start)
        {
            for (uint iM = m - 1; iM > start; iM--)
            {
                for (uint i = start; i < iM - 1; i++)
                {
                    if (GetHeadPos(i) < GetHeadPos(i + 1))
                    {
                        ChangeLines(i, i + 1);
                    }
                }
            }
        }

        public void ToNZSF()
        {
            //Phase 1:
            for (uint i = 0; i < m-1; i++)
            {
                SortLines(i);
                uint curHead = GetHeadPos(i);
                double curHeadVal = _matrix[i, curHead];

                for (uint ii = i + 1; ii < m; ii++)
                {
                    LineAdd(ii, i, ii, curHeadVal, -_matrix[ii, curHead]);
                }
            }
            //Phase 2:
            for (uint i = 1; i < m; i++)
            {
                uint curHead = GetHeadPos(i);
                double curHeadVal = _matrix[i, curHead];

                for (uint ii = 0; ii < i; ii++)
                {
                    LineAdd(ii, i, ii, curHeadVal, -_matrix[ii, curHead]);
                }
            }
            //Phase 3:
            for (uint i = 0; i < m; i++)
            {
                uint curHead = GetHeadPos(i);
                double curHeadVal = _matrix[i, curHead];

                if (curHeadVal != 0)
                { 
                    MultLine(i, 1.0/curHeadVal);
                }
            }
        }

    }
}
