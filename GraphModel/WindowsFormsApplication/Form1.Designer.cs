namespace WindowsFormsApplication {
	partial class Form1 {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
            this.graphBox = new System.Windows.Forms.GroupBox();
            this.debugLabel = new System.Windows.Forms.Label();
            this.saveButtonLabel = new System.Windows.Forms.Label();
            this.DataGridMatrix = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripOpenGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSaveGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripImportCode = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMatrix)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphBox
            // 
            this.graphBox.Location = new System.Drawing.Point(67, 118);
            this.graphBox.Margin = new System.Windows.Forms.Padding(4);
            this.graphBox.Name = "graphBox";
            this.graphBox.Padding = new System.Windows.Forms.Padding(4);
            this.graphBox.Size = new System.Drawing.Size(751, 414);
            this.graphBox.TabIndex = 2;
            this.graphBox.TabStop = false;
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(788, 556);
            this.debugLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(98, 17);
            this.debugLabel.TabIndex = 4;
            this.debugLabel.Text = "<debug label>";
            // 
            // saveButtonLabel
            // 
            this.saveButtonLabel.AutoSize = true;
            this.saveButtonLabel.Location = new System.Drawing.Point(369, 43);
            this.saveButtonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saveButtonLabel.Name = "saveButtonLabel";
            this.saveButtonLabel.Size = new System.Drawing.Size(0, 17);
            this.saveButtonLabel.TabIndex = 6;
            // 
            // DataGridMatrix
            // 
            this.DataGridMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridMatrix.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridMatrix.Location = new System.Drawing.Point(825, 135);
            this.DataGridMatrix.Name = "DataGridMatrix";
            this.DataGridMatrix.RowTemplate.Height = 24;
            this.DataGridMatrix.Size = new System.Drawing.Size(346, 179);
            this.DataGridMatrix.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpenGraph,
            this.toolStripSaveGraph,
            this.toolStripImportCode});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1183, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripOpenGraph
            // 
            this.toolStripOpenGraph.Name = "toolStripOpenGraph";
            this.toolStripOpenGraph.Size = new System.Drawing.Size(116, 24);
            this.toolStripOpenGraph.Text = "Открыть граф";
            this.toolStripOpenGraph.Click += new System.EventHandler(this.toolStripOpenGraph_Click);
            // 
            // toolStripSaveGraph
            // 
            this.toolStripSaveGraph.Name = "toolStripSaveGraph";
            this.toolStripSaveGraph.Size = new System.Drawing.Size(132, 24);
            this.toolStripSaveGraph.Text = "Сохранить граф";
            this.toolStripSaveGraph.Click += new System.EventHandler(this.toolStripSaveGraph_Click);
            // 
            // toolStripImportCode
            // 
            this.toolStripImportCode.Name = "toolStripImportCode";
            this.toolStripImportCode.Size = new System.Drawing.Size(112, 24);
            this.toolStripImportCode.Text = "Импорт кода";
            this.toolStripImportCode.Click += new System.EventHandler(this.toolStripImportCode_Click);
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.TextBox.Location = new System.Drawing.Point(825, 320);
            this.TextBox.Name = "TextBox";
            this.TextBox.ReadOnly = true;
            this.TextBox.Size = new System.Drawing.Size(346, 212);
            this.TextBox.TabIndex = 11;
            this.TextBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 583);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.DataGridMatrix);
            this.Controls.Add(this.saveButtonLabel);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.graphBox);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMatrix)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.GroupBox graphBox;
		private System.Windows.Forms.Label debugLabel;
		private System.Windows.Forms.Label saveButtonLabel;
        private System.Windows.Forms.DataGridView DataGridMatrix;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripOpenGraph;
        private System.Windows.Forms.ToolStripMenuItem toolStripSaveGraph;
        private System.Windows.Forms.ToolStripMenuItem toolStripImportCode;
        private System.Windows.Forms.RichTextBox TextBox;
    }
}

