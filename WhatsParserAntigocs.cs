using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avilla_Forensics
{
    public partial class WhatsParserAntigocs  Form
    {
        public WhatsParserAntigocs()
        {
            InitializeComponent();
        }
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
             Create a new database connection
            sqlite_conn = new SQLiteConnection(Data Source= + caminho.caminhoDB + ;Version=3;New=True;Compress=True;);
             Open the connection
            try
            {
                sqlite_conn.Open();
            }
            catch
            {

            }
            return sqlite_conn;
        }

        public static DateTime UnixTimeToDateTime(long unixTime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTime).ToLocalTime();
            return dateTime;
        }

        public class caminho Variável Pública
        {
            public static string caminhoLOCAL = ;
            public static string caminhoIMG = ;
            public static string caminhoDB = ;

            public static string IDCHAT = ;
            public static string JID = ;
            public static string GRUPO = ;

            public static string SEPARASTRING = ;
        }  

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog backupfolderIPEDArquivo = new FolderBrowserDialog();
            backupfolderIPEDArquivo.Description = Choose source folder;
            if (backupfolderIPEDArquivo.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = backupfolderIPEDArquivo.SelectedPath;
                caminho.caminhoLOCAL = backupfolderIPEDArquivo.SelectedPath;
                webBrowser2.Navigate(backupfolderIPEDArquivo.SelectedPath);
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog backupfolderIPEDArquivo = new FolderBrowserDialog();
            backupfolderIPEDArquivo.Description = Choose source folder;
            if (backupfolderIPEDArquivo.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = backupfolderIPEDArquivo.SelectedPath;
                caminho.caminhoIMG = backupfolderIPEDArquivo.SelectedPath;
                button10.Enabled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            define as propriedades do controle 
            OpenFileDialog
            this.ofd2.Multiselect = true;
            this.ofd2.Title = Select File;
            ofd2.InitialDirectory = @C;
            ofd2.Filter = Files (.db).db;
            ofd2.CheckFileExists = true;
            ofd2.CheckPathExists = true;
            ofd2.FilterIndex = 2;
            ofd2.RestoreDirectory = true;
            ofd2.ReadOnlyChecked = true;
            ofd2.ShowReadOnly = true;

            DialogResult drIPED = this.ofd2.ShowDialog();

            if (drIPED == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = ofd2.FileName;
                caminho.caminhoDB = ofd2.FileName;
                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBoxMESSAGE.Items.Clear();

            pictureBox2.Visible = true;
            tabControl1.Enabled = false;

            backgroundWorker1.RunWorkerAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageList1.Images.Clear();
            listView1.Items.Clear();
            listBoxMESSAGEMEDIA.Items.Clear();
            listBoxMESSAGE.Items.Clear();
            listBox1.Visible = false;
            label5.Text = ;

            string pathBin = @bin;
            string fullPathBin;
            fullPathBin = Path.GetFullPath(pathBin);

            try             
            {
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();

                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = SELECT  FROM chat_view WHERE hidden=0 and raw_string_jid like % + textBoxNUMBER.Text + %;

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                int myreaderID;
                string myreaderRawJID;
                string myreaderGrupo;

                int contar = 0;

                while (sqlite_datareader.Read())
                {
                    myreaderID = sqlite_datareader.GetInt16(0);

                    try
                    {
                        myreaderRawJID = sqlite_datareader.GetString(1);
                    }
                    catch
                    {
                        myreaderRawJID = NULL;
                    }

                    try
                    {
                        imageList1.Images.Add(Image.FromFile(caminho.caminhoIMG +  + myreaderRawJID + .j));
                    }
                    catch
                    {
                        imageList1.Images.Add(Image.FromFile(fullPathBin + erro2.png));
                    }

                    try
                    {
                        myreaderGrupo = sqlite_datareader.GetString(3); subject
                    }
                    catch
                    {
                        myreaderGrupo = .;
                    }

                    listView1.Items.Add(myreaderID +  + myreaderRawJID +  + myreaderGrupo, contar);

                    contar++;

                    listBoxMESSAGEMEDIA.Items.Add(myreaderID +  + myreaderRawJID +  + myreaderGrupo);
                }
                sqlite_conn.Close();
                listView1.Enabled = true;
                button5.Enabled = true;
            }
            catch 
            {
                MessageBox.Show(Select the database, Heads up);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBoxMESSAGE.Items.Clear();

            pictureBox2.Visible = true;
            tabControl1.Enabled = false;

            backgroundWorker2.RunWorkerAsync();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Visible = true;

            string[] Separa = listView1.Items[listView1.FocusedItem.Index].Text.Split('');
            caminho.IDCHAT = Separa[0]; _id                
                                    
            label5.Text = Separa[1]; raw_string_jid            
            caminho.JID = Separa[1]; raw_string_jid
            caminho.GRUPO = Separa[2]; subject (Grupo)

            button3.Enabled = true;
        }

        private void listBoxMESSAGEMEDIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Separa = listBoxMESSAGEMEDIA.Text.Split('');
            caminho.SEPARASTRING = Separa[1];

            webBrowser1.Navigate(caminho.caminhoLOCAL + WhatsAppChat- + Separa[1] + .html);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(chrome.exe, caminho.caminhoLOCAL + WhatsAppChat- + caminho.SEPARASTRING + .html);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Escerver Corpo HTML
            StreamWriter EscreverTXT = new StreamWriter(@caminho.caminhoLOCAL + WhatsAppChat- + caminho.JID + .html);
            EscreverTXT.WriteLine($!DOCTYPE html);
            EscreverTXT.WriteLine($html);
            EscreverTXT.WriteLine($    head);
            EscreverTXT.WriteLine($        title{caminho.JID} {caminho.GRUPO}title);
            EscreverTXT.WriteLine($        meta http-equiv=Content-Type content=texthtml; charset=UTF-8 );
            EscreverTXT.WriteLine($        meta name=viewport content=width=device-width );
            EscreverTXT.WriteLine($        meta charset=UTF-8 );
            EscreverTXT.WriteLine($        link rel=shortcut icon href=${{favicon}} );
            EscreverTXT.WriteLine($        style);
            EscreverTXT.WriteLine($            body);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-imageurl(dataimagejpg;base64,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););
            EscreverTXT.WriteLine($	background-size 642px;);
            EscreverTXT.WriteLine($	background-repeatrepeat-y;);
            EscreverTXT.WriteLine($	margin0 auto;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($div.audioImg {{);
            EscreverTXT.WriteLine($    width 100px;);
            EscreverTXT.WriteLine($    height 102px;);
            EscreverTXT.WriteLine($    background-imageurl(dataimagepng;base64,iVBORw0KGgoAAAANSUhEUgAAAIMAAACFCAMAAABPGrtJAAABv1BMVEXnlIHkjnrfg27cfGbbeWLad2DkjXnlj3zef2nbeGHee2PffGTgfWXdfmjlj3vji3fcemPiinXceWHegGrghnHgg2zffmbffWXce2TjjHjggWnkj3vgf2fkkHzhgGnihWihnDjjXnihG3jinThiHPlkHziiXTiiHLkkHvggGnonIrggmvmlYPomojji3blkX3mlIHom4nqopLhf2jol4Tihmefmjnk4DomIbpn43gfmbpoInlIDff2jomIXmlYLpnozpnYziiXXlkn7onYvdfGbmknrqZnkjnnmk4DefWfol4XkjXjfgm3rqJjrp5fjiHLhgmvjinXomYfpn47rpJTsq5ztrqDtsKLjinbonYzjjHfkjHjcemLki3bpnIrpo5LgfmfnmojhhnHhh3Lnl4TqoZDdemLdf2nqopHqpZXfg23ihG7rpZbnloPhg2zdfWfhgWrrppbad2Hce2XaeGHsqJnjh3HrqJnsqprlk3lkXzrqpvnlYLrpZXpoZDig23pnYvtrJ3mloPhgGjsrJzqo5Lsq53nmIbnmIXsrJ3qpJPtrZqpZTmlYHtrppm4ntr6Hrp5jsr6DtsaN3l1VvAAACsklEQVR42u2Z+XPSQBSAA41iyBbopVarVhTUKhUBbQQtxatVbAPFUoWE6X1oIj1Ntb7wqMef7Cj+xJICR2dySY6877f3u7O7je7YY8HxyEIgiAIgiDBg5nC7+Cb3GutM3AtUpwi4QQ0S20euxR8PrcREX0ee1QaGsn9bS3Wag8RE9PuuXo4MspcPyX4TY4CA6LHboJI10WuzQZeDQZbHDagOHNRY7ECPQAR3QAR3QAR3QAR3QAR3QYXnW8rxoFDN23Km+6wjg6zHsIeA4UeqNtAw42mO2yiHXdD2Gvg0At1m2noN91hC+14K4QBA4cA1AVpuM10h+204x0Q9hk49EHdThqawzfBQ98Fw1DQ0KSFatRvSAmHTHfYQWJEoksUohH9UpCQ6Q6xvbTnfQM0lvbHdQrxhEQrDhyET3LQA0iCYMFIR5K1M9ENJGC8kOwFIcZbFJHYLSjx6AgNTyiKYwMqwrHT0BRioFD+iR0PqqWSGNyOJONZzNheUxSC50wDeM5Frv1KXBwB7Wi3ER+8vRkfqI23hk1b3qWyYkRUifCd65pm6lpaHP+Aptj66L6EU43SwxPqYnTSxFGR2f6spYYvmLYoMCrDa5KrM7va+pqkKJBoj4wU1Srx2fZXSKua8elyI+69AY3eC11eMWw4tMSZ7TtgS3UC446KY54Cm0CrUEm5XYixvU2nZX5egLgp35ssz5fm7QrEufZ2tSBxTpHv39YnyX+hvMg9yHGNiDx+R5Xj8ZJBjjvJ04VlTg7lERbHkXv8xctXhgbR5Oshqx4Xypu37yNcD+Q1XhrKM0+FTMlO7xcQzn6tfIiWLH1rcYjX9RsoyNz1UXOFpQcOOQUzj7seeyiAzqgAzqgw9wg8IhCIIgCIIgCIIgCIIgCIIgyHFTKdifb88m8QAAAAAElFTkSuQmCC););
            EscreverTXT.WriteLine($    background-size 100% 100%;);
            EscreverTXT.WriteLine($    margin0;);
            EscreverTXT.WriteLine($    padding0;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($div.videoImg {{);
            EscreverTXT.WriteLine($    width 100px;);
            EscreverTXT.WriteLine($    height 102px;);
            EscreverTXT.WriteLine($    background-imageurl(dataimagepng;base64,iVBORw0KGgoAAAANSUhEUgAAAIMAAACFCAAAAABdrxSnAAABIElEQVR42u3Wz27CMAzHcTsFqRIdu5QDe7C9x1pBxb1D22SPQBDYlbA2TtuZY+zc+Oq+igTBgAEDBgwYMGDAgAHDfzBszJV5uKS7Xjw8zLCciwbfc8hnUVNhkatCq2E0nl5cr8OxGhbT4cU5hHpZJEPNMHwhpunTeAlZq09mz+PQcXVcCuG5xmWz6zedVU1H1fNOxNDBgwYMDwRwwvrb+h9H3jbchj87ZX38okTV13cfk3JNlXm4GYjVsKwZiNbSWQMbu2Fbsh92QLIGEvhGJlQzr4ZRME1Ixi5KP+63W6VfzbKape73vA+LDDFJmdhYGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwH7DF6f1Qrdy7jA4AAAAAElFTkSuQmCC););
            EscreverTXT.WriteLine($    background-size 100% 100%;);
            EscreverTXT.WriteLine($    margin0;);
            EscreverTXT.WriteLine($    padding0;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($div.documento {{);
            EscreverTXT.WriteLine($    width 100px;);
            EscreverTXT.WriteLine($    height 102px;);
            EscreverTXT.WriteLine($    background-imageurl(dataimagepng;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAYAAADDPmHLAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAC8lJREFUeJztnVuMVdUdxr9v7eOAIxqLQUNNuGgldGJok1pMS8KMLRMOzBD6opbORQxJ30wbSWMsDCeHAxrbYGx8ahuiMozG9qESZ + CQwQAmPIC + SJUSx8tIYqgQwBg7CMxeXx  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););
            EscreverTXT.WriteLine($    background-size 100% 100%;);
            EscreverTXT.WriteLine($    margin0;);
            EscreverTXT.WriteLine($    padding0;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#topbar);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	positionfixed;);
            EscreverTXT.WriteLine($	background#475959;);
            EscreverTXT.WriteLine($	border1px solid #394d4d;);
            EscreverTXT.WriteLine($	width640px;);
            EscreverTXT.WriteLine($	z-index100;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#topbar.telegram);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	positionfixed;);
            EscreverTXT.WriteLine($	background#5682a3;);
            EscreverTXT.WriteLine($	border1px solid #394d4d;);
            EscreverTXT.WriteLine($	width640px;);
            EscreverTXT.WriteLine($	z-index100;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#topbar .left);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	float left;);
            EscreverTXT.WriteLine($	displayblock;);
            EscreverTXT.WriteLine($	margin0.5em;);
            EscreverTXT.WriteLine($	background#475959;);
            EscreverTXT.WriteLine($	color #ffffff;);
            EscreverTXT.WriteLine($	font-family 'Roboto-Medium';);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#topbar .right);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	float right;);
            EscreverTXT.WriteLine($	displayblock;);
            EscreverTXT.WriteLine($	margin0.5em;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#chatlist);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	width 640px;);
            EscreverTXT.WriteLine($	font-family 'Roboto-Light';);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#chatlist .contact);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	height 64px;);
            EscreverTXT.WriteLine($	background-color #ffffff;);
            EscreverTXT.WriteLine($	border-bottom 2px solid #e5e5e5;);
            EscreverTXT.WriteLine($	display block;);
            EscreverTXT.WriteLine($	padding 6px 6px;);
            EscreverTXT.WriteLine($	position relative;);
            EscreverTXT.WriteLine($	vertical-align top;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#chatlist .contacthover);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #1ba4d3;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#chatlist .left);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	float left;);
            EscreverTXT.WriteLine($	displayblock;);
            EscreverTXT.WriteLine($	margin0.5em;);
            EscreverTXT.WriteLine($	color #ffffff;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#chatlist .right);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	float right;);
            EscreverTXT.WriteLine($	displayblock;);
            EscreverTXT.WriteLine($	margin0.5em;);
            EscreverTXT.WriteLine($	color #666666;);
            EscreverTXT.WriteLine($	font-family 'Roboto-Light';);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	width 640px;);
            EscreverTXT.WriteLine($	font-family 'Roboto-Light';);
            EscreverTXT.WriteLine($	color #000000;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .linha);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	display block;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .linhaafter);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	content .;);
            EscreverTXT.WriteLine($    display block;);
            EscreverTXT.WriteLine($    height 0;);
            EscreverTXT.WriteLine($    clear both;);
            EscreverTXT.WriteLine($    visibility hidden;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .incoming, #conversation .outgoing,);
            EscreverTXT.WriteLine($ #conversation .date, #conversation .systemmessage, );
            EscreverTXT.WriteLine($ #conversation .specialmessage {{);
            EscreverTXT.WriteLine($	cursor pointer;);
            EscreverTXT.WriteLine($	border-radius 2px;);
            EscreverTXT.WriteLine($	box-shadow 0 0 6px #b8b7b7;);
            EscreverTXT.WriteLine($	display block;);
            EscreverTXT.WriteLine($	padding 10px 18px;);
            EscreverTXT.WriteLine($	vertical-align top;);
            EscreverTXT.WriteLine($	position relative;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .date);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #d7f5ff;);
            EscreverTXT.WriteLine($	color #000000;);
            EscreverTXT.WriteLine($	width 160px;);
            EscreverTXT.WriteLine($	margin 0 auto;);
            EscreverTXT.WriteLine($	text-align center;);
            EscreverTXT.WriteLine($	margin-top 8px;);
            EscreverTXT.WriteLine($	margin-bottom 8px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .systemmessage);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #f7f76e;);
            EscreverTXT.WriteLine($	color #333333;);
            EscreverTXT.WriteLine($	margin 0 auto;);
            EscreverTXT.WriteLine($	text-align center;);
            EscreverTXT.WriteLine($	margin-top 8px;);
            EscreverTXT.WriteLine($	margin-bottom 8px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .systemmessagehover);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #f0f075;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .pages);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #f4fa58;);
            EscreverTXT.WriteLine($	color #333333;);
            EscreverTXT.WriteLine($	width 300px;);
            EscreverTXT.WriteLine($	margin 0 auto;);
            EscreverTXT.WriteLine($	text-align center;);
            EscreverTXT.WriteLine($	margin-top 8px;);
            EscreverTXT.WriteLine($	margin-bottom 8px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .time);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	color #888888;);
            EscreverTXT.WriteLine($	float right;);
            EscreverTXT.WriteLine($	padding-left 12px;);
            EscreverTXT.WriteLine($	font-size small;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .incoming);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	color #000000;);
            EscreverTXT.WriteLine($	background-color #fcfbf6;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .incominghover);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #def1f3;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .incomingbefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #fcfbf6;);
            EscreverTXT.WriteLine($	content 00a0;);
            EscreverTXT.WriteLine($	display block;);
            EscreverTXT.WriteLine($	height 16px;);
            EscreverTXT.WriteLine($	position absolute;);
            EscreverTXT.WriteLine($	top 11px;);
            EscreverTXT.WriteLine($	transform         rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	-moz-transform    rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	-ms-transform     rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -o-transform      rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -webkit-transform rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	width  20px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .incominghoverbefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #def1f3;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .specialmessage);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	color #333333;);
            EscreverTXT.WriteLine($	background-color #66ccff;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .specialmessagehover);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #00aaff;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .specialmessagebefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #66ccff;);
            EscreverTXT.WriteLine($	content 00a0;);
            EscreverTXT.WriteLine($	display block;);
            EscreverTXT.WriteLine($	height 16px;);
            EscreverTXT.WriteLine($	position absolute;);
            EscreverTXT.WriteLine($	top 11px;);
            EscreverTXT.WriteLine($	transform         rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	-moz-transform    rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	-ms-transform     rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -o-transform      rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -webkit-transform rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	width  20px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .specialmessagehoverbefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #00aaff;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .outgoing);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #def1f3;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .outgoinghover);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #def1f3;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .outgoingbefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #def1f3;);
            EscreverTXT.WriteLine($	content 00a0;);
            EscreverTXT.WriteLine($	display block;);
            EscreverTXT.WriteLine($	height 16px;);
            EscreverTXT.WriteLine($	position absolute;);
            EscreverTXT.WriteLine($	top 11px;);
            EscreverTXT.WriteLine($	transform         rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -moz-transform    rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -ms-transform     rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -o-transform      rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($        -webkit-transform rotate(29deg) skew(-35deg););
            EscreverTXT.WriteLine($	width  20px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .outgoinghoverbefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	background-color #def1f3;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .to);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	float right; );
            EscreverTXT.WriteLine($	margin 5px 20px 5px 45px;);
            EscreverTXT.WriteLine($	text-align right;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .tobefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	box-shadow 2px -2px 2px 0 rgba(178,178,178,.4););
            EscreverTXT.WriteLine($	right -9px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .from);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	float left;);
            EscreverTXT.WriteLine($	margin 5px 45px 5px 20px; );
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .frombefore);
            EscreverTXT.WriteLine(${{);
            EscreverTXT.WriteLine($	box-shadow -2px 2px 2px 0 rgba(178,178,178,.4););
            EscreverTXT.WriteLine($	left -9px; );
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($#conversation .thumb {{);
            EscreverTXT.WriteLine($	max-width 160px;);
            EscreverTXT.WriteLine($	max-height 160px;);
            EscreverTXT.WriteLine($}});
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($            .tab {{display inline-block; border-collapse collapse; border 1px solid black;}});
            EscreverTXT.WriteLine($            .cel {{border-colapse colapse; border 1px solid black; font-family Arial, sans-serif;}});
            EscreverTXT.WriteLine($            .check {{vertical - align top;}});
            EscreverTXT.WriteLine($        style);
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($		script src='httpscode.jquery.comjquery-3.6.0.min.js'script);
            EscreverTXT.WriteLine($		script);
            EscreverTXT.WriteLine($			$(document).ready(function() {{);
            EscreverTXT.WriteLine($				$('body').on('click', '.audio', function (e) {{);
            EscreverTXT.WriteLine($					e.preventDefault(););
            EscreverTXT.WriteLine($					$('.audio-control').remove(););
            EscreverTXT.WriteLine($					$(this).prepend(audio controls class='audio-control'source src=' + $(this).attr('href') + ' type='audioogg'audiobr););
            EscreverTXT.WriteLine($				}}););
            EscreverTXT.WriteLine($			}}););
            EscreverTXT.WriteLine($		script);
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($    head);
            EscreverTXT.WriteLine($    body);
            EscreverTXT.WriteLine($        div id=topbar);
            EscreverTXT.WriteLine($          span class=left);
            try
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(@caminho.caminhoIMG +  + caminho.JID + .j);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                EscreverTXT.WriteLine($img src=dataimagejpg;base64,{base64ImageRepresentation} width=40 height=40 );
            }
            catch
            {
                string base64ImageRepresentation = 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;
                EscreverTXT.WriteLine($img src=dataimagejpg;base64,{base64ImageRepresentation} width=40 height=40 );
            }
            EscreverTXT.WriteLine($            WhatsApp Chat - {caminho.JID} {caminho.GRUPO});
            EscreverTXT.WriteLine($          span);
            EscreverTXT.WriteLine($        div);
            EscreverTXT.WriteLine($        div id=conversationbrbrbr);
            EscreverTXT.WriteLine($        div class=linha);
            EscreverTXT.WriteLine($);

            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = SELECT  FROM messages LEFT JOIN message_media ON message_media.message_row_id = messages._id WHERE status != 6 and _id  1 and messages.key_remote_jid= + caminho.JID + ;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            int myreaderID;
            string KeyJID;
            int myreaderFROM;
            string myreaderDATA;
            string myreaderTYPE;
            Int64 myreaderTIME;
            string myreaderTIMEConvertido;
            string myreaderContatoGrupo;

            while (sqlite_datareader.Read())
            {

                myreaderID = sqlite_datareader.GetInt32(0);  _id                  
                KeyJID = sqlite_datareader.GetString(1);  key_remote_jid     


                try
                {
                    myreaderFROM = sqlite_datareader.GetInt32(2); key_from_me
                }
                catch
                {
                    myreaderFROM = 0000;
                }

                try
                {
                    myreaderDATA = sqlite_datareader.GetString(6); data
                }
                catch
                {
                    try
                    {
                        myreaderDATA = sqlite_datareader.GetString(50); file_patch
                    }
                    catch
                    {
                        myreaderDATA = NULL;
                    }
                }

                try
                {
                    myreaderTYPE = sqlite_datareader.GetString(10); media_wa_type
                }
                catch
                {
                    myreaderTYPE = NULL;
                }

                try
                {
                    myreaderTIME = sqlite_datareader.GetInt64(7); timestamp
                    myreaderTIMEConvertido = UnixTimeToDateTime(myreaderTIME).ToString();

                }
                catch
                {
                    myreaderTIMEConvertido = 0000;
                }

                try
                {
                    myreaderContatoGrupo = sqlite_datareader.GetString(20); remote_resource (Contato Grupo)
                }
                catch
                {
                    myreaderContatoGrupo = NULL;
                }

                listBoxMESSAGE.Items.Add(myreaderDATA +    + myreaderTIMEConvertido);

                if (myreaderTYPE == 2) Audio
                {
                    if (myreaderFROM == 0) Recebido
                    {
                        EscreverTXT.WriteLine($div class=linha id=305672);
                        EscreverTXT.WriteLine($div class=incoming from);

                        if (caminho.GRUPO != .)
                        {
                            EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                            EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbra href='.{myreaderDATA}' class='audio play'{myreaderDATA}abr);                          
                        }
                        else
                        {
                            EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                            EscreverTXT.WriteLine($({caminho.JID})spanbra href='.{myreaderDATA}' class='audio play'{myreaderDATA}abr);
                        }

                        try
                        {
                            myreaderDATA = sqlite_datareader.GetString(50); file_patch
                        }
                        catch
                        {
                            myreaderDATA = NULL;
                        }

                        Transcricao
                        string fullPathTXT;
                        String str = myreaderDATA;
                        StringBuilder sb = new StringBuilder(str);
                        fullPathTXT = sb.Replace(.opus, .txt).ToString();

                        Link TXT Transcrição
                        if (checkBoxLinkaudio.Checked) 
                        {
                            EscreverTXT.WriteLine($a  href=.{fullPathTXT}{fullPathTXT}a);
                        }

                        Plotagem Transcrição no HTML
                        if (checkBoxEscreverAudio.Checked)
                        {
                            EscreverTXT.WriteLine($iframe src=.{fullPathTXT} frameborder=0 scrolling=auto height=200 width=400iframe);
                        }

                        Transcrição Direta
                        if (checkBoxDuranteParser.Checked) 
                        {
                            string fullPathTXT2;
                            String str2 = myreaderDATA;
                            StringBuilder sb2 = new StringBuilder(str2);
                            fullPathTXT2 = sb2.Replace(, ).ToString();
                            fullPathTXT2 = textBox2.Text +  + fullPathTXT2;

                            string pathListen = @binlisten;
                            string fullPath;
                            fullPath = Path.GetFullPath(pathListen);

                            Process process3 = new Process();
                            ProcessStartInfo startInfo3 = new ProcessStartInfo();
                            startInfo3.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo3.CreateNoWindow = true;
                            startInfo3.UseShellExecute = false;
                            startInfo3.RedirectStandardOutput = true;
                            startInfo3.WorkingDirectory = fullPath;
                            startInfo3.FileName = fullPath + listen.exe;
                            startInfo3.Arguments =   + fullPathTXT2 + ;
                            process3.StartInfo = startInfo3;
                            process3.Start();
                            process3.StandardOutput.ReadToEnd();
                            process3.Close();

                            string fullPathWAV;
                            String str3 = fullPathTXT2;
                            StringBuilder sb3 = new StringBuilder(str3);
                            fullPathWAV = sb3.Replace(.opus, .wav).ToString();
                            try
                            {
                                File.Delete(fullPathWAV);
                            }
                            catch
                            {
                            }
                            
                        }

                        EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                        EscreverTXT.WriteLine($div class=audioImg title=Audiodiv);
                        EscreverTXT.WriteLine($a);
                        EscreverTXT.WriteLine($span class=time);
                        EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                        EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                        EscreverTXT.WriteLine($div class=delivereddiv);
                        EscreverTXT.WriteLine($span);
                        EscreverTXT.WriteLine($);
                        EscreverTXT.WriteLine($divdiv);
                        EscreverTXT.WriteLine($);
                    }
                    else Enviado
                    {
                        EscreverTXT.WriteLine($div class=linha id=305672);
                        EscreverTXT.WriteLine($div class=outgoing to);

                        EscreverTXT.WriteLine($spanbra href='.{myreaderDATA}' class='audio play'{myreaderDATA}abr);

                        try
                        {
                            myreaderDATA = sqlite_datareader.GetString(50); file_patch
                        }
                        catch
                        {
                            myreaderDATA = NULL;
                        }

                        Transcricao
                        string fullPathTXT;
                        String str = myreaderDATA;
                        StringBuilder sb = new StringBuilder(str);
                        fullPathTXT = sb.Replace(.opus, .txt).ToString();

                        Link TXT Transcrição
                        if (checkBoxLinkaudio.Checked)
                        {
                            EscreverTXT.WriteLine($a  href=.{fullPathTXT}{fullPathTXT}a);
                        }

                        Plotagem Transcrição no HTML
                        if (checkBoxEscreverAudio.Checked)
                        {
                            EscreverTXT.WriteLine($iframe src=.{fullPathTXT} frameborder=0 scrolling=auto height=200 width=400iframe);
                        }

                        Transcrição Direta
                        if (checkBoxDuranteParser.Checked)
                        {
                            string fullPathTXT2;
                            String str2 = myreaderDATA;
                            StringBuilder sb2 = new StringBuilder(str2);
                            fullPathTXT2 = sb2.Replace(, ).ToString();
                            fullPathTXT2 = textBox2.Text +  + fullPathTXT2;

                            string pathListen = @binlisten;
                            string fullPath;
                            fullPath = Path.GetFullPath(pathListen);

                            Process process3 = new Process();
                            ProcessStartInfo startInfo3 = new ProcessStartInfo();
                            startInfo3.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo3.CreateNoWindow = true;
                            startInfo3.UseShellExecute = false;
                            startInfo3.RedirectStandardOutput = true;
                            startInfo3.WorkingDirectory = fullPath;
                            startInfo3.FileName = fullPath + listen.exe;
                            startInfo3.Arguments =   + fullPathTXT2 + ;
                            process3.StartInfo = startInfo3;
                            process3.Start();
                            process3.StandardOutput.ReadToEnd();
                            process3.Close();

                            string fullPathWAV;
                            String str3 = fullPathTXT2;
                            StringBuilder sb3 = new StringBuilder(str3);
                            fullPathWAV = sb3.Replace(.opus, .wav).ToString();
                            try
                            {
                                File.Delete(fullPathWAV);
                            }
                            catch
                            {
                            }
                            
                        }

                        EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                        EscreverTXT.WriteLine($div class=audioImg title=Audiodiv);
                        EscreverTXT.WriteLine($a);
                        EscreverTXT.WriteLine($span class=time);
                        EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                        EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                        EscreverTXT.WriteLine($div class=delivereddiv);
                        EscreverTXT.WriteLine($span);
                        EscreverTXT.WriteLine($);
                        EscreverTXT.WriteLine($divdiv);
                        EscreverTXT.WriteLine($);
                    }
                }
                else
                {
                    if (myreaderTYPE == 1  myreaderTYPE == 20  myreaderTYPE == 45) Imagem Jpg = 1  webp = 20  jpeg = 45
                    {
                        if (myreaderFROM == 0) Recebido
                        {
                            EscreverTXT.WriteLine($div class=linha id=305644);
                            EscreverTXT.WriteLine($div class=incoming from);
                            EscreverTXT.WriteLine($span style=font-family 'Roboto-Medium'; color #b4c74b;);

                            if (caminho.GRUPO != .)
                            {
                                EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                            }
                            else
                            {
                                EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                            }

                            try
                            {
                                myreaderDATA = sqlite_datareader.GetString(50); file_patch
                            }
                            catch
                            {
                                myreaderDATA = NULL;
                            }

                            EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                            EscreverTXT.WriteLine($img class=thumb src=.{myreaderDATA} title=image);
                            EscreverTXT.WriteLine($a);
                            EscreverTXT.WriteLine($span class=time{myreaderTIMEConvertido} &nbsp; span);
                            EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                            EscreverTXT.WriteLine($span);
                            EscreverTXT.WriteLine($);
                            EscreverTXT.WriteLine($divdiv);
                            EscreverTXT.WriteLine($);
                        }
                        else Enviado
                        {
                            EscreverTXT.WriteLine($div class=linha id=305644);
                            EscreverTXT.WriteLine($div class=outgoing to);
                            EscreverTXT.WriteLine($span style=font-family 'Roboto-Medium'; color #b4c74b;spanbr);

                            EscreverTXT.WriteLine($spanbr{myreaderDATA}br);

                            try
                            {
                                myreaderDATA = sqlite_datareader.GetString(50); file_patch
                            }
                            catch
                            {
                                myreaderDATA = NULL;
                            }

                            EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                            EscreverTXT.WriteLine($img class=thumb src=.{myreaderDATA} title=image);
                            EscreverTXT.WriteLine($a);
                            EscreverTXT.WriteLine($span class=time{myreaderTIMEConvertido} &nbsp; span);
                            EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                            EscreverTXT.WriteLine($span);
                            EscreverTXT.WriteLine($);
                            EscreverTXT.WriteLine($divdiv);
                            EscreverTXT.WriteLine($);
                        }
                    }
                    else
                    {
                        if (myreaderTYPE == 3  myreaderTYPE == 13) Video MP4 = 3  MP4 (GIF) = 13 
                        {

                            if (myreaderFROM == 0) Recebido
                            {
                                EscreverTXT.WriteLine($div class=linha id=305672);
                                EscreverTXT.WriteLine($div class=incoming from);

                                if (caminho.GRUPO != .)
                                {
                                    EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                }
                                else
                                {
                                    EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                }

                                try
                                {
                                    myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                }
                                catch
                                {
                                    myreaderDATA = NULL;
                                }

                                EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                EscreverTXT.WriteLine($div class=videoImg title=Videodiv);
                                EscreverTXT.WriteLine($a);
                                EscreverTXT.WriteLine($span class=time);
                                EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                                EscreverTXT.WriteLine($div class=delivereddiv);
                                EscreverTXT.WriteLine($span);
                                EscreverTXT.WriteLine($);
                                EscreverTXT.WriteLine($divdiv);
                                EscreverTXT.WriteLine($);

                            }
                            else Enviado
                            {
                                EscreverTXT.WriteLine($div class=linha id=305672);
                                EscreverTXT.WriteLine($div class=outgoing to);

                                EscreverTXT.WriteLine($spanbr{myreaderDATA}br);

                                try
                                {
                                    myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                }
                                catch
                                {
                                    myreaderDATA = NULL;
                                }

                                EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                EscreverTXT.WriteLine($div class=videoImg title=Videodiv);
                                EscreverTXT.WriteLine($a);
                                EscreverTXT.WriteLine($span class=time);
                                EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                                EscreverTXT.WriteLine($div class=delivereddiv);
                                EscreverTXT.WriteLine($span);
                                EscreverTXT.WriteLine($);
                                EscreverTXT.WriteLine($divdiv);
                                EscreverTXT.WriteLine($);
                            }
                        }
                        else
                        {
                            if (myreaderTYPE == 9) Documento PDF  RAR  EXE  (Qualquer Formato) 
                            {

                                if (myreaderFROM == 0) Recebido
                                {
                                    EscreverTXT.WriteLine($div class=linha id=305672);
                                    EscreverTXT.WriteLine($div class=incoming from);

                                    if (caminho.GRUPO != .)
                                    {
                                        EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                    }
                                    else
                                    {
                                        EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                    }

                                    try
                                    {
                                        myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                    }
                                    catch
                                    {
                                        myreaderDATA = NULL;
                                    }

                                    EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                    EscreverTXT.WriteLine($div class=documento title=Documentodiv);
                                    EscreverTXT.WriteLine($a);
                                    EscreverTXT.WriteLine($span class=time);
                                    EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                    EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                                    EscreverTXT.WriteLine($div class=delivereddiv);
                                    EscreverTXT.WriteLine($span);
                                    EscreverTXT.WriteLine($);
                                    EscreverTXT.WriteLine($divdiv);
                                    EscreverTXT.WriteLine($);

                                }
                                else Enviado
                                {
                                    EscreverTXT.WriteLine($div class=linha id=305672);
                                    EscreverTXT.WriteLine($div class=outgoing to);

                                    EscreverTXT.WriteLine($spanbr{myreaderDATA}br);

                                    try
                                    {
                                        myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                    }
                                    catch
                                    {
                                        myreaderDATA = NULL;
                                    }

                                    EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                    EscreverTXT.WriteLine($div class=documento title=Documentodiv);
                                    EscreverTXT.WriteLine($a);
                                    EscreverTXT.WriteLine($span class=time);
                                    EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                    EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                                    EscreverTXT.WriteLine($div class=delivereddiv);
                                    EscreverTXT.WriteLine($span);
                                    EscreverTXT.WriteLine($);
                                    EscreverTXT.WriteLine($divdiv);
                                    EscreverTXT.WriteLine($);
                                }
                            }
                            else
                            {
                                Texto
                                if (myreaderFROM == 0) Recebido
                                {
                                    EscreverTXT.WriteLine($div class=linha id=307748);
                                    EscreverTXT.WriteLine($div class=incoming from);
                                    EscreverTXT.WriteLine($span style=font-family 'Roboto-Medium'; color #b4c74b;);

                                    if (caminho.GRUPO != .)
                                    {
                                        EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                    }
                                    else
                                    {
                                        EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                    }

                                    EscreverTXT.WriteLine($span class=time{myreaderTIMEConvertido} &nbsp; span);
                                    EscreverTXT.WriteLine($brspan class=timeReceived &nbsp;div class=delivereddivspan);
                                    EscreverTXT.WriteLine($);
                                    EscreverTXT.WriteLine($divdiv);
                                    EscreverTXT.WriteLine($);
                                }
                                else Enviado
                                {
                                    EscreverTXT.WriteLine($div class=linha id=307746);
                                    EscreverTXT.WriteLine($div class=outgoing to);
                                    EscreverTXT.WriteLine(${myreaderDATA}brspan class=time{myreaderTIMEConvertido} &nbsp;div class=delivereddivspan);
                                    EscreverTXT.WriteLine($brspan class=timeSent &nbsp;div class=delivereddivspan);
                                    EscreverTXT.WriteLine($);
                                    EscreverTXT.WriteLine($divdiv);
                                    EscreverTXT.WriteLine($);
                                }
                            }
                        }
                    }
                }
            }
            EscreverTXT.WriteLine($);
            EscreverTXT.WriteLine($        brbrbr);
            EscreverTXT.WriteLine($        div id=lastmsg&nbsp;div);
            EscreverTXT.WriteLine($    body);
            EscreverTXT.WriteLine($html);

            sqlite_conn.Close();

            EscreverTXT.Close();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox2.Visible = false;
            tabControl1.Enabled = true;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string Items in listBoxMESSAGEMEDIA.Items)
            {
                string[] Separa = Items.Split('');

                caminho.IDCHAT = Separa[0];
                caminho.JID = Separa[1];
                caminho.GRUPO = Separa[2];

                
                

                Escerver Corpo HTML
                StreamWriter EscreverTXT = new StreamWriter(@caminho.caminhoLOCAL + WhatsAppChat- + caminho.JID + .html);
                EscreverTXT.WriteLine($!DOCTYPE html);
                EscreverTXT.WriteLine($html);
                EscreverTXT.WriteLine($    head);
                EscreverTXT.WriteLine($        title{caminho.JID} {caminho.GRUPO}title);
                EscreverTXT.WriteLine($        meta http-equiv=Content-Type content=texthtml; charset=UTF-8 );
                EscreverTXT.WriteLine($        meta name=viewport content=width=device-width );
                EscreverTXT.WriteLine($        meta charset=UTF-8 );
                EscreverTXT.WriteLine($        link rel=shortcut icon href=${{favicon}} );
                EscreverTXT.WriteLine($        style);
                EscreverTXT.WriteLine($            body);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-imageurl(dataimagejpg;base64,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););
                EscreverTXT.WriteLine($	background-size 642px;);
                EscreverTXT.WriteLine($	background-repeatrepeat-y;);
                EscreverTXT.WriteLine($	margin0 auto;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($div.audioImg {{);
                EscreverTXT.WriteLine($    width 100px;);
                EscreverTXT.WriteLine($    height 102px;);
                EscreverTXT.WriteLine($    background-imageurl(dataimagepng;base64,iVBORw0KGgoAAAANSUhEUgAAAIMAAACFCAMAAABPGrtJAAABv1BMVEXnlIHkjnrfg27cfGbbeWLad2DkjXnlj3zef2nbeGHee2PffGTgfWXdfmjlj3vji3fcemPiinXceWHegGrghnHgg2zffmbffWXce2TjjHjggWnkj3vgf2fkkHzhgGnihWihnDjjXnihG3jinThiHPlkHziiXTiiHLkkHvggGnonIrggmvmlYPomojji3blkX3mlIHom4nqopLhf2jol4Tihmefmjnk4DomIbpn43gfmbpoInlIDff2jomIXmlYLpnozpnYziiXXlkn7onYvdfGbmknrqZnkjnnmk4DefWfol4XkjXjfgm3rqJjrp5fjiHLhgmvjinXomYfpn47rpJTsq5ztrqDtsKLjinbonYzjjHfkjHjcemLki3bpnIrpo5LgfmfnmojhhnHhh3Lnl4TqoZDdemLdf2nqopHqpZXfg23ihG7rpZbnloPhg2zdfWfhgWrrppbad2Hce2XaeGHsqJnjh3HrqJnsqprlk3lkXzrqpvnlYLrpZXpoZDig23pnYvtrJ3mloPhgGjsrJzqo5Lsq53nmIbnmIXsrJ3qpJPtrZqpZTmlYHtrppm4ntr6Hrp5jsr6DtsaN3l1VvAAACsklEQVR42u2Z+XPSQBSAA41iyBbopVarVhTUKhUBbQQtxatVbAPFUoWE6X1oIj1Ntb7wqMef7Cj+xJICR2dySY6877f3u7O7je7YY8HxyEIgiAIgiDBg5nC7+Cb3GutM3AtUpwi4QQ0S20euxR8PrcREX0ee1QaGsn9bS3Wag8RE9PuuXo4MspcPyX4TY4CA6LHboJI10WuzQZeDQZbHDagOHNRY7ECPQAR3QAR3QAR3QAR3QAR3QYXnW8rxoFDN23Km+6wjg6zHsIeA4UeqNtAw42mO2yiHXdD2Gvg0At1m2noN91hC+14K4QBA4cA1AVpuM10h+204x0Q9hk49EHdThqawzfBQ98Fw1DQ0KSFatRvSAmHTHfYQWJEoksUohH9UpCQ6Q6xvbTnfQM0lvbHdQrxhEQrDhyET3LQA0iCYMFIR5K1M9ENJGC8kOwFIcZbFJHYLSjx6AgNTyiKYwMqwrHT0BRioFD+iR0PqqWSGNyOJONZzNheUxSC50wDeM5Frv1KXBwB7Wi3ER+8vRkfqI23hk1b3qWyYkRUifCd65pm6lpaHP+Aptj66L6EU43SwxPqYnTSxFGR2f6spYYvmLYoMCrDa5KrM7va+pqkKJBoj4wU1Srx2fZXSKua8elyI+69AY3eC11eMWw4tMSZ7TtgS3UC446KY54Cm0CrUEm5XYixvU2nZX5egLgp35ssz5fm7QrEufZ2tSBxTpHv39YnyX+hvMg9yHGNiDx+R5Xj8ZJBjjvJ04VlTg7lERbHkXv8xctXhgbR5Oshqx4Xypu37yNcD+Q1XhrKM0+FTMlO7xcQzn6tfIiWLH1rcYjX9RsoyNz1UXOFpQcOOQUzj7seeyiAzqgAzqgw9wg8IhCIIgCIIgCIIgCIIgCIIgyHFTKdifb88m8QAAAAAElFTkSuQmCC););
                EscreverTXT.WriteLine($    background-size 100% 100%;);
                EscreverTXT.WriteLine($    margin0;);
                EscreverTXT.WriteLine($    padding0;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($div.videoImg {{);
                EscreverTXT.WriteLine($    width 100px;);
                EscreverTXT.WriteLine($    height 102px;);
                EscreverTXT.WriteLine($    background-imageurl(dataimagepng;base64,iVBORw0KGgoAAAANSUhEUgAAAIMAAACFCAAAAABdrxSnAAABIElEQVR42u3Wz27CMAzHcTsFqRIdu5QDe7C9x1pBxb1D22SPQBDYlbA2TtuZY+zc+Oq+igTBgAEDBgwYMGDAgAHDfzBszJV5uKS7Xjw8zLCciwbfc8hnUVNhkatCq2E0nl5cr8OxGhbT4cU5hHpZJEPNMHwhpunTeAlZq09mz+PQcXVcCuG5xmWz6zedVU1H1fNOxNDBgwYMDwRwwvrb+h9H3jbchj87ZX38okTV13cfk3JNlXm4GYjVsKwZiNbSWQMbu2Fbsh92QLIGEvhGJlQzr4ZRME1Ixi5KP+63W6VfzbKape73vA+LDDFJmdhYGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwH7DF6f1Qrdy7jA4AAAAAElFTkSuQmCC););
                EscreverTXT.WriteLine($    background-size 100% 100%;);
                EscreverTXT.WriteLine($    margin0;);
                EscreverTXT.WriteLine($    padding0;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($div.documento {{);
                EscreverTXT.WriteLine($    width 100px;);
                EscreverTXT.WriteLine($    height 102px;);
                EscreverTXT.WriteLine($    background-imageurl(dataimagepng;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAYAAADDPmHLAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAC8lJREFUeJztnVuMVdUdxr9v7eOAIxqLQUNNuGgldGJok1pMS8KMLRMOzBD6opbORQxJ30wbSWMsDCeHAxrbYGx8ahuiMozG9qESZ + CQwQAmPIC + SJUSx8tIYqgQwBg7CMxeXx  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););
                EscreverTXT.WriteLine($    background-size 100% 100%;);
                EscreverTXT.WriteLine($    margin0;);
                EscreverTXT.WriteLine($    padding0;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#topbar);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	positionfixed;);
                EscreverTXT.WriteLine($	background#475959;);
                EscreverTXT.WriteLine($	border1px solid #394d4d;);
                EscreverTXT.WriteLine($	width640px;);
                EscreverTXT.WriteLine($	z-index100;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#topbar.telegram);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	positionfixed;);
                EscreverTXT.WriteLine($	background#5682a3;);
                EscreverTXT.WriteLine($	border1px solid #394d4d;);
                EscreverTXT.WriteLine($	width640px;);
                EscreverTXT.WriteLine($	z-index100;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#topbar .left);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	float left;);
                EscreverTXT.WriteLine($	displayblock;);
                EscreverTXT.WriteLine($	margin0.5em;);
                EscreverTXT.WriteLine($	background#475959;);
                EscreverTXT.WriteLine($	color #ffffff;);
                EscreverTXT.WriteLine($	font-family 'Roboto-Medium';);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#topbar .right);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	float right;);
                EscreverTXT.WriteLine($	displayblock;);
                EscreverTXT.WriteLine($	margin0.5em;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#chatlist);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	width 640px;);
                EscreverTXT.WriteLine($	font-family 'Roboto-Light';);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#chatlist .contact);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	height 64px;);
                EscreverTXT.WriteLine($	background-color #ffffff;);
                EscreverTXT.WriteLine($	border-bottom 2px solid #e5e5e5;);
                EscreverTXT.WriteLine($	display block;);
                EscreverTXT.WriteLine($	padding 6px 6px;);
                EscreverTXT.WriteLine($	position relative;);
                EscreverTXT.WriteLine($	vertical-align top;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#chatlist .contacthover);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #1ba4d3;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#chatlist .left);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	float left;);
                EscreverTXT.WriteLine($	displayblock;);
                EscreverTXT.WriteLine($	margin0.5em;);
                EscreverTXT.WriteLine($	color #ffffff;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#chatlist .right);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	float right;);
                EscreverTXT.WriteLine($	displayblock;);
                EscreverTXT.WriteLine($	margin0.5em;);
                EscreverTXT.WriteLine($	color #666666;);
                EscreverTXT.WriteLine($	font-family 'Roboto-Light';);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	width 640px;);
                EscreverTXT.WriteLine($	font-family 'Roboto-Light';);
                EscreverTXT.WriteLine($	color #000000;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .linha);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	display block;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .linhaafter);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	content .;);
                EscreverTXT.WriteLine($    display block;);
                EscreverTXT.WriteLine($    height 0;);
                EscreverTXT.WriteLine($    clear both;);
                EscreverTXT.WriteLine($    visibility hidden;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .incoming, #conversation .outgoing,);
                EscreverTXT.WriteLine($ #conversation .date, #conversation .systemmessage, );
                EscreverTXT.WriteLine($ #conversation .specialmessage {{);
                EscreverTXT.WriteLine($	cursor pointer;);
                EscreverTXT.WriteLine($	border-radius 2px;);
                EscreverTXT.WriteLine($	box-shadow 0 0 6px #b8b7b7;);
                EscreverTXT.WriteLine($	display block;);
                EscreverTXT.WriteLine($	padding 10px 18px;);
                EscreverTXT.WriteLine($	vertical-align top;);
                EscreverTXT.WriteLine($	position relative;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .date);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #d7f5ff;);
                EscreverTXT.WriteLine($	color #000000;);
                EscreverTXT.WriteLine($	width 160px;);
                EscreverTXT.WriteLine($	margin 0 auto;);
                EscreverTXT.WriteLine($	text-align center;);
                EscreverTXT.WriteLine($	margin-top 8px;);
                EscreverTXT.WriteLine($	margin-bottom 8px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .systemmessage);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #f7f76e;);
                EscreverTXT.WriteLine($	color #333333;);
                EscreverTXT.WriteLine($	margin 0 auto;);
                EscreverTXT.WriteLine($	text-align center;);
                EscreverTXT.WriteLine($	margin-top 8px;);
                EscreverTXT.WriteLine($	margin-bottom 8px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .systemmessagehover);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #f0f075;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .pages);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #f4fa58;);
                EscreverTXT.WriteLine($	color #333333;);
                EscreverTXT.WriteLine($	width 300px;);
                EscreverTXT.WriteLine($	margin 0 auto;);
                EscreverTXT.WriteLine($	text-align center;);
                EscreverTXT.WriteLine($	margin-top 8px;);
                EscreverTXT.WriteLine($	margin-bottom 8px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .time);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	color #888888;);
                EscreverTXT.WriteLine($	float right;);
                EscreverTXT.WriteLine($	padding-left 12px;);
                EscreverTXT.WriteLine($	font-size small;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .incoming);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	color #000000;);
                EscreverTXT.WriteLine($	background-color #fcfbf6;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .incominghover);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #def1f3;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .incomingbefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #fcfbf6;);
                EscreverTXT.WriteLine($	content 00a0;);
                EscreverTXT.WriteLine($	display block;);
                EscreverTXT.WriteLine($	height 16px;);
                EscreverTXT.WriteLine($	position absolute;);
                EscreverTXT.WriteLine($	top 11px;);
                EscreverTXT.WriteLine($	transform         rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	-moz-transform    rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	-ms-transform     rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -o-transform      rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -webkit-transform rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	width  20px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .incominghoverbefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #def1f3;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .specialmessage);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	color #333333;);
                EscreverTXT.WriteLine($	background-color #66ccff;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .specialmessagehover);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #00aaff;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .specialmessagebefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #66ccff;);
                EscreverTXT.WriteLine($	content 00a0;);
                EscreverTXT.WriteLine($	display block;);
                EscreverTXT.WriteLine($	height 16px;);
                EscreverTXT.WriteLine($	position absolute;);
                EscreverTXT.WriteLine($	top 11px;);
                EscreverTXT.WriteLine($	transform         rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	-moz-transform    rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	-ms-transform     rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -o-transform      rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -webkit-transform rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	width  20px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .specialmessagehoverbefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #00aaff;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .outgoing);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #def1f3;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .outgoinghover);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #def1f3;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .outgoingbefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #def1f3;);
                EscreverTXT.WriteLine($	content 00a0;);
                EscreverTXT.WriteLine($	display block;);
                EscreverTXT.WriteLine($	height 16px;);
                EscreverTXT.WriteLine($	position absolute;);
                EscreverTXT.WriteLine($	top 11px;);
                EscreverTXT.WriteLine($	transform         rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -moz-transform    rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -ms-transform     rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -o-transform      rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($        -webkit-transform rotate(29deg) skew(-35deg););
                EscreverTXT.WriteLine($	width  20px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .outgoinghoverbefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	background-color #def1f3;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .to);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	float right; );
                EscreverTXT.WriteLine($	margin 5px 20px 5px 45px;);
                EscreverTXT.WriteLine($	text-align right;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .tobefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	box-shadow 2px -2px 2px 0 rgba(178,178,178,.4););
                EscreverTXT.WriteLine($	right -9px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .from);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	float left;);
                EscreverTXT.WriteLine($	margin 5px 45px 5px 20px; );
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .frombefore);
                EscreverTXT.WriteLine(${{);
                EscreverTXT.WriteLine($	box-shadow -2px 2px 2px 0 rgba(178,178,178,.4););
                EscreverTXT.WriteLine($	left -9px; );
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($#conversation .thumb {{);
                EscreverTXT.WriteLine($	max-width 160px;);
                EscreverTXT.WriteLine($	max-height 160px;);
                EscreverTXT.WriteLine($}});
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($            .tab {{display inline-block; border-collapse collapse; border 1px solid black;}});
                EscreverTXT.WriteLine($            .cel {{border-colapse colapse; border 1px solid black; font-family Arial, sans-serif;}});
                EscreverTXT.WriteLine($            .check {{vertical - align top;}});
                EscreverTXT.WriteLine($        style);
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($		script src='httpscode.jquery.comjquery-3.6.0.min.js'script);
                EscreverTXT.WriteLine($		script);
                EscreverTXT.WriteLine($			$(document).ready(function() {{);
                EscreverTXT.WriteLine($				$('body').on('click', '.audio', function (e) {{);
                EscreverTXT.WriteLine($					e.preventDefault(););
                EscreverTXT.WriteLine($					$('.audio-control').remove(););
                EscreverTXT.WriteLine($					$(this).prepend(audio controls class='audio-control'source src=' + $(this).attr('href') + ' type='audioogg'audiobr););
                EscreverTXT.WriteLine($				}}););
                EscreverTXT.WriteLine($			}}););
                EscreverTXT.WriteLine($		script);
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($    head);
                EscreverTXT.WriteLine($    body);
                EscreverTXT.WriteLine($        div id=topbar);
                EscreverTXT.WriteLine($          span class=left);
                try
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@caminho.caminhoIMG +  + caminho.JID + .j);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    EscreverTXT.WriteLine($img src=dataimagejpg;base64,{base64ImageRepresentation} width=40 height=40 );
                }
                catch
                {
                    string base64ImageRepresentation = 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;
                    EscreverTXT.WriteLine($img src=dataimagejpg;base64,{base64ImageRepresentation} width=40 height=40 );
                }
                EscreverTXT.WriteLine($            WhatsApp Chat - {caminho.JID} {caminho.GRUPO});
                EscreverTXT.WriteLine($          span);
                EscreverTXT.WriteLine($        div);
                EscreverTXT.WriteLine($        div id=conversationbrbrbr);
                EscreverTXT.WriteLine($        div class=linha);
                EscreverTXT.WriteLine($);

                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();

                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = SELECT  FROM messages LEFT JOIN message_media ON message_media.message_row_id = messages._id WHERE status != 6 and _id  1 and messages.key_remote_jid= + caminho.JID + ;

                sqlite_datareader = sqlite_cmd.ExecuteReader();

                int myreaderID;
                string KeyJID;
                int myreaderFROM;
                string myreaderDATA;
                string myreaderTYPE;
                Int64 myreaderTIME;
                string myreaderTIMEConvertido;
                string myreaderContatoGrupo;

                while (sqlite_datareader.Read())
                {

                    myreaderID = sqlite_datareader.GetInt32(0);  _id                  
                    KeyJID = sqlite_datareader.GetString(1);  key_remote_jid     


                    try
                    {
                        myreaderFROM = sqlite_datareader.GetInt32(2); key_from_me
                    }
                    catch
                    {
                        myreaderFROM = 0000;
                    }

                    try
                    {
                        myreaderDATA = sqlite_datareader.GetString(6); data
                    }
                    catch
                    {
                        try
                        {
                            myreaderDATA = sqlite_datareader.GetString(50); file_patch
                        }
                        catch
                        {
                            myreaderDATA = NULL;
                        }
                    }

                    try
                    {
                        myreaderTYPE = sqlite_datareader.GetString(10); media_wa_type
                    }
                    catch
                    {
                        myreaderTYPE = NULL;
                    }

                    try
                    {
                        myreaderTIME = sqlite_datareader.GetInt64(7); timestamp
                        myreaderTIMEConvertido = UnixTimeToDateTime(myreaderTIME).ToString();

                    }
                    catch
                    {
                        myreaderTIMEConvertido = 0000;
                    }

                    try
                    {
                        myreaderContatoGrupo = sqlite_datareader.GetString(20); remote_resource (Contato Grupo)
                    }
                    catch
                    {
                        myreaderContatoGrupo = NULL;
                    }

                    listBoxMESSAGE.Items.Add(myreaderDATA +    + myreaderTIMEConvertido);

                    if (myreaderTYPE == 2) Audio
                    {
                        if (myreaderFROM == 0) Recebido
                        {
                            EscreverTXT.WriteLine($div class=linha id=305672);
                            EscreverTXT.WriteLine($div class=incoming from);

                            if (caminho.GRUPO != .)
                            {
                                EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbra href='.{myreaderDATA}' class='audio play'{myreaderDATA}abr);
                            }
                            else
                            {
                                EscreverTXT.WriteLine($({caminho.JID})spanbra href='.{myreaderDATA}' class='audio play'{myreaderDATA}abr);
                            }

                            try
                            {
                                myreaderDATA = sqlite_datareader.GetString(50); file_patch
                            }
                            catch
                            {
                                myreaderDATA = NULL;
                            }

                            Transcricao
                            string fullPathTXT;
                            String str = myreaderDATA;
                            StringBuilder sb = new StringBuilder(str);
                            fullPathTXT = sb.Replace(.opus, .txt).ToString();

                            Link TXT Transcrição
                            if (checkBoxLinkaudio.Checked)
                            {
                                EscreverTXT.WriteLine($a  href=.{fullPathTXT}{fullPathTXT}a);
                            }

                            Plotagem Transcrição no HTML
                            if (checkBoxEscreverAudio.Checked)
                            {
                                EscreverTXT.WriteLine($iframe src=.{fullPathTXT} frameborder=0 scrolling=auto height=200 width=400iframe);
                            }

                            Transcrição Direta
                            if (checkBoxDuranteParser.Checked)
                            {
                                string fullPathTXT2;
                                String str2 = myreaderDATA;
                                StringBuilder sb2 = new StringBuilder(str2);
                                fullPathTXT2 = sb2.Replace(, ).ToString();
                                fullPathTXT2 = textBox2.Text +  + fullPathTXT2;

                                string pathListen = @binlisten;
                                string fullPath;
                                fullPath = Path.GetFullPath(pathListen);

                                Process process3 = new Process();
                                ProcessStartInfo startInfo3 = new ProcessStartInfo();
                                startInfo3.WindowStyle = ProcessWindowStyle.Hidden;
                                startInfo3.CreateNoWindow = true;
                                startInfo3.UseShellExecute = false;
                                startInfo3.RedirectStandardOutput = true;
                                startInfo3.WorkingDirectory = fullPath;
                                startInfo3.FileName = fullPath + listen.exe;
                                startInfo3.Arguments =   + fullPathTXT2 + ;
                                process3.StartInfo = startInfo3;
                                process3.Start();
                                process3.StandardOutput.ReadToEnd();
                                process3.Close();

                                string fullPathWAV;
                                String str3 = fullPathTXT2;
                                StringBuilder sb3 = new StringBuilder(str3);
                                fullPathWAV = sb3.Replace(.opus, .wav).ToString();
                                try
                                {
                                    File.Delete(fullPathWAV);
                                }
                                catch
                                {
                                }
                                
                            }

                            EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                            EscreverTXT.WriteLine($div class=audioImg title=Audiodiv);
                            EscreverTXT.WriteLine($a);
                            EscreverTXT.WriteLine($span class=time);
                            EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                            EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                            EscreverTXT.WriteLine($div class=delivereddiv);
                            EscreverTXT.WriteLine($span);
                            EscreverTXT.WriteLine($);
                            EscreverTXT.WriteLine($divdiv);
                            EscreverTXT.WriteLine($);
                        }
                        else Enviado
                        {
                            EscreverTXT.WriteLine($div class=linha id=305672);
                            EscreverTXT.WriteLine($div class=outgoing to);

                            EscreverTXT.WriteLine($spanbra href='.{myreaderDATA}' class='audio play'{myreaderDATA}abr);

                            try
                            {
                                myreaderDATA = sqlite_datareader.GetString(50); file_patch
                            }
                            catch
                            {
                                myreaderDATA = NULL;
                            }

                            Transcricao
                            string fullPathTXT;
                            String str = myreaderDATA;
                            StringBuilder sb = new StringBuilder(str);
                            fullPathTXT = sb.Replace(.opus, .txt).ToString();

                            Link TXT Transcrição
                            if (checkBoxLinkaudio.Checked)
                            {
                                EscreverTXT.WriteLine($a  href=.{fullPathTXT}{fullPathTXT}a);
                            }

                            Plotagem Transcrição no HTML
                            if (checkBoxEscreverAudio.Checked)
                            {
                                EscreverTXT.WriteLine($iframe src=.{fullPathTXT} frameborder=0 scrolling=auto height=200 width=400iframe);
                            }

                            Transcrição Direta
                            if (checkBoxDuranteParser.Checked)
                            {
                                string fullPathTXT2;
                                String str2 = myreaderDATA;
                                StringBuilder sb2 = new StringBuilder(str2);
                                fullPathTXT2 = sb2.Replace(, ).ToString();
                                fullPathTXT2 = textBox2.Text +  + fullPathTXT2;

                                string pathListen = @binlisten;
                                string fullPath;
                                fullPath = Path.GetFullPath(pathListen);

                                Process process3 = new Process();
                                ProcessStartInfo startInfo3 = new ProcessStartInfo();
                                startInfo3.WindowStyle = ProcessWindowStyle.Hidden;
                                startInfo3.CreateNoWindow = true;
                                startInfo3.UseShellExecute = false;
                                startInfo3.RedirectStandardOutput = true;
                                startInfo3.WorkingDirectory = fullPath;
                                startInfo3.FileName = fullPath + listen.exe;
                                startInfo3.Arguments =   + fullPathTXT2 + ;
                                process3.StartInfo = startInfo3;
                                process3.Start();
                                process3.StandardOutput.ReadToEnd();
                                process3.Close();

                                string fullPathWAV;
                                String str3 = fullPathTXT2;
                                StringBuilder sb3 = new StringBuilder(str3);
                                fullPathWAV = sb3.Replace(.opus, .wav).ToString();
                                try
                                {
                                    File.Delete(fullPathWAV);
                                }
                                catch
                                {
                                }
                                
                            }

                            EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                            EscreverTXT.WriteLine($div class=audioImg title=Audiodiv);
                            EscreverTXT.WriteLine($a);
                            EscreverTXT.WriteLine($span class=time);
                            EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                            EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                            EscreverTXT.WriteLine($div class=delivereddiv);
                            EscreverTXT.WriteLine($span);
                            EscreverTXT.WriteLine($);
                            EscreverTXT.WriteLine($divdiv);
                            EscreverTXT.WriteLine($);
                        }
                    }
                    else
                    {
                        if (myreaderTYPE == 1  myreaderTYPE == 20  myreaderTYPE == 45) Imagem Jpg = 1  webp = 20  jpeg = 45
                        {
                            if (myreaderFROM == 0) Recebido
                            {
                                EscreverTXT.WriteLine($div class=linha id=305644);
                                EscreverTXT.WriteLine($div class=incoming from);
                                EscreverTXT.WriteLine($span style=font-family 'Roboto-Medium'; color #b4c74b;);

                                if (caminho.GRUPO != .)
                                {
                                    EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                }
                                else
                                {
                                    EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                }

                                try
                                {
                                    myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                }
                                catch
                                {
                                    myreaderDATA = NULL;
                                }

                                EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                EscreverTXT.WriteLine($img class=thumb src=.{myreaderDATA} title=image);
                                EscreverTXT.WriteLine($a);
                                EscreverTXT.WriteLine($span class=time{myreaderTIMEConvertido} &nbsp; span);
                                EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                                EscreverTXT.WriteLine($span);
                                EscreverTXT.WriteLine($);
                                EscreverTXT.WriteLine($divdiv);
                                EscreverTXT.WriteLine($);
                            }
                            else Enviado
                            {
                                EscreverTXT.WriteLine($div class=linha id=305644);
                                EscreverTXT.WriteLine($div class=outgoing to);
                                EscreverTXT.WriteLine($span style=font-family 'Roboto-Medium'; color #b4c74b;spanbr);

                                EscreverTXT.WriteLine($spanbr{myreaderDATA}br);

                                try
                                {
                                    myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                }
                                catch
                                {
                                    myreaderDATA = NULL;
                                }

                                EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                EscreverTXT.WriteLine($img class=thumb src=.{myreaderDATA} title=image);
                                EscreverTXT.WriteLine($a);
                                EscreverTXT.WriteLine($span class=time{myreaderTIMEConvertido} &nbsp; span);
                                EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                                EscreverTXT.WriteLine($span);
                                EscreverTXT.WriteLine($);
                                EscreverTXT.WriteLine($divdiv);
                                EscreverTXT.WriteLine($);
                            }
                        }
                        else
                        {
                            if (myreaderTYPE == 3  myreaderTYPE == 13) Video MP4 = 3  MP4 (GIF) = 13 
                            {

                                if (myreaderFROM == 0) Recebido
                                {
                                    EscreverTXT.WriteLine($div class=linha id=305672);
                                    EscreverTXT.WriteLine($div class=incoming from);

                                    if (caminho.GRUPO != .)
                                    {
                                        EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                    }
                                    else
                                    {
                                        EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                    }

                                    try
                                    {
                                        myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                    }
                                    catch
                                    {
                                        myreaderDATA = NULL;
                                    }

                                    EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                    EscreverTXT.WriteLine($div class=videoImg title=Videodiv);
                                    EscreverTXT.WriteLine($a);
                                    EscreverTXT.WriteLine($span class=time);
                                    EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                    EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                                    EscreverTXT.WriteLine($div class=delivereddiv);
                                    EscreverTXT.WriteLine($span);
                                    EscreverTXT.WriteLine($);
                                    EscreverTXT.WriteLine($divdiv);
                                    EscreverTXT.WriteLine($);

                                }
                                else Enviado
                                {
                                    EscreverTXT.WriteLine($div class=linha id=305672);
                                    EscreverTXT.WriteLine($div class=outgoing to);

                                    EscreverTXT.WriteLine($spanbr{myreaderDATA}br);

                                    try
                                    {
                                        myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                    }
                                    catch
                                    {
                                        myreaderDATA = NULL;
                                    }

                                    EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                    EscreverTXT.WriteLine($div class=videoImg title=Videodiv);
                                    EscreverTXT.WriteLine($a);
                                    EscreverTXT.WriteLine($span class=time);
                                    EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                    EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                                    EscreverTXT.WriteLine($div class=delivereddiv);
                                    EscreverTXT.WriteLine($span);
                                    EscreverTXT.WriteLine($);
                                    EscreverTXT.WriteLine($divdiv);
                                    EscreverTXT.WriteLine($);
                                }
                            }
                            else
                            {
                                if (myreaderTYPE == 9) Documento PDF  RAR  EXE  (Qualquer Formato) 
                                {

                                    if (myreaderFROM == 0) Recebido
                                    {
                                        EscreverTXT.WriteLine($div class=linha id=305672);
                                        EscreverTXT.WriteLine($div class=incoming from);

                                        if (caminho.GRUPO != .)
                                        {
                                            EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                        }
                                        else
                                        {
                                            EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                        }

                                        try
                                        {
                                            myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                        }
                                        catch
                                        {
                                            myreaderDATA = NULL;
                                        }

                                        EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                        EscreverTXT.WriteLine($div class=documento title=Documentodiv);
                                        EscreverTXT.WriteLine($a);
                                        EscreverTXT.WriteLine($span class=time);
                                        EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                        EscreverTXT.WriteLine($span class=timeReceived &nbsp; span);
                                        EscreverTXT.WriteLine($div class=delivereddiv);
                                        EscreverTXT.WriteLine($span);
                                        EscreverTXT.WriteLine($);
                                        EscreverTXT.WriteLine($divdiv);
                                        EscreverTXT.WriteLine($);

                                    }
                                    else Enviado
                                    {
                                        EscreverTXT.WriteLine($div class=linha id=305672);
                                        EscreverTXT.WriteLine($div class=outgoing to);

                                        EscreverTXT.WriteLine($spanbr{myreaderDATA}br);

                                        try
                                        {
                                            myreaderDATA = sqlite_datareader.GetString(50); file_patch
                                        }
                                        catch
                                        {
                                            myreaderDATA = NULL;
                                        }

                                        EscreverTXT.WriteLine($a  href=.{myreaderDATA});
                                        EscreverTXT.WriteLine($div class=documento title=Documentodiv);
                                        EscreverTXT.WriteLine($a);
                                        EscreverTXT.WriteLine($span class=time);
                                        EscreverTXT.WriteLine(${myreaderTIMEConvertido} &nbsp;);
                                        EscreverTXT.WriteLine($span class=timeSent &nbsp; span);
                                        EscreverTXT.WriteLine($div class=delivereddiv);
                                        EscreverTXT.WriteLine($span);
                                        EscreverTXT.WriteLine($);
                                        EscreverTXT.WriteLine($divdiv);
                                        EscreverTXT.WriteLine($);
                                    }
                                }
                                else
                                {
                                    Texto
                                    if (myreaderFROM == 0) Recebido
                                    {
                                        EscreverTXT.WriteLine($div class=linha id=307748);
                                        EscreverTXT.WriteLine($div class=incoming from);
                                        EscreverTXT.WriteLine($span style=font-family 'Roboto-Medium'; color #b4c74b;);

                                        if (caminho.GRUPO != .)
                                        {
                                            EscreverTXT.WriteLine($({myreaderContatoGrupo})spanbr{myreaderDATA}br);
                                        }
                                        else
                                        {
                                            EscreverTXT.WriteLine($({caminho.JID})spanbr{myreaderDATA}br);
                                        }

                                        EscreverTXT.WriteLine($span class=time{myreaderTIMEConvertido} &nbsp; span);
                                        EscreverTXT.WriteLine($brspan class=timeReceived &nbsp;div class=delivereddivspan);
                                        EscreverTXT.WriteLine($);
                                        EscreverTXT.WriteLine($divdiv);
                                        EscreverTXT.WriteLine($);
                                    }
                                    else Enviado
                                    {
                                        EscreverTXT.WriteLine($div class=linha id=307746);
                                        EscreverTXT.WriteLine($div class=outgoing to);
                                        EscreverTXT.WriteLine(${myreaderDATA}brspan class=time{myreaderTIMEConvertido} &nbsp;div class=delivereddivspan);
                                        EscreverTXT.WriteLine($brspan class=timeSent &nbsp;div class=delivereddivspan);
                                        EscreverTXT.WriteLine($);
                                        EscreverTXT.WriteLine($divdiv);
                                        EscreverTXT.WriteLine($);
                                    }
                                }
                            }
                        }
                    }
                }
                EscreverTXT.WriteLine($);
                EscreverTXT.WriteLine($        brbrbr);
                EscreverTXT.WriteLine($        div id=lastmsg&nbsp;div);
                EscreverTXT.WriteLine($    body);
                EscreverTXT.WriteLine($html);

                sqlite_conn.Close();

                EscreverTXT.Close();

                
                

                button3_Click(null, null);
            }
            listBoxMESSAGEMEDIA.Enabled = true;
            listBoxMESSAGE.Items.Clear();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox2.Visible = false;
            tabControl1.Enabled = true;
        }

        private void checkBoxDuranteParser_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxLinkaudio.Checked = true;
            checkBoxEscreverAudio.Checked = true;
        }

        private void WhatsParserAntigocs_Load(object sender, EventArgs e)
        {
            string pathTEMP = @temp;
            string fullPathTEMP;
            fullPathTEMP = Path.GetFullPath(pathTEMP);

            System.IO.StreamReader file = new System.IO.StreamReader(fullPathTEMP + PathAcquisition.txt);
            string paths = @file.ReadLine();            
            textBoxTEMP.Text = paths; 
            file.Close();

            try
            {
                string[] dirs2 = Directory.GetDirectories(@textBoxTEMP.Text, WhatsApp, SearchOption.AllDirectories);
                foreach (string dir2 in dirs2)
                {
                    textBox2.Text = @dir2;
                    caminho.caminhoLOCAL = @dir2;

                    listBox1.Items.Add(@dir2);
                }
            }
            catch { }

            try
            {
                string[] dirs = Directory.GetDirectories(@textBoxTEMP.Text, Avatars, SearchOption.AllDirectories);
                foreach (string dir in dirs)
                {
                    textBox1.Text = @dir;
                    caminho.caminhoIMG = @dir;
                }
            }
            catch { }

            try 
            {
                DirectoryInfo Dir = new DirectoryInfo(textBoxTEMP.Text);
                FileInfo[] Files = Dir.GetFiles(msgstore.db, SearchOption.AllDirectories);
                foreach (FileInfo File in Files)
                {
                    textBox3.Text = @File.FullName.ToString();
                    caminho.caminhoDB = @File.FullName.ToString();
                }
            }
            catch { }  
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = @listBox1.Text;
            caminho.caminhoLOCAL = @listBox1.Text;
        }
    }
}
