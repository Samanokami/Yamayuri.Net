using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using NMeCab;
using System.Data.SQLite;

namespace yamayuri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            input_files_listbox.AllowDrop = true;
        }

        //「処理ファイルを選択」ボタンの動作
        private void file_choosing_button_Click(object sender, EventArgs e)
        {           
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string names = null;
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK){
                input_files_listbox.BeginUpdate();
                foreach (string file in openFileDialog.FileNames){
                    //リストボックスにファイル名を追加、重複はスキップ
                    if (input_files_listbox.FindStringExact(file) == ListBox.NoMatches)
                        input_files_listbox.Items.Add(file);
                    else
                        names = names + file + ",";
                }
                input_files_listbox.EndUpdate();

                //スキップしたファイル名をメッセージボックスで出力
                if (names != null){
                    MessageBox.Show("すでに選択されているファイル（" + names + "）はスキップされました。");
                    names = null;
                }
            }
            header_err_msg.Visible = true;
        }

        //ファイルを開くダイアログ
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        //リストボックスへのドラッグアンドドロップの動作
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string names = null;
            input_files_listbox.BeginUpdate();
            string[] filename = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < filename.Length; i++)
            {
                //リストボックスにファイル名を追加、重複はスキップ
                if (input_files_listbox.FindStringExact(filename[i])==ListBox.NoMatches)
                    input_files_listbox.Items.Add(filename[i]);
                else
                    names = names + filename[i];              

            }
            input_files_listbox.EndUpdate();
            //スキップしたファイル名をメッセージボックスで出力
            if (names != null)
            {
                MessageBox.Show("すでに選択されているファイル（" + names + "）はスキップされました。");
                names = null;
            }

        }
        
        //リストボックスへのドラッグアンドドロップの動作
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                e.Effect = DragDropEffects.Copy;
            }
            else{
                e.Effect = DragDropEffects.None;
            }
        }

        //「選択ファイルを削除」ボタンの動作
        private void button2_Click(object sender, EventArgs e)
        {
            input_files_listbox.BeginUpdate();
            for (int i=input_files_listbox.SelectedItems.Count-1 ; i>=0 ; i--)
            {
                input_files_listbox.Items.Remove(input_files_listbox.SelectedItems[i]);
            }
            input_files_listbox.EndUpdate();
            header_err_msg.Visible = true;
        }

        //「処理ファイルリストをクリア」ボタンの動作
        private void button3_Click(object sender, EventArgs e)
        {
            input_files_listbox.Items.Clear();
            header_err_msg.Visible = true;
        }

        //「連番」ボタンの動作
        private void button8_Click(object sender, EventArgs e)
        {
            header_err_msg.Visible = false;

            if (header_textBox.Text != "")
            {
                header_textBox.Clear();
            }

            for (int num_of_files = 1; num_of_files<= input_files_listbox.Items.Count; num_of_files++)
            {
                header_textBox.Text = header_textBox.Text + num_of_files.ToString("0000") + ",";
            }
            header_textBox.Text = System.Text.RegularExpressions.Regex.Replace(header_textBox.Text, ",$", "");
        }

        //「連番＋ファイル名」ボタンの動作
        private void button10_Click(object sender, EventArgs e)
        {
            header_err_msg.Visible = false;

            if (header_textBox.Text != "")
            {
                header_textBox.Clear();
            }

            for (int num_of_files = 1; num_of_files <= input_files_listbox.Items.Count; num_of_files++)
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension((string)input_files_listbox.Items[num_of_files - 1]);
                header_textBox.Text = header_textBox.Text + num_of_files.ToString("0000") + filename + ",";
            }
            header_textBox.Text = System.Text.RegularExpressions.Regex.Replace(header_textBox.Text, ",$", "");
        }

        //チェックリストボックス
        private void checkedListBox1_ItemCheck(object sender, EventArgs e)
        {
            if(
                (target_listBox.GetItemChecked(0)==true)||
                (target_listBox.GetItemChecked(1)==true)||
                (target_listBox.GetItemChecked(2)==true)
            )
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;

            }
            if (
                (target_listBox.GetItemChecked(3) == true) ||
                (target_listBox.GetItemChecked(4) == true) ||
                (target_listBox.GetItemChecked(5) == true) ||
                (target_listBox.GetItemChecked(6) == true) ||
                (target_listBox.GetItemChecked(7) == true) ||
                (target_listBox.GetItemChecked(8) == true) ||
                (target_listBox.GetItemChecked(9) == true) ||
                (target_listBox.GetItemChecked(10) == true) ||
                (target_listBox.GetItemChecked(11) == true) ||
                (target_listBox.GetItemChecked(12) == true) ||
                (target_listBox.GetItemChecked(13) == true) ||
                (target_listBox.GetItemChecked(14) == true)

            )
            {
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
            }
            else
            {
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;

            }

        }

        //「全て選択」ボタンの動作
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < target_listBox.Items.Count; i++)
            {
                target_listBox.SetItemChecked(i,true);
            }

            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
            numericUpDown4.Enabled = true;
        }

        //「全て解除」ボタンの動作
        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < target_listBox.Items.Count; i++)
            {
                target_listBox.SetItemChecked(i, false);
            } 
            
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
        }

        //「フォルダの選択」ボタンの動作
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                save_forder_path.Text = folderBrowserDialog.SelectedPath + "\\";
            }
        }

        //フォルダ選択ダイアログ
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        }

        //「最小値・最大値」設定コントロールの動作
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > numericUpDown2.Value) { ngram_err_msg.Visible = true; }
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Minimum = numericUpDown1.Value;
            if (numericUpDown2.Value >= numericUpDown1.Value) { ngram_err_msg.Visible = false; }
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value > numericUpDown4.Value) { span_err_msg.Visible = true; }
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown4.Minimum = numericUpDown3.Value;
            if (numericUpDown4.Value >= numericUpDown3.Value) { span_err_msg.Visible = false; }
        }

        //「解析開始」ボタンの動作
        private void button7_Click(object sender, EventArgs e)
        {
            string yamayuri_error_message = "";
            if (input_files_listbox.Items.Count == 0) { yamayuri_error_message = "処理ファイルが選択されていません。" + Environment.NewLine; }
            if (header_err_msg.Visible == true) { yamayuri_error_message = yamayuri_error_message + "ヘッダ・カラム名が更新されていません。" + Environment.NewLine; }
            if (target_listBox.CheckedItems.Count == 0) { yamayuri_error_message = yamayuri_error_message + "採取項目が選択されていません。" + Environment.NewLine; }
            if (save_forder_path.Text == "") { yamayuri_error_message = yamayuri_error_message + "保存先フォルダが指定されていません。" + Environment.NewLine; }
            if ((save_name.Text == "")&&(output_sql_radioButton.Checked)) { yamayuri_error_message = yamayuri_error_message + "データベース名が設定されていません。" + Environment.NewLine; }
            if ((output_csv_radioButton.Checked == false) && (output_sql_radioButton.Checked == false)) { yamayuri_error_message = yamayuri_error_message + "出力形式が選択されていません。" + Environment.NewLine; }
            if (ngram_err_msg.Visible == true) { yamayuri_error_message = yamayuri_error_message + "N-gram の最大値と最小値を見直してください。" + Environment.NewLine; }
            if (span_err_msg.Visible == true) { yamayuri_error_message = yamayuri_error_message + "スパンの最大値と最小値を見直してください。"; }
            if (yamayuri_error_message != "")
            {
                MessageBox.Show(yamayuri_error_message, "エラー！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                //Nの最小値と最大値
                decimal n_min = numericUpDown1.Value;
                decimal n_max = numericUpDown2.Value;
                
                //スパンの最小値と最大値
                decimal span_min = numericUpDown3.Value * 2 + 1;
                decimal span_max = numericUpDown4.Value * 2 + 1;

                //ヘッダの設定
                string[] header_names = header_textBox.Text.Split(',');
                int header_num = 0;

                //書き込み先ファイル・データベースの設定
                string[] writing_file_name = new string[15];
                string[] writing_table_name = new string[15];
                string db_name = save_forder_path.Text + save_name.Text + ".ymyr";
                database_access data_base = new database_access();

                if (output_csv_radioButton.Checked)
                {
                    if (save_name.Text == "")
                    {
                        writing_file_name[0] = save_forder_path.Text + "ngram_moji.csv";
                        writing_file_name[1] = save_forder_path.Text + "ngram_syoji.csv";
                        writing_file_name[2] = save_forder_path.Text + "ngram_goiso.csv";
                        writing_file_name[3] = save_forder_path.Text + "mi_syoji_before.csv";
                        writing_file_name[4] = save_forder_path.Text + "mi_goiso_before.csv";
                        writing_file_name[5] = save_forder_path.Text + "mi_syoji_after.csv";
                        writing_file_name[6] = save_forder_path.Text + "mi_goiso_after.csv";
                        writing_file_name[7] = save_forder_path.Text + "t_syoji_before.csv";
                        writing_file_name[8] = save_forder_path.Text + "t_goiso_before.csv";
                        writing_file_name[9] = save_forder_path.Text + "t_syoji_after.csv";
                        writing_file_name[10] = save_forder_path.Text + "t_goiso_after.csv";
                        writing_file_name[11] = save_forder_path.Text + "freq_syoji_before.csv";
                        writing_file_name[12] = save_forder_path.Text + "freq_goiso_before.csv";
                        writing_file_name[13] = save_forder_path.Text + "freq_syoji_after.csv";
                        writing_file_name[14] = save_forder_path.Text + "freq_goiso_after.csv";
                    }
                    else
                    {
                        writing_file_name[0] = save_forder_path.Text + save_name.Text + "_ngram_moji.csv";
                        writing_file_name[1] = save_forder_path.Text + save_name.Text + "_ngram_syoji.csv";
                        writing_file_name[2] = save_forder_path.Text + save_name.Text + "_ngram_goiso.csv";
                        writing_file_name[3] = save_forder_path.Text + save_name.Text + "_mi_syoji_before.csv";
                        writing_file_name[4] = save_forder_path.Text + save_name.Text + "_mi_goiso_before.csv";
                        writing_file_name[5] = save_forder_path.Text + save_name.Text + "_mi_syoji_after.csv";
                        writing_file_name[6] = save_forder_path.Text + save_name.Text + "_mi_goiso_after.csv";
                        writing_file_name[7] = save_forder_path.Text + save_name.Text + "_t_syoji_before.csv";
                        writing_file_name[8] = save_forder_path.Text + save_name.Text + "_t_goiso_before.csv";
                        writing_file_name[9] = save_forder_path.Text + save_name.Text + "_t_syoji_after.csv";
                        writing_file_name[10] = save_forder_path.Text + save_name.Text + "_t_goiso_after.csv";
                        writing_file_name[11] = save_forder_path.Text + save_name.Text + "_freq_syoji_before.csv";
                        writing_file_name[12] = save_forder_path.Text + save_name.Text + "_freq_goiso_before.csv";
                        writing_file_name[13] = save_forder_path.Text + save_name.Text + "_freq_syoji_after.csv";
                        writing_file_name[14] = save_forder_path.Text + save_name.Text + "_freq_goiso_after.csv";
                    }

                    //既存のCSVファイルの上書き確認
                    string exists_filepath = null;
                    for (int item = 0; item <= 14; item++)
                    {
                        if(
                            (File.Exists(writing_file_name[item]))&&
                            (target_listBox.GetItemChecked(item))
                            )
                        {
                            exists_filepath = exists_filepath + writing_file_name[item].Replace(save_forder_path.Text, "") + Environment.NewLine;
                        }
                    }
                    if (exists_filepath != null)
                    {
                        DialogResult warning_result = MessageBox.Show("既存のファイル（" + Environment.NewLine + exists_filepath + "）が保存先として指定されています。重複するファイルは上書きされますが構いませんか？", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        if (warning_result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                else if (output_sql_radioButton.Checked)
                {
                    //既存のデータベースの上書き確認
                    if (File.Exists(db_name))
                    {
                        DialogResult warning_result = MessageBox.Show("既存のデータベースが保存先として指定されています。重複するテーブルは上書きされますが構いませんか？", "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        if (warning_result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    writing_table_name[0] = "ngram_moji";
                    writing_table_name[1] = "ngram_syoji";
                    writing_table_name[2] = "ngram_goiso";
                    writing_table_name[3] = "mi_syoji_before";
                    writing_table_name[4] = "mi_goiso_before";
                    writing_table_name[5] = "mi_syoji_after";
                    writing_table_name[6] = "mi_goiso_after";
                    writing_table_name[7] = "t_syoji_before";
                    writing_table_name[8] = "t_goiso_before";
                    writing_table_name[9] = "t_syoji_after";
                    writing_table_name[10] = "t_goiso_after";
                    writing_table_name[11] = "freq_syoji_before";
                    writing_table_name[12] = "freq_goiso_before";
                    writing_table_name[13] = "freq_syoji_after";
                    writing_table_name[14] = "freq_goiso_after";
                }


                //N-gram 用ヘッダ
                for (int item = 0; item <= 2; item++)
                {
                    if (target_listBox.GetItemChecked(item))
                    {
                        if (output_csv_radioButton.Checked)
                        {
                            System.IO.File.WriteAllText(writing_file_name[item], "N,グラム,品詞," + header_textBox.Text + ",合計値,該当数" + Environment.NewLine);
                        }
                        else if (output_sql_radioButton.Checked)
                        {
                            //テーブル作成
                            string column = "N INTEGER,Gram TEXT,Pos TEXT,";
                            foreach(string header_item in header_names)
                            {
                                column = column + "[" + header_item + "]" + " INTEGER,";
                            }
                            column = column + "Sum INTEGER,Files INTEGER";
                            data_base.create_table(db_name,writing_table_name[item],column);
                        }
                    }
                }

                //MI・Tスコア用ヘッダ
                for (int item = 3; item <= 10; item++)
                {
                    if (target_listBox.GetItemChecked(item))
                    {
                        if (output_csv_radioButton.Checked)
                        {
                            System.IO.File.WriteAllText(writing_file_name[item], "スパン,中心語,共起語," + header_textBox.Text + ",平均値,該当数" + Environment.NewLine);
                        }
                        else if (output_sql_radioButton.Checked)
                        {
                            //テーブル作成
                            string column = "Span INTEGER,Node TEXT,Cooccurrence TEXT,";
                            foreach(string header_item in header_names)
                            {
                                column = column + "[" + header_item + "]" + " INTEGER,";
                            }
                            column = column + "Average INTEGER,Files INTEGER";
                            data_base.create_table(db_name, writing_table_name[item], column);
                        }
                    }
                }

                //共起頻度用ヘッダ
                for (int item = 11; item <= 14; item++)
                {
                    if (target_listBox.GetItemChecked(item))
                    {
                        if (output_csv_radioButton.Checked)
                        {
                            System.IO.File.WriteAllText(writing_file_name[item], "スパン,中心語,共起語," + header_textBox.Text + ",合計値,平均値,該当数" + Environment.NewLine);
                        }
                        else if (output_sql_radioButton.Checked)
                        {
                            //テーブル作成
                            string column = "Span INTEGER,Node TEXT,Cooccurrence TEXT,";
                            foreach(string header_item in header_names)
                            {
                                column = column + "[" + header_item + "]" + " INTEGER,";
                            }
                            column = column + "Sum INTEGER,Average INTEGER,Files INTEGER";
                            data_base.create_table(db_name, writing_table_name[item], column);
                        }
                    }
                }

                SortedList<string, int>[] array = new SortedList<string,int>[25];
                List<int> number_of_words_array = new List<int>();
                //ファイルの読み込み
                foreach(string src_file in input_files_listbox.Items){
                    string file_name = header_names[header_num];
                    IEnumerable<string> src_sentences = File.ReadLines(src_file);
                    int number_of_words = new int();

                    //一行ずつ処理
                    foreach(string src_sentence in src_sentences){
                        if (src_sentence != "")
                        {
                            //MeCab_kaiseki class のインスタンス作成
                            MeCab_kaiseki target_sentence = new MeCab_kaiseki(src_sentence,file_name,header_names);

                            //形態素解析
                            number_of_words = number_of_words + target_sentence.kaiseki();

                            //N-gram の処理
                            //N-gram（文字ー書字形）
                            if (target_listBox.GetItemChecked(0) == true)
                            {
                                SortedList<string, int> moji_ngram = target_sentence.moji_ngram(n_min, n_max);
                                SortedList<string, int> base_cp = array[0];
                                array[0] = target_sentence.merge(base_cp, moji_ngram);
                            }

                            //N-gram（単語ー書字形）
                            if (target_listBox.GetItemChecked(1) == true)
                            {
                                SortedList<string, int> syoji_ngram = target_sentence.syoji_ngram(n_min, n_max);
                                SortedList<string, int> base_cp = target_sentence.clone(array[1]);
                                array[1] = target_sentence.merge(base_cp, syoji_ngram);
                            }

                            //N-gram（単語ー語彙素形）
                            if (target_listBox.GetItemChecked(2) == true)
                            {
                                SortedList<string,int> goiso_ngram = target_sentence.goiso_ngram(n_min, n_max);
                                SortedList<string, int> base_cp = target_sentence.clone(array[2]);
                                array[2] = target_sentence.merge(base_cp, goiso_ngram);
                            }

                            //スパンの処理
                            //書字形
                            if (
                            (target_listBox.GetItemChecked(3) == true) ||
                            (target_listBox.GetItemChecked(5) == true) ||
                            (target_listBox.GetItemChecked(7) == true) ||
                            (target_listBox.GetItemChecked(9) == true) ||
                            (target_listBox.GetItemChecked(11) == true) ||
                            (target_listBox.GetItemChecked(13) == true)
                            )
                            {
                                SortedList<string, int> syoji_span = target_sentence.clone(target_sentence.syoji_span(span_min, span_max));
                                SortedList<string, int> base_cp = target_sentence.clone(array[15]);
                                array[15] = target_sentence.merge(base_cp, syoji_span);

                                SortedList<string, int> syoji_ngram = target_sentence.clone(target_sentence.syoji_ngram(1, 1));
                                SortedList<string, int> base_cp2 = target_sentence.clone(array[16]);
                                array[16] = target_sentence.merge(base_cp2, syoji_ngram);
                            }

                            //語彙素形
                            if (
                            (target_listBox.GetItemChecked(4) == true) ||
                            (target_listBox.GetItemChecked(6) == true) ||
                            (target_listBox.GetItemChecked(8) == true) ||
                            (target_listBox.GetItemChecked(10) == true) ||
                            (target_listBox.GetItemChecked(12) == true) ||
                            (target_listBox.GetItemChecked(14) == true)
                            )
                            {
                                SortedList<string, int> goiso_span = target_sentence.clone(target_sentence.goiso_span(span_min, span_max));
                                SortedList<string, int> base_cp = target_sentence.clone(array[18]);
                                array[18] = target_sentence.merge(base_cp, goiso_span);

                                SortedList<string, int> goiso_ngram = target_sentence.clone(target_sentence.goiso_ngram(1, 1));
                                SortedList<string, int> base_cp2 = target_sentence.clone(array[19]);
                                array[19] = target_sentence.merge(base_cp2, goiso_ngram);
                            }
                        }
                    }
                    header_num++;
                    number_of_words_array.Add(number_of_words);
                }

                //N-gram をファイル出力
                MeCab_kaiseki output = new MeCab_kaiseki(header_names);
                for (int num = 0; num <= 2; num++)
                {
                    if (array[num] != null)
                    {
                        if (output_csv_radioButton.Checked)
                        {
                            output.forming(array[num], writing_file_name[num]);
                        }
                        else if (output_sql_radioButton.Checked)
                        {
                            List<List<string>> insert_records = output.forming(array[num]);
                            foreach (List<string> record in insert_records)
                            {
                                string insert_record = null;
                                foreach (string record_item in record)
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(record_item, @"^\d+$"))
                                    {
                                        insert_record = insert_record + record_item + ",";
                                    }
                                    else
                                    {
                                        insert_record = insert_record +  "'" + record_item + "'" + ",";
                                    }
                                }
                                insert_record = System.Text.RegularExpressions.Regex.Replace(insert_record, ",$", "");
                                //insert処理
                                data_base.insert(db_name, writing_table_name[num], insert_record);
                            }
                        }
                    }
                }

                //スパンの処理
                //書字形
                if (
                (target_listBox.GetItemChecked(3) == true) ||
                (target_listBox.GetItemChecked(5) == true) ||
                (target_listBox.GetItemChecked(7) == true) ||
                (target_listBox.GetItemChecked(9) == true) ||
                (target_listBox.GetItemChecked(11) == true) ||
                (target_listBox.GetItemChecked(13) == true)
                )
                {
                    array[17] = output.merge(array[15], array[16]);
                    List<List<string>> syoji_score_array = output.forming(array[17]);

                    //前方共起
                    if (
                        (target_listBox.GetItemChecked(3) == true) ||
                        (target_listBox.GetItemChecked(7) == true) ||
                        (target_listBox.GetItemChecked(11) == true)
                    )
                    {
                        var packed = output.prepare(syoji_score_array, true);
                        SortedList<string, int> syoji_before_array = packed.Before;
                        SortedList<string, int> syoji_count_array = packed.Count_array;

                        //MI（単語ー書字形ー前方共起）
                        if (target_listBox.GetItemChecked(3))
                        {
                            List<string> syoji_mi_bef_array = output.syoji_mi_bef(syoji_before_array, syoji_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in syoji_mi_bef_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[3], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[3], insert_record);
                                }
                            }
                        }

                        //T（単語ー書字形ー前方共起）

                        if (target_listBox.GetItemChecked(7))
                        {
                            List<string> syoji_t_bef_array = output.syoji_t_bef(syoji_before_array, syoji_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in syoji_t_bef_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[7], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[7], insert_record);
                                }
                            }
                        }

                        //共起頻度（単語ー書字形ー前方共起）
                        if (target_listBox.GetItemChecked(11))
                        {
                            List<string> syoji_freq_bef_array = output.syoji_freq_bef(syoji_before_array, syoji_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in syoji_freq_bef_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[11], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[11], insert_record);
                                }
                            }
                        }
                    }

                    //後方共起
                    if (
                        (target_listBox.GetItemChecked(5) == true) ||
                        (target_listBox.GetItemChecked(9) == true) ||
                        (target_listBox.GetItemChecked(13) == true)
                    )
                    {
                        var packed = output.prepare(syoji_score_array, false);
                        SortedList<string, int> syoji_after_array = packed.After;
                        SortedList<string, int> syoji_count_array = packed.Count_array;

                        //MI（単語ー書字形ー後方共起）
                        if (target_listBox.GetItemChecked(5))
                        {
                            List<string> test_output_array = output.syoji_mi_aft(syoji_after_array, syoji_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in test_output_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[5], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[5], insert_record);
                                }
                            }
                        }

                        //T（単語ー書字形ー後方共起）
                        if (target_listBox.GetItemChecked(9))
                        {
                            List<string> syoji_t_aft_array = output.syoji_t_aft(syoji_after_array, syoji_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in syoji_t_aft_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[9], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[9], insert_record);
                                }
                            }
                        }

                        //共起頻度（単語ー書字形ー後方共起）
                        if (target_listBox.GetItemChecked(13))
                        {
                            List<string> syoji_freq_aft_array = output.syoji_freq_aft(syoji_after_array, syoji_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in syoji_freq_aft_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[13], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[13], insert_record);
                                }
                            }
                        }
                    }
                }

                //語彙素形
                if (
                (target_listBox.GetItemChecked(4) == true) ||
                (target_listBox.GetItemChecked(6) == true) ||
                (target_listBox.GetItemChecked(8) == true) ||
                (target_listBox.GetItemChecked(10) == true) ||
                (target_listBox.GetItemChecked(12) == true) ||
                (target_listBox.GetItemChecked(14) == true)
                )
                {
                    array[20] = output.merge(array[18], array[19]);
                    List<List<string>> goiso_score_array = output.forming(array[20]);

                    //前方共起
                    if (
                        (target_listBox.GetItemChecked(4) == true) ||
                        (target_listBox.GetItemChecked(8) == true) ||
                        (target_listBox.GetItemChecked(12) == true)
                    )
                    {
                        var packed = output.prepare(goiso_score_array, true);
                        SortedList<string, int> goiso_before_array = packed.Before;
                        SortedList<string, int> goiso_count_array = packed.Count_array;

                        //MI（単語ー語彙素形ー前方共起）
                        if (target_listBox.GetItemChecked(4))
                        {
                            List<string> goiso_mi_bef_array = output.goiso_mi_bef(goiso_before_array, goiso_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in goiso_mi_bef_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[4], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[4], insert_record);
                                }
                            }
                        }

                        //T（単語ー語彙素形ー前方共起）
                        if (target_listBox.GetItemChecked(8))
                        {
                            List<string> goiso_t_bef_array = output.goiso_t_bef(goiso_before_array, goiso_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in goiso_t_bef_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[8], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[8], insert_record);
                                }
                            }
                        }

                        //共起頻度（単語ー語彙素形ー前方共起）
                        if (target_listBox.GetItemChecked(12))
                        {
                            List<string> goiso_freq_bef_array = output.goiso_freq_bef(goiso_before_array, goiso_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in goiso_freq_bef_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[12], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[12], insert_record);
                                }
                            }
                        }
                    }

                    //後方共起
                    if (
                        (target_listBox.GetItemChecked(6) == true) ||
                        (target_listBox.GetItemChecked(10) == true) ||
                        (target_listBox.GetItemChecked(14) == true)
                    )
                    {
                        var packed = output.prepare(goiso_score_array, false);
                        SortedList<string, int> goiso_after_array = packed.After;
                        SortedList<string, int> goiso_count_array = packed.Count_array;

                        //MI（単語ー語彙素形ー後方共起）
                        if (target_listBox.GetItemChecked(6))
                        {
                            List<string> goiso_mi_aft_array = output.goiso_mi_aft(goiso_after_array, goiso_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in goiso_mi_aft_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[6], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[6], insert_record);
                                }
                            }
                        }

                        //T（単語ー語彙素形ー後方共起）
                        if (target_listBox.GetItemChecked(10))
                        {
                            List<string> goiso_t_aft_array = output.goiso_t_aft(goiso_after_array, goiso_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in goiso_t_aft_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[10], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[10], insert_record);
                                }
                            }
                        }

                        //共起頻度（単語ー語彙素形ー後方共起）
                        if (target_listBox.GetItemChecked(14))
                        {
                            List<string> goiso_freq_aft_array = output.goiso_freq_aft(goiso_after_array, goiso_count_array, number_of_words_array, header_names.Length);
                            foreach (string item in goiso_freq_aft_array)
                            {
                                if (output_csv_radioButton.Checked)
                                {
                                    System.IO.File.AppendAllText(writing_file_name[14], item + Environment.NewLine);
                                }
                                else if (output_sql_radioButton.Checked)
                                {
                                    string insert_record = data_base.sqlize(item);
                                    data_base.insert(db_name, writing_table_name[14], insert_record);
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("終了");
            }
        }

        //CSVが選択されたとき
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (output_csv_radioButton.Checked)
            {
                save_name_label.Text = "CSV共通タイトル（オプション）";
            }
        }

        //SQLが選択されたとき
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (output_sql_radioButton.Checked)
            {
                save_name_label.Text = "データベース名";
            }
        }
    }
}