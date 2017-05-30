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
			this.coloringModeRadioButton = new System.Windows.Forms.RadioButton();
			this.defaultModeButton = new System.Windows.Forms.RadioButton();
			this.colorPickerButton = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.DataGridMatrix)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// graphBox
			// 
			this.graphBox.Location = new System.Drawing.Point(50, 96);
			this.graphBox.Name = "graphBox";
			this.graphBox.Size = new System.Drawing.Size(563, 336);
			this.graphBox.TabIndex = 2;
			this.graphBox.TabStop = false;
			// 
			// debugLabel
			// 
			this.debugLabel.AutoSize = true;
			this.debugLabel.Location = new System.Drawing.Point(591, 452);
			this.debugLabel.Name = "debugLabel";
			this.debugLabel.Size = new System.Drawing.Size(74, 13);
			this.debugLabel.TabIndex = 4;
			this.debugLabel.Text = "<debug label>";
			// 
			// saveButtonLabel
			// 
			this.saveButtonLabel.AutoSize = true;
			this.saveButtonLabel.Location = new System.Drawing.Point(277, 35);
			this.saveButtonLabel.Name = "saveButtonLabel";
			this.saveButtonLabel.Size = new System.Drawing.Size(0, 13);
			this.saveButtonLabel.TabIndex = 6;
			// 
			// DataGridMatrix
			// 
			this.DataGridMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DataGridMatrix.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.DataGridMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridMatrix.Location = new System.Drawing.Point(619, 110);
			this.DataGridMatrix.Margin = new System.Windows.Forms.Padding(2);
			this.DataGridMatrix.Name = "DataGridMatrix";
			this.DataGridMatrix.RowTemplate.Height = 24;
			this.DataGridMatrix.Size = new System.Drawing.Size(260, 145);
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
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(887, 24);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripOpenGraph
			// 
			this.toolStripOpenGraph.Name = "toolStripOpenGraph";
			this.toolStripOpenGraph.Size = new System.Drawing.Size(96, 20);
			this.toolStripOpenGraph.Text = "Открыть граф";
			this.toolStripOpenGraph.Click += new System.EventHandler(this.toolStripOpenGraph_Click);
			// 
			// toolStripSaveGraph
			// 
			this.toolStripSaveGraph.Name = "toolStripSaveGraph";
			this.toolStripSaveGraph.Size = new System.Drawing.Size(107, 20);
			this.toolStripSaveGraph.Text = "Сохранить граф";
			this.toolStripSaveGraph.Click += new System.EventHandler(this.toolStripSaveGraph_Click);
			// 
			// toolStripImportCode
			// 
			this.toolStripImportCode.Name = "toolStripImportCode";
			this.toolStripImportCode.Size = new System.Drawing.Size(91, 20);
			this.toolStripImportCode.Text = "Импорт кода";
			this.toolStripImportCode.Click += new System.EventHandler(this.toolStripImportCode_Click);
			// 
			// TextBox
			// 
			this.TextBox.BackColor = System.Drawing.SystemColors.Menu;
			this.TextBox.Location = new System.Drawing.Point(619, 260);
			this.TextBox.Margin = new System.Windows.Forms.Padding(2);
			this.TextBox.Name = "TextBox";
			this.TextBox.ReadOnly = true;
			this.TextBox.Size = new System.Drawing.Size(260, 173);
			this.TextBox.TabIndex = 11;
			this.TextBox.Text = "";
			// 
			// coloringModeRadioButton
			// 
			this.coloringModeRadioButton.AutoSize = true;
			this.coloringModeRadioButton.Location = new System.Drawing.Point(506, 58);
			this.coloringModeRadioButton.Name = "coloringModeRadioButton";
			this.coloringModeRadioButton.Size = new System.Drawing.Size(75, 17);
			this.coloringModeRadioButton.TabIndex = 12;
			this.coloringModeRadioButton.Text = "Покраска";
			this.coloringModeRadioButton.UseVisualStyleBackColor = true;
			this.coloringModeRadioButton.CheckedChanged += new System.EventHandler(this.coloringModeButton_CheckedChanged);
			// 
			// defaultModeButton
			// 
			this.defaultModeButton.AutoSize = true;
			this.defaultModeButton.Checked = true;
			this.defaultModeButton.Location = new System.Drawing.Point(506, 35);
			this.defaultModeButton.Name = "defaultModeButton";
			this.defaultModeButton.Size = new System.Drawing.Size(65, 17);
			this.defaultModeButton.TabIndex = 13;
			this.defaultModeButton.TabStop = true;
			this.defaultModeButton.Text = "Дефолт";
			this.defaultModeButton.UseVisualStyleBackColor = true;
			this.defaultModeButton.CheckedChanged += new System.EventHandler(this.defaultModeButton_CheckedChanged);
			// 
			// colorPickerButton
			// 
			this.colorPickerButton.Location = new System.Drawing.Point(741, 58);
			this.colorPickerButton.Name = "colorPickerButton";
			this.colorPickerButton.Size = new System.Drawing.Size(103, 23);
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(887, 474);
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
		private System.Windows.Forms.RadioButton coloringModeRadioButton;
		private System.Windows.Forms.RadioButton defaultModeButton;
		private System.Windows.Forms.Button colorPickerButton;
		private System.Windows.Forms.ColorDialog colorDialog;
	}
}

