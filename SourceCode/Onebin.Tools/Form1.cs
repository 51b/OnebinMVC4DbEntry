using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace Onebin.Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tbDllName.Text = ConfigurationManager.AppSettings["DllName"];
            tbOutputName.Text = ConfigurationManager.AppSettings["OutputName"];
            tbDllPath.Text = ConfigurationManager.AppSettings["DllPath"];
            tbOutputPath.Text = ConfigurationManager.AppSettings["OutputPath"];
            cbDbType.Text = ConfigurationManager.AppSettings["DbType"];
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string msg = "";
            ISqlStatement sqlStatement = null;
            switch (cbDbType.Text)
            {
                case "MSSQL":
                    sqlStatement = new MSSQL();
                    break;
                case "MySQL":
                    sqlStatement = new MySQL();
                    break;
                default:
                    msg += "不支持数据库【" + cbDbType.Text + "】的脚本生成！\n";
                    break;
            }
            if (string.IsNullOrWhiteSpace(tbDllName.Text))
            {
                msg += "请输入DLL的文件全称！\n";
            }
            if (string.IsNullOrWhiteSpace(tbDllPath.Text))
            {
                msg += "请输入DLL的文件的存放路径！\n";
            }
            if (string.IsNullOrWhiteSpace(tbOutputPath.Text))
            {
                msg += "请输入输出路径！\n";
            }
            if (string.IsNullOrWhiteSpace(tbOutputName.Text))
            {
                msg += "请输入输出的文件名！\n";
            }
            if (string.IsNullOrWhiteSpace(msg))
            {
                new GenerateSQL(tbDllPath.Text, tbOutputPath.Text, tbDllName.Text, tbOutputName.Text, sqlStatement).CreateScript(out msg);
            }
            MessageBox.Show(msg);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            tbOutputPath.Text = tbDllPath.Text;
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            if (IsDirExists())
            {
                Process.Start("explorer.exe", tbOutputPath.Text);
            }
        }

        private void btnOpenFileInTxt_Click(object sender, EventArgs e)
        {
            if (IsFileExists())
            {
                Process.Start("notepad.exe", tbOutputPath.Text + "\\" + this.tbOutputName.Text);
            }
        }

        private void btnOpenFileInDefault_Click(object sender, EventArgs e)
        {
            if (IsFileExists())
            {
                Process.Start(tbOutputPath.Text + "\\" + this.tbOutputName.Text);
            }
        }

        private bool IsDirExists()
        {
            if (Directory.Exists(this.tbOutputPath.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("文件路径不存在！");
                return false;
            }
        }

        private bool IsFileExists()
        {
            if (IsDirExists())
            {
                if (File.Exists(this.tbOutputPath.Text + "\\" + this.tbOutputName.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("文件不存在！");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
