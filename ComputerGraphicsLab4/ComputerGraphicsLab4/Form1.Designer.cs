namespace ComputerGraphicsLab4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawingPanel = new System.Windows.Forms.PictureBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.polygonsListBox = new System.Windows.Forms.ListBox();
            this.label = new System.Windows.Forms.Label();
            this.dxTextBox = new System.Windows.Forms.TextBox();
            this.dyTextBox = new System.Windows.Forms.TextBox();
            this.offsetButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.angleTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rotateAroundPointButton = new System.Windows.Forms.Button();
            this.rotateAroundCenterButton = new System.Windows.Forms.Button();
            this.pointXTextBox = new System.Windows.Forms.TextBox();
            this.pointYTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scaleXTextBox = new System.Windows.Forms.TextBox();
            this.scaleYTextBox = new System.Windows.Forms.TextBox();
            this.scaleAroundCenterButton = new System.Windows.Forms.Button();
            this.scaleAroundPointButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawingPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingPanel.Location = new System.Drawing.Point(0, 0);
            this.drawingPanel.Margin = new System.Windows.Forms.Padding(4);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(1667, 761);
            this.drawingPanel.TabIndex = 0;
            this.drawingPanel.TabStop = false;
            this.drawingPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseClick);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(1524, 718);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(127, 28);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Очистить cцену";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // polygonsListBox
            // 
            this.polygonsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.polygonsListBox.FormattingEnabled = true;
            this.polygonsListBox.ItemHeight = 16;
            this.polygonsListBox.Location = new System.Drawing.Point(1431, 0);
            this.polygonsListBox.Name = "polygonsListBox";
            this.polygonsListBox.Size = new System.Drawing.Size(236, 692);
            this.polygonsListBox.TabIndex = 4;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label.Location = new System.Drawing.Point(925, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(131, 16);
            this.label.TabIndex = 5;
            this.label.Text = "Смещение на dx, dy";
            // 
            // dxTextBox
            // 
            this.dxTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dxTextBox.Location = new System.Drawing.Point(928, 46);
            this.dxTextBox.Name = "dxTextBox";
            this.dxTextBox.Size = new System.Drawing.Size(120, 22);
            this.dxTextBox.TabIndex = 6;
            this.dxTextBox.Text = "10";
            // 
            // dyTextBox
            // 
            this.dyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dyTextBox.Location = new System.Drawing.Point(928, 84);
            this.dyTextBox.Name = "dyTextBox";
            this.dyTextBox.Size = new System.Drawing.Size(120, 22);
            this.dyTextBox.TabIndex = 7;
            this.dyTextBox.Text = "10";
            // 
            // offsetButton
            // 
            this.offsetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetButton.Location = new System.Drawing.Point(928, 120);
            this.offsetButton.Name = "offsetButton";
            this.offsetButton.Size = new System.Drawing.Size(120, 70);
            this.offsetButton.TabIndex = 8;
            this.offsetButton.Text = "Смешение";
            this.offsetButton.UseVisualStyleBackColor = true;
            this.offsetButton.Click += new System.EventHandler(this.offsetButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1217, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Угол";
            // 
            // angleTextBox
            // 
            this.angleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.angleTextBox.Location = new System.Drawing.Point(1180, 46);
            this.angleTextBox.Name = "angleTextBox";
            this.angleTextBox.Size = new System.Drawing.Size(120, 22);
            this.angleTextBox.TabIndex = 10;
            this.angleTextBox.Text = "15";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(1090, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Точка (X, Y)";
            // 
            // rotateAroundPointButton
            // 
            this.rotateAroundPointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rotateAroundPointButton.Location = new System.Drawing.Point(1054, 120);
            this.rotateAroundPointButton.Name = "rotateAroundPointButton";
            this.rotateAroundPointButton.Size = new System.Drawing.Size(120, 70);
            this.rotateAroundPointButton.TabIndex = 12;
            this.rotateAroundPointButton.Text = "Поворот вокруг точки";
            this.rotateAroundPointButton.UseVisualStyleBackColor = true;
            this.rotateAroundPointButton.Click += new System.EventHandler(this.rotateAroundPointButton_Click);
            // 
            // rotateAroundCenterButton
            // 
            this.rotateAroundCenterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rotateAroundCenterButton.Location = new System.Drawing.Point(1180, 120);
            this.rotateAroundCenterButton.Name = "rotateAroundCenterButton";
            this.rotateAroundCenterButton.Size = new System.Drawing.Size(120, 70);
            this.rotateAroundCenterButton.TabIndex = 13;
            this.rotateAroundCenterButton.Text = "Поворот вокруг центра";
            this.rotateAroundCenterButton.UseVisualStyleBackColor = true;
            this.rotateAroundCenterButton.Click += new System.EventHandler(this.rotateAroundCenterButton_Click);
            // 
            // pointXTextBox
            // 
            this.pointXTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointXTextBox.Location = new System.Drawing.Point(1054, 46);
            this.pointXTextBox.Name = "pointXTextBox";
            this.pointXTextBox.Size = new System.Drawing.Size(120, 22);
            this.pointXTextBox.TabIndex = 14;
            this.pointXTextBox.Text = "100";
            // 
            // pointYTextBox
            // 
            this.pointYTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pointYTextBox.Location = new System.Drawing.Point(1054, 84);
            this.pointYTextBox.Name = "pointYTextBox";
            this.pointYTextBox.Size = new System.Drawing.Size(120, 22);
            this.pointYTextBox.TabIndex = 15;
            this.pointYTextBox.Text = "100";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(1302, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Масштаб sx, sy";
            // 
            // scaleXTextBox
            // 
            this.scaleXTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleXTextBox.Location = new System.Drawing.Point(1305, 46);
            this.scaleXTextBox.Name = "scaleXTextBox";
            this.scaleXTextBox.Size = new System.Drawing.Size(120, 22);
            this.scaleXTextBox.TabIndex = 17;
            this.scaleXTextBox.Text = "1,1";
            // 
            // scaleYTextBox
            // 
            this.scaleYTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleYTextBox.Location = new System.Drawing.Point(1305, 84);
            this.scaleYTextBox.Name = "scaleYTextBox";
            this.scaleYTextBox.Size = new System.Drawing.Size(120, 22);
            this.scaleYTextBox.TabIndex = 18;
            this.scaleYTextBox.Text = "1,1";
            // 
            // scaleAroundCenterButton
            // 
            this.scaleAroundCenterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleAroundCenterButton.Location = new System.Drawing.Point(1305, 120);
            this.scaleAroundCenterButton.Name = "scaleAroundCenterButton";
            this.scaleAroundCenterButton.Size = new System.Drawing.Size(120, 70);
            this.scaleAroundCenterButton.TabIndex = 19;
            this.scaleAroundCenterButton.Text = "Масштаб от центра";
            this.scaleAroundCenterButton.UseVisualStyleBackColor = true;
            this.scaleAroundCenterButton.Click += new System.EventHandler(this.scaleAroundCenterButton_Click);
            // 
            // scaleAroundPointButton
            // 
            this.scaleAroundPointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleAroundPointButton.Location = new System.Drawing.Point(1305, 196);
            this.scaleAroundPointButton.Name = "scaleAroundPointButton";
            this.scaleAroundPointButton.Size = new System.Drawing.Size(120, 70);
            this.scaleAroundPointButton.TabIndex = 20;
            this.scaleAroundPointButton.Text = "Масштаб от точки";
            this.scaleAroundPointButton.UseVisualStyleBackColor = true;
            this.scaleAroundPointButton.Click += new System.EventHandler(this.scaleAroundPointButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1667, 761);
            this.Controls.Add(this.scaleAroundPointButton);
            this.Controls.Add(this.scaleAroundCenterButton);
            this.Controls.Add(this.scaleYTextBox);
            this.Controls.Add(this.scaleXTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pointYTextBox);
            this.Controls.Add(this.pointXTextBox);
            this.Controls.Add(this.rotateAroundCenterButton);
            this.Controls.Add(this.rotateAroundPointButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.angleTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.offsetButton);
            this.Controls.Add(this.dyTextBox);
            this.Controls.Add(this.dxTextBox);
            this.Controls.Add(this.label);
            this.Controls.Add(this.polygonsListBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.drawingPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.drawingPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox drawingPanel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ListBox polygonsListBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox dxTextBox;
        private System.Windows.Forms.TextBox dyTextBox;
        private System.Windows.Forms.Button offsetButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox angleTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button rotateAroundPointButton;
        private System.Windows.Forms.Button rotateAroundCenterButton;
        private System.Windows.Forms.TextBox pointXTextBox;
        private System.Windows.Forms.TextBox pointYTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox scaleXTextBox;
        private System.Windows.Forms.TextBox scaleYTextBox;
        private System.Windows.Forms.Button scaleAroundCenterButton;
        private System.Windows.Forms.Button scaleAroundPointButton;
    }
}

