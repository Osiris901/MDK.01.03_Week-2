using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TrainingPractice_02.Models;

namespace TrainingPractice_02.Views
{
    internal class GridView
    {
        public readonly int StartX, StartY;
        public readonly int Columns, Rows;

        private readonly Grid _grid;
        private bool _useHighlight;

        private Dictionary<int, (bool, bool)> _cellTypeSalts;

        public GridView(Grid? model = null, int x = 0, int y = 0, int cols = Constants.GridCols, int rows = Constants.GridRows)
        {
            _grid = model ?? new Grid();
            StartX = x;
            StartY = y;
            Columns = cols;
            Rows = rows;
            _cellTypeSalts = new Dictionary<int, (bool, bool)>();
            _useHighlight = false;
        }

        public void ToggleHighlight(bool value)
        {
            _useHighlight = value;
        }

        public void Draw(Graphics g)
        {
            var rnd = new Random(_grid.seed);
            var activePen = Pens.Black;
            var gb = new SolidBrush(Color.LightGray);

            foreach (var cell in _grid.Cells)
            {
                var col = cell.Id % Columns;
                var row = cell.Id / Columns;

                var x = StartX + col * Constants.Width;
                var y = StartY + row * Constants.Height;

                g.DrawRectangle(activePen, x, y, Constants.Width, Constants.Height);

                switch (cell.ChildrenCount)
                {
                    case 2:
                    case 3:
                    case 4:
                        DrawInner(g, cell, activePen, new Point(x, y), rnd);
                        break;
                    default:
                        DrawValue(g, cell.Value?.ToString(), cell.IsActive && _useHighlight ? gb : activePen.Brush, new Point(x + Constants.Width / 2, y + Constants.Height / 2), 3f);
                        break;
                }
            }
        }

        public void ActivateAt(int o, int i)
        {
            //MessageBox.Show($"Clicked at X:{x}, Y:{y}; Tile results: o '{o}', i '{i}'");
            if (o == -1)
            {
                // Not Found!
            }
            else
            {
                if (i != -1)
                {
                    _grid.ActivateInner(o, i);
                }
                else
                {
                    _grid.Activate(o);
                }
            }
        }

        public (int, int, int) GetValueAt(int x, int y)
        {
            var (o, i) = GetIdByCoord(x, y);

            if (o == -1)
            {
                return (-1, -1, -1);
            }

            var target = _grid.Cells.ElementAt(o);

            if (i != -1)
            {
                var cell = ((IEnumerable<Cell>) target.Value).ElementAt(i);
                return (o, i, (int)cell.Value);
            }

            return (o, -1, (int)target.Value);
        }

        private void DrawValue(Graphics g, string? s, Brush brush, Point point, float sizeMultiplier = 1)
        {
            var actualSize = SystemFonts.DefaultFont.Size * sizeMultiplier;
            var font = new Font(SystemFonts.DefaultFont.FontFamily, actualSize, sizeMultiplier < 1.5f ? FontStyle.Bold : FontStyle.Regular);

            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(s, font, brush, point.X, point.Y, sf);
        }

        private void DrawInner(Graphics g, Cell cell, Pen pen, Point cellStart, Random rnd)
        {
            var inner = (IEnumerable<Cell>) cell.Value;
            var gb = new SolidBrush(Color.LightGray);

            if (cell.ChildrenCount is < 2 or > 4)
            {
                return;
            }

            // { True - Vertical; False - Horizontal }
            var primaryOrientation = rnd.Next(10) % 2 == 0;
            // { True - First Half; False - Second Half }
            var secondaryOrientation = rnd.Next(10) % 2 == 0;

            if (_cellTypeSalts.ContainsKey(cell.Id) == false)
            {
                _cellTypeSalts.Add(cell.Id, (primaryOrientation, secondaryOrientation));
            }

            if (cell.ChildrenCount == 4)
            {
                g.DrawLine(pen, cellStart.X, cellStart.Y + Constants.Height / 2, cellStart.X + Constants.Width, cellStart.Y + Constants.Height / 2);
                g.DrawLine(pen, cellStart.X + Constants.Width / 2, cellStart.Y, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height);
                DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + Constants.Height / 4));
                DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + Constants.Height / 4));
                DrawValue(g, inner.ElementAt(2).Value?.ToString(), inner.ElementAt(2).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y - 4 + (Constants.Width * 3) / 4));
                DrawValue(g, inner.ElementAt(3).Value?.ToString(), inner.ElementAt(3).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y - 4 + (Constants.Width * 3) / 4));
                return;
            }

            if (primaryOrientation)
            {
                g.DrawLine(pen, cellStart.X, cellStart.Y + Constants.Height / 2, cellStart.X + Constants.Width, cellStart.Y + Constants.Height / 2);

                if (cell.ChildrenCount == 2)
                {
                    DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height / 4), 1.3f);
                    DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 2, cellStart.Y + (Constants.Height * 3) / 4), 1.3f);
                    return;
                }

                if (secondaryOrientation)
                {
                    g.DrawLine(pen, cellStart.X + Constants.Width / 2, cellStart.Y, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height / 2);
                    DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + Constants.Height / 4));
                    DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + Constants.Height / 4));
                    DrawValue(g, inner.ElementAt(2).Value?.ToString(), inner.ElementAt(2).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 2, cellStart.Y + (Constants.Height * 3) / 4), 1.3f);
                }
                else
                {
                    g.DrawLine(pen, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height / 2, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height);
                    DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height / 4), 1.3f);
                    DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + (Constants.Height * 3) / 4));
                    DrawValue(g, inner.ElementAt(2).Value?.ToString(), inner.ElementAt(2).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + (Constants.Height * 3) / 4));
                }
            }
            else
            {
                g.DrawLine(pen, cellStart.X + Constants.Width / 2, cellStart.Y, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height);

                if (cell.ChildrenCount == 2)
                {
                    DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + Constants.Height / 2), 1.3f);
                    DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + Constants.Height / 2), 1.3f);
                    return;
                }
                
                if (secondaryOrientation)
                {
                    g.DrawLine(pen, cellStart.X, cellStart.Y + Constants.Height / 2, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height / 2);
                    DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + Constants.Height / 4));
                    DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + (Constants.Height * 3) / 4));
                    DrawValue(g, inner.ElementAt(2).Value?.ToString(), inner.ElementAt(2).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + Constants.Height / 2), 1.3f);
                }
                else
                {
                    g.DrawLine(pen, cellStart.X + Constants.Width / 2, cellStart.Y + Constants.Height / 2, cellStart.X + Constants.Width, cellStart.Y + Constants.Height / 2);
                    DrawValue(g, inner.ElementAt(0).Value?.ToString(), inner.ElementAt(0).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + Constants.Width / 4, cellStart.Y + Constants.Height / 2), 1.3f);
                    DrawValue(g, inner.ElementAt(1).Value?.ToString(), inner.ElementAt(1).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + Constants.Height / 4));
                    DrawValue(g, inner.ElementAt(2).Value?.ToString(), inner.ElementAt(2).IsActive && _useHighlight ? gb : pen.Brush, new Point(cellStart.X + (Constants.Width * 3) / 4, cellStart.Y + (Constants.Height * 3) / 4));
                }
            }
        }

        private (int, int) GetIdByCoord(int x, int y)
        {
            if (x < StartX || y < StartY || x > Constants.Width * Columns + StartX || y > Constants.Height * Rows + StartY)
            {
                return (-1, -1);
            }

            var col = ((x - StartX) / Constants.Width) + 1;
            var row = ((y - StartY) / Constants.Height) + 1;

            var id = (row - 1) * Columns + col - 1;
            var cell = _grid.Cells.ElementAtOrDefault(id);
            if (cell != null)
            {
                if (cell.ChildrenCount > 1)
                {
                    if (_cellTypeSalts.TryGetValue(id, out var orientations))
                    {
                        var (po, so) = orientations;
                        var q = GetInnerIdByCoord(col, row, x, y);
                        var count = cell.ChildrenCount;

                        if (count == 4)
                        {
                            return (id, q);
                        }

                        if (count == 2)
                        {
                            if (po)
                            {
                                switch (q)
                                {
                                    case 0 or 1:
                                        return (id, 0);
                                    case 2 or 3:
                                        return (id, 1);
                                }
                            }

                            switch (q)
                            {
                                case 0 or 2:
                                    return (id, 0);
                                case 1 or 3:
                                    return (id, 1);
                            }
                        }
                        
                        if (count == 3)
                        {
                            if (po)
                            {
                                if (so)
                                {
                                    switch (q)
                                    {
                                        case 0 or 1:
                                            return (id, q);
                                        case 2 or 3:
                                            return (id, 2);
                                    }
                                }

                                switch (q)
                                {
                                    case 0 or 1:
                                        return (id, 0);
                                    case 2:
                                        return (id, 1);
                                    case 3:
                                        return (id, 2);
                                }
                            }

                            if (so)
                            {
                                switch (q)
                                {
                                    case 0:
                                        return (id, 0);
                                    case 2:
                                        return (id, 1);
                                    case 1 or 3:
                                        return (id, 2);
                                }
                            }

                            switch (q)
                            {
                                case 0 or 2:
                                    return (id, 0);
                                case 1:
                                    return (id, 1);
                                case 3:
                                    return (id, 2);
                            }
                        }
                    }
                }

                return (id, -1);
            }

            return (-1, -1);
        }

        private int GetInnerIdByCoord(int col, int row, int x, int y)
        {
            var relX = StartX + (col - 1) * Constants.Width;
            var relY = StartY + (row - 1) * Constants.Height;

            var id = x - relX > Constants.Width / 2 ? 1 : 0;
            return y - relY > Constants.Height / 2 ? id + 2 : id;
        }
    }
}
