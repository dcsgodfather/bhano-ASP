using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection("Data Source=KISHAN\\SQLEXPRESS;Initial Catalog=nandani;Integrated Security=True");
   

    protected void Page_Load(object sender, EventArgs e)
    {
        fillGridView();
       
        
    }

    protected void btn_insert_Click(object sender, EventArgs e)
    {
        cn.Open();
        string ename = txtename.Text;
         int salary = Convert.ToInt32(txtsalary.Text);
         DateTime dob = Convert.ToDateTime(txtdob.Text);
        string dept = DropDownList1.SelectedItem.ToString();
        string qry = "insert into employee values('"+ ename + "','"+ salary + "','" + dob + "','" + dept + "')";
        SqlCommand cmd = new SqlCommand(qry,cn);
        cmd.ExecuteNonQuery();
        fillGridView();
        clear();
        cn.Close();
        

    }

    private void fillGridView()
    {
        string qry = "select * from employee";
        SqlDataAdapter da = new SqlDataAdapter(qry, cn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        cn.Open();
        int eid = Convert.ToInt32(txtid.Text);
        string ename = txtename.Text;
        int salary = Convert.ToInt32(txtsalary.Text);
        DateTime dob = Convert.ToDateTime(txtdob.Text);
        string dept = DropDownList1.SelectedItem.ToString();
        string qry = "update employee set ename='"+ename + "',salary='"+ salary +"', dob='" + dob + "',dept='" + dept + "' where eid='" + eid +"'";
        SqlCommand cmd = new SqlCommand(qry, cn);
        cmd.ExecuteNonQuery();
        fillGridView();
        clear();
        cn.Close();
       
    }

    public void clear()
    {
        txtid.Text = "";
        txtename.Text = " ";
        txtdob.Text = "";
        txtsalary.Text = "";

    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        cn.Open();
        int eid = Convert.ToInt32(txtid.Text);
        string qry = "delete from employee where eid='" + eid +"'";
        SqlCommand cmd = new SqlCommand(qry, cn);
        cmd.ExecuteNonQuery();
        fillGridView();
        clear();
        cn.Close();
        
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        Label6.Text = "";
        cn.Open();
        int eid = Convert.ToInt32(txtid.Text);
        string qry = "select *  from employee where eid='" + eid + "'";
        SqlCommand cmd = new SqlCommand(qry, cn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            txtename.Text = dr[1].ToString();
            txtsalary.Text = dr[2].ToString();
            txtdob.Text = dr[3].ToString();
            DropDownList1.SelectedItem.Text = dr[4].ToString();
            
        }
        else
        {
            Label6.Text = "not found";
            clear();
        }
       
       
        cn.Close();
       

    }
}