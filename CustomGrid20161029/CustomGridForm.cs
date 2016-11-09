using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomGrid20161029
{
    public partial class CustomGridForm : Form
    {
        

        public CustomGridForm()
        {
            

            InitializeComponent();
            init();
            /*
            grid.Rows.Add(new string[] { "" });
            grid.Rows.Add(new string[] { "" });
            grid.Rows.Add(new string[] { "" });
            grid.Rows.Add(new string[] { "" });
            */
            adjustColumnWidthBasedOnColumnCount();

            setFontSize(15);

            return;
        }

        public void setFontSize(int size)
        {
            grid.Font = new Font(grid.Font.FontFamily, size);
        }

        public void setState(int userid, User.UserState state)
        {
            if (userid < User.getUserCount())
            {
                User.setUserState(userid, state);
                selectedColumn = userid % grid.ColumnCount;
                selectedRow = getRowCount(userid + 1, grid.ColumnCount) - 1;
                changeSelectedCellState(state);
            }
        }
        private void init()
        {
            this.conMenu.Items.Clear();
            ToolStripMenuItem menuitem = null;
            //
            // add/remove menu items
            //
            menuitem = new ToolStripMenuItem("Add", null, menuitem_add_Click);
            menuitem.Size = new System.Drawing.Size(117, 22);
            this.conMenu.Items.Add(menuitem);


            //menuitem = new ToolStripSeparator();
            //menuitem.Size = new System.Drawing.Size(114, 6);
            this.conMenu.Items.Add(new ToolStripSeparator());

            menuitem = new ToolStripMenuItem("Remove", null, menuitem_remove_Click);
            menuitem.Size = new System.Drawing.Size(117, 22);
            this.conMenu.Items.Add(menuitem);

            //
            // add state menu items
            //

            int index = -1;
            foreach (object obj in Enum.GetValues(typeof(User.UserState)))
            {
                index++;
                if (index == 0) 
                    continue;
                menuitem = new ToolStripMenuItem(obj.ToString(), null, menuitem_Click);
                menuitem.Size = new System.Drawing.Size(117, 22);
                menuitem.Tag = index;
                this.conMenu.Items.Add(menuitem);
            }


            //this.conMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {menuitem});
            this.conMenu.Size = new System.Drawing.Size(118, 120);

            CellHeight = 30;
            //grid.Rows[0].DefaultCellStyle.BackColor = ROW_COLOR;
            adjustColumnCount();


            addUserLast("Helmus", User.UserState.A);
            addUserLast("Sharlotte", User.UserState.B);
            addUserLast("Chris", User.UserState.C);
            addUserLast("Roman", User.UserState.D);
            addUserLast(User.UserState.E);
            addUserLast("Peri", User.UserState.E);
            addUserLast("Ougus", User.UserState.E);


        }


        void menuitem_Click(object sender, EventArgs e)
        {
            setState(selectedRow*grid.ColumnCount+selectedColumn,(User.UserState)(int.Parse( ((ToolStripItem)sender).Tag.ToString())));
        }

        Color ROW_COLOR = Color.AntiqueWhite;
        private int cellHeight = 80;
        public int CellHeight
        {
            get { return cellHeight; }
            set
            {
                cellHeight = value;
                for (int i = 0; i < grid.RowCount; i++)
                    grid.Rows[i].Height = cellHeight;
            }
        }

        private int minimumCellWidth = 200;
        public int MinimumCellWidth
        {
            get
            {
                return minimumCellWidth;
            }
            set
            {
                minimumCellWidth = value;
                adjustColumnCount();
                adjustColumnWidthBasedOnColumnCount();
            }
        }
        private Point getPos(int oldRow, int oldColumn, int newRow, int NewColumn, int row, int col)
        {
            int a, b = 0;

            int n = row * oldColumn + col;

            a = n / newRow;
            b = n % NewColumn;

            return new Point(a, b);
        }
        private int getRowCount(int userCount, int columns)
        {
            return  userCount / columns + Math.Sign(userCount % columns);
        }
        public int GridColumnCount
        {
            get { return grid.ColumnCount; }
            set
            {
                int dCount = value - GridColumnCount;
                bool changedColumns = true;
                if (dCount > 0)
                {
                    int rowcount = 0;
                    rowcount = getRowCount(User.getUserCount(), value);
                    for (int i = 0; i < dCount; i++)
                    {
                        grid.Columns.Add(new DataGridViewRolloverCellColumn());
                    }

                    //remove rows
                    for (int i = grid.Rows.Count; i > rowcount; i--)
                    {
                        grid.Rows.RemoveAt(i - 1);
                    }
                }
                else if (dCount < 0)
                {
                    int rowcount = 0;
                    rowcount = getRowCount(User.getUserCount(), value);

                    for (int i = dCount; i < 0; i++)
                    {
                        grid.Columns.RemoveAt(grid.ColumnCount - 1);
                    }
                    // add row
                    for (int i = grid.Rows.Count; i < rowcount; i++)
                    {
                        grid.Rows.Add("");
                    }
                }
                else
                {
                    changedColumns = false;
                    //return;
                }


                paintGrid();
            }
        }

        private void paintGrid()
        {
            //
            // adjustment cell state
            //
            int userid = -1;
            for (int row = 0; row < grid.RowCount; row++)
            {

                for (int col = 0; col < grid.ColumnCount; col++)
                {
                    userid++;
                    User.UserState cellState = User.UserState.NONE;
                    string cellName = "";
                    if (userid < User.getUserCount())
                    {
                        cellState = User.getUser(userid).state;
                        cellName = User.getUser(userid).name;
                        ((DataGridViewRolloverCell)grid.Rows[row].Cells[col]).ReadOnly = false;
                    }
                    else
                    {
                        ((DataGridViewRolloverCell)grid.Rows[row].Cells[col]).ReadOnly = true;
                    }
                    ((DataGridViewRolloverCell)grid.Rows[row].Cells[col]).setUserState(cellState);
                    ((DataGridViewRolloverCell)grid.Rows[row].Cells[col]).Value = cellName;

                }
            }
            grid.Refresh();
        }

        

        private void adjustColumnWidthBasedOnColumnCount()
        {
            //
            // adjustment column width
            //
            int cellWidth = grid.Width / GridColumnCount - 2;
            for (int i = 0; i < grid.ColumnCount; i++)
            {
                grid.Columns[i].Width = cellWidth;
            }
        }


        private void adjustColumnCount()
        {
            //
            // adjust Column Count
            //
            int columnCount = Math.Max(1,grid.Width / MinimumCellWidth);
            if (columnCount > 0 && columnCount != grid.ColumnCount)
            { 
                GridColumnCount = columnCount; 
            }
            //
            // repaint user state
            //
            /*
            int userid = -1;
            for (int row = 0; row < grid.RowCount; row++)
            {

                for (int col = 0; col < grid.ColumnCount; col++)
                {
                    userid++;
                    Color cellColor = User.nonColor;
                    if (userid < User.getUserCount())
                    {
                        cellColor = User.stateColors[(int)User.getUser(userid).state];
                    }
                    ((DataGridViewRolloverCell)grid.Rows[row].Cells[col]).stateColor = cellColor;
                }
            }
            grid.Refresh();
            */
        }

        private void grid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.HeaderText = "" + e.Column.Index;
            e.Column.Width = MinimumCellWidth;
        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            grid.Rows[e.RowIndex].Height = cellHeight;
            grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = ROW_COLOR;
        }

        public ContextMenuStrip getConMenu()
        {
            return conMenu;
        }
        public DataGridView GridView()
        {
            return grid;
        }

        

        int selectedRow, selectedColumn;
        public void setSelectedCell(int row, int col)
        {
            selectedRow = row;
            selectedColumn = col;
        }
        private void state3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setState(selectedRow*grid.ColumnCount+selectedColumn,(User.UserState)3);
        }

        private void changeSelectedCellState(User.UserState state)
        {
            ((DataGridViewRolloverCell)grid.Rows[selectedRow].Cells[selectedColumn]).setUserState(state);
            grid.Refresh();
        }

        private void state2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setState(selectedRow*grid.ColumnCount+selectedColumn, (User.UserState)2);
        }

        private void state1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setState(selectedRow*grid.ColumnCount+selectedColumn,(User.UserState)1);
        }




        private void btnAddClick(object sender, EventArgs e)
        {
            addUserLast();
        }
        public void addUser(User user){
            User.add(user);

            if (User.getUserCount() > grid.RowCount * grid.ColumnCount)
            {
                grid.Rows.Add("");

            }
            paintGrid();
            adjustColumnCount();
        }
        public void addUserLast()
        {
            addUser(new User());
        }
        private void addUserLast(string name, User.UserState state)
        {
            User user = new User();
            user.name = name;
            user.state = state;
            addUser(user);
            
        }
        private void addUserLast(User.UserState state)
        {
            User user = new User();
            user.state = state;
            addUser(user);
        }

        public void removeUser(int userid)
        {
            User.remove(userid);

            if (User.getUserCount() <= (grid.RowCount - 1) * grid.ColumnCount)
            {
                grid.Rows.RemoveAt(grid.RowCount - 1);
            }
            paintGrid();
            adjustColumnCount();
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                User.setUsername(e.ColumnIndex + e.RowIndex * grid.ColumnCount, grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                grid.ClearSelection();
                grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                setSelectedCell(e.RowIndex, e.ColumnIndex);
                if (selectedColumn + selectedRow * grid.ColumnCount < User.getUserCount())
                    conMenu.Show(this.grid, new Point(contextMenuLocation.X, contextMenuLocation.Y));
            }
            
            base.OnMouseClick(e);
        }
        Point contextMenuLocation = new Point();
        private void grid_MouseClick(object sender, MouseEventArgs e)
        {

            contextMenuLocation = e.Location;
            
        }

        private void grid_SizeChanged(object sender, EventArgs e)
        {
            adjustColumnCount();
            adjustColumnWidthBasedOnColumnCount();
        }


        private void menuitem_remove_Click(object sender, EventArgs e)
        {
            removeUser(selectedColumn+selectedRow*grid.ColumnCount);
        }

        private void menuitem_add_Click(object sender, EventArgs e)
        {
            addUserLast();
        }
        
    }
    public class User
    {
        //
        // should be matched with UserState & stateColors
        //
        public enum UserState { NONE, A, B, C, D, E, F, G, H };
        public static Color[] stateColors = new Color[] { nonColor, Color.White, Color.Red, Color.Yellow, Color.Blue, Color.Magenta, Color.Brown, Color.Honeydew, Color.Indigo, Color.Lavender, Color.Beige };

        private static List<User> users = new List<User>();
        public static void add(User user)
        {
            users.Add(user);
        }
        public static int getUserCount()
        {
            return users.Count;
        }
        public string name = "[Anonymous]";

        public UserState state = UserState.A;
        public static Color nonColor = Color.White;

        internal static User getUser(int userid)
        {
            return users[userid];
        }

        internal static void setUserState(int userid, UserState state)
        {
            users[userid].state = state;
        }

        internal static void setUsername(int userid, string name)
        {
            users[userid].name = name;
        }

        internal static void remove(int userid)
        {
            users.RemoveAt(userid);
        }
    }
    public class DataGridViewRolloverCell : DataGridViewTextBoxCell
    {
        private User.UserState userState = User.UserState.NONE;
        public void setUserState(User.UserState state)
        {
            userState = state;
            if ((int)state >= Enum.GetValues(typeof(User.UserState)).Length)
            {
                int a = 1;
            }
        }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value, //overide
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            
            // Call the base class method to paint the default cell appearance.
            if (this.userState == User.UserState.NONE)
            {
                value = null;
                formattedValue = null;

                DataGridViewCellStyle newCellStyle = new DataGridViewCellStyle(cellStyle);
                newCellStyle.BackColor = this.DataGridView.BackgroundColor;

                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                value, null, errorText, newCellStyle,
                advancedBorderStyle, paintParts);
                
                return;
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                value, null, errorText, cellStyle,
                advancedBorderStyle, paintParts);

            

            // Retrieve the client location of the mouse pointer.

            Point cursorPosition = this.DataGridView.PointToClient(Cursor.Position);
            /*
            // If the mouse pointer is over the current cell, draw a custom border.
            if (cellBounds.Contains(cursorPosition))
            {
                Rectangle newRect = new Rectangle(cellBounds.X + 1,
                    cellBounds.Y + 1, cellBounds.Width - 4,
                    cellBounds.Height - 4);
                graphics.DrawRectangle(Pens.DarkBlue, newRect);
            }
            */

            int margin = 5;
            Rectangle newRect = new Rectangle(cellBounds.X + margin,
                    cellBounds.Y + margin, cellBounds.Height - margin * 2,
                    cellBounds.Height - margin * 2);
            graphics.FillEllipse(new SolidBrush(User.stateColors[(int)userState]), newRect);
            graphics.DrawEllipse(Pens.Black, newRect);


            //
            // draw value
            //
            bool paint = true;
            bool computeContentBounds = false;

            Rectangle borderWidths = BorderWidths(advancedBorderStyle);
            Rectangle valBounds = new Rectangle(cellBounds.X + cellBounds.Height,
                    cellBounds.Y, cellBounds.Width - cellBounds.Height ,
                    cellBounds.Height); 
            
            valBounds.Offset(borderWidths.X, borderWidths.Y);
            valBounds.Width -= borderWidths.Right;
            valBounds.Height -= borderWidths.Bottom;

            SolidBrush br;
            Point ptCurrentCell = this.DataGridView.CurrentCellAddress;
            bool cellCurrent = ptCurrentCell.X == this.ColumnIndex && ptCurrentCell.Y == rowIndex;
            bool cellEdited = cellCurrent && this.DataGridView.EditingControl != null;
            bool cellSelected = (cellState & DataGridViewElementStates.Selected) != 0;
            Rectangle errorBounds = valBounds;
            string formattedString = formattedValue as string;

            
            byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginLeft = 0;
            byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginRight = 0;
            
            byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithWrapping = 1;
            byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithoutWrapping = 2;
            byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginBottom = 1;


            Rectangle resultBounds = Rectangle.Empty;

            if (formattedString != null && ((paint && !cellEdited) || computeContentBounds))
            {
                // Font independent margins
                int verticalTextMarginTop = cellStyle.WrapMode == DataGridViewTriState.True ? DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithWrapping : DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithoutWrapping;
                valBounds.Offset(DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginLeft, verticalTextMarginTop);
                valBounds.Width -= DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginLeft + DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginRight;
                valBounds.Height -= verticalTextMarginTop + DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginBottom;
                if (valBounds.Width > 0 && valBounds.Height > 0)
                {
                    TextFormatFlags flags = ComputeTextFormatFlagsForCellStyleAlignment(this.DataGridView.RightToLeft == RightToLeft.Yes, cellStyle.Alignment, cellStyle.WrapMode);
                    if (paint)
                    {
                        if (PaintContentForeground(paintParts))
                        {
                            if ((flags & TextFormatFlags.SingleLine) != 0)
                            {
                                flags |= TextFormatFlags.EndEllipsis;
                            }
                            TextRenderer.DrawText(graphics,
                                formattedString,
                                cellStyle.Font,
                                valBounds,
                                cellSelected ? cellStyle.SelectionForeColor : cellStyle.ForeColor,
                                flags);
                        }
                    }
                    else
                    {
                        resultBounds = GetTextBounds(valBounds, formattedString, flags, cellStyle);
                    }
                }
            }
        }
        internal static bool PaintContentForeground(DataGridViewPaintParts paintParts)
        {
            return (paintParts & DataGridViewPaintParts.ContentForeground) != 0;
        }
        internal static Rectangle GetTextBounds(Rectangle cellBounds,
                                                string text,
                                                TextFormatFlags flags,
                                                DataGridViewCellStyle cellStyle)
        {
            return GetTextBounds(cellBounds, text, flags, cellStyle, cellStyle.Font);
        }

        internal static Rectangle GetTextBounds(Rectangle cellBounds,
                                                string text,
                                                TextFormatFlags flags,
                                                DataGridViewCellStyle cellStyle,
                                                Font font)
        {
            if ((flags & TextFormatFlags.SingleLine) != 0)
            {
                Size sizeRequired = TextRenderer.MeasureText(text, font, new Size(System.Int32.MaxValue, System.Int32.MaxValue), flags);
                if (sizeRequired.Width > cellBounds.Width)
                {
                    flags |= TextFormatFlags.EndEllipsis;
                }
            }

            Size sizeCell = new Size(cellBounds.Width, cellBounds.Height);
            Size sizeConstraint = TextRenderer.MeasureText(text, font, sizeCell, flags);
            if (sizeConstraint.Width > sizeCell.Width)
            {
                sizeConstraint.Width = sizeCell.Width;
            }
            if (sizeConstraint.Height > sizeCell.Height)
            {
                sizeConstraint.Height = sizeCell.Height;
            }
            if (sizeConstraint == sizeCell)
            {
                return cellBounds;
            }
            return new Rectangle(GetTextLocation(cellBounds, sizeConstraint, flags, cellStyle), sizeConstraint);
        }
        internal static Point GetTextLocation(Rectangle cellBounds,
                                              Size sizeText,
                                              TextFormatFlags flags,
                                              DataGridViewCellStyle cellStyle)
        {
            Point ptTextLocation = new Point(0, 0);

            // now use the alignment on the cellStyle to determine the final text location
            DataGridViewContentAlignment alignment = cellStyle.Alignment;
            if ((flags & TextFormatFlags.RightToLeft) != 0)
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.TopLeft:
                        alignment = DataGridViewContentAlignment.TopRight;
                        break;

                    case DataGridViewContentAlignment.TopRight:
                        alignment = DataGridViewContentAlignment.TopLeft;
                        break;

                    case DataGridViewContentAlignment.MiddleLeft:
                        alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case DataGridViewContentAlignment.MiddleRight:
                        alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;

                    case DataGridViewContentAlignment.BottomLeft:
                        alignment = DataGridViewContentAlignment.BottomRight;
                        break;

                    case DataGridViewContentAlignment.BottomRight:
                        alignment = DataGridViewContentAlignment.BottomLeft;
                        break;
                }
            }

            switch (alignment)
            {
                case DataGridViewContentAlignment.TopLeft:
                    ptTextLocation.X = cellBounds.X;
                    ptTextLocation.Y = cellBounds.Y;
                    break;

                case DataGridViewContentAlignment.TopCenter:
                    ptTextLocation.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
                    ptTextLocation.Y = cellBounds.Y;
                    break;

                case DataGridViewContentAlignment.TopRight:
                    ptTextLocation.X = cellBounds.Right - sizeText.Width;
                    ptTextLocation.Y = cellBounds.Y;
                    break;

                case DataGridViewContentAlignment.MiddleLeft:
                    ptTextLocation.X = cellBounds.X;
                    ptTextLocation.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
                    break;

                case DataGridViewContentAlignment.MiddleCenter:
                    ptTextLocation.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
                    ptTextLocation.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
                    break;

                case DataGridViewContentAlignment.MiddleRight:
                    ptTextLocation.X = cellBounds.Right - sizeText.Width;
                    ptTextLocation.Y = cellBounds.Y + (cellBounds.Height - sizeText.Height) / 2;
                    break;

                case DataGridViewContentAlignment.BottomLeft:
                    ptTextLocation.X = cellBounds.X;
                    ptTextLocation.Y = cellBounds.Bottom - sizeText.Height;
                    break;

                case DataGridViewContentAlignment.BottomCenter:
                    ptTextLocation.X = cellBounds.X + (cellBounds.Width - sizeText.Width) / 2;
                    ptTextLocation.Y = cellBounds.Bottom - sizeText.Height;
                    break;

                case DataGridViewContentAlignment.BottomRight:
                    ptTextLocation.X = cellBounds.Right - sizeText.Width;
                    ptTextLocation.Y = cellBounds.Bottom - sizeText.Height;
                    break;

                default:
                    //Debug.Assert(cellStyle.Alignment == DataGridViewContentAlignment.NotSet, "this is the only alignment left");
                    break;
            }
            return ptTextLocation;
        }
        internal static TextFormatFlags ComputeTextFormatFlagsForCellStyleAlignment(bool rightToLeft,
                                                                                    DataGridViewContentAlignment alignment,
                                                                                    DataGridViewTriState wrapMode)
        {
            TextFormatFlags tff;
            switch (alignment)
            {
                case DataGridViewContentAlignment.TopLeft:
                    tff = TextFormatFlags.Top;
                    if (rightToLeft)
                    {
                        tff |= TextFormatFlags.Right;
                    }
                    else
                    {
                        tff |= TextFormatFlags.Left;
                    }
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    tff = TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case DataGridViewContentAlignment.TopRight:
                    tff = TextFormatFlags.Top;
                    if (rightToLeft)
                    {
                        tff |= TextFormatFlags.Left;
                    }
                    else
                    {
                        tff |= TextFormatFlags.Right;
                    }
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    tff = TextFormatFlags.VerticalCenter;
                    if (rightToLeft)
                    {
                        tff |= TextFormatFlags.Right;
                    }
                    else
                    {
                        tff |= TextFormatFlags.Left;
                    }
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    tff = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    tff = TextFormatFlags.VerticalCenter;
                    if (rightToLeft)
                    {
                        tff |= TextFormatFlags.Left;
                    }
                    else
                    {
                        tff |= TextFormatFlags.Right;
                    }
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    tff = TextFormatFlags.Bottom;
                    if (rightToLeft)
                    {
                        tff |= TextFormatFlags.Right;
                    }
                    else
                    {
                        tff |= TextFormatFlags.Left;
                    }
                    break;
                case DataGridViewContentAlignment.BottomCenter:
                    tff = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    tff = TextFormatFlags.Bottom;
                    if (rightToLeft)
                    {
                        tff |= TextFormatFlags.Left;
                    }
                    else
                    {
                        tff |= TextFormatFlags.Right;
                    }
                    break;
                default:
                    tff = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    break;
            }
            if (wrapMode == DataGridViewTriState.False)
            {
                tff |= TextFormatFlags.SingleLine;
            }
            else
            {
                //tff |= TextFormatFlags.NoFullWidthCharacterBreak;  VSWhidbey 518422
                tff |= TextFormatFlags.WordBreak;
            }
            tff |= TextFormatFlags.NoPrefix;
            tff |= TextFormatFlags.PreserveGraphicsClipping;
            if (rightToLeft)
            {
                tff |= TextFormatFlags.RightToLeft;
            }
            return tff;
        }


        // Force the cell to repaint itself when the mouse pointer enters it.
        protected override void OnMouseEnter(int rowIndex)
        {
            //this.DataGridView.InvalidateCell(this);
        }

        // Force the cell to repaint itself when the mouse pointer leaves it.
        protected override void OnMouseLeave(int rowIndex)
        {
            //this.DataGridView.InvalidateCell(this);
        }
        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            
            base.OnMouseClick(e);
        }
        public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            //if (userState == User.UserState.NONE)
           
            //    cellBounds = new Rectangle(0, 0, 0, 0);
            
            //else
            
                base.PositionEditingControl(setLocation, setSize, cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
        }

    }

    public class DataGridViewRolloverCellColumn : DataGridViewColumn
    {
        public DataGridViewRolloverCellColumn()
        {
            this.CellTemplate = new DataGridViewRolloverCell();
        }
    }
}