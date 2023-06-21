using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ControlAsistencia
{
    class Conexion
    {

        public static SqlConnection Conectar()
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=localhost;Initial Catalog=ControlAsistencia;User ID=sa;Password=123";
            cn.Open();
            return cn;



        }

    }
}
