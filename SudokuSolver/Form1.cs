using System.Collections;
namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        List<List<int>> grid = new List<List<int>>();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            grid.Clear();
            grid = getGrid();
            if (checkGrid())
            {
                label1.Text = "";
                solve();
                show();
            }
            else
            {
                label1.Text = "Data Error!";
            }
        }
        private List<List<int>> getGrid()
        {
            int x = 0;
            int y = -1;
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (x == 0 || x == 9)
                    {
                        x = 0;
                        y++;
                        grid.Add(new List<int>());
                    }
                    if (!String.IsNullOrWhiteSpace(c.Text) && !String.IsNullOrEmpty(c.Text))
                    {
                        int z;
                        if (int.TryParse(c.Text, out z))
                        {

                            grid[y].Add(z);
                        }
                        else
                        {
                            grid[y].Add(0);
                        }

                    }
                    else
                    {
                        grid[y].Add(0);
                    }
                    x++;
                }
            }
            label1.Text = "";
            return grid;
        }
        private bool possible(int x, int y, int n)
        {
            for (int z = 0; z < 9; z++)
            {
                if (grid[y][z] == n)
                {
                    return false;
                }
            }


            for (int z = 0; z < 9; z++)
            {
                if (grid[z][x] == n)
                {
                    return false;
                }
            }


            int x0 = Decimal.ToInt32(x / 3);
            int y0 = Decimal.ToInt32(y / 3);
            x0 *= 3;
            y0 *= 3;
            for (int z = 0; z < 3; z++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (grid[y0 + z][x0 + k] == n)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool solve()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (grid[y][x] == 0)
                    {
                        for (int num = 1; num < 10; num++)
                        {
                            if (possible(x, y, num))
                            {
                                grid[y][x] = num;
                                if (solve())
                                {
                                    return solve();
                                }
                                else
                                {
                                    grid[y][x] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        private void show()
        {
            int y = 0;
            int x = 0;
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (x == 9)
                    {
                        x = 0;
                        y++;
                    }
                    c.Text = (grid[y][x]).ToString();
                    x++;
                }
            }
        }
        private bool checkGrid()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (grid[y][x] != 0)
                    {
                        int num = grid[y][x];
                        if (!check(x, y, num))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool check(int x, int y, int num)
        {
            int res = 0;
            for (int z = 0; z < 9; z++)
            {
                if (grid[y][z] == num)
                {
                    res++;
                }
            }


            for (int z = 0; z < 9; z++)
            {
                if (grid[z][x] == num)
                {
                    res++;
                }
            }


            int x0 = Decimal.ToInt32(x / 3);
            int y0 = Decimal.ToInt32(y / 3);
            x0 *= 3;
            y0 *= 3;
            for (int z = 0; z < 3; z++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (grid[y0 + z][x0 + k] == num)
                    {
                        res++;
                    }
                }
            }


            if (res > 3)
            {
                return false;
            }
            return true;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            grid.Clear();
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
    }
}