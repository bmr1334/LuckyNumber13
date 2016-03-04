namespace Map_Maker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Random = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Length = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.File = new System.Windows.Forms.GroupBox();
            this.Overwrite = new System.Windows.Forms.CheckBox();
            this.FileName = new System.Windows.Forms.Label();
            this.FileInput = new System.Windows.Forms.TextBox();
            this.CreateFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Output = new System.Windows.Forms.TextBox();
            this.FileContents = new System.Windows.Forms.Label();
            this.Display = new System.Windows.Forms.Label();
            this.Reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).BeginInit();
            this.File.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).BeginInit();
            this.SuspendLayout();
            // 
            // Random
            // 
            this.Random.Location = new System.Drawing.Point(12, 25);
            this.Random.Name = "Random";
            this.Random.Size = new System.Drawing.Size(81, 35);
            this.Random.TabIndex = 0;
            this.Random.Text = "Random";
            this.Random.UseVisualStyleBackColor = true;
            this.Random.Click += new System.EventHandler(this.Random_Click);
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(123, 25);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(81, 35);
            this.New.TabIndex = 1;
            this.New.Text = "New";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(233, 25);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(81, 35);
            this.Load.TabIndex = 2;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(343, 25);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(81, 35);
            this.Save.TabIndex = 3;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(262, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(271, 243);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Length
            // 
            this.Length.Location = new System.Drawing.Point(66, 71);
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(38, 20);
            this.Length.TabIndex = 5;
            this.Length.ValueChanged += new System.EventHandler(this.Length_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Width";
            // 
            // File
            // 
            this.File.BackColor = System.Drawing.SystemColors.Control;
            this.File.Controls.Add(this.Overwrite);
            this.File.Controls.Add(this.FileName);
            this.File.Controls.Add(this.FileInput);
            this.File.Controls.Add(this.CreateFile);
            this.File.Controls.Add(this.label3);
            this.File.Controls.Add(this.Width);
            this.File.Controls.Add(this.Length);
            this.File.Controls.Add(this.label2);
            this.File.Controls.Add(this.label1);
            this.File.Location = new System.Drawing.Point(14, 94);
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(244, 126);
            this.File.TabIndex = 8;
            this.File.TabStop = false;
            this.File.Text = "File";
            this.File.Enter += new System.EventHandler(this.File_Enter);
            // 
            // Overwrite
            // 
            this.Overwrite.AutoSize = true;
            this.Overwrite.Location = new System.Drawing.Point(8, 101);
            this.Overwrite.Name = "Overwrite";
            this.Overwrite.Size = new System.Drawing.Size(71, 17);
            this.Overwrite.TabIndex = 15;
            this.Overwrite.Text = "Overwrite";
            this.Overwrite.UseVisualStyleBackColor = true;
            this.Overwrite.CheckedChanged += new System.EventHandler(this.Overwrite_CheckedChanged);
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.Location = new System.Drawing.Point(11, 22);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(57, 13);
            this.FileName.TabIndex = 14;
            this.FileName.Text = "File Name:";
            // 
            // FileInput
            // 
            this.FileInput.Location = new System.Drawing.Point(69, 19);
            this.FileInput.Name = "FileInput";
            this.FileInput.Size = new System.Drawing.Size(169, 20);
            this.FileInput.TabIndex = 13;
            this.FileInput.TextChanged += new System.EventHandler(this.FileInput_TextChanged);
            // 
            // CreateFile
            // 
            this.CreateFile.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CreateFile.Location = new System.Drawing.Point(82, 97);
            this.CreateFile.Name = "CreateFile";
            this.CreateFile.Size = new System.Drawing.Size(75, 23);
            this.CreateFile.TabIndex = 12;
            this.CreateFile.Text = "Create File";
            this.CreateFile.UseVisualStyleBackColor = false;
            this.CreateFile.Click += new System.EventHandler(this.CreateFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "X";
            // 
            // Width
            // 
            this.Width.Location = new System.Drawing.Point(140, 71);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(38, 20);
            this.Width.TabIndex = 10;
            this.Width.ValueChanged += new System.EventHandler(this.Width_ValueChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(12, 95);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Output.Size = new System.Drawing.Size(521, 243);
            this.Output.TabIndex = 9;
            this.Output.WordWrap = false;
            this.Output.TextChanged += new System.EventHandler(this.Output_TextChanged);
            // 
            // FileContents
            // 
            this.FileContents.AutoSize = true;
            this.FileContents.Location = new System.Drawing.Point(11, 79);
            this.FileContents.Name = "FileContents";
            this.FileContents.Size = new System.Drawing.Size(68, 13);
            this.FileContents.TabIndex = 10;
            this.FileContents.Text = "File Contents";
            this.FileContents.Click += new System.EventHandler(this.FileContents_Click);
            // 
            // Display
            // 
            this.Display.AutoSize = true;
            this.Display.Location = new System.Drawing.Point(264, 79);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(41, 13);
            this.Display.TabIndex = 11;
            this.Display.Text = "Display";
            this.Display.Visible = false;
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(452, 25);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(81, 35);
            this.Reset.TabIndex = 12;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 350);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Display);
            this.Controls.Add(this.FileContents);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.File);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.New);
            this.Controls.Add(this.Random);
            this.Name = "Form1";
            this.Text = "Map Maker";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Length)).EndInit();
            this.File.ResumeLayout(false);
            this.File.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Random;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown Length;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox File;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown Width;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CreateFile;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.Label FileContents;
        private System.Windows.Forms.Label Display;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.TextBox FileInput;
        private System.Windows.Forms.CheckBox Overwrite;
    }
}

