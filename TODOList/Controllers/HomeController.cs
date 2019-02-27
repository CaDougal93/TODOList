using System.Linq;
using System.Web.Mvc;
using TODOList.DAL;
using TODOList.Models;
using TODOList.Models.ViewModels;

namespace TODOList.Controllers
{
    public class HomeController : Controller
    {
        TODOViewModel vm;

        public ActionResult Index()
        {
            vm = new TODOViewModel();

            using (var context = new TODOItemContext())
            {
                var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                vm.TODOList = context.TODOItems.ToList();
                vm.TODOJSON = jsonSerializer.Serialize(vm.TODOList);
            }

                return View(vm);
        }

        public TODOItem Get(int id)
        {
            using (var context = new TODOItemContext())
            {
                return context.TODOItems.ToList().Where(x => x.TODOItemID == id).FirstOrDefault();
            }
        }

        public void Delete(int id)
        {
            using (var context = new TODOItemContext())
            {
                var itemToDelete = context.TODOItems.Where(x => x.TODOItemID == id).FirstOrDefault();
                context.TODOItems.Remove(itemToDelete);
                context.SaveChanges();
            }
        }

        public void Update(int id, string todoItemName)
        {
            using (var context = new TODOItemContext())
            {
                context.TODOItems.Where(x => x.TODOItemID == id && x.TODOItemName != todoItemName).FirstOrDefault().TODOItemName = todoItemName;
                context.SaveChanges();
            }
        }

        public void Complete(int id, bool isComplete)
        {
            using (var context = new TODOItemContext())
            {
                context.TODOItems.Where(x => x.TODOItemID == id && x.IsComplete != isComplete).FirstOrDefault().IsComplete = isComplete;
                context.SaveChanges();
            }
        }

        public void Add(string todoItemName)
        {
            using (var context = new TODOItemContext())
            {
                if(!context.TODOItems.Where(x => x.TODOItemName == todoItemName).Any() && !string.IsNullOrEmpty(todoItemName))
                {
                    TODOItem newItem = new TODOItem
                    {
                        TODOItemName = todoItemName,
                        IsComplete = false
                    };

                    context.TODOItems.Add(newItem);
                    context.SaveChanges();
                }
            }
        }
    }
}