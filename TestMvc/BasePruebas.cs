using System.Web.Mvc;
using SubidaFicherosMVC.Controllers;
using SubidaFicherosMVC.Models;

namespace TestMvc
{
    public abstract class BasePruebas
    {
        protected HomeController Controller;
        protected FicherosEntities db;

        protected BasePruebas()
        {
            db = new FicherosEntities();
            Controller = new HomeController();
        }
    }
}