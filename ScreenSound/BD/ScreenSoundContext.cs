using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    internal class ScreenSoundContext : DbContext   
    {
        string connectionString = "Data Source=DESKTOP-5A72BGU\\SQLEXPRESS;Initial Catalog= ScreenSound;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }
    }
}
