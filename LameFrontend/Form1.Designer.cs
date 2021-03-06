﻿namespace OggencFrontend
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.showSetting = new System.Windows.Forms.Button();
            this.startEnc = new System.Windows.Forms.Button();
            this.addFile = new System.Windows.Forms.Button();
            this.openAudio = new System.Windows.Forms.OpenFileDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.deleteFromList = new System.Windows.Forms.Button();
            this.openLame = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ghostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // showSetting
            // 
            this.showSetting.Location = new System.Drawing.Point(269, 23);
            this.showSetting.Name = "showSetting";
            this.showSetting.Size = new System.Drawing.Size(86, 23);
            this.showSetting.TabIndex = 0;
            this.showSetting.Text = "設定";
            this.showSetting.UseVisualStyleBackColor = true;
            this.showSetting.Click += new System.EventHandler(this.showSetting_Click);
            // 
            // startEnc
            // 
            this.startEnc.Location = new System.Drawing.Point(269, 249);
            this.startEnc.Name = "startEnc";
            this.startEnc.Size = new System.Drawing.Size(86, 23);
            this.startEnc.TabIndex = 1;
            this.startEnc.Text = "エンコード開始";
            this.startEnc.UseVisualStyleBackColor = true;
            this.startEnc.Click += new System.EventHandler(this.button2_Click);
            // 
            // addFile
            // 
            this.addFile.Location = new System.Drawing.Point(269, 159);
            this.addFile.Name = "addFile";
            this.addFile.Size = new System.Drawing.Size(86, 23);
            this.addFile.TabIndex = 2;
            this.addFile.Text = "ファイル追加";
            this.addFile.UseVisualStyleBackColor = true;
            this.addFile.Click += new System.EventHandler(this.addFile_Click);
            // 
            // openAudio
            // 
            this.openAudio.Multiselect = true;
            this.openAudio.FileOk += new System.ComponentModel.CancelEventHandler(this.openAudio_FileOk);
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(13, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(234, 244);
            this.listBox1.TabIndex = 3;
            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            // 
            // deleteFromList
            // 
            this.deleteFromList.Location = new System.Drawing.Point(269, 189);
            this.deleteFromList.Name = "deleteFromList";
            this.deleteFromList.Size = new System.Drawing.Size(86, 23);
            this.deleteFromList.TabIndex = 4;
            this.deleteFromList.Text = "リストから削除";
            this.deleteFromList.UseVisualStyleBackColor = true;
            this.deleteFromList.Click += new System.EventHandler(this.deleteFromList_Click);
            // 
            // openLame
            // 
            this.openLame.FileName = "lame.exe";
            this.openLame.Filter = "Lame exe|lame.exe";
            this.openLame.FileOk += new System.ComponentModel.CancelEventHandler(this.openOggEnc_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ghostToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(367, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // ghostToolStripMenuItem
            // 
            this.ghostToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.ghostToolStripMenuItem.Name = "ghostToolStripMenuItem";
            this.ghostToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.ghostToolStripMenuItem.Text = "Ghost";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 284);
            this.Controls.Add(this.deleteFromList);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.addFile);
            this.Controls.Add(this.startEnc);
            this.Controls.Add(this.showSetting);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "LameGui";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showSetting;
        private System.Windows.Forms.Button startEnc;
        private System.Windows.Forms.Button addFile;
        private System.Windows.Forms.OpenFileDialog openAudio;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button deleteFromList;
        private System.Windows.Forms.OpenFileDialog openLame;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ghostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}

