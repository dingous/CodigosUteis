using PagueVeloz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PagueVeloz.ViewModels
{
    public class HomeViewModel : Controller
    {

        public List<EMPRESA> EmpresaList { get; set; }
      

    }
}
