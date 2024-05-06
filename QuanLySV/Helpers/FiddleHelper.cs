using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.Helpers
{
    public class FiddleHelper
    {
        public static string GetConnectionStringSQLServer()
        {
            return "Data Source=Keydiaz;Initial Catalog=StudentManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True";
        }
    }
}