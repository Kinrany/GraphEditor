using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication
{
    class FileLoader
    {
        public static void LoadMatrix(string path, DataGridView DataGridMatrix)
        {
            int counter = 0;
            string line;
            int size = 0;
            string tmp = null;

            DataGridMatrix.Rows.Clear();
            DataGridMatrix.Refresh();

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            line = file.ReadLine();
            size = int.Parse(line);
            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if (line == "Edge colors:")
                {
                    break;
                }
                tmp += line + " ";
            }

            string[] nums = tmp.Split(' ');
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = int.Parse(nums[j + size * i]);
                }
            }

            var rowCount = matrix.GetLength(0);
            var rowLength = matrix.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = matrix[rowIndex, columnIndex]
                    });
                }

                DataGridMatrix.ColumnCount = size;
                DataGridMatrix.Rows.Add(row);
            }

            file.Close();
        }

        public static void LoadText(string path, RichTextBox TextBox)
        {
            int counter = 0;
            string line;
            string tmp = null;

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if (line == "Text:")
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        tmp += line;
                    }
                }
                if (tmp != null)
                {
                    TextBox.Text = tmp;
                }
            }

            file.Close();
        }
    }
}
