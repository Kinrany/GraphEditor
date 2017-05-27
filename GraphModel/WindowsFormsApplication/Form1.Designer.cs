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
			this.loadGraphButton = new System.Windows.Forms.Button();
			this.graphBox = new System.Windows.Forms.GroupBox();
			this.loadExampleButton = new System.Windows.Forms.Button();
			this.debugLabel = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.saveButtonLabel = new System.Windows.Forms.Label();
			this.CodeImport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// loadGraphButton
			// 
			this.loadGraphButton.Location = new System.Drawing.Point(37, 32);
			this.loadGraphButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.loadGraphButton.Name = "loadGraphButton";
			this.loadGraphButton.Size = new System.Drawing.Size(139, 28);
			this.loadGraphButton.TabIndex = 0;
			this.loadGraphButton.Text = "Открыть граф";
			this.loadGraphButton.UseVisualStyleBackColor = true;
			this.loadGraphButton.Click += new System.EventHandler(this.loadGraphButton_Click);
			// 
			// graphBox
			// 
			this.graphBox.Location = new System.Drawing.Point(67, 118);
			this.graphBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.graphBox.Name = "graphBox";
			this.graphBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.graphBox.Size = new System.Drawing.Size(1072, 414);
			this.graphBox.TabIndex = 2;
			this.graphBox.TabStop = false;
			// 
			// loadExampleButton
			// 
			this.loadExampleButton.Location = new System.Drawing.Point(37, 68);
			this.loadExampleButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.loadExampleButton.Name = "loadExampleButton";
			this.loadExampleButton.Size = new System.Drawing.Size(139, 28);
			this.loadExampleButton.TabIndex = 3;
			this.loadExampleButton.Text = "Открыть пример";
			this.loadExampleButton.UseVisualStyleBackColor = true;
			this.loadExampleButton.Click += new System.EventHandler(this.loadExampleButton_Click);
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
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(260, 32);
			this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(100, 28);
			this.saveButton.TabIndex = 5;
			this.saveButton.Text = "Сохранить";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
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
			// CodeImport
			// 
			this.CodeImport.Location = new System.Drawing.Point(403, 32);
			this.CodeImport.Name = "CodeImport";
			this.CodeImport.Size = new System.Drawing.Size(130, 28);
			this.CodeImport.TabIndex = 7;
			this.CodeImport.Text = "Импорт кода";
			this.CodeImport.UseVisualStyleBackColor = true;
			this.CodeImport.Click += new System.EventHandler(this.CodeImport_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1183, 583);
			this.Controls.Add(this.CodeImport);
			this.Controls.Add(this.saveButtonLabel);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.debugLabel);
			this.Controls.Add(this.loadExampleButton);
			this.Controls.Add(this.graphBox);
			this.Controls.Add(this.loadGraphButton);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button loadGraphButton;
		private System.Windows.Forms.GroupBox graphBox;
		private System.Windows.Forms.Button loadExampleButton;
		private System.Windows.Forms.Label debugLabel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Label saveButtonLabel;
		private System.Windows.Forms.Button CodeImport;
	}
}

