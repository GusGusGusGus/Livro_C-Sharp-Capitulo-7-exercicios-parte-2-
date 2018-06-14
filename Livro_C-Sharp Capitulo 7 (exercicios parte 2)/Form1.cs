using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Livro_C_Sharp_Capitulo_7__exercicios_parte_2_
{
    public partial class Form1 : Form
    {
        private int sortColumn = -1;


        public Form1()
        {
            InitializeComponent();
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ColumnClick(object sender,
                           System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listView1.Sorting);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listView1.Columns.Add("ID");
            listView1.Columns.Add("Nome");
            listView1.Columns.Add("Departamento");

            DataClasses1DataContext dc = new DataClasses1DataContext();
            var lista = from Funcionarios in dc.Funcionarios select Funcionarios;
            foreach (Funcionarios func in lista)
            {
                ListViewItem item;
                item = listView1.Items.Add(func.ID.ToString());
                item.SubItems.Add(func.Nome);
                item.SubItems.Add(func.Departamento);
            }
            for (int idx = 0; idx <= 2; idx++) {
                listView1.Columns[idx].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        // Implements the manual sorting of items by column.
        class ListViewItemComparer : System.Collections.IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                // Determine whether the sort order is descending.
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;
                return returnVal;
            }
        }
    }
}
