namespace Diplom
{
    partial class FormWrapper
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.импортироватьДанныеИзФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортироватьРезультатыВФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonInfo = new System.Windows.Forms.ToolStripDropDownButton();
            this.информацияОСоздателеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonImportData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNoise = new System.Windows.Forms.CheckBox();
            this.buttonChooseImportFile = new System.Windows.Forms.Button();
            this.textBoxImportFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonExportData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxExportFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxOpenExportFile = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonChooseExportFile = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxParamK = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelN = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarMethodCusumParamB = new System.Windows.Forms.TrackBar();
            this.trackBarMethodCusumParamN = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMethodCusumParamB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMethodCusumParamN)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFile,
            this.toolStripDropDownButtonInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1548, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonFile
            // 
            this.toolStripButtonFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.импортироватьДанныеИзФайлаToolStripMenuItem,
            this.экспортироватьРезультатыВФайлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.toolStripButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFile.Name = "toolStripButtonFile";
            this.toolStripButtonFile.Size = new System.Drawing.Size(71, 29);
            this.toolStripButtonFile.Text = "Файл";
            // 
            // импортироватьДанныеИзФайлаToolStripMenuItem
            // 
            this.импортироватьДанныеИзФайлаToolStripMenuItem.Name = "импортироватьДанныеИзФайлаToolStripMenuItem";
            this.импортироватьДанныеИзФайлаToolStripMenuItem.Size = new System.Drawing.Size(472, 30);
            this.импортироватьДанныеИзФайлаToolStripMenuItem.Text = "Импортировать данные из файла (CTRL + O)";
            this.импортироватьДанныеИзФайлаToolStripMenuItem.Click += new System.EventHandler(this.импортироватьДанныеИзФайлаToolStripMenuItem_Click);
            // 
            // экспортироватьРезультатыВФайлToolStripMenuItem
            // 
            this.экспортироватьРезультатыВФайлToolStripMenuItem.Name = "экспортироватьРезультатыВФайлToolStripMenuItem";
            this.экспортироватьРезультатыВФайлToolStripMenuItem.Size = new System.Drawing.Size(472, 30);
            this.экспортироватьРезультатыВФайлToolStripMenuItem.Text = "Экспортировать результаты в файл (CTRL + S)";
            this.экспортироватьРезультатыВФайлToolStripMenuItem.Click += new System.EventHandler(this.экспортироватьРезультатыВФайлToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(472, 30);
            this.выходToolStripMenuItem.Text = "Выход (ESC)";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // toolStripDropDownButtonInfo
            // 
            this.toolStripDropDownButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.информацияОСоздателеToolStripMenuItem});
            this.toolStripDropDownButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonInfo.Name = "toolStripDropDownButtonInfo";
            this.toolStripDropDownButtonInfo.Size = new System.Drawing.Size(108, 29);
            this.toolStripDropDownButtonInfo.Text = "Сведения";
            // 
            // информацияОСоздателеToolStripMenuItem
            // 
            this.информацияОСоздателеToolStripMenuItem.Name = "информацияОСоздателеToolStripMenuItem";
            this.информацияОСоздателеToolStripMenuItem.Size = new System.Drawing.Size(241, 30);
            this.информацияОСоздателеToolStripMenuItem.Text = "Подробнее о ВКР";
            this.информацияОСоздателеToolStripMenuItem.Click += new System.EventHandler(this.информацияОСоздателеToolStripMenuItem_Click);
            // 
            // buttonImportData
            // 
            this.buttonImportData.Location = new System.Drawing.Point(6, 92);
            this.buttonImportData.Name = "buttonImportData";
            this.buttonImportData.Size = new System.Drawing.Size(421, 53);
            this.buttonImportData.TabIndex = 3;
            this.buttonImportData.Text = "Импортировать показатели давления из файла";
            this.buttonImportData.UseVisualStyleBackColor = true;
            this.buttonImportData.Click += new System.EventHandler(this.buttonImportData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNoise);
            this.groupBox1.Controls.Add(this.buttonChooseImportFile);
            this.groupBox1.Controls.Add(this.textBoxImportFileName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonImportData);
            this.groupBox1.Location = new System.Drawing.Point(22, 689);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 155);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Импорт";
            // 
            // checkBoxNoise
            // 
            this.checkBoxNoise.AutoSize = true;
            this.checkBoxNoise.Location = new System.Drawing.Point(10, 62);
            this.checkBoxNoise.Name = "checkBoxNoise";
            this.checkBoxNoise.Size = new System.Drawing.Size(221, 24);
            this.checkBoxNoise.TabIndex = 6;
            this.checkBoxNoise.Text = "Добавить шум к данным";
            this.checkBoxNoise.UseVisualStyleBackColor = true;
            // 
            // buttonChooseImportFile
            // 
            this.buttonChooseImportFile.Location = new System.Drawing.Point(326, 26);
            this.buttonChooseImportFile.Name = "buttonChooseImportFile";
            this.buttonChooseImportFile.Size = new System.Drawing.Size(101, 35);
            this.buttonChooseImportFile.TabIndex = 12;
            this.buttonChooseImportFile.Text = "Выбрать";
            this.buttonChooseImportFile.UseVisualStyleBackColor = true;
            this.buttonChooseImportFile.Click += new System.EventHandler(this.buttonChooseImportFile_Click);
            // 
            // textBoxImportFileName
            // 
            this.textBoxImportFileName.Location = new System.Drawing.Point(182, 29);
            this.textBoxImportFileName.Name = "textBoxImportFileName";
            this.textBoxImportFileName.Size = new System.Drawing.Size(138, 26);
            this.textBoxImportFileName.TabIndex = 12;
            this.textBoxImportFileName.Text = "data3.xlsx";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Имя входного файла";
            // 
            // buttonExportData
            // 
            this.buttonExportData.Location = new System.Drawing.Point(15, 92);
            this.buttonExportData.Name = "buttonExportData";
            this.buttonExportData.Size = new System.Drawing.Size(426, 53);
            this.buttonExportData.TabIndex = 7;
            this.buttonExportData.Text = "Экспортировать результаты в файл";
            this.buttonExportData.UseVisualStyleBackColor = true;
            this.buttonExportData.Click += new System.EventHandler(this.buttonExportData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Показатели датчика давления";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1063, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Вычисления алгоритма";
            // 
            // textBoxExportFileName
            // 
            this.textBoxExportFileName.Location = new System.Drawing.Point(198, 30);
            this.textBoxExportFileName.Name = "textBoxExportFileName";
            this.textBoxExportFileName.Size = new System.Drawing.Size(132, 26);
            this.textBoxExportFileName.TabIndex = 8;
            this.textBoxExportFileName.Text = "results.xlsx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Имя выходного файла";
            // 
            // checkBoxOpenExportFile
            // 
            this.checkBoxOpenExportFile.AutoSize = true;
            this.checkBoxOpenExportFile.Checked = true;
            this.checkBoxOpenExportFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOpenExportFile.Location = new System.Drawing.Point(15, 62);
            this.checkBoxOpenExportFile.Name = "checkBoxOpenExportFile";
            this.checkBoxOpenExportFile.Size = new System.Drawing.Size(277, 24);
            this.checkBoxOpenExportFile.TabIndex = 10;
            this.checkBoxOpenExportFile.Text = "Открыть файл при завершении";
            this.checkBoxOpenExportFile.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonChooseExportFile);
            this.groupBox2.Controls.Add(this.buttonExportData);
            this.groupBox2.Controls.Add(this.checkBoxOpenExportFile);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxExportFileName);
            this.groupBox2.Location = new System.Drawing.Point(890, 689);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 155);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Экспорт";
            // 
            // buttonChooseExportFile
            // 
            this.buttonChooseExportFile.Location = new System.Drawing.Point(340, 26);
            this.buttonChooseExportFile.Name = "buttonChooseExportFile";
            this.buttonChooseExportFile.Size = new System.Drawing.Size(101, 35);
            this.buttonChooseExportFile.TabIndex = 13;
            this.buttonChooseExportFile.Text = "Выбрать";
            this.buttonChooseExportFile.UseVisualStyleBackColor = true;
            this.buttonChooseExportFile.Click += new System.EventHandler(this.buttonChooseExportFile_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxParamK);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.labelB);
            this.groupBox3.Controls.Add(this.labelN);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.trackBarMethodCusumParamB);
            this.groupBox3.Controls.Add(this.trackBarMethodCusumParamN);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(471, 689);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(410, 155);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры алгоритма";
            // 
            // textBoxParamK
            // 
            this.textBoxParamK.Location = new System.Drawing.Point(326, 40);
            this.textBoxParamK.Name = "textBoxParamK";
            this.textBoxParamK.Size = new System.Drawing.Size(63, 26);
            this.textBoxParamK.TabIndex = 11;
            this.textBoxParamK.Text = "20";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(303, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "K";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(79, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 20);
            this.label14.TabIndex = 21;
            this.label14.Text = ")";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(81, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 20);
            this.label13.TabIndex = 20;
            this.label13.Text = ")";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(29, 94);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(18, 20);
            this.labelB.TabIndex = 19;
            this.labelB.Text = "0";
            // 
            // labelN
            // 
            this.labelN.AutoSize = true;
            this.labelN.Location = new System.Drawing.Point(32, 40);
            this.labelN.Name = "labelN";
            this.labelN.Size = new System.Drawing.Size(18, 20);
            this.labelN.TabIndex = 18;
            this.labelN.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(245, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "0.050";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(103, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "-1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "0";
            // 
            // trackBarMethodCusumParamB
            // 
            this.trackBarMethodCusumParamB.Location = new System.Drawing.Point(105, 86);
            this.trackBarMethodCusumParamB.Maximum = 50;
            this.trackBarMethodCusumParamB.Name = "trackBarMethodCusumParamB";
            this.trackBarMethodCusumParamB.Size = new System.Drawing.Size(173, 69);
            this.trackBarMethodCusumParamB.TabIndex = 13;
            this.trackBarMethodCusumParamB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMethodCusumParamB.Value = 25;
            this.trackBarMethodCusumParamB.ValueChanged += new System.EventHandler(this.trackBarParamB_ValueChanged);
            // 
            // trackBarMethodCusumParamN
            // 
            this.trackBarMethodCusumParamN.Location = new System.Drawing.Point(107, 32);
            this.trackBarMethodCusumParamN.Maximum = 0;
            this.trackBarMethodCusumParamN.Minimum = -100;
            this.trackBarMethodCusumParamN.Name = "trackBarMethodCusumParamN";
            this.trackBarMethodCusumParamN.Size = new System.Drawing.Size(173, 69);
            this.trackBarMethodCusumParamN.TabIndex = 12;
            this.trackBarMethodCusumParamN.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMethodCusumParamN.Value = -40;
            this.trackBarMethodCusumParamN.ValueChanged += new System.EventHandler(this.trackBarParamN_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "b(";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "N(";
            // 
            // FormWrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1548, 856);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1570, 912);
            this.MinimumSize = new System.Drawing.Size(1570, 912);
            this.Name = "FormWrapper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программное обеспечение для обнаружения утечек нефтепродуктов в трубопроводе";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FormWrapper_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMethodCusumParamB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMethodCusumParamN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButtonFile;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonInfo;
        private System.Windows.Forms.ToolStripMenuItem информацияОСоздателеToolStripMenuItem;
        private System.Windows.Forms.Button buttonImportData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxNoise;
        private System.Windows.Forms.Button buttonExportData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxExportFileName;
        private System.Windows.Forms.TextBox textBoxImportFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxOpenExportFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarMethodCusumParamB;
        private System.Windows.Forms.TrackBar trackBarMethodCusumParamN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxParamK;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonChooseImportFile;
        private System.Windows.Forms.Button buttonChooseExportFile;
        private System.Windows.Forms.ToolStripMenuItem импортироватьДанныеИзФайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортироватьРезультатыВФайлToolStripMenuItem;
    }
}

