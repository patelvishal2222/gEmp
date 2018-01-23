using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using  System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Models
{
    public class Emp 
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public virtual City  city {get;set;}

    }


    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
    //public class Employee : DbContext
    //{

    //    public Employee()
    //        : base("emp")
    //    {

    //        Bemp b = new Bemp();

    //    }

    //    public virtual DbSet<Emp> Emp { get; set; }
    //}


  public  class Connection
    {
       protected  SqlConnection cn;
        public Connection()
        {

            string strcn = System.Configuration.ConfigurationManager.ConnectionStrings["emp"].ToString();


            cn = new SqlConnection(strcn); 

        }
    }
    public class Bemp :Connection
    {

        

        public bool insertUpdate(Emp emp)
        {
            String sql = string.Empty;
            if (emp.EmpId == 0)

                sql = "Insert into emp (empname,address,CityId) values ('" + emp.EmpName + "','" + emp.Address + "'," + emp.CityId + ")";
            else
                sql = "update emp set empname='" + emp.EmpName + "',address='" + emp.Address + "',CityId=" + emp.CityId + " where empId=" + emp.EmpId.ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            

            try
            {
                cn.Open();
                cmd.CommandText = sql;
                int row = (int)cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cn.Close();

            }
            return true;

        }
        public bool Delete(int? Id)
        {
            String sql = "Delete Emp  where EmpId=" + Id.ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            

            try
            {
                cn.Open();
                cmd.CommandText = sql;
                int row= (int)cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                cn.Close();

            }

            return true;
        }
        public Emp Find(int ?Id)
        {
            String sql = "Select *  from Emp  where EmpId=" + Id.ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
               Emp emp=new Emp();
            
            try{
                cn.Open();
                cmd.CommandText = sql;
              SqlDataReader read=cmd.ExecuteReader();
               if(read.Read())
               {

                   emp = Get(read);
                  
               }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                cn.Close();

            }
            

             
            return emp;

        }

        public Emp Get(SqlDataReader read)
        {
            Emp emp = new Emp();
            emp.EmpId = Convert.ToInt32(read["EmpId"]);
            emp.EmpName = read["EmpName"].ToString();
            emp.Address = read["Address"].ToString();
            emp.CityId = read["CityId"] is DBNull?0:Convert.ToInt32(read["CityId"]);
            emp.city = new BCity().Find(emp.CityId);
            return emp;
        }
        public List<Emp> ToList()
        {


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            List<Emp> emps = new List<Emp>();
            String sql = "Select *  from Emp";
            try
            {
                cn.Open();
                cmd.CommandText = sql;
                SqlDataReader read = cmd.ExecuteReader();
                
               while (read.Read())
                {
                    Emp emp = new Emp();
                    emp = Get(read);
                    emps.Add(emp);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                cn.Close();

            }



            return emps;

        }
    }

public class BCity:Connection
{
    public List<City> ToList()
    {


        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cn;
        List<City> Citys = new List<City>();
        String sql = "Select *  from City";
        try
        {
            cn.Open();
            cmd.CommandText = sql;
            SqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                City city = new City();
                city = Get(read);
                Citys.Add(city);
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            cn.Close();

        }



        return Citys;
    }

    public City Find(int? Id)
    {
        String sql = "Select *  from city  where cityId=" + Id.ToString();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cn;
        City city = new City();

        try
        {
            cn.Open();
            cmd.CommandText = sql;
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {

                city = Get(read);

            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            cn.Close();

        }



        return city;

    }

    public City Get(SqlDataReader read)
    {
        City City = new City();

        City.CityName = read["CityName"].ToString();

        City.CityId = Convert.ToInt32(read["CityId"].ToString());
        return City;
    }
    }


}