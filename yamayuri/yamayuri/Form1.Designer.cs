namespace yamayuri
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
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.input_files_listbox = new System.Windows.Forms.ListBox();
            this.file_choosing_button = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.file_remove_button = new System.Windows.Forms.Button();
            this.files_clear_button = new System.Windows.Forms.Button();
            this.target_listBox = new System.Windows.Forms.CheckedListBox();
            this.all_choose_button = new System.Windows.Forms.Button();
            this.all_clear_button = new System.Windows.Forms.Button();
            this.output_format_panel = new System.Windows.Forms.Panel();
            this.output_format_label = new System.Windows.Forms.Label();
            this.output_sql_radioButton = new System.Windows.Forms.RadioButton();
            this.output_csv_radioButton = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.save_forder_path = new System.Windows.Forms.Label();
            this.save_forder_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.save_name = new System.Windows.Forms.TextBox();
            this.save_name_label = new System.Windows.Forms.Label();
            this.header_textBox = new System.Windows.Forms.TextBox();
            this.header_label = new System.Windows.Forms.Label();
            this.numbers_button = new System.Windows.Forms.Button();
            this.number_name_button = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.ngram_label = new System.Windows.Forms.Label();
            this.span_label = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.max_label = new System.Windows.Forms.Label();
            this.min_label = new System.Windows.Forms.Label();
            this.header_err_msg = new System.Windows.Forms.Label();
            this.ngram_err_msg = new System.Windows.Forms.Label();
            this.span_err_msg = new System.Windows.Forms.Label();
            this.output_format_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // input_files_listbox
            // 
            this.input_files_listbox.AllowDrop = true;
            this.input_files_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input_files_listbox.FormattingEnabled = true;
            this.input_files_listbox.ItemHeight = 12;
            this.input_files_listbox.Location = new System.Drawing.Point(12, 41);
            this.input_files_listbox.Name = "input_files_listbox";
            this.input_files_listbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.input_files_listbox.Size = new System.Drawing.Size(555, 76);
            this.input_files_listbox.Sorted = true;
            this.input_files_listbox.TabIndex = 2;
            this.input_files_listbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.input_files_listbox.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
            // 
            // file_choosing_button
            // 
            this.file_choosing_button.AutoSize = true;
            this.file_choosing_button.Location = new System.Drawing.Point(12, 12);
            this.file_choosing_button.Name = "file_choosing_button";
            this.file_choosing_button.Size = new System.Drawing.Size(108, 24);
            this.file_choosing_button.TabIndex = 1;
            this.file_choosing_button.Text = "処理ファイルを選択";
            this.file_choosing_button.UseCompatibleTextRendering = true;
            this.file_choosing_button.UseVisualStyleBackColor = true;
            this.file_choosing_button.Click += new System.EventHandler(this.file_choosing_button_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "\"テキストファイル（*.txt）|*.txt|htmlファイル（*.html;*.htm;*.xhtml）|*.html;*.htm;*.xhtml|xmlファイ" +
    "ル（*.xml）|*.xml|すべてのファイル（*.*）|*.*\"";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ReadOnlyChecked = true;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.ShowReadOnly = true;
            this.openFileDialog.SupportMultiDottedExtensions = true;
            this.openFileDialog.Title = "処理ファイルを選択";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // file_remove_button
            // 
            this.file_remove_button.AutoSize = true;
            this.file_remove_button.Location = new System.Drawing.Point(124, 12);
            this.file_remove_button.Name = "file_remove_button";
            this.file_remove_button.Size = new System.Drawing.Size(106, 23);
            this.file_remove_button.TabIndex = 0;
            this.file_remove_button.TabStop = false;
            this.file_remove_button.Text = "選択ファイルを削除";
            this.file_remove_button.UseVisualStyleBackColor = true;
            this.file_remove_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // files_clear_button
            // 
            this.files_clear_button.AutoSize = true;
            this.files_clear_button.Location = new System.Drawing.Point(236, 12);
            this.files_clear_button.Name = "files_clear_button";
            this.files_clear_button.Size = new System.Drawing.Size(130, 23);
            this.files_clear_button.TabIndex = 0;
            this.files_clear_button.TabStop = false;
            this.files_clear_button.Text = "処理ファイルリストをクリア";
            this.files_clear_button.UseVisualStyleBackColor = true;
            this.files_clear_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // target_listBox
            // 
            this.target_listBox.CheckOnClick = true;
            this.target_listBox.FormattingEnabled = true;
            this.target_listBox.Items.AddRange(new object[] {
            "N-gram（文字ー書字形）",
            "N-gram（単語ー書字形）",
            "N-gram（単語ー語彙素形）",
            "MI（単語ー書字形ー前方共起）",
            "MI（単語ー語彙素形ー前方共起）",
            "MI（単語ー書字形ー後方共起）",
            "MI（単語ー語彙素形ー後方共起）",
            "T（単語ー書字形ー前方共起）",
            "T（単語ー語彙素形ー前方共起）",
            "T（単語ー書字形ー後方共起）",
            "T（単語ー語彙素形ー後方共起）",
            "共起頻度（単語ー書字形ー前方共起）",
            "共起頻度（単語ー語彙素形ー前方共起）",
            "共起頻度（単語ー書字形ー後方共起）",
            "共起頻度（単語ー語彙素形ー後方共起）"});
            this.target_listBox.Location = new System.Drawing.Point(12, 194);
            this.target_listBox.Name = "target_listBox";
            this.target_listBox.Size = new System.Drawing.Size(224, 214);
            this.target_listBox.TabIndex = 5;
            this.target_listBox.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_ItemCheck);
            // 
            // all_choose_button
            // 
            this.all_choose_button.Location = new System.Drawing.Point(249, 300);
            this.all_choose_button.Name = "all_choose_button";
            this.all_choose_button.Size = new System.Drawing.Size(61, 23);
            this.all_choose_button.TabIndex = 6;
            this.all_choose_button.Text = "全て選択";
            this.all_choose_button.UseVisualStyleBackColor = true;
            this.all_choose_button.Click += new System.EventHandler(this.button4_Click);
            // 
            // all_clear_button
            // 
            this.all_clear_button.Location = new System.Drawing.Point(316, 300);
            this.all_clear_button.Name = "all_clear_button";
            this.all_clear_button.Size = new System.Drawing.Size(62, 23);
            this.all_clear_button.TabIndex = 7;
            this.all_clear_button.Text = "全て解除";
            this.all_clear_button.UseVisualStyleBackColor = true;
            this.all_clear_button.Click += new System.EventHandler(this.button5_Click);
            // 
            // output_format_panel
            // 
            this.output_format_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output_format_panel.Controls.Add(this.output_format_label);
            this.output_format_panel.Controls.Add(this.output_sql_radioButton);
            this.output_format_panel.Controls.Add(this.output_csv_radioButton);
            this.output_format_panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.output_format_panel.Location = new System.Drawing.Point(249, 338);
            this.output_format_panel.Name = "output_format_panel";
            this.output_format_panel.Size = new System.Drawing.Size(129, 61);
            this.output_format_panel.TabIndex = 17;
            // 
            // output_format_label
            // 
            this.output_format_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.output_format_label.AutoSize = true;
            this.output_format_label.Location = new System.Drawing.Point(3, 14);
            this.output_format_label.Name = "output_format_label";
            this.output_format_label.Size = new System.Drawing.Size(53, 12);
            this.output_format_label.TabIndex = 13;
            this.output_format_label.Text = "出力形式";
            // 
            // output_sql_radioButton
            // 
            this.output_sql_radioButton.AutoSize = true;
            this.output_sql_radioButton.Location = new System.Drawing.Point(66, 29);
            this.output_sql_radioButton.Name = "output_sql_radioButton";
            this.output_sql_radioButton.Size = new System.Drawing.Size(44, 16);
            this.output_sql_radioButton.TabIndex = 13;
            this.output_sql_radioButton.Text = "SQL";
            this.output_sql_radioButton.UseVisualStyleBackColor = true;
            this.output_sql_radioButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // output_csv_radioButton
            // 
            this.output_csv_radioButton.AutoSize = true;
            this.output_csv_radioButton.Checked = true;
            this.output_csv_radioButton.Location = new System.Drawing.Point(10, 29);
            this.output_csv_radioButton.Name = "output_csv_radioButton";
            this.output_csv_radioButton.Size = new System.Drawing.Size(46, 16);
            this.output_csv_radioButton.TabIndex = 12;
            this.output_csv_radioButton.TabStop = true;
            this.output_csv_radioButton.Text = "CSV";
            this.output_csv_radioButton.UseVisualStyleBackColor = true;
            this.output_csv_radioButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.folderBrowserDialog.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // save_forder_path
            // 
            this.save_forder_path.AutoEllipsis = true;
            this.save_forder_path.AutoSize = true;
            this.save_forder_path.Location = new System.Drawing.Point(136, 419);
            this.save_forder_path.Name = "save_forder_path";
            this.save_forder_path.Size = new System.Drawing.Size(0, 12);
            this.save_forder_path.TabIndex = 18;
            // 
            // save_forder_button
            // 
            this.save_forder_button.AutoSize = true;
            this.save_forder_button.Location = new System.Drawing.Point(10, 414);
            this.save_forder_button.Name = "save_forder_button";
            this.save_forder_button.Size = new System.Drawing.Size(120, 23);
            this.save_forder_button.TabIndex = 14;
            this.save_forder_button.Text = "保存先フォルダの選択";
            this.save_forder_button.UseVisualStyleBackColor = true;
            this.save_forder_button.Click += new System.EventHandler(this.button6_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(404, 338);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(163, 61);
            this.start_button.TabIndex = 16;
            this.start_button.Text = "解析開始";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.button7_Click);
            // 
            // save_name
            // 
            this.save_name.Location = new System.Drawing.Point(160, 441);
            this.save_name.Name = "save_name";
            this.save_name.Size = new System.Drawing.Size(100, 19);
            this.save_name.TabIndex = 15;
            // 
            // save_name_label
            // 
            this.save_name_label.AutoSize = true;
            this.save_name_label.Location = new System.Drawing.Point(12, 444);
            this.save_name_label.Name = "save_name_label";
            this.save_name_label.Size = new System.Drawing.Size(142, 12);
            this.save_name_label.TabIndex = 0;
            this.save_name_label.Text = "CSV共通タイトル（オプション）";
            // 
            // header_textBox
            // 
            this.header_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.header_textBox.Location = new System.Drawing.Point(12, 152);
            this.header_textBox.Multiline = true;
            this.header_textBox.Name = "header_textBox";
            this.header_textBox.ReadOnly = true;
            this.header_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.header_textBox.Size = new System.Drawing.Size(555, 36);
            this.header_textBox.TabIndex = 23;
            // 
            // header_label
            // 
            this.header_label.AutoSize = true;
            this.header_label.Location = new System.Drawing.Point(10, 129);
            this.header_label.Name = "header_label";
            this.header_label.Size = new System.Drawing.Size(31, 12);
            this.header_label.TabIndex = 0;
            this.header_label.Text = "ヘッダ";
            // 
            // numbers_button
            // 
            this.numbers_button.AutoSize = true;
            this.numbers_button.Location = new System.Drawing.Point(47, 124);
            this.numbers_button.Name = "numbers_button";
            this.numbers_button.Size = new System.Drawing.Size(75, 23);
            this.numbers_button.TabIndex = 3;
            this.numbers_button.Text = "連番";
            this.numbers_button.UseVisualStyleBackColor = true;
            this.numbers_button.Click += new System.EventHandler(this.button8_Click);
            // 
            // number_name_button
            // 
            this.number_name_button.AutoSize = true;
            this.number_name_button.Location = new System.Drawing.Point(128, 124);
            this.number_name_button.Name = "number_name_button";
            this.number_name_button.Size = new System.Drawing.Size(91, 23);
            this.number_name_button.TabIndex = 4;
            this.number_name_button.Text = "連番+ファイル名";
            this.number_name_button.UseVisualStyleBackColor = true;
            this.number_name_button.Click += new System.EventHandler(this.button10_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(308, 229);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 19);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Enabled = false;
            this.numericUpDown2.Location = new System.Drawing.Point(361, 229);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(47, 19);
            this.numericUpDown2.TabIndex = 9;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // ngram_label
            // 
            this.ngram_label.AutoEllipsis = true;
            this.ngram_label.AutoSize = true;
            this.ngram_label.Location = new System.Drawing.Point(264, 231);
            this.ngram_label.Name = "ngram_label";
            this.ngram_label.Size = new System.Drawing.Size(38, 12);
            this.ngram_label.TabIndex = 31;
            this.ngram_label.Text = "Ngram";
            // 
            // span_label
            // 
            this.span_label.AutoEllipsis = true;
            this.span_label.AutoSize = true;
            this.span_label.Location = new System.Drawing.Point(264, 267);
            this.span_label.Name = "span_label";
            this.span_label.Size = new System.Drawing.Size(33, 12);
            this.span_label.TabIndex = 34;
            this.span_label.Text = "スパン";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Enabled = false;
            this.numericUpDown3.Location = new System.Drawing.Point(308, 265);
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(47, 19);
            this.numericUpDown3.TabIndex = 10;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Enabled = false;
            this.numericUpDown4.Location = new System.Drawing.Point(361, 265);
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(47, 19);
            this.numericUpDown4.TabIndex = 11;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // max_label
            // 
            this.max_label.AutoSize = true;
            this.max_label.Location = new System.Drawing.Point(361, 214);
            this.max_label.Name = "max_label";
            this.max_label.Size = new System.Drawing.Size(41, 12);
            this.max_label.TabIndex = 35;
            this.max_label.Text = "最大値";
            // 
            // min_label
            // 
            this.min_label.AutoSize = true;
            this.min_label.Location = new System.Drawing.Point(314, 214);
            this.min_label.Name = "min_label";
            this.min_label.Size = new System.Drawing.Size(41, 12);
            this.min_label.TabIndex = 36;
            this.min_label.Text = "最小値";
            // 
            // header_err_msg
            // 
            this.header_err_msg.AutoSize = true;
            this.header_err_msg.ForeColor = System.Drawing.Color.Red;
            this.header_err_msg.Location = new System.Drawing.Point(226, 129);
            this.header_err_msg.Name = "header_err_msg";
            this.header_err_msg.Size = new System.Drawing.Size(123, 12);
            this.header_err_msg.TabIndex = 0;
            this.header_err_msg.Text = "ヘッダを更新してください。";
            this.header_err_msg.Visible = false;
            // 
            // ngram_err_msg
            // 
            this.ngram_err_msg.AutoSize = true;
            this.ngram_err_msg.ForeColor = System.Drawing.Color.Red;
            this.ngram_err_msg.Location = new System.Drawing.Point(414, 231);
            this.ngram_err_msg.Name = "ngram_err_msg";
            this.ngram_err_msg.Size = new System.Drawing.Size(89, 12);
            this.ngram_err_msg.TabIndex = 0;
            this.ngram_err_msg.Text = "最小値＞最大値";
            this.ngram_err_msg.Visible = false;
            // 
            // span_err_msg
            // 
            this.span_err_msg.AutoSize = true;
            this.span_err_msg.ForeColor = System.Drawing.Color.Red;
            this.span_err_msg.Location = new System.Drawing.Point(414, 267);
            this.span_err_msg.Name = "span_err_msg";
            this.span_err_msg.Size = new System.Drawing.Size(89, 12);
            this.span_err_msg.TabIndex = 0;
            this.span_err_msg.Text = "最小値＞最大値";
            this.span_err_msg.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(579, 471);
            this.Controls.Add(this.span_err_msg);
            this.Controls.Add(this.ngram_err_msg);
            this.Controls.Add(this.header_err_msg);
            this.Controls.Add(this.min_label);
            this.Controls.Add(this.max_label);
            this.Controls.Add(this.span_label);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.ngram_label);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.number_name_button);
            this.Controls.Add(this.numbers_button);
            this.Controls.Add(this.header_label);
            this.Controls.Add(this.header_textBox);
            this.Controls.Add(this.save_name_label);
            this.Controls.Add(this.save_name);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.save_forder_button);
            this.Controls.Add(this.save_forder_path);
            this.Controls.Add(this.output_format_panel);
            this.Controls.Add(this.all_clear_button);
            this.Controls.Add(this.all_choose_button);
            this.Controls.Add(this.target_listBox);
            this.Controls.Add(this.files_clear_button);
            this.Controls.Add(this.file_remove_button);
            this.Controls.Add(this.file_choosing_button);
            this.Controls.Add(this.input_files_listbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Yamayuri";
            this.output_format_panel.ResumeLayout(false);
            this.output_format_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox input_files_listbox;
        private System.Windows.Forms.Button file_choosing_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button file_remove_button;
        private System.Windows.Forms.Button files_clear_button;
        private System.Windows.Forms.CheckedListBox target_listBox;
        private System.Windows.Forms.Button all_choose_button;
        private System.Windows.Forms.Button all_clear_button;
        private System.Windows.Forms.Panel output_format_panel;
        private System.Windows.Forms.RadioButton output_csv_radioButton;
        private System.Windows.Forms.RadioButton output_sql_radioButton;
        private System.Windows.Forms.Label output_format_label;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label save_forder_path;
        private System.Windows.Forms.Button save_forder_button;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.TextBox save_name;
        private System.Windows.Forms.Label save_name_label;
        private System.Windows.Forms.TextBox header_textBox;
        private System.Windows.Forms.Label header_label;
        private System.Windows.Forms.Button numbers_button;
        private System.Windows.Forms.Button number_name_button;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label ngram_label;
        private System.Windows.Forms.Label span_label;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label max_label;
        private System.Windows.Forms.Label min_label;
        private System.Windows.Forms.Label header_err_msg;
        private System.Windows.Forms.Label ngram_err_msg;
        private System.Windows.Forms.Label span_err_msg;
    }
}

