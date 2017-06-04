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
            this.настройкиГрафаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.coloringModeRadioButton = new System.Windows.Forms.RadioButton();
            this.defaultModeButton = new System.Windows.Forms.RadioButton();
            this.colorPickerButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.LoadCheckerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.debugLabel.Visible = false;
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
            this.DataGridMatrix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DataGridMatrix.Name = "DataGridMatrix";
            this.DataGridMatrix.RowTemplate.Height = 24;
            this.DataGridMatrix.Size = new System.Drawing.Size(347, 178);
            this.DataGridMatrix.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpenGraph,
            this.toolStripSaveGraph,
            this.toolStripImportCode,
            this.настройкиГрафаToolStripMenuItem,
            this.LoadCheckerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
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
            // настройкиГрафаToolStripMenuItem
            // 
            this.настройкиГрафаToolStripMenuItem.Name = "настройкиГрафаToolStripMenuItem";
            this.настройкиГрафаToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.настройкиГрафаToolStripMenuItem.Text = "Настройки графа";
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.TextBox.Location = new System.Drawing.Point(825, 320);
            this.TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBox.Name = "TextBox";
            this.TextBox.ReadOnly = true;
            this.TextBox.Size = new System.Drawing.Size(345, 212);
            this.TextBox.TabIndex = 11;
            this.TextBox.Text = "";
            // 
            // coloringModeRadioButton
            // 
            this.coloringModeRadioButton.AutoSize = true;
            this.coloringModeRadioButton.Location = new System.Drawing.Point(157, 98);
            this.coloringModeRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.coloringModeRadioButton.Name = "coloringModeRadioButton";
            this.coloringModeRadioButton.Size = new System.Drawing.Size(92, 21);
            this.coloringModeRadioButton.TabIndex = 12;
            this.coloringModeRadioButton.Text = "Покраска";
            this.coloringModeRadioButton.UseVisualStyleBackColor = true;
            this.coloringModeRadioButton.CheckedChanged += new System.EventHandler(this.coloringModeButton_CheckedChanged);
            // 
            // defaultModeButton
            // 
            this.defaultModeButton.AutoSize = true;
            this.defaultModeButton.Checked = true;
            this.defaultModeButton.Location = new System.Drawing.Point(67, 98);
            this.defaultModeButton.Margin = new System.Windows.Forms.Padding(4);
            this.defaultModeButton.Name = "defaultModeButton";
            this.defaultModeButton.Size = new System.Drawing.Size(82, 21);
            this.defaultModeButton.TabIndex = 13;
            this.defaultModeButton.TabStop = true;
            this.defaultModeButton.Text = "Дефолт";
            this.defaultModeButton.UseVisualStyleBackColor = true;
            this.defaultModeButton.CheckedChanged += new System.EventHandler(this.defaultModeButton_CheckedChanged);
            // 
            // colorPickerButton
            // 
            this.colorPickerButton.Location = new System.Drawing.Point(681, 82);
            this.colorPickerButton.Margin = new System.Windows.Forms.Padding(4);
            this.colorPickerButton.Name = "colorPickerButton";
            this.colorPickerButton.Size = new System.Drawing.Size(137, 28);
            this.colorPickerButton.TabIndex = 14;
            this.colorPickerButton.Text = "Выбрать цвет";
            this.colorPickerButton.UseVisualStyleBackColor = true;
            this.colorPickerButton.Click += new System.EventHandler(this.colorPickerButton_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AllowFullOpen = false;
            this.colorDialog.SolidColorOnly = true;
            // 
            // LoadCheckerToolStripMenuItem
            // 
            this.LoadCheckerToolStripMenuItem.Name = "LoadCheckerToolStripMenuItem";
            this.LoadCheckerToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.LoadCheckerToolStripMenuItem.Text = "Загрузить проверяльщик";
            this.LoadCheckerToolStripMenuItem.Click += new System.EventHandler(this.LoadCheckerToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 583);
            this.Controls.Add(this.colorPickerButton);
            this.Controls.Add(this.defaultModeButton);
            this.Controls.Add(this.coloringModeRadioButton);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.DataGridMatrix);
            this.Controls.Add(this.saveButtonLabel);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.graphBox);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "GraphMaker";
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
		private System.Windows.Forms.RadioButton coloringModeRadioButton;
		private System.Windows.Forms.RadioButton defaultModeButton;
		private System.Windows.Forms.Button colorPickerButton;
		private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem настройкиГрафаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadCheckerToolStripMenuItem;
    }
}

