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
                vm.TODOList = context.TODOItems.ToList();
            }

                return View(vm);
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
                if (context.TODOItems.Where(x => x.TODOItemID == id && x.TODOItemName != todoItemName).Any())
                {
                    context.TODOItems.Where(x => x.TODOItemID == id).FirstOrDefault().TODOItemName = todoItemName;
                    context.SaveChanges();
                }
            }
        }

        public void Complete(int id, bool isComplete)
        {
            using (var context = new TODOItemContext())
            {
                if (context.TODOItems.Where(x => x.TODOItemID == id && x.IsComplete != isComplete).Any())
                {
                    context.TODOItems.Where(x => x.TODOItemID == id).FirstOrDefault().IsComplete = isComplete;
                    context.SaveChanges();
                }
            }
        }

        public string Add(string todoItemName)
        {
            TODOItem newItem = new TODOItem
            {
                TODOItemName = todoItemName,
                IsComplete = false
            };

            using (var context = new TODOItemContext())
            {
                if(!context.TODOItems.Where(x => x.TODOItemName == todoItemName).Any() && !string.IsNullOrEmpty(todoItemName))
                {
                    newItem = context.TODOItems.Add(newItem);
                    context.SaveChanges();

                    return Newtonsoft.Json.JsonConvert.SerializeObject(newItem);
                }
            }
            return string.Empty;
        }
    }
}