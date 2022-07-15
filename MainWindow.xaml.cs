using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;//WPF定时器
using System.Data;
using System.Data.SqlClient;

namespace WPF2022
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//全局变量
		bool indnx_one = false;
		bool indnx_two = true;
		bool indnx_there = true;
		public MainWindow()
		{
			//初始化
			InitializeComponent();

			//警示状态栏
			Bgm_color_get();
		}

		public void Bgm_color_get()
        {
			view_1.Background = Brushes.Black;
			view_2.Background = Brushes.Red;
			view_3.Background = Brushes.Black;
			out_2.IsEnabled = false;
			out_3.IsEnabled = false;
			out_4.IsEnabled = false;
			out_5.IsEnabled = false;
			Alarm.Text = "系统未启动......" + "\n";
		}

		//启动按钮
        private void but_2_Click(object sender, RoutedEventArgs e)
        {
			indnx_one=true;
			Alarm.Text= "系统已启动.....";
			view_1.Background = Brushes.Green;
			view_2.Background = Brushes.Black;
			but_2.IsEnabled= false;
			
		}

		//查询按钮
        private void but_1_Click(object sender, RoutedEventArgs e)
        {
			if (indnx_one)
			{
				string name_user = input_name1.Text;
				if (name_user == "")
				{
					MessageBox.Show("输入不能为空！");
				}
				else
				{
					Get_sql_server();
				}
			}
			else
			{
				MessageBox.Show("pls to run system!");
			}
        }

		//images设置
		public void images_get(string? loadimg)
        {
			//string loadimg = "https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fp0.itc.cn%2Fq_70%2Fimages01%2F20210719%2F8fecb5c42a3b48fa9ba8ea36acc164a0.jpeg&refer=http%3A%2F%2Fp0.itc.cn&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1658732936&t=817ba819577f04eddb1a23e435e6e02b";

			//string loadimg = "F:/VS2022_Code/source/repos/Code/WPF2022/WPF2022/Imges/liuke.jpg";
			BitmapImage bmp = new BitmapImage(new Uri(loadimg));
			images_view.Source = bmp;
        }

		//connet SQL Server
		public void Get_sql_server()
		{
			string D_xt =input_name1.Text;
			try
			{
				//连接loacel sql server
				string connStr = "Data Source=AE86-LIUKE;Initial Catalog=Test_WPF;User ID=sa;Pwd=xiaolan520126";
				string select_sql = "Select * from TUser where TName ='" + D_xt + "';";
				SqlConnection Connet_sql = new SqlConnection(connStr);
				Connet_sql.Open();//构造函数open sql server 

				SqlCommand cmd = new SqlCommand(select_sql, Connet_sql);//执行数据库查询函数
				SqlDataReader checkstr=cmd.ExecuteReader();//只读取model(ExecuteReader)

				while (checkstr.Read())//每行读取
				{
					//Alarm.AppendText(checkstr["Gonghao"].ToString() + "\r\n");
					out_2.Text = checkstr["Xb"].ToString();//填充字符串
					out_3.Text = checkstr["Gonghao"].ToString();
					out_4.Text = checkstr["Bm"].ToString();
					out_5.Text = checkstr["Zhiw"].ToString();
					images_get(checkstr["img"].ToString());
				}
				checkstr .Close();
				Connet_sql.Close();//关闭连接
			}
			catch (Exception ex)//捕获异常
			{
				MessageBox.Show("错误信息：" + ex.Message, "SQL连接失败");
			}
		}
	}
}
