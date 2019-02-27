using System.Collections.Generic;

namespace TODOList.Models.ViewModels
{
    public class TODOViewModel
    {
        public List<TODOItem> TODOList { get; set; }
        public string TODOJSON { get; set; }
    }
}