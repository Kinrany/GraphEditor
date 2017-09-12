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
			this.debugLabel = new System.Windows.Forms.Label();
			this.saveButtonLabel = new System.Windows.Forms.Label();
			this.DataGridMatrix = new System.Windows.Forms.DataGridView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.открытьГрафА1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.а1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.а1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripImportCode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSaveImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripRearrangeMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripRearrangeCircle = new System.Windows.Forms.ToolStripMenuItem();
			this.TextBox = new System.Windows.Forms.RichTextBox();
			this.coloringModeRadioButton = new System.Windows.Forms.RadioButton();
			this.defaultModeButton = new System.Windows.Forms.RadioButton();
			this.colorPickerButton = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.graphBox = new System.Windows.Forms.PictureBox();
			this.а2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.DataGridMatrix)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.graphBox)).BeginInit();
			this.SuspendLayout();
			// 
			// debugLabel
			// 
			this.debugLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.debugLabel.AutoSize = true;
			this.debugLabel.Enabled = false;
			this.debugLabel.Location = new System.Drawing.Point(756, 35);
			this.debugLabel.Name = "debugLabel";
			this.debugLabel.Size = new System.Drawing.Size(74, 13);
			this.debugLabel.TabIndex = 4;
			this.debugLabel.Text = "<debug label>";
			this.debugLabel.Visible = false;
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
			this.DataGridMatrix.AllowUserToAddRows = false;
			this.DataGridMatrix.AllowUserToDeleteRows = false;
			this.DataGridMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DataGridMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DataGridMatrix.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.DataGridMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridMatrix.Location = new System.Drawing.Point(640, 96);
			this.DataGridMatrix.Margin = new System.Windows.Forms.Padding(2);
			this.DataGridMatrix.Name = "DataGridMatrix";
			this.DataGridMatrix.RowTemplate.Height = 24;
			this.DataGridMatrix.Size = new System.Drawing.Size(330, 273);
			this.DataGridMatrix.TabIndex = 8;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.toolStripImportCode,
            this.toolStripSaveImage,
            this.toolStripRearrangeMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(981, 24);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьГрафА1ToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// открытьГрафА1ToolStripMenuItem
			// 
			this.открытьГрафА1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.а1ToolStripMenuItem1,
            this.а2ToolStripMenuItem});
			this.открытьГрафА1ToolStripMenuItem.Name = "открытьГрафА1ToolStripMenuItem";
			this.открытьГрафА1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.открытьГрафА1ToolStripMenuItem.Text = "Открыть";
			// 
			// а1ToolStripMenuItem1
			// 
			this.а1ToolStripMenuItem1.Name = "а1ToolStripMenuItem1";
			this.а1ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.а1ToolStripMenuItem1.Text = "А1";
			this.а1ToolStripMenuItem1.Click += new System.EventHandler(this.toolStripOpenA1Graph_Click);
			// 
			// сохранитьToolStripMenuItem
			// 
			this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.а1ToolStripMenuItem});
			this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
			this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.сохранитьToolStripMenuItem.Text = "Сохранить";
			// 
			// а1ToolStripMenuItem
			// 
			this.а1ToolStripMenuItem.Name = "а1ToolStripMenuItem";
			this.а1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.а1ToolStripMenuItem.Text = "А1";
			this.а1ToolStripMenuItem.Click += new System.EventHandler(this.toolStripSaveA1Graph_Click);
			// 
			// toolStripImportCode
			// 
			this.toolStripImportCode.Name = "toolStripImportCode";
			this.toolStripImportCode.Size = new System.Drawing.Size(91, 20);
			this.toolStripImportCode.Text = "Импорт кода";
			this.toolStripImportCode.Click += new System.EventHandler(this.toolStripImportCode_Click);
			// 
			// toolStripSaveImage
			// 
			this.toolStripSaveImage.Name = "toolStripSaveImage";
			this.toolStripSaveImage.Size = new System.Drawing.Size(154, 20);
			this.toolStripSaveImage.Text = "Сохранить изображение";
			this.toolStripSaveImage.Click += new System.EventHandler(this.toolStripSaveImage_Click);
			// 
			// toolStripRearrangeMenu
			// 
			this.toolStripRearrangeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRearrangeCircle});
			this.toolStripRearrangeMenu.Name = "toolStripRearrangeMenu";
			this.toolStripRearrangeMenu.Size = new System.Drawing.Size(147, 20);
			this.toolStripRearrangeMenu.Text = "Упорядочить вершины";
			// 
			// toolStripRearrangeCircle
			// 
			this.toolStripRearrangeCircle.Name = "toolStripRearrangeCircle";
			this.toolStripRearrangeCircle.Size = new System.Drawing.Size(142, 22);
			this.toolStripRearrangeCircle.Text = "Окружность";
			this.toolStripRearrangeCircle.Click += new System.EventHandler(this.toolStripRearrangeCircle_Click);
			// 
			// TextBox
			// 
			this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBox.BackColor = System.Drawing.SystemColors.Menu;
			this.TextBox.Location = new System.Drawing.Point(640, 373);
			this.TextBox.Margin = new System.Windows.Forms.Padding(2);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(330, 138);
			this.TextBox.TabIndex = 11;
			this.TextBox.Text = "";
			this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// coloringModeRadioButton
			// 
			this.coloringModeRadioButton.AutoSize = true;
			this.coloringModeRadioButton.Location = new System.Drawing.Point(12, 73);
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
			this.defaultModeButton.Location = new System.Drawing.Point(12, 50);
			this.defaultModeButton.Name = "defaultModeButton";
			this.defaultModeButton.Size = new System.Drawing.Size(109, 17);
			this.defaultModeButton.TabIndex = 13;
			this.defaultModeButton.TabStop = true;
			this.defaultModeButton.Text = "Редактирование";
			this.defaultModeButton.UseVisualStyleBackColor = true;
			this.defaultModeButton.CheckedChanged += new System.EventHandler(this.defaultModeButton_CheckedChanged);
			// 
			// colorPickerButton
			// 
			this.colorPickerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.colorPickerButton.Location = new System.Drawing.Point(532, 70);
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
			// graphBox
			// 
			this.graphBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphBox.Location = new System.Drawing.Point(12, 96);
			this.graphBox.Name = "graphBox";
			this.graphBox.Size = new System.Drawing.Size(623, 415);
			this.graphBox.TabIndex = 15;
			this.graphBox.TabStop = false;
			this.graphBox.Paint += new System.Windows.Forms.PaintEventHandler(this.graphBox_Draw);
			this.graphBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphBox_MouseMove);
			// 
			// а2ToolStripMenuItem
			// 
			this.а2ToolStripMenuItem.Name = "а2ToolStripMenuItem";
			this.а2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.а2ToolStripMenuItem.Text = "А2";
			this.а2ToolStripMenuItem.Click += new System.EventHandler(this.toolStripOpenA2Graph_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(981, 523);
			this.Controls.Add(this.graphBox);
			this.Controls.Add(this.colorPickerButton);
			this.Controls.Add(this.defaultModeButton);
			this.Controls.Add(this.coloringModeRadioButton);
			this.Controls.Add(this.TextBox);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.DataGridMatrix);
			this.Controls.Add(this.saveButtonLabel);
			this.Controls.Add(this.debugLabel);
			this.DoubleBuffered = true;
			this.Name = "Form1";
			this.Text = "Шкатулка графов";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.DataGridMatrix)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.graphBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label debugLabel;
		private System.Windows.Forms.Label saveButtonLabel;
        private System.Windows.Forms.DataGridView DataGridMatrix;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripImportCode;
        private System.Windows.Forms.RichTextBox TextBox;
		private System.Windows.Forms.RadioButton coloringModeRadioButton;
		private System.Windows.Forms.RadioButton defaultModeButton;
		private System.Windows.Forms.Button colorPickerButton;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.ToolStripMenuItem toolStripSaveImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripRearrangeMenu;
		private System.Windows.Forms.ToolStripMenuItem toolStripRearrangeCircle;
		private System.Windows.Forms.PictureBox graphBox;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem открытьГрафА1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem а1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem а1ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem а2ToolStripMenuItem;
	}
}

