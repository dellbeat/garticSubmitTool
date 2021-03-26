
namespace garticSubmitTool
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.JsonText = new System.Windows.Forms.TextBox();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.GetWordListBtn = new System.Windows.Forms.Button();
            this.WordLV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClearListBtn = new System.Windows.Forms.Button();
            this.AddListBtn = new System.Windows.Forms.Button();
            this.UserImagePic = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UserImagePic)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gartic.io登录后JSON-Cookie";
            // 
            // JsonText
            // 
            this.JsonText.Location = new System.Drawing.Point(12, 231);
            this.JsonText.Multiline = true;
            this.JsonText.Name = "JsonText";
            this.JsonText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.JsonText.Size = new System.Drawing.Size(298, 161);
            this.JsonText.TabIndex = 1;
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Location = new System.Drawing.Point(115, 415);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(75, 23);
            this.ApplyBtn.TabIndex = 2;
            this.ApplyBtn.Text = "应用Json";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // GetWordListBtn
            // 
            this.GetWordListBtn.Location = new System.Drawing.Point(375, 415);
            this.GetWordListBtn.Name = "GetWordListBtn";
            this.GetWordListBtn.Size = new System.Drawing.Size(106, 23);
            this.GetWordListBtn.TabIndex = 3;
            this.GetWordListBtn.Text = "获取词库列表";
            this.GetWordListBtn.UseVisualStyleBackColor = true;
            this.GetWordListBtn.Click += new System.EventHandler(this.GetWordListBtn_Click);
            // 
            // WordLV
            // 
            this.WordLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.WordLV.HideSelection = false;
            this.WordLV.Location = new System.Drawing.Point(337, 34);
            this.WordLV.Name = "WordLV";
            this.WordLV.Size = new System.Drawing.Size(187, 358);
            this.WordLV.TabIndex = 4;
            this.WordLV.UseCompatibleStateImageBehavior = false;
            this.WordLV.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "单词";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "难度代码";
            this.columnHeader2.Width = 70;
            // 
            // ClearListBtn
            // 
            this.ClearListBtn.Location = new System.Drawing.Point(584, 126);
            this.ClearListBtn.Name = "ClearListBtn";
            this.ClearListBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearListBtn.TabIndex = 5;
            this.ClearListBtn.Text = "清空词库";
            this.ClearListBtn.UseVisualStyleBackColor = true;
            this.ClearListBtn.Click += new System.EventHandler(this.ClearListBtn_Click);
            // 
            // AddListBtn
            // 
            this.AddListBtn.Location = new System.Drawing.Point(584, 273);
            this.AddListBtn.Name = "AddListBtn";
            this.AddListBtn.Size = new System.Drawing.Size(75, 23);
            this.AddListBtn.TabIndex = 6;
            this.AddListBtn.Text = "追加词库";
            this.AddListBtn.UseVisualStyleBackColor = true;
            this.AddListBtn.Click += new System.EventHandler(this.AddListBtn_Click);
            // 
            // UserImagePic
            // 
            this.UserImagePic.Image = global::garticSubmitTool.Properties.Resources.DefaultImage;
            this.UserImagePic.Location = new System.Drawing.Point(43, 20);
            this.UserImagePic.Name = "UserImagePic";
            this.UserImagePic.Size = new System.Drawing.Size(100, 100);
            this.UserImagePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UserImagePic.TabIndex = 7;
            this.UserImagePic.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UserNameLabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.UserImagePic);
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 163);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "昵称";
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(53, 132);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(41, 12);
            this.UserNameLabel.TabIndex = 9;
            this.UserNameLabel.Text = "未登录";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddListBtn);
            this.Controls.Add(this.ClearListBtn);
            this.Controls.Add(this.WordLV);
            this.Controls.Add(this.GetWordListBtn);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.JsonText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gartic.io 自定义词库导入工具";
            ((System.ComponentModel.ISupportInitialize)(this.UserImagePic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JsonText;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Button GetWordListBtn;
        private System.Windows.Forms.ListView WordLV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button ClearListBtn;
        private System.Windows.Forms.Button AddListBtn;
        private System.Windows.Forms.PictureBox UserImagePic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label label2;
    }
}

