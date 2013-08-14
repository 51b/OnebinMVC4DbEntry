namespace Onebin.Tools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDllName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEqual = new System.Windows.Forms.Button();
            this.tbOutputPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDllPath = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbOutputName = new System.Windows.Forms.TextBox();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.btnOpenFileInTxt = new System.Windows.Forms.Button();
            this.btnOpenFileInDefault = new System.Windows.Forms.Button();
            this.cbDbType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-151, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "DLL名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-150, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "输出路径：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-152, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "DLL路径：";
            // 
            // tbDllName
            // 
            this.tbDllName.Location = new System.Drawing.Point(74, 49);
            this.tbDllName.Name = "tbDllName";
            this.tbDllName.Size = new System.Drawing.Size(210, 21);
            this.tbDllName.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "DLL名称：";
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(560, 131);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(37, 23);
            this.btnEqual.TabIndex = 20;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // tbOutputPath
            // 
            this.tbOutputPath.Location = new System.Drawing.Point(74, 132);
            this.tbOutputPath.Name = "tbOutputPath";
            this.tbOutputPath.Size = new System.Drawing.Size(479, 21);
            this.tbOutputPath.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "输出路径：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "DLL路径：";
            // 
            // tbDllPath
            // 
            this.tbDllPath.Location = new System.Drawing.Point(74, 89);
            this.tbDllPath.Name = "tbDllPath";
            this.tbDllPath.Size = new System.Drawing.Size(523, 21);
            this.tbDllPath.TabIndex = 16;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(111, 205);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "生成脚本";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "输出文件名：";
            // 
            // tbOutputName
            // 
            this.tbOutputName.Location = new System.Drawing.Point(385, 49);
            this.tbOutputName.Name = "tbOutputName";
            this.tbOutputName.Size = new System.Drawing.Size(210, 21);
            this.tbOutputName.TabIndex = 24;
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Location = new System.Drawing.Point(203, 205);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(75, 23);
            this.btnOpenDir.TabIndex = 25;
            this.btnOpenDir.Text = "打开目录";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // btnOpenFileInTxt
            // 
            this.btnOpenFileInTxt.Location = new System.Drawing.Point(294, 205);
            this.btnOpenFileInTxt.Name = "btnOpenFileInTxt";
            this.btnOpenFileInTxt.Size = new System.Drawing.Size(88, 23);
            this.btnOpenFileInTxt.TabIndex = 26;
            this.btnOpenFileInTxt.Text = "文本方式打开";
            this.btnOpenFileInTxt.UseVisualStyleBackColor = true;
            this.btnOpenFileInTxt.Click += new System.EventHandler(this.btnOpenFileInTxt_Click);
            // 
            // btnOpenFileInDefault
            // 
            this.btnOpenFileInDefault.Location = new System.Drawing.Point(401, 205);
            this.btnOpenFileInDefault.Name = "btnOpenFileInDefault";
            this.btnOpenFileInDefault.Size = new System.Drawing.Size(88, 23);
            this.btnOpenFileInDefault.TabIndex = 26;
            this.btnOpenFileInDefault.Text = "默认方式打开";
            this.btnOpenFileInDefault.UseVisualStyleBackColor = true;
            this.btnOpenFileInDefault.Click += new System.EventHandler(this.btnOpenFileInDefault_Click);
            // 
            // cbDbType
            // 
            this.cbDbType.FormattingEnabled = true;
            this.cbDbType.Items.AddRange(new object[] {
            "MSSQL",
            "MySQL"});
            this.cbDbType.Location = new System.Drawing.Point(74, 170);
            this.cbDbType.Name = "cbDbType";
            this.cbDbType.Size = new System.Drawing.Size(121, 20);
            this.cbDbType.TabIndex = 27;
            this.cbDbType.Text = "MSSQL";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "数据库：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 262);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbDbType);
            this.Controls.Add(this.btnOpenFileInDefault);
            this.Controls.Add(this.btnOpenFileInTxt);
            this.Controls.Add(this.btnOpenDir);
            this.Controls.Add(this.tbOutputName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDllName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.tbOutputPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbDllPath);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "权限脚本生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDllName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.TextBox tbOutputPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDllPath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbOutputName;
        private System.Windows.Forms.Button btnOpenDir;
        private System.Windows.Forms.Button btnOpenFileInTxt;
        private System.Windows.Forms.Button btnOpenFileInDefault;
        private System.Windows.Forms.ComboBox cbDbType;
        private System.Windows.Forms.Label label8;
    }
}

