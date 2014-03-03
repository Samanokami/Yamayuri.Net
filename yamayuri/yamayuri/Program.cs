using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NMeCab;
using System.Data.SQLite;

namespace yamayuri
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class MeCab_kaiseki
    {
        //変数の宣言
        int num_of_words;

        int w_tail;
        int goiso_w_tail;
        SortedList<int, int> w_array1;
        SortedList<int, int> goiso_w_array1;
        SortedList<int, string> w_array2;
        SortedList<int, string> goiso_w_array2;

        string syojikei;
        //string[] result;
        List<string> result;
        string goiso;

        //moji_ngram メソッド用
        int position;
        int gram_tail;
        //int n;
        string arr_item;
        string file_name;
        SortedList<string, int> s_array = new SortedList<string,int>();
        SortedList<string,int> moji_s_array = new SortedList<string,int>();

        System.Globalization.StringInfo surrogate_sentence;

        string goiso_sentence;
        System.Globalization.StringInfo goiso_surrogate_sentence;

        //syoji_ngram メソッド用
        int num;
        int word_num;
        SortedList<int, string> part_array = new SortedList<int,string>();
        SortedList<int, string> morpheme_array = new SortedList<int,string>();
        int s_pos;
        int pos;
        SortedList<string, int> syoji_s_array;

        //goiso_ngram メソッド用
        SortedList<string, int> goiso_s_array;

        //syoji_span メソッド用
        SortedList<string,int> syoji_span_array;

        //goiso_span メソッド用
        SortedList<string, int> goiso_span_array;

        //joint メソッド用
        int pre_item;
        int item;
        string part;
        string keyword;
        int key_length = 0;

        MeCabTagger tag;
        MeCabNode node;

        string[] header_names;

        //コンストラクター
        public MeCab_kaiseki(string[] header_names)
        {
            this.header_names = header_names;
        }
        public MeCab_kaiseki(string sentence,string file_name,string[] header_names)
        {
            this.surrogate_sentence = new System.Globalization.StringInfo(sentence);
            w_tail = 0;
            goiso_w_tail = 0;
            w_array1 = new SortedList<int, int>();
            goiso_w_array1 = new SortedList<int, int>();
            w_array2 = new SortedList<int, string>();
            goiso_w_array2 = new SortedList<int, string>();

            tag = MeCabTagger.Create();
            node = tag.ParseToNode(sentence);
            this.file_name = file_name;
            this.header_names = header_names;
        }

        //解析メソッド
        /// <summary>
        /// 形態素解析を行い、一行毎の語数を返す。
        /// </summary>
        /// <returns></returns>
        public int kaiseki()
        {
            num_of_words = new int();
            while (node != null)
            {
                if(
                    (node.Feature.Contains("BOS/EOS") == false)
                )
                {
                    if (node.Feature.Split(',').Length == 17)
                    {
                        syojikei = node.Surface;
                        result = new List<string>(node.Feature.Split(','));
                        goiso = result[7];

                        w_tail = w_tail + node.Length;
                        goiso_w_tail = goiso_w_tail + goiso.Length;

                        w_array1[w_tail - 1] = syojikei.Length;
                        goiso_w_array1[goiso_w_tail - 1] = goiso.Length;
                        w_array2[w_tail - 1] = goiso_w_array2[goiso_w_tail - 1] = result[0];

                        goiso_sentence = goiso_sentence + goiso;
                        num_of_words++;
                    }
                    else if (node.Feature.Split(',').Length == 6)
                    {
                        syojikei = goiso = node.Surface;
                        result = new List<string>(node.Feature.Split(','));

                        w_tail = w_tail + node.Length;
                        goiso_w_tail = goiso_w_tail + goiso.Length;

                        w_array1[w_tail - 1] = syojikei.Length;
                        goiso_w_array1[goiso_w_tail - 1] = goiso.Length;
                        w_array2[w_tail - 1] = goiso_w_array2[goiso_w_tail - 1] = result[0];

                        goiso_sentence = goiso_sentence + goiso;
                        num_of_words++;
                    }
                }
                node = node.Next;
            }
            goiso_surrogate_sentence = new System.Globalization.StringInfo(goiso_sentence);
            return num_of_words;
        }

        //N-gram（文字ー書字形）
        public SortedList<string,int> moji_ngram(decimal min,decimal max)
        {
            for (int n = (int) min; n <= (int) max; n++)
            {
                for (position = 0; position <= surrogate_sentence.LengthInTextElements - n; position++)
                {
                    gram_tail = position + n - 1;
                    if (w_array2.ContainsKey(gram_tail))
                    {
                        if (n <= w_array1[gram_tail])
                        {
                            arr_item = n + "," + surrogate_sentence.SubstringByTextElements(position, n) + "," + w_array2[gram_tail];

                            if (moji_s_array.ContainsKey(arr_item + "," + file_name))
                            {
                                moji_s_array[arr_item + "," + file_name]++;
                            }
                            else
                            {
                                moji_s_array[arr_item + "," + file_name] = 1;
                            }
                        }
                        else
                        {
                            joint(n);
                            part = System.Text.RegularExpressions.Regex.Replace(part, "/$", "");
                            keyword = System.Text.RegularExpressions.Regex.Replace(keyword, "/$", "");
                            arr_item = n + "," + keyword + "," + part;

                            if (moji_s_array.ContainsKey(arr_item + "," + file_name))
                            {
                                moji_s_array[arr_item + "," + file_name]++;
                            }
                            else
                            {
                                moji_s_array[arr_item + "," + file_name] = 1;
                            }

                            initialize();
                        }
                    }
                    else
                    {
                        joint(n);
                        while(w_array2.ContainsKey(gram_tail) == false)
                        {
                            if (gram_tail > surrogate_sentence.LengthInTextElements - 1)
                            {
                                break;
                            }
                            gram_tail++;
                        }

                        if (w_array2.ContainsKey(gram_tail) == true)
                        {
                            part = part + w_array2[gram_tail];
                            keyword = keyword + surrogate_sentence.SubstringByTextElements(pre_item, n - key_length);
                            part = System.Text.RegularExpressions.Regex.Replace(part, "/$", "");
                            keyword = System.Text.RegularExpressions.Regex.Replace(keyword, "/$", "");
                            arr_item = n + "," + keyword + "," + part;

                            if (moji_s_array.ContainsKey(arr_item + "," + file_name))
                            {
                                moji_s_array[arr_item + "," + file_name]++;
                            }
                            else
                            {
                                moji_s_array[arr_item + "," + file_name] = 1;
                            }

                            initialize();
                        }
                    }
                }
            }
            return moji_s_array;
        }

        //結合メソッド
        private void joint(int n)
        {
            pre_item = gram_tail - n + 1;
            for (item = pre_item; item <= gram_tail; item++)
            {
                if(w_array2.ContainsKey(item))
                {
                    if (pre_item <= surrogate_sentence.LengthInTextElements - 1)
                    {
                        part = part + w_array2[item] + "/";
                        keyword = keyword + surrogate_sentence.SubstringByTextElements(pre_item, item - pre_item + 1) + "/";
                    }
                        key_length = key_length + item - pre_item + 1;
                        pre_item = item + 1;
                }
            }
        }

        //初期化メソッド
        private void initialize()
        {
            part = null;
            keyword = null;
            key_length = 0;
            pre_item = 0;
        }

        //N-gram（単語ー書字形）
        public SortedList<string, int> syoji_ngram(decimal min, decimal max)
        {
            syoji_s_array = s_array_set(min,max,1,w_array1,w_array2,surrogate_sentence);
            return syoji_s_array;
        }

        //N-gram（単語ー語彙素形）
        public SortedList<string, int> goiso_ngram(decimal min, decimal max)
        {
            goiso_s_array = s_array_set(min,max,1,goiso_w_array1,goiso_w_array2,goiso_surrogate_sentence);
            return goiso_s_array;
        }

        //スパン（単語ー書字形）
        public SortedList<string, int> syoji_span(decimal min, decimal max)
        {
            syoji_span_array = s_array_set(min, max, 2, w_array1, w_array2, surrogate_sentence);
            return syoji_span_array;
        }

        //スパン（単語ー語彙素形）
        public SortedList<string, int> goiso_span(decimal min, decimal max)
        {
            goiso_span_array = s_array_set(min, max, 2, goiso_w_array1,goiso_w_array2,goiso_surrogate_sentence);
            return goiso_span_array;
        }
        
        //配列格納メソッド
        private SortedList<string,int> s_array_set(decimal min,decimal max,SortedList<int,int> w_array1,SortedList<int,string> w_array2,System.Globalization.StringInfo sentence)
        {
            s_array.Clear();
            for (int n = (int)min; n <= (int)max; n = n ++)
            {
                num = 0;
                for (word_num = 0; word_num <= sentence.LengthInTextElements; word_num++)
                {
                    if (w_array2.ContainsKey(word_num))
                    {
                        part_array[num] = w_array2[word_num];
                        morpheme_array[num] = sentence.SubstringByTextElements(word_num - w_array1[word_num] + 1, w_array1[word_num]);
                        num++;
                    }
                }
                num = num - 1;
                for (s_pos = 0; s_pos <= num - n + 1; s_pos++)
                {
                    for (pos = s_pos; pos <= s_pos + n - 1; pos++)
                    {
                        if (keyword != null)
                        {
                            keyword = keyword + morpheme_array[pos] + "/";
                        }
                        else
                        {
                            keyword = morpheme_array[pos] + "/";
                        }

                        if (part != null)
                        {
                            part = part + part_array[pos] + "/";
                        }
                        else
                        {
                            part = part_array[pos] + "/";
                        }
                    }
                    part = System.Text.RegularExpressions.Regex.Replace(part, "/$", "");
                    keyword = System.Text.RegularExpressions.Regex.Replace(keyword, "/$", "");
                    arr_item = n + "," + keyword + "," + part;
                    if (s_array.ContainsKey(arr_item + "," + file_name))
                    {
                        s_array[arr_item + "," + file_name]++;
                    }
                    else
                    {
                        s_array[arr_item + "," + file_name] = 1;
                    }
                    initialize();
                }
                part_array.Clear();
                morpheme_array.Clear();
            }
            return s_array;
        }
        private SortedList<string, int> s_array_set(decimal min, decimal max, int add_num, SortedList<int, int> w_array1, SortedList<int, string> w_array2, System.Globalization.StringInfo sentence)
        {
            s_array.Clear();
            for (int n = (int)min; n <= (int)max; n = n + add_num)
            {
                //numは語数
                num = 0;
                for (word_num = 0; word_num <= sentence.LengthInTextElements - 1; word_num++)
                {
                    if (w_array2.ContainsKey(word_num))
                    {
                        part_array[num] = w_array2[word_num];
                        morpheme_array[num] = sentence.SubstringByTextElements(word_num - w_array1[word_num] + 1, w_array1[word_num]);
                        num++;
                   }
                }
                num = num - 1;
                for (s_pos = 0; s_pos <= num - n + 1; s_pos++)
                {
                    for (pos = s_pos; pos <= s_pos + n - 1; pos++)
                    {
                        if (keyword != null)
                        {
                            keyword = keyword + morpheme_array[pos] + "/";
                        }
                        else
                        {
                            keyword = morpheme_array[pos] + "/";
                        }

                        if (part != null)
                        {
                            part = part + part_array[pos] + "/";
                        }
                        else
                        {
                            part = part_array[pos] + "/";
                        }
                    }
                    part = System.Text.RegularExpressions.Regex.Replace(part, "/$", "");
                    keyword = System.Text.RegularExpressions.Regex.Replace(keyword, "/$", "");
                    arr_item = n + "," + keyword + "," + part;

                    if (s_array.ContainsKey(arr_item + "," + file_name))
                    {
                        s_array[arr_item + "," + file_name]++;
                    }
                    else
                    {
                        s_array[arr_item + "," + file_name] = 1;
                    }
                    initialize();
                }
                part_array.Clear();
                morpheme_array.Clear();
            }
            return s_array;
        }

        //マージ用メソッド
        public SortedList<string, int> merge(SortedList<string,int> base_item,SortedList<string,int> comparer_item)
        {
            SortedList<string, int> merged_array = new SortedList<string, int>();
            merged_array = base_item;
            foreach (KeyValuePair<string, int> item in comparer_item)
            {
                if (base_item == null)
                {
                    merged_array = comparer_item;
                    return merged_array;
                }
                else
                {
                    if (base_item.ContainsKey(item.Key))
                    {
                        merged_array[item.Key] = base_item[item.Key] + item.Value;
                    }
                    else
                    {
                        merged_array.Add(item.Key, item.Value);
                    }
                }
            }
            return merged_array;
        }

        ////整形メソッド（文字列）
        //public string forming(SortedList<string, int> array)
        //{
        //    string last_output = null;
        //    object[][] last_array = new object[array.Count][];
        //    int num_gram = 0;
        //    foreach (KeyValuePair<string, int> item in array)
        //    {
        //        last_array[num_gram] = new object[5];

        //        //Nの値
        //        last_array[num_gram][0] = item.Key.Split(',')[0];

        //        //グラム
        //        last_array[num_gram][1] = item.Key.Split(',')[1];

        //        //品詞情報
        //        last_array[num_gram][2] = item.Key.Split(',')[2];

        //        //ファイル名
        //        last_array[num_gram][3] = item.Key.Split(',')[3];

        //        //出現頻度
        //        last_array[num_gram][4] = item.Value;

        //        num_gram++;
        //    }

        //    string pre_val1 = null;
        //    int f_name = 0;
        //    string output = null;
        //    for (int count = 0; count <= num_gram - 1; count++)
        //    {
        //        string val1 = last_array[count][1].ToString() + "," + last_array[count][2];
        //        string val2 = (string)last_array[count][3];
        //        int val3 = (int)last_array[count][4];
        //        string val4 = (string)last_array[count][0];

        //        if (val1 == pre_val1)
        //        {
        //            var packed1 = chain(val1, val2, val3, header_names, f_name, output);
        //            pre_val1 = packed1.Pre_val1;
        //            f_name = packed1.F_name;
        //            output = packed1.Output;
        //        }
        //        else
        //        {
        //            var packed2 = file_output(output, header_names);
        //            output = packed2.Output;
        //            if (packed2.Last_output != null)
        //            {
        //                last_output = last_output + packed2.Last_output;
        //                //last_output = packed2.Last_output;
        //                //System.IO.File.AppendAllText(filepath, last_output);
        //            }

        //            f_name = 0;
        //            output = val4 + "," + val1;
        //            var packed1 = chain(val1, val2, val3, header_names, f_name, output);
        //            pre_val1 = packed1.Pre_val1;
        //            f_name = packed1.F_name;
        //            output = packed1.Output;
        //        }
        //    }
        //    var packed3 = file_output(output, header_names);
        //    output = packed3.Output;
        //    if (packed3.Last_output != null)
        //    {
        //        last_output = last_output + packed3.Last_output;
        //        //last_output = packed3.Last_output;
        //        //System.IO.File.AppendAllText(filepath, last_output);
        //    }
        //    return last_output;
        //}



        //整形メソッド（配列）
        public List<List<string>> forming(SortedList<string, int> array)
        {
            string last_output = null;
            List<List<string>> last_output_array;
            object[][] last_array = new object[array.Count][];
            int num_gram = 0;
            foreach (KeyValuePair<string, int> item in array)
            {
                last_array[num_gram] = new object[5];

                //Nの値
                last_array[num_gram][0] = item.Key.Split(',')[0];

                //グラム
                last_array[num_gram][1] = item.Key.Split(',')[1];

                //品詞情報
                last_array[num_gram][2] = item.Key.Split(',')[2];

                //ファイル名
                last_array[num_gram][3] = item.Key.Split(',')[3];

                //出現頻度
                last_array[num_gram][4] = item.Value;

                num_gram++;
            }

            string pre_val1 = null;
            int f_name = 0;
            string output = null;
            for (int count = 0; count <= num_gram - 1; count++)
            {
                string val1 = last_array[count][1].ToString() + "," + last_array[count][2];
                string val2 = (string)last_array[count][3];
                int val3 = (int)last_array[count][4];
                string val4 = (string)last_array[count][0];

                if (val1 == pre_val1)
                {
                    var packed1 = chain(val1, val2, val3, header_names, f_name, output);
                    pre_val1 = packed1.Pre_val1;
                    f_name = packed1.F_name;
                    output = packed1.Output;
                }
                else
                {
                    var packed2 = file_output(output, header_names);
                    output = packed2.Output;
                    if (packed2.Last_output != null)
                    {
                        last_output = last_output + packed2.Last_output;
                        //last_output = packed2.Last_output;
                        //System.IO.File.AppendAllText(filepath, last_output);
                    }

                    f_name = 0;
                    output = val4 + "," + val1;
                    var packed1 = chain(val1, val2, val3, header_names, f_name, output);
                    pre_val1 = packed1.Pre_val1;
                    f_name = packed1.F_name;
                    output = packed1.Output;
                }
            }
            var packed3 = file_output(output, header_names);
            output = packed3.Output;
            if (packed3.Last_output != null)
            {
                last_output = last_output + packed3.Last_output;
            }

            string[] temp1 = last_output.Split(new string[]{ Environment.NewLine},StringSplitOptions.RemoveEmptyEntries);
            last_output_array = new List<List<string>>();

            foreach (string item in temp1)
            {
                List<string> temp2 = new List<string>();
                foreach (string temp_item in item.Split(','))
                {
                    temp2.Add(temp_item);
                }
                last_output_array.Add(temp2);
            }
            return last_output_array;
        }

        //整形メソッド（出力）
        public void forming(SortedList<string, int> array,string filepath)
        {
            string last_output;
            object[][] last_array = new object[array.Count][];
            int num_gram = 0;
            foreach (KeyValuePair<string, int> item in array)
            {
                last_array[num_gram] = new object[5];

                //Nの値
                last_array[num_gram][0] = item.Key.Split(',')[0];

                //グラム
                last_array[num_gram][1] = item.Key.Split(',')[1];

                //品詞情報
                last_array[num_gram][2] = item.Key.Split(',')[2];

                //ファイル名
                last_array[num_gram][3] = item.Key.Split(',')[3];

                //出現頻度
                last_array[num_gram][4] = item.Value;

                num_gram++;
            }

            string pre_val1 = null;
            int f_name = 0;
            string output = null;
            for (int count = 0; count <= num_gram - 1; count++)
            {
                string val1 = last_array[count][1].ToString() + "," + last_array[count][2];
                string val2 = (string)last_array[count][3];
                int val3 = (int)last_array[count][4];
                string val4 = (string)last_array[count][0];

                if(val1 == pre_val1){
                    var packed1 = chain(val1,val2,val3,header_names,f_name,output);
                    pre_val1 = packed1.Pre_val1;
                    f_name = packed1.F_name;
                    output = packed1.Output;
                }
                else
                {
                    var packed2 = file_output(output,header_names);
                    output = packed2.Output;
                    if (packed2.Last_output != null)
                    {
                        last_output = packed2.Last_output;
                        System.IO.File.AppendAllText(filepath, last_output);
                    }

                    f_name = 0;
                    output = val4 + "," + val1;
                    var packed1 = chain(val1, val2, val3, header_names, f_name, output);
                    pre_val1 = packed1.Pre_val1;
                    f_name = packed1.F_name;
                    output = packed1.Output;
                }
            }
            var packed3 = file_output(output,header_names);
            output = packed3.Output;
            if (packed3.Last_output != null)
            {
                last_output = packed3.Last_output;
                System.IO.File.AppendAllText(filepath, last_output);
            }
        }

        //forming メソッドの内部で利用するメソッド（１）
        private dynamic chain(string val1, string val2,int val3,string[] header_names,int f_name,string output)
        {
            while (val2 != header_names[f_name])
            {
                output = output + "," + 0;
                f_name++;
            }
            output = output + "," + val3;
            string pre_val1 = val1;
            f_name++;
            return new
            {
                Pre_val1 = pre_val1,
                F_name = f_name,
                Output = output
            };
        }

        //forming メソッドの内部で利用するメソッド（２）
        private dynamic file_output(string output,string[] header_names)
        {
            if (output != null)
            {
                List<object> output_array = new List<object>(output.Split(','));
                for (int output_num = output_array.Count; output_num <= header_names.Length + 2; output_num++)
                {
                    //output_array[output_num] = 0;
                    output_array.Add(0);
                }

                string last_output = null;
                for (int output_num = 0; output_num <= header_names.Length + 2; output_num++)
                {
                    last_output = last_output + output_array[output_num] + ",";
                }

                int sum = 0;
                int inclusion_file = 0;
                for (int output_num = 3; output_num <= header_names.Length + 2; output_num++)
                {
                    sum = sum + int.Parse(output_array[output_num].ToString());
                    if(int.Parse(output_array[output_num].ToString()) != 0){
                        inclusion_file ++;
                    }
                }
                last_output = last_output + sum + "," + inclusion_file + Environment.NewLine;

                return new{
                    Last_output = last_output,
                    Output = ""
                };
            }
            else
            {
                return new
                {
                    Last_output = "",
                    Output = output
                };
            }
        }

        //共起配列の前準備メソッド
        public dynamic prepare(List<List<string>> array, bool mode)
        {
            string word = "";
            SortedList<string, int> count = new SortedList<string, int>();
            int span;

            SortedList<string, int> before = new SortedList<string, int>();
            SortedList<string, int> after = new SortedList<string, int>();

            foreach (List<string> item in array)
            {
                SortedList<int, int> temp_array = new SortedList<int, int>();
                temp_array[0] = int.Parse(item[0]);
                for (int temp = 3; temp <= item.Count - 1; temp++)
                {
                    temp_array[temp] = int.Parse(item[temp]);
                }

                string[] node;
                string[] part;
                if (item[0] == "1")
                {
                    word = item[1] + "-" + item[2];
                    for (int num = 3; num <= item.Count - 3; num++)
                    {
                        count[word + "," + (num - 3)] = temp_array[num];
                    }
                }
                else
                {
                    span = (temp_array[0] - 1) / 2;
                    node = item[1].Split('/');
                    part = item[2].Split('/');
                    for (int num_word = 0; num_word <= temp_array[0] - 1; num_word++)
                    {
                        node[num_word] = node[num_word] + "-" + part[num_word];
                    }

                    if (mode)
                    {
                        before = this.before(before, item, span, temp_array, node);
                    }
                    else
                    {
                        after = this.after(after, item, span, temp_array, node);
                    }
                }
            }

            if (mode)
            {
                return new
                {
                    Count_array = count,
                    Before = before
                };
            }
            else
            {
                return new
                {
                    Count_array = count,
                    After = after
                };
            }
        }

        //前方共起の配列を作るメソッド
        private SortedList<string,int> before(SortedList<string,int> before,List<string> item, int span, SortedList<int,int> temp_array,string[] node)
        {
            for (int num_gram = 0; num_gram <= span - 1; num_gram++)
            {
                for (int file_num = 0; file_num <= item.Count - 6; file_num++)
                {
                    if (before.ContainsKey(item[0] + "," + node[span] + "," + node[num_gram] + "," + file_num))
                    {
                        before[item[0] + "," + node[span] + "," + node[num_gram] + "," + file_num] += temp_array[file_num + 3];
                    }
                    else
                    {
                        before[item[0] + "," + node[span] + "," + node[num_gram] + "," + file_num] = temp_array[file_num + 3];

                    }
                }
            }
            return before;
        }

        //後方共起の配列を作るメソッド
        private SortedList<string, int> after(SortedList<string, int> after, List<string> item, int span, SortedList<int,int> temp_array, string[] node)
        {
            for (int num_gram = span + 1; num_gram <= temp_array[0] - 1; num_gram++)
            {
                for (int file_num = 0; file_num <= item.Count - 6; file_num++)
                {
                    if (after.ContainsKey(item[0] + "," + node[span] + "," + node[num_gram] + "," + file_num))
                    {
                        after[item[0] + "," + node[span] + "," + node[num_gram] + "," + file_num] += temp_array[file_num + 3];
                    }
                    else
                    {
                        after[item[0] + "," + node[span] + "," + node[num_gram] + "," + file_num] = temp_array[file_num + 3];
                    }
                }
            }
            return after;
        }

        //スコア計算の準備
        private List<string> pre_calc(SortedList<string,int> before_after, SortedList<string,int> count, List<int> number_of_words_array,int num_of_file,string mode)
        {
            int file_count = 1;
            string score = null;
            double calc = 0;

            double average = 0;
            double sum = 0;
            int inclusion_file = 0;
            List<string> result_array = new List<string>();

            foreach (KeyValuePair<string, int> item in before_after)
            {
                int app_freq = item.Value;
                string[] sub_arr = item.Key.Split(',');
                int node_freq;
                int other_freq;

                if (count.ContainsKey(sub_arr[1] + "," + sub_arr[3]))
                {
                    node_freq = count[sub_arr[1] + "," + sub_arr[3]];
                }
                else
                {
                    node_freq = 0;
                }

                if (count.ContainsKey(sub_arr[2] + "," + sub_arr[3]))
                {
                    other_freq = count[sub_arr[2] + "," + sub_arr[3]];
                }
                else
                {
                    other_freq = 0;
                }

                if(
                    (app_freq != 0)&&
                    (node_freq != 0)&&
                    (other_freq != 0)
                )
                {
                    //計算
                    calc = score_calc(mode,app_freq,node_freq,other_freq,number_of_words_array[int.Parse(sub_arr[3])]);
                    sum += calc;
                    score = score + calc + ",";
                    inclusion_file++;
                }
                else
                {
                    //0か空欄の処理
                    if ((mode == "T") || (mode == "MI"))
                    {
                        score = score + "" + ",";
                    }
                    else if (mode == "Freq")
                    {
                        calc = 0;
                        score = score + calc + ",";
                    }
                }

                if (file_count == num_of_file)
                {
                    average = sum / num_of_file;
                    //score = System.Text.RegularExpressions.Regex.Replace(score, "^,", "");
                    score = System.Text.RegularExpressions.Regex.Replace(score, ",$", "");
                    //出力用配列の作成
                    if ((mode == "T") || (mode == "MI"))
                    {
                        result_array.Add((int.Parse(sub_arr[0]) - 1) / 2 + "," + sub_arr[1] + "," + sub_arr[2] + "," + score + "," + average + "," + inclusion_file);
                    }
                    else if (mode == "Freq")
                    {
                        result_array.Add((int.Parse(sub_arr[0]) - 1) / 2 + "," + sub_arr[1] + "," + sub_arr[2] + "," + score + "," + sum + "," + average + "," + inclusion_file);
                    }

                    file_count = 1;
                    score = null;
                    average = sum = inclusion_file = 0;
                }
                else
                {
                    file_count++;
                }
            }
            return result_array;
        }
  
        //計算処理
        private double score_calc(string mode, int app_freq, int node_freq, int other_freq, int number_of_words)
        {
            double calc = new double();
            if (mode == "T")
            {
                calc = (app_freq - (double)(node_freq * other_freq) / (double)number_of_words) / Math.Sqrt(app_freq);
            }
            else if (mode == "MI")
            {
                calc = Math.Log((double)(app_freq * number_of_words) / (double)(node_freq * other_freq), 2);
            }
            else if (mode == "Freq")
            {
                calc = app_freq;
            }
            return calc;
        }

        //MI（単語ー書字形ー前方共起）
        public List<string> syoji_mi_bef(SortedList<string,int> before,SortedList<string,int> count,List<int> number_of_words_array,int num_of_file)
        {
            return this.pre_calc(before, count, number_of_words_array, num_of_file, "MI");
        }

        //MI（単語ー語彙素形ー前方共起）
        public List<string> goiso_mi_bef(SortedList<string, int> before, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(before, count, number_of_words_array, num_of_file, "MI");
        }

        //MI（単語ー書字形ー後方共起）
        public List<string> syoji_mi_aft(SortedList<string, int> after, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(after, count, number_of_words_array, num_of_file, "MI");
        }

        //MI（単語ー語彙素形ー後方共起）
        public List<string> goiso_mi_aft(SortedList<string, int> after, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(after, count, number_of_words_array, num_of_file, "MI");
        }

        //T（単語ー書字形ー前方共起）
        public List<string> syoji_t_bef(SortedList<string, int> before, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(before, count, number_of_words_array, num_of_file, "T");
        }

        //T（単語ー語彙素形ー前方共起）
        public List<string> goiso_t_bef(SortedList<string, int> before, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(before, count, number_of_words_array, num_of_file, "T");
        }

        //T（単語ー書字形ー後方共起）
        public List<string> syoji_t_aft(SortedList<string, int> after, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(after, count, number_of_words_array, num_of_file, "T");
        }

        //T（単語ー語彙素形ー後方共起）
        public List<string> goiso_t_aft(SortedList<string, int> after, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(after, count, number_of_words_array, num_of_file, "T");
        }

        //共起頻度（単語ー書字形ー前方共起）
        public List<string> syoji_freq_bef(SortedList<string, int> before, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(before, count, number_of_words_array, num_of_file, "Freq");
        }

        //共起頻度（単語ー語彙素形ー前方共起）
        public List<string> goiso_freq_bef(SortedList<string, int> before, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(before, count, number_of_words_array, num_of_file, "Freq");
        }

        //共起頻度（単語ー書字形ー後方共起）
        public List<string> syoji_freq_aft(SortedList<string, int> after, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(after, count, number_of_words_array, num_of_file, "Freq");
        }

        //共起頻度（単語ー語彙素形ー後方共起）
        public List<string> goiso_freq_aft(SortedList<string, int> after, SortedList<string, int> count, List<int> number_of_words_array, int num_of_file)
        {
            return this.pre_calc(after, count, number_of_words_array, num_of_file, "Freq");
        }

        //配列コピーメソッド
        public SortedList<string,int> clone(SortedList<string,int> array)
        {
            SortedList<string,int> copy_array = new SortedList<string,int>();
            if (array != null)
            {
                foreach (KeyValuePair<string, int> item in array)
                {
                    copy_array.Add(item.Key, item.Value);
                }
            }
            else
            {
                copy_array = null;
            }
            return copy_array;
        }
    }
}